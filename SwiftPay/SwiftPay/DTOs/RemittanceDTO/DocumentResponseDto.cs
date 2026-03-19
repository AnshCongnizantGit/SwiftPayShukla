using System;

namespace SwiftPay.DTOs.RemittanceDTO
{
    public class DocumentResponseDto
    {
        public int DocumentId { get; set; }
        public string RemitId { get; set; } = default!;
        public string DocType { get; set; } = default!;
        public string FileURI { get; set; } = default!;
        public string VerificationStatus { get; set; } = default!;
        public DateTimeOffset UploadedDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
