using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bookstore.Application.UseCases.DTO
{
    public class CreateUserCartDto:UpdateEntityDto
    { 
        public IEnumerable<CartItemDto> CartItems { get; set; } = new List<CartItemDto>();
    }

    public class CartItemDto
    {
       

        public int BookPublisherId { get; set; }
        public int Quantity { get; set; }
     }


   


}
