
using Bookstore.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookstore.Application.UseCases.DTO;
using Bookstore.Implementation.Extensions;
using System.Text.RegularExpressions;

namespace Bookstore.Implementation.Validators
{
    public class CreateBookValidator:AbstractValidator<CreateBookDto>
    {
       
        public CreateBookValidator(BookstoreContext context)
        {
           
            RuleLevelCascadeMode = CascadeMode.Stop;
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.Code).NotEmpty().WithMessage("Code is required").Must(x => !context.Books.Any(b => b.Code == x)).WithMessage("Code already exist");

            RuleForEach(x => x.BookAuthors).SetValidator(new BookAuthorValidator(context));

            RuleForEach(x => x.BookGenres).SetValidator(new BookGenreValidator(context));
            RuleFor(x => x.BookPublisher).SetValidator(new CreateBookPublisher2Validator(context));
            
        }
    }

    public class CreateBookPublisher2Validator : AbstractValidator<CreateBookPublisher>
    {

        public CreateBookPublisher2Validator(BookstoreContext context)
        {

            Regex format = new Regex(@"^[0-9]{2}x[0-9]{2}$");
            RuleLevelCascadeMode = CascadeMode.Stop;


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

    public class BookAuthorValidator : AbstractValidator<CreateBookAuthorDto>
    {
      
        public BookAuthorValidator(BookstoreContext context)
        {
       

            RuleFor(x => x.AuthorId).NotEmpty().WithMessage("Author id is required").Must(x => context.Authors.Any(a => a.Id == x && a.IsActive)).WithMessage("Author id does not exist");

        }
    }

    public class BookGenreValidator : AbstractValidator<CreateBookGenreDto>
    {
       
        public BookGenreValidator(BookstoreContext context)
        {

            RuleFor(x => x.GenreId).NotEmpty().WithMessage("Genre id is required").Must(x => context.Genres.Any(g => g.Id == x && g.IsActive)).WithMessage("Genre id does not exist");

        }
    }
    
   
}
