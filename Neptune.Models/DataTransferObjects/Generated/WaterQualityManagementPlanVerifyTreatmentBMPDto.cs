//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanVerifyTreatmentBMP]
using System;


namespace Neptune.Models.DataTransferObjects
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
        public System.Int32 WaterQualityManagementPlanVerifyID { get; set; }
        public System.Int32 TreatmentBMPID { get; set; }
        public bool? IsAdequate { get; set; }
        public string WaterQualityManagementPlanVerifyTreatmentBMPNote { get; set; }
    }

}