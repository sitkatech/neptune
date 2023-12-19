using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities;

public partial class OnlandVisualTrashAssessment
{
    public async Task DeleteFull(NeptuneDbContext dbContext)
    {
        await dbContext.OnlandVisualTrashAssessmentObservations.Where(x => x.OnlandVisualTrashAssessmentID == OnlandVisualTrashAssessmentID).ExecuteDeleteAsync();
        await dbContext.OnlandVisualTrashAssessmentObservationPhotoStagings.Where(x => x.OnlandVisualTrashAssessmentID == OnlandVisualTrashAssessmentID).ExecuteDeleteAsync();
        await dbContext.OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypes.Where(x => x.OnlandVisualTrashAssessmentID == OnlandVisualTrashAssessmentID).ExecuteDeleteAsync();
    }

    public Geometry? GetOnlandVisualTrashAssessmentGeometry()
    {
        return OnlandVisualTrashAssessmentArea?.OnlandVisualTrashAssessmentAreaGeometry4326 ?? DraftGeometry;
    }
}