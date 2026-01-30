using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Models;

namespace MovieLibrary.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Review review)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Details", "Movies", new { id = review.MovieId });

            review.CreatedAt = DateTime.Now;

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Movies", new { id = review.MovieId });
        }
    }
}
