//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanPhoto]
using System;


namespace Neptune.Models.DataTransferObjects
{
    public partial class WaterQualityManagementPlanPhotoDto
    {
        public int WaterQualityManagementPlanPhotoID { get; set; }
        public FileResourceDto FileResource { get; set; }
        public string Caption { get; set; }
        public DateTime UploadDate { get; set; }
    }

    public partial class WaterQualityManagementPlanPhotoSimpleDto
    {
        public int WaterQualityManagementPlanPhotoID { get; set; }
        public System.Int32 FileResourceID { get; set; }
        public string Caption { get; set; }
        public DateTime UploadDate { get; set; }
    }

}