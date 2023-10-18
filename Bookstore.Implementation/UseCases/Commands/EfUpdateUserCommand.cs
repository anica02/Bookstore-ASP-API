using Bookstore.Application.UseCases.DTO;
using Bookstore.Application.Exceptions;
using Bookstore.Application.UseCaseHandiling;
using Bookstore.Application.UseCases.Commands;

using Bookstore.DataAccess;
using Bookstore.Implementation.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.Application;

namespace Bookstore.Implementation.UseCases.Commands
{
    public class EfUpdateUserCommand : EfUseCase,  IUpdateUserCommand
    {
      
        private readonly IApplicationActor _actor;
        private readonly UpdateUserValidator _validator;
        public EfUpdateUserCommand(
             BookstoreContext context,
            IApplicationActor actor,
           UpdateUserValidator validator):base(context)
        {
          
            _actor = actor;
            _validator = validator;
        }
        public int Id => 10;

        public string Name => "User edit";

        public string Description => "";

        public void Execute(UpdateUserDto request)
        {
            var user = Context.Users.Find(request.Id);

            if (user == null)
            {
                throw new EntityNotFoundException(request.Id.Value, "user");
            }

            _validator.ValidateAndThrow(request);

          

           

            user.RoleId = request.RoleId.Value;
            user.IsActive = request.IsActive;
            if (request.IsActive) {
                user.DeletedAt = null;
                user.DeletedBy = null;
            }

            user.ModifiedAt = DateTime.UtcNow;
            user.ModifiedBy = _actor.Username;
            Context.Entry(user).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
