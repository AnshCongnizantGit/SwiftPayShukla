using Microsoft.AspNetCore.Authorization; // Added for [Authorize]
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims; // Added to extract token data
using System.Threading.Tasks;
using SwiftPay.DTOs.FXQuoteDTO;
using SwiftPay.Services.Interfaces;

namespace SwiftPay.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // 1. Locks the controller so ONLY logged-in users can access it
    public class RateLocksController : ControllerBase
    {
        private readonly IRateLockService _service;

        public RateLocksController(IRateLockService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRateLock([FromBody] CreateRateLockRequestDto request)
        {
            // 2. THE MAGIC: Extract the secure User ID directly from the JWT Token!
            var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(customerId))
            {
                return Unauthorized("Invalid or missing user identity in token.");
            }

            // 3. OVERWRITE whatever the frontend sent. 
            // Even if a hacker tries to send someone else's ID, we force it to be THEIR ID.
            request.CustomerID = customerId;

            var response = await _service.LockRateAsync(request);
            return Ok(response);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRateLock(string id)
        {
            var response = await _service.GetRateLockAsync(id);
            if (response == null) return NotFound($"Rate Lock with ID {id} not found.");
            
            // 4. PREVENT SNOOPING: Check if the logged-in user actually owns this lock!
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (response.CustomerID != currentUserId)
            {
                return Forbid(); // 403 Forbidden: "You are not allowed to view other people's data!"
            }

            return Ok(response);
        }
    }
}