//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanPriority]
using System;


namespace Hippocamp.Models.DataTransferObjects
{
    public partial class WaterQualityManagementPlanPriorityDto
    {
        public int WaterQualityManagementPlanPriorityID { get; set; }
        public string WaterQualityManagementPlanPriorityName { get; set; }
        public string WaterQualityManagementPlanPriorityDisplayName { get; set; }
        public int SortOrder { get; set; }
    }

    public partial class WaterQualityManagementPlanPrioritySimpleDto
    {
        public int WaterQualityManagementPlanPriorityID { get; set; }
        public string WaterQualityManagementPlanPriorityName { get; set; }
        public string WaterQualityManagementPlanPriorityDisplayName { get; set; }
        public int SortOrder { get; set; }
    }

}