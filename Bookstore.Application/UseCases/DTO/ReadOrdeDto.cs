using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Application.UseCases.DTO
{
    public class ReadOrdeDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Address { get; set; }
        public string PaymentMethod { get; set; }
        public string DeliveryMethod { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
        public List<ReadUserOrderItemDto> OrderItems { get; set; } = new List<ReadUserOrderItemDto>();
    }
    /*
    public class ReadOrderItemDto
    {
        public int Id { get; set; }
        public int BookPublisherId { get; set; }
        public int Quantity { get; set; }

    }*/

    public class ReadOrderUserDto
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string PaymentMethod { get; set; }
        public string DeliveryMethod { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
        public List<ReadUserOrderItemDto> OrderItems { get; set; } = new List<ReadUserOrderItemDto>();
    }


    public class ReadUserOrderItemDto
    {
        public int Id { get; set; }
        public int BookPublisherId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public int Total { get; set; }
    }
}
