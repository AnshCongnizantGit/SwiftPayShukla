using System;

namespace YourNamespace.Models
{
    public class ReconciliationRecord
    {
        public int ReconID { get; set; }

        public string? ReferenceType { get; set; } // Remit, Instruction, PartnerAck

        public string? ReferenceID { get; set; }

        public decimal ExpectedAmount { get; set; }

        public decimal ActualAmount { get; set; }

        public string? Result { get; set; } // Matched, Mismatched

        public DateTime ReconDate { get; set; }

        public string? Notes { get; set; } // Optional
    }
}
