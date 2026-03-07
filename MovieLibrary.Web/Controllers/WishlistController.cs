using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                    Id = um.Id,
                    MovieTitle = um.Movie.Title,
                    PosterUrl = um.Movie.ImageUrl,
                    WatchStatus = um.WatchStatus.ToString()
                })
                .ToListAsync();

            return View(wishlist);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Guid movieId, WatchStatus status)
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
                return Challenge();

            // Prevent duplicates: update existing entry if present
            var existing = await _context.UserMovies
                .FirstOrDefaultAsync(um => um.UserId == userId && um.MovieId == movieId);

            if (existing != null)
            {
                existing.WatchStatus = status;
                _context.UserMovies.Update(existing);
            }
            else
            {
                var item = new UserMovie
                {
                    MovieId = movieId,
                    UserId = userId,
                    WatchStatus = status
                };

                await _context.UserMovies.AddAsync(item);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(Guid id, WatchStatus status)
        {
            var item = await _context.UserMovies.FindAsync(id);
            if (item == null)
                return NotFound();

            item.WatchStatus = status;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
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
