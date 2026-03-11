// /Domain/Remittance/RemittanceRequest.cs
using System;
using SwiftPay.Constants.Enums;
namespace RemittanceModule
{
	/// <summary>
	/// Remittance request header.
	/// </summary>
	public class RemittanceRequest
	{
		public Guid RemitId { get; set; }           // PK

		public Guid CustomerId { get; set; }
		public Guid BeneficiaryId { get; set; }

		public string FromCurrency { get; set; }     // 3-letter code, required
		public string ToCurrency { get; set; }       // 3-letter code, required

		public decimal SendAmount { get; set; }      // >= 0
		public decimal ReceiverAmount { get; set; }  // computed: (SendAmount * RateApplied) - FeeApplied

		public string QuoteId { get; set; }          // required (external quote ref)

		public decimal FeeApplied { get; set; }      // >= 0
		public decimal RateApplied { get; set; }     // > 0 (e.g., 83.123456)

		public string PurposeCode { get; set; }      // required, non-empty
		public string SourceOfFunds { get; set; }    // required, non-empty

		public DateTimeOffset CreatedDate { get; set; } // default GETUTCDATE()

		public DateTimeOffset UpdateDate { get; set; }
		public RemittanceRequestStatus Status { get; set; }    // default Draft/Validated/...

		// Navigation (optional to add later)
		// public ICollection<RemitValidation> Validations { get; set; }
		// public ICollection<Document> Documents { get; set; }
		//added something
	}
}
