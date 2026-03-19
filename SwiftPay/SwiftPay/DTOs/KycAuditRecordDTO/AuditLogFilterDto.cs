using System;
using System.ComponentModel.DataAnnotations;

namespace SwiftPay.DTOs.UserCustomerDTO
{
    /// <summary>
    /// DTO for filtering audit logs with validation
    /// </summary>
    public class AuditLogFilterDto
    {
        /// <summary>
        /// Filter by User ID (optional)
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// Filter by Resource name (optional)
        /// </summary>
        [StringLength(255)]
        public string? Resource { get; set; }

        /// <summary>
        /// Filter by start date (optional)
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Filter by end date (optional)
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Page number for pagination (default: 1)
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "Page number must be at least 1.")]
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Page size for pagination (default: 20, max: 100)
        /// </summary>
        [Range(1, 100, ErrorMessage = "Page size must be between 1 and 100.")]
        public int PageSize { get; set; } = 20;

        /// <summary>
        /// Validates that start date is before or equal to end date
        /// </summary>
        public bool IsValid(out string? error)
        {
            error = null;
            
            if (StartDate.HasValue && EndDate.HasValue && StartDate > EndDate)
            {
                error = "Start date must be before or equal to end date.";
                return false;
            }

            return true;
        }
    }
}
