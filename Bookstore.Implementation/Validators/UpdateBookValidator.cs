
using Bookstore.Application.UseCases.DTO;

using Bookstore.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Implementation.Validators
{
    public class UpdateBookValidator:AbstractValidator<UpdateBookDto>
    {
      
        public UpdateBookValidator(BookstoreContext context)
        {
    
            RuleLevelCascadeMode = CascadeMode.Stop;
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");

            RuleFor(x => x.Code).NotEmpty().WithMessage("Code is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.Year).NotEmpty().WithMessage("Year is required").LessThanOrEqualTo(DateTime.Now.Year).WithMessage("Year must be less or equal to the current one");

            RuleForEach(x => x.BookAuthors).SetValidator(new BookAuthorValidator(context));

            RuleForEach(x => x.BookGenres).SetValidator(new BookGenreValidator(context));
         
        }
    }
}
