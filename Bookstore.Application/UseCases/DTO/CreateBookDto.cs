using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Application.UseCases.DTO
{
    public class CreateBookDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Code { get; set; }
        public IEnumerable<CreateBookAuthorDto> BookAuthors { get; set; } = new List<CreateBookAuthorDto>();
        public IEnumerable<CreateBookGenreDto> BookGenres { get; set; } =new List<CreateBookGenreDto>();

        public CreateBookPublisher BookPublisher { get; set; }

    }
    public class CreateBookPublisher
    {
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

    public class CreateBookAuthorDto { 
        public int? AuthorId { get; set; }
    }

    public class CreateBookGenreDto
    {
        public int? GenreId { get; set; }
    }

    public class CreateBookPriceDto
    {
        public int? BookPublisherId { get; set; }
        public decimal BookPrice { get; set; }
    }

    public class UpdateBookDto: UpdateEntityDto
    {
     
        public string Name { get; set; }
        public string Description { get; set; }
        public int Code { get; set; }
        public int Year { get; set; }
        public IEnumerable<CreateBookAuthorDto> BookAuthors { get; set; } = new List<CreateBookAuthorDto>();
        public IEnumerable<CreateBookGenreDto> BookGenres { get; set; } = new List<CreateBookGenreDto>();
        public UpdateBookPublisheraDto BookPublisher { get; set; }

    }



}
