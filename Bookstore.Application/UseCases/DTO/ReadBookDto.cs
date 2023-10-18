using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Application.UseCases.DTO
{
    public class ReadBookDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Code { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string DeletedBy { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<AuthorDto> BookAuthors { get; set; }
        public PublisherDto BookPublisher { get; set; }

        public IEnumerable<GenreDto> BookGenres { get; set; }

    }

    public class AuthorDto {
        public int Id { get; set; }
        public string? Pseudonym { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Country { get; set; }
    }

    public class PublisherDto
    {
        public int Id { get; set; }
        public int NumberOfPages { get; set; }
        public string BookCover { get; set; }
        public string BookFormat { get; set; }
        public string BookWritingSystem { get; set; }

        public int Year { get; set; }

        public int PublisherId { get; set; }
        public string PublisherName { get; set; }

        public double Price { get; set; }

        public PriceDiscountDto? Discount { get; set; }
        public ImageDto Image { get; set; }

    }
    public class GenreDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
      
    }
    public class PriceDiscountDto
    {
        public int Id { get; set; }
        public int? DiscountPercentage { get; set; }
        public DateTime StartsFrom { get; set; }
        public DateTime EndsAt { get; set; }
  
    }

    public class ImageDto
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public int Size { get; set; }
    }
}
