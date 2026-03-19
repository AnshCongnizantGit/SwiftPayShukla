using System.ComponentModel.DataAnnotations;
using SwiftPay.Constants.Enums;

namespace SwiftPay.DTOs.UserCustomerDTO
{
    public class UpdateBeneficiaryVerificationStatusDto
    {
        [Required(ErrorMessage = "VerificationStatus is required.")]
        public BeneficiaryVerificationStatus VerificationStatus { get; set; }
    }
}
