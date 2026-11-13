using Application.DTOs.LedStatistics;
using Application.DTOs.RequestDTOs.LED;
using Application.IServices.LED;
using Infrastructure.Exceptions;
using Infrastructure.Services.IO;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> AddNewLedResult(LedResultRequest request)
        {
            try
            {
                var result = await _ledResultService.AddnewLedResult(request);
                return Ok(result);

            }
            catch (NotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Internal server error" });
            }
        }

        [HttpGet("overview")]
        [Authorize]

        public async Task<IActionResult> Overview([FromQuery] LedResultStatisticFilter filter)
          => Ok(await _ledResultService.GetOverviewAsync(filter));

        [HttpGet("by-model")]
        [Authorize]

        public async Task<IActionResult> ByModel([FromQuery] LedResultStatisticFilter filter)
            => Ok(await _ledResultService.GetByModelAsync(filter));
        [HttpGet("by-model-group")]
        [Authorize]

        public async Task<IActionResult> ByModelGroup([FromQuery] LedResultStatisticFilter filter)
            => Ok(await _ledResultService.GetByModelGroupAsync(filter));

        [HttpGet("by-line")]
        [Authorize]

        public async Task<IActionResult> ByLine([FromQuery] LedResultStatisticFilter filter)
            => Ok(await _ledResultService.GetByLineAsync(filter));

        [HttpGet("by-device")]
        [Authorize]

        public async Task<IActionResult> ByDevice([FromQuery] LedResultStatisticFilter filter)
            => Ok(await _ledResultService.GetByDeviceAsync(filter));

        [HttpGet("by-day")]
        [Authorize]

        public async Task<IActionResult> ByDay([FromQuery] LedResultStatisticFilter filter)
            => Ok(await _ledResultService.GetByDayAsync(filter));
        [HttpGet("by-hour")]
        [Authorize]

        public async Task<IActionResult> ByHour([FromQuery] LedResultStatisticFilter filter)
            => Ok(await _ledResultService.GetByHourAsync(filter));
        [HttpGet("by-sn")]
        [Authorize]

        public async Task<IActionResult> GetBySN([FromQuery] string sn)
        {
            if (string.IsNullOrWhiteSpace(sn))
                return BadRequest("SN không được rỗng.");

            var result = await _ledResultService.GetBySNAsync(sn);
            return Ok(result);
        }
        [HttpGet("led-log")]
        [Authorize]

        public async Task<IActionResult> GetLedLog([FromQuery] LedResultStatisticFilter filter, int? lastId = null, int pageSize = 20)
        {
            var result = await _ledResultService.GetLedLogPagedAsync(filter, lastId, pageSize);
            return Ok(result);
        }

        [HttpGet("fail-statistics")]
        [Authorize]

        public async Task<IActionResult> GetFailItemStatistics([FromQuery] LedResultStatisticFilter filter)
            => Ok(await _ledResultService.GetFailItemStatisticsAsync(filter));
    }
}
