using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieLibrary.Data;
using MovieLibrary.Data.Persistance;
using MovieLibrary.Business.Services.Interfaces;

namespace MovieLibrary.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMovieService _movieService;

        public HomeController(ApplicationDbContext context, IMovieService movieService)
        {
            _context = context;
            _movieService = movieService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.NavGenres = await _context.Genres
                .Select(g => new { g.Id, g.Name })
                .ToListAsync();

            var movies = await _movieService.GetAllAsync();

            var topMovies = movies
                .OrderByDescending(m => m.AverageReviewRating)
                .ThenByDescending(m => m.ReviewsCount)
                .Take(4)
                .ToList();

            return View(topMovies);
        }
    }
}
