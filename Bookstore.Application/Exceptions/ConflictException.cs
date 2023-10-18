using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Application.Exceptions
{
    public class ConflictException:Exception
    {
        public ConflictException(int id, string entityType, string action)
           : base($"Conflict in the request: ({action}) on entity of type {entityType} with an id of {id}.")
        {

        }

    }


    public class ConflictExceptionCreating : Exception
    {

        public ConflictExceptionCreating(string entityType, string action)
          : base($"Conflict in the request: ({action}) on entity of type {entityType}.")
        {

        }


    }
}
