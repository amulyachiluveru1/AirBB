namespace AirBB.Models
{
    public class AirBnbViewModel
    {
        public List<Residence>? Residences { get; set; }
        public List<Location>? Locations { get; set; }
        public List<Reservation>? Reservations { get; set; }
        public string? SelectedLocation { get; set; }
        public int Guests { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }

        public AirBnbViewModel()
        {
            Residences = new List<Residence>();
            Locations = new List<Location>();
            Reservations = new List<Reservation>();
        }
    }

}
