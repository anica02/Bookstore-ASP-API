using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Application.UseCases.DTO
{
    public class CreateBookPublisherDto
    {
        public int? BookId { get; set; }
        public int? PublisherId { get; set; }
        public int NumberOfPages { get; set; }
        public string BookCover { get; set; }
        public string BookFormat { get; set; }
        public string BookWritingSystem { get; set; }
        public int Year { get; set; }

        public double Price { get; set; }
        public CreateImage Image { get; set; }
        public CreateBookDiscountDto? Discount { get; set; }
    }
    public class CreateImage
    {
      
        public string Path { get; set; }
        public int Size { get; set; } 
    }

    public class CreateBookDiscountDto
    {

        public int DiscountPercentage { get; set; }
        public DateTime StartsFrom { get; set; }
        public DateTime EndsAt { get; set; }
    }


    public class UpdateBookPublisheraDto:UpdateEntityDto
    {
  
        public int NumberOfPages { get; set; }
        public string BookCover { get; set; }
        public string BookFormat { get; set; }
        public string BookWritingSystem { get; set; }
        public int Year { get; set; }

        public double Price { get; set; }
        public CreateImage Image { get; set; }
        public CreateBookDiscountDto? Discount { get; set; }
    }

}
