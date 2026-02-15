using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Business.Services;
using MovieLibrary.Models.ViewModels.Actors;

namespace MovieLibrary.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorService _actorService;

        public ActorsController(IActorService actorService)
        {
            _actorService = actorService;
        }

        // GET: Actors
        public async Task<IActionResult> Index()
        {
            var actors = await _actorService.GetAllAsync();
            return View(actors);
        }

        // GET: Actors/Details/{id}
        public async Task<IActionResult> Details(Guid id)
        {
            var actor = await _actorService.GetByIdAsync(id);

            if (actor == null)
                return NotFound();

            return View(actor);
        }

        // GET: Actors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Actors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ActorCreateOrEditViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var id = await _actorService.CreateAsync(model);

            return RedirectToAction(nameof(Details), new { id });
        }

        // GET: Actors/Edit/{id}
        public async Task<IActionResult> Edit(Guid id)
        {
            var actor = await _actorService.GetForEditAsync(id);

            if (actor == null)
                return NotFound();

            return View(actor);
        }

        // POST: Actors/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ActorCreateOrEditViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var updated = await _actorService.UpdateAsync(id, model);

            if (!updated)
                return NotFound();

            return RedirectToAction(nameof(Details), new { id });
        }

        // POST: Actors/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _actorService.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
