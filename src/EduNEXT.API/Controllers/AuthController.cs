using EduNEXT.Application.UseCases.Commands.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EduNEXT.API.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [Route("signin/")]
    public async Task<IActionResult> Auth([FromBody] SignInCommand request)
    {
        var res = await mediator.Send(request);

        if (res.IsFailure)
        {
            return Unauthorized(res.Error);
        }

        return Ok(new
        {
            Token = res.Value
        });
    }
}