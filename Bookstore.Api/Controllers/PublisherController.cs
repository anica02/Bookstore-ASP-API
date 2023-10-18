
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
using Bookstore.Application.UseCase.Queries.Seraches;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bookstore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PublisherController : ControllerBase
    {


        private ICommandHandler _commandHandler;
        private IQueryHandler _queryHandler;
        public PublisherController(ICommandHandler commandHandler, IQueryHandler queryHandler)
        {
            _queryHandler = queryHandler;
            _commandHandler = commandHandler;

        }

        [HttpGet]
        public IActionResult Get([FromQuery] PublisherSearch search,
                                 [FromServices] IGetPublishersQuery query)
        {
            return Ok(_queryHandler.HandleQuery(query, search));
        }

        // GET api/<PublisherController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IFindPublisherQuery query)
        {

            return Ok(_queryHandler.HandleQuery(query, id));

        }

        // POST api/<PublisherController>
        [HttpPost]
        public IActionResult Post([FromBody] CreatePublisherDto dto, [FromServices] ICreatePublisherCommand command)
        {
            _commandHandler.HandleCommand(command, dto);
            return StatusCode(201);

        }

        // PUT api/<PublisherController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdatePublisherDto dto, [FromServices] IUpdatePublisherCommand command)
        {
            dto.Id = id;
            _commandHandler.HandleCommand(command, dto);
            return NoContent();
        }

        // DELETE api/<PublisherController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromBody] DeleteEntityDto dto, [FromServices] IDeletePublisherCommand command)
        {
            dto.Id = id;
            _commandHandler.HandleCommand(command, dto);
            return NoContent();
        }
    }
}
