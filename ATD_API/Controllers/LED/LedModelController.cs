using Application.DTOs.RequestDTOs.LED;
using Application.IServices.LED;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace ATD_API.Controllers.LED
{
    [Route("api/[controller]")]
    [ApiController]
    public class LedModelController : ControllerBase
    {
        private readonly ILedModelService _ledModelService;
        public LedModelController(ILedModelService ledModelService)
        {
            _ledModelService = ledModelService;
        }
        [HttpPost]
        public async Task<IActionResult> AddLedModelAsync(LedModelDTO ledmodelDTO)
        {
            //try
            //{
            //    var result = await _ledModelService.AddLedModelAsync(ledmodelDTO);
            //    return Ok(result);

            //}
            //catch (NotFoundException ex)
            //{
            //    return NotFound(new { ex.Message });
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Internal server error" });
            //}
            var result = await _ledModelService.AddLedModelAsync(ledmodelDTO);
            return Ok(result);
        }
        [HttpGet("by-model-version")]
        public async Task<IActionResult> GetLedModelAsync([FromQuery] string line, [FromQuery] string devicename, [FromQuery] string model, [FromQuery] string kb, [FromQuery] string fp, [FromQuery] int? lastId = null, [FromQuery] int pageSize = 20)
        {
            try
            {
                var result = await _ledModelService.GetLedModelAsync(line, devicename, model, kb, fp, lastId, pageSize);
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
        public async Task<IActionResult> GetLedModelByDevice([FromQuery] string line, [FromQuery] string deviceName, [FromQuery] int? lastId = null, [FromQuery] int pageSize = 20)
        {
            try
            {
                if (string.IsNullOrEmpty(line) || string.IsNullOrEmpty(deviceName))
                {
                    return BadRequest(new { Message = "Line and DeviceName are required." });
                }
                var result = await _ledModelService.GetLedModelsByDevice(line, deviceName, lastId, pageSize);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message });
            }
        }
        [HttpGet("by-id")]
        public async Task<IActionResult> GetLedModelById(int id)
        {
            try
            {
                var result = await _ledModelService.GetLedModelById(id);
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
