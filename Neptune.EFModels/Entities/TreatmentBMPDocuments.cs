using Microsoft.EntityFrameworkCore;
using Neptune.Common.DesignByContract;

namespace Neptune.EFModels.Entities;

public static class TreatmentBMPDocuments
{
    public static List<TreatmentBMPDocument> ListByTreatmentBMPID(NeptuneDbContext dbContext, int treatmentBMPID)
    {
        return GetImpl(dbContext).AsNoTracking().Where(x => x.TreatmentBMPID == treatmentBMPID).OrderBy(ht => ht.DisplayName).ToList();
    }

    public static TreatmentBMPDocument GetByIDWithChangeTracking(NeptuneDbContext dbContext, int treatmentBMPDocumentID)
    {
        var treatmentBMPDocument = GetImpl(dbContext)
            .SingleOrDefault(x => x.TreatmentBMPDocumentID == treatmentBMPDocumentID);
        Check.RequireNotNull(treatmentBMPDocument, $"TreatmentBMPDocument with ID {treatmentBMPDocumentID} not found!");
        return treatmentBMPDocument;
    }

    public static TreatmentBMPDocument GetByIDWithChangeTracking(NeptuneDbContext dbContext, TreatmentBMPDocumentPrimaryKey treatmentBMPDocumentPrimaryKey)
    {
        return GetByIDWithChangeTracking(dbContext, treatmentBMPDocumentPrimaryKey.PrimaryKeyValue);
    }

    public static TreatmentBMPDocument GetByID(NeptuneDbContext dbContext, int treatmentBMPDocumentID)
    {
        var treatmentBMPDocument = GetImpl(dbContext).AsNoTracking()
            .SingleOrDefault(x => x.TreatmentBMPDocumentID == treatmentBMPDocumentID);
        Check.RequireNotNull(treatmentBMPDocument, $"TreatmentBMPDocument with ID {treatmentBMPDocumentID} not found!");
        return treatmentBMPDocument;
    }

    public static TreatmentBMPDocument GetByID(NeptuneDbContext dbContext, TreatmentBMPDocumentPrimaryKey treatmentBMPDocumentPrimaryKey)
    {
        return GetByID(dbContext, treatmentBMPDocumentPrimaryKey.PrimaryKeyValue);
    }


    private static IQueryable<TreatmentBMPDocument> GetImpl(NeptuneDbContext dbContext)
    {
        return dbContext.TreatmentBMPDocuments
            .Include(x => x.FileResource);
    }
}