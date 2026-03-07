using EduNEXT.Application.UseCases.Commands.Lessons.CancelLessonCommand;
using EduNEXT.Application.UseCases.Commands.Lessons.ChangeLessonsStatus;
using EduNEXT.Application.UseCases.Commands.Lessons.UpdateLesson;
using EduNEXT.Application.UseCases.Queries.Lessons.GetAllLessonsByFilter;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduNEXT.API.Controllers;

[Authorize]
[Route("api/lessons")]
[ApiController]
public class LessonsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [Route("status/")]
    public async Task<IActionResult> ChangeLessonStatus([FromBody] ChangeLessonStatusCommand command)
    {
        var result = await mediator.Send(command);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
            
        return BadRequest(result.Error);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateLessonCommand command)
    {
        var result = await mediator.Send(command);

        if (result.IsSuccess)
        {
            return Ok();
        }
            
        return BadRequest(result.Error);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync([FromBody] CancelLessonCommand command)
    {
        var result = await mediator.Send(command);

        if (result.IsSuccess)
        {
            return NoContent();
        }
            
        return BadRequest(result.Error);
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync(string criteriaName)
    {
        return Ok(await mediator.Send(new GetAllLessonsByFilterQuery()
        {
            CriteriaName = criteriaName
        }));
    }
}