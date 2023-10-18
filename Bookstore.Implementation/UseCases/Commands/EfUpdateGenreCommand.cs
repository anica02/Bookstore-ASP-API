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
    public class EfUpdateGenreCommand : EfUseCase, IUpdateGenreCommand
    {
    
      
        private readonly UpdateGenreValidator _validator;
        private readonly IApplicationActor _actor;
        public EfUpdateGenreCommand(
             BookstoreContext context,
             IApplicationActor actor,
           UpdateGenreValidator validator):base(context)
        {
            _actor = actor;
            _validator = validator;
        }
        public int Id => 8;

        public string Name => "Genre edit";

        public string Description => "";

        public void Execute(UpdateGenreDto request)
        {
            var genre = Context.Genres.Find(request.Id.Value);

            if (genre == null || !genre.IsActive || genre.DeletedAt.HasValue)
            {
                throw new EntityNotFoundException(request.Id.Value, "genre");
            }


            _validator.ValidateAndThrow(request);

            if (request.Name.ToLower() != genre.Name.ToLower())
            {
                var name = Context.Genres.Any(x => x.Name == request.Name && x.Id != request.Id && x.IsActive);

                if (!name)
                {
                    genre.Name = request.Name;
                }
                else
                {
                    throw new ConflictException(request.Id.Value, "genre", "Genre name is taken");
                }

            }

            genre.ParentId = request.ParentId;
            genre.ModifiedAt = DateTime.UtcNow;
            genre.ModifiedBy = _actor.Username;
            Context.Entry(genre).State = EntityState.Modified;
            Context.SaveChanges();

        }
    }
}
