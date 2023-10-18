using System.Collections.Generic;

namespace Bookstore.Application
{
    public interface IApplicationActor
    {
        int Id { get; }
        string Email { get; }
        string Username {get; }
        IEnumerable<int> AllowedUseCases { get; }
    }
}
