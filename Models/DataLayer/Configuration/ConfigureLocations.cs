using AirBB.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirBB.Models.DataLayer.Configuration
{
    public class ConfigureLocations : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> entity)
        {
            entity.HasData(
                new Location { LocationId = 1, Name = "New York" },
                new Location { LocationId = 2, Name = "Los Angeles" },
                new Location { LocationId = 3, Name = "Chicago" },
                new Location { LocationId = 4, Name = "Boston" },
                new Location { LocationId = 5, Name = "Miami" }
            );
        }
    }
}
