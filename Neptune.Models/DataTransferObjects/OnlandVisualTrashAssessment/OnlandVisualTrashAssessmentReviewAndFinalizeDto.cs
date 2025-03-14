﻿using System.ComponentModel.DataAnnotations;

namespace Neptune.Models.DataTransferObjects;

public class OnlandVisualTrashAssessmentReviewAndFinalizeDto
{
    public bool Finalize { get; set; }
    public int OnlandVisualTrashAssessmentID { get; set; }
    public int? OnlandVisualTrashAssessmentAreaID { get; set; }
    public string? OnlandVisualTrashAssessmentAreaName { get; set; }
    public int? StormwaterJurisdictionID { get; set; }
    public int? OnlandVisualTrashAssessmentScoreID { get; set; }
    public int OnlandVisualTrashAssessmentStatusID { get; set; }
    public string? AssessmentAreaDescription { get; set; }
    public bool AssessingNewArea { get; set; }
    public bool? IsProgressAssessment { get; set; }
    public string? Notes { get; set; }
    public DateTime? AssessmentDate { get; set; }
    public List<OnlandVisualTrashAssessmentPreliminarySourceIdentificationUpsertDto> PreliminarySourceIdentifications { get; set; }
}