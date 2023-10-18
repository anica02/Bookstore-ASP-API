
using Bookstore.Application.UseCases.DTO;
using Bookstore.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Implementation.Validators
{
    public class CreateBookPriceValidator : AbstractValidator<CreateBookPriceDto>
    {
        
        public CreateBookPriceValidator(BookstoreContext context)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;
           
            RuleFor(x => x.BookPublisherId).NotEmpty().WithMessage("Book publisher id is required").Must(x => context.BookPublishers.Any(b => b.Id == x && b.IsActive)).WithMessage("Book publisher id does not exist");
           
            RuleFor(x => x.BookPrice).NotEmpty().WithMessage("Price is required").GreaterThan(0).WithMessage("Price should be greatr than 0");
                
        }
    }
}
