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
    public class EfUpdateOrderCommand: EfUseCase, IUpdateOrderCommand
    {
        private readonly IApplicationActor _actor;

        public EfUpdateOrderCommand(
             BookstoreContext context,
             IApplicationActor actor
           ) : base(context)
        {
            _actor = actor;
        }
        public int Id => 17;

        public string Name => "Order edit";

        public string Description => "";

        public void Execute(UpdateOrderDto request)
        {
            var order = Context.Orders.Find(request.Id.Value);

            if (order == null  || order.DeletedAt.HasValue)
            {
                throw new EntityNotFoundException(request.Id.Value, "order");
            }

            order.IsActive = request.IsActive;
            order.ModifiedAt = DateTime.UtcNow;
            order.ModifiedBy = _actor.Username;
            Context.Entry(order).State = EntityState.Modified;
            Context.SaveChanges();
                
        }
    }
}
