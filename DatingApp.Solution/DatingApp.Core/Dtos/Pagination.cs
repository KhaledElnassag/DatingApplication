using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Core.Dtos
{
	public class Pagination<T>
	{
		public Pagination(int count, int pageIndex, int pageSize, IReadOnlyList<T> data)
		{
			Count = count;
			PageIndex = pageIndex;
			PageSize = pageSize;
			Data = data;
		}

		public int Count { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
		public IReadOnlyList<T> Data { get; set; }	
	}
}
