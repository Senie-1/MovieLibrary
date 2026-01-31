
    namespace MovieLibrary.Controllers
    {
        public class GenresController : Controller
        {
            private readonly ApplicationDbContext _context;

            public GenresController(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<IActionResult> Index()
            {
                return View(await _context.Genres.ToListAsync());
            }

            public async Task<IActionResult> Details(int id)
            {
                var genre = await _context.Genres
                    .Include(g => g.MovieGenres)
                    .ThenInclude(mg => mg.Movie)
                    .FirstOrDefaultAsync(g => g.Id == id);

                if (genre == null)
                    return NotFound();

                return View(genre);
            }

            public IActionResult Create()
            {
                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(Genre genre)
            {
                if (!ModelState.IsValid)
                    return View(genre);

                _context.Genres.Add(genre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            public async Task<IActionResult> Edit(int id)
            {
                var genre = await _context.Genres.FindAsync(id);
                if (genre == null)
                    return NotFound();

                return View(genre);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(Genre genre)
            {
                if (!ModelState.IsValid)
                    return View(genre);

                _context.Genres.Update(genre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            public async Task<IActionResult> Delete(int id)
            {
                var genre = await _context.Genres.FindAsync(id);
                if (genre == null)
                    return NotFound();

                _context.Genres.Remove(genre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }
    }



