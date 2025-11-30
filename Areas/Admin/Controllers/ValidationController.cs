using AirBB.Models;
using AirBB.Models.DataLayer;
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
        public JsonResult CheckEmail(string Email)
        {
            string msg = Check.EmailExists(_ctx, Email);
            if (string.IsNullOrEmpty(msg))
            {
                TempData["okEmail"] = true;
                return Json(true);
            }
            else return Json(msg);
        }

        public JsonResult CheckMobile(string PhoneNumber)
        {
            string msg = Check.MobileExists(_ctx, PhoneNumber);
            if (string.IsNullOrEmpty(msg))
            {
                TempData["okEmail"] = true;
                return Json(true);
            }
            else return Json(msg);
        }
    }


}
