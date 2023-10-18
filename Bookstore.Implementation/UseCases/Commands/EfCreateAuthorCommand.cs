using Bookstore.Application.UseCases.DTO;
using Bookstore.Application.UseCaseHandiling;
using Bookstore.Application.UseCases.Commands;
using Bookstore.DataAccess;
using Bookstore.Domain.Entities;
using Bookstore.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.Application;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Implementation.UseCases.Commands
{
    public class EfCreateAuthorCommand : EfUseCase, ICreateAuthorCommand
    {

       
        private readonly CreateAuthorValidator _validator;
        public EfCreateAuthorCommand(
             BookstoreContext context,
            CreateAuthorValidator validator):base(context)
        {
            _validator = validator;
        }

        public int Id => 3;

        public string Name => "Author create";

        public string Description => "";

        public void Execute(CreateAuthorDto request)
        {
            _validator.ValidateAndThrow(request);

            Author author = new Author();
            author.FirstName = request.FirstName;
            author.LastName = request.LastName;
            author.DateOfBirth = request.DateOfBirth;
            author.Country = request.Country;


            if (!string.IsNullOrEmpty(request.Pseudonym))
            {
                author.Pseudonym = request.Pseudonym;
            }

            Context.Authors.Add(author);
   
            Context.SaveChanges();
        }
    }
}
