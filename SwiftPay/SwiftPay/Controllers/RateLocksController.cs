using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims; 
using System.Threading.Tasks;
using SwiftPay.DTOs.FXQuoteDTO;
using SwiftPay.Services.Interfaces;

namespace SwiftPay.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // 1. BASE LEVEL: Must be logged in.
    public class RateLocksController : ControllerBase
    {
        private readonly IRateLockService _service;

        public RateLocksController(IRateLockService service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize(Roles = "Customer")] // 2. CREATE: Strictly Customers only
        public async Task<IActionResult> CreateRateLock([FromBody] CreateRateLockRequestDto request)
        {
            var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(customerId))
            {
                return Unauthorized("Invalid or missing user identity in token.");
            }

            request.CustomerID = customerId;

            var response = await _service.LockRateAsync(request);
            return Ok(response);
        }
        
        [HttpGet("{id}")]
        [Authorize(Roles = "Customer,Admin")] // 3. READ: Customers and Admins allowed
        public async Task<IActionResult> GetRateLock(string id)
        {
            var response = await _service.GetRateLockAsync(id);
            if (response == null) return NotFound($"Rate Lock with ID {id} not found.");
            
            // 4. PREVENT SNOOPING (With Admin Bypass)
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isAdmin = User.IsInRole("Admin"); // Check if the token has the Admin badge

            // If they are NOT an admin, AND the IDs don't match, kick them out.
            if (!isAdmin && response.CustomerID != currentUserId)
            {
                return Forbid(); 
            }

            return Ok(response);
        }
    }
}