using System.Security.Policy;

namespace AirBB.Models
{
    public class Location
    {
        public int LocationId { get; set; }
        public string Name { get; set; }

        public ICollection<Residence> Residences { get; set; }
    }
}
