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
    public class EfCreateUserCartCommand: EfUseCase , ICreateUserCartCommand
    {
        private readonly IApplicationActor _actor;
        private readonly CreateUserCartValidator _validator;
        public EfCreateUserCartCommand(BookstoreContext context, IApplicationActor actor,
            CreateUserCartValidator validator):base(context)
        {
            _actor = actor;
            _validator = validator;
        }
        public int Id => 14;

        public string Name => "Cart create";

        public string Description => "";

        public void Execute(CreateUserCartDto request)
        {
            var userHasCart = Context.Carts.Any(x => x.UserId == _actor.Id && x.IsActive);



            _validator.ValidateAndThrow(request);


            if (!request.CartItems.Any())
            {
                throw new ConflictExceptionCreating("cart", $"No items were added to the cart");
            }

            if (!userHasCart)
            {
                Cart cart = new Cart();
                cart.UserId = _actor.Id;
                Context.Carts.Add(cart);
                Context.SaveChanges();

                foreach (var item in request.CartItems)
                {
                   
                    var existingCartItem = Context.CartItems.FirstOrDefault(x =>
                        x.CartId == cart.Id && x.BookPublisherId == item.BookPublisherId);

                    if (existingCartItem != null)
                    {
                       
                        existingCartItem.Quantity += item.Quantity;
                        Context.Entry(existingCartItem).State = EntityState.Modified;
                    }
                    else
                    {
                        
                        CartItem newItem = new CartItem
                        {
                            BookPublisherId = item.BookPublisherId,
                            Quantity = item.Quantity,
                            CartId = cart.Id
                        };
                        Context.CartItems.Add(newItem);
                    }
                }

                Context.SaveChanges();
            }
            else
            {

                var userCart = Context.Carts.Include(x => x.CartItems).FirstOrDefault(x => x.UserId == _actor.Id && x.IsActive);

                if (userCart.CartItems.Any())
                {
                    foreach (var item in request.CartItems)
                    {
                        var existingItem = userCart.CartItems.FirstOrDefault(itemInDb => itemInDb.BookPublisherId == item.BookPublisherId);

                        if (existingItem != null)
                        {
                           
                            existingItem.Quantity += item.Quantity;
                            existingItem.ModifiedAt = DateTime.UtcNow;
                            existingItem.ModifiedBy = _actor.Username;
                            Context.Entry(existingItem).State = EntityState.Modified;
                        }
                        else
                        {
                           
                            var newItem = new CartItem
                            {
                                BookPublisherId = item.BookPublisherId,
                                Quantity = item.Quantity,
                                CartId = userCart.Id
                            };
                            userCart.CartItems.Add(newItem);
                            Context.Entry(userCart).State = EntityState.Modified;
                        }
                    }
                }
                else
                {
                    
                    foreach (var item in request.CartItems)
                    {
                        var newItem = new CartItem
                        {
                            BookPublisherId = item.BookPublisherId,
                            Quantity = item.Quantity,
                            CartId = userCart.Id
                        };
                        userCart.CartItems.Add(newItem);
                    }
                    Context.Entry(userCart).State = EntityState.Modified;
                }

                Context.SaveChanges();


            }
        }

        
    }
}
