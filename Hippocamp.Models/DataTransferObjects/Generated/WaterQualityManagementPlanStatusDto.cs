//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanStatus]
using System;


namespace Hippocamp.Models.DataTransferObjects
{
    public partial class WaterQualityManagementPlanStatusDto
    {
        public int WaterQualityManagementPlanStatusID { get; set; }
        public string WaterQualityManagementPlanStatusName { get; set; }
        public string WaterQualityManagementPlanStatusDisplayName { get; set; }
        public int SortOrder { get; set; }
    }

    public partial class WaterQualityManagementPlanStatusSimpleDto
    {
        public int WaterQualityManagementPlanStatusID { get; set; }
        public string WaterQualityManagementPlanStatusName { get; set; }
        public string WaterQualityManagementPlanStatusDisplayName { get; set; }
        public int SortOrder { get; set; }
    }

}