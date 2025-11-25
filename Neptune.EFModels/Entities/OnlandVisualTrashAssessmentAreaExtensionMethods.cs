using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities;

public static class OnlandVisualTrashAssessmentAreaExtensionMethods
{
    public static OnlandVisualTrashAssessmentAreaSimpleDto AsSimpleDto(this OnlandVisualTrashAssessmentArea onlandVisualTrashAssessmentArea)
    {
        var dto = new OnlandVisualTrashAssessmentAreaSimpleDto()
        {
            OnlandVisualTrashAssessmentAreaID = onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaID,
            OnlandVisualTrashAssessmentAreaName = onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaName,
            StormwaterJurisdictionID = onlandVisualTrashAssessmentArea.StormwaterJurisdictionID,
            OnlandVisualTrashAssessmentBaselineScoreID = onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentBaselineScoreID,
            AssessmentAreaDescription = onlandVisualTrashAssessmentArea.AssessmentAreaDescription,
            OnlandVisualTrashAssessmentProgressScoreID = onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentProgressScoreID
        };
        return dto;
    }

    public static OnlandVisualTrashAssessmentAreaGridDto AsGridDto(this OnlandVisualTrashAssessmentArea onlandVisualTrashAssessmentArea)
    {
        var dto = new OnlandVisualTrashAssessmentAreaGridDto()
        {
            OnlandVisualTrashAssessmentAreaID = onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaID,
            OnlandVisualTrashAssessmentAreaName = onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaName,
            AssessmentAreaDescription = onlandVisualTrashAssessmentArea.AssessmentAreaDescription,
            StormwaterJurisdictionID = onlandVisualTrashAssessmentArea.StormwaterJurisdictionID,
            StormwaterJurisdictionName = onlandVisualTrashAssessmentArea.StormwaterJurisdiction.GetOrganizationDisplayName(),
            OnlandVisualTrashAssessmentBaselineScoreName = onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentBaselineScore?.OnlandVisualTrashAssessmentScoreDisplayName,
            OnlandVisualTrashAssessmentProgressScoreName = onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentProgressScore?.OnlandVisualTrashAssessmentScoreDisplayName,
            NumberOfAssessmentsInProgress = onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessments.Count(x => x.OnlandVisualTrashAssessmentStatusID == (int) OnlandVisualTrashAssessmentStatusEnum.InProgress),
            NumberOfAssessmentsCompleted = onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessments.Count(x => x.OnlandVisualTrashAssessmentStatusID == (int) OnlandVisualTrashAssessmentStatusEnum.Complete),
            LastAssessmentDate = onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessments.Select(x => x.CompletedDate).Max()
        };
        return dto;
    }

    public static OnlandVisualTrashAssessmentAreaDetailDto AsDetailDto(this OnlandVisualTrashAssessmentArea onlandVisualTrashAssessmentArea)
    {
        var dto = new OnlandVisualTrashAssessmentAreaDetailDto()
        {
            OnlandVisualTrashAssessmentAreaID = onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaID,
            OnlandVisualTrashAssessmentAreaName = onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaName,
            AssessmentAreaDescription = onlandVisualTrashAssessmentArea.AssessmentAreaDescription,
            StormwaterJurisdictionID = onlandVisualTrashAssessmentArea.StormwaterJurisdictionID,
            StormwaterJurisdictionName = onlandVisualTrashAssessmentArea.StormwaterJurisdiction.GetOrganizationDisplayName(),
            OnlandVisualTrashAssessmentBaselineScoreName = onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentBaselineScore?.OnlandVisualTrashAssessmentScoreDisplayName,
            OnlandVisualTrashAssessmentBaselineScoreTrashGenerationRate= onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentBaselineScore?.TrashGenerationRate,
            OnlandVisualTrashAssessmentProgressScoreName = onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentProgressScore?.OnlandVisualTrashAssessmentScoreDisplayName,
            OnlandVisualTrashAssessmentProgressScoreTrashGenerationRate= onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentProgressScore?.TrashGenerationRate,
            LastAssessmentDate = onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessments.Where(x => x.OnlandVisualTrashAssessmentStatusID == (int)OnlandVisualTrashAssessmentStatusEnum.Complete).Select(x => x.CompletedDate).Max(),
            CompletedBaselineAssessmentCount = onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessments.Count(x => x.OnlandVisualTrashAssessmentStatusID == (int)OnlandVisualTrashAssessmentStatusEnum.Complete && !x.IsProgressAssessment),
            CompletedProgressAssessmentCount = onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessments.Count(x => x.OnlandVisualTrashAssessmentStatusID == (int)OnlandVisualTrashAssessmentStatusEnum.Complete && x.IsProgressAssessment),
            BoundingBox = new BoundingBoxDto(onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaGeometry4326),
            Geometry = onlandVisualTrashAssessmentArea.GetGeometry4326GeoJson()
        };
        return dto;
    }

}