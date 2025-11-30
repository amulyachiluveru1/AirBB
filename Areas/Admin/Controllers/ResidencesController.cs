using AirBB.Models.DataLayer;
using AirBB.Models.DataLayer.Repositories;
using AirBB.Models.DomainModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AirBB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ResidencesController : Controller
    {
        private readonly IResidenceRepository _resRepo;
        private readonly ILocationRepository _locRepo;
        private readonly IUserRepository _userRepo;

        public ResidencesController(
            IResidenceRepository resRepo,
            ILocationRepository locRepo,
            IUserRepository userRepo)
        {
            _resRepo = resRepo;
            _locRepo = locRepo;
            _userRepo = userRepo;
        }

        public IActionResult Index()
        {
            var options = new QueryOptions<Residence>
            {
                Includes = "Location,Owner",
                OrderBy = r => r.ResidenceId
            };

            var list = _resRepo.List(options).ToList();
            return View(list);
        }

        public IActionResult AddUpdate(int? id)
        {
            var locationOptions = new QueryOptions<Location>
            {
                OrderBy = l => l.Name,
                OrderByDirection = "asc"
            };
            var OwnerOptions = new QueryOptions<User>
            {
                OrderBy = l => l.Name,
                OrderByDirection = "asc"
            };

            ViewBag.Locations = _locRepo.List(locationOptions).ToList();
            ViewBag.Owners = _userRepo.List(OwnerOptions).ToList();
            if (id == null || id == 0)
                return View(new Residence());

            var entity = _resRepo.Get(id.Value);
            if (entity == null)
                return NotFound();

            return View(entity);
        }

        [HttpPost]
        public IActionResult AddUpdate(Residence model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ModelError"] = "Please fix the error";
                var locationOptions = new QueryOptions<Location>
                {
                    OrderBy = l => l.Name,
                    OrderByDirection = "asc"
                };
                var OwnerOptions = new QueryOptions<User>
                {
                    OrderBy = l => l.Name,
                    OrderByDirection = "asc"
                };

                ViewBag.Locations = _locRepo.List(locationOptions).ToList();
                ViewBag.Owners = _userRepo.List(OwnerOptions).ToList();

                return View(model);
            }

            if (model.ResidenceId == 0)
                _resRepo.Insert(model);
            else
                _resRepo.Update(model);

            _resRepo.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var options = new QueryOptions<Residence>
            {
                Includes = "Location,Owner",
                Where = r => r.ResidenceId == id
            };

            var entity = _resRepo.List(options).FirstOrDefault();

            if (entity == null)
                return NotFound();

            return View(entity);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int residenceId)
        {
            var entity = _resRepo.Get(residenceId);
            if (entity == null)
                return NotFound();

            _resRepo.Delete(entity);
            _resRepo.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}
