//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanVerifyQuickBMP]
using System;


namespace Hippocamp.Models.DataTransferObjects
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
        public int WaterQualityManagementPlanVerifyID { get; set; }
        public int QuickBMPID { get; set; }
        public bool? IsAdequate { get; set; }
        public string WaterQualityManagementPlanVerifyQuickBMPNote { get; set; }
    }

}