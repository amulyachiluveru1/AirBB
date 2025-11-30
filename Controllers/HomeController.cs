using AirBB.Models;
using AirBB.Models.DataLayer;
using AirBB.Models.DataLayer.Repositories;
using AirBB.Models.DomainModels;
using AirBB.Models.ExtensionMethods;
using AirBB.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AirBB.Controllers
{
    public class HomeController : Controller
    {
        private readonly IResidenceRepository _residenceRepo;
        private readonly ILocationRepository _locationRepo;
        private readonly IReservationRepository _reservationRepo;

        public HomeController(IResidenceRepository residenceRepo,
                              ILocationRepository locationRepo,
                              IReservationRepository reservationRepo)
        {
            _residenceRepo = residenceRepo;
            _locationRepo = locationRepo;
            _reservationRepo = reservationRepo;
        }

        public async Task<IActionResult> Index(
            int? selectedLocationId,
            string start = null,
            string end = null,
            int? guests = null,
            bool reset = false)
        {
            var session = new AirBnbSession(HttpContext.Session);

            if (reset)
            {
                session.RemoveReservations();
                session.SetFilters(new AirBnbViewModel());
                return RedirectToAction(nameof(Index));
            }

            var previousFilters = session.GetFilters();

            // Build new filter
            var filter = new AirBnbViewModel
            {
                SelectedLocation = selectedLocationId ?? previousFilters.SelectedLocation,
                CheckIn = !string.IsNullOrEmpty(start) ? DateTime.Parse(start) : previousFilters.CheckIn,
                CheckOut = !string.IsNullOrEmpty(end) ? DateTime.Parse(end) : previousFilters.CheckOut,
                Guests = guests ?? previousFilters.Guests
            };

            session.SetFilters(filter);

            var options = new QueryOptions<Residence>
            {
                Includes = "Location",
                OrderBy = r => (double)r.PricePerNight,
                OrderByDirection = "asc"
            };

            // Build WHERE clause dynamically
            options.Where = r =>
                (!filter.SelectedLocation.HasValue || r.LocationId == filter.SelectedLocation) &&
                (filter.Guests == 0 || r.GuestNumber >= filter.Guests);

            // If date range provided ? remove reserved residences
            if (filter.CheckIn.HasValue && filter.CheckOut.HasValue)
            {
                var reservedIds = await _reservationRepo
                    .GetReservedResidenceIdsAsync(filter.CheckIn.Value.Date,
                                                  filter.CheckOut.Value.Date);

                options.Where = r =>
                    (!filter.SelectedLocation.HasValue || r.LocationId == filter.SelectedLocation) &&
                    (filter.Guests == 0 || r.GuestNumber >= filter.Guests) &&
                    !reservedIds.Contains(r.ResidenceId);
            }

            // Execute query
            var residences = _residenceRepo.List(options).ToList();

            // Locations using QueryOptions
            var locationOptions = new QueryOptions<Location>
            {
                OrderBy = l => l.Name,
                OrderByDirection = "asc"
            };
            var locations = _locationRepo.List(locationOptions);

            // Build ViewModel
            var vm = new AirBnbViewModel
            {
                Residences = residences,
                Locations = locations.ToList(),
                SelectedLocation = filter.SelectedLocation,
                CheckIn = filter.CheckIn,
                CheckOut = filter.CheckOut,
                Guests = filter.Guests
            };

            // Cookie reservations
            var cookie = new AirBnbCookies(new HttpContextAccessor { HttpContext = HttpContext });
            ViewBag.ReservationCount = cookie.GetReservationIds().Count;

            return View(vm);
        }

        //public async Task<IActionResult> Index(int? selectedLocationId, string start = null, string end = null, int? guests = null, bool reset = false)
        //{
        //    var session = new AirBnbSession(HttpContext.Session);

        //    if (reset)
        //    {
        //        session.RemoveReservations();
        //        session.SetFilters(new AirBnbViewModel());
        //        return RedirectToAction(nameof(Index));
        //    }

        //    var sessionFilter = session.GetFilters();

        //    var filter = new AirBnbViewModel
        //    {
        //        SelectedLocation = selectedLocationId ?? sessionFilter.SelectedLocation,
        //        CheckIn = !string.IsNullOrEmpty(start) ? DateTime.Parse(start) : sessionFilter.CheckIn,
        //        CheckOut = !string.IsNullOrEmpty(end) ? DateTime.Parse(end) : sessionFilter.CheckOut,
        //        Guests = guests ?? sessionFilter.Guests
        //    };
        //    session.SetFilters(filter);
        //    var q = _context.Residences.Include(r => r.Location).AsQueryable();

        //    if (filter.SelectedLocation.HasValue)
        //        q = q.Where(r => r.LocationId == filter.SelectedLocation.Value);

        //    if (filter.Guests > 0)
        //        q = q.Where(r => r.GuestNumber >= filter.Guests);

        //    if (filter.CheckIn.HasValue && filter.CheckOut.HasValue)
        //    {
        //        var s = filter.CheckIn.Value.Date;
        //        var e = filter.CheckOut.Value.Date;

        //        var reservedResidenceIds = await _context.Reservations
        //            .Where(res => res.ReservationStartDate <= e && res.ReservationEndDate >= s)
        //            .Select(res => res.ResidenceId)
        //            .Distinct()
        //            .ToListAsync();

        //        q = q.Where(r => !reservedResidenceIds.Contains(r.ResidenceId));
        //    }

        //    var residences = await q.OrderBy(r => (double)r.PricePerNight).ToListAsync();
        //    var locations = await _context.Locations.OrderBy(l => l.Name).ToListAsync();

        //    var vm = new AirBnbViewModel
        //    {
        //        Residences = residences,
        //        Locations = locations,
        //        SelectedLocation = filter.SelectedLocation,
        //        CheckIn = filter.CheckIn,
        //        CheckOut = filter.CheckOut,
        //        Guests = filter.Guests
        //    };

        //    var cookie = new AirBnbCookies(new Microsoft.AspNetCore.Http.HttpContextAccessor { HttpContext = HttpContext });
        //    var reservationIds = cookie.GetReservationIds();
        //    ViewBag.ReservationCount = reservationIds.Count;
        //    return View(vm);

        //}
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
