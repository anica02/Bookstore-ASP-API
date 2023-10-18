using Bookstore.Application.Exceptions;
using Bookstore.Application.UseCases.DTO;
using Bookstore.Application.UseCases.Queries;
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
    public class EfFindUserQuery: EfUseCase, IFindUserQuery
    {
        public EfFindUserQuery(BookstoreContext context) : base(context)
        {

        }
        public int Id => 28;

        public string Name => "Find user";

        public string Description => "";

        public ReadUserDto Execute(int search)
        {
            User user = Context.Users
                                .Include(x => x.Role)
                                .FirstOrDefault(x => x.Id == search);

            if (user == null)
            {
                throw new EntityNotFoundException(search, nameof(user));
            }

           
            return new ReadUserDto
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role.Name,
                RoleId=user.RoleId,
                ModifiedAt=user.ModifiedAt.HasValue ? user.ModifiedAt.Value : null,
                ModifiedBy=user.ModifiedBy,
                CreatedAt=user.CreatedAt,
                IsActive=user.IsActive
            };
        }


    }
}
