using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Core.Dtos
{
	public class ServerErrorDto : ErrorDto
	{
        public string? Details { get; set; }
        public ServerErrorDto(string? message=null,string? error=null):base(500,message) {
			Details = error;
		}
	}
}
