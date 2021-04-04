using Microsoft.EntityFrameworkCore;
using Mobility.Web.DAL.Entities;

namespace Mobility.Web.DAL
{
    public class MobilityDbContext : DbContext
    {
        public MobilityDbContext(DbContextOptions<MobilityDbContext> options)
            : base(options)
        {
        }

        public DbSet<ScooterLocation> ScooterLocations { get; set; }
    }

}
