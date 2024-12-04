//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[QuickBMP]

namespace Neptune.Models.DataTransferObjects
{
    public partial class QuickBMPSimpleDto
    {
        public int QuickBMPID { get; set; }
        public int WaterQualityManagementPlanID { get; set; }
        public int TreatmentBMPTypeID { get; set; }
        public string QuickBMPName { get; set; }
        public string QuickBMPNote { get; set; }
        public decimal? PercentOfSiteTreated { get; set; }
        public decimal? PercentCaptured { get; set; }
        public decimal? PercentRetained { get; set; }
        public int? DryWeatherFlowOverrideID { get; set; }
        public int NumberOfIndividualBMPs { get; set; }
    }
}