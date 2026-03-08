using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MovieLibrary.Business.Repositories.Interfaces;
using MovieLibrary.Models.Domain.Entities;
using MovieLibrary.Models.ViewModels.Movies;
using MovieLibrary.Business.Services.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace MovieLibrary.Business.Services.Implementations
{
    public class MovieService : IMovieService
    {
        private readonly IRepository<Movie> _repository;
        private readonly IMapper _mapper;

        public MovieService(
            IRepository<Movie> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MovieViewModel>> GetAllAsync()
        {
            return await _repository
                .Query()
                .AsNoTracking()
                .ProjectTo<MovieViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<MovieViewModel?> GetByIdAsync(Guid id)
        {
            return await _repository
                .Query()
                .Where(m => m.Id == id)
                .AsNoTracking()
                .ProjectTo<MovieViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
         
        }

        public async Task<MovieCreateOrEditViewModel?> GetForEditAsync(Guid id)
        {
            var movie = await _repository.GetByIdAsync(id);

            return movie == null
                ? null
                : _mapper.Map<MovieCreateOrEditViewModel>(movie);
        }

        public async Task<Guid> CreateAsync(MovieCreateOrEditViewModel model)
        {
            var movie = _mapper.Map<Movie>(model);
            movie.Id = Guid.NewGuid();

            if (model.SelectedGenreIds != null)
            {
                foreach (var genreId in model.SelectedGenreIds)
                {
                    movie.MovieGenres.Add(new MovieGenre
                    {
                        MovieId = movie.Id,
                        GenreId = genreId
                    });
                }
            }
            if (!string.IsNullOrWhiteSpace(model.ActorNames))
            {
                var actorNames = model.ActorNames
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(a => a.Trim());

                foreach (var name in actorNames)
                {
                    var actor = new Actor
                    {
                        Id = Guid.NewGuid(),
                        FullName = name
                    };

                    movie.MovieActors.Add(new MovieActor
                    {
                        MovieId = movie.Id,
                        Actor = actor
                    });
                }
            }


            await _repository.AddAsync(movie);
            await _repository.CommitAsync();

            return movie.Id;
        }
        public async Task<bool> UpdateAsync(Guid id, MovieCreateOrEditViewModel model)
        {
            var movie = await _repository.GetByIdAsync(id);

            if (movie == null)
                return false;

            _mapper.Map(model, movie);

            _repository.Update(movie);
            await _repository.CommitAsync();

            return true;
        }
        public async Task<List<MovieViewModel>> GetByGenreAsync(Guid genreId)
        {
            var movies = await _repository
                .Query()
                .Where(m => m.MovieGenres.Any(mg => mg.GenreId == genreId))
                .ToListAsync();

            return _mapper.Map<List<MovieViewModel>>(movies);
        }


        public async Task<bool> DeleteAsync(Guid id)
        {
            var movie = await _repository.GetByIdAsync(id);

            if (movie == null)
                return false;

            _repository.Remove(movie);
            await _repository.CommitAsync();

            return true;
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _repository
                .Query()
                .AnyAsync(m => m.Id == id);
        }
    }
}
