using Bookstore.Application.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Application.UseCaseHandiling
{
    public interface IQueryHandler
    {
        TResult HandleQuery<TSerach, TResult>(IQuery<TSerach, TResult> query, TSerach serach) where TResult : class;
    }
}
