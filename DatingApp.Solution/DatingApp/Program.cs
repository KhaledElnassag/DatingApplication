
using DatingApp.Core.Dtos;
using DatingApp.Core.Interfaces;
using DatingApp.Core.Models;
using DatingApp.Helper.Mapper;
using DatingApp.MiddleWares;
using DatingApp.Repository.DataBase;
using DatingApp.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DatingApp
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddScoped<ITokenService, TokenService>();
			builder.Services.Configure<ApiBehaviorOptions>(options =>
			{
				options.InvalidModelStateResponseFactory = action =>
				{
					var Errors=action.ModelState.SelectMany(M => M.Value.Errors).Select(E => E.ErrorMessage).ToList();
					return new BadRequestObjectResult(new ValidationErrorDto(400,Errors));
				};
			});

			builder.Services.AddDbContext<ApplicationContext>(opt =>
			{
				opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			});
			builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(op =>
			{
				op.Password.RequireLowercase = true;
				op.Password.RequireUppercase = true;
				op.Password.RequireDigit = true;
				op.Password.RequireNonAlphanumeric = true;
			})
				.AddEntityFrameworkStores<ApplicationContext>();
			builder.Services.AddAuthentication(op =>
			{
				op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(op =>
			{
				op.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidateIssuer = true,
					ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
					ValidateAudience = true,
					ValidAudience = builder.Configuration["JWT:ValidAudience"],
					ValidateLifetime = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
				};
			});
			builder.Services.AddCors(op =>
			{
				op.AddPolicy("MyPolicy", option =>
				{
					
						string FrontUrl = builder.Configuration["FronBaseUrl"];
						option.AllowAnyHeader().AllowAnyMethod().WithOrigins(FrontUrl);
					
				});
			});
			builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
			var app = builder.Build();

			#region Update Database
			using var Scope = app.Services.CreateScope();
			var Provider = Scope.ServiceProvider;
			var Context = Provider.GetRequiredService<ApplicationContext>();
			var UserManager = Provider.GetRequiredService<UserManager<ApplicationUser>>();
			try
			{
				await Context.Database.MigrateAsync();
				await SeedingData.Seeding(Context, UserManager);
			}
			catch (Exception e)
			{
				var Factory = Provider.GetRequiredService<ILoggerFactory>();
				var Logger= Factory.CreateLogger<Program>();
				Logger.LogError(e,e.Message);
			}
			#endregion

			// Configure the HTTP request pipeline.
			app.UseMiddleware<ExceptionMiddleWare>();
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();
			app.UseStatusCodePagesWithReExecute("/Error/{0}");
			app.UseStaticFiles();
			app.UseCors("MyPolicy");
			app.UseAuthentication();
			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
