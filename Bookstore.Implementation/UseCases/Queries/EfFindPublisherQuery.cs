using Bookstore.Application.Exceptions;
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
    public class EfFindPublisherQuery: EfUseCase, IFindPublisherQuery
    {
        public EfFindPublisherQuery(BookstoreContext context) : base(context)
        {

        }

        public int Id => 27;

        public string Name => "Find publisher";

        public string Description => "";

        public ReadPublisherDto Execute(int search)
        {
            var publisher = Context.Publishers.Find(search);

            if (publisher == null || !publisher.IsActive || publisher.DeletedAt.HasValue)
            {
                throw new EntityNotFoundException(search, nameof(publisher));
            }

            return new ReadPublisherDto
            {
                Id = publisher.Id,
                Name = publisher.Name,
                Location = publisher.Location,
                Website = publisher.Website != null ? publisher.Website : "none"
            };
        }
    }
}
