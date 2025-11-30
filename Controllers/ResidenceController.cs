using AirBB.Models.DataLayer;
using AirBB.Models.DataLayer.Repositories;
using AirBB.Models.DomainModels;
using AirBB.Models.ExtensionMethods;
using AirBB.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AirBB.Controllers
{
    public class ResidenceController : Controller
    {
        private readonly AirBnBContext _context;
        private readonly AirBnbCookies _cookies;
        private readonly IResidenceRepository _residenceRepo;
        private readonly ILocationRepository _locationRepo;
        public ResidenceController(AirBnBContext context, IHttpContextAccessor accessor,IResidenceRepository residenceRepo, ILocationRepository locationRepo)
        {
            _context = context;
            _residenceRepo = residenceRepo;
            _cookies = new AirBnbCookies(accessor);
            _locationRepo = locationRepo;
        }
        [HttpGet]
        public IActionResult Detail(int id)
        {
            var residence = _residenceRepo.Get(id);

            if (residence == null) return NotFound();

            var vm = new AirBnbViewModel
            {
                SelectedResidence = residence,
                Locations = _locationRepo.List(new QueryOptions<Location>
                {
                    OrderBy = l => l.Name
                }).ToList()

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
