using AirBB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AirBB.Controllers
{
    public class ReservationController : Controller
    {
        private readonly AirBnBContext _context;
        private readonly AirBnbCookies _cookies;

        public ReservationController(AirBnBContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _cookies = new AirBnbCookies(accessor);
        }

        [HttpGet]
        public IActionResult Index()
        {
            var ids = _cookies.GetReservationIds();
            var reservations = new List<Reservation>();
            if (ids.Any())
            {
                reservations = _context.Reservations
                    .Include(r => r.Residence)
                    .ThenInclude(res => res.Location)
                    .Where(r => ids.Contains(r.ReservationId))
                    .OrderBy(r => r.ReservationStartDate)
                    .ToList();
            }

            var vm = new AirBnbViewModel
            {
                Reservations = reservations
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Cancel(int reservationId)
        {
            var r = _context.Reservations.Find(reservationId);
            if (r != null)
            {
                _context.Reservations.Remove(r);
                _context.SaveChanges();
                _cookies.RemoveReservationId(reservationId);
                TempData["ReservationMessage"] = "Reservation canceled.";
            }

            return RedirectToAction("Index");
        }
    }
}
