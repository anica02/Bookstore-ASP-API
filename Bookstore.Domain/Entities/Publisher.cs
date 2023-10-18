using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Domain.Entities
{
    public class Publisher:Entity
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string? Website { get; set; }

        public virtual ICollection<BookPublisher> BookPublishers { get; set; } = new HashSet<BookPublisher>();
    }
}
