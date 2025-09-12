namespace Neptune.Models.DataTransferObjects;

public class LandUseBlockGridDto
{
    public int LandUseBlockID { get; set; }
    public int? PriorityLandUseTypeID { get; set; }
    public string? PriorityLandUseTypeName { get; set; }
    public string LandUseDescription { get; set; }
    public decimal? TrashGenerationRate { get; set; }
    public string LandUseForTGR { get; set; }
    public decimal? MedianHouseholdIncomeResidential { get; set; }
    public decimal? MedianHouseholdIncomeRetail { get; set; }
    public int StormwaterJurisdictionID { get; set; }
    public string StormwaterJurisdictionName { get; set; }
    public int PermitTypeID { get; set; }
    public string PermitTypeName { get; set; }
    public double Area { get; set; }
    public double TrashGeneratingArea { get; set; }
}