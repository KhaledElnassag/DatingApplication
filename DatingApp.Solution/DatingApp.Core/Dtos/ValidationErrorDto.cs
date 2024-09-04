using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Core.Dtos
{
	public class ValidationErrorDto:ErrorDto
	{
        public List<string> Errors { get; set; }
        public ValidationErrorDto(int code,List<string>errors):base(code) {
			Errors = errors;
		}
	}
}
