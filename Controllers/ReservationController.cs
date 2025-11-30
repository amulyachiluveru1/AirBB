using AirBB.Models.DataLayer;
using AirBB.Models.DomainModels;
using AirBB.Models.ExtensionMethods;
using AirBB.Models.ViewModels;
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
                ClientUserId = 1
            };

            _context.Reservations.Add(reservation);
            _context.SaveChanges();
            _cookies.AddReservationId(reservation.ReservationId);
            TempData["ReservationMessage"] = $"Reserved from {start:MM/dd/yyyy} to {end:MM/dd/yyyy}";
            return RedirectToAction("Index", "Home");
        }
    }
}
