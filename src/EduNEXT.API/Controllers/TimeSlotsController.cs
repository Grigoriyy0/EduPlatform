using EduNEXT.Application.UseCases.Commands.TimeSlot.AssignTimeSlotsCommand;
using EduNEXT.Application.UseCases.Queries.TimeSlots.GetAllTimeSlots;
using EduNEXT.Application.UseCases.Queries.TimeSlots.GetTImeSlotsByStudentId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduNEXT.API.Controllers;

[Authorize]
[Route("api/time-slots")]
[ApiController]
public class TimeSlotsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [Route("add/")]
    public async Task<IActionResult> AddTimeSlotAsync([FromBody] AssignTimeSlotsCommand command)
    {
        var result =  await mediator.Send(command);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
            
        return BadRequest(result.Error);
    }

    [HttpGet]
    [Route("all/")]
    public async Task<IActionResult> GetAllTimeSlots()
    {
        return Ok(await mediator.Send(new GetAllTimeSlotsQuery()));
    }

    [HttpGet]
    [Route("{id:guid}/")]
    public async Task<IActionResult> GetTimeSlotById(Guid id)
    {
        return Ok(await mediator.Send(new GetTimeSlotsByStudentIdQuery
        {
            StudentId = id
        }));
    }
}