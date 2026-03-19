namespace SwiftPay.DTOs.RemittanceDTO
{
    public class RemitValidationDto
    {
        // ValidationId corresponds to RemitValidation.ValidationId
        public Guid ValidationId { get; set; }

        // RemitId is a string GUID in the domain model
        public string RemitId { get; set; } = default!;

        public string Rule { get; set; } = default!;
        public string Result { get; set; } = default!;
        public string? Message { get; set; }
        public DateTimeOffset CheckedDate { get; set; }
    }
}
