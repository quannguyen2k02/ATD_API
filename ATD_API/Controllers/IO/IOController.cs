using Application.DTOs.IOStatitics;
using Application.DTOs.LedStatistics;
using Application.DTOs.RequestDTOs.IO;
using Application.DTOs.RequestDTOs.LCD;
using Application.DTOs.RequestDTOs.LED;
using Application.IServices.IO;
using ATD_API.Hubs;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

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
        private readonly IIOService _ioService;
        private readonly IIOPlugService _ioPlugService;
        private readonly IIOReplacementRecordService _svc;
        private readonly IIOResultService _ioResultService;
        private readonly IIOStatusService _ioStatusService;

        public IOController(IIOModelService ioModelService, IIOConfigManagementService ioConfigManagementService, IIOMotionPointManagementService iOMotionPointManagementService, IIOOffsetManagementService ioOffsetManagementService, IIOPressureService ioPressureService, IIOService iOService, IIOPlugService iOPlugService, IIOReplacementRecordService svc, IIOResultService ioResultService, IIOStatusService ioStatusService)
        {
            _ioModelService = ioModelService;
            _ioConfigManagementService = ioConfigManagementService;
            _ioMotionPointManagementService = iOMotionPointManagementService;
            _ioOffsetManagementService = ioOffsetManagementService;
            _ioPressureService = ioPressureService;
            _ioService = iOService;
            _ioPlugService = iOPlugService;
            _svc = svc;
            _ioResultService = ioResultService;
            _ioStatusService = ioStatusService;
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
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
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
        public async Task<IActionResult> GetMotionPointsByodelId(int modelId, [FromQuery] int? lastId = null, [FromQuery] int pageSize = 20)
        {
            try
            {
                var result = await _ioMotionPointManagementService.GetMotionPoints(modelId, lastId, pageSize);
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
        [HttpGet("Offset")]
        public async Task<IActionResult> GetOffsetBymodelId(int modelId, [FromQuery] int? lastId = null, [FromQuery] int pageSize = 20)
        {
            try
            {
                var result = await _ioOffsetManagementService.GetOffsets(modelId, lastId, pageSize);
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
        [HttpGet("GetIOByLineId")]
        public async Task<IActionResult> GetIOsByLineId(int lineId)
        {
            try
            {
                var result = await _ioService.GetIOsByLineId(lineId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Internal server error" });
            }
        }
        [HttpGet("GetModelsByIOId")]
        public async Task<IActionResult> GetModelsByIOId(int ioId)
        {
            try
            {
                var result = await _ioModelService.GetModelsByIOId(ioId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Internal server error" });
            }
        }
        [HttpPut("Update-Port")]
        public async Task<IActionResult> UpdatePort(PortRequest request)
        {
            try
            {
                var result = await _ioPlugService.UpdatePort(request);
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
        public async Task<ActionResult<ImportResult>> Import(
        [FromBody] List<ReplacementRecordRequest> rows, CancellationToken ct)
        {
            if (rows is null || rows.Count == 0)
                return BadRequest("Danh sách rỗng");

            return Ok(await _svc.ImportAsync(rows, ct));
        }
        [HttpPost("batch")]
        public async Task<IActionResult> AddBatch([FromBody] List<IOResultRequest> batch)
        {
            if (batch == null || batch.Count == 0)
                return BadRequest("No data received.");

            try
            {
                var (inserted, skipped) = await _ioResultService.AddBatchAsync(batch);
                return Ok(new { Inserted = inserted, Skipped = skipped });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error."+ex.Message);
            }

        }

        [HttpGet("overview")]
        [Authorize]

        public async Task<IActionResult> Overview([FromQuery] IOResultStatisticFilter filter)
          => Ok(await _ioResultService.GetOverviewAsync(filter));

        [HttpGet("by-model")]
        [Authorize]

        public async Task<IActionResult> ByModel([FromQuery] IOResultStatisticFilter filter)
            => Ok(await _ioResultService.GetByModelAsync(filter));
        [HttpGet("by-model-group")]
        [Authorize]

        public async Task<IActionResult> ByModelGroup([FromQuery] IOResultStatisticFilter filter)
            => Ok(await _ioResultService.GetByModelGroupAsync(filter));

        [HttpGet("by-line")]
        [Authorize]

        public async Task<IActionResult> ByLine([FromQuery] IOResultStatisticFilter filter)
            => Ok(await _ioResultService.GetByLineAsync(filter));

        [HttpGet("by-device")]
        [Authorize]

        public async Task<IActionResult> ByDevice([FromQuery] IOResultStatisticFilter filter)
            => Ok(await _ioResultService.GetByDeviceAsync(filter));

        [HttpGet("by-day")]
        [Authorize]

        public async Task<IActionResult> ByDay([FromQuery] IOResultStatisticFilter filter)
            => Ok(await _ioResultService.GetByDayAsync(filter));
        [HttpGet("by-hour")]
        [Authorize]

        public async Task<IActionResult> ByHour([FromQuery] IOResultStatisticFilter filter)
            => Ok(await _ioResultService.GetByHourAsync(filter));
        [HttpGet("by-sn")]
        [Authorize]

        public async Task<IActionResult> GetBySN([FromQuery] string sn)
        {
            if (string.IsNullOrWhiteSpace(sn))
                return BadRequest("SN không được rỗng.");

            var result = await _ioResultService.GetBySNAsync(sn);
            return Ok(result);
        }
        [HttpGet("io-log")]
        [Authorize]

        public async Task<IActionResult> GetLedLog([FromQuery] IOResultStatisticFilter filter, int? lastId = null, int pageSize = 20)
        {
            var result = await _ioResultService.GetIOResultLogPagedAsync(filter, lastId, pageSize);
            return Ok(result);
        }
    }
}