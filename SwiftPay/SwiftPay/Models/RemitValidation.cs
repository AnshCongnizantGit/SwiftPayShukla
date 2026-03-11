using System.ComponentModel.DataAnnotations;

using SwiftPay.Constants.Enums;
using SwiftPay.Domain.Remittance.Entities;
namespace SwiftPay.Models
{
    public class RemitValidation
    {

		public Guid ValidationId { get; set; }       // PK

		public Guid RemitId { get; set; }            // FK -> RemittanceRequest
		public virtual RemittanceRequest RemittanceRequest { get; set; }

		public ValidationRuleName RuleName { get; set; }   // Limit/Velocity/Docs/Corridor
		public Constants.Enums.ValidationResult Result { get; set; }       // Pass/Fail

		public string Message { get; set; }          // required, non-empty
		public DateTimeOffset CheckedDate { get; set; } // default GETUTCDATE()

		public DateTimeOffset CreatedDate { get; set; }
		public DateTimeOffset UpdateDate { get; set; }

		public DateTimeOffset IsDeleted { get; set; }

	}
}
