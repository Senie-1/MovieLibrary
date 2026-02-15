using MovieLibrary.Models.ViewModels.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Business.Services
{
    public interface IActorService
    {
        Task<IEnumerable<ActorViewModel>> GetAllAsync();

        Task<ActorViewModel?> GetByIdAsync(Guid id);

        Task<ActorCreateOrEditViewModel?> GetForEditAsync(Guid id);

        Task<Guid> CreateAsync(ActorCreateOrEditViewModel model);

        Task<bool> UpdateAsync(Guid id, ActorCreateOrEditViewModel model);

        Task<bool> DeleteAsync(Guid id);

        Task<bool> ExistsAsync(Guid id);
    }
}
