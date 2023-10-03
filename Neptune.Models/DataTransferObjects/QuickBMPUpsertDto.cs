using System.ComponentModel.DataAnnotations;

namespace Neptune.Models.DataTransferObjects;

public class QuickBMPUpsertDto
{
    [Required(ErrorMessage = "A Simplified Structural BMP is missing a name.")]
    public int? TreatmentBMPTypeID { get; set; }
    [Required(ErrorMessage = "A Simplified Structural BMP is missing a Treatment Type.")]
    public string? QuickBMPName { get; set; }
    public string? QuickBMPNote { get; set; }
    public decimal? PercentOfSiteTreated { get; set; }
    public decimal? PercentCaptured { get; set; }
    public decimal? PercentRetained { get; set; }
    public int? DryWeatherFlowOverrideID { get; set; }
}