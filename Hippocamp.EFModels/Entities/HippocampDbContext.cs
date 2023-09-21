using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    public partial class HippocampDbContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
        }
    }
}