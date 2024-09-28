using AutoMapper;
using DatingApp.Core.Dtos;
using DatingApp.Core.Interfaces;
using DatingApp.Core.Models;

using DatingApp.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Security.Claims;

namespace DatingApp.Controllers
{

	public class MessagesController : BaseApiController
    {
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _UnitOfWork;

		public MessagesController( UserManager<ApplicationUser> userManager,IUnitOfWork unitOfWork,IMapper mapper)
        {
            
            _UserManager = userManager;
            _mapper = mapper;
            _UnitOfWork = unitOfWork;

		}

        [HttpPost]
        public async Task<ActionResult> CreateMessage(CreateMessageDto createMessage)
        {
            var usernam = User.FindFirstValue(ClaimTypes.Name);
            if(usernam== createMessage.RecipientUsername)return BadRequest(new ErrorDto(400,"you can't send to yourself"));
            var sender = await _UserManager.Users.Include(U => U.Photos).FirstOrDefaultAsync(U => U.UserName == usernam.ToLower());
            var recipient= await _UserManager.Users.Include(U => U.Photos)
                .FirstOrDefaultAsync(U => U.UserName == createMessage.RecipientUsername.ToLower()); ;
            var message = new Message()
            {
                Sender = sender,
                Reciver = recipient,
                SenderName = sender.UserName,
				ReciverName = recipient.UserName,
                Content = createMessage.Content
            };

            await _UnitOfWork.GetRepository<Message>().AddAsync(message);
            var res = await _UnitOfWork.CompleteAsync();
            if(res==0) return BadRequest(new ErrorDto(400));
            return Ok(_mapper.Map<MessageDto>(message));
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessagesForUser([FromQuery]
        MessageParams messageParams)
        {
            var usernam = User.FindFirstValue(ClaimTypes.Name);
            messageParams.Username=usernam;
            var messageSpec = new MessageSpec(messageParams);
            var messages = await _UnitOfWork.GetRepository<Message>().GetAllWithSpecAsync(messageSpec);
            var MessDto = _mapper.Map<IEnumerable<MessageDto>>(messages);
            return Ok(MessDto);
        }

        [HttpGet("thread/{userid}")]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessageForUser(string userid)
        {
            var usernam = User.FindFirstValue(ClaimTypes.Name);
            var sender = await _UserManager.FindByNameAsync(usernam);
            var messageSpec = new MessageSpec(sender.Id, userid);
            var messages=await _UnitOfWork.GetRepository<Message>().GetAllWithSpecAsync(messageSpec);
             messageSpec.updateUserMessage(messages,sender.Id,userid, _UnitOfWork.GetRepository<Message>());
            var MessDto=_mapper.Map<IEnumerable<MessageDto>>(messages);
           
            return Ok(MessDto);
        }
    }
}
