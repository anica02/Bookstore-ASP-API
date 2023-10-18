using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Application.UseCases.DTO
{
    public class UpdateOrderDto:UpdateEntityDto
    {
        public bool IsActive { get; set; }
    }
}
