
namespace DatingApp.Core.Models
{
	public class MessageDto
    {
        public int Id { get; set; }

        public string SenderId { get; set; }
        public string SenderName { get; set; }
        public string SenderPhotoUrl { get; set; }
        public string ReciverId { get; set;}
        public string ReciverName { get; set; }
        public string ReciverPhotoUrl { get; set; }
        public string Content { get; set; }
        public DateTime? DateRead { get; set; }
        public DateTime MessageSent { get; set; } 
       
    }
}
