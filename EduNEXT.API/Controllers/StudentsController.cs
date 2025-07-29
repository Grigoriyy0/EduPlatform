using EduNEXT.Application.Commands.Student.AddStudentCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EduNEXT.API.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [Route("add/")]
        public async Task<IActionResult> AddStudent([FromBody] AddStudentCommand command)
        {
            var result = await mediator.Send(command);
            
            var value = result.TryGetValue(out var student);

            if (value)
            {
                return Ok(student);
            }
            
            return BadRequest(result.Error);
        }
    }
}