using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MovieLibrary.Business.Repositories.Interfaces;
using MovieLibrary.Business.Services.Interfaces;
using MovieLibrary.Models.Domain.Entities;
using MovieLibrary.Models.ViewModels.Genres;

namespace MovieLibrary.Business.Services.Implementations
{
    public class GenreService : IGenreService
    {
        private readonly IRepository<Genre> _repository;
        private readonly IMapper _mapper;

        public GenreService(
            IRepository<Genre> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GenreViewModel>> GetAllAsync()
        {
            return await _repository
                .Query()
                .AsNoTracking()
                .ProjectTo<GenreViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<GenreViewModel?> GetByIdAsync(Guid id)
        {
            return await _repository
                .Query()
                .Where(g => g.Id == id)
                .AsNoTracking()
                .ProjectTo<GenreViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<GenreCreateOrEditViewModel?> GetForEditAsync(Guid id)
        {
            var genre = await _repository.GetByIdAsync(id);

            return genre == null
                ? null
                : _mapper.Map<GenreCreateOrEditViewModel>(genre);
        }

        public async Task<Guid> CreateAsync(GenreCreateOrEditViewModel model)
        {
            var genre = _mapper.Map<Genre>(model);
            genre.Id = Guid.NewGuid();

            await _repository.AddAsync(genre);
            await _repository.CommitAsync();

            return genre.Id;
        }

        public async Task<bool> UpdateAsync(Guid id, GenreCreateOrEditViewModel model)
        {
            var genre = await _repository.GetByIdAsync(id);

            if (genre == null)
                return false;

            _mapper.Map(model, genre);

            _repository.Update(genre);
            await _repository.CommitAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var genre = await _repository.GetByIdAsync(id);

            if (genre == null)
                return false;

            _repository.Remove(genre);
            await _repository.CommitAsync();

            return true;
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _repository
                .Query()
                .AnyAsync(g => g.Id == id);
        }
    }
}
