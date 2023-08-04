//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectLoadGeneratingUnit]
using System;


namespace Neptune.Models.DataTransferObjects
{
    public partial class ProjectLoadGeneratingUnitDto
    {
        public int ProjectLoadGeneratingUnitID { get; set; }
        public ProjectDto Project { get; set; }
        public ModelBasinDto ModelBasin { get; set; }
        public RegionalSubbasinDto RegionalSubbasin { get; set; }
        public DelineationDto Delineation { get; set; }
        public WaterQualityManagementPlanDto WaterQualityManagementPlan { get; set; }
        public bool? IsEmptyResponseFromHRUService { get; set; }
    }

    public partial class ProjectLoadGeneratingUnitSimpleDto
    {
        public int ProjectLoadGeneratingUnitID { get; set; }
        public System.Int32 ProjectID { get; set; }
        public System.Int32? ModelBasinID { get; set; }
        public System.Int32? RegionalSubbasinID { get; set; }
        public System.Int32? DelineationID { get; set; }
        public System.Int32? WaterQualityManagementPlanID { get; set; }
        public bool? IsEmptyResponseFromHRUService { get; set; }
    }

}