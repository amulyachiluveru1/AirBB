using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AirBB.Models
{
    public class AirBnBContext : DbContext
    {
        public AirBnBContext(DbContextOptions<AirBnBContext> options) : base(options) { }

        public DbSet<Location> Locations { get; set; }
        public DbSet<Residence> Residences { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Location>().HasData(
                new Location { LocationId = 1, Name = "Chicago" },
                new Location { LocationId = 2, Name = "New York" },
                new Location { LocationId = 3, Name = "Miami" },
                new Location { LocationId = 4, Name = "Atlanta" }
            );
            modelBuilder.Entity<Residence>().HasData(
                new Residence { ResidenceId = 101, Name = "Chicago Loop Apartment", ResidencePicture = "chi_loop.jpg", LocationId = 1, GuestNumber = 3, BedroomNumber = 1, BathroomNumber = 1, PricePerNight = 120m },
                new Residence { ResidenceId = 102, Name = "NYC Cozy Studio", ResidencePicture = "nyc_studio.jpg", LocationId = 2, GuestNumber = 2, BedroomNumber = 0, BathroomNumber = 1, PricePerNight = 150m },
                new Residence { ResidenceId = 103, Name = "Miami Beach House", ResidencePicture = "miami_beach.jpg", LocationId = 3, GuestNumber = 6, BedroomNumber = 3, BathroomNumber = 2, PricePerNight = 320m },
                new Residence { ResidenceId = 104, Name = "Atlanta Suburban Single Family House", ResidencePicture = "atl_house.jpg", LocationId = 4, GuestNumber = 5, BedroomNumber = 3, BathroomNumber = 2, PricePerNight = 180m }
            );
        }
    }
}
