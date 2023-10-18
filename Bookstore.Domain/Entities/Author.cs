using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Domain.Entities
{
    public class Author: Entity
    {
        public string? Pseudonym { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Country { get; set; }

        public virtual ICollection<BookAuthor> BookAuthors { get; set; }= new HashSet<BookAuthor>();
    }
}
