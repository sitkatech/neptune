using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

public partial class OnlandVisualTrashAssessment
{
    public async Task DeleteFull(NeptuneDbContext dbContext)
    {
        await dbContext.OnlandVisualTrashAssessmentObservationPhotos.Include(x => x.OnlandVisualTrashAssessmentObservation).Where(x => x.OnlandVisualTrashAssessmentObservation.OnlandVisualTrashAssessmentID == OnlandVisualTrashAssessmentID).ExecuteDeleteAsync();
        await dbContext.OnlandVisualTrashAssessmentObservations.Where(x => x.OnlandVisualTrashAssessmentID == OnlandVisualTrashAssessmentID).ExecuteDeleteAsync();
        await dbContext.OnlandVisualTrashAssessmentObservationPhotoStagings.Where(x => x.OnlandVisualTrashAssessmentID == OnlandVisualTrashAssessmentID).ExecuteDeleteAsync();
        await dbContext.OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypes.Where(x => x.OnlandVisualTrashAssessmentID == OnlandVisualTrashAssessmentID).ExecuteDeleteAsync();
        await dbContext.OnlandVisualTrashAssessments.Where(x => x.OnlandVisualTrashAssessmentID == OnlandVisualTrashAssessmentID).ExecuteDeleteAsync();
    }
}