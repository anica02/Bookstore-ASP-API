using Bookstore.Application.UseCases.DTO;
using Bookstore.DataAccess;
using Bookstore.Implementation.Extensions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bookstore.Implementation.Validators
{
    public class CreateOrderValidator:AbstractValidator<CreateOrderDto>
    {
        public CreateOrderValidator(BookstoreContext context)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;
            Regex regexL = new Regex(@"^[A-ZČĆŠĐŽ]{1}[a-zčćšđž]{2,15}(\s[A-ZČĆŠĐŽ]{1}[a-zčćšđž]{0,15})*\s[\d]{1,5}(\s[A-ZČĆŠĐŽ]{1}[a-zčćšđž]{2,15})*,(\s[A-ZČĆŠĐŽ]{1}[a-zčćšđž]{2,10})+\s[\d]{5}$");

            RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required").Matches(regexL).WithMessage("Valid format: Dimitrija Tucovića 12, Belgarde 11000");
            RuleFor(x => x.PaymentMethod).NotEmpty().WithMessage("Patment method is required").Must(x => ValidationExtensionEntity.AllowedPaymentMethods.Contains(x))
                .WithMessage("Invalid payment method. Available: " + string.Join(", ", ValidationExtensionEntity.AllowedBookCovers)); 
            RuleFor(x => x.DeliveryMethod).NotEmpty().WithMessage("Delivery method is required").Must(x => ValidationExtensionEntity.AllowedDeliveryMethods.Contains(x))
                .WithMessage("Invalid delivery method. Available: " + string.Join(", ", ValidationExtensionEntity.AllowedBookCovers)); 
        }
    }
}
