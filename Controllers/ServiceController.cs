using Microsoft.AspNetCore.Mvc;

namespace AirBB.Controllers
{
    public class ServiceController : Controller
    {
        public IActionResult List(string id = "All")
        {
            var area = RouteData.Values["area"] ?? "Public";
            return Content($"Area: {area} | Controller: Service | Action: List | id = {id}");
        }
    }
}
