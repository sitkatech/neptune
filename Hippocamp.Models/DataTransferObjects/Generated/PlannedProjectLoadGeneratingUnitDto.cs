//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PlannedProjectLoadGeneratingUnit]
using System;


namespace Hippocamp.Models.DataTransferObjects
{
    public partial class PlannedProjectLoadGeneratingUnitDto
    {
        public int PlannedProjectLoadGeneratingUnitID { get; set; }
        public ProjectDto Project { get; set; }
        public ModelBasinDto ModelBasin { get; set; }
        public RegionalSubbasinDto RegionalSubbasin { get; set; }
        public DelineationDto Delineation { get; set; }
        public WaterQualityManagementPlanDto WaterQualityManagementPlan { get; set; }
        public bool? IsEmptyResponseFromHRUService { get; set; }
    }

    public partial class PlannedProjectLoadGeneratingUnitSimpleDto
    {
        public int PlannedProjectLoadGeneratingUnitID { get; set; }
        public int ProjectID { get; set; }
        public int? ModelBasinID { get; set; }
        public int? RegionalSubbasinID { get; set; }
        public int? DelineationID { get; set; }
        public int? WaterQualityManagementPlanID { get; set; }
        public bool? IsEmptyResponseFromHRUService { get; set; }
    }

}