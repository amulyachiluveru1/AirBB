using System.ComponentModel.DataAnnotations;

namespace AirBB.Models.DomainModels
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime? DOB { get; set; }
    }

}
