using AirBB.Models.DataLayer;

namespace AirBB.Models
{
    public static class Check
    {
        public static string EmailExists(AirBnBContext ctx, string email)
        {
            string msg = string.Empty;
            if (!string.IsNullOrEmpty(email))
            {
                var customer = ctx.Users.FirstOrDefault(
                    c => c.Email.ToLower() == email.ToLower());
                if (customer != null)
                    msg = $"Email address {email} already in use.";
            }
            return msg;
        }
        public static string MobileExists(AirBnBContext ctx, string mobile)
        {
            string msg = string.Empty;
            if (!string.IsNullOrEmpty(mobile))
            {
                var customer = ctx.Users.FirstOrDefault(
                    c => c.PhoneNumber.ToLower() == mobile.ToLower());
                if (customer != null)
                    msg = $"Mobile Number {mobile} already in use.";
            }
            return msg;
        }
    }
}
