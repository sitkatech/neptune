//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanVerifyPhoto]
using System;


namespace Neptune.Models.DataTransferObjects
{
    public partial class WaterQualityManagementPlanVerifyPhotoDto
    {
        public int WaterQualityManagementPlanVerifyPhotoID { get; set; }
        public WaterQualityManagementPlanVerifyDto WaterQualityManagementPlanVerify { get; set; }
        public WaterQualityManagementPlanPhotoDto WaterQualityManagementPlanPhoto { get; set; }
    }

    public partial class WaterQualityManagementPlanVerifyPhotoSimpleDto
    {
        public int WaterQualityManagementPlanVerifyPhotoID { get; set; }
        public System.Int32 WaterQualityManagementPlanVerifyID { get; set; }
        public System.Int32 WaterQualityManagementPlanPhotoID { get; set; }
    }

}