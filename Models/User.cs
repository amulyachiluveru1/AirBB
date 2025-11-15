using System.ComponentModel.DataAnnotations;

namespace AirBB.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Phone]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
        public bool HasValidContact => !string.IsNullOrEmpty(PhoneNumber) || !string.IsNullOrEmpty(Email);

        [Required, StringLength(20)]
        public string SSN { get; set; } = string.Empty;

        [Required]
        public string UserType { get; set; }
    }
}
