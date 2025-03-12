﻿namespace Neptune.Models.DataTransferObjects;

public class OnlandVisualTrashAssessmentReviewAndFinalizeDto
{
    public int OnlandVisualTrashAssessmentID { get; set; }
    public int? OnlandVisualTrashAssessmentAreaID { get; set; }
    public string? OnlandVisualTrashAssessmentAreaName { get; set; }
    public int? StormwaterJurisdictionID { get; set; }
    public int? OnlandVisualTrashAssessmentBaselineScoreID { get; set; }
    public int OnlandVisualTrashAssessmentStatusID { get; set; }
    public string? AssessmentAreaDescription { get; set; }
    public bool? IsProgressAssessment { get; set; }
    public string? Notes { get; set; }
    public DateTime? AssessmentDate { get; set; }
    public List<OnlandVisualTrashAssessmentObservationWithPhotoDto> Observations { get; set; }
    public List<int> PreliminarySourceIdentificationTypeIDs { get; set; }
    public BoundingBoxDto BoundingBox { get; set; }
    public string Geometry { get; set; }
}