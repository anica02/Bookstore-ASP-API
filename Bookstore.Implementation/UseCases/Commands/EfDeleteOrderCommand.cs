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
    public class EfDeleteOrderCommand : EfUseCase, IDeleteOrderCommand
    {
        private readonly IApplicationActor _actor;

        public EfDeleteOrderCommand(
             BookstoreContext context,
            IApplicationActor actor
           ) : base(context)
        {
            _actor = actor;
        }

        public int Id => 24;
        public string Name => "Order delete";

        public string Description => "";

        public void Execute(DeleteEntityDto request)
        {
            var order=Context.Orders.Include(x=>x.OrderItems).FirstOrDefault(x => x.Id == request.Id);


            if (order == null || !order.IsActive || order.DeletedAt.HasValue)
            {
                throw new EntityNotFoundException(request.Id, "order");
            }

            Context.OrderItems.RemoveRange(order.OrderItems);
            Context.Entry(order).State = EntityState.Deleted;
            //Context.Orders.Remove(order);
            Context.SaveChanges();
        }
    }
}
