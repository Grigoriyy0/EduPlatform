using EduNEXT.Application.Commands.TimeSlot.AssignTimeSlotsCommand;
using EduNEXT.Application.Queries.TimeSlots;
using EduNEXT.Application.Queries.TimeSlots.GetAllTimeSlots;
using EduNEXT.Application.Queries.TimeSlots.GetTImeSlotsByStudentId;
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

        [HttpGet]
        [Route("all/")]
        public async Task<IActionResult> GetAllTimeSlots()
        {
            return Ok(await _mediator.Send(new GetAllTimeSlotsQuery()));
        }

        [HttpGet]
        [Route("{id:guid}/")]
        public async Task<IActionResult> GetTimeSlotById(Guid id)
        {
            return Ok(await _mediator.Send(new GetTimeSlotsByStudentIdQuery
            {
                StudentId = id
            }));
        }
    }
}
