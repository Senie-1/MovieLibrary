using Microsoft.AspNetCore.Authorization;
using MovieLibrary.Data.Persistance;
using MovieLibrary.DTOs;
using MovieLibrary.Models.Domain.Entities;
using MovieLibrary.Models.Domain.Enums;


namespace MovieLibrary.Controllers
{
    [Authorize]
    public class WishlistController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;


        public WishlistController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

  
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);

            var wishlist = await _context.UserMovies
                .Where(um => um.UserId == userId)
                .Include(um => um.Movie)
                .Select(um => new WishlistDto
                {
                    Id = um.Movie.Id,
                    MovieTitle = um.Movie.Title,
                    PosterUrl = um.Movie.ImageUrl,
                    
                })
                .ToListAsync();

            return View(wishlist);
        }

    
        [HttpPost]
        public async Task<IActionResult> Add(int movieId, WatchStatus status)
        {

            var userId = _userManager.GetUserId(User);
            var item = new UserMovie
            {
                MovieId = movieId,
                UserId = userId,
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
