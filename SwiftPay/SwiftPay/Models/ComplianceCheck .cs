using System;

namespace SwiftPay.Domain.Remittance.Entities
{
    public class ComplianceCheck
    {
        public string CheckId { get; set; }

        // Foreign Key to RemittanceRequest
        public string RemitId { get; set; }

        // Check Type: Sanctions, PEP, AML, Geo
        public string CheckType { get; set; }

        // Result: Clear, Flag, Hold
        public string Result { get; set; }

        // Severity: Low, Medium, High
        public string Severity { get; set; }

        public DateTimeOffset CheckedDate { get; set; }

        // Optimistic concurrency to prevent race conditions during compliance updates
        public byte[] RowVersion { get; set; }

        // Navigation Property (Optional but recommended in .NET EF Core)
        // [ForeignKey("RemitId")]
        // public virtual RemittanceRequest Remittance { get; set; } = default!;
    }
}