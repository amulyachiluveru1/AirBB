using AirBB.Models.DataLayer;
using AirBB.Models.DataLayer.Repositories;
using AirBB.Models.DomainModels;
using Microsoft.AspNetCore.Mvc;

namespace AirBB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepo;

        public UsersController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public IActionResult Index()
        {
            var OwnerOptions = new QueryOptions<User>
            {
                OrderBy = l => l.Name,
                OrderByDirection = "asc"
            };
            var list = _userRepo.List(OwnerOptions).ToList();
            return View(list);
        }

        public IActionResult AddUpdate(int? id)
        {
            ViewBag.UserTypes = new[] { "Owner", "Admin", "Client" };

            if (id == null || id == 0)
                return View(new User());

            var entity = _userRepo.Get(id.Value);
            if (entity == null)
                return NotFound();

            return View(entity);
        }

        [HttpPost]
        public IActionResult AddUpdate(User model)
        {
            ViewBag.UserTypes = new[] { "Owner", "Admin", "Client" };

            // Custom validation example
            if (!model.HasValidContact)
                ModelState.AddModelError("", "Either Phone Number or Email must be provided.");

            if (!ModelState.IsValid)
                return View(model);

            if (model.UserId == 0)
                _userRepo.Insert(model);
            else
                _userRepo.Update(model);

            _userRepo.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var entity = _userRepo.Get(id);
            if (entity == null)
                return NotFound();

            return View(entity);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int userId)
        {
            var entity = _userRepo.Get(userId);
            if (entity == null)
                return NotFound();

            _userRepo.Delete(entity);
            _userRepo.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}
