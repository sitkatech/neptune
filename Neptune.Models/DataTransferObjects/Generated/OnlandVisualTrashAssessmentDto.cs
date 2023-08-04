//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OnlandVisualTrashAssessment]
using System;


namespace Neptune.Models.DataTransferObjects
{
    public partial class OnlandVisualTrashAssessmentDto
    {
        public int OnlandVisualTrashAssessmentID { get; set; }
        public PersonDto CreatedByPerson { get; set; }
        public DateTime CreatedDate { get; set; }
        public OnlandVisualTrashAssessmentAreaDto OnlandVisualTrashAssessmentArea { get; set; }
        public string Notes { get; set; }
        public StormwaterJurisdictionDto StormwaterJurisdiction { get; set; }
        public bool? AssessingNewArea { get; set; }
        public OnlandVisualTrashAssessmentStatusDto OnlandVisualTrashAssessmentStatus { get; set; }
        public bool? IsDraftGeometryManuallyRefined { get; set; }
        public OnlandVisualTrashAssessmentScoreDto OnlandVisualTrashAssessmentScore { get; set; }
        public DateTime? CompletedDate { get; set; }
        public string DraftAreaName { get; set; }
        public string DraftAreaDescription { get; set; }
        public bool IsTransectBackingAssessment { get; set; }
        public bool IsProgressAssessment { get; set; }
    }

    public partial class OnlandVisualTrashAssessmentSimpleDto
    {
        public int OnlandVisualTrashAssessmentID { get; set; }
        public System.Int32 CreatedByPersonID { get; set; }
        public DateTime CreatedDate { get; set; }
        public System.Int32? OnlandVisualTrashAssessmentAreaID { get; set; }
        public string Notes { get; set; }
        public System.Int32 StormwaterJurisdictionID { get; set; }
        public bool? AssessingNewArea { get; set; }
        public System.Int32 OnlandVisualTrashAssessmentStatusID { get; set; }
        public bool? IsDraftGeometryManuallyRefined { get; set; }
        public System.Int32? OnlandVisualTrashAssessmentScoreID { get; set; }
        public DateTime? CompletedDate { get; set; }
        public string DraftAreaName { get; set; }
        public string DraftAreaDescription { get; set; }
        public bool IsTransectBackingAssessment { get; set; }
        public bool IsProgressAssessment { get; set; }
    }

}