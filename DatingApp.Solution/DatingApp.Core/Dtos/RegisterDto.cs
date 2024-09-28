using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Core.Dtos
{
	public class RegisterDto
	{
        [Required(ErrorMessage = "User Name Is Required!")]
        public string UserName { get; set; }
		public DateTime DateOfBirth { get; set; }
		public string? KnownAs { get; set; }
		public string? Gender { get; set; }
		public string? City { get; set; }
		public string? Country { get; set; }
		[Required(ErrorMessage = "Password Is Required!")]
		[DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
