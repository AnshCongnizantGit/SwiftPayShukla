using System;

namespace SwiftPay.DTOs.RemittanceDTO
{
    public class CreateRemittanceResponseDto
    {
        // RemitId stored as string GUID in the domain model
        public string RemitId { get; set; } = default!;

        public string Status { get; set; } = default!;

        public string FromCurrency { get; set; } = default!;
        public string ToCurrency { get; set; } = default!;

        public decimal SendAmount { get; set; }
        public decimal? ReceiverAmount { get; set; }

        public decimal? RateApplied { get; set; }
        public decimal? FeeApplied { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
