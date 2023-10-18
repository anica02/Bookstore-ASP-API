using Bookstore.Application.UseCases;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Application.UseCaseHandiling
{
    public class TimeTrackingQueryHandeler: IQueryHandler
    {
        private IQueryHandler _next;

        public TimeTrackingQueryHandeler(IQueryHandler next)
        {
            _next = next;
        }

        public TResult HandleQuery<TSerach, TResult>(IQuery<TSerach, TResult> query, TSerach serach) where TResult : class
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var result = _next.HandleQuery(query, serach);

            stopwatch.Stop();

            Console.WriteLine($"Usecase: {query.Name}, Time: {stopwatch.ElapsedMilliseconds} ms");

            return result;
        }
    }
}
