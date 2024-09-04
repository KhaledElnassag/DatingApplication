using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Core.Dtos
{
	public class ErrorDto
	{
		public int StatusCode { get; set; }
		public string? Message { get; set; }
		public ErrorDto(int statusCode, string? message=null)
		{
			StatusCode = statusCode;
			if (string.IsNullOrEmpty(message))
			{
				Message = statusCode switch
				{
					400 => "Bad Request You Are Made", 
					401 => "Un Authorize User!",
					403 => "Forbidden Error!",
					404 => "Not Found Resources",
					500 => "Server Side Error",
					_ => "There Some Thing Wrong"
				};
			}
			else Message=message;
		}
	}
}
