using EduNEXT.Application.UseCases.Queries.Lessons.GetAnalyticsData;
using EduNEXT.Application.UseCases.Queries.Salary.GetAnalyticsData;
using EduNEXT.Application.UseCases.Queries.Students.GetAnalyticsData;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduNEXT.API.Controllers;

[Authorize]
[Route("api/analytics")]
[ApiController]
public class AnalyticsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [Route("salary/")]
    public async Task<IActionResult> GetSalaryAnalytics()
    {
        return Ok(await mediator.Send(new GetSalaryAnalyticsQuery()));
    }
    
    [HttpGet]
    [Route("students/")]
    public async Task<IActionResult> GetStudentsAnalytics()
    {
        return Ok(await mediator.Send(new GetStudentAnalyticsQuery()));
    }

    [HttpGet]
    [Route("lessons/")]
    public async Task<IActionResult> GetLessonAnalytics()
    {
        return Ok(await mediator.Send(new GetLessonsAnalyticsQuery()));
    }
    
    
}