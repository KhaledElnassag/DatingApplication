using DatingApp.Core.Interfaces;
using DatingApp.Core.Models;
using DatingApp.Repository.DataBase.Migrations;
using DatingApp.Repository.Repositories;
using Message = DatingApp.Core.Models.Message;

namespace DatingApp.Helper
{
    public class MessageSpec:Specification<Message>
    {
        public MessageSpec(string senderId,string receptientId) {
            FilterCrietria = M => (M.SenderId == senderId && M.ReciverId == receptientId)
                                                           || (M.SenderId == receptientId && M.ReciverId == senderId);
			OrderByCrietria =M => M.InsertedIn;
            IncludeCrietria.Add(M => M.Reciver);
            IncludeCrietria.Add(M => M.Reciver.Photos);
            IncludeCrietria.Add(M => M.Sender);
            IncludeCrietria.Add(M => M.Sender.Photos);
        }

        public MessageSpec(MessageParams messageParams) 
        {
			OrderByCrietria=M => M.InsertedIn;
            
             switch(messageParams.Container)
            {
                case "Inbox" :
                    FilterCrietria=(u => u.Reciver.UserName == messageParams.Username &&
                 u.SenderDelete == null);
                    break;
                case "Outbox":
					FilterCrietria = (u => u.Sender.UserName == messageParams.Username &&
                    u.SenderDelete == null);
                    break;
               default:
					FilterCrietria = (u => u.Reciver.UserName == messageParams.Username
                    && u.ReciverDeleted == null && u.DateRead == null);
                    break;
            };
            IncludeCrietria.Add(M => M.Reciver);
            IncludeCrietria.Add(M => M.Reciver.Photos);
            IncludeCrietria.Add(M => M.Sender);
            IncludeCrietria.Add(M => M.Sender.Photos);
        }

        public  void updateUserMessage(IEnumerable<Message> messages,string sender,string reciver,IGenericeRepository<Message>_messageRepo)
        {
            foreach (var item in messages)
            {
                if (item.DateRead == null&&item.ReciverId== sender&&item.SenderId==reciver)
                {
                    item.DateRead = DateTime.UtcNow;
                    _messageRepo.Update(item);
                }
            }
           
        }
    }
}
