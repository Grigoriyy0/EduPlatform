using EduNEXT.Application.UseCases.Commands.Lessons.CancelLessonCommand;
using EduNEXT.Application.UseCases.Commands.Lessons.ChangeLesson;
using EduNEXT.Application.UseCases.Commands.Lessons.ChangeLessonsStatus;
using EduNEXT.Application.UseCases.Queries.Lessons.GetAllLessonsByFilter;
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
                return Ok(result.Value);
            }
            
            return BadRequest(result.Error);
        }

        [HttpPost]
        [Route("reschedule/")]
        public async Task<IActionResult> RescheduleLessonTime([FromBody] ChangeLessonCommand command)
        {
            var result = await mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            
            return BadRequest(result.Error);
        }

        [HttpDelete]
        [Route("cancel/")]
        public async Task<IActionResult> CancelLesson([FromBody] CancelLessonCommand command)
        {
            var result = await mediator.Send(command);

            if (result.IsSuccess)
            {
                return NoContent();
            }
            
            return BadRequest(result.Error);
        }

        [HttpGet]
        [Route("all/")]
        public async Task<IActionResult> GetAllLessons(string criteriaName)
        {
            return Ok(await mediator.Send(new GetAllLessonsByFilterQuery()
            {
                CriteriaName = criteriaName
            }));
        }
    }
}
