using Bookstore.Application;
using Bookstore.Application.Exceptions;
using Bookstore.Application.UseCases.Commands;
using Bookstore.Application.UseCases.DTO;
using Bookstore.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Implementation.UseCases.Commands
{
    public class EfDeletePublisherCommand:EfUseCase, IDeletePublisherCommand
    {
        private readonly IApplicationActor _actor;

        public EfDeletePublisherCommand(
             BookstoreContext context,
            IApplicationActor actor
           ) : base(context)
        {
            _actor = actor;
        }

        public int Id => 22;

        public string Name => "Publisher delete";

        public string Description => "";

        public void Execute(DeleteEntityDto request)
        {
            var publisher = Context.Publishers.Include(x=>x.BookPublishers).FirstOrDefault(x => x.Id == request.Id); 
          
            if (publisher == null || !publisher.IsActive || publisher.DeletedAt.HasValue)
            {
                throw new EntityNotFoundException(request.Id, "publisher");
            }

            if (publisher.BookPublishers.Any())
            {
                throw new ConflictException(request.Id, "publisher", "Publisher cannot be deleted because it has books published");
            }

            publisher.IsActive = false;
            publisher.DeletedAt = DateTime.UtcNow;
            publisher.DeletedBy = _actor.Username;
            Context.SaveChanges();

        }
    }
}
