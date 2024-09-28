using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Core.Models
{
	public class UserLike :BaseEntity
	{
		[ForeignKey("YourLikes")]
		public string YourLikeId { get; set; }
        public ApplicationUser? YourLike { get; set;}
		[ForeignKey("LikedBy")]
		public string LikeById { get; set;}
		public ApplicationUser? LikeBy { get; set;}

	}
}
