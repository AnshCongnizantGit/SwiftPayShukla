using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SwiftPay.DTOs.FXQuoteDTO;
using SwiftPay.Services.Interfaces;

namespace SwiftPay.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,Treasury")]
    public class FeeRulesController : ControllerBase
    {
        private readonly IFeeRuleService _service;

        public FeeRulesController(IFeeRuleService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateFeeRule([FromBody] CreateFeeRuleRequestDto request)
        {
            var response = await _service.CreateFeeRuleAsync(request);
            return Ok(new { message = "Fee rule created successfully.", data = response });
        }

        [HttpGet]
        public async Task<IActionResult> GetActiveFeeRules()
        {
            var response = await _service.GetActiveFeeRulesAsync();
            return Ok(new { message = "Fee rules retrieved.", data = response });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFeeRule(string id, [FromBody] UpdateFeeRuleRequestDto request)
        {
            var response = await _service.UpdateFeeRuleAsync(id, request);
            if (response == null)
                return NotFound(new { message = $"Fee rule with ID {id} not found." });

            return Ok(new { message = "Fee rule updated successfully.", data = response });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeeRule(string id)
        {
            var isDeleted = await _service.DeleteFeeRuleAsync(id);
            if (!isDeleted)
                return NotFound(new { message = $"Fee rule with ID {id} not found." });

            return Ok(new { message = "Fee rule deleted successfully." });
        }
    }
}
