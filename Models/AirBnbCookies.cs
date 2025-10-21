namespace AirBB.Models
{
    public class AirBnbCookies
    {
        private const string ReservationCookieKey = "ReservationIDs";
        private readonly IHttpContextAccessor _accessor;

        public AirBnbCookies(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public void SetReservationCookie(string reservationIds, int days = 7)
        {
            var options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(days),
                HttpOnly = true,
                IsEssential = true
            };
            _accessor.HttpContext!.Response.Cookies.Append(ReservationCookieKey, reservationIds, options);
        }

        public string? GetReservationCookie()
        {
            _accessor.HttpContext!.Request.Cookies.TryGetValue(ReservationCookieKey, out string? ids);
            return ids;
        }

        public void RemoveReservationCookie()
        {
            _accessor.HttpContext!.Response.Cookies.Delete(ReservationCookieKey);
        }
    }
}
