namespace Neptune.Models.DataTransferObjects;

public class WaterQualityManagementPlanVerifyTreatmentBMPSimpleDto
{
    public int WaterQualityManagementPlanVerifyTreatmentBMPID { get; set; }
    public int WaterQualityManagementPlanVerifyID { get; set; }
    public int TreatmentBMPID { get; set; }
    public bool? IsAdequate { get; set; }
    public string WaterQualityManagementPlanVerifyTreatmentBMPNote { get; set; }
    public string TreatmentBMPName { get; set; }
    public string TreatmentBMPType { get; set; }
    public string FieldVisiLastVisitedtDate { get; set; }
    public string FieldVisitMostRecentScore { get; set; }
    public string TreatmentBMPDetailUrl { get; set; }
}