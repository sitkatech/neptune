using Microsoft.EntityFrameworkCore;
using Neptune.Common.GeoSpatial;
using NetTopologySuite.Features;

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

    public string GetGeometry4326GeoJson()
    {
        var attributesTable = new AttributesTable
        {
            { "OnlandVisualTrashAssessmentAreaID", OnlandVisualTrashAssessmentAreaID },
        };

        var feature = new Feature(OnlandVisualTrashAssessmentAreaGeometry4326, attributesTable);
        return GeoJsonSerializer.Serialize(feature);
    }
}