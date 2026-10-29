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
    }
}
