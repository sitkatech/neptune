﻿namespace Neptune.Models.DataTransferObjects;

public class OnlandVisualTrashAssessmentAddRemoveParcelsDto
{
    public int OnlandVisualTrashAssessmentID { get; set; }
    public int? OnlandVisualTrashAssessmentAreaID { get; set; }
    public int StormwaterJurisdictionID { get; set; }
    public bool IsDraftGeometryManuallyRefined { get; set; }
    public DateTime? CompletedDate { get; set; }
    public BoundingBoxDto BoundingBox { get; set; }
    public string? TransectLineAsGeoJson { get; set; }
}