using Application.IServices.LED;
using Microsoft.AspNetCore.Mvc;

namespace ATD_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LineController : ControllerBase
    {
        private readonly ILineService _lineService;
        public LineController(ILineService lineService)
        {
            _lineService = lineService;
        }
        [HttpGet("get-all-line")]

        public async Task<IActionResult> GetAllLineAsync()
        {
            var lines =  await _lineService.GetAllLinesAsync();
            return Ok(lines);
        }
    }
}
