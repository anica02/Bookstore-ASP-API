using Bookstore.Application.UseCase.Queries.Seraches;
using Bookstore.Application.UseCases.DTO;
using Bookstore.Application.UseCases.Queries;
using Bookstore.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Implementation.UseCases.Queries
{
    public class EfGetBooksQuery : EfUseCase, IGetBooksQuery
    {
        public EfGetBooksQuery(BookstoreContext context) : base(context)
        {
        }

        public int Id => 38;

        public string Name => "Search books";

        public string Description => "";

        public IEnumerable<ReadBookDto> Execute(BookSearch search)
        {

            var query = Context.Books.Where(x=>x.IsActive && !x.DeletedAt.HasValue).AsQueryable();

            if (!string.IsNullOrEmpty(search.Name))
            {
                query = query.Where(x => x.Name.Contains(search.Name));
            }

            if (!string.IsNullOrEmpty(search.AuthorFirstName))
            {
                query = query.Where(x => x.BookAuthors.Any(b => b.Author.FirstName.ToLower() == search.AuthorFirstName.ToLower()));
            }

            if (!string.IsNullOrEmpty(search.AuthorLastName))
            {
                query = query.Where(x => x.BookAuthors.Any(b => b.Author.LastName.ToLower() == search.AuthorLastName.ToLower()));
            }

            if (!string.IsNullOrEmpty(search.AuthorPseudonym))
            {
                query = query.Where(x => x.BookAuthors.Any(b => b.Author.Pseudonym.ToLower() == search.AuthorPseudonym.ToLower()));
            }

            if (!string.IsNullOrEmpty(search.Genre))
            {
                query = query.Where(x => x.BookGenres.Any(b => b.Genre.Name==search.Genre));
            }
            if (!string.IsNullOrEmpty(search.Publisher))
            {
                query = query.Where(x => x.BookPublishers.Any(b => b.Publisher.Name.ToLower() == search.Publisher.ToLower()));
            }


            if (search.PublicationFromYear.HasValue && search.PublicationToYear.HasValue)
            {
                query = query.Where(x => x.BookPublishers.Any(b => b.Year >= search.PublicationFromYear.Value && b.Year <= search.PublicationToYear.Value));
            }

           

            IEnumerable<ReadBookDto> books = query.Select(x => new ReadBookDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Code = x.Code,
                CreatedAt=x.CreatedAt,
                ModifiedAt=x.ModifiedAt.HasValue? x.ModifiedAt.Value : null,
                ModifiedBy=x.ModifiedBy,
                DeletedAt=x.DeletedAt.HasValue? x.DeletedAt.Value : null,
                DeletedBy=x.DeletedBy,
                IsActive=x.IsActive,
                BookAuthors = x.BookAuthors.Select(a => new AuthorDto
                {
                    Id = a.Author.Id,
                    FirstName = a.Author.FirstName,
                    LastName = a.Author.LastName,
                    Pseudonym = a.Author.Pseudonym != null ? a.Author.Pseudonym : "none",
                    Country = a.Author.Country,
                    DateOfBirth = a.Author.DateOfBirth
                }),
                BookPublisher = x.BookPublishers.Select(p => new PublisherDto
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
                    Discount = p.Discounts.Where(x => x.IsActive).Select(d => new PriceDiscountDto
                    {
                        Id = d.Id,
                        DiscountPercentage = d.DiscountPercentage,
                        StartsFrom = d.StartsFrom,
                        EndsAt = d.EndsAt
                    }).FirstOrDefault(),
                    Image = new ImageDto
                    {
                        Id = p.Id,
                        Path = p.Image.Path,
                        Size = p.Image.Size
                    }
                }).FirstOrDefault(),
                BookGenres = x.BookGenres.Select(g => new GenreDto
                {
                    Id = g.Genre.Id,
                    Name = g.Genre.Name,


                })
            }).ToList();


            return books;
        }
    }
}
