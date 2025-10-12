using Microsoft.AspNetCore.Mvc;

namespace AirBB.Controllers
{
    public class ResidenceController : Controller
    {
        public IActionResult List(string id = "All")
        {
            var area = RouteData.Values["area"] ?? "Public";
            return Content($"Area: {area} | Controller: Residence | Action: List | id = {id}");
        }
    }
}
