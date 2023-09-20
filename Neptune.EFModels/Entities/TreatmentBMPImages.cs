using Microsoft.EntityFrameworkCore;
using Neptune.Common.DesignByContract;

namespace Neptune.EFModels.Entities;

public static class TreatmentBMPImages
{
    public static List<TreatmentBMPImage> ListByTreatmentBMPID(NeptuneDbContext dbContext, int treatmentBMPID)
    {
        return GetImpl(dbContext).AsNoTracking().Where(x => x.TreatmentBMPID == treatmentBMPID).OrderBy(ht => ht.Caption).ToList();
    }

    public static List<TreatmentBMPImage> ListByTreatmentBMPIDWithChangeTracking(NeptuneDbContext dbContext, int treatmentBMPID)
    {
        return GetImpl(dbContext).Where(x => x.TreatmentBMPID == treatmentBMPID).OrderBy(ht => ht.Caption).ToList();
    }

    public static TreatmentBMPImage GetByIDWithChangeTracking(NeptuneDbContext dbContext, int treatmentBMPImageID)
    {
        var treatmentBMPImage = GetImpl(dbContext)
            .SingleOrDefault(x => x.TreatmentBMPImageID == treatmentBMPImageID);
        Check.RequireNotNull(treatmentBMPImage, $"TreatmentBMPImage with ID {treatmentBMPImageID} not found!");
        return treatmentBMPImage;
    }

    public static TreatmentBMPImage GetByIDWithChangeTracking(NeptuneDbContext dbContext, TreatmentBMPImagePrimaryKey treatmentBMPImagePrimaryKey)
    {
        return GetByIDWithChangeTracking(dbContext, treatmentBMPImagePrimaryKey.PrimaryKeyValue);
    }

    public static TreatmentBMPImage GetByID(NeptuneDbContext dbContext, int treatmentBMPImageID)
    {
        var treatmentBMPImage = GetImpl(dbContext).AsNoTracking()
            .SingleOrDefault(x => x.TreatmentBMPImageID == treatmentBMPImageID);
        Check.RequireNotNull(treatmentBMPImage, $"TreatmentBMPImage with ID {treatmentBMPImageID} not found!");
        return treatmentBMPImage;
    }

    public static TreatmentBMPImage GetByID(NeptuneDbContext dbContext, TreatmentBMPImagePrimaryKey treatmentBMPImagePrimaryKey)
    {
        return GetByID(dbContext, treatmentBMPImagePrimaryKey.PrimaryKeyValue);
    }


    private static IQueryable<TreatmentBMPImage> GetImpl(NeptuneDbContext dbContext)
    {
        return dbContext.TreatmentBMPImages
            .Include(x => x.FileResource);
    }
}