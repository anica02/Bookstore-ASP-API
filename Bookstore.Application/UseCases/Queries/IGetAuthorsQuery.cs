using Bookstore.Application.UseCase.Queries.Seraches;
using Bookstore.Application.UseCases.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Application.UseCases.Queries
{
    public interface IGetAuthorsQuery : IQuery<AuthorSearch, IEnumerable<ReadAuthorDto>>
    {
    }
}
