using Microsoft.AspNetCore.Authorization;
using MovieLibrary.Data.Persistance;
using MovieLibrary.DTOs;
using MovieLibrary.Models;
using MovieLibrary.Models.Domain.Entities;

namespace MovieLibrary.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(Guid? genreId)
        {
        
            ViewBag.Genres = await _context.Genres
                .Select(g => new
                {
                    g.Id,
                    g.Name
                })
                .ToListAsync();

            var moviesQuery = _context.Movies
                .Include(m => m.MovieGenres)
                .Include(m => m.Reviews)
                .AsQueryable();

            if (genreId.HasValue)
            {
                moviesQuery = moviesQuery
                    .Where(m => m.MovieGenres
                        .Any(mg => mg.GenreId == genreId.Value));
            }

            var movies = await moviesQuery
                .Select(m => new MovieListDto
                {
                    Id = m.Id,
                    Title = m.Title,
                    ImageUrl = m.ImageUrl,
                    Year = m.ReleaseYear,
                    Rating = m.Reviews.Any()
                        ? m.Reviews.Average(r => r.Rating)
                        : 0
                })
                .ToListAsync();

            return View(movies);
        }




        public async Task<IActionResult> Details(Guid id)
        {
            var movie = await _context.Movies
                .Where(m => m.Id == id)
                .Select(m => new MovieDetailsDto
                {
                    Id = m.Id,
                    Title = m.Title,
                    Description = m.Description,
                    Year = m.ReleaseYear,
                    ImageUrl = m.ImageUrl,

                    Genres = m.MovieGenres
                        .Select(mg => mg.Genre.Name)
                        .ToList(),

                    Actors = m.MovieActors
                        .Select(ma => ma.Actor.FullName)
                        .ToList(),

                    Reviews = m.Reviews
                        .Select(r => new ReviewDto
                        {
                            UserName = r.User.UserName,
                            Rating = r.Rating,
                            Comment = r.Content
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();

            if (movie == null)
                return NotFound();

            return View(movie);
        }

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
                ReleaseYear = dto.Year,
                ImageUrl = dto.PosterUrl,
                Rating = dto.Rating
            };

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
