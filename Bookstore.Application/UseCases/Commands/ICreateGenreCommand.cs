using Bookstore.Application.UseCases.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Application.UseCases.Commands
{
    public interface ICreateGenreCommand:ICommand<CreateGenreDto>
    {
    }
}
