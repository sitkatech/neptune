using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities;

public static partial class OnlandVisualTrashAssessmentExtensionMethods
{
    public static OnlandVisualTrashAssessmentGridDto AsGridDto(this OnlandVisualTrashAssessment onlandVisualTrashAssessment)
    {
        var dto = new OnlandVisualTrashAssessmentGridDto()
        {
            OnlandVisualTrashAssessmentID = onlandVisualTrashAssessment.OnlandVisualTrashAssessmentID,
            CreatedByPersonFullName = onlandVisualTrashAssessment.CreatedByPerson.GetFullNameFirstLast(),
            CreatedDate = onlandVisualTrashAssessment.CreatedDate,
            OnlandVisualTrashAssessmentAreaID = onlandVisualTrashAssessment.OnlandVisualTrashAssessmentAreaID,
            OnlandVisualTrashAssessmentAreaName = onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea?.OnlandVisualTrashAssessmentAreaName,
            Notes = onlandVisualTrashAssessment.Notes,
            StormwaterJurisdictionID = onlandVisualTrashAssessment.StormwaterJurisdictionID,
            StormwaterJurisdictionName = onlandVisualTrashAssessment.StormwaterJurisdiction.GetOrganizationDisplayName(),
            OnlandVisualTrashAssessmentStatusName = onlandVisualTrashAssessment.OnlandVisualTrashAssessmentStatus.OnlandVisualTrashAssessmentStatusDisplayName,
            OnlandVisualTrashAssessmentScoreName = onlandVisualTrashAssessment.OnlandVisualTrashAssessmentScore?.OnlandVisualTrashAssessmentScoreDisplayName,
            CompletedDate = onlandVisualTrashAssessment.CompletedDate,
            IsProgressAssessment = onlandVisualTrashAssessment.IsProgressAssessment ? "Progress" : "Baseline"
        };
        return dto;
    }

}