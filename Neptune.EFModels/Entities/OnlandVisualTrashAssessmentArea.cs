using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

public partial class OnlandVisualTrashAssessmentArea
{
    public async Task DeleteFull(NeptuneDbContext dbContext)
    {
        await dbContext.OnlandVisualTrashAssessmentObservationPhotos
            .Include(x => x.OnlandVisualTrashAssessmentObservation).ThenInclude(x => x.OnlandVisualTrashAssessment).Where(x =>
                x.OnlandVisualTrashAssessmentObservation.OnlandVisualTrashAssessment.OnlandVisualTrashAssessmentAreaID == OnlandVisualTrashAssessmentAreaID)
            .ExecuteDeleteAsync();
        await dbContext.OnlandVisualTrashAssessmentObservations.Include(x => x.OnlandVisualTrashAssessment)
            .Where(x => x.OnlandVisualTrashAssessment.OnlandVisualTrashAssessmentAreaID == OnlandVisualTrashAssessmentAreaID).ExecuteDeleteAsync();
        await dbContext.OnlandVisualTrashAssessmentObservationPhotoStagings.Include(x => x.OnlandVisualTrashAssessment)
            .Where(x => x.OnlandVisualTrashAssessment.OnlandVisualTrashAssessmentAreaID == OnlandVisualTrashAssessmentAreaID).ExecuteDeleteAsync();
        await dbContext.OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypes.Include(x => x.OnlandVisualTrashAssessment)
            .Where(x => x.OnlandVisualTrashAssessment.OnlandVisualTrashAssessmentAreaID == OnlandVisualTrashAssessmentAreaID).ExecuteDeleteAsync();
        await dbContext.OnlandVisualTrashAssessments
            .Where(x => x.OnlandVisualTrashAssessmentAreaID == OnlandVisualTrashAssessmentAreaID).ExecuteDeleteAsync();
        await dbContext.OnlandVisualTrashAssessmentAreas
            .Where(x => x.OnlandVisualTrashAssessmentAreaID == OnlandVisualTrashAssessmentAreaID).ExecuteDeleteAsync();
    }
}