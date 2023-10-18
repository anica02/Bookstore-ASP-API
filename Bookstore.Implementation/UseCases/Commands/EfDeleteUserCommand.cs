using Bookstore.Application;
using Bookstore.Application.Exceptions;
using Bookstore.Application.UseCases.Commands;
using Bookstore.Application.UseCases.DTO;
using Bookstore.DataAccess;
using Bookstore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Implementation.UseCases.Commands
{
    public class EfDeleteUserCommand:EfUseCase, IDeleteUserCommand
    {
        private readonly IApplicationActor _actor;

        public EfDeleteUserCommand(
             BookstoreContext context,
            IApplicationActor actor
           ) : base(context)
        {
            _actor = actor;
        }

        public int Id => 23;

        public string Name => "User delete";

        public string Description => "";

        public void Execute(DeleteEntityDto request)
        {
            var user = Context.Users.Include(x => x.Orders).Include(x=>x.Carts).ThenInclude(b=>b.CartItems).FirstOrDefault(x => x.Id == request.Id); 

            if (user == null)
            {
                throw new EntityNotFoundException(request.Id, "user");
            }

            if (user.Orders.Any(x=>x.IsActive))
            {
                throw new ConflictException(request.Id, "user", "User cannot be deleted because his orders aren't  marked as delivered ");
            }

            if (user.Carts.Any(x=>x.IsActive))
            {
                throw new ConflictException(request.Id, "user", "User cannot be deleted because he has a cart which is active");
            }

            Context.Users.Remove(user);
            Context.Entry(user).State = EntityState.Deleted;
         
            Context.SaveChanges();
            /*
            user.IsActive = false;
            user.DeletedAt = DateTime.UtcNow;
            user.DeletedBy = _actor.Username;
            Context.SaveChanges();
            */

        }
    }
}
