﻿using System;
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
		[Required(ErrorMessage = "Password Is Required!")]
		[DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
