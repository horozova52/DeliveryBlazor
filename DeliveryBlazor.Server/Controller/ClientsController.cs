using DeliveryApp.UseCase.Features.Client.Commands;
using DeliveryBlazor.UseCase.Features.ApplicationUsers.Queries;
using DeliveryBlazor.UseCase.Features.Clients.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryBlazor.Server.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateClientCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateClientCommand command)
        {
            if (id != command.Id) return BadRequest("ID mismatch.");
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteClientCommand { Id = id });
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _mediator.Send(new GetAllClientsQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var client = await _mediator.Send(new GetClientByIdQuery { Id = id });
            if (client == null) return NotFound();
            return Ok(client);
        }
    }
}