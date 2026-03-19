using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SwiftPay.Services.Interfaces;
using SwiftPay.DTOs.RemittanceDTO;
namespace SwiftPay.Controllers
{
	[ApiController]
	[Route("api/remittances")]
	public class RemittancesController : ControllerBase
	{
		private readonly IRemittanceService _remittanceService;

		public RemittancesController(IRemittanceService remittanceService)
		{
			_remittanceService = remittanceService;
		}

		/// <summary>
		/// Update a remittance (replace fields).
		/// </summary>
		[HttpPut("{remitId}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> Update(string remitId, [FromBody] CreateRemittanceDto dto)
		{
			try
			{
				await _remittanceService.UpdateAsync(remitId, dto);
				return NoContent();
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Failed to update remittance.", error = ex.Message });
			}
		}

		/// <summary>
		/// Delete (soft) a remittance.
		/// </summary>
		[HttpDelete("{remitId}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> Delete(string remitId)
		{
			try
			{
				var deleted = await _remittanceService.DeleteAsync(remitId);
				if (!deleted) return NotFound(new { message = "Remittance not found or could not be deleted." });
				return NoContent();
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Failed to delete remittance.", error = ex.Message });
			}
		}

		/// <summary>
		/// Update a validation record for a remittance.
		/// </summary>
		[HttpPut("{remitId}/validations/{validationId}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> UpdateValidation(string remitId, Guid validationId, [FromBody] RemitValidationDto dto)
		{
			try
			{
				if (validationId != dto.ValidationId || !string.Equals(remitId, dto.RemitId, StringComparison.OrdinalIgnoreCase))
					return BadRequest(new { message = "RemitId or ValidationId mismatch." });

				await _remittanceService.UpdateValidationAsync(dto);
				return NoContent();
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Failed to update validation.", error = ex.Message });
			}
		}

		/// <summary>
		/// Delete a validation record for a remittance.
		/// </summary>
		[HttpDelete("{remitId}/validations/{validationId}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> DeleteValidation(string remitId, Guid validationId)
		{
			try
			{
				// optional: ensure validation belongs to remitId
				await _remittanceService.DeleteValidationAsync(validationId);
				return NoContent();
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Failed to delete validation.", error = ex.Message });
			}
		}

		/// <summary>
		/// Creates a new remittance request in Draft state.
		/// </summary>
		[HttpPost]
		[ProducesResponseType(typeof(CreateRemittanceResponseDto), StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> Create([FromBody] CreateRemittanceDto dto)
		{
			try
			{
				var createdRemittance = await _remittanceService.CreateAsync(dto);

				return CreatedAtAction(
					nameof(GetById),
					new { remitId = createdRemittance.RemitId },
					createdRemittance
				);
			}
			catch (Exception ex)
			{
				// Single centralized error response as per team guidance
				return StatusCode(
					StatusCodes.Status500InternalServerError,
					new
					{
						message = "Failed to create remittance.",
						error = ex.Message
					}
				);
			}
		}

		/// <summary>
		/// Retrieves remittance details by ID.
		/// </summary>
        [HttpGet("{remitId}")]
		[ProducesResponseType(typeof(CreateRemittanceResponseDto), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(string remitId)
		{
			try
			{
                var result = await _remittanceService.GetByIdAsync(remitId);

				if (result == null)
					return NotFound(new { message = "Remittance not found." });

				return Ok(result);
			}
			catch (Exception ex)
			{
				return StatusCode(
					StatusCodes.Status500InternalServerError,
					new
					{
						message = "Failed to retrieve remittance.",
						error = ex.Message
					}
				);
			}
		}


		/// <summary>
		/// Runs validation rules for a remittance.
		/// </summary>
		[HttpPost("{remitId}/validate")]
		[ProducesResponseType(typeof(ValidateRemittanceResponseDto), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> Validate(string remitId)
		{
			try
			{
				var response = await _remittanceService.ValidateAsync(remitId);

				// If validation failed, service should return status Draft
				if (response.Status != "Validated")
				{
					return UnprocessableEntity(response);
				}

				return Ok(response);
			}
			catch (Exception ex)
			{
				return StatusCode(
					StatusCodes.Status500InternalServerError,
					new
					{
						message = "Failed to validate remittance.",
						error = ex.Message
					}
				);
			}
		}

		/// <summary>
		/// Retrieves validation results for a remittance.
		/// </summary>
		[HttpGet("{remitId}/validations")]
		[ProducesResponseType(typeof(List<RemitValidationDto>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetValidations(string remitId)
		{
			try
			{
				var validations = await _remittanceService.GetValidationsAsync(remitId);

				if (validations == null || !validations.Any())
					return NotFound(new { message = "No validation records found." });

				return Ok(validations);
			}
			catch (Exception ex)
			{
				return StatusCode(
					StatusCodes.Status500InternalServerError,
					new
					{
						message = "Failed to retrieve validation results.",
						error = ex.Message
					}
				);
			}
		}

	}
}