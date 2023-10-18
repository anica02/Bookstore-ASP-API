using Bookstore.Application.Exceptions;
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
    public class EfGetCartsQuery : EfUseCase, IGetCartsQuery
    {
        public EfGetCartsQuery(BookstoreContext context) : base(context)
        {
        }

        public int Id => 40;

        public string Name => "Search carts";

        public string Description => "";

        public IEnumerable<ReadCartDto> Execute(CartSearch search)
        {


            var query = Context.Carts.AsQueryable();

            if (query == null)
            {
                throw new NotFoundException("carts");
                     
            }

            if (search.UserId.HasValue)
            {
                query = query.Where(x => x.UserId == search.UserId);
            }

            if (search.UserId.HasValue)
            {
                query = query.Where(x => x.CartItems.Any(i=>i.BookPublisherId==search.BookPublisherId));
            }




            IEnumerable<ReadCartDto> result = query.Select(p => new ReadCartDto
            {
                Id = p.Id,
              
                CartItems = p.CartItems.Select(x => new ReadCartItemDto
                {
                    Id = x.Id,
                    BookPublisherId = x.BookPublisherId,
                    Quantity = x.Quantity
                }).ToList()

            }).ToList();

            return result;
        }
    }
}
