using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieLibrary.Data;
using MovieLibrary.DTOs;
using MovieLibrary.Models;

namespace MovieLibrary.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // =======================
        // INDEX (ALL USERS)
        // =======================
        public async Task<IActionResult> Index()
        {
            var movies = await _context.Movies
                .Select(m => new MovieListDto
                {
                    Id = m.Id,
                    Title = m.Title,
                    Year = m.Year,
                    ImageUrl = m.ImageUrl,
                    Rating = m.Rating
                })
                .ToListAsync();

            return View(movies);
        }

        // =======================
        // DETAILS (ALL USERS)
        // =======================
        public async Task<IActionResult> Details(int id)
        {
            var movie = await _context.Movies
                .Where(m => m.Id == id)
                .Select(m => new MovieDetailsDto
                {
                    Id = m.Id,
                    Title = m.Title,
                    Description = m.Description,
                    Year = m.Year,
                    ImageUrl = m.ImageUrl,

                    Genres = m.MovieGenres
                        .Select(mg => mg.Genre.Name)
                        .ToList(),

                    Actors = m.MovieActors
                        .Select(ma => ma.Actor.Name)
                        .ToList(),

                    Reviews = m.Reviews
                        .Select(r => new ReviewDto
                        {
                            Id = r.Id,
                            Rating = r.Rating,
                            Comment = r.Comment
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();

            if (movie == null)
                return NotFound();

            return View(movie);
        }

        // =======================
        // CREATE (ADMIN ONLY)
        // =======================
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovieCreateDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var movie = new Movie
            {
                Title = dto.Title,
                Description = dto.Description,
                Year = dto.Year,
                ImageUrl = dto.ImageUrl,
                Rating = dto.Rating
            };

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
