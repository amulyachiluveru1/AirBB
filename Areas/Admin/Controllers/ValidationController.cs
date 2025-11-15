using AirBB.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AirBB.Areas.Admin.Controllers
{
    public class ValidationController : Controller
    {
        private readonly AirBnBContext _ctx;
        public ValidationController(AirBnBContext ctx) => _ctx = ctx;
        [AcceptVerbs("GET", "POST")]
        public JsonResult CheckOwner(int ownerId)
        {
            var exists = _ctx.Users
                .Any(u => u.UserId == ownerId && u.UserType == "Owner");

            if (exists)
            {
                TempData["okOwner"] = true;
                return Json(true);
            }

            return Json("OwnerId must exist and must be an Owner user.");
        }
    }

}
