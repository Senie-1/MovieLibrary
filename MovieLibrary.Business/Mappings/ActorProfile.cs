using AutoMapper;
using MovieLibrary.Models.Domain.Entities;
using MovieLibrary.Models.ViewModels.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Business.Mappings
{
    public class ActorProfile : Profile
    {
        public ActorProfile()
        {
            // Entity -> ViewModel (List / Details)
            CreateMap<Actor, ActorViewModel>()
                .ForMember(dest => dest.MoviesCount,
                    opt => opt.MapFrom(src => src.MovieActors.Count));

            // Entity -> Create/Edit ViewModel
            CreateMap<Actor, ActorCreateOrEditViewModel>();

            // Create/Edit ViewModel -> Entity
            CreateMap<ActorCreateOrEditViewModel, Actor>()
                .ForMember(dest => dest.MovieActors,
                    opt => opt.Ignore()); // Не пипаме navigation property
        }
    }
}
