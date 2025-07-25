using EduNEXT.Application.Commands.TimeSlot.AssignTimeSlotsCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EduNEXT.API.Controllers
{
    [Route("api/time-slots")]
    [ApiController]
    public class TimeSlotsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TimeSlotsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("add/")]
        public async Task<IActionResult> AddTimeSlotAsync([FromBody] AssignTimeSlotsCommand command)
        {
            var result =  await _mediator.Send(command);

            var val = result.TryGetValue(out var slot);

            if (val)
            {
                return Ok(slot);
            }
            
            return BadRequest(result.Error);
        }
    }
}
