using Microsoft.EntityFrameworkCore;

namespace AirBB.Models.DataLayer.Repositories
{
    public interface IReservationRepository
    {
        Task<List<int>> GetReservedResidenceIdsAsync(DateTime start, DateTime end);
    }
    public class ReservationRepository : IReservationRepository
    {
        private readonly AirBnBContext _context;

        public ReservationRepository(AirBnBContext ctx)
        {
            _context = ctx;
        }

        public async Task<List<int>> GetReservedResidenceIdsAsync(DateTime start, DateTime end)
        {
            return await _context.Reservations
                .Where(res =>
                    res.ReservationStartDate <= end &&
                    res.ReservationEndDate >= start)
                .Select(res => res.ResidenceId)
                .Distinct()
                .ToListAsync();
        }
    }

}
