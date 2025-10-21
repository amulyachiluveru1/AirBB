namespace AirBB.Models
{
    public class AirBnbViewModel
    {
        public List<Residence>? Residences { get; set; } = new();
        public List<Location>? Locations { get; set; }=new();
        public List<Reservation>? Reservations { get; set; } = new();
        public int? SelectedLocation { get; set; }
        public int Guests { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public Residence? SelectedResidence { get; set; } = new();

    }

}
