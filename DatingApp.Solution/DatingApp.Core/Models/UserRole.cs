using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Core.Models
{
	public class UserRole : IdentityUserRole<string>
	{
        public ApplicationUser? User { get; set; }
        public ApplicationRole? Role { get; set; }
        public bool IsActive { get; set; } = true;
		public string? InsertedById { get; set; }
		public ApplicationUser? InsertedBy { get; set; }
		public DateTime InsertedIn { get; set; }=DateTime.Now;

		public string? ModifiedById { get; set; }
		public ApplicationUser? ModifiedBy { get; set; }
		public DateTime? ModifiedIn { get; set; }

		public string? DeletedById { get; set; }
		public ApplicationUser? DeletedBy { get; set; }
		public DateTime? DeletedIn { get; set; }
	}
}
