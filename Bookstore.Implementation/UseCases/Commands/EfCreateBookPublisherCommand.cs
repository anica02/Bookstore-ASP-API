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
    public class EfCreateBookPublisherCommand : EfUseCase, ICreateBookPublisherCommand
    {
   
      
        private readonly CreateBookPublisherValidator _validator;
        private readonly IBase64FileUploader _fileUploader;
        public EfCreateBookPublisherCommand(
             BookstoreContext context,
           CreateBookPublisherValidator validator, 
           IBase64FileUploader uploader) :base(context)
        {
        
          
            _validator = validator;
            _fileUploader = uploader;
        }


        public int Id => 11;

        public string Name => "Book edtion create";

        public string Description => "";

        public void Execute(CreateBookPublisherDto request)
        {

            _validator.ValidateAndThrow(request);

            var bookPublishers = Context.BookPublishers.Any(x => x.BookId == request.BookId && x.PublisherId == request.PublisherId && x.Year == request.Year && x.BookCover == request.BookCover && x.BookFormat == request.BookFormat && x.BookWritingSystem == request.BookWritingSystem && x.NumberOfPages == request.NumberOfPages && x.Image.Path == request.Image.Path);
            if (bookPublishers)
            {
                throw new ConflictExceptionCreating("book publisher", "There is already the same edition of the book in the database");
            }

            BookPublisher bookPublisher = new BookPublisher();
            bookPublisher.BookId = request.BookId.Value;
            bookPublisher.PublisherId = request.PublisherId.Value;
            bookPublisher.BookCover = request.BookCover;
            bookPublisher.BookFormat = request.BookFormat;
            bookPublisher.BookWritingSystem = request.BookWritingSystem;
            bookPublisher.NumberOfPages = request.NumberOfPages;
            bookPublisher.Year = request.Year;
            Context.BookPublishers.Add(bookPublisher);
            
            Context.SaveChanges();
            
            Price price = new Price();
            price.BookPublisherId = bookPublisher.Id;
            price.BookPrice = request.Price;
            Context.Prices.Add(price);
            Context.SaveChanges();

            /*var filePath = _fileUploader.Upload(request.Image.Path, UploadType.BookImage);*/
            File image = new File();
            image.Path = request.Image.Path;
            image.Size = request.Image.Size;
            image.BookPublisherId = bookPublisher.Id;
            Context.Files.Add(image);
            Context.SaveChanges();

            if (request.Discount != null)
            {
                Context.BookDiscounts.Add(new Domain.Entities.BookDiscount
                {
                    BookPublisherId = bookPublisher.Id,
                    StartsFrom = request.Discount.StartsFrom,
                    EndsAt = request.Discount.EndsAt,
                    DiscountPercentage = request.Discount.DiscountPercentage
                });
            }

            Context.SaveChanges();
        }
    }
}
