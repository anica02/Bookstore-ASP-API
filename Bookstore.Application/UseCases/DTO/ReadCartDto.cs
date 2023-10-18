using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Application.UseCases.DTO
{
   public class ReadCartDto
    {
        public int Id { get; set; }
   
        public IEnumerable<ReadCartItemDto> CartItems { get; set; } = new List<ReadCartItemDto>();
    }

    public class ReadCartItemDto
    {
        public int Id { get; set; }
        public int BookPublisherId { get; set; }
        public int Quantity { get; set; }
  
    }
}
