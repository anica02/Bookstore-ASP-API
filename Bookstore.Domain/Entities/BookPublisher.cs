using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Domain.Entities
{
    public class BookPublisher:Entity
    {
        public int NumberOfPages { get; set; }
        public string BookCover { get; set; }
        public string BookFormat { get; set; }
        public string BookWritingSystem { get; set; }

        public int Year { get; set; }
        public int BookId { get; set; }

        public int PublisherId { get; set; }

        public virtual Book Book { get; set; }
        public virtual Publisher Publisher { get; set; }
        public virtual File Image { get; set; }
        public virtual ICollection<Price> Prices { get; set; } = new HashSet<Price>();
        public virtual ICollection<BookDiscount> Discounts { get; set; } = new HashSet<BookDiscount>();

        public virtual ICollection<CartItem> CartItems { get; set; } = new HashSet<CartItem>();
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
    }
}
