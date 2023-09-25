using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    public partial class NeptuneDbContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
        }
    }
}