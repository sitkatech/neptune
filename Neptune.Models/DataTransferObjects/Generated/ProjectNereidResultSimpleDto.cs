//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectNereidResult]

namespace Neptune.Models.DataTransferObjects
{
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