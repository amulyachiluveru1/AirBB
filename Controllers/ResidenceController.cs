using AirBB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AirBB.Controllers
{
    public class ResidenceController : Controller
    {
        private readonly AirBnBContext _context;
        private readonly AirBnbCookies _cookies;

        public ResidenceController(AirBnBContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _cookies = new AirBnbCookies(accessor);
        }
        [HttpGet]
        public IActionResult Detail(int id)
        {
            var residence = _context.Residences
                .Include(r => r.Location)
                .FirstOrDefault(r => r.ResidenceId == id);

            if (residence == null) return NotFound();

            var vm = new AirBnbViewModel
            {
                SelectedResidence = residence,
                Locations = _context.Locations.OrderBy(l => l.Name).ToList()
            };

            return View(vm);
        }

        
        public IActionResult List(string id = "All")
        {
            var area = RouteData.Values["area"] ?? "Public";
            return Content($"Area: {area} | Controller: Residence | Action: List | id = {id}");
        }
    }
}
