using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Core.Models
{
	public class BaseEntity
	{
		public int Id { get; set; }
		public bool IsActive { get; set; } = true;
		[ForeignKey("InsertedBy")]
		public string? InsertedById { get; set; }
		public ApplicationUser? InsertedBy { get; set; }
		public DateTime InsertedIn { get; set; } = DateTime.Now;
		[ForeignKey("ModifiedBy")]

		public string? ModifiedById { get; set; }
		public ApplicationUser? ModifiedBy { get; set; }
		public DateTime? ModifiedIn { get; set; }
		[ForeignKey("DeletedBy")]

		public string? DeletedById { get; set; }
		public ApplicationUser? DeletedBy { get; set; }
		public DateTime? DeletedIn { get; set; }
	}
}
