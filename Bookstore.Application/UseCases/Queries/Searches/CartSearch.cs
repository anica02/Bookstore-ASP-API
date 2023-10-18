using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Application.UseCases.Queries.Searches
{
    public class CartSearch
    {
        public int? UserId { get; set; }
        public int? BookPublisherId { get; set; }
    }
}
