using Microsoft.AspNetCore.Mvc;

namespace EduNEXT.API.Controllers
{
    [Route("api/salary")]
    [ApiController]
    public class SalaryController : ControllerBase
    {
        [HttpGet]
        [Route("expected/")]
        public async Task<IActionResult> GetExpectedSalary()
        {
            return Ok();
        }
    }
}
