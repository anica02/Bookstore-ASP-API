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
    public class EfUpdateAuthorCommand : EfUseCase, IUpdateAuthorCommand
    {

        private readonly IApplicationActor _actor;
        private readonly UpdateAuthorValidator _validator;
        public EfUpdateAuthorCommand(
             BookstoreContext context,
             IApplicationActor actor,
           UpdateAuthorValidator validator):base(context)
        {
            _actor = actor;
            _validator = validator;
        }

        public int Id => 4;

        public string Name => "Author edit";

        public string Description => "";

        public void Execute(UpdateAuthorDto request)
        {
            var author = Context.Authors.Find(request.Id);

            if (author == null || !author.IsActive || author.DeletedAt.HasValue)
            {
               throw new EntityNotFoundException(request.Id.Value, "author");
            }

            _validator.ValidateAndThrow(request);

           

            author.FirstName = request.FirstName;
            author.LastName = request.LastName;

            if (!string.IsNullOrEmpty(author.Pseudonym))
            {
                if (author.Pseudonym.ToLower() != request.Pseudonym.ToLower())
                {
                    var pseudo = Context.Authors.Any(x => x.Pseudonym.ToLower() == request.Pseudonym.ToLower() && x.Id != request.Id);
                    if (!pseudo)
                    {
                        author.Pseudonym = request.Pseudonym;
                    }
                    else
                    {
                        throw new ConflictException(request.Id.Value, "author", "Author pseudonym is taken");
                    }
                }

            }
            else
            {
                author.Pseudonym = null;

            }

            
            author.Country = request.Country;
            author.ModifiedAt = DateTime.UtcNow;
            author.ModifiedBy = _actor.Username;
            Context.Entry(author).State = EntityState.Modified;

            Context.SaveChanges();
        }
    }
}
