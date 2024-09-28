using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Core.Models
{
	public class Message : BaseEntity
	{
		public string SenderName { get; set; }
		[ForeignKey("Sender")]
		public string SenderId { get; set; }
		public ApplicationUser? Sender { get; set;}
		public string ReciverName { get; set; }
		[ForeignKey("Reciver")]
		public string ReciverId { get; set;}
		public ApplicationUser? Reciver { get; set;}
		public string Content { get; set; }
		public DateTime? DateRead { get; set; }
		public bool? SenderDelete { get; set; }
		public bool? ReciverDeleted { get; set; }



	}
}
