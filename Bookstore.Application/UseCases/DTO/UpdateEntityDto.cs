using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bookstore.Application.UseCases.DTO
{
    public class UpdateEntityDto
    {
        [JsonIgnore]
        public int? Id { get; set; }
    }
}
