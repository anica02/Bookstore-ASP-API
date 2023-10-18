using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Domain.Entities
{
    public class Genre:Entity
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public virtual Genre Parent { get; set; }
        public virtual ICollection<Genre> Subgenres { get; set; }
        public virtual ICollection<BookGenre> BookGenres { get; set; } = new HashSet<BookGenre>();
    }
}
