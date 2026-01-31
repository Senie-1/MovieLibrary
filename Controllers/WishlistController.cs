
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
            return View(await _context.UserMovies
                .Include(um => um.Movie)
                .ToListAsync());
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

            return RedirectToAction(nameof(Index));
        }

       
        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int id, WatchStatus status)
        {
            var item = await _context.UserMovies.FindAsync(id);
            if (item == null)
                return NotFound();

            item.WatchStatus = status;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

      
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.UserMovies.FindAsync(id);
            if (item == null)
                return NotFound();

            _context.UserMovies.Remove(item);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
