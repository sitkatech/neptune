//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectNereidResult]
using System;


namespace Neptune.Models.DataTransferObjects
{
    public partial class ProjectNereidResultDto
    {
        public int ProjectNereidResultID { get; set; }
        public ProjectDto Project { get; set; }
        public bool IsBaselineCondition { get; set; }
        public TreatmentBMPDto TreatmentBMP { get; set; }
        public WaterQualityManagementPlanDto WaterQualityManagementPlan { get; set; }
        public RegionalSubbasinDto RegionalSubbasin { get; set; }
        public DelineationDto Delineation { get; set; }
        public string NodeID { get; set; }
        public string FullResponse { get; set; }
        public DateTime? LastUpdate { get; set; }
    }

    public partial class ProjectNereidResultSimpleDto
    {
        public int ProjectNereidResultID { get; set; }
        public System.Int32 ProjectID { get; set; }
        public bool IsBaselineCondition { get; set; }
        public System.Int32? TreatmentBMPID { get; set; }
        public System.Int32? WaterQualityManagementPlanID { get; set; }
        public System.Int32? RegionalSubbasinID { get; set; }
        public System.Int32? DelineationID { get; set; }
        public string NodeID { get; set; }
        public string FullResponse { get; set; }
        public DateTime? LastUpdate { get; set; }
    }

}