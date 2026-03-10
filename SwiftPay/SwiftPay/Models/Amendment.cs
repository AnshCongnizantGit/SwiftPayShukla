using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwiftPay.Domain.Remittance.Entities
{
    public class Amendment
    {
        // Primary Key
        public string AmendmentId { get; set; }

        // Foreign references (by ID only for Phase-1)
        // FK → RemittanceRequest(RemitId)
        public string RemitId { get; set; }

        // Which field changed
        public string FieldChanged { get; set; }

        // Old/New values (TEXT in DB)
        public string? OldValue { get; set; }

        public string? NewValue { get; set; }

        // Who requested (FK → Users(UserId)) — IDs-only in Phase-1
        public string RequestedByUserId { get; set; }

        // When requested
        public DateTimeOffset RequestedDate { get; set; }

        // Status: Pending | Approved | Rejected (string for Phase-1, to mirror your pattern)
        public string Status { get; set; }

        // Audit (mirrors RemittanceRequest.cs)
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset? UpdatedDate { get; set; }

        public string? CreatedByUserId { get; set; }

        public string? UpdatedByUserId { get; set; }

        // Optimistic concurrency
        public byte[] RowVersion { get; set; }
    }
}