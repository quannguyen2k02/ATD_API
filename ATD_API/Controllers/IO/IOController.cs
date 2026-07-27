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
        private readonly IIOPressureService _ioPressureService;
        public IOController(IIOModelService ioModelService, IIOConfigManagementService ioConfigManagementService, IIOMotionPointManagementService iOMotionPointManagementService , IIOOffsetManagementService ioOffsetManagementService, IIOPressureService ioPressureService)
        {
            _ioModelService = ioModelService;
            _ioConfigManagementService = ioConfigManagementService;
            _ioMotionPointManagementService = iOMotionPointManagementService;
            _ioOffsetManagementService = ioOffsetManagementService;
            _ioPressureService = ioPressureService;
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
        public async Task<IActionResult> AddMotion(IOMotionPointsRequest motionPointsRequest)
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

        [HttpPost("Add-Pressure")]
        public async Task<IActionResult> AddPressure(IOPressureRequest pressures)
        {
            try
            {
                var result = await _ioPressureService.AddNewPressures(pressures);
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
        [HttpGet("MotionPoint")]
        public async Task<IActionResult> GetMotionPointsByodelId(int modelId,[FromQuery] int? lastId = null, [FromQuery] int pageSize = 20)
        {
            try
            {
                var result = await _ioMotionPointManagementService.GetMotionPoints(modelId, lastId, pageSize);
                return Ok(result);
            }
            catch(NotFoundException ex)
            {
                 return NotFound(new { ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Internal server error" });
            }

        }
        [HttpGet("Offset")]
        public async Task<IActionResult> GetOffsetBymodelId(int modelId, [FromQuery] int? lastId = null, [FromQuery] int pageSize = 20)
        {
            try
            {
                var result = await _ioOffsetManagementService.GetOffsets(modelId, lastId, pageSize);
                return Ok(result);
            }
            catch(NotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Internal server error" });
            }

        }
        [HttpGet("IO_Config")]
        public async Task<IActionResult> GetIOConfigByModelId(int modelId, [FromQuery] int? lastId = null, [FromQuery] int pageSize = 20)
        {
            try
            {
                var result = await _ioConfigManagementService.GetIOConfig(modelId, lastId, pageSize);
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
