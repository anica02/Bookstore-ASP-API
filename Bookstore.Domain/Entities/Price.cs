using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Domain.Entities
{
    public class Price:Entity
    {
        public int BookPublisherId{ get; set; }
        public  double  BookPrice { get; set; }

        public virtual BookPublisher BookPublisher { get; set; }

    }
}
