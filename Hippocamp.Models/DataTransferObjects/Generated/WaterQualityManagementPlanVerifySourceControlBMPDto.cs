//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanVerifySourceControlBMP]
using System;


namespace Hippocamp.Models.DataTransferObjects
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
        public int WaterQualityManagementPlanVerifyID { get; set; }
        public int SourceControlBMPID { get; set; }
        public string WaterQualityManagementPlanSourceControlCondition { get; set; }
    }

}