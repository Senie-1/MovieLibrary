using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieLibrary.Business.Services;
using MovieLibrary.Business.Services.Interfaces;
using MovieLibrary.Models.ViewModels.Movies;
using MovieLibrary.Data.Persistance;
using MovieLibrary.Models.Domain.Entities;

namespace MovieLibrary.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IGenreService _genreService;
        private readonly IActorService _actorService;
        private readonly ApplicationDbContext _context;

        public MoviesController(
            IMovieService movieService,
            IGenreService genreService,
            IActorService actorService,
            ApplicationDbContext context)
        {
            _movieService = movieService;
            _genreService = genreService;
            _actorService = actorService;
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            var movies = await _movieService.GetAllAsync();
            return View(movies);
        }

        // GET: Movies/Details/{id}
        public async Task<IActionResult> Details(Guid id)
        {
            var movie = await _movieService.GetByIdAsync(id);

            if (movie == null)
                return NotFound();

            return View(movie);
        }

        // GET: Movies/Create
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Genres = new SelectList(
                await _genreService.GetAllAsync(),
                "Id",
                "Name");

            ViewBag.Actors = new SelectList(
                await _actorService.GetAllAsync(),
                "Id",
                "FullName");

            return View();
        }

        // POST: Movies/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovieCreateOrEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var id = await _movieService.CreateAsync(model);

            return RedirectToAction(nameof(Details), new { id });
        }

        // GET: Movies/Edit/{id}
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var movie = await _movieService.GetForEditAsync(id);

            if (movie == null)
                return NotFound();

            return View(movie);
        }

        // POST: Movies/Edit/{id}
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, MovieCreateOrEditViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var updated = await _movieService.UpdateAsync(id, model);

            if (!updated)
                return NotFound();

            return RedirectToAction(nameof(Details), new { id });
        }

        // GET: Movies/Delete/{id}
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var movie = await _movieService.GetByIdAsync(id);

            if (movie == null)
                return NotFound();

            return View(movie);
        }

        // POST: Movies/Delete/{id}
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var deleted = await _movieService.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ByGenre(Guid genreId)
        {
            var movies = await _movieService.GetByGenreAsync(genreId);
            return View("Index", movies);
        }

        [HttpPost]
        public async Task<IActionResult> Rate([FromBody] RatingRequest request)
        {
            if (request == null)
                return BadRequest();

            if (request.Rating < 0 || request.Rating > 10)
                return BadRequest("Rating must be between 0 and 10.");

            var movie = await _context.Movies.FindAsync(request.MovieId);

            if (movie == null)
                return NotFound();

            movie.Rating = request.Rating;
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}

