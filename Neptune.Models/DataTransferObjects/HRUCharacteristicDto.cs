namespace Neptune.Models.DataTransferObjects
{
    public class HRUCharacteristicDto
    {
        public int HRUCharacteristicID { get; set; }
        public DateTime LastUpdated { get; set; }
        public int LoadGeneratingUnitID { get; set; }
        public string HRUEntity { get; set; } = null!;
        public int? TreatmentBMPID { get; set; }
        public string? TreatmentBMPName { get; set; }
        public int? WaterQualityManagementPlanID { get; set; }
        public string? WaterQualityManagementPlanName { get; set; }
        public int? RegionalSubbasinID { get; set; }
        public string RegionalSubbasinName { get; set; } = null!;
        public string? HydrologicSoilGroup { get; set; }
        public int SlopePercentage { get; set; }
        public double Area { get; set; }
        public double ImperviousAcres { get; set; }
        public int HRUCharacteristicLandUseCodeID { get; set; }
        public string? HRUCharacteristicLandUseCodeDisplayName { get; set; }
        public double BaselineImperviousAcres { get; set; }
        public int BaselineHRUCharacteristicLandUseCodeID { get; set; }
        public string? BaselineHRUCharacteristicLandUseCodeDisplayName { get; set; }
    }
}
