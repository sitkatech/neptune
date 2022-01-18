//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[SourceControlBMP]
using System;


namespace Hippocamp.Models.DataTransferObjects
{
    public partial class SourceControlBMPDto
    {
        public int SourceControlBMPID { get; set; }
        public WaterQualityManagementPlanDto WaterQualityManagementPlan { get; set; }
        public SourceControlBMPAttributeDto SourceControlBMPAttribute { get; set; }
        public bool? IsPresent { get; set; }
        public string SourceControlBMPNote { get; set; }
    }

    public partial class SourceControlBMPSimpleDto
    {
        public int SourceControlBMPID { get; set; }
        public int WaterQualityManagementPlanID { get; set; }
        public int SourceControlBMPAttributeID { get; set; }
        public bool? IsPresent { get; set; }
        public string SourceControlBMPNote { get; set; }
    }

}