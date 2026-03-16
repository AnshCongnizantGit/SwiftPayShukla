using System.ComponentModel.DataAnnotations;

namespace SwiftPay.DTOs.RemittanceDTO
{
    // DTO containing only required fields for creating a remittance request
    public class CreateRemittanceDto
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int BeneficiaryId { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3)]
        public string FromCurrency { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3)]
        public string ToCurrency { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal SendAmount { get; set; }
    }
}
