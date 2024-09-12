using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Core.Extensions
{
	public static class DateOnlyExtensions
	{
		public static int GetAge(this DateOnly date)
		{
			var today = DateOnly.FromDateTime(DateTime.Now);
			if ( today>= date)
			{
				return today.Year - date.Year;
			}
			return -1;
		}
	}
}
