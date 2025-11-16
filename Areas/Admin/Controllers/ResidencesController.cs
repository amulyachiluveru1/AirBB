using AirBB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AirBB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ResidencesController : Controller
    {
        private readonly AirBnBContext _db;
        public ResidencesController(AirBnBContext ctx) => _db = ctx;

        public IActionResult Index()
        {
            var list = _db.Residences.Include(r => r.Location).Include(r => r.Owner).ToList();
            return View(list);
        }
        public IActionResult AddUpdate(int? id)
        {
            ViewBag.Locations = _db.Locations.OrderBy(l => l.Name).ToList();
            ViewBag.Owners = _db.Users.OrderBy(u => u.Name).ToList();
            //ViewBag.Owners = _db.Users.Where(u => u.UserType == "Owner").OrderBy(u => u.Name).ToList();
            if (id == null || id == 0)
            {
                // Create mode
                return View(new Residence());
            }

            // Edit mode
            var entity = _db.Residences.FirstOrDefault(x => x.ResidenceId == id);
            if (entity == null) return NotFound();

            return View(entity);
        }
        [HttpPost]
        public IActionResult AddUpdate(Residence model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ModelError"] = "Please fix the error";
                ViewBag.Locations = _db.Locations.OrderBy(l => l.Name).ToList();
                ViewBag.Owners = _db.Users.OrderBy(u => u.Name).ToList();
                //ViewBag.Owners = _db.Users.Where(u => u.UserType == "Owner").OrderBy(u => u.Name).ToList();
                return View(model);
            }

            if (model.ResidenceId == 0)
            {
                _db.Residences.Add(model);
            }
            else
            {
                _db.Residences.Update(model);
            }

            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var entity = _db.Residences
                .Include(r => r.Location)
                .FirstOrDefault(r => r.ResidenceId == id);

            if (entity == null)
                return NotFound();

            return View(entity);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int ResidenceId)
        {
            var entity = _db.Residences.Find(ResidenceId);
            if (entity == null)
                return NotFound();

            _db.Residences.Remove(entity);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    }
}
