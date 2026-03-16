using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SwiftPay.Services.Interfaces;
using SwiftPay.DTOs.RemittanceDTO;

namespace SwiftPay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RemittanceController : ControllerBase
    {
        private readonly IRemittanceService _service;

        public RemittanceController(IRemittanceService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRemittanceDto dto)
        {
            if (dto == null)
                return BadRequest("Request body is required.");

            var created = await _service.CreateAsync(dto);
            return Ok(created);
        }
    }
}
