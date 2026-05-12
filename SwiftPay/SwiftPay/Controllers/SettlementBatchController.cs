using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using SwiftPay.Services.Interfaces;
using SwiftPay.DTOs.SettlementDTO;
using SwiftPay.Models;

namespace SwiftPay.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Authorize] 
	public class SettlementBatchController : ControllerBase
	{
		private readonly ISettlementBatchService _service;

		public SettlementBatchController(ISettlementBatchService service)
		{
			_service = service;
		}

		/// <summary>
		/// Generate a new settlement batch covering the provided period. The service will aggregate
		/// eligible instructions into a new SettlementBatch entity.
		/// </summary>
		/// <param name="dto">Batch generation parameters (period start/end, corridor, etc.).</param>
		/// <returns>HTTP 201 with the created SettlementBatch or HTTP 400 if validation fails.</returns>
		[HttpPost("generate")]
		[Authorize(Roles = "Treasury, Admin, Ops")] 
		public async Task<ActionResult<SettlementBatch>> GenerateBatch([FromBody] GenerateBatchDto dto)
		{
			// 1. Basic Input Validation
			if (dto.PeriodStart >= dto.PeriodEnd)
			{
				return BadRequest(new { Message = "PeriodStart must be before PeriodEnd." });
			}

			try
			{
				// 2. Attempt to generate the batch
				var batch = await _service.GenerateBatchAsync(dto);
				return CreatedAtAction(nameof(GetById), new { id = batch.BatchID }, batch);
			}
			catch (InvalidOperationException ex)
			{
				// 3. Gracefully handle the case where no transactions exist
				return BadRequest(new { Message = ex.Message });
			}
		}

		/// <summary>
		/// Retrieve a settlement batch by identifier.
		/// </summary>
		/// <param name="id">The batch identifier.</param>
		/// <returns>HTTP 200 with the batch if found; HTTP 404 otherwise.</returns>
		[HttpGet("{id}")]
		[Authorize(Roles = "Treasury, Admin, Compliance, Ops")] 
		public async Task<ActionResult<SettlementBatch>> GetById(int id)
		{
			var batch = await _service.GetByIdAsync(id);
			if (batch == null) return NotFound();
			return Ok(batch);
		}

		/// <summary>
		/// Update the status of an existing settlement batch. Useful for marking batches as
		/// Reconciled, Open, or other lifecycle states.
		/// </summary>
		/// <param name="id">The batch identifier (must match dto.BatchID).</param>
		/// <param name="dto">Status update payload.</param>
		/// <returns>HTTP 200 with the updated batch; HTTP 404 if not found.</returns>
		[HttpPatch("{id}/status")]
		[Authorize(Roles = "Treasury, Admin")] 
		public async Task<ActionResult<SettlementBatch>> UpdateStatus(int id, [FromBody] UpdateBatchStatusDto dto)
		{
			if (id != dto.BatchID) return BadRequest(new { Message = "ID mismatch between URL and payload." });

			try
			{
				var batch = await _service.UpdateStatusAsync(dto);
				return Ok(batch);
			}
			catch (KeyNotFoundException ex)
			{
				return NotFound(new { Message = ex.Message });
			}
		}
	}
}