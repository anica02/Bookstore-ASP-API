using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Domain.Entities
{
    public class BookDiscount: Entity
    {
        public int BookPublisherId { get; set; }
        public int DiscountPercentage { get; set; }
        public DateTime StartsFrom { get; set; }
        public DateTime EndsAt { get; set; }

        public virtual BookPublisher BookPublisher { get; set; }
    }
}
