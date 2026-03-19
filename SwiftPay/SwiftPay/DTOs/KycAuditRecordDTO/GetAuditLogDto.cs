using System;
using System.ComponentModel.DataAnnotations;

namespace SwiftPay.DTOs.UserCustomerDTO
{
    // DTO for retrieving audit logs with additional details
    public class GetAuditLogDto
    {
        public int AuditID { get; set; }

        public int UserID { get; set; }

        public string UserName { get; set; }

        public string Action { get; set; }

        public string Resource { get; set; }

        public DateTime Timestamp { get; set; }

        public DateTime CreatedAt { get; set; }
    }

    // DTO for audit log list responses with filtering
    public class AuditLogListDto
    {
        public ICollection<GetAuditLogDto> AuditLogs { get; set; } = new List<GetAuditLogDto>();

        public int TotalCount { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }
}
