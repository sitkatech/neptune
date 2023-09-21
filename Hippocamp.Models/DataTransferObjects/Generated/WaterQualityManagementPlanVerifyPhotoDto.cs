//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanVerifyPhoto]
using System;


namespace Hippocamp.Models.DataTransferObjects
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
        public int WaterQualityManagementPlanVerifyID { get; set; }
        public int WaterQualityManagementPlanPhotoID { get; set; }
    }

}