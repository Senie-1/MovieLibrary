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
            // We only need to prepare the dropdowns here
            ViewBag.Genres = new SelectList(await _genreService.GetAllAsync(), "Id", "Name");
            ViewBag.Actors = new SelectList(await _actorService.GetAllAsync(), "Id", "FullName");
            return View(new MovieCreateOrEditViewModel());
        }

        // POST: Movies/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovieCreateOrEditViewModel model, IFormFile ImageFile)
        {
            if (ImageFile != null)
            {
                // 1. Ensure the directory exists
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

                // 2. Create a unique filename to prevent overwriting
                var fileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(ImageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                // 3. Save the file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                // 4. Set the URL for the database (relative path)
                model.ImageUrl = "/images/" + fileName;
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Genres = new SelectList(await _genreService.GetAllAsync(), "Id", "Name");
                ViewBag.Actors = new SelectList(await _actorService.GetAllAsync(), "Id", "FullName");
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

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
                return NotFound();

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
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
        


    } }

