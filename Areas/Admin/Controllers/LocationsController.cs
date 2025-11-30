using AirBB.Models.DataLayer;
using AirBB.Models.DataLayer.Repositories;
using AirBB.Models.DomainModels;
using Microsoft.AspNetCore.Mvc;

namespace AirBB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LocationsController : Controller
    {
        private readonly ILocationRepository _locationRepo;

        public LocationsController(ILocationRepository locationRepo)
        {
            _locationRepo = locationRepo;
        }

        public IActionResult Index()
        {
            var options = new QueryOptions<Location>
            {
                OrderBy = l => l.Name
            };

            var locations = _locationRepo.List(options).ToList();
            return View(locations);
        }

        public IActionResult AddUpdate(int? id)
        {
            if (id == null || id == 0)
                return View(new Location());

            var location = _locationRepo.Get(id.Value);
            if (location == null)
                return NotFound();

            return View(location);
        }

        [HttpPost]
        public IActionResult AddUpdate(Location model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.LocationId == 0)
            {
                _locationRepo.Insert(model);
            }
            else
            {
                _locationRepo.Update(model);
            }

            _locationRepo.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var location = _locationRepo.Get(id);
            if (location == null)
                return NotFound();

            return View(location);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int locationId)
        {
            var entity = _locationRepo.Get(locationId);
            if (entity == null)
                return NotFound();

            _locationRepo.Delete(entity);
            _locationRepo.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}
