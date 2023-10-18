using Bookstore.Application.UseCases.DTO;
using Bookstore.Application.UseCases.Queries;
using Bookstore.Application.UseCases.Queries.Searches;
using Bookstore.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Implementation.UseCases.Queries
{
    public class EfGetBookPublishersQuery : EfUseCase, IGetBookPublishersQuery
    {
        public EfGetBookPublishersQuery(BookstoreContext context) : base(context)
        {
        }

        public int Id => 39;

        public string Name => "Search book editions";

        public string Description => "";

        public IEnumerable<PublisherDto> Execute(BookPublishersSearch search)
        {


            var query = Context.BookPublishers.AsQueryable();

            if (!string.IsNullOrEmpty(search.PublisherName))
            {
                query = query.Where(x => x.Publisher.Name.ToLower()==search.PublisherName.ToLower());
            }

            if (!string.IsNullOrEmpty(search.BookName))
            {
                query = query.Where(x => x.Book.Name.ToLower()==search.BookName.ToLower());
            }

            if (!string.IsNullOrEmpty(search.BookCover))
            {
                query = query.Where(x => x.BookCover.ToLower() == search.BookCover.ToLower());
            }

            if (!string.IsNullOrEmpty(search.BookCover))
            {
                query = query.Where(x => x.Discounts.Any()); 
            }


            IEnumerable<PublisherDto> result = query.Select(p => new PublisherDto
            {
                Id = p.Id,
                PublisherId = p.PublisherId,
                PublisherName = p.Publisher.Name,
                NumberOfPages = p.NumberOfPages,
                BookCover = p.BookCover,
                BookFormat = p.BookFormat,
                BookWritingSystem = p.BookWritingSystem,
                Year = p.Year,
                Price = p.Prices.Where(x => x.IsActive).OrderByDescending(x => x.Id).Select(x => x.BookPrice).First(),
                Discount = p.Discounts.Where(x => x.IsActive && x.StartsFrom <= DateTime.UtcNow && x.EndsAt >= DateTime.UtcNow).Select(d => new PriceDiscountDto
                {
                    Id = d.Id,
                    DiscountPercentage = d.DiscountPercentage,
                    StartsFrom = d.StartsFrom,
                    EndsAt = d.EndsAt
                }).First(),
                Image = new ImageDto
                {
                    Id = p.Id,
                    Path = p.Image.Path,
                    Size = p.Image.Size
                }
            }).ToList();

            return result;
        }
    }
}
