using MovieLibrary.Models.ViewModels.Movies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Business.Services.Interfaces
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieViewModel>> GetAllAsync();
        Task<MovieViewModel?> GetByIdAsync(Guid id);
        Task<MovieCreateOrEditViewModel?> GetForEditAsync(Guid id);
        Task<Guid> CreateAsync(MovieCreateOrEditViewModel model);
        Task<bool> UpdateAsync(Guid id, MovieCreateOrEditViewModel model);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task<List<MovieViewModel>> GetByGenreAsync(Guid genreId);

    }
}
