using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Domain.Entities
{
    public class OrderItem:Entity
    {
        public int OrderId { get; set; }
        public int BookPublisherId { get; set; }
        public int Quantity { get; set; }

        public virtual Order Order{ get; set; }
        public virtual BookPublisher BookPublisher { get; set; }
    }
}
