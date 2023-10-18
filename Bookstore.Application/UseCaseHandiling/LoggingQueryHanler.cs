using Bookstore.Application.Logging;
using Bookstore.Application.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Application.UseCaseHandiling
{
    public class LoggingQueryHanler: IQueryHandler
    {
        private IQueryHandler _next;
        private IApplicationActor _actor;
        private IUseCaseLogger _logger;

        public LoggingQueryHanler(IQueryHandler next, IApplicationActor actor, IUseCaseLogger logger)
        {
            
            if (next == null)
            {
                throw new ArgumentNullException("next");
            }
            _next = next;
            _actor = actor;
            _logger = logger;
        }
        public TResult HandleQuery<TSerach, TResult>(IQuery<TSerach, TResult> query, TSerach serach) where TResult : class
        {
            _logger.Add(new UseCaseLogEntry
            {
                Actor=_actor.Username,
                ActorId=_actor.Id,
                Data=serach,
                UseCaseName=query.Name
            });

            return _next.HandleQuery(query,serach);
        }
    }
}
