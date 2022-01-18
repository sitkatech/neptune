//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[DirtyModelNode]
using System;


namespace Hippocamp.Models.DataTransferObjects
{
    public partial class DirtyModelNodeDto
    {
        public int DirtyModelNodeID { get; set; }
        public int? TreatmentBMPID { get; set; }
        public int? WaterQualityManagementPlanID { get; set; }
        public int? RegionalSubbasinID { get; set; }
        public int? DelineationID { get; set; }
        public DateTime CreateDate { get; set; }
    }

    public partial class DirtyModelNodeSimpleDto
    {
        public int DirtyModelNodeID { get; set; }
        public int? TreatmentBMPID { get; set; }
        public int? WaterQualityManagementPlanID { get; set; }
        public int? RegionalSubbasinID { get; set; }
        public int? DelineationID { get; set; }
        public DateTime CreateDate { get; set; }
    }

}