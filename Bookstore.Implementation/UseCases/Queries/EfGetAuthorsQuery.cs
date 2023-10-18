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
    public class EfGetAuthorsQuery : EfUseCase, IGetAuthorsQuery
    {
        public EfGetAuthorsQuery(BookstoreContext context) : base(context)
        {

        }

        public int Id => 35;

        public string Name => "Search authors";

        public string Description => "";

        public IEnumerable<ReadAuthorDto> Execute(AuthorSearch search)
        {
            var query = Context.Authors.Where(x => x.IsActive && !x.DeletedAt.HasValue).AsQueryable();

            if (!string.IsNullOrEmpty(search.Pseudonym))
            {
                query = query.Where(x => x.Pseudonym.ToLower() == search.Pseudonym.ToLower());
            }

            if (!string.IsNullOrEmpty(search.FirstName))
            {
                query = query.Where(x => x.FirstName.ToLower() == search.FirstName.ToLower());
            }
            if (!string.IsNullOrEmpty(search.LastName))
            {
                query = query.Where(x => x.LastName.ToLower() == search.LastName.ToLower());
            }

            if (!string.IsNullOrEmpty(search.Country))
            {
                query = query.Where(x => x.Country.ToLower() == search.Country.ToLower());
            }

            IEnumerable<ReadAuthorDto> result = query.Select(x => new ReadAuthorDto
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Pseudonym = x.Pseudonym != null ? x.Pseudonym : "none",
                Country = x.Country,
                DateOfBirth = x.DateOfBirth
            }).ToList();

            return result;
        }
    }
}
