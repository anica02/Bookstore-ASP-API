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
using Bookstore.Application.Uploads;
using static Bookstore.Application.Uploads.IBase64FileUploader;

namespace Bookstore.Implementation.UseCases.Commands
{
    public class EfUpdateBookPublisherCommand :EfUseCase,  IUpdateBookPublisherCommand
    {

        private readonly IApplicationActor _actor;
        private readonly UpdateBookPublisherValidator _validator;
        private readonly IBase64FileUploader _fileUploader;
        public EfUpdateBookPublisherCommand(
             BookstoreContext context,
             IApplicationActor actor,
             IBase64FileUploader fileUploader,
           UpdateBookPublisherValidator validator):base(context)
        {
            _fileUploader = fileUploader;
            _actor =actor;
            _validator = validator;
        }

        public int Id => 12;

        public string Name => "Book edition edit";

        public string Description => throw new NotImplementedException();

        public void Execute(UpdateBookPublisheraDto request)
        {

            var bookPublisher = Context.BookPublishers.Find(request.Id);
            if (bookPublisher == null || !bookPublisher.IsActive || bookPublisher.DeletedAt.HasValue)
            {
                throw new EntityNotFoundException(request.Id.Value, "book publisher");
            }

            
            _validator.ValidateAndThrow(request);

            bookPublisher.BookCover = request.BookCover;
            bookPublisher.BookFormat = request.BookFormat;
            bookPublisher.BookWritingSystem = request.BookWritingSystem;
            bookPublisher.NumberOfPages = request.NumberOfPages;
            bookPublisher.Year = request.Year;
            bookPublisher.ModifiedAt = DateTime.UtcNow;
            bookPublisher.ModifiedBy = _actor.Username;
            Context.Entry(bookPublisher).State = EntityState.Modified;

            var priceCurrent = Context.Prices.Where(x => x.BookPublisherId == request.Id && x.IsActive).OrderByDescending(x => x.Id).First();

            if (request.Price != priceCurrent.BookPrice)
            {

                var price = Context.Prices.Find(priceCurrent.Id);
                price.IsActive = false;
                price.DeletedAt = DateTime.UtcNow;
               
                Context.Prices.Add(new Domain.Entities.Price
                {
                    BookPublisherId = request.Id.Value,
                    BookPrice = request.Price
                });
            }
            

            var imageCurrent = Context.Files.Where(x => x.BookPublisherId == request.Id && x.IsActive).First();
          
            if (request.Image.Path != imageCurrent.Path)
            {
                /*var filePath = _fileUploader.Upload(request.Image.Path, UploadType.BookImage);*/

                imageCurrent.Path = request.Image.Path; 
                imageCurrent.Size = request.Image.Size;
                imageCurrent.ModifiedAt = DateTime.UtcNow;
                imageCurrent.ModifiedBy = _actor.Username;
                Context.Entry(imageCurrent).State = EntityState.Modified;
            }
          
            if (request.Discount != null)
            {
                var discount = Context.BookDiscounts.Any(x => x.BookPublisherId == request.Id && x.IsActive);

                if (!discount)
                {
                    Context.BookDiscounts.Add(new Domain.Entities.BookDiscount
                    {
                        DiscountPercentage = request.Discount.DiscountPercentage,
                        StartsFrom = request.Discount.StartsFrom,
                        EndsAt = request.Discount.EndsAt,
                        BookPublisherId = request.Id.Value
                    });
                }
                else
                {
                    var discountE = Context.BookDiscounts.Where(x => x.BookPublisherId == request.Id && x.IsActive).First();
                    discountE.IsActive = false;
                    discountE.DeletedAt = DateTime.UtcNow;
                    Context.BookDiscounts.Add(new Domain.Entities.BookDiscount
                    {
                        DiscountPercentage = request.Discount.DiscountPercentage,
                        StartsFrom = request.Discount.StartsFrom,
                        EndsAt = request.Discount.EndsAt,
                        BookPublisherId = request.Id.Value
                    });
                }
               
            }
            Context.SaveChanges();
        }
    }
}
