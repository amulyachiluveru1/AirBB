using AirBB.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirBB.Models.DataLayer.Configuration
{
    public class ConfigureUsers : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.HasData(
                new User { UserId = 1, Name = "Owner One", PhoneNumber = "7001002003", Email = "owner1@mail.com", SSN = "SSN001", UserType = "Owner" },
                new User { UserId = 2, Name = "Owner Two", PhoneNumber = "7001002004", Email = "owner2@mail.com", SSN = "SSN002", UserType = "Owner" },
                new User { UserId = 3, Name = "Client A", PhoneNumber = "7001002005", Email = "clientA@mail.com", SSN = "SSN003", UserType = "Client" },
                new User { UserId = 4, Name = "Client B", PhoneNumber = "7001002006", Email = "clientB@mail.com", SSN = "SSN004", UserType = "Client" },
                new User { UserId = 5, Name = "Client C", PhoneNumber = "7001002007", Email = "clientC@mail.com", SSN = "SSN005", UserType = "Client" }
            );
        }
    }
}
