using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptune.EFModels.Entities
{
    public partial class NeptuneDbContext
    {
        public NeptuneDbContext(string connectionString) : this(GetOptions(connectionString))
        {
        }

        private static DbContextOptions<NeptuneDbContext> GetOptions(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<NeptuneDbContext>();
            optionsBuilder.UseSqlServer(connectionString, x =>
            {
                x.CommandTimeout((int)TimeSpan.FromMinutes(3).TotalSeconds);
                x.UseNetTopologySuite();
            });
            return optionsBuilder.Options;
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
        }

        public virtual DbSet<RegionalSubbasinNetworkResult> RegionalSubbasinNetworkResults { get; set; }
    }
}
