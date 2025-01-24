using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities;

public static partial class OnlandVisualTrashAssessmentAreaExtensionMethods
{
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
            NumberOfAssessmentsCompleted = onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessments.Count,
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
            OnlandVisualTrashAssessmentProgressScoreName = onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentProgressScore?.OnlandVisualTrashAssessmentScoreDisplayName,
            NumberOfAssessmentsCompleted = onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessments.Count,
            LastAssessmentDate = onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessments.Select(x => x.CompletedDate).Max(),
            OnlandVisualTrashAssessments = onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessments.Select(x => x.AsGridDto()).ToList()
        };
        return dto;
    }

}