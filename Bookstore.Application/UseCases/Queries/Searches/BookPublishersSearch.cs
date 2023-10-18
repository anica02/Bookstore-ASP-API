using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Application.UseCases.Queries.Searches
{
   public  class BookPublishersSearch
   {
        public string BookName { get; set; }
        public string PublisherName { get; set; }
        public string BookCover { get; set; }

        public bool HasDiscount { get; set; }
   }
}
