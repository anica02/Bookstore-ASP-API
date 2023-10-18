using Bookstore.Application.UseCaseHandiling;
using Bookstore.Application.UseCases.Commands;
using Bookstore.Application.UseCases.DTO;
using Bookstore.Application.UseCases.Queries;
using Bookstore.Application.UseCases.Queries.Searches;
using Microsoft.AspNetCore.Authorization;
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
    public class OrderController : ControllerBase
    {

        private ICommandHandler _commandHandler;
        private IQueryHandler _queryHandler;
        public OrderController(ICommandHandler commandHandler, IQueryHandler queryHandler)
        {
            _queryHandler = queryHandler;
            _commandHandler = commandHandler;

        }

        // GET: api/<OrderController>
        [HttpGet]
        public IActionResult Get([FromQuery] CartSearch search,
                                 [FromServices] IGetOrdersQuery query)
        {
            return Ok(_queryHandler.HandleQuery(query, search));
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IFindOrderQuery query)
        {
            return Ok(_queryHandler.HandleQuery(query, id));
        }

        // POST api/<OrderController>
        [HttpPost]
        public IActionResult Post([FromBody] CreateOrderDto dto, [FromServices]ICreateOrderCommand command)
        {
            _commandHandler.HandleCommand(command, dto);
            return StatusCode(201);
        }

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateOrderDto dto, [FromServices] IUpdateOrderCommand command)
        {
            dto.Id = id;
            _commandHandler.HandleCommand(command, dto);
            return NoContent();
        }

        // DELETE api/<CartController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id,  [FromServices] IDeleteOrderCommand command)
        {
            DeleteEntityDto dto = new DeleteEntityDto();
            dto.Id = id;
            _commandHandler.HandleCommand(command, dto);
            return NoContent();
        }

    }
}
