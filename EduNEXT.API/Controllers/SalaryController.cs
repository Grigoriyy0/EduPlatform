using EduNEXT.Application.UseCases.Queries.Salary.GetActual;
using EduNEXT.Application.UseCases.Queries.Salary.GetExpected;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EduNEXT.API.Controllers
{
    [Route("api/salary")]
    [ApiController]
    public class SalaryController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [Route("expected/")]
        public async Task<IActionResult> GetExpectedSalary()
        {
            var query = new GetExpectedSalaryQuery();
            
            return Ok(await mediator.Send(query));
        }

        [HttpGet]
        [Route("actual/")]
        public async Task<IActionResult> GetActualSalary()
        {
            var query = new GetActualSalaryQuery();
            
            return Ok(await mediator.Send(query));
        }
    }
}
