//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanDocument]
using System;


namespace Neptune.Models.DataTransferObjects
{
    public partial class WaterQualityManagementPlanDocumentDto
    {
        public int WaterQualityManagementPlanDocumentID { get; set; }
        public WaterQualityManagementPlanDto WaterQualityManagementPlan { get; set; }
        public FileResourceDto FileResource { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public DateTime UploadDate { get; set; }
        public WaterQualityManagementPlanDocumentTypeDto WaterQualityManagementPlanDocumentType { get; set; }
    }

    public partial class WaterQualityManagementPlanDocumentSimpleDto
    {
        public int WaterQualityManagementPlanDocumentID { get; set; }
        public System.Int32 WaterQualityManagementPlanID { get; set; }
        public System.Int32 FileResourceID { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public DateTime UploadDate { get; set; }
        public System.Int32 WaterQualityManagementPlanDocumentTypeID { get; set; }
    }

}