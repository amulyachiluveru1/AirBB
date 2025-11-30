using AirBB.Models.DataLayer;
using AirBB.Models.DomainModels;
using Microsoft.AspNetCore.Mvc;

namespace AirBB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly AirBnBContext _ctx;
        public UsersController(AirBnBContext ctx) => _ctx = ctx;

        public IActionResult Index()
        {
            return View(_ctx.Users.ToList());
        }

        public IActionResult AddUpdate(int? id)
        {
            ViewBag.UserTypes = new[] { "Owner", "Admin", "Client" };

            if (id == null || id == 0)
                return View(new User());

            var entity = _ctx.Users.Find(id);
            if (entity == null)
                return NotFound();

            return View(entity);
        }

        [HttpPost]
        public IActionResult AddUpdate(User model)
        {
            ViewBag.UserTypes = new[] { "Owner", "Admin", "Client" };

            if (!model.HasValidContact)
                ModelState.AddModelError("", "Either Phone Number or Email must be provided.");

            if (!ModelState.IsValid)
                return View(model);

            if (model.UserId == 0)
                _ctx.Users.Add(model);
            else
                _ctx.Users.Update(model);

            _ctx.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var entity = _ctx.Users.Find(id);
            if (entity == null)
                return NotFound();

            return View(entity);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int userId)
        {
            var entity = _ctx.Users.Find(userId);
            if (entity == null)
                return NotFound();

            _ctx.Users.Remove(entity);
            _ctx.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
