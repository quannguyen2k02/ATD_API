using Application.DTOs.RequestDTOs.LED;
using Application.IServices.LED;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ATD_API.Controllers.LED
{
    [Route("api/[controller]")]
    [ApiController]
    public class LedResultController : ControllerBase
    {
        private readonly ILedResultService _ledResultService;
        public LedResultController(ILedResultService ledResultService)
        {
            _ledResultService = ledResultService;
        }
        [HttpPost]
        public async Task<IActionResult> AddBatchLedResultAsync([FromBody] List<LedResultRequest> batch)
        {
            try
            {
                if (batch == null || batch.Count == 0)
                    return BadRequest("No data received.");
                var (inserted, skipped) = await _ledResultService.AddBatchLedResultAsync(batch);
                return Ok(new { Inserted = inserted, Skipped = skipped });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Internal server error" });

            }


        }
    }
}
