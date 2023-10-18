using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.Application;
using Bookstore.Application.Exceptions;
using Bookstore.Application.UseCases.DTO;
using Bookstore.Application.UseCases.Queries;
using Bookstore.DataAccess;
using Bookstore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Implementation.UseCases.Queries
{
    public class EfFindOrderQuery : EfUseCase, IFindOrderQuery
    {
        private readonly IApplicationActor _actor;

        public EfFindOrderQuery(BookstoreContext context, IApplicationActor actor) : base(context)
        {
            _actor = actor;
        }

        public int Id => 32;

        public string Name => "Find order";

        public string Description => "";

        public IEnumerable<ReadOrderUserDto> Execute(int search)
        {
            var orders = Context.Orders
                .Include(x => x.OrderItems)
                .Where(x => x.UserId == search && x.IsActive && !x.DeletedAt.HasValue)
                .ToList();

           var books= Context.Books
            .Include(x => x.BookPublishers)
            .ThenInclude(bp => bp.Image)
            .Include(x => x.BookPublishers)
            .ThenInclude(bp => bp.Prices)
            .ToList(); // Include BookPublishers navigation property


            if (orders.Count == 0)
            {
                throw new EntityNotFoundException(search, nameof(Order));
            }

            List<ReadOrderUserDto> result = new List<ReadOrderUserDto>();

            foreach (var order in orders)
            {
                ReadOrderUserDto readUserOrder = new ReadOrderUserDto
                {
                    Id = order.Id,
                    Address = order.Address,
                    DeliveryMethod = order.DeliveryMethod,
                    PaymentMethod = order.PaymentMethod,
                    CreatedAt= order.CreatedAt.Date,
                    IsActive=order.IsActive,
                    OrderItems = new List<ReadUserOrderItemDto>()
            
                };

                foreach (var item in order.OrderItems)
                {
                    var book = books.FirstOrDefault(b => b.BookPublishers.Any(bp => bp.Id == item.BookPublisherId));
                   
                    if (book != null)
                    {
                        var bookPublisher = book.BookPublishers.FirstOrDefault(bp => bp.Id == item.BookPublisherId);

                        if (bookPublisher != null)
                        {
                            

                            ReadUserOrderItemDto itemDto = new ReadUserOrderItemDto
                            {
                                Id = item.Id,
                                Name = book.Name,  
                                Image = bookPublisher.Image?.Path,  
                                BookPublisherId = bookPublisher.Id,
                                Price = CalculatePrice(bookPublisher),
                                Quantity = item.Quantity,
                                Total = CalculateTotalPrice(bookPublisher, item.Quantity)
                            };

                            readUserOrder.OrderItems.Add(itemDto);
                        }
                    }
                }


                result.Add(readUserOrder);
            }

            return result;
        }

        private int CalculatePrice(BookPublisher bookPublisher)
        {
            var discount = bookPublisher.Discounts.FirstOrDefault(d => d.IsActive && d.StartsFrom <= DateTime.Today && d.EndsAt >= DateTime.Today);

            if (discount != null)
            {
                return (int)bookPublisher.Prices
                    .Where(x => x.IsActive)
                    .OrderByDescending(x => x.Id)
                    .Select(x => x.BookPrice)
                    .First() * (int)(1 - (discount.DiscountPercentage / 100.0));
            }
            else
            {
                int price = (int)bookPublisher.Prices
                .Where(x => x.IsActive)
                .OrderByDescending(x => x.Id)
                .Select(x => x.BookPrice)
                .FirstOrDefault();

                return price;
            }
        }

        private int CalculateTotalPrice(BookPublisher bookPublisher, int quantity)
        {
            return CalculatePrice(bookPublisher) * quantity;
        }
    }
}
