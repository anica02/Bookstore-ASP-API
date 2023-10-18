using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Application.UseCases.DTO
{
    public class RegisterUserDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
      
    }

    public class UpdateUserDto: UpdateEntityDto
    {
        public bool IsActive { get; set; }
        public int? RoleId { get; set; }
       
    }


}
