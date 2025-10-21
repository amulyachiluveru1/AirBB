namespace AirBB.Models
{
    public class Residence
    {
        public int ResidenceId { get; set; }
        public string Name { get; set; }
        public string ResidencePicture { get; set; } 
        public int LocationId { get; set; }
        public Location Location { get; set; }

        public int GuestNumber { get; set; }
        public int BedroomNumber { get; set; }
        public int BathroomNumber { get; set; }
        public decimal PricePerNight { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
    }

}
