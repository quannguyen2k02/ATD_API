using Application.DTOs.RequestDTOs.LCD;
using Application.DTOs.RequestDTOs.LED;
using Application.IServices.LCD;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ATD_API.Controllers.LCD
{
    [Route("api/[controller]")]
    [ApiController]
    public class LCDModelController : ControllerBase
    {
        private readonly ILCDModelService _lcdModelService;
        public LCDModelController(ILCDModelService lcdModelService)
        {
            _lcdModelService = lcdModelService;
        }
        [HttpGet("Get-LCDModel-By-DeviceId")]
        public async Task<IActionResult> GetLCDModelsByDeviceIdAsync(int deviceId, int? lastId = null, int pageSize = 20)
        {

            try
            {
                var result = await _lcdModelService.GetLCDModelsByDeviceIdAsync(deviceId, lastId, pageSize);
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
        [HttpGet("Get-LCDModel-By-Version")]
        public async Task<IActionResult> GetLCDModelsByModelNameAsync(string modelName, int deviceId, int? lastId = null, int pageSize = 20)
        {
            try
            {
                var result = await _lcdModelService.GetLCDModelsByModelNameAsync(modelName, deviceId, lastId, pageSize);
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
        public async Task<IActionResult> AddNewLCDModelAsync(RequestLCDModel model)
        {
            try
            {
                var result = await _lcdModelService.AddNewLCDModelAsync(model);
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
