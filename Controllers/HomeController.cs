using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace AirBB.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
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
