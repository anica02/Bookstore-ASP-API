using Bookstore.Application.UseCases.DTO;
using Bookstore.Application.UseCases.Queries.Searches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Application.UseCases.Queries
{
    public interface IGetOrdersQuery: IQuery<CartSearch, IEnumerable<ReadOrdeDto>>
    {
    }
}
