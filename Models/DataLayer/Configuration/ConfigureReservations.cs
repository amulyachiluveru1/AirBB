using AirBB.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirBB.Models.DataLayer.Configuration
{
    public class ConfigureReservations : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> entity)
        {
            entity.HasData(
                new Reservation { ReservationId = 1, ResidenceId = 1, ClientUserId = 3, ReservationStartDate = new DateTime(2025, 01, 10), ReservationEndDate = new DateTime(2025, 01, 15) },
                new Reservation { ReservationId = 2, ResidenceId = 2, ClientUserId = 4, ReservationStartDate = new DateTime(2025, 02, 05), ReservationEndDate = new DateTime(2025, 02, 12) },
                new Reservation { ReservationId = 3, ResidenceId = 3, ClientUserId = 5, ReservationStartDate = new DateTime(2025, 03, 18), ReservationEndDate = new DateTime(2025, 03, 20) }
            );
        }
    }
}
