using Bookstore.Application.UseCases.DTO;

using Bookstore.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bookstore.Implementation.Validators
{
    public class UpdatePublisherValidator:AbstractValidator<UpdatePublisherDto>
    {
    
        public UpdatePublisherValidator(BookstoreContext context)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;
            Regex regexL = new Regex(@"^[A-ZČĆŠĐŽ]{1}[a-zčćšđž]{2,15}(\s[A-ZČĆŠĐŽ]{1}[a-zčćšđž]{0,15})?(\s[\d]{1,5})?,(\s[A-ZČĆŠĐŽ]{1}[a-zčćšđž]{2,10})$");
            Regex regexU = new Regex(@"^https?:\/\/(?:www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b(?:[-a-zA-Z0-9()@:%_\+.~#?&\/=]*)$");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Location).NotEmpty().WithMessage("Location is required").Matches(regexL).WithMessage("Valid format: Dimitrija Tucovića 12, Belgarde");
            RuleFor(x => x.Website).Matches(regexU).When(x => !string.IsNullOrEmpty(x.Website));
        }
    }
}
