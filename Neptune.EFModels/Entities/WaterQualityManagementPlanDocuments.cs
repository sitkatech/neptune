using Microsoft.EntityFrameworkCore;
using Neptune.Common.DesignByContract;

namespace Neptune.EFModels.Entities;

public static class WaterQualityManagementPlanDocuments
{
    public static List<WaterQualityManagementPlanDocument> ListByWaterQualityManagementPlanID(NeptuneDbContext dbContext, int waterQualityManagementPlanID)
    {
        return GetImpl(dbContext).AsNoTracking().Where(x => x.WaterQualityManagementPlanID == waterQualityManagementPlanID).OrderBy(ht => ht.DisplayName).ToList();
    }

    public static WaterQualityManagementPlanDocument GetByIDWithChangeTracking(NeptuneDbContext dbContext, int waterQualityManagementPlanDocumentID)
    {
        var waterQualityManagementPlanDocument = GetImpl(dbContext)
            .SingleOrDefault(x => x.WaterQualityManagementPlanDocumentID == waterQualityManagementPlanDocumentID);
        Check.RequireNotNull(waterQualityManagementPlanDocument, $"WaterQualityManagementPlanDocument with ID {waterQualityManagementPlanDocumentID} not found!");
        return waterQualityManagementPlanDocument;
    }

    public static WaterQualityManagementPlanDocument GetByIDWithChangeTracking(NeptuneDbContext dbContext, WaterQualityManagementPlanDocumentPrimaryKey waterQualityManagementPlanDocumentPrimaryKey)
    {
        return GetByIDWithChangeTracking(dbContext, waterQualityManagementPlanDocumentPrimaryKey.PrimaryKeyValue);
    }

    public static WaterQualityManagementPlanDocument GetByID(NeptuneDbContext dbContext, int waterQualityManagementPlanDocumentID)
    {
        var waterQualityManagementPlanDocument = GetImpl(dbContext).AsNoTracking()
            .SingleOrDefault(x => x.WaterQualityManagementPlanDocumentID == waterQualityManagementPlanDocumentID);
        Check.RequireNotNull(waterQualityManagementPlanDocument, $"WaterQualityManagementPlanDocument with ID {waterQualityManagementPlanDocumentID} not found!");
        return waterQualityManagementPlanDocument;
    }

    public static WaterQualityManagementPlanDocument GetByID(NeptuneDbContext dbContext, WaterQualityManagementPlanDocumentPrimaryKey waterQualityManagementPlanDocumentPrimaryKey)
    {
        return GetByID(dbContext, waterQualityManagementPlanDocumentPrimaryKey.PrimaryKeyValue);
    }


    private static IQueryable<WaterQualityManagementPlanDocument> GetImpl(NeptuneDbContext dbContext)
    {
        return dbContext.WaterQualityManagementPlanDocuments
            .Include(x => x.FileResource);
    }
}