using DatingApp.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Repository.DataBase.Configurations
{
	public class UserLikeConfigurations : IEntityTypeConfiguration<UserLike>
	{
		public void Configure(EntityTypeBuilder<UserLike> builder)
		{
			builder.HasIndex(U =>new { U.LikeById, U.YourLikeId }).IsUnique();
		
		}
	}
}
