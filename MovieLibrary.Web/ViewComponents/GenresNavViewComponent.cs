using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Business.Services.Interfaces;
using System.Threading.Tasks;

namespace MovieLibrary.Web.ViewComponents
{
    public class GenresNavViewComponent : ViewComponent
    {
        private readonly IGenreService _genreService;

        public GenresNavViewComponent(IGenreService genreService)
        {
            _genreService = genreService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var genres = await _genreService.GetAllAsync();
            return View(genres);
        }
    }
}
