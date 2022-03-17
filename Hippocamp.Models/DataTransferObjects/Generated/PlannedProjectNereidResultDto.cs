//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PlannedProjectNereidResult]
using System;


namespace Hippocamp.Models.DataTransferObjects
{
    public partial class PlannedProjectNereidResultDto
    {
        public int PlannedProjectNereidResultID { get; set; }
        public ProjectDto Project { get; set; }
        public bool IsBaselineCondition { get; set; }
        public int? TreatmentBMPID { get; set; }
        public int? WaterQualityManagementPlanID { get; set; }
        public int? RegionalSubbasinID { get; set; }
        public string NodeID { get; set; }
        public string FullResponse { get; set; }
        public DateTime? LastUpdate { get; set; }
    }

    public partial class PlannedProjectNereidResultSimpleDto
    {
        public int PlannedProjectNereidResultID { get; set; }
        public int ProjectID { get; set; }
        public bool IsBaselineCondition { get; set; }
        public int? TreatmentBMPID { get; set; }
        public int? WaterQualityManagementPlanID { get; set; }
        public int? RegionalSubbasinID { get; set; }
        public string NodeID { get; set; }
        public string FullResponse { get; set; }
        public DateTime? LastUpdate { get; set; }
    }

}