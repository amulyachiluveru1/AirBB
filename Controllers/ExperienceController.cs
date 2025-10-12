using Microsoft.AspNetCore.Mvc;

namespace AirBB.Controllers
{
    public class ExperienceController : Controller
    {
        public IActionResult List(string id = "All")
        {
            var area = RouteData.Values["area"] ?? "Public";
            return Content($"Area: {area} | Controller: Experience | Action: List | id = {id}");
        }
    }
}
