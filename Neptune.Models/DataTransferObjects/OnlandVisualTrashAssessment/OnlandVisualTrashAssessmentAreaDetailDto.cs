﻿namespace Neptune.Models.DataTransferObjects;

public class OnlandVisualTrashAssessmentAreaDetailDto
{
    public int OnlandVisualTrashAssessmentAreaID { get; set; }
    public string? OnlandVisualTrashAssessmentAreaName { get; set; }
    public int? StormwaterJurisdictionID { get; set; }
    public string? StormwaterJurisdictionName { get; set; }
    public string? OnlandVisualTrashAssessmentBaselineScoreName { get; set; }
    public decimal? OnlandVisualTrashAssessmentBaselineScoreTrashGenerationRate { get; set; }
    public string? AssessmentAreaDescription { get; set; }
    public string? OnlandVisualTrashAssessmentProgressScoreName { get; set; }
    public decimal? OnlandVisualTrashAssessmentProgressScoreTrashGenerationRate { get; set; }
    public DateOnly? LastAssessmentDate { get; set; }
    public BoundingBoxDto BoundingBox { get; set; }
    public string Geometry { get; set; }
}