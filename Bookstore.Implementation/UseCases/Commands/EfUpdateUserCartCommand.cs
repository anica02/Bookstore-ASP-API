using Bookstore.Application;
using Bookstore.Application.Exceptions;
using Bookstore.Application.UseCases.Commands;
using Bookstore.Application.UseCases.DTO;
using Bookstore.DataAccess;
using Bookstore.Domain.Entities;
using Bookstore.Implementation.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Implementation.UseCases.Commands
{
    public class EfUpdateUserCartCommand : EfUseCase, IUpdateUserCartCommand
    {
        private readonly IApplicationActor _actor;
        private readonly CreateUserCartValidator _validator;

       

        public EfUpdateUserCartCommand(
            BookstoreContext context,
            IApplicationActor actor,
            CreateUserCartValidator validator
            ):base(context)
        {
            _actor = actor;
            _validator = validator;
        }
        public int Id => 15;

        public string Name => "Cart edit";

        public string Description => "";

        public void Execute(CreateUserCartDto request)
        {
            var cart = Context.Carts.Include(x=>x.CartItems).FirstOrDefault(x => x.Id == request.Id.Value && x.IsActive && x.UserId==_actor.Id);

            if (cart == null || cart.DeletedAt.HasValue)
            {
                throw new EntityNotFoundException(request.Id.Value, "cart");

            }

            _validator.ValidateAndThrow(request);


            if (!request.CartItems.Any())
            {
                var remove = Context.CartItems.Where(x => x.CartId == cart.Id);
                Context.CartItems.RemoveRange(remove);

                Context.Carts.Remove(cart);
                cart.ModifiedAt = DateTime.UtcNow;
                cart.ModifiedBy = _actor.Username;
                Context.Entry(cart).State = EntityState.Modified;

                Context.SaveChanges();
            }
            else
            {
                var userCart = Context.Carts.Include(x => x.CartItems).FirstOrDefault(x => x.UserId == _actor.Id && x.IsActive);

              
                var itemsToRemove = new List<CartItem>();

                foreach (var itemInDb in userCart.CartItems.ToList()) 
                {
                    var matchingItem = request.CartItems.FirstOrDefault(item => item.BookPublisherId == itemInDb.BookPublisherId);

                    if (matchingItem != null)
                    {
                      
                        if (itemInDb.Quantity != matchingItem.Quantity)
                        {
                            itemInDb.Quantity = matchingItem.Quantity;
                            itemInDb.ModifiedAt = DateTime.UtcNow;
                            itemInDb.ModifiedBy = _actor.Username;
                            Context.Entry(itemInDb).State = EntityState.Modified;
                        }
                    }
                    else
                    {
                       
                        itemsToRemove.Add(itemInDb);
                    }
                }

              
                foreach (var itemToRemove in itemsToRemove)
                {
                    userCart.CartItems.Remove(itemToRemove);
                    Context.Entry(itemToRemove).State = EntityState.Deleted;
                }

                userCart.ModifiedAt = DateTime.UtcNow;
                userCart.ModifiedBy = _actor.Username;
                Context.Entry(userCart).State = EntityState.Modified;

                Context.SaveChanges();

            }


        }
    }
}
