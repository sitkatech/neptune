//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[QuickBMP]
using System;


namespace Neptune.Models.DataTransferObjects
{
    public partial class QuickBMPDto
    {
        public int QuickBMPID { get; set; }
        public WaterQualityManagementPlanDto WaterQualityManagementPlan { get; set; }
        public TreatmentBMPTypeDto TreatmentBMPType { get; set; }
        public string QuickBMPName { get; set; }
        public string QuickBMPNote { get; set; }
        public decimal? PercentOfSiteTreated { get; set; }
        public decimal? PercentCaptured { get; set; }
        public decimal? PercentRetained { get; set; }
        public DryWeatherFlowOverrideDto DryWeatherFlowOverride { get; set; }
    }

    public partial class QuickBMPSimpleDto
    {
        public int QuickBMPID { get; set; }
        public System.Int32 WaterQualityManagementPlanID { get; set; }
        public System.Int32 TreatmentBMPTypeID { get; set; }
        public string QuickBMPName { get; set; }
        public string QuickBMPNote { get; set; }
        public decimal? PercentOfSiteTreated { get; set; }
        public decimal? PercentCaptured { get; set; }
        public decimal? PercentRetained { get; set; }
        public System.Int32? DryWeatherFlowOverrideID { get; set; }
    }

}