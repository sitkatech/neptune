//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanVerifySourceControlBMP]
using System;


namespace Neptune.Models.DataTransferObjects
{
    public partial class WaterQualityManagementPlanVerifySourceControlBMPDto
    {
        public int WaterQualityManagementPlanVerifySourceControlBMPID { get; set; }
        public WaterQualityManagementPlanVerifyDto WaterQualityManagementPlanVerify { get; set; }
        public SourceControlBMPDto SourceControlBMP { get; set; }
        public string WaterQualityManagementPlanSourceControlCondition { get; set; }
    }

    public partial class WaterQualityManagementPlanVerifySourceControlBMPSimpleDto
    {
        public int WaterQualityManagementPlanVerifySourceControlBMPID { get; set; }
        public System.Int32 WaterQualityManagementPlanVerifyID { get; set; }
        public System.Int32 SourceControlBMPID { get; set; }
        public string WaterQualityManagementPlanSourceControlCondition { get; set; }
    }

}