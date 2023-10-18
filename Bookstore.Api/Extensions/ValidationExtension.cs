
using Bookstore.API.DTO;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.API.Extensions
{
    public static class ValidationExtension
    {
        public static UnprocessableEntityObjectResult ToUnprocessableEntity (this ValidationResult result)
        {
            var errors = result.Errors.Select(x => new ClientErrorDto
            {
            
            Error=x.ErrorMessage,
            Property=x.PropertyName
            
            });

            return new UnprocessableEntityObjectResult(errors);
        }

       
    }

    
}
