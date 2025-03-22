using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Neptune.Models.DataTransferObjects;

public class TrashGeneratingUnitDto
{
    public int TrashGeneratingUnitID { get; set; }
    public int? TrashCaptureEffectivenessBMP { get; set; }
    public int? TreatmentBMPID { get; set; }
    public string TreatmentBMPName { get; set; }
    public string TreatmentBMPTypeName { get; set; }
    public string? TrashCaptureStatusBMP { get; set; }
    public int StormwaterJurisdictionID { get; set; }
    public string StormwaterJurisdictionName { get; set; } = null!;
    public decimal? BaselineLoadingRate { get; set; }
    public string? LandUseType { get; set; }
    public decimal? CurrentLoadingRate { get; set; }
    public int? OnlandVisualTrashAssessmentAreaID { get; set; }
    public string? OnlandVisualTrashAssessmentAreaName { get; set; }
    public string? OnlandVisualTrashAssessmentAreaBaselineScore { get; set; }
    public int? WaterQualityManagementPlanID { get; set; }
    public string? WaterQualityManagementPlanName { get; set; }
    public string? TrashCaptureStatusWQMP { get; set; }
    public int? TrashCaptureEffectivenessWQMP { get; set; }
    public DateTime? LastUpdateDate { get; set; }
    public double Area { get; set; }
    public string? AssessmentScore { get; set; }
    public DateTime? AssessmentDate { get; set; }
    public int? CompletedAssessmentCount { get; set; }
}