using System.ComponentModel.DataAnnotations;
using SwiftPay.Constants.Enums;

namespace SwiftPay.DTOs.UserCustomerDTO
{
    public class UpdateKycStatusDto
    {
        [Required(ErrorMessage = "VerificationStatus is required.")]
        public KycVerificationStatus VerificationStatus { get; set; }

        [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters.")]
        public string Notes { get; set; }
    }
}
