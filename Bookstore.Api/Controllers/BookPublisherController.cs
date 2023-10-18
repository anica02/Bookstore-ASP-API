

using Bookstore.Application.UseCases.DTO;
using Bookstore.Application.UseCases.Commands;
using Bookstore.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookstore.Application.UseCaseHandiling;
using Bookstore.Application.UseCases.Queries;
using Bookstore.Application.UseCases.Queries.Searches;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bookstore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookPublisherController :ControllerBase
    {

        private ICommandHandler _commandHandler;
        private IQueryHandler _queryHandler;
        public BookPublisherController(ICommandHandler commandHandler, IQueryHandler queryHandler)
        {
            _queryHandler = queryHandler;
            _commandHandler = commandHandler;

        }

        // GET: api/<BookPublisherController>
        [HttpGet]
        public IActionResult Get([FromQuery] BookPublishersSearch search, [FromServices] IGetBookPublishersQuery query)
        {

            return Ok(_queryHandler.HandleQuery(query, search));
        }

        // GET api/<BookPublisherController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IFindBookPublisherQuery query)
        {
            return Ok(_queryHandler.HandleQuery(query, id));
        }

        // POST api/<BookPublisherController>
        [HttpPost]
        public IActionResult Post([FromBody] CreateBookPublisherDto dto, [FromServices] ICreateBookPublisherCommand command)
        {
            _commandHandler.HandleCommand(command, dto);
            return StatusCode(201);

        }

        // PUT api/<BookPublisherController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateBookPublisheraDto dto, [FromServices] IUpdateBookPublisherCommand command)
        {
            dto.Id = id;
            _commandHandler.HandleCommand(command, dto);
            return NoContent();
        }

        // DELETE api/<BookPublisherController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromBody] DeleteEntityDto dto, [FromServices] IDeleteBookPublisherCommand command)
        {

            dto.Id = id;
            _commandHandler.HandleCommand(command, dto);
            return NoContent();
        }
    }
}
