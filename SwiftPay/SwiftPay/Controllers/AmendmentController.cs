using Microsoft.AspNetCore.Mvc;
using SwiftPay.Services.Interfaces;
using SwiftPay.DTOs.AmendmentDTO;

namespace SwiftPay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmendmentController : ControllerBase
    {
        private readonly IAmendmentService _amendmentService;

        public AmendmentController(IAmendmentService amendmentService)
        {
            _amendmentService = amendmentService;
        }

        [HttpPost]
        public async Task<IActionResult> RequestAmendment([FromBody] CreateAmendmentDto dto)
        {
            try
            {
                var result = await _amendmentService.CreateAsync(dto);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while requesting amendment.", error = ex.Message });
            }
        }
    }
}