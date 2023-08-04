//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanVerifyQuickBMP]
using System;


namespace Neptune.Models.DataTransferObjects
{
    public partial class WaterQualityManagementPlanVerifyQuickBMPDto
    {
        public int WaterQualityManagementPlanVerifyQuickBMPID { get; set; }
        public WaterQualityManagementPlanVerifyDto WaterQualityManagementPlanVerify { get; set; }
        public QuickBMPDto QuickBMP { get; set; }
        public bool? IsAdequate { get; set; }
        public string WaterQualityManagementPlanVerifyQuickBMPNote { get; set; }
    }

    public partial class WaterQualityManagementPlanVerifyQuickBMPSimpleDto
    {
        public int WaterQualityManagementPlanVerifyQuickBMPID { get; set; }
        public System.Int32 WaterQualityManagementPlanVerifyID { get; set; }
        public System.Int32 QuickBMPID { get; set; }
        public bool? IsAdequate { get; set; }
        public string WaterQualityManagementPlanVerifyQuickBMPNote { get; set; }
    }

}