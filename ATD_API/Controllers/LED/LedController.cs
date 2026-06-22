using Application.IServices.LED;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ATD_API.Controllers.LED
{
    [Route("api/[controller]")]
    [ApiController]
    public class LedController : ControllerBase
    {
        private readonly ILedService _ledService;
        public LedController(ILedService ledService)
        {
            _ledService = ledService;
        }
        [HttpGet]
        public async Task<IActionResult> GetLedDeviceByLineId(int lineId)
        {
            var result = await _ledService.GetLedsByLineId(lineId);
            return Ok(result);
        }
    }
}
