using Application.DTOs.RequestDTOs.LCD;
using Application.IServices.LCD;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace ATD_API.Controllers.LCD
{
    [Route("api/[controller]")]
    [ApiController]
    public class LCDConfigController : ControllerBase
    {
        private readonly ILCDConfigService _lcdConfigService;
        public LCDConfigController(ILCDConfigService lcdConfigService)
        {
            _lcdConfigService = lcdConfigService;
        }
        [HttpGet("Get-LCDConfig-By-DeviceId")]
        public async Task<IActionResult> GetLCDConfigsByDeviceIdAsync(int deviceId, int? lastId = null, int pageSize = 20)
        {
            try
            {
                var result = await _lcdConfigService.GetLCDConfigsByDeviceIdAsync(deviceId, lastId, pageSize);
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
        [HttpPost]
        public async Task<IActionResult> AddLCDConfigAsync(RequestLCDConfig lcdConfig)
        {
            try
            {
                var result = await _lcdConfigService.AddLCDConfigAsync(lcdConfig);
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