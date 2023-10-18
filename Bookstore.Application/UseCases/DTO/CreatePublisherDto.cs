using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Application.UseCases.DTO
{
    public class CreatePublisherDto
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string? Website { get; set; }

    }

    public class UpdatePublisherDto:UpdateEntityDto
    {

        public string Name { get; set; }
        public string Location { get; set; }
        public string? Website { get; set; }
    }
}
