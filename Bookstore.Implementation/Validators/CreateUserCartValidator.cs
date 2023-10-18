using Bookstore.Application.UseCases.DTO;
using Bookstore.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Implementation.Validators
{
    public class CreateUserCartValidator:AbstractValidator<CreateUserCartDto>
    {
        public CreateUserCartValidator(BookstoreContext context)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;
           
            RuleForEach(x => x.CartItems).SetValidator(new CreateUserCartItemValidator(context));
        } 
    }

    public class CreateUserCartItemValidator : AbstractValidator<CartItemDto>
    {
        public CreateUserCartItemValidator(BookstoreContext context)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;
            RuleFor(x=>x.BookPublisherId).NotEmpty().WithMessage("Book publisher id is required").Must(x => context.BookPublishers.Any(p => p.Id == x && p.IsActive)).WithMessage("Book publisher id doesn't exist");
            RuleFor(x => x.Quantity).NotEmpty().WithMessage("Quantity is required").GreaterThan(0).WithMessage("Quantity must be greater than 0");
        }
    }
}
