using NuGet.Packaging.Signing;
using System.Text.Json;
namespace AirBB.Models.ExtensionMethods
{
    public class AirBnbCookies
    {
        private const string ReservationCookieKey = "ReservationIDs";
        private readonly IHttpContextAccessor _accessor;

        public AirBnbCookies(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public void SetReservationCookie(List<int> reservationIds, int days = 7)
        {
            var options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(days),
                HttpOnly = true,
                IsEssential = true
            };
            var json = JsonSerializer.Serialize(reservationIds.Distinct().ToList());
            _accessor.HttpContext!.Response.Cookies.Append(ReservationCookieKey, json, options);
        }

        public string? GetReservationCookie()
        {
            _accessor.HttpContext!.Request.Cookies.TryGetValue(ReservationCookieKey, out string? ids);
            return ids;
        }
        public List<int> GetReservationIds()
        {
            var req = _accessor.HttpContext!.Request;
            if (req.Cookies.TryGetValue(ReservationCookieKey, out var json))
            {
                try
                {
                    return JsonSerializer.Deserialize<List<int>>(json) ?? new List<int>();
                }
                catch
                {
                    return new List<int>();
                }
            }
            return new List<int>();
        }

        public void AddReservationId(int id)
        {
            var ids = GetReservationIds();
            if (!ids.Contains(id))
                ids.Add(id);
            SetReservationCookie(ids);
        }

        public void RemoveReservationId(int id)
        {
            var ids = GetReservationIds();
            if (ids.Contains(id))
            {
                ids.Remove(id);
                SetReservationCookie(ids);
            }
        }

        public void RemoveReservationCookie()
        {
            _accessor.HttpContext!.Response.Cookies.Delete(ReservationCookieKey);
        }
    }
}
