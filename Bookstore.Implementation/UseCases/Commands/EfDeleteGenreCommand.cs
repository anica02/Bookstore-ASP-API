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
    public class EfDeleteGenreCommand:EfUseCase, IDeleteGenreCommand
    {
        private readonly IApplicationActor _actor;

        public EfDeleteGenreCommand(
             BookstoreContext context,
            IApplicationActor actor
           ) : base(context)
        {
            _actor = actor;
        }

        public int Id => 21;

        public string Name => "Genre delete";

        public string Description => "";

        public void Execute(DeleteEntityDto request)
        {
            var genre = Context.Genres.Include(x => x.Subgenres).FirstOrDefault(x => x.Id == request.Id);

            if (genre == null || !genre.IsActive || genre.DeletedAt.HasValue)
            {
                throw new EntityNotFoundException(request.Id, "genre");
            }

            if (genre.Subgenres.Any())
            {
                throw new ConflictException(request.Id, "genre", "Genre cannot be deleted because it has subgenres");
            }

            if (Context.BookGenres.Any(x => x.GenreId == genre.Id && x.Book.IsActive))
            {
                throw new ConflictException(request.Id, "genre", "Genre cannot be deleted because it belongs to an active book");
            }

            genre.IsActive = false;
            genre.DeletedAt = DateTime.UtcNow;
            genre.DeletedBy = _actor.Username;
            Context.SaveChanges();
        }
    }
}
