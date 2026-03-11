using System;

namespace SwiftPay.Domain.Remittance.Entities
{
    public class RemitReport
    {
        public int ReportID { get; set; }
        public string Scope { get; set; }
        public string Metrics { get; set; } // JSON
        public DateTime GeneratedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}