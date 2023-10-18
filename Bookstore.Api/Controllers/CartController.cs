using Bookstore.Application.UseCaseHandiling;
using Bookstore.Application.UseCases.Commands;
using Bookstore.Application.UseCases.DTO;
using Bookstore.Application.UseCases.Queries;
using Bookstore.Application.UseCases.Queries.Searches;
using Bookstore.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bookstore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class CartController : ControllerBase
    {


        private ICommandHandler _commandHandler;
        private IQueryHandler _queryHandler;
        public CartController(ICommandHandler commandHandler, IQueryHandler queryHandler)
        {
            _queryHandler = queryHandler;
            _commandHandler = commandHandler;

        }

        // GET: api/<CartController>
      /*
        [HttpGet]
        public IActionResult Get([FromQuery] CartSearch search,
                                 [FromServices] IGetCartsQuery query)
        {
            return Ok(_queryHandler.HandleQuery(query, search));
        }*/

        // GET api/<CartController>/5
        [HttpGet]
        public IActionResult Get([FromQuery] CartSearch search, [FromServices] IFindUserCartQuery query)
        {
            return Ok(_queryHandler.HandleQuery(query, search));
        }

        // POST api/<CartController>
        [HttpPost]
        public IActionResult Post([FromBody] CreateUserCartDto dto, [FromServices] ICreateUserCartCommand command)
        {
            _commandHandler.HandleCommand(command, dto);
            return StatusCode(201);
        }

        // PUT api/<CartController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CreateUserCartDto dto, [FromServices] IUpdateUserCartCommand command)
        {
            dto.Id = id;
            _commandHandler.HandleCommand(command, dto);
            return NoContent();
        }

        // DELETE api/<CartController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromBody] DeleteEntityDto dto, [FromServices] IDeleteCartCommand command)
        {
            dto.Id = id;
            _commandHandler.HandleCommand(command, dto);
            return NoContent();
        }
    }
}
