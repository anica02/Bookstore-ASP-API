using Bookstore.Application.Exceptions;
using Bookstore.Application.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Application.UseCaseHandiling
{
    public class AuthorizationQueryHanler : IQueryHandler
    {
        private IApplicationActor _actor;
        private IQueryHandler _next;


        public AuthorizationQueryHanler(IApplicationActor actor, IQueryHandler next)
        {
            _actor = actor;
            if (next == null)
            {
                throw new ArgumentNullException("next");
            }
            _next = next;
        }
        public TResult HandleQuery<TSerach, TResult>(IQuery<TSerach, TResult> query, TSerach serach) where TResult : class
        {
            if (!_actor.AllowedUseCases.Contains(query.Id))
            {
                throw new UnauthorizedUseCaseExecutionException(_actor.Username, query.Name);
            }

            return _next.HandleQuery(query, serach);
        }
    }
}
