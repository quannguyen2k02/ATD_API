using Application.DTOs.RequestDTOs.LED;
using Application.IServices.LED;
using ATD_API.Hubs;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ATD_API.Controllers.LED
{
    [Route("api/[controller]")]
    [ApiController]
    public class LedStatusController : ControllerBase
    {
        private readonly ILogger<LedStatusController> _logger;
        private readonly ILedStatusService _ledStatusService;
        private readonly IHubContext<NotificationHub> _hubContext;
        public LedStatusController(ILogger<LedStatusController> logger, ILedStatusService ledStatusService, IHubContext<NotificationHub> hubContext)
        {
            _logger = logger;
            _ledStatusService = ledStatusService;
            _hubContext = hubContext;
        }
        [HttpPost]
        public async Task<IActionResult> AddNewStatus(LedDeviceStatusRequest ledDeviceStatusRequest)
        {
            try
            {
                _logger.LogInformation("Nhận yêu cầu cập nhật trạng thái LED");
                var result = await _ledStatusService.AddNewStatus(ledDeviceStatusRequest);
                await _hubContext.Clients.All.SendAsync("ReceiveNotification", ledDeviceStatusRequest);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex, "LedId is not exist");
                return NotFound(new { ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Internal server error" });
            }
        }
    }
}
