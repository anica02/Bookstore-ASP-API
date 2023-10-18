

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
    public class UpdateUserValidator:AbstractValidator<UpdateUserDto>
    {
      
        public UpdateUserValidator(BookstoreContext context)
        {
         
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.RoleId).NotEmpty().WithMessage("Role id is required").Must(x => context.Roles.Any(r => r.Id == x && r.IsActive)).WithMessage("Role id does not exits");
            /*RuleFor(x => x.IsActive).NotEmpty().WithMessage("Is active is required");*/
        }
    }
}
