using MovieLibrary.Models.ViewModels.Actors;
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
