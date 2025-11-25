using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

public static class WaterQualityManagementPlanDocumentVectorStores
{
    public static async Task<string> GetByWaterQualityManagementPlanDocumentIDAsDtoAsync(NeptuneDbContext dbContext, int waterQualityManagementPlanDocumentID)
    {
        var entity = await dbContext.WaterQualityManagementPlanDocumentVectorStores.AsNoTracking().FirstOrDefaultAsync(x => x.WaterQualityManagementPlanDocumentID == waterQualityManagementPlanDocumentID);
        return entity?.OpenAIVectorStoreID;
    }

    public static async Task UpsertAsync(NeptuneDbContext dbContext, int waterQualityManagementPlanDocumentID, string openAIVectorStoreID)
    {
        if (string.IsNullOrWhiteSpace(openAIVectorStoreID))
        {
            throw new ArgumentException("OpenAI Vector Store ID cannot be null or empty.", nameof(openAIVectorStoreID));
        }

        var docVectorStore = await dbContext.WaterQualityManagementPlanDocumentVectorStores
            .FirstOrDefaultAsync(x => x.WaterQualityManagementPlanDocumentID == waterQualityManagementPlanDocumentID);

        if (docVectorStore != null)
        {
            docVectorStore.OpenAIVectorStoreID = openAIVectorStoreID;
            dbContext.WaterQualityManagementPlanDocumentVectorStores.Update(docVectorStore);
        }
        else
        {
            var newVectorStore = new WaterQualityManagementPlanDocumentVectorStore
            {
                WaterQualityManagementPlanDocumentID = waterQualityManagementPlanDocumentID,
                OpenAIVectorStoreID = openAIVectorStoreID
            };
            await dbContext.WaterQualityManagementPlanDocumentVectorStores.AddAsync(newVectorStore);
        }

        await dbContext.SaveChangesAsync();
    }

    public static async Task<bool> DeleteAsync(NeptuneDbContext dbContext, int waterQualityManagementPlanDocumentID)
    {
        await dbContext.WaterQualityManagementPlanDocumentVectorStores.Where(x => x.WaterQualityManagementPlanDocumentID == waterQualityManagementPlanDocumentID).ExecuteDeleteAsync();
        return true;
    }
}