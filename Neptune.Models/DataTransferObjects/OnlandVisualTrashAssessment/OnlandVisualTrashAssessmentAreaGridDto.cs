﻿namespace Neptune.Models.DataTransferObjects;

public class OnlandVisualTrashAssessmentAreaGridDto
{
    public int OnlandVisualTrashAssessmentAreaID { get; set; }
    public string? OnlandVisualTrashAssessmentAreaName { get; set; }
    public int? StormwaterJurisdictionID { get; set; }
    public string? StormwaterJurisdictionName { get; set; }
    public string? OnlandVisualTrashAssessmentBaselineScoreName { get; set; }
    public string? AssessmentAreaDescription { get; set; }
    public string? OnlandVisualTrashAssessmentProgressScoreName { get; set; }
    public int NumberOfAssessmentsInProgress { get; set; }
    public int NumberOfAssessmentsCompleted { get; set; }
    public DateOnly? LastAssessmentDate { get; set; }
}