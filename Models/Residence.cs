using Airbnb.Models.Validation;
using AirbnbProject.Models.Validation;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AirBB.Models
{
    public class Residence
    {
        public int ResidenceId { get; set; }

        [Required]
        [Display(Name = "Location")]
        public int LocationId { get; set; }
        public Location? Location { get; set; }
        [Required]
        [StringLength(50)]
        [RegularExpression("^[a-zA-Z0-9 ]+$",ErrorMessage = "Name must be alphanumeric only.")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [Remote(action: "CheckOwner", controller: "Validation")]
        [Display(Name = "Owner Id")]
        public int OwnerId { get; set; }
        public User? Owner { get; set; }
        [Required]
        [Range(1, 1000, ErrorMessage = "Accommodation must be a positive integer.")]
        public int Accommodation { get; set; }

        [Required]
        [Range(0, 1000, ErrorMessage = "Bedrooms must be an integer.")]
        public int Bedrooms { get; set; }

        [Required]
        [Bathrooms(ErrorMessage = "Bathrooms must be an integer or end with .5 (e.g., 1.5).")]
        public decimal Bathrooms { get; set; }

        [Required]
        [BuiltYear(150, ErrorMessage = "Built year must be in the past and not older than 150 years.")]
        [Display(Name = "Built Year")]
        public int BuiltYear { get; set; }

        public string? ImageFileName { get; set; }

        public int GuestNumber { get; set; }
        public int BedroomNumber { get; set; }
        public int BathroomNumber { get; set; }
        [Required]
        [Range(0, 9999999, ErrorMessage = "Price must be numeric.")]
        public decimal PricePerNight { get; set; }

        public ICollection<Reservation>? Reservations { get; set; }
    }

}
