//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TrashGeneratingUnit]
using System;


namespace Neptune.Models.DataTransferObjects
{
    public partial class TrashGeneratingUnitDto
    {
        public int TrashGeneratingUnitID { get; set; }
        public StormwaterJurisdictionDto StormwaterJurisdiction { get; set; }
        public int? OnlandVisualTrashAssessmentAreaID { get; set; }
        public LandUseBlockDto LandUseBlock { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public int? DelineationID { get; set; }
        public int? WaterQualityManagementPlanID { get; set; }
    }

    public partial class TrashGeneratingUnitSimpleDto
    {
        public int TrashGeneratingUnitID { get; set; }
        public System.Int32 StormwaterJurisdictionID { get; set; }
        public int? OnlandVisualTrashAssessmentAreaID { get; set; }
        public System.Int32? LandUseBlockID { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public int? DelineationID { get; set; }
        public int? WaterQualityManagementPlanID { get; set; }
    }

}