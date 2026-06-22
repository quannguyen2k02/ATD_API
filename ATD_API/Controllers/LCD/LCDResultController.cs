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

        public LCDResultController(ILCDResultService lcdResultService)
        {
            _lcdResultService = lcdResultService;
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

    }
}
