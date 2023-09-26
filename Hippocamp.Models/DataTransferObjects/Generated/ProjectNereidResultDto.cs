//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectNereidResult]
using System;


namespace Hippocamp.Models.DataTransferObjects
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
        public int ProjectID { get; set; }
        public bool IsBaselineCondition { get; set; }
        public int? TreatmentBMPID { get; set; }
        public int? WaterQualityManagementPlanID { get; set; }
        public int? RegionalSubbasinID { get; set; }
        public int? DelineationID { get; set; }
        public string NodeID { get; set; }
        public string FullResponse { get; set; }
        public DateTime? LastUpdate { get; set; }
    }

}