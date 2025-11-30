using AirBB.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirBB.Models.DataLayer.Configuration
{
    public class ConfigureClients : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> entity)
        {
            entity.HasData(
                new Client { ClientId = 1, Name = "John Doe", PhoneNumber = "1234567890", Email = "john@example.com", DOB = new DateTime(1990, 3, 10) },
                new Client { ClientId = 2, Name = "Emma Watson", PhoneNumber = "9876543210", Email = "emma@example.com", DOB = new DateTime(1988, 6, 1) },
                new Client { ClientId = 3, Name = "Chris Evans", PhoneNumber = "7778889999", Email = "chris@example.com", DOB = new DateTime(1985, 11, 20) },
                new Client { ClientId = 4, Name = "Mia Smith", PhoneNumber = "5551237890", Email = "mia@example.com", DOB = new DateTime(1993, 2, 14) },
                new Client { ClientId = 5, Name = "Liam Brown", PhoneNumber = "4442221111", Email = "liam@example.com", DOB = new DateTime(1995, 9, 7) }
            );
        }
    }
}
