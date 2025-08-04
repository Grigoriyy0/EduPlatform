using EduNEXT.Application.Commands.Student.AddStudentCommand;
using EduNEXT.Application.Commands.Student.DeleteStudentCommand;
using EduNEXT.Application.Queries.Students.GetAllStudents;
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

        [HttpDelete]
        [Route("delete/{id:guid}")]
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            var result = await mediator.Send(new DeleteStudentCommand()
            {
                Id = id
            });

            if (result.IsSuccess)
            {
                return NoContent();
            }
            
            return BadRequest(result.Error);
        }

        [HttpGet]
        [Route("all/")]
        public async Task<IActionResult> GetAllStudents()
        {
            return Ok(await mediator.Send(new GetAllStudentsQuery()));
        }
    }
}