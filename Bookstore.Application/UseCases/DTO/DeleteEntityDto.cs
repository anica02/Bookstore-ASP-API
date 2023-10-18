using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bookstore.Application.UseCases.DTO
{
    public class DeleteEntityDto
    {
        [JsonIgnore]
        public int Id { get; set; }
    }
}
