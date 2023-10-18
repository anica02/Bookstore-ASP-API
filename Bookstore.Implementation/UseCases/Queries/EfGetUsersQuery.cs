using Bookstore.Application;
using Bookstore.Application.UseCase.Queries.Seraches;
using Bookstore.Application.UseCases.DTO;
using Bookstore.Application.UseCases.Queries;
using Bookstore.DataAccess;
using Bookstore.Domain.Entities;
using Bookstore.Implementation.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Implementation.UseCases.Queries
{
    public class EfGetUsersQuery : EfUseCase, IGetUsersQuery
    {
        private readonly IApplicationActor _actor;

        public EfGetUsersQuery(BookstoreContext context, IApplicationActor actor) : base(context)
        {
            _actor = actor;
        }

        public int Id => 34;

        public string Name => "Search users";

        public string Description => "";

        public IEnumerable<ReadUserDto> Execute(UserSearch search)
        {
          

            var query = Context.Users.Include(x => x.Role).AsQueryable();
            query = query.Where(x => x.Id != _actor.Id);
            if (!string.IsNullOrEmpty(search.Username))
            {
                query = query.Where(x => x.Username.Contains(search.Username));
            }

            if (!string.IsNullOrEmpty(search.Email))
            {
                query = query.Where(x => x.Email.ToLower() == search.Email.ToLower());
            }

            if (!string.IsNullOrEmpty(search.Role))
            {
                query = query.Where(x => x.Role.Name.ToLower() == search.Role.ToLower());
            }
         
            IEnumerable<ReadUserDto> result = query.Select(x => new ReadUserDto
            {
                Id = x.Id,
                Username = x.Username,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Role = x.Role.Name,
                RoleId=x.RoleId,
                ModifiedAt=x.ModifiedAt.Value,
                ModifiedBy=x.ModifiedBy,
                CreatedAt=x.CreatedAt,
                IsActive=x.IsActive
              
            }).ToList();

            return result;
        }
    }
}
