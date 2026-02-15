using AutoMapper;
using MovieLibrary.Models.Domain.Entities;
using MovieLibrary.Models.ViewModels.Actors;
namespace MovieLibrary.Business.Mappings
{
    public class ActorProfile : Profile
    {
        public ActorProfile()
        {
           
            CreateMap<Actor, ActorViewModel>()
                .ForMember(dest => dest.MoviesCount,
                    opt => opt.MapFrom(src => src.MovieActors.Count));

           
            CreateMap<Actor, ActorCreateOrEditViewModel>();

            
            CreateMap<ActorCreateOrEditViewModel, Actor>()
                .ForMember(dest => dest.MovieActors,
                    opt => opt.Ignore()); 
        }
    }
}
