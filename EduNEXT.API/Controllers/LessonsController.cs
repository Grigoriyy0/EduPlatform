using EduNEXT.Application.Commands.Lessons.ChangeLessonsStatus;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EduNEXT.API.Controllers
{
    [Route("api/lessons")]
    [ApiController]
    public class LessonsController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [Route("change-status/")]
        public async Task<IActionResult> ChangeLessonStatus([FromBody] ChangeLessonStatusCommand command)
        {
            var result = await mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok();
            }
            
            return BadRequest(result.Error);
        }
    }
}
