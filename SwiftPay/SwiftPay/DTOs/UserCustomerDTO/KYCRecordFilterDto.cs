using System.ComponentModel.DataAnnotations;
using SwiftPay.Constants.Enums;

namespace SwiftPay.DTOs.UserCustomerDTO
{
    public class KYCRecordFilterDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "VerificationStatus filter value must be valid.")]
        public KycVerificationStatus? VerificationStatus { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "PageNumber must be at least 1.")]
        public int PageNumber { get; set; } = 1;

        [Range(1, 100, ErrorMessage = "PageSize must be between 1 and 100.")]
        public int PageSize { get; set; } = 10;

        public bool IsValid(out string error)
        {
            error = null;
            if (PageNumber < 1 || PageSize < 1)
            {
                error = "PageNumber and PageSize must be positive integers.";
                return false;
            }
            return true;
        }
    }

    public class KYCRecordListDto
    {
        public IEnumerable<KYCRecordResponseDto> Records { get; set; }
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    }
}
