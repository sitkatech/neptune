//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TrashGeneratingUnit4326]
using System;


namespace Neptune.Models.DataTransferObjects
{
    public partial class TrashGeneratingUnit4326Dto
    {
        public int TrashGeneratingUnit4326ID { get; set; }
        public StormwaterJurisdictionDto StormwaterJurisdiction { get; set; }
        public OnlandVisualTrashAssessmentAreaDto OnlandVisualTrashAssessmentArea { get; set; }
        public LandUseBlockDto LandUseBlock { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public int? DelineationID { get; set; }
        public WaterQualityManagementPlanDto WaterQualityManagementPlan { get; set; }
    }

    public partial class TrashGeneratingUnit4326SimpleDto
    {
        public int TrashGeneratingUnit4326ID { get; set; }
        public System.Int32 StormwaterJurisdictionID { get; set; }
        public System.Int32? OnlandVisualTrashAssessmentAreaID { get; set; }
        public System.Int32? LandUseBlockID { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public int? DelineationID { get; set; }
        public System.Int32? WaterQualityManagementPlanID { get; set; }
    }

}