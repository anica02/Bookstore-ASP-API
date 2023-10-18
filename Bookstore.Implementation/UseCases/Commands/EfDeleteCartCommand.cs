using Bookstore.Application;
using Bookstore.Application.Exceptions;
using Bookstore.Application.UseCases.Commands;
using Bookstore.Application.UseCases.DTO;
using Bookstore.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Implementation.UseCases.Commands
{
    public class EfDeleteCartCommand:EfUseCase, IDeleteCartCommand
    {
        private readonly IApplicationActor _actor;

        public EfDeleteCartCommand(
             BookstoreContext context,
            IApplicationActor actor
           ) : base(context)
        {
            _actor = actor;
        }

        public int Id => 25;
        public string Name => "Cart delete";

        public string Description => "";

        public void Execute(DeleteEntityDto request)
        {

            var cart = Context.Carts.Include(x => x.CartItems).FirstOrDefault(x => x.Id == request.Id);

            if (cart == null)
            {
                throw new EntityNotFoundException(request.Id, "cart");
            }

            Context.CartItems.RemoveRange(cart.CartItems);
            Context.Entry(cart).State = EntityState.Deleted;
            //Context.Carts.Remove(cart);
            Context.SaveChanges();
        }
    }
}
