using Bookstore.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Implementation.UseCases
{
    public abstract class EfUseCase
    {
        protected BookstoreContext Context { get; }

        protected EfUseCase(BookstoreContext context)
        {
            Context = context;
        }
    }
}
