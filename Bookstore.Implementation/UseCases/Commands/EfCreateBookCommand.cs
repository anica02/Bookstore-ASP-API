using Bookstore.Application.UseCases.DTO;
using Bookstore.Application.Exceptions;
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
using Bookstore.Application.Uploads;
using static Bookstore.Application.Uploads.IBase64FileUploader;

namespace Bookstore.Implementation.UseCases.Commands
{
    public class EfCreateBookCommand : EfUseCase, ICreateBookCommand
    {


        private readonly CreateBookValidator _validator;
        private readonly IBase64FileUploader _fileUploader;
        public EfCreateBookCommand(
             BookstoreContext context,
             IBase64FileUploader fileUploader,
            CreateBookValidator validator):base(context)
        {
            _fileUploader = fileUploader;
            _validator = validator;
        }
        public int Id => 1;

        public string Name => "Book create";

        public string Description => "";

        public void Execute(CreateBookDto request)
        {
            _validator.ValidateAndThrow(request);

            List<CreateBookAuthorDto> authors = new List<CreateBookAuthorDto>();
            authors.AddRange(request.BookAuthors);

            List<CreateBookGenreDto> genres = new List<CreateBookGenreDto>();
            genres.AddRange(request.BookGenres);

            Book book = new Book();

            foreach (var author in authors)
            {
                var nameExsits = Context.BookAuthors.Any(x => x.AuthorId == author.AuthorId && x.Book.Name.ToLower() == request.Name && x.Book.IsActive);
                if (nameExsits)
                {
                    throw new ConflictExceptionCreating("book", $"There is already an author with this book name {request.Name}");
                }
            }

            book.Name = request.Name;
            book.Code = request.Code;
            book.Description = request.Description;
            

            book.BookAuthors = authors.Select(x => new BookAuthor
            {
                AuthorId = x.AuthorId.Value
            }).ToList();
            book.BookGenres = genres.Select(x => new BookGenre
            {
                GenreId = x.GenreId.Value
            }).ToList();
            Context.Books.Add(book);
            Context.SaveChanges();

            BookPublisher bookPublisher = new BookPublisher();
            bookPublisher.BookId = book.Id;
            bookPublisher.PublisherId = request.BookPublisher.PublisherId.Value;
            bookPublisher.BookCover = request.BookPublisher.BookCover;
            bookPublisher.BookFormat = request.BookPublisher.BookFormat;
            bookPublisher.BookWritingSystem = request.BookPublisher.BookWritingSystem;
            bookPublisher.NumberOfPages = request.BookPublisher.NumberOfPages;
            bookPublisher.Year = request.BookPublisher.Year;
            Context.BookPublishers.Add(bookPublisher);
            Context.SaveChanges();


            Price price = new Price();
            price.BookPublisherId = bookPublisher.Id;
            price.BookPrice = request.BookPublisher.Price;
            Context.Prices.Add(price);
            Context.SaveChanges();

            /*var filePath = _fileUploader.Upload(request.BookPublisher.Image.Path, UploadType.BookImage);*/
            File image = new File();
           image.Path = request.BookPublisher.Image.Path;


            image.Size = request.BookPublisher.Image.Size;
            image.BookPublisherId = bookPublisher.Id;
            Context.Files.Add(image);
            Context.SaveChanges();

            if (request.BookPublisher.Discount != null)
            {
                Context.BookDiscounts.Add(new Domain.Entities.BookDiscount
                {
                    BookPublisherId = bookPublisher.Id,
                    StartsFrom = request.BookPublisher.Discount.StartsFrom,
                    EndsAt = request.BookPublisher.Discount.EndsAt,
                    DiscountPercentage = request.BookPublisher.Discount.DiscountPercentage
                });
            }
     
            Context.SaveChanges();

        }
    }
}
