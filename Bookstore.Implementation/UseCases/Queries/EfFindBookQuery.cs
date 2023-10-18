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
    public class EfFindBookQuery:EfUseCase, IFindBookQuery
    {
        public EfFindBookQuery(BookstoreContext context) : base(context)
        {

        }
        public int Id => 30;

        public string Name => "Find book";

        public string Description => "";

        public ReadBookDto Execute(int search)
        {

           
            var book = Context.Books.Include(x => x.BookAuthors).ThenInclude(a => a.Author).Include(x => x.BookGenres).ThenInclude(g => g.Genre).ThenInclude(s => s.Subgenres).Include(x => x.BookPublishers).ThenInclude(p => p.Publisher).Include(x => x.BookPublishers).ThenInclude(d=>d.Discounts).Include(x => x.BookPublishers).ThenInclude(p => p.Prices).Include(x => x.BookPublishers).ThenInclude(i => i.Image).FirstOrDefault(x => x.Id == search);

            if (book == null || !book.IsActive || book.DeletedAt.HasValue)
            {
                throw new EntityNotFoundException(search, nameof(book));
            }

            ReadBookDto bookDto = new ReadBookDto();
            bookDto.Id = book.Id;
            bookDto.Name = book.Name;
            bookDto.Description = book.Description;
            bookDto.Code = book.Code;
            bookDto.CreatedAt = book.CreatedAt;
            bookDto.ModifiedBy = book.ModifiedBy;
            bookDto.ModifiedAt = book.ModifiedAt.HasValue ? book.ModifiedAt.Value : null;
            bookDto.DeletedBy = book.DeletedBy;
            bookDto.DeletedAt = book.DeletedAt.HasValue ? book.DeletedAt.Value : null;
            bookDto.IsActive = book.IsActive;

            bookDto.BookAuthors = book.BookAuthors.Select(x => new AuthorDto
            {
                Id = x.Author.Id,
                FirstName = x.Author.FirstName,
                LastName = x.Author.LastName,
                Pseudonym = x.Author.Pseudonym != null ? x.Author.Pseudonym : "none",
                Country = x.Author.Country,
                DateOfBirth = x.Author.DateOfBirth
            }).ToList();



            bookDto.BookPublisher = book.BookPublishers.Select(p => new PublisherDto
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
                Discount = p.Discounts.Where(d => d.IsActive && d.StartsFrom <= DateTime.Today && d.EndsAt >= DateTime.Today).Select(d => new PriceDiscountDto
                {
                    Id = d.Id,
                    DiscountPercentage = d.DiscountPercentage,
                    StartsFrom = d.StartsFrom,
                    EndsAt = d.EndsAt
                }).FirstOrDefault(),
                Image = new ImageDto
                {
                    Id = p.Image.Id,
                    Path = p.Image.Path,
                    Size = p.Image.Size
                }
            }).FirstOrDefault();

            bookDto.BookGenres = book.BookGenres.Select(x => new GenreDto
            {
                Id = x.Genre.Id,
                Name = x.Genre.Name

            }).ToList();

            return bookDto;

        }
    }
}
