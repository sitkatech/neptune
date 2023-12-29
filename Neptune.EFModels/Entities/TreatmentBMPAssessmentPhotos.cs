using Microsoft.EntityFrameworkCore;
using Neptune.Common.DesignByContract;

namespace Neptune.EFModels.Entities;

public static class TreatmentBMPAssessmentPhotos
{
    public static List<TreatmentBMPAssessmentPhoto> ListByTreatmentBMPAssessmentID(NeptuneDbContext dbContext, int treatmentBMPAssessmentID)
    {
        return GetImpl(dbContext).AsNoTracking().Where(x => x.TreatmentBMPAssessmentID == treatmentBMPAssessmentID).OrderBy(ht => ht.Caption).ToList();
    }

    public static List<TreatmentBMPAssessmentPhoto> ListByTreatmentBMPAssessmentIDWithChangeTracking(NeptuneDbContext dbContext, int treatmentBMPAssessmentID)
    {
        return GetImpl(dbContext).Where(x => x.TreatmentBMPAssessmentID == treatmentBMPAssessmentID).OrderBy(ht => ht.Caption).ToList();
    }

    public static TreatmentBMPAssessmentPhoto GetByIDWithChangeTracking(NeptuneDbContext dbContext, int treatmentBMPAssessmentPhotoID)
    {
        var treatmentBMPAssessmentPhoto = GetImpl(dbContext)
            .SingleOrDefault(x => x.TreatmentBMPAssessmentPhotoID == treatmentBMPAssessmentPhotoID);
        Check.RequireNotNull(treatmentBMPAssessmentPhoto, $"TreatmentBMPAssessmentPhoto with ID {treatmentBMPAssessmentPhotoID} not found!");
        return treatmentBMPAssessmentPhoto;
    }

    public static TreatmentBMPAssessmentPhoto GetByIDWithChangeTracking(NeptuneDbContext dbContext, TreatmentBMPAssessmentPhotoPrimaryKey treatmentBMPAssessmentPhotoPrimaryKey)
    {
        return GetByIDWithChangeTracking(dbContext, treatmentBMPAssessmentPhotoPrimaryKey.PrimaryKeyValue);
    }

    public static TreatmentBMPAssessmentPhoto GetByID(NeptuneDbContext dbContext, int treatmentBMPAssessmentPhotoID)
    {
        var treatmentBMPAssessmentPhoto = GetImpl(dbContext).AsNoTracking()
            .SingleOrDefault(x => x.TreatmentBMPAssessmentPhotoID == treatmentBMPAssessmentPhotoID);
        Check.RequireNotNull(treatmentBMPAssessmentPhoto, $"TreatmentBMPAssessmentPhoto with ID {treatmentBMPAssessmentPhotoID} not found!");
        return treatmentBMPAssessmentPhoto;
    }

    public static TreatmentBMPAssessmentPhoto GetByID(NeptuneDbContext dbContext, TreatmentBMPAssessmentPhotoPrimaryKey treatmentBMPAssessmentPhotoPrimaryKey)
    {
        return GetByID(dbContext, treatmentBMPAssessmentPhotoPrimaryKey.PrimaryKeyValue);
    }


    private static IQueryable<TreatmentBMPAssessmentPhoto> GetImpl(NeptuneDbContext dbContext)
    {
        return dbContext.TreatmentBMPAssessmentPhotos
            .Include(x => x.FileResource);
    }
}