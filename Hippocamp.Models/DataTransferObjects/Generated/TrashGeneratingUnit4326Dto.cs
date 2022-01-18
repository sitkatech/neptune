//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TrashGeneratingUnit4326]
using System;


namespace Hippocamp.Models.DataTransferObjects
{
    public partial class TrashGeneratingUnit4326Dto
    {
        public int TrashGeneratingUnit4326ID { get; set; }
        public StormwaterJurisdictionDto StormwaterJurisdiction { get; set; }
        public int? OnlandVisualTrashAssessmentAreaID { get; set; }
        public LandUseBlockDto LandUseBlock { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public int? DelineationID { get; set; }
        public int? WaterQualityManagementPlanID { get; set; }
    }

    public partial class TrashGeneratingUnit4326SimpleDto
    {
        public int TrashGeneratingUnit4326ID { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        public int? OnlandVisualTrashAssessmentAreaID { get; set; }
        public int? LandUseBlockID { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public int? DelineationID { get; set; }
        public int? WaterQualityManagementPlanID { get; set; }
    }

}