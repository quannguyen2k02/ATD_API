using Application.DTOs.RequestDTOs.LCD;
using Application.IRepositories.LCD;
using Application.IServices.LCD;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
namespace ATD_API.Controllers.LCD
{
    [Route("api/[controller]")]
    [ApiController]
    public class LCDResultController : ControllerBase
    {
        private readonly ILCDResultService _lcdResultService;
        private readonly ILCDResultDemoService _lcdResultDemoService;
        public LCDResultController(ILCDResultService lcdResultService, ILCDResultDemoService lCDResultDemoService)
        {
            _lcdResultService = lcdResultService;
            _lcdResultDemoService = lCDResultDemoService;
        }
        [HttpPost("batch")]
        public async Task<IActionResult> AddBatch([FromBody] List<RequestLCDResult> batch)
        {
            if (batch == null || batch.Count == 0)
                return BadRequest("No data received.");

            try
            {
                var (inserted, skipped) = await _lcdResultService.AddBatchAsync(batch);
                return Ok(new { Inserted = inserted, Skipped = skipped });
            }
            catch(NotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error.");
            }
            
        }
        [HttpGet]
        public async Task<IActionResult> GetLCDResultsAsync(DateTime? fromDate, DateTime? toDate, string? modelName, int deviceId, int? lastId = null, int pageSize = 20)
        {
            try
            {
                var result = await _lcdResultDemoService.GetLCDResultDemoAsync(fromDate, toDate, modelName, deviceId, lastId, pageSize);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}
