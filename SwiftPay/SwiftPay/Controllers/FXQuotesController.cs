using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Security.Claims;
using SwiftPay.DTOs.FXQuoteDTO;
using SwiftPay.Services.Interfaces;

namespace SwiftPay.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FXQuotesController : ControllerBase
    {
        private readonly IFXQuoteService _service;

        public FXQuotesController(IFXQuoteService service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize(Roles = "Customer,Treasury")]
        public async Task<IActionResult> CreateQuote([FromBody] CreateQuoteRequestDto request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { message = "Invalid token: User ID could not be found." });

            request.CustomerID = userId;

            var response = await _service.GenerateQuoteAsync(request);
            return Ok(new { message = "FX quote generated successfully.", data = response });
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Customer,Admin,Treasury")]
        public async Task<IActionResult> GetQuote(string id)
        {
            var response = await _service.GetQuoteAsync(id);
            if (response == null)
                return NotFound(new { message = $"Quote with ID {id} not found." });

            return Ok(new { message = "FX quote retrieved.", data = response });
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Treasury,Ops")]
        public async Task<IActionResult> GetAll()
        {
            var all = await _service.GetAllQuotesAsync();
            return Ok(new { message = "FX quotes retrieved.", data = all });
        }
    }
}
