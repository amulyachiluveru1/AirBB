using AirBB.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirBB.Models.DataLayer.Configuration
{
    public class ConfigureResidences : IEntityTypeConfiguration<Residence>
    {
        public void Configure(EntityTypeBuilder<Residence> entity)
        {
            entity.HasData(
                new Residence
                {
                    ResidenceId = 1,
                    LocationId = 1,
                    OwnerId = 1,
                    Name = "Central Apartment",
                    Accommodation = 4,
                    Bedrooms = 2,
                    Bathrooms = 1.5m,
                    BuiltYear = 2010,
                    PricePerNight = 120,
                    GuestNumber =5,
                    ImageFileName= "atl_house.jpg"
                },
                new Residence
                {
                    ResidenceId = 2,
                    LocationId = 2,
                    OwnerId = 2,
                    Name = "Ocean View House",
                    Accommodation = 6,
                    Bedrooms = 3,
                    Bathrooms = 2m,
                    BuiltYear = 2015,
                    PricePerNight = 200,
                    GuestNumber = 3,
                    ImageFileName = "chi_loop.jpg"
                },
                new Residence
                {
                    ResidenceId = 3,
                    LocationId = 3,
                    OwnerId = 1,
                    Name = "City Studio",
                    Accommodation = 2,
                    Bedrooms = 1,
                    Bathrooms = 1m,
                    BuiltYear = 2020,
                    PricePerNight = 90,
                    GuestNumber = 10,
                    ImageFileName = "nyc_studio.jpg"
                },
                new Residence
                {
                    ResidenceId = 4,
                    LocationId = 4,
                    OwnerId = 2,
                    Name = "Modern Condo",
                    Accommodation = 5,
                    Bedrooms = 2,
                    Bathrooms = 1.5m,
                    BuiltYear = 2018,
                    PricePerNight = 150,
                    GuestNumber = 1,
                    ImageFileName = "miami_beach.jpg"
                }
            );
        }
    }
}
