using System;
using SwiftPay.Constants.Enums;

namespace SwiftPay.Domain.Remittance.Entities
{
    public class ComplianceDecision
    {
        public string DecisionId { get; set; }
        public string RemitId { get; set; }
        public string AnalystId { get; set; }
        public ComplianceDecisionStatus Decision { get; set; } // Approve, Hold, Reject
        public string Notes { get; set; }
        public DateTimeOffset DecisionDate { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
        
        public byte[] RowVersion { get; set; }
    }
}