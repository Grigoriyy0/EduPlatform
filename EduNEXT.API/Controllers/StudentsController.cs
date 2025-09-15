using EduNEXT.Application.Dtos;
using EduNEXT.Application.UseCases.Commands.Student.AddStudentCommand;
using EduNEXT.Application.UseCases.Commands.Student.AddStudentPayment;
using EduNEXT.Application.UseCases.Commands.Student.DeleteStudentCommand;
using EduNEXT.Application.UseCases.Commands.Student.UpdateStudentCommand;
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

        [HttpPost]
        [Route("add-payment/")]
        public async Task<IActionResult> AddStudentPayment(AddPaymentDto dto)
        {
            var cmd = new AddStudentPaymentCommand
            {
                dto = dto
            };
            
            var result = await mediator.Send(cmd);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            
            return Created();
        }

        [HttpPut]
        [Route("update/")]
        public async Task<IActionResult> UpdateStudent(UpdateStudentDto dto)
        {
            var cmd = new UpdateStudentCommand
            {
                dto = dto
            };
            
            var result = await mediator.Send(cmd);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }
    }
}