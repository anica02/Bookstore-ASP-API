using System;
using Bookstore.Application;
using Bookstore.Application.Exceptions;
using Bookstore.Application.UseCases.Commands;
using Bookstore.Application.UseCases.DTO;
using Bookstore.DataAccess;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Implementation.UseCases.Commands
{
    public class EfDeleteBookPublisherCommand:EfUseCase, IDeleteBookPublisherCommand
    {
        private readonly IApplicationActor _actor;

        public EfDeleteBookPublisherCommand(
             BookstoreContext context,
            IApplicationActor actor
           ) : base(context)
        {

            _actor = actor;

        }

        public int Id => 20;

        public string Name => "Book edtion delete";

        public string Description => "";

        public void Execute(DeleteEntityDto request)
        {
            var bookPublisher = Context.BookPublishers.Find(request.Id);

            if (bookPublisher == null || !bookPublisher.IsActive || bookPublisher.DeletedAt.HasValue)
            {
                throw new EntityNotFoundException(request.Id, "book publisher");
            }

            if (Context.OrderItems.Any(x => x.BookPublisherId == request.Id && x.IsActive))
            {
                throw new ConflictException(request.Id, "book publisher", "Book edition cannot be deleted because it is part of an user order");
            }

            bookPublisher.DeletedAt = DateTime.UtcNow;
            bookPublisher.IsActive = false;
            bookPublisher.DeletedBy = _actor.Username;
            Context.SaveChanges();
        }
    }
}
