using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SwiftPay.DTOs.FXQuoteDTO;
using SwiftPay.Services.Interfaces;

namespace SwiftPay.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // 1. BASE REQUIREMENT: Anyone accessing this controller must be logged in.
    public class FXQuotesController : ControllerBase
    {
        private readonly IFXQuoteService _service;

        public FXQuotesController(IFXQuoteService service)
        {
            _service = service;
        }

        [HttpPost]
        // 2. CREATE RULE: Strictly locked to Customers only. Admins will get a 403 Forbidden here.
        [Authorize(Roles = "Customer")] 
        public async Task<IActionResult> CreateQuote([FromBody] CreateQuoteRequestDto request)
        {
            var response = await _service.GenerateQuoteAsync(request);
            return Ok(response); 
        }

        [HttpGet("{id}")]
        // 3. READ RULE: Both Customers and Admins can view the quote. (Note: No spaces in the string!)
        [Authorize(Roles = "Customer,Admin")] 
        public async Task<IActionResult> GetQuote(string id)
        {
            var response = await _service.GetQuoteAsync(id);
            if (response == null) return NotFound($"Quote with ID {id} not found.");
            
            return Ok(response);
        }
    }
}