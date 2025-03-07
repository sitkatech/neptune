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
            StormwaterJurisdictionName = onlandVisualTrashAssessment.StormwaterJurisdiction?.GetOrganizationDisplayName(),
            OnlandVisualTrashAssessmentStatusName = onlandVisualTrashAssessment.OnlandVisualTrashAssessmentStatus.OnlandVisualTrashAssessmentStatusDisplayName,
            OnlandVisualTrashAssessmentScoreName = onlandVisualTrashAssessment.OnlandVisualTrashAssessmentScore?.OnlandVisualTrashAssessmentScoreDisplayName,
            CompletedDate = onlandVisualTrashAssessment.CompletedDate,
            IsProgressAssessment = onlandVisualTrashAssessment.IsProgressAssessment ? "Progress" : "Baseline"
        };
        return dto;
    }

    public static OnlandVisualTrashAssessmentDetailDto AsDetailDto(this OnlandVisualTrashAssessment onlandVisualTrashAssessment)
    {
        var dto = new OnlandVisualTrashAssessmentDetailDto()
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
            IsProgressAssessment = onlandVisualTrashAssessment.IsProgressAssessment ? "Progress" : "Baseline",
            PreliminarySourceIdentificationTypeDictionary = onlandVisualTrashAssessment.OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypes.GroupBy(x => x.PreliminarySourceIdentificationType.PreliminarySourceIdentificationCategory.PreliminarySourceIdentificationCategoryDisplayName).ToDictionary(x => x.Key.ToString(), x => x.Select(x => x.PreliminarySourceIdentificationType.PreliminarySourceIdentificationTypeDisplayName).ToList()),
            Observations = onlandVisualTrashAssessment.OnlandVisualTrashAssessmentObservations.Select(x => x.AsPhotoDto()).ToList(),
            BoundingBox = new BoundingBoxDto(onlandVisualTrashAssessment.GetOnlandVisualTrashAssessmentGeometry()),
        };
        return dto;
    }

    public static OnlandVisualTrashAssessmentWorkflowDto AsWorkflowDto(this OnlandVisualTrashAssessment onlandVisualTrashAssessment)
    {
        var dto = new OnlandVisualTrashAssessmentWorkflowDto()
        {
            OnlandVisualTrashAssessmentID = onlandVisualTrashAssessment.OnlandVisualTrashAssessmentID,
            OnlandVisualTrashAssessmentAreaID = onlandVisualTrashAssessment.OnlandVisualTrashAssessmentAreaID,
            OnlandVisualTrashAssessmentAreaName = onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea?.OnlandVisualTrashAssessmentAreaName,
            Notes = onlandVisualTrashAssessment.Notes,
            StormwaterJurisdictionID = onlandVisualTrashAssessment.StormwaterJurisdictionID,
            StormwaterJurisdictionName = onlandVisualTrashAssessment.StormwaterJurisdiction.GetOrganizationDisplayName(), 
            OnlandVisualTrashAssessmentBaselineScoreID = onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentBaselineScoreID, 
            AssessmentAreaDescription = onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea.AssessmentAreaDescription, 
            IsProgressAssessment = onlandVisualTrashAssessment.IsProgressAssessment, 
            LastAssessmentDate = onlandVisualTrashAssessment.CompletedDate,
            PreliminarySourceIdentificationTypeWorkflowDtos = onlandVisualTrashAssessment.GetPreliminarySourceIdentificationTypeWorkflowDtosForOnlandVisualTrashAssessments(),
            Observations = onlandVisualTrashAssessment.OnlandVisualTrashAssessmentObservations.Select(x => x.AsPhotoDto()).ToList(),
            BoundingBox = new BoundingBoxDto(onlandVisualTrashAssessment.GetOnlandVisualTrashAssessmentGeometry()),
        };
        return dto;
    }

}