using System.ComponentModel.DataAnnotations;

namespace AirBB.Models
{
    public class Client
    {
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime? DOB { get; set; }
    }

}
