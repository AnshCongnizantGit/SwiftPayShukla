using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using SwiftPay.Services.Interfaces;
using SwiftPay.DTOs.RemittanceDTO;
using SwiftPay.Domain.Remittance.Entities;

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
        [ProducesResponseType(typeof(RemittanceRequest), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateRemittanceDto dto)
        {
            try
            {
                // Basic null check
                if (dto == null)
                    return BadRequest(new { message = "Request body is required." });

                // Model validation
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var created = await _service.CreateAsync(dto);
                return Ok(new { message = "Remittance created successfully.", data = created });
            }
            catch (ArgumentNullException ex)
            {
                // Bad input
                return BadRequest(new { message = "Invalid input.", error = ex.Message });
            }
            catch (ValidationException ex)
            {
                // Data annotations validation failure
                return BadRequest(new { message = "Validation failed.", error = ex.Message });
            }
            catch (DbUpdateException ex)
            {
                // Database update error
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "A database error occurred while creating the remittance.", error = ex.Message });
            }
            catch (Exception ex)
            {
                // Catch-all for unexpected errors
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while creating the remittance.", error = ex.Message });
            }
        }
    }
}
