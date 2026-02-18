using AutoMapper;

using MovieLibrary.Models.Domain.Entities;
using MovieLibrary.Models.ViewModels.Genres;

namespace MovieLibrary.Business.Mappings
{
    public class GenreProfile : Profile
    {
        public GenreProfile()
        {
           
            CreateMap<Genre, GenreViewModel>();

           
            CreateMap<Genre, GenreCreateOrEditViewModel>();

          
            CreateMap<GenreCreateOrEditViewModel, Genre>();
        }
    }
}
