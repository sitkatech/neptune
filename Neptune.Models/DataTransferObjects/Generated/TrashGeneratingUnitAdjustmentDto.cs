//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TrashGeneratingUnitAdjustment]
using System;


namespace Neptune.Models.DataTransferObjects
{
    public partial class TrashGeneratingUnitAdjustmentDto
    {
        public int TrashGeneratingUnitAdjustmentID { get; set; }
        public int? AdjustedDelineationID { get; set; }
        public int? AdjustedOnlandVisualTrashAssessmentAreaID { get; set; }
        public DateTime AdjustmentDate { get; set; }
        public PersonDto AdjustedByPerson { get; set; }
        public bool IsProcessed { get; set; }
        public DateTime? ProcessedDate { get; set; }
    }

    public partial class TrashGeneratingUnitAdjustmentSimpleDto
    {
        public int TrashGeneratingUnitAdjustmentID { get; set; }
        public int? AdjustedDelineationID { get; set; }
        public int? AdjustedOnlandVisualTrashAssessmentAreaID { get; set; }
        public DateTime AdjustmentDate { get; set; }
        public System.Int32 AdjustedByPersonID { get; set; }
        public bool IsProcessed { get; set; }
        public DateTime? ProcessedDate { get; set; }
    }

}