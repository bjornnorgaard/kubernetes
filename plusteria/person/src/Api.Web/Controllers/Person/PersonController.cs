using System;
using System.Threading.Tasks;
using Application.Features.Person;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Web.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PersonController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<GetAllPerson.Result>> Get([FromQuery] int take = 10,
                                                                 [FromQuery] int skip = 0)
        {
            var command = new GetAllPerson.Command { Take = take, Skip = skip };
            var result = await _mediator.Send(command);

            return result;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetPerson.Result>> Get(Guid id)
        {
            var command = new GetPerson.Command { Id = id };

            var result = await _mediator.Send(command);

            return result;
        }

        [HttpPost]
        public async Task<ActionResult<CreatePerson.Result>> Post(
            [FromBody] CreatePerson.Command command)
        {
            var result = await _mediator.Send(command);

            return result;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UpdatePerson.Result>> Post(Guid id,
                                                                  [FromBody]
                                                                  UpdatePerson.Command command)
        {
            if (id != command.Id) return BadRequest("Ids not matching");

            var result = await _mediator.Send(command);

            return result;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DeletePerson.Result>> Post(Guid id)
        {
            var command = new DeletePerson.Command { Id = id };

            var result = await _mediator.Send(command);

            return result;
        }
    }
}



