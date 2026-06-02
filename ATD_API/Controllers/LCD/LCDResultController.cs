using Application.DTOs.RequestDTOs.LCD;
using Application.IServices.LCD;
using Microsoft.AspNetCore.Mvc;
namespace ATD_API.Controllers.LCD
{
    [Route("api/[controller]")]
    [ApiController]
    public class LCDResultController : ControllerBase
    {
        private readonly ILCDResultService _lcdResultService;
        public LCDResultController(ILCDResultService lcdResultService)
        {
            _lcdResultService = lcdResultService;
        }
        [HttpPost("batch")]
        public async Task<IActionResult> AddBatch([FromBody] List<RequestLCDResult> batch)
        {
            if (batch == null || batch.Count == 0)
                return BadRequest("No data received.");

            //try
            //{
            //    var (inserted, skipped) = await _lcdResultService.AddBatchAsync(batch);
            //    return Ok(new { Inserted = inserted, Skipped = skipped });
            //}
            //catch (Exception ex)
            //{

            //    return StatusCode(500, "Internal server error.");
            //}
            var (inserted, skipped) = await _lcdResultService.AddBatchAsync(batch);
                return Ok(new { Inserted = inserted, Skipped = skipped });
        }
    }
}
