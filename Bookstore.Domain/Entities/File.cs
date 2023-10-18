using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Domain.Entities
{
    public class File:Entity
    {
        public string Path { get; set; }
        public int Size { get; set; }

        public int BookPublisherId { get; set; }
        public virtual BookPublisher BookPublisher { get; set; }
    }
}
