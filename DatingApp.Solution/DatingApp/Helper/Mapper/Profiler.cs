using AutoMapper;
using DatingApp.Core.Dtos;
using DatingApp.Core.Models;

namespace DatingApp.Helper.Mapper
{
	public class Profiler:Profile
	{
        public Profiler()
        {
            CreateMap<ApplicationUser, MembrsDto>()
                .ForMember(S=>S.Created,O=>O.MapFrom(D=>D.InsertedIn))
                .ForMember(S=>S.PhotoUrl,O=>O.MapFrom(D=>D.Photos.FirstOrDefault(P=>P.IsMain).Url))
                ;
            CreateMap<Photo, PhotoDto>();
            CreateMap<UpdateDto, ApplicationUser>();
			CreateMap<RegisterDto, ApplicationUser>()
                .ForMember(S => S.DateOfBirth, O => O.MapFrom(D =>DateOnly.FromDateTime(D.DateOfBirth)));
			CreateMap<Message, MessageDto>()
				.ForMember(S => S.MessageSent, O => O.MapFrom(D => D.InsertedIn))
				.ForMember(S => S.SenderPhotoUrl, O => O.MapFrom(D => D.Sender.Photos.FirstOrDefault(P => P.IsMain).Url))
				.ForMember(S => S.ReciverPhotoUrl, O => O.MapFrom(D => D.Reciver.Photos.FirstOrDefault(P => P.IsMain).Url));

		}
	}
}
