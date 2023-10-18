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

namespace Bookstore.Implementation.UseCases.Commands
{
    public class EfCreateGenreCommand : EfUseCase, ICreateGenreCommand
    {
        private readonly CreateGenreValidator _validator;
        public EfCreateGenreCommand(
             BookstoreContext context,
            CreateGenreValidator validator):base(context)
        {
            _validator = validator;
        }

        public int Id => 7;
        public string Name => "genre create";

        public string Description => "";

        public void Execute(CreateGenreDto request)
        {
            _validator.ValidateAndThrow(request);

            Genre genre = new Genre();
            genre.Name = request.Name;
            if (request.ParentId.HasValue)
            {
                genre.ParentId = request.ParentId;
            }
           Context.Genres.Add(genre);

           Context.SaveChanges();
        }
    }
}
