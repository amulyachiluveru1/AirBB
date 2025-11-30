using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace AirBB.Models.DomainModels
{
    public class Location
    {
        public int LocationId { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        public ICollection<Residence> Residences { get; set; }
    }
}
