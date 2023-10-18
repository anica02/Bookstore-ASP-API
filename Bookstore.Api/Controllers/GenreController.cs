
using Bookstore.Application.UseCases.DTO;
using Bookstore.Application.UseCases.Commands;

using Bookstore.DataAccess;
using Bookstore.Domain.Entities;
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

    public class GenreController : ControllerBase
    {

     
        private ICommandHandler _commandHandler;
        private IQueryHandler _queryHandler;
        public GenreController( ICommandHandler commandHandler, IQueryHandler queryHandler) 
        {
            _queryHandler = queryHandler;
            _commandHandler = commandHandler;

        }

        [HttpGet]
        public IActionResult Get([FromQuery] GenreSearch search,
                                [FromServices] IGetGenresQuery query)
        {
            return Ok(_queryHandler.HandleQuery(query, search));
        }

        // GET: api/<GenreController>/id
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IFindGenreQuery query)
        {

            return Ok(_queryHandler.HandleQuery(query,id));

        }



        // POST api/<GenreController>
        [HttpPost]
        public IActionResult Post([FromBody] CreateGenreDto dto, [FromServices] ICreateGenreCommand command)
        {
            _commandHandler.HandleCommand(command, dto);
            return StatusCode(201);
        }

        // PUT api/<GenreController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateGenreDto dto, [FromServices] IUpdateGenreCommand command)
        {
            dto.Id = id;
            _commandHandler.HandleCommand(command, dto);
            return NoContent();
        }

        // DELETE api/<GenreController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromBody] DeleteEntityDto dto, [FromServices] IDeleteGenreCommand command)
        {
            dto.Id = id;
            _commandHandler.HandleCommand(command, dto);
            return NoContent();
        }
    }
}
