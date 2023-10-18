using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Application.UseCases.DTO
{
    public class CreateGenreDto
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }
    }

    public class UpdateGenreDto:UpdateEntityDto
    {
       
        public string Name { get; set; }
        public int? ParentId { get; set; }
    }
}
