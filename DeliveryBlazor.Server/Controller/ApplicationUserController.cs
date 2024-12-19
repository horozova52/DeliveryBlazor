using DeliveryBlazor.UseCase.Features.ApplicationUsers.Commands;
using DeliveryBlazor.UseCase.Features.ApplicationUsers.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryBlazor.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class ApplicationUserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ApplicationUserController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("list")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _mediator.Send(new GetAllApplicationUsersQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var result = await _mediator.Send(new GetApplicationUserByIdQuery { Id = id });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateApplicationUserCommand command)
        {
            var userId = await _mediator.Send(command);
            return Ok(new { Id = userId });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, UpdateApplicationUserCommand command)
        {
            command.Id = id;
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            await _mediator.Send(new DeleteApplicationUserCommand { Id = id });
            return NoContent();
        }
    }
}
