using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Domain.Entities
{
    public class CartItem : Entity
    {
        public int CartId { get; set; }
        public int BookPublisherId { get; set; }
     
        public int Quantity { get; set; }

        public virtual Cart Cart { get; set; }
        public virtual BookPublisher BookPublisher { get; set; }
    }
}
