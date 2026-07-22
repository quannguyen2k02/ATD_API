using Application.DTOs.RequestDTOs.IO;
using Application.DTOs.RequestDTOs.LED;
using Application.IServices.IO;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ATD_API.Controllers.IO
{
    [Route("api/[controller]")]
    [ApiController]
    public class IOController : ControllerBase
    {
        private readonly IIOModelService _ioModelService;
        private readonly IIOConfigManagementService _ioConfigManagementService;
        private readonly IIOMotionPointManagementService _ioMotionPointManagementService;
        private readonly IIOOffsetManagementService _ioOffsetManagementService;
        public IOController(IIOModelService ioModelService, IIOConfigManagementService ioConfigManagementService, IIOMotionPointManagementService iOMotionPointManagementService , IIOOffsetManagementService ioOffsetManagementService)
        {
            _ioModelService = ioModelService;
            _ioConfigManagementService = ioConfigManagementService;
            _ioMotionPointManagementService = iOMotionPointManagementService;
            _ioOffsetManagementService = ioOffsetManagementService;
        }
        [HttpPost("Add-New-Model")]
        public async Task<IActionResult> AddNewIOModel(IOModelRequest IOModel)
        {
            var result = await _ioModelService.AddNewModel(IOModel);
            return Ok(result);
        }
        [HttpPost("Add-Config")]
        public async Task<IActionResult> AddConfig(IOConfigRequest configRequest)
        {
            try
            {
                var result = await _ioConfigManagementService.AddNewConfig(configRequest);
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
        [HttpPost("Add-MotionPoints")]
        public async Task<IActionResult> AddConfig(IOMotionPointsRequest motionPointsRequest)
        {
            try
            {
                var result = await _ioMotionPointManagementService.AddNewIOMotionPoints(motionPointsRequest);
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
        [HttpPost("Add-Offset")]
        public async Task<IActionResult> AddOffset(IOOffsetsRequest offsets)
        {
            try
            {
                var result = await _ioOffsetManagementService.AddNewOffset(offsets);
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
