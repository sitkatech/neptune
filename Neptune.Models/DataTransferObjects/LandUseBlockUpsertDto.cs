using System.ComponentModel.DataAnnotations;

namespace Neptune.Models.DataTransferObjects;

public class LandUseBlockUpsertDto
{
    [Display(Name = "Land Use Type")]
    [Required]
    public int PriorityLandUseTypeID { get; set; }
    [Display(Name = "Trash Generation Rate")]
    [Required]
    [Range(0, 999.9, ErrorMessage = "Trash Generation Rate must be between 0 and 999.9.")]
    public decimal? TrashGenerationRate { get; set; }
    public string LandUseDescription { get; set; }
    public decimal? MedianHouseholdIncomeResidential { get; set; }
    public decimal? MedianHouseholdIncomeRetail { get; set; }
    [Display(Name = "Permit Type")]
    [Required]
    public int PermitTypeID { get; set; }
}