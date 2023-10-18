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

namespace Bookstore.Implementation.UseCases.Commands
{
    public class EfCreatePublisherCommand :EfUseCase, ICreatePublisherCommand
    {
        private readonly CreatePublisherValidator _validator;
        public EfCreatePublisherCommand(
             BookstoreContext context,
            CreatePublisherValidator validator):base(context)
        {
            _validator = validator;
        }

        public int Id => 5;

        public string Name => "Publisher create";
        public string Description => "";

        public void Execute(CreatePublisherDto request)
        {
            _validator.ValidateAndThrow(request);


            Context.Publishers.Add(new Domain.Entities.Publisher
            {
                Name = request.Name,
                Location = request.Location,
                Website = request.Website != null ? request.Website : null
            });

            Context.SaveChanges();

        }
    }
}
