
using MovieLibrary.Business.Services.Interfaces;
using MovieLibrary.Models.ViewModels.Genres;

namespace MovieLibrary.Controllers
{
    public class GenresController : Controller
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        
        public async Task<IActionResult> Index()
        {
            var genres = await _genreService.GetAllAsync();
            return View(genres);
        }

       
        public async Task<IActionResult> Details(Guid id)
        {
            var genre = await _genreService.GetByIdAsync(id);

            if (genre == null)
                return NotFound();

            return View(genre);
        }

       
        public IActionResult Create()
        {
            return View();
        }

   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GenreCreateOrEditViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var id = await _genreService.CreateAsync(model);

            return RedirectToAction(nameof(Details), new { id });
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var genre = await _genreService.GetForEditAsync(id);

            if (genre == null)
                return NotFound();

            return View(genre);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, GenreCreateOrEditViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var updated = await _genreService.UpdateAsync(id, model);

            if (!updated)
                return NotFound();

            return RedirectToAction(nameof(Details), new { id });
        }

    
        public async Task<IActionResult> Delete(Guid id)
        {
            var genre = await _genreService.GetByIdAsync(id);

            if (genre == null)
                return NotFound();

            return View(genre);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var deleted = await _genreService.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}

