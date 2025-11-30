using AirBB.Models.DataLayer.Configuration;
using AirBB.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AirBB.Models.DataLayer
{
    public class AirBnBContext : DbContext
    {
        public AirBnBContext(DbContextOptions<AirBnBContext> options) : base(options) { }

        public DbSet<Location> Locations { get; set; }
        public DbSet<Residence> Residences { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ConfigureClients());
            modelBuilder.ApplyConfiguration(new ConfigureUsers());
            modelBuilder.ApplyConfiguration(new ConfigureLocations());
            modelBuilder.ApplyConfiguration(new ConfigureResidences());
            modelBuilder.ApplyConfiguration(new ConfigureReservations());
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    // LOCATION SEED
        //    modelBuilder.Entity<Location>().HasData(
        //        new Location { LocationId = 1, Name = "Chicago" },
        //        new Location { LocationId = 2, Name = "New York" },
        //        new Location { LocationId = 3, Name = "Miami" },
        //        new Location { LocationId = 4, Name = "Atlanta" }
        //    );

        //    // RESIDENCE SEED
        //    modelBuilder.Entity<Residence>().HasData(
        //        new Residence { ResidenceId = 101, LocationId = 1, Name = "Chicago Loop Apartment", OwnerId = 201, Accommodation = 3, Bedrooms = 1, Bathrooms = 1, BuiltYear = 2000, PricePerNight = 120, ImageFileName = "chi_loop.jpg" },
        //        new Residence { ResidenceId = 102, LocationId = 2, Name = "NYC Cozy Studio", OwnerId = 202, Accommodation = 2, Bedrooms = 0, Bathrooms = 1, BuiltYear = 2005, PricePerNight = 150, ImageFileName = "nyc_studio.jpg" },
        //        new Residence { ResidenceId = 103, LocationId = 3, Name = "Miami Beach House", OwnerId = 203, Accommodation = 6, Bedrooms = 3, Bathrooms = 2, BuiltYear = 1998, PricePerNight = 320, ImageFileName = "miami_beach.jpg" },
        //        new Residence { ResidenceId = 104, LocationId = 4, Name = "Atlanta Suburban House", OwnerId = 204, Accommodation = 5, Bedrooms = 3, Bathrooms = 2, BuiltYear = 2010, PricePerNight = 180, ImageFileName = "atl_house.jpg" }
        //    );

        //    // USERS SEED
        //    modelBuilder.Entity<User>().HasData(
        //        new User { UserId = 201, Name = "John Owner", Email = "john@mail.com", PhoneNumber = "1111111111", SSN = "111-22-3333", UserType = "Owner" },
        //        new User { UserId = 202, Name = "Emma Owner", Email = "emma@mail.com", PhoneNumber = "2222222222", SSN = "444-55-6666", UserType = "Owner" },
        //        new User { UserId = 203, Name = "Michael Owner", Email = "mike@mail.com", PhoneNumber = "3333333333", SSN = "777-88-9999", UserType = "Owner" }
        //    );

        //    // CLIENT SEED
        //    modelBuilder.Entity<Client>().HasData(
        //        new Client { ClientId = 1, Name = "Client A", Email = "clientA@mail.com", PhoneNumber = "9999999999" },
        //        new Client { ClientId = 2, Name = "Client B", Email = "clientB@mail.com", PhoneNumber = "8888888888" }
        //    );

        //    // RESERVATION SEED
        //    modelBuilder.Entity<Reservation>().HasData(
        //        new Reservation { ReservationId = 1, ResidenceId = 101, ClientUserId = 1, ReservationStartDate = DateTime.Parse("2025-01-10"), ReservationEndDate = DateTime.Parse("2025-01-15") },
        //        new Reservation { ReservationId = 2, ResidenceId = 103, ClientUserId = 2, ReservationStartDate = DateTime.Parse("2025-02-02"), ReservationEndDate = DateTime.Parse("2025-02-05") }
        //    );
        //}

    }
}
