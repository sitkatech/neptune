//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanBoundary]
using System;


namespace Neptune.Models.DataTransferObjects
{
    public partial class WaterQualityManagementPlanBoundaryDto
    {
        public int WaterQualityManagementPlanGeometryID { get; set; }
        public WaterQualityManagementPlanDto WaterQualityManagementPlan { get; set; }
    }

    public partial class WaterQualityManagementPlanBoundarySimpleDto
    {
        public int WaterQualityManagementPlanGeometryID { get; set; }
        public System.Int32 WaterQualityManagementPlanID { get; set; }
    }

}