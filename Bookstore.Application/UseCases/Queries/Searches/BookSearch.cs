using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Application.UseCase.Queries.Seraches
{
    public class BookSearch
    {
        public string Name { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public string AuthorPseudonym { get; set; }
        public string Genre { get; set; }
        public string Publisher { get; set; }
        public int? PublicationFromYear { get; set; }
        public int? PublicationToYear { get; set; }
       

    }

}
