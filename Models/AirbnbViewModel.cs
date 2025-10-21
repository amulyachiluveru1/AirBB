namespace AirBB.Models
{
    public class AirbnbViewModel
    {
        public IEnumerable<Residence> Residences { get; set; }
        public IEnumerable<Location> Locations { get; set; }
        public int? SelectedLocationId { get; set; } 
        public DateTime? RequestedStart { get; set; }
        public DateTime? RequestedEnd { get; set; }
        public int GuestCount { get; set; } = 1;
        public Residence SelectedResidence { get; set; }
        public IEnumerable<Reservation> Reservations { get; set; }
    }

}
