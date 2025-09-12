using System.ComponentModel.DataAnnotations;

namespace Neptune.Models.DataTransferObjects;

public class LandUseBlockUpsertDto
{
    [Display(Name = "Land Use Type")]
    [Required]
    public int LandUseTypeID { get; set; }
    [Display(Name = "Trash Generation Rate")]
    [Required]
    public decimal? TrashGenerationRate { get; set; }
    public string LandUseDescription { get; set; }
    public decimal? MedianHouseholdIncomeResidential { get; set; }
    public decimal? MedianHouseholdIncomeRetail { get; set; }
    [Display(Name = "Permit Type")]
    [Required]
    public int PermitTypeID { get; set; }
}