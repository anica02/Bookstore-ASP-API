

using Bookstore.Application.UseCases.DTO;
using Bookstore.DataAccess;
using Bookstore.Implementation.Extensions;
using FluentValidation;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    namespace Bookstore.Implementation.Validators
    {
        public class UpdateBookPublisherValidator : AbstractValidator<UpdateBookPublisheraDto>
        {
          
            public UpdateBookPublisherValidator(BookstoreContext context)
            {
               

                RuleLevelCascadeMode = CascadeMode.Stop;

                RuleFor(x => x.BookCover)
                    .NotEmpty()
                    .WithMessage("Book cover is required")
                    .Must(x => ValidationExtensionEntity.AllowedBookCovers.Contains(x))
                    .WithMessage("Invalid book cover. Available: " + string.Join(", ", ValidationExtensionEntity.AllowedBookCovers)); ;
            RuleFor(x => x.BookFormat)
                .NotEmpty()
                .WithMessage("Book format is required").Must(x => ValidationExtensionEntity.AllowedBookFormats.Contains(x))
                .WithMessage("Invalid book format. Available: " + string.Join(", ", ValidationExtensionEntity.AllowedBookFormats));
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
                    .SetValidator(new UpdateImageValidator(context));
                RuleFor(x => x.Discount)
                    .SetValidator(new UpdateBookDiscountValidator(context));
            }
        }

        public class UpdateImageValidator : AbstractValidator<CreateImage>
        {

            public UpdateImageValidator(BookstoreContext context)
            {
                RuleLevelCascadeMode = CascadeMode.Stop;

                RuleFor(x => x.Size).NotEmpty().WithMessage("Size is required").LessThanOrEqualTo(280);
                RuleFor(x => x.Path).NotEmpty().WithMessage("Path is required")
                    .Must(x => x.Split(".").Count() == 2)
                    .WithMessage("Invalid file path.")
                    .Must(x => ValidationExtensionEntity.AllowedFileExtensions.Contains(x.Split(".")[1]))
                    .WithMessage("Unsupported file extension. Supported: " + string.Join(", ", ValidationExtensionEntity.AllowedFileExtensions));

            }
        }

    public class UpdateBookDiscountValidator : AbstractValidator<CreateBookDiscountDto>
    {

        public UpdateBookDiscountValidator(BookstoreContext context)
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



