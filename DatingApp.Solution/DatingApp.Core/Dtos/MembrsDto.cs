using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Core.Dtos
{
	public class MembrsDto
	{
		public string Id { get; set; }
		public string UserName { get; set; }
		public string PhotoUrl { get; set; }
		public int Age { get; set; }
		public string KnownAs { get; set; }
		public DateTime Created { get; set; }
		public DateTime LastActive { get; set; }

		public string Gender { get; set; }

		public string Introduction { get; set; }
		public string LokingFor { get; set; }
		public string Intersts { get; set; }
		public string City { get; set; }
		public string Country { get; set; }
		public List<PhotoDto> Photos { get; set; }
	}
}
