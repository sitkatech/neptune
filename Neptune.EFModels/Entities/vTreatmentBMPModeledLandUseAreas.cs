using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;
public static class vTreatmentBMPModeledLandUseAreas
{
    private static IQueryable<vTreatmentBMPModeledLandUseArea> GetImpl(NeptuneDbContext dbContext)
    {
        return dbContext.vTreatmentBMPModeledLandUseAreas.AsNoTracking();
    }
    public static List<vTreatmentBMPModeledLandUseArea> List(NeptuneDbContext dbContext)
    {
        return GetImpl(dbContext).ToList();
    }
}