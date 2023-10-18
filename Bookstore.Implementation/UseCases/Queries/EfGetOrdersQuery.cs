using Bookstore.Application.Exceptions;
using Bookstore.Application.UseCases.DTO;
using Bookstore.Application.UseCases.Queries;
using Bookstore.Application.UseCases.Queries.Searches;
using Bookstore.DataAccess;
using Bookstore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Implementation.UseCases.Queries
{
    public class EfGetOrdersQuery : EfUseCase, IGetOrdersQuery
    {
        public EfGetOrdersQuery(BookstoreContext context) : base(context)
        {

        }

        public int Id => 41;

        public string Name => "Search orders";

        public string Description => "";

        public IEnumerable<ReadOrdeDto> Execute(CartSearch search)
        {

          
            var query = Context.Orders.Include(x => x.OrderItems).AsQueryable();

            if (query == null)
            {
                throw new NotFoundException("orders");

            }

            if (search.UserId.HasValue)
            {
                query = query.Where(x => x.UserId == search.UserId);
            }

            if (search.UserId.HasValue)
            {
                query = query.Where(x => x.OrderItems.Any(i => i.BookPublisherId == search.BookPublisherId));
            }

            
              

            var books = Context.Books
             .Include(x => x.BookPublishers)
             .ThenInclude(bp => bp.Image)
             .Include(x => x.BookPublishers)
             .ThenInclude(bp => bp.Prices)
             .ToList(); 


            List<ReadOrdeDto> result = new List<ReadOrdeDto>();

            foreach (var order in query)
            {
                ReadOrdeDto readUserOrder = new ReadOrdeDto
                {
                    Id = order.Id,
                    UserId=order.UserId,
                    Address = order.Address,
                    DeliveryMethod = order.DeliveryMethod,
                    PaymentMethod = order.PaymentMethod,
                    CreatedAt = order.CreatedAt.Date,
                    IsActive = order.IsActive,
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
        /*

        IEnumerable<ReadOrdeDto> result = query.Select(p => new ReadOrdeDto
        {
            Id = p.Id,
            UserId = p.User.Id,
            Username = p.User.Username,
            OrderItems = p.OrderItems.Select(x => new ReadOrderItemDto
            {
                Id = x.Id,
                BookPublisherId = x.BookPublisherId,
                Quantity = x.Quantity
            }).ToList()

        }).ToList();
        */


    }
}
