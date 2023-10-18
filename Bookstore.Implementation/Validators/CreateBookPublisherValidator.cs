
using Bookstore.Implementation.Extensions;
using Bookstore.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookstore.Application.UseCases.DTO;
using System.Text.RegularExpressions;

namespace Bookstore.Implementation.Validators
{
    public class CreateBookPublisherValidator : AbstractValidator<CreateBookPublisherDto>
    {
        
        public CreateBookPublisherValidator(BookstoreContext context)
        {
            Regex format = new Regex(@"^[0-9]{2}x[0-9]{2}$");

            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.BookId)
                .NotEmpty()
                .WithMessage("Book id is required")
                .Must(x => context.Books.Any(b => b.Id == x && b.IsActive))
                .WithMessage("Book id doesn't exist");



            RuleFor(x => x.PublisherId)
                .NotEmpty()
                .WithMessage("Publisher id is required")
                .Must(x => context.Publishers.Any(p => p.Id == x && p.IsActive))
                .WithMessage("Publisher id does not exist");
             
            RuleFor(x => x.BookCover)
                .NotEmpty()
                .WithMessage("Book cover is required")
                .Must(x => ValidationExtensionEntity.AllowedBookCovers.Contains(x))
                .WithMessage("Invalid book cover. Available: " + string.Join(", ", ValidationExtensionEntity.AllowedBookCovers)); ;
            RuleFor(x => x.BookFormat)
                .NotEmpty()
                .WithMessage("Book format is required").Matches(format).WithMessage("Book format example: 22x12");
            RuleFor(x => x.BookWritingSystem)
                .NotEmpty()
                .WithMessage("Book writing system is required")
                .Must(x => ValidationExtensionEntity.AllowedBookWriting.Contains(x))
                .WithMessage("Invalid book writing. Available: " + string.Join(", ", ValidationExtensionEntity.AllowedBookWriting));
            RuleFor(x => x.NumberOfPages)
                .NotEmpty()
                .WithMessage("Number of pages is required");
            RuleFor(x => x.Year)
                .NotEmpty()
                .WithMessage("Year is required")
                .LessThanOrEqualTo(DateTime.Now.Year)
                .WithMessage("Year must be less or equal to the current one");
            RuleFor(x => x.Image)
                .SetValidator(new CreateImageValidator(context));
            RuleFor(x => x.Discount)
                .SetValidator(new CreateBookDiscountValidator(context));
            RuleFor(x => x.Price).NotEmpty().WithMessage("Price is required").GreaterThan(0).WithMessage("Price should be greatr than 0");
        }
    }

   
    public class CreateImageValidator : AbstractValidator<CreateImage>
    {
  
        public CreateImageValidator(BookstoreContext context)
        {
           

            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Size).NotEmpty().WithMessage("Size is required").LessThanOrEqualTo(280);
            RuleFor(x => x.Path).NotEmpty().WithMessage("Path is required")
                .Must(x => !context.Files.Any(f => f.Path == x))
                .WithMessage("Path is already assigned to an  existing book")
                .Must(x => x.Split(".").Count() == 2)
                .WithMessage("Invalid file path.")
                .Must(x => ValidationExtensionEntity.AllowedFileExtensions.Contains(x.Split(".")[1]))
                .WithMessage("Unsupported file extension. Supported: " + string.Join(", ", ValidationExtensionEntity.AllowedFileExtensions));

        }
    }

    public class CreateBookDiscountValidator : AbstractValidator<CreateBookDiscountDto>
    {
      
        public CreateBookDiscountValidator(BookstoreContext context)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

         
            RuleFor(x => x.DiscountPercentage).NotEmpty().WithMessage("Discount precentage is required").GreaterThan(0).WithMessage("Discount precentage should be greater than 0");
            RuleFor(x => x.StartsFrom).NotEmpty().WithMessage("Starts from is required").GreaterThanOrEqualTo(x => DateTime.Today);
            RuleFor(x => x.EndsAt).NotEmpty().WithMessage("Starts from is required");
            RuleFor(x => x).Must(x => x.EndsAt > x.StartsFrom)
            .WithMessage("Ends at must be  greater than starts from");
        }
    }

   
}


