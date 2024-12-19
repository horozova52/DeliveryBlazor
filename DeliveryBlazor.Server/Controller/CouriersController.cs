using DeliveryBlazor.UseCase.Features.Couriers.Commands;
using DeliveryBlazor.UseCase.Features.Couriers.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryBlazor.Server.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class CouriersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CouriersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCourierCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCourierCommand command)
        {
            if (id != command.Id) return BadRequest();
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteCourierCommand { Id = id });
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllCouriersQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetCourierByIdQuery { Id = id });
            return Ok(result);
        }
    }
}
