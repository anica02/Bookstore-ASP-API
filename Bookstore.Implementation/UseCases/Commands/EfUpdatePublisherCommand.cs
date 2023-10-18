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
    public class EfUpdatePublisherCommand : EfUseCase, IUpdatePublisherCommand
    {

        private readonly IApplicationActor _actor;
        private readonly UpdatePublisherValidator _validator;
        public EfUpdatePublisherCommand(
             BookstoreContext context,
            IApplicationActor actor,
            UpdatePublisherValidator validator):base(context)
        {
           
            _actor = actor;
            _validator = validator;
        }

        public int Id => 6;

        public string Name => "Publisher edit";

        public string Description => "";

        public void Execute(UpdatePublisherDto request)
        {

            var publisher = Context.Publishers.Find(request.Id.Value);
            if (publisher == null || !publisher.IsActive || publisher.DeletedAt.HasValue)
            {
                throw new EntityNotFoundException(request.Id.Value, "publisher");

            }

            _validator.ValidateAndThrow(request);

            if(request.Name.ToLower()!= request.Name.ToLower())
            {
                var name = Context.Publishers.Any(x => x.Name.ToLower() == request.Name.ToLower() && x.Id != request.Id);
                if (!name)
                {
                    publisher.Name = request.Name;
                }
                else
                {
                    throw new ConflictException(request.Id.Value, "publisher", "Publisher name is taken");
                }
            }

            if (request.Website != request.Website)
            {
                var website = Context.Publishers.Any(x => x.Website == request.Website && x.Id != request.Id);
                if (!website)
                {
                    publisher.Website = request.Website;
                }
                else
                {
                    throw new ConflictException(request.Id.Value, "publisher", "Website is taken");
                }
            }

            publisher.Location = request.Location;
            publisher.ModifiedBy = _actor.Username;
            publisher.ModifiedAt = DateTime.UtcNow;

            Context.Entry(publisher).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
