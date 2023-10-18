using Bookstore.Application;
using Bookstore.Application.Exceptions;
using Bookstore.Application.UseCases.Commands;
using Bookstore.Application.UseCases.DTO;
using Bookstore.DataAccess;
using Bookstore.Domain.Entities;
using Bookstore.Implementation.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Implementation.UseCases.Commands
{
    public class EfCreateOrderCommand : EfUseCase, ICreateOrderCommand
    {
        private readonly IApplicationActor _actor;
        private readonly CreateOrderValidator _validator;
        public EfCreateOrderCommand(
             BookstoreContext context,
            IApplicationActor actor,
            CreateOrderValidator validator) : base(context)
        {
            _actor = actor;
            _validator = validator;
        }

        public int Id => 16;

        public string Name => "Order create";

        public string Description => "";

        public void Execute(CreateOrderDto request)
        {
            var cart = Context.Carts.Where(x => x.UserId == _actor.Id).Include(x=>x.CartItems).FirstOrDefault();
            if (cart==null)
            {
                throw new ConflictExceptionCreating("order", "Can't create order because your cart is empty");
            }

            _validator.ValidateAndThrow(request);

        
            Order order = new Order();
            order.Address = request.Address;
            order.DeliveryMethod = request.DeliveryMethod;
            order.PaymentMethod = request.PaymentMethod;
            order.UserId = _actor.Id;
            Context.Orders.Add(order);
            Context.SaveChanges();

            foreach(var item in cart.CartItems)
            {
                OrderItem oItem = new OrderItem();
                oItem.OrderId = order.Id;
                oItem.BookPublisherId = item.BookPublisherId;
                oItem.Quantity = item.Quantity;
          
                Context.OrderItems.Add(oItem);
            }

            Context.CartItems.RemoveRange(cart.CartItems);
            Context.Carts.Remove(cart);
            Context.SaveChanges();

        }
    }
}
