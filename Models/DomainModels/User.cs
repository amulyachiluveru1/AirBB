using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AirBB.Models.DomainModels
{
    public class User
    {
        public int UserId { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [Display(Name = "Phone Number")]
        [Remote("CheckMobile", "Validation")]
        public string? PhoneNumber { get; set; }

        [EmailAddress]
        [Remote("CheckEmail", "Validation")]
        public string? Email { get; set; }
        public bool HasValidContact => !string.IsNullOrEmpty(PhoneNumber) || !string.IsNullOrEmpty(Email);

        [Required, StringLength(20)]
        public string SSN { get; set; } = string.Empty;

        [Required]
        public string UserType { get; set; }
    }
}
