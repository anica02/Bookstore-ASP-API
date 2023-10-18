using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Domain.Entities
{
    public class Book:Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Code { get; set; }
        public virtual ICollection<BookAuthor> BookAuthors { get; set; } = new HashSet<BookAuthor>();
        public virtual ICollection<BookPublisher> BookPublishers { get; set; } = new HashSet<BookPublisher>();

        public virtual ICollection<BookGenre> BookGenres { get; set; } = new HashSet<BookGenre>();

     
  
       
    }
}
