//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[QuickBMP]
using System;


namespace Hippocamp.Models.DataTransferObjects
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
        public int WaterQualityManagementPlanID { get; set; }
        public int TreatmentBMPTypeID { get; set; }
        public string QuickBMPName { get; set; }
        public string QuickBMPNote { get; set; }
        public decimal? PercentOfSiteTreated { get; set; }
        public decimal? PercentCaptured { get; set; }
        public decimal? PercentRetained { get; set; }
        public int? DryWeatherFlowOverrideID { get; set; }
    }

}