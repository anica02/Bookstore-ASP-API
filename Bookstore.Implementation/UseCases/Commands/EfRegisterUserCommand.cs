using Bookstore.Application.UseCases.DTO;
using Bookstore.Application.UseCaseHandiling;
using Bookstore.Application.UseCases.Commands;
using Bookstore.DataAccess;
using Bookstore.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.Application;
using Bookstore.Domain.Entities;

namespace Bookstore.Implementation.UseCases.Commands
{
    public class EfRegisterUserCommand : EfUseCase, IRegisterUserCommand
    {
        private readonly CreateUserValidator _validator;
      
        public EfRegisterUserCommand(
             BookstoreContext context,
           CreateUserValidator validator
          ) :base(context)
        {
            _validator = validator;

        }
        public int Id => 9;

        public string Name => "User registration";

        public string Description => "";

        public void Execute(RegisterUserDto request)
        {
            _validator.ValidateAndThrow(request);

            Role defaultRole = Context.Roles.FirstOrDefault(x => x.IsDefault);

            if (defaultRole == null)
            {
                throw new InvalidOperationException("Default role doesn't exist.");
            }

          
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            Context.Users.Add(new Domain.Entities.User
            {
                Username = request.Username,
                Password = passwordHash,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Role = defaultRole
            });
            Context.SaveChanges();
        }
    }
}
