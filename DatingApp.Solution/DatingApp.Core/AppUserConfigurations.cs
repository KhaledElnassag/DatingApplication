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
	public class AppUserConfigurations : IEntityTypeConfiguration<ApplicationUser>
	{
		public void Configure(EntityTypeBuilder<ApplicationUser> builder)
		{
			builder.HasIndex(U => U.UserName).IsUnique();
			builder.HasIndex(U => U.Email).IsUnique();
			builder.HasOne(U => U.InsertedBy).WithMany().HasForeignKey(U => U.InsertedById);
			builder.HasOne(U => U.ModifiedBy).WithMany().HasForeignKey(U => U.ModifiedById);
			builder.HasOne(U => U.DeletedBy).WithMany().HasForeignKey(U => U.DeletedById);
			builder.HasMany(A => A.YourLikes).WithOne(U => U.YourLike).HasForeignKey(U => U.YourLikeId);
			builder.HasMany(A => A.LikedBy).WithOne(U => U.LikeBy).HasForeignKey(U => U.LikeById);
			builder.HasMany(A => A.Send).WithOne(U => U.Sender).HasForeignKey(U => U.SenderId);
			builder.HasMany(A => A.Recive).WithOne(U => U.Reciver).HasForeignKey(U => U.ReciverId);
		}
	}
}
