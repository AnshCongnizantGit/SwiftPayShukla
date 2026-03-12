using System;
using SwiftPay.Constants.Enums;
//using SwiftPay.Domain.Remittance.Enums;

namespace SwiftPay.Domain.Remittance.Entities
{
	/// <summary>
	/// Remittance request header.
	/// </summary>
	public class RemittanceRequest
	{
        public string RemitId { get; set; }

		// Foreign references (by ID only for Phase-1)
		public int CustomerId { get; set; }

		public int BeneficiaryId { get; set; }

		// Currencies (ISO 4217)
		public string FromCurrency { get; set; }

		public string ToCurrency { get; set; }

		// Amounts
		public decimal SendAmount { get; set; }

		public decimal? ReceiverAmount { get; set; }

		// FX & Fees linkage
		public string? QuoteId { get; set; }

		public string? RateLockId { get; set; }

		public decimal? RateApplied { get; set; }

		public decimal? FeeApplied { get; set; }

		// Purpose & SoF
		public string? PurposeCode { get; set; }

		public string? SourceOfFunds { get; set; }

		// Status & lifecycle
		public RemittanceRequestStatus Status { get; set; }

		public DateTime CreatedDate { get; set; }
		public DateTime UpdateDate { get; set; }

		public bool IsDeleted { get; set; }

		// Navigation (optional to add later)
		// public ICollection<RemitValidation> Validations { get; set; }
		// public ICollection<Document> Documents { get; set; }
		//added something
	}
}

