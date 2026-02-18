using MovieLibrary.Models.ViewModels.Genres;

namespace MovieLibrary.Business.Services.Interfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreViewModel>> GetAllAsync();
        Task<GenreViewModel?> GetByIdAsync(Guid id);
        Task<GenreCreateOrEditViewModel?> GetForEditAsync(Guid id);
        Task<Guid> CreateAsync(GenreCreateOrEditViewModel model);
        Task<bool> UpdateAsync(Guid id, GenreCreateOrEditViewModel model);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
    }
}
