using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Application.Exceptions
{
    public class EntityNotFoundException :Exception
    {
        public EntityNotFoundException(int id, string entityType)
            :base($"Entity of type {entityType} with an id of {id} was not found.")
        {

        }
    }
    public class NotFoundException : Exception
    {
        public NotFoundException( string entityType)
            : base($"Entity of type {entityType}  was not found.")
        {

        }
    }

}
