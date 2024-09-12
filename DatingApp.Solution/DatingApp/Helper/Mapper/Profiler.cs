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
		}
    }
}
