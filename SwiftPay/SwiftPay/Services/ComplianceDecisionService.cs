using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using SwiftPay.Constants.Enums;
using SwiftPay.Domain.Remittance.Entities;
using SwiftPay.DTOs.ComplianceDTO;
using SwiftPay.DTOs.RefundRefDTO;
using SwiftPay.DTOs.UserCustomerDTO;
using SwiftPay.Repositories.Interfaces;
using SwiftPay.Services.Interfaces;

namespace SwiftPay.Services
{
	public class ComplianceDecisionService : IComplianceDecisionService
	{
		private readonly IComplianceDecisionRepository _repo;
		private readonly IMapper _mapper;
		private readonly IRemittanceService _remittanceService;
		private readonly IRefundRefService _refundService;
		private readonly INotificationAlertService _notificationService;
		private readonly ICustomerService _customerService;

		public ComplianceDecisionService(
			IComplianceDecisionRepository repo,
			IMapper mapper,
			IRemittanceService remittanceService,
			IRefundRefService refundService,
			INotificationAlertService notificationService,
			ICustomerService customerService)
		{
			_repo = repo;
			_mapper = mapper;
			_remittanceService = remittanceService;
			_refundService = refundService;
			_notificationService = notificationService;
			_customerService = customerService;
		}

		/// <summary>
		/// Records a compliance decision and drives the remittance status forward:
		///   Approve  → Validated       (compliance cleared, agent can now approve)
		///   Hold     → ComplianceHold  (blocks agent approval, needs further review)
		///   Reject   → Cancelled       (auto-creates RefundRef, notifies customer)
		/// </summary>
		public async Task<ComplianceDecision> RecordDecisionAsync(CreateComplianceDecisionDto dto)
		{
			// 1. Persist the decision record
			var decision = _mapper.Map<ComplianceDecision>(dto);
			decision.DecisionId = Guid.NewGuid().ToString();
			decision.DecisionDate = DateTime.UtcNow;
			decision.IsDeleted = false;
			var saved = await _repo.CreateAsync(decision);

			// 2. Resolve the remit as int (RemitId is stored as string in ComplianceDecision)
			if (!int.TryParse(dto.RemitId, out int remitId)) return saved;

			var remit = await _remittanceService.GetByIdAsync(remitId);
			if (remit == null) return saved;

			// 3. Resolve the customer's userId for notifications
			int? notifyUserId = null;
			try
			{
				var customer = await _customerService.GetByIdAsync(remit.CustomerId);
				notifyUserId = customer?.UserID;
			}
			catch { /* notification failure should never block the decision */ }

			// 4. Drive status based on decision
			switch (dto.Decision?.Trim().ToLower())
			{
				case "approve":
					// Compliance cleared → move to Validated so agent can approve
					await _remittanceService.UpdateVerificationStatusByRemitIdAsync(
						remitId, RemittanceRequestStatus.Validated);

					await TryNotifyAsync(notifyUserId, remitId, NotificationCategory.Compliance,
						$"Remittance #{remitId} has passed compliance review and is now Validated. It will be processed shortly.");
					break;

				case "hold":
					// Flag needs further review → block agent from approving
					await _remittanceService.UpdateVerificationStatusByRemitIdAsync(
						remitId, RemittanceRequestStatus.ComplianceHold);

					await TryNotifyAsync(notifyUserId, remitId, NotificationCategory.Compliance,
						$"Remittance #{remitId} has been placed on Compliance Hold. Our team is reviewing your transaction. Notes: {dto.Notes}");
					break;

				case "reject":
					// Compliance rejected → cancel and initiate refund
					await _remittanceService.UpdateVerificationStatusByRemitIdAsync(
						remitId, RemittanceRequestStatus.Cancelled);

					// Auto-create RefundRef for the full send amount
					int? refundId = null;
					try
					{
						var refund = await _refundService.CreateAsync(new CreateRefundRefDto
						{
							RemitId = remitId,
							Amount = remit.SendAmount,
							Method = RefundMethod.Original,
						});
						refundId = refund?.RefundID;
					}
					catch { /* don't block rejection if refund write fails */ }

					var refundLine = refundId.HasValue
						? $" A refund of {remit.SendAmount:0.00} has been initiated (Ref #{refundId})."
						: string.Empty;

					await TryNotifyAsync(notifyUserId, remitId, NotificationCategory.Compliance,
						$"Remittance #{remitId} has been rejected by our compliance team. Reason: {dto.Notes}.{refundLine}");
					break;
			}

			return saved;
		}

		// Best-effort notification — never let it break the main flow
		private async Task TryNotifyAsync(int? userId, int remitId, NotificationCategory category, string message)
		{
			if (userId == null || userId <= 0) return;
			try
			{
				await _notificationService.CreateAsync(new CreateNotificationDto
				{
					UserID = userId.Value,
					RemitID = remitId,
					Message = message,
					Category = category,
				});
			}
			catch { }
		}

		public async Task<IEnumerable<ComplianceDecision>> GetDecisionsByRemittanceAsync(string remitId) =>
			await _repo.GetByRemitIdAsync(remitId);

		public async Task<ComplianceDecision?> GetDecisionByIdAsync(string id) =>
			await _repo.GetByIdAsync(id);

		public async Task<bool> UpdateDecisionAsync(string id, UpdateComplianceDecisionDto dto)
		{
			var existing = await _repo.GetByIdAsync(id);
			if (existing == null) return false;

			existing.Decision = dto.Decision;
			existing.Notes = dto.Notes;
			existing.UpdateDate = DateTime.UtcNow;

			await _repo.UpdateAsync(existing);
			return true;
		}

		public async Task<bool> SoftDeleteDecisionAsync(string id)
		{
			var existing = await _repo.GetByIdAsync(id);
			if (existing == null) return false;

			existing.IsDeleted = true;
			existing.UpdateDate = DateTime.UtcNow;

			await _repo.UpdateAsync(existing);
			return true;
		}
	}
}