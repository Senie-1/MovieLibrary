
using MovieLibrary.Data.Persistance;
using MovieLibrary.Models.Domain.Entities;

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

        
        public async Task<IActionResult> Delete(Guid id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
                return NotFound();

            Guid movieId = review.MovieId;
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Movies", new { id = movieId });
        }
    }
}

