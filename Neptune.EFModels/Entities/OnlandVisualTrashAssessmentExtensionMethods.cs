﻿using Neptune.Models.DataTransferObjects;

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
        };
        return dto;
    }

    public static OnlandVisualTrashAssessmentAddRemoveParcelsDto AsAddRemoveParcelDto(this OnlandVisualTrashAssessment onlandVisualTrashAssessment)
    {
        var dto = new OnlandVisualTrashAssessmentAddRemoveParcelsDto()
        {
            OnlandVisualTrashAssessmentID = onlandVisualTrashAssessment.OnlandVisualTrashAssessmentID,
            OnlandVisualTrashAssessmentAreaID = onlandVisualTrashAssessment.OnlandVisualTrashAssessmentAreaID,
            StormwaterJurisdictionID = onlandVisualTrashAssessment.StormwaterJurisdictionID,
            IsDraftGeometryManuallyRefined = onlandVisualTrashAssessment.IsDraftGeometryManuallyRefined ?? false,
            BoundingBox = new BoundingBoxDto(onlandVisualTrashAssessment.OnlandVisualTrashAssessmentObservations.Select(x => x.LocationPoint4326)),
            TransectLineAsGeoJson = OnlandVisualTrashAssessments.GetTransectLine4326GeoJson(onlandVisualTrashAssessment)
        };
        return dto;
    }

    public static OnlandVisualTrashAssessmentRefineAreaDto AsRefineAreaDto(this OnlandVisualTrashAssessment onlandVisualTrashAssessment)
    {
        var dto = new OnlandVisualTrashAssessmentRefineAreaDto()
        {
            OnlandVisualTrashAssessmentID = onlandVisualTrashAssessment.OnlandVisualTrashAssessmentID,
            OnlandVisualTrashAssessmentAreaID = onlandVisualTrashAssessment.OnlandVisualTrashAssessmentAreaID,
            BoundingBox = new BoundingBoxDto(onlandVisualTrashAssessment.GetOnlandVisualTrashAssessmentGeometry()),
            TransectLineAsGeoJson = OnlandVisualTrashAssessments.GetTransectLine4326GeoJson(onlandVisualTrashAssessment),
            GeometryAsGeoJson = onlandVisualTrashAssessment.GetGeometry4326GeoJson()
        };
        return dto;
    }

    public static OnlandVisualTrashAssessmentReviewAndFinalizeDto AsReviewAndFinalizeDto(this OnlandVisualTrashAssessment onlandVisualTrashAssessment)
    {
        var dto = new OnlandVisualTrashAssessmentReviewAndFinalizeDto()
        {
            OnlandVisualTrashAssessmentID = onlandVisualTrashAssessment.OnlandVisualTrashAssessmentID,
            OnlandVisualTrashAssessmentAreaID = onlandVisualTrashAssessment.OnlandVisualTrashAssessmentAreaID,
            OnlandVisualTrashAssessmentAreaName = onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea != null ? onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaName : onlandVisualTrashAssessment.DraftAreaName,
            Notes = onlandVisualTrashAssessment.Notes,
            StormwaterJurisdictionID = onlandVisualTrashAssessment.StormwaterJurisdictionID,
            OnlandVisualTrashAssessmentScoreID = onlandVisualTrashAssessment.OnlandVisualTrashAssessmentScoreID,
            AssessmentAreaDescription = onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea != null ? onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea.AssessmentAreaDescription : onlandVisualTrashAssessment.DraftAreaDescription,
            AssessingNewArea = onlandVisualTrashAssessment.AssessingNewArea ?? false,
            IsProgressAssessment = onlandVisualTrashAssessment.IsProgressAssessment,
            AssessmentDate = DateTime.UtcNow,
            OnlandVisualTrashAssessmentStatusID = (int)OnlandVisualTrashAssessmentStatusEnum.InProgress,
            PreliminarySourceIdentificationTypeIDs = onlandVisualTrashAssessment.OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypes.Select(x => x.PreliminarySourceIdentificationTypeID).ToList(),
        };
        return dto;
    }

}