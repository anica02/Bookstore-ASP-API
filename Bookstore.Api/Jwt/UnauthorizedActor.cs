
using Bookstore.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.API.Jwt
{
    public class UnauthorizedActor:IApplicationActor
    {
        public int Id => 0;

        public string Email => "";

        public string Username => "unauthorized";

        public IEnumerable<int> AllowedUseCases => new List<int> {9 , 30, 38, 37};
    }
}
