using System;
using Bookstore.Application.UseCase.Queries.Seraches;
using Bookstore.Application.UseCases.DTO;
using System.Collections.Generic;

namespace Bookstore.Application.UseCases.Queries
{
    public interface IGetBooksAdminQuery : IQuery<BookSearch, IEnumerable<ReadBookDto>>
    {

    }

}

