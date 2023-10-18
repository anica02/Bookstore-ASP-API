
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
    public class CreatePublisherValidator:AbstractValidator<CreatePublisherDto>
    {
       
        public CreatePublisherValidator(BookstoreContext context)
        {
            
            RuleLevelCascadeMode = CascadeMode.Stop;
            Regex regexL = new Regex(@"^[A-ZČĆŠĐŽ]{1}[a-zčćšđž]{2,15}(\s[A-ZČĆŠĐŽ]{1}[a-zčćšđž]{0,15})*(\s[\d]{1,5})*,(\s[A-ZČĆŠĐŽa-zčćšđž]{2,10})$");
            Regex regexU = new Regex(@"^https?:\/\/(?:www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b(?:[-a-zA-Z0-9()@:%_\+.~#?&\/=]*)$");
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Name is required").Must(x=> !context.Publishers.Any(p=>p.Name==x)).WithMessage("Name already exists");
            RuleFor(x => x.Location).NotEmpty().WithMessage("Location is required").Matches(regexL).WithMessage("Valid format: Dimitrija Tucovića 12, Belgarde");
            RuleFor(x => x.Website).Must(x=>!context.Publishers.Any(p => p.Website == x && p.IsActive)).WithMessage("Website is taken").Matches(regexU).When(x => !string.IsNullOrEmpty(x.Website));
        }
    }
}
