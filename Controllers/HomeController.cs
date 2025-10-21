using AirBB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AirBB.Controllers
{
    public class HomeController : Controller
    {
        private readonly AirBnBContext _context;

        public HomeController(AirBnBContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int? selectedLocationId, string start = null, string end = null, int? guests = null)
        {
            var session = new AirBnbSession(HttpContext.Session);
            var sessionFilter = session.GetFilters();

            var filter = new AirBnbViewModel
            {
                SelectedLocation = selectedLocationId ?? sessionFilter.SelectedLocation,
                CheckIn = !string.IsNullOrEmpty(start) ? DateTime.Parse(start) : sessionFilter.CheckIn,
                CheckOut = !string.IsNullOrEmpty(end) ? DateTime.Parse(end) : sessionFilter.CheckOut,
                Guests = guests ?? sessionFilter.Guests
            };
            session.SetFilters(filter);

            var q = _context.Residences.Include(r => r.Location).AsQueryable();

            if (filter.SelectedLocation.HasValue)
                q = q.Where(r => r.LocationId == filter.SelectedLocation.Value);

            if (filter.Guests > 0)
                q = q.Where(r => r.GuestNumber >= filter.Guests);

            if (filter.CheckIn.HasValue && filter.CheckOut.HasValue)
            {
                var s = filter.CheckIn.Value.Date;
                var e = filter.CheckOut.Value.Date;

                var reservedResidenceIds = await _context.Reservations
                    .Where(res => res.ReservationStartDate <= e && res.ReservationEndDate >= s)
                    .Select(res => res.ResidenceId)
                    .Distinct()
                    .ToListAsync();

                q = q.Where(r => !reservedResidenceIds.Contains(r.ResidenceId));
            }

            var residences = await q.OrderBy(r => r.PricePerNight).ToListAsync();
            var locations = await _context.Locations.OrderBy(l => l.Name).ToListAsync();

            var vm = new AirBnbViewModel
            {
                Residences = residences,
                Locations = locations,
                SelectedLocation = filter.SelectedLocation,
                CheckIn = filter.CheckIn,
                CheckOut = filter.CheckOut,
                Guests = filter.Guests
            };

            var cookie = new AirBnbCookies(new Microsoft.AspNetCore.Http.HttpContextAccessor { HttpContext = HttpContext });
            ViewBag.ReservationCount = cookie.GetReservationCookie().Count();

            return View(vm);
        }

        public IActionResult Support()
        {
            return Content($"Area: {(RouteData.Values["area"] ?? "Public")} | Controller: Home | Action: Support");
        }

        public IActionResult CancellationPolicy()
        {
            return Content($"Area: {(RouteData.Values["area"] ?? "Public")} | Controller: Home | Action: CancellationPolicy");
        }

        public IActionResult TermsAndConditions()
        {
            return Content($"Area: {(RouteData.Values["area"] ?? "Public")} | Controller: Home | Action: TermsAndConditions");
        }

        public IActionResult CookiePolicy()
        {
            return Content($"Area: {(RouteData.Values["area"] ?? "Public")} | Controller: Home | Action: CookiePolicy");
        }
    }
}
