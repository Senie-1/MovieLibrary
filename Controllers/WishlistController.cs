using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Models.Enums;

namespace MovieLibrary.Controllers
{
    public class WishlistController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WishlistController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserMovies.Include(x => x.Movie).ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Add(int movieId, WatchStatus status)
        {
            var item = new UserMovie
            {
                MovieId = movieId,
                WatchStatus = status
            };

            _context.UserMovies.Add(item);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
