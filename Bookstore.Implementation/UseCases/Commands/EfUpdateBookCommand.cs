
using Bookstore.Application.UseCases.DTO;
using Bookstore.Application.Exceptions;
using Bookstore.Application.UseCaseHandiling;
using Bookstore.Application.UseCases.Commands;

using Bookstore.DataAccess;
using Bookstore.Domain.Entities;
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
    public class EfUpdateBookCommand:EfUseCase, IUpdateBookCommand
    {
    
        private readonly UpdateBookValidator _validator;
        private readonly IApplicationActor _actor;

        public EfUpdateBookCommand(
            BookstoreContext context,
            IApplicationActor actor,
            UpdateBookValidator validator ):base(context)
        {

            _actor = actor;
            _validator = validator;
        }

        public int Id => 2;

        public string Name => "Book edit";

        public string Description => "";

        public void Execute(UpdateBookDto request) { 


            var book = Context.Books.Include(x => x.BookAuthors).Include(x => x.BookGenres).Include(x => x.BookPublishers).FirstOrDefault(x => x.Id == request.Id.Value && x.IsActive);
            if (book == null || book.DeletedAt.HasValue)
            {
                throw new EntityNotFoundException(request.Id.Value, "book");

            }
             _validator.ValidateAndThrow(request);


            if (request.BookAuthors.Any())
            {
                foreach (var author in request.BookAuthors)
                {

                    var remove = book.BookAuthors.Where(x => !request.BookAuthors.Any(b => b.AuthorId == x.AuthorId));
                    Context.BookAuthors.RemoveRange(remove);

                    if (book.BookAuthors.Any(x => x.AuthorId == author.AuthorId && x.BookId == request.Id))
                    {
                        continue;
                    }
                    else
                    {
                        book.BookAuthors.Add(new BookAuthor
                        {
                            AuthorId = author.AuthorId.Value
                        });
                    }
                }


            }
            else
            {
                throw new ConflictException(request.Id.Value, "book", "No book authors were given for an update, book needs to have at least one author");
            }

            if (request.BookGenres.Any())
            {
                foreach (var genre in request.BookGenres)
                {
                    var remove = book.BookGenres.Where(x => !request.BookGenres.Any(b => b.GenreId == x.GenreId));
                    Context.BookGenres.RemoveRange(remove);

                    if (book.BookGenres.Any(x => x.GenreId == genre.GenreId && x.BookId == request.Id))
                    {
                        continue;
                    }
                    else
                    {
                        book.BookGenres.Add(new BookGenre
                        {
                            GenreId = genre.GenreId.Value
                        });
                    }
                }
            }
            else
            {
                throw new ConflictException(request.Id.Value, "book", "No book genres were given for an update, book needs to have at least one genre");
            }

            foreach (var author in request.BookAuthors)
            {
                var nameExsits = Context.BookAuthors.Any(x => x.AuthorId == author.AuthorId && x.Book.Name.ToLower() == request.Name && x.Book.IsActive);
                if (nameExsits)
                {
                    throw new ConflictExceptionCreating("book", $"There is already an author with this book name {request.Name}");
                }
            }


            if (!book.Code.Equals(request.Code))
            {
                var code = Context.Books.Any(x => x.Code == request.Code && x.Id != request.Id);
                if (!code)
                {
                    book.Code = request.Code;
                }
                else
                {
                    throw new ConflictException(request.Id.Value, "book", "Book code is taken");
                }

            }
           
                

             if (!string.IsNullOrEmpty(request.Name))
             {
                 var name = Context.Books.Any(x => x.Name == request.Name && x.Id != request.Id);
                  if (!name)
                  {
                    book.Name = request.Name;
                  }
                  else
                  {
                     throw new ConflictException(request.Id.Value, "book", "Book name is taken");
                  }

             }

             book.Description = request.Description;
             book.ModifiedAt = DateTime.UtcNow;
             book.ModifiedBy = _actor.Username;

             Context.Entry(book).State = EntityState.Modified;

             Context.SaveChanges();
        }
    }
    
}
