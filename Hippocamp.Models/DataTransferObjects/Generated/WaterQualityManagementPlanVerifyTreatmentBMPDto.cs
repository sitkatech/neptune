//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanVerifyTreatmentBMP]
using System;


namespace Hippocamp.Models.DataTransferObjects
{
    public partial class WaterQualityManagementPlanVerifyTreatmentBMPDto
    {
        public int WaterQualityManagementPlanVerifyTreatmentBMPID { get; set; }
        public WaterQualityManagementPlanVerifyDto WaterQualityManagementPlanVerify { get; set; }
        public TreatmentBMPDto TreatmentBMP { get; set; }
        public bool? IsAdequate { get; set; }
        public string WaterQualityManagementPlanVerifyTreatmentBMPNote { get; set; }
    }

    public partial class WaterQualityManagementPlanVerifyTreatmentBMPSimpleDto
    {
        public int WaterQualityManagementPlanVerifyTreatmentBMPID { get; set; }
        public int WaterQualityManagementPlanVerifyID { get; set; }
        public int TreatmentBMPID { get; set; }
        public bool? IsAdequate { get; set; }
        public string WaterQualityManagementPlanVerifyTreatmentBMPNote { get; set; }
    }

}