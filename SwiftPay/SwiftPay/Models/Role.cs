using System;
using System.Collections.Generic;
using SwiftPay.Constants.Enums;

namespace SwiftPay.Models
{
    public class Role
    {
        public int RoleId { get; set; }

        public RoleType RoleType { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }
        // Navigation
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
