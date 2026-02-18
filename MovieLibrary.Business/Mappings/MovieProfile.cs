using AutoMapper;
using MovieLibrary.Models.Domain.Entities;
using MovieLibrary.Models.ViewModels.Movies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Business.Mappings
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
    {
        // Entity -> ViewModel (Details/List)
        CreateMap<Movie, MovieViewModel>()
            .ForMember(dest => dest.Genres,
                opt => opt.MapFrom(src =>
                    src.MovieGenres.Select(mg => mg.Genre.Name)))

            .ForMember(dest => dest.Actors,
                opt => opt.MapFrom(src =>
                    src.MovieActors.Select(ma =>
                        ma.Actor.FullName)))

            .ForMember(dest => dest.ReviewsCount,
                opt => opt.MapFrom(src => src.Reviews.Count))

            .ForMember(dest => dest.AverageReviewRating,
                opt => opt.MapFrom(src =>
                    src.Reviews.Any()
                        ? src.Reviews.Average(r => r.Rating)
                        : 0));

        // Entity -> Create/Edit
        CreateMap<Movie, MovieCreateOrEditViewModel>()
            .ForMember(dest => dest.SelectedGenreIds,
                opt => opt.MapFrom(src =>
                    src.MovieGenres.Select(mg => mg.GenreId)))

            .ForMember(dest => dest.SelectedActorIds,
                opt => opt.MapFrom(src =>
                    src.MovieActors.Select(ma => ma.ActorId)));

        // Create/Edit -> Entity
        CreateMap<MovieCreateOrEditViewModel, Movie>()
            .ForMember(dest => dest.MovieGenres,
                opt => opt.Ignore()) // ще се управлява ръчно в Service слоя

            .ForMember(dest => dest.MovieActors,
                opt => opt.Ignore())

            .ForMember(dest => dest.Reviews,
                opt => opt.Ignore())

            .ForMember(dest => dest.UserMovies,
                opt => opt.Ignore());
    }
}
}
