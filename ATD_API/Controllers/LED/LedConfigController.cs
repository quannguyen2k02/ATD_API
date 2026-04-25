using Application.DTOs.RequestDTOs.LED;
using Application.IServices.LED;
using Infrastructure.Exceptions;
using Infrastructure.Services.LED;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ATD_API.Controllers.LED
{
    [Route("api/[controller]")]
    [ApiController]
    public class LedConfigController : ControllerBase
    {
        private readonly ILedConfigService _ledConfigService;
        public LedConfigController(ILedConfigService ledConfigService)
        {
            _ledConfigService = ledConfigService;
        }
        [HttpPost]
        public async Task<IActionResult> AddLedConfigAsync(LedConfigDTO ledConfigDTO)
        {
            try
            {
                var result = await _ledConfigService.AddLedConfigAsync(ledConfigDTO);
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
        [HttpGet("by-device")]
        public async Task<IActionResult> GetLedModelAsync([FromQuery] int deviceId, [FromQuery] int? lastId = null, [FromQuery] int pageSize = 20)
        {
            try
            {
                var result = await _ledConfigService.GetLedConfigByDeviceIdAsync(deviceId, lastId, pageSize);
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
    }
}
