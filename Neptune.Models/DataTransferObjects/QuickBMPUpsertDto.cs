using System.ComponentModel.DataAnnotations;

namespace Neptune.Models.DataTransferObjects;

public class QuickBMPUpsertDto
{
    [Required(ErrorMessage = "A Simplified Structural BMP is missing a Treatment Type.")]
    public int? TreatmentBMPTypeID { get; set; }

    [Required(ErrorMessage = "A Simplified Structural BMP is missing a name.")]
    public string? QuickBMPName { get; set; }
    public string? QuickBMPNote { get; set; }
    public decimal? PercentOfSiteTreated { get; set; }
    public decimal? PercentCaptured { get; set; }
    public decimal? PercentRetained { get; set; }
    public int? DryWeatherFlowOverrideID { get; set; }

    [Required(ErrorMessage = "A Simplified Structural BMP is missing the # of Individual BMPs.")]
    public int? NumberOfIndividualBMPs { get; set; }
}