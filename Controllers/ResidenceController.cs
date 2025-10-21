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
                Residence = residence,
                Locations = _context.Locations.OrderBy(l => l.Name).ToList()
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Reserve(int residenceId, DateTime start, DateTime end)
        {
            if (start.Date > end.Date)
            {
                TempData["ReservationMessage"] = "Invalid date range.";
                return RedirectToAction("Index", "Home");
            }
            bool conflict = _context.Reservations.Any(r =>
                            r.ResidenceId == residenceId && r.ReservationStartDate <= end && r.ReservationEndDate >= start);

            if (conflict)
            {
                TempData["ReservationMessage"] = "Residence not available for selected dates.";
                return RedirectToAction("Index", "Home");
            }

            var reservation = new Reservation
            {
                ResidenceId = residenceId,
                ReservationStartDate = start.Date,
                ReservationEndDate = end.Date,
                ClientUserId = "guest" 
            };

            _context.Reservations.Add(reservation);
            _context.SaveChanges();

            TempData["ReservationMessage"] = $"Reserved from {start:MM/dd/yyyy} to {end:MM/dd/yyyy}";
            return RedirectToAction("Index", "Home");
        }
        public IActionResult List(string id = "All")
        {
            var area = RouteData.Values["area"] ?? "Public";
            return Content($"Area: {area} | Controller: Residence | Action: List | id = {id}");
        }
    }
}
