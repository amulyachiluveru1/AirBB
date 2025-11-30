using AirBB.Models.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace AirBB.Models.DataLayer.Repositories
{
    public interface ILocationRepository : IRepository<Location>
    {
    }
    public class LocationRepository : Repository<Location>, ILocationRepository
    {
        public LocationRepository(AirBnBContext ctx) : base(ctx) { }
    }
}
