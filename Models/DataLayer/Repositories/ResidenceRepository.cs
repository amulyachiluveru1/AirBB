using AirBB.Models.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace AirBB.Models.DataLayer.Repositories
{
    public class ResidenceRepository : Repository<Residence>, IResidenceRepository
    {
        public ResidenceRepository(AirBnBContext ctx) : base(ctx) { }

    }

}
