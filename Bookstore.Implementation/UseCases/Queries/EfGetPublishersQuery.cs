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
    public class EfGetPublishersQuery: EfUseCase, IGetPublishersQuery
    {
        public EfGetPublishersQuery(BookstoreContext context) : base(context)
        {
        }

        public int Id => 36;

        public string Name => "Search publishers";

        public string Description => "";

        public IEnumerable<ReadPublisherDto> Execute(PublisherSearch search)
        {


            var query = Context.Publishers.AsQueryable();

            if (!string.IsNullOrEmpty(search.Name))
            {
                query = query.Where(x => x.Name.Contains(search.Name));
            }

            if (!string.IsNullOrEmpty(search.Location))
            {
                query = query.Where(x => x.Location.Contains(search.Location));
            }

            IEnumerable<ReadPublisherDto> result = query.Select(x => new ReadPublisherDto
            {
                Id = x.Id,
                Name = x.Name,
                Location = x.Location,
                Website = x.Website
            }).ToList();

            return result;
        }
    }
}
