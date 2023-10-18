
using Bookstore.Application.UseCases.DTO;

using Bookstore.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Implementation.Validators
{
    public class CreateGenreValidator:AbstractValidator<CreateGenreDto>
    {
       
        public CreateGenreValidator(BookstoreContext context)
        {
          
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required").Must(x => !context.Genres.Any(g => g.Name == x)).WithMessage("Name already exists");
            RuleFor(x => x.ParentId).Must(x => x == null || context.Genres.Any(g => g.Id == x && g.IsActive)).WithMessage("Parent genre doesn't exist.");
        }
    }

    public class UpdateGenreValidator : AbstractValidator<UpdateGenreDto>
    {
       
        public UpdateGenreValidator(BookstoreContext context)
        {
            
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.ParentId).Must(x => x == null || context.Genres.Any(g => g.Id == x && g.IsActive)).WithMessage("Parent genre doesn't exist.");
        }
    }
}
