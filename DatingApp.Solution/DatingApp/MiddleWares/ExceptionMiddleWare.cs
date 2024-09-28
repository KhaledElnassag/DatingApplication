using DatingApp.Core.Dtos;
using DatingApp.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace DatingApp.MiddleWares
{
	public class ExceptionMiddleWare
	{
		private readonly RequestDelegate _Next;
		private readonly IHostEnvironment _Environment;
		private readonly ILogger<ExceptionMiddleWare> _Logger;

		public ExceptionMiddleWare(RequestDelegate next,IHostEnvironment environment,ILogger<ExceptionMiddleWare>logger)
        {
			_Next = next;
			_Environment = environment;
			_Logger = logger;
		}
        public async Task InvokeAsync(HttpContext action)
		{
			try
			{
				await _Next.Invoke(action);
				if (action.User.Identity.IsAuthenticated)
				{
					var userManager = action.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();
					var name = action.User.FindFirstValue(ClaimTypes.Name);
					var user=await userManager.FindByNameAsync(name);
					user.LastActive=DateTime.Now;
					await userManager.UpdateAsync(user);
				}
			}
			catch (Exception e)
			{
				_Logger.LogError(e,e.Message);
				action.Response.StatusCode = StatusCodes.Status500InternalServerError;
				action.Response.ContentType = "application/json";
				var Error = _Environment.IsDevelopment() ? new ServerErrorDto(e.Message,e.StackTrace):
				                                           new ServerErrorDto();
				var Options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
				var Json=JsonSerializer.Serialize(Error,Options);
				await action.Response.WriteAsync(Json);
			}
		}
	}
}
