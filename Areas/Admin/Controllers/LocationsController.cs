using AirBB.Models;
using Microsoft.AspNetCore.Mvc;

namespace AirBB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LocationsController : Controller
    {
        private readonly AirBnBContext _ctx;
        public LocationsController(AirBnBContext ctx) => _ctx = ctx;

        public IActionResult Index()
        {
            return View(_ctx.Locations.OrderBy(l => l.Name).ToList());
        }

        public IActionResult AddUpdate(int? id)
        {
            if (id == null || id == 0)
                return View(new Location());

            var entity = _ctx.Locations.Find(id);
            if (entity == null)
                return NotFound();

            return View(entity);
        }

        [HttpPost]
        public IActionResult AddUpdate(Location model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.LocationId == 0)
                _ctx.Locations.Add(model);
            else
                _ctx.Locations.Update(model);

            _ctx.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var entity = _ctx.Locations.Find(id);
            if (entity == null)
                return NotFound();

            return View(entity);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int locationId)
        {
            var entity = _ctx.Locations.Find(locationId);
            if (entity == null)
                return NotFound();

            _ctx.Locations.Remove(entity);
            _ctx.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
