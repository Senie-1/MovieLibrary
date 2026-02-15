using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MovieLibrary.Business.Repositories.Interfaces;
using MovieLibrary.Models.Domain.Entities;
using MovieLibrary.Models.ViewModels.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Business.Services.Implementations
{
    public class ActorService : IActorService
    {
        private readonly IRepository<Actor> _repository;
        private readonly IMapper _mapper;

        public ActorService(
            IRepository<Actor> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ActorViewModel>> GetAllAsync()
        {
            return await _repository
                .Query()
                .AsNoTracking()
                .ProjectTo<ActorViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<ActorViewModel?> GetByIdAsync(Guid id)
        {
            return await _repository
                .Query()
                .Where(a => a.Id == id)
                .AsNoTracking()
                .ProjectTo<ActorViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<ActorCreateOrEditViewModel?> GetForEditAsync(Guid id)
        {
            var actor = await _repository.GetByIdAsync(id);

            return actor == null
                ? null
                : _mapper.Map<ActorCreateOrEditViewModel>(actor);
        }

        public async Task<Guid> CreateAsync(ActorCreateOrEditViewModel model)
        {
            var actor = _mapper.Map<Actor>(model);

            actor.Id = Guid.NewGuid();

            await _repository.AddAsync(actor);
            await _repository.CommitAsync();

            return actor.Id;
        }

        public async Task<bool> UpdateAsync(Guid id, ActorCreateOrEditViewModel model)
        {
            var actor = await _repository.GetByIdAsync(id);

            if (actor == null)
                return false;

            _mapper.Map(model, actor);

            _repository.Update(actor);
            await _repository.CommitAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var actor = await _repository.GetByIdAsync(id);

            if (actor == null)
                return false;

            _repository.Remove(actor);
            await _repository.CommitAsync();

            return true;
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _repository
                .Query()
                .AnyAsync(a => a.Id == id);
        }
    }
}
