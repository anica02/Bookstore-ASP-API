
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
    public class UpdateAuthorValidator : AbstractValidator<UpdateAuthorDto>
    {
      
        public UpdateAuthorValidator(BookstoreContext context)
        {
          
            RuleLevelCascadeMode = CascadeMode.Stop;

            var dateMust = new DateTime(2004, 01, 01);
            Regex regexN = new Regex(@"^[A-ZĆČĐŽŠ]{1}[a-zćčđžš]{2,15}(\s[A-ZČĆŠĐŽ]{1}[a-zčćšđž]{2,15})*$");

            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required").Matches(regexN);
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required").Matches(regexN);
            RuleFor(x => x.Country).NotEmpty().WithMessage("Country is required");
        }
    }
}
