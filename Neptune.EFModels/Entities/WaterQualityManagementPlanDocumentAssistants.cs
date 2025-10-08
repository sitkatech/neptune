using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

public static class WaterQualityManagementPlanDocumentAssistants
{
    public static async Task<string> GetByWaterQualityManagementPlanDocumentIDAsDtoAsync(NeptuneDbContext dbContext, int waterQualityManagementPlanDocumentID)
    {
        var entity = await dbContext.WaterQualityManagementPlanDocumentAssistants.AsNoTracking().FirstOrDefaultAsync(x => x.WaterQualityManagementPlanDocumentID == waterQualityManagementPlanDocumentID);
        return entity?.AssistantID;
    }

    public static async Task UpsertAsync(NeptuneDbContext dbContext, int waterQualityManagementPlanDocumentID, string assistantID)
    {
        //assert that assistantID is not null or empty
        if (string.IsNullOrWhiteSpace(assistantID))
        {
            throw new ArgumentException("Assistant ID cannot be null or empty.", nameof(assistantID));
        }

        var docAssistant = await dbContext.WaterQualityManagementPlanDocumentAssistants
            .FirstOrDefaultAsync(x => x.WaterQualityManagementPlanDocumentID == waterQualityManagementPlanDocumentID);

        if (docAssistant != null)
        {
            docAssistant.AssistantID = assistantID;
            dbContext.WaterQualityManagementPlanDocumentAssistants.Update(docAssistant);
        }
        else
        {
            //create one if it doesn't exist
            var newAssistant = new WaterQualityManagementPlanDocumentAssistant
            {
                WaterQualityManagementPlanDocumentID = waterQualityManagementPlanDocumentID,
                AssistantID = assistantID
            };
            await dbContext.WaterQualityManagementPlanDocumentAssistants.AddAsync(newAssistant);
        }

        await dbContext.SaveChangesAsync();
    }

    public static async Task<bool> DeleteAsync(NeptuneDbContext dbContext, int waterQualityManagementPlanDocumentID)
    {
        await dbContext.WaterQualityManagementPlanDocumentAssistants.Where(x => x.WaterQualityManagementPlanDocumentID == waterQualityManagementPlanDocumentID).ExecuteDeleteAsync();
        return true;
    }
}