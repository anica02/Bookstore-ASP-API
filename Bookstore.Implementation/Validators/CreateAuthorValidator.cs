
using Bookstore.Application.UseCases.DTO;
using Bookstore.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bookstore.Implementation.Validators
{
    public class CreateAuthorValidator:AbstractValidator<CreateAuthorDto>
    {
        

        public CreateAuthorValidator(BookstoreContext context)
        {
            

            RuleLevelCascadeMode = CascadeMode.Stop;

            var dateMust = new DateTime(2004, 01, 01);
            Regex regexN = new Regex(@"^[A-ZĆČĐŽŠ]{1}[a-zćčđžš]{2,15}(\s[A-ZČĆŠĐŽ]{1}[a-zčćšđž]{2,15})*$");

            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required").Matches(regexN);
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required").Matches(regexN);
            RuleFor(x => x.Pseudonym).Must(x => !context.Authors.Any(a => a.Pseudonym == x)).WithMessage("Pseudonym is already taken");
            RuleFor(x => x.DateOfBirth).NotEmpty().WithMessage("Date of birth is required").LessThanOrEqualTo(dateMust).WithMessage("Date of birth should be older than current date");
            RuleFor(x => x.Country).NotEmpty().WithMessage("Country is required");
        }
    }
}
