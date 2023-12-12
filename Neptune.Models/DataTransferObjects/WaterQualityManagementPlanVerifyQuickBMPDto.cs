namespace Neptune.Models.DataTransferObjects;

public class WaterQualityManagementPlanVerifyQuickBMPDto
{
    public int WaterQualityManagementPlanVerifyQuickBMPID { get; set; }
    public int WaterQualityManagementPlanVerifyID { get; set; }
    public int QuickBMPID { get; set; }
    public bool? IsAdequate { get; set; }
    public string WaterQualityManagementPlanVerifyQuickBMPNote { get; set; }
    public string QuickBMPName { get; set; }
    public string TreatmentBMPType { get; set; }
}