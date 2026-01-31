
    namespace MovieLibrary.Controllers
    {
        public class ActorsController : Controller
        {
            private readonly ApplicationDbContext _context;

            public ActorsController(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<IActionResult> Index()
            {
                return View(await _context.Actors.ToListAsync());
            }

            public async Task<IActionResult> Details(int id)
            {
                var actor = await _context.Actors.FindAsync(id);
                if (actor == null)
                    return NotFound();

                return View(actor);
            }

            public IActionResult Create()
            {
                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(Actor actor)
            {
                if (!ModelState.IsValid)
                    return View(actor);

                _context.Actors.Add(actor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            public async Task<IActionResult> Edit(int id)
            {
                var actor = await _context.Actors.FindAsync(id);
                if (actor == null)
                    return NotFound();

                return View(actor);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(Actor actor)
            {
                if (!ModelState.IsValid)
                    return View(actor);

                _context.Actors.Update(actor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            public async Task<IActionResult> Delete(int id)
            {
                var actor = await _context.Actors.FindAsync(id);
                if (actor == null)
                    return NotFound();

                _context.Actors.Remove(actor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }
    }


