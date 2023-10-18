using Bookstore.Application.Exceptions;
using Bookstore.Application.UseCases.DTO;
using Bookstore.Application.UseCases.Queries;
using Bookstore.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Implementation.UseCases.Queries
{
    public class EfFindAuthorQuery: EfUseCase, IFindAuthorQuery
    {
        public EfFindAuthorQuery(BookstoreContext context) : base(context)
        {

        }
        public int Id => 29;

        public string Name => "Find author";

        public string Description => "";

        public ReadAuthorDto Execute(int search)
        {
            var author = Context.Authors.FirstOrDefault(x => x.Id == search);
           

            if (author == null || !author.IsActive || author.DeletedAt.HasValue)
            {
                throw new EntityNotFoundException(search, nameof(author));
            }


            return new ReadAuthorDto
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                Pseudonym = author.Pseudonym != null ? author.Pseudonym : "none",
                DateOfBirth = author.DateOfBirth,
                Country = author.Country
            };

        }


       

         
    }

          
    
}
