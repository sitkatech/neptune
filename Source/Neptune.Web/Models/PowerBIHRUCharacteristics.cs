using System;

namespace Neptune.Web.Models
{
    public class PowerBIHRUCharacteristics
    {
        public PowerBIHRUCharacteristics()
        {
            HRUEntityType = String.Empty;
            HydrologicSoilGroup = String.Empty;
            ImperviousAcres = 0;
            LastUpdated = String.Empty;
            LSPCLandUseDescription = String.Empty;
            RegionalSubbasin = String.Empty;
            SlopePercentage = 0;
            TotalAcres = 0;
            TreatmentBMP = String.Empty;
            WaterQualityManagementPlan =
                String.Empty;
        }

        public string HRUEntityType { get; set; }
        public string LSPCLandUseDescription { get; set; }
        public string HydrologicSoilGroup { get; set; }
        public int SlopePercentage { get; set; }
        public double ImperviousAcres { get; set; }
        public double TotalAcres { get; set; }
        public string TreatmentBMP { get; set; }
        public string WaterQualityManagementPlan { get; set; }
        public string RegionalSubbasin { get; set; }
        public string LastUpdated { get; set; }
    }
}