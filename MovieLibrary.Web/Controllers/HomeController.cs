using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieLibrary.Data;
using MovieLibrary.Data.Persistance;

namespace MovieLibrary.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.NavGenres = await _context.Genres
    .Select(g => new { g.Id, g.Name })
    .ToListAsync();

            var topMovies = await _context.Movies
            .OrderByDescending(m => m.Reviews.Any()
                ? m.Reviews.Average(r => r.Rating)
                : 0)
            .Take(4)
            .ToListAsync();

            return View(topMovies);
          

        }
    }
}
