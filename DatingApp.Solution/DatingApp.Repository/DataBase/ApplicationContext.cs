using DatingApp.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Repository.DataBase
{
	public class ApplicationContext:IdentityDbContext<ApplicationUser,ApplicationRole,string>
	{
        public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options)
        {
            
        }
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}
        public DbSet<Photo> Photos { get; set; }
        public DbSet<UserLike> UserLikes { get; set; }
        public DbSet<Message> Messages { get; set; }
	}
}
