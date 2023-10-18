using Bookstore.Application.Exceptions;
using Bookstore.Application.UseCases.DTO;
using Bookstore.Application.UseCases.Queries;
using Bookstore.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Implementation.UseCases.Queries
{
    public class EfFindBookPublisherQuery : EfUseCase, IFindBookPublisherQuery
    {
        public EfFindBookPublisherQuery(BookstoreContext context) : base(context)
        {

        }
        public int Id => 33;

        public string Name => "Find book edition";

        public string Description => "";

        public PublisherDto Execute(int search)
        {
            var bookPublisher = Context.BookPublishers.Include(x => x.Discounts).Include(x => x.Image).Include(x => x.Publisher).Include(p => p.Prices).FirstOrDefault(x => x.Id == search);
           

            if (bookPublisher == null || !bookPublisher.IsActive || bookPublisher.DeletedAt.HasValue)
            {
                throw new EntityNotFoundException(search, nameof(bookPublisher));
            }

            return  new PublisherDto
            {
                Id = bookPublisher.Id,
                PublisherId = bookPublisher.PublisherId,
                PublisherName = bookPublisher.Publisher.Name,
                NumberOfPages = bookPublisher.NumberOfPages,
                BookCover = bookPublisher.BookCover,
                BookFormat = bookPublisher.BookFormat,
                BookWritingSystem = bookPublisher.BookWritingSystem,
                Year = bookPublisher.Year,
                Price = bookPublisher.Prices.Where(x => x.IsActive).OrderByDescending(x => x.Id).Select(x => x.BookPrice).First(),
                Discount = bookPublisher.Discounts.Where(d => d.IsActive && d.StartsFrom <= DateTime.Today && d.EndsAt >= DateTime.Today).Select(d => new PriceDiscountDto
                {
                    Id = d.Id,
                    DiscountPercentage = d.DiscountPercentage,
                    StartsFrom = d.StartsFrom,
                    EndsAt = d.EndsAt
                }).First(),
                Image = new ImageDto
                {
                    Id = bookPublisher.Image.Id,
                    Path = bookPublisher.Image.Path,
                    Size = bookPublisher.Image.Size
                }
            };

           

        }





    }
}
