namespace Neptune.Models.DataTransferObjects;

public class LoadGeneratingUnitGridDto
{
    public int LoadGeneratingUnitID { get; set; }
    public int? ModelBasinID { get; set; }
    public int? RegionalSubbasinID { get; set; }
    public string? RegionalSubbasinName { get; set; }
    public int? TreatmentBMPID { get; set; }
    public string? TreatmentBMPName { get; set; }
    public int? WaterQualityManagementPlanID { get; set; }
    public string? WaterQualityManagementPlanName { get; set; }
    public bool? IsEmptyResponseFromHRUService { get; set; }
    public DateTime? DateHRURequested { get; set; }
}