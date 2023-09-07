using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

public static class QuickBMPs
{
    public static List<QuickBMP> GetFullyParameterized(NeptuneDbContext dbContext)
    {
        return dbContext.QuickBMPs.AsNoTracking().Where(x => x.PercentOfSiteTreated != null && x.PercentCaptured != null && x.PercentRetained != null && x.TreatmentBMPType.IsAnalyzedInModelingModule)
            .ToList();
    }
}