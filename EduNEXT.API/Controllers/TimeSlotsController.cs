using EduNEXT.Application.Services;
using EduNEXT.Core.Domain.Utils;
using Microsoft.AspNetCore.Mvc;

namespace EduNEXT.API.Controllers
{
    [Route("api/schedule")]
    [ApiController]
    public class TimeSlotsController : ControllerBase
    {
        private readonly TimeTableService _service;

        public TimeSlotsController(TimeTableService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("/add-slots")]
        public async Task<IActionResult> AddSlots([FromBody] TimeSlotRange[] slots)
        {
            var result = await _service.CreateTimeSlots(slots);
            
            return Ok(result);
        }
    }
}
