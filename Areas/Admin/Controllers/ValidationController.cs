using AirBB.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AirBB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ValidationController : Controller
    {
        private readonly AirBnBContext _ctx;
        public ValidationController(AirBnBContext ctx) => _ctx = ctx;

        [AcceptVerbs("GET", "POST")]
        public JsonResult CheckOwner(int OwnerId)
        {
            var exists = _ctx.Users
                .Any(u => u.UserId == OwnerId && u.UserType == "Owner");

            if (exists)
            {
                return Json(true); // valid
            }

            return Json("OwnerId must exist in Users table and must have UserType 'Owner'.");
        }
    }


}
