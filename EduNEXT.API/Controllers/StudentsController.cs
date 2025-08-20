using EduNEXT.Application.UseCases.Commands.Student.AddStudentCommand;
using EduNEXT.Application.UseCases.Commands.Student.DeleteStudentCommand;
using EduNEXT.Application.UseCases.Queries.Students.GetAllStudents;
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

            if (result.IsSuccess)
            {
                return Ok(result.Value);
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