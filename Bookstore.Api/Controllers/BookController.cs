using FluentValidation.Results;
using Bookstore.Application.UseCases.DTO;
using Bookstore.Application.UseCases.Commands;
using Bookstore.DataAccess;
using Bookstore.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Bookstore.API.DTO;
using Bookstore.Application.UseCaseHandiling;
using Bookstore.Application.UseCases.Queries;
using Bookstore.Application.UseCase.Queries.Seraches;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bookstore.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
  
    public class BookController : ControllerBase
    {

        private ICommandHandler _commandHandler;
        private IQueryHandler _queryHandler;
        public BookController(ICommandHandler commandHandler, IQueryHandler queryHandler)
        {
            _queryHandler = queryHandler;
            _commandHandler = commandHandler;

        }


        // GET: api/<BookController>
        [HttpGet]
        public IActionResult Get([FromQuery] BookSearch search, [FromServices] IGetBooksQuery query)
        {

            return Ok(_queryHandler.HandleQuery(query, search));
        }

        [HttpGet("admin")]
        [Authorize]
        public IActionResult GetAdminBooks([FromQuery] BookSearch search, [FromServices] IGetBooksAdminQuery query)
        {
            return Ok(_queryHandler.HandleQuery(query, search));
        }


        // GET api/<BookController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IFindBookQuery query)
        {
            return Ok(_queryHandler.HandleQuery(query, id));
        }

        // POST api/<BookController>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] CreateBookDto dto, [FromServices] ICreateBookCommand command)
        {

            _commandHandler.HandleCommand(command, dto);
            return StatusCode(201);
        }



        // PUT api/<BookController>/5
        [HttpPut("{id}")]
        [Authorize]

        public IActionResult Put(int id, [FromBody] UpdateBookDto dto, [FromServices] IUpdateBookCommand command)
        {

            dto.Id = id;
            _commandHandler.HandleCommand(command, dto);
            return NoContent();
        }

        // DELETE api/<BookController>/5
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id, [FromServices] IDeleteBookCommand command)
        {
            DeleteEntityDto dto = new DeleteEntityDto();
            dto.Id = id;
            _commandHandler.HandleCommand(command, dto);
            return NoContent();

        }




    }
}
