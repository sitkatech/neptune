//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[DirtyModelNode]
using System;


namespace Neptune.Models.DataTransferObjects
{
    public partial class DirtyModelNodeDto
    {
        public int DirtyModelNodeID { get; set; }
        public TreatmentBMPDto TreatmentBMP { get; set; }
        public WaterQualityManagementPlanDto WaterQualityManagementPlan { get; set; }
        public RegionalSubbasinDto RegionalSubbasin { get; set; }
        public DelineationDto Delineation { get; set; }
        public DateTime CreateDate { get; set; }
    }

    public partial class DirtyModelNodeSimpleDto
    {
        public int DirtyModelNodeID { get; set; }
        public System.Int32? TreatmentBMPID { get; set; }
        public System.Int32? WaterQualityManagementPlanID { get; set; }
        public System.Int32? RegionalSubbasinID { get; set; }
        public System.Int32? DelineationID { get; set; }
        public DateTime CreateDate { get; set; }
    }

}