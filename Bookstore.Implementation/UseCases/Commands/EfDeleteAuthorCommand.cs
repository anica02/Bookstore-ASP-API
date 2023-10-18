using Bookstore.Application;
using Bookstore.Application.Exceptions;
using Bookstore.Application.UseCases.Commands;
using Bookstore.Application.UseCases.DTO;
using Bookstore.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Implementation.UseCases.Commands
{
    public class EfDeleteAuthorCommand: EfUseCase, IDeleteAuthorCommand
    {
        private readonly IApplicationActor _actor;

        public EfDeleteAuthorCommand(
             BookstoreContext context,
            IApplicationActor actor
           ) : base(context)
        {
            _actor = actor;
        }

        public int Id => 18;

        public string Name => "Author delete";

        public string Description => "";

        public void Execute(DeleteEntityDto request)
        {
            var author = Context.Authors.Find(request.Id);

            if (author == null || !author.IsActive || author.DeletedAt.HasValue)
            {
                throw new EntityNotFoundException(request.Id, "author");
            }

            if (author.BookAuthors.Any())
            {
                throw new ConflictException(request.Id, "author", "Author cannot be deleted because he has books that have been published");
            
            }

            author.DeletedAt = DateTime.Now;
            author.IsActive = false;
            author.DeletedBy = _actor.Username;
            Context.SaveChanges();
        }
    }
}
