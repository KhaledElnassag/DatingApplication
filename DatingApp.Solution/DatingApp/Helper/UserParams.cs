namespace DatingApp.Helper
{
	public class UserParams
	{
		private int pageIndex;

		public int PageIndex
		{
			get { return pageIndex; }
			set { pageIndex = value>=1?value:1; }
		}
		private int pageSize;
		public int PageSize
		{
			get { return pageSize; }
			set { pageSize = value >= 5 ? value : 5; }
		}
		public string? Gender { get; set; }
		public int MinAge { get; set; } = 18;
		public int MaxAge { get; set; } = 100;


	}
}
