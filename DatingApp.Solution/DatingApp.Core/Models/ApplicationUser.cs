using DatingApp.Core.Extensions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Core.Models
{
	public class ApplicationUser : IdentityUser
	{
		public DateOnly DateOfBirth { get; set; }
		public string? KnownAs { get; set; }
		public DateTime LastActive { get; set; }
		public string? Gender { get; set; }
		public string? Introduction { get; set; }
		public string? LokingFor { get; set; }
		public string? Intersts { get; set; }
		public string? City { get; set; }
		public string? Country { get; set; }
		public ICollection<Photo> Photos { get; set; } = new HashSet<Photo>();
		public bool IsActive { get; set; } = true;
		[ForeignKey("InsertedBy")]
		public string? InsertedById { get; set; }
		public ApplicationUser? InsertedBy { get; set; }
		public DateTime InsertedIn { get; set; }=DateTime.Now;
		[ForeignKey("ModifiedBy")]
		public string? ModifiedById { get; set; }
		public ApplicationUser? ModifiedBy { get; set; }
		public DateTime? ModifiedIn { get; set; }
		[ForeignKey("DeletedBy")]

		public string? DeletedById { get; set; }
		public ApplicationUser? DeletedBy { get; set; }
		public DateTime? DeletedIn { get; set; }
		ICollection<UserRole> UserRoles=new HashSet<UserRole>();
		public int Age => DateOfBirth.GetAge();
    }
}
