using System;
using System.Collections.Generic;
using SwiftPay.Constants.Enums;

namespace SwiftPay.Models
{
	public class User
	{
		public int UserId { get; set; }

		public string Name { get; set; }

		public string Email { get; set; }

		public string Phone { get; set; }

		// Hashed password (BCrypt). Only this is persisted.
		public string? PasswordHash { get; set; }

		public UserStatus Status { get; set; }

		public bool IsDeleted { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime UpdatedAt { get; set; }

		// Navigation
		public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
	}
}
