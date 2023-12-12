//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TrashGeneratingUnitAdjustment]

namespace Neptune.Models.DataTransferObjects
{
    public partial class TrashGeneratingUnitAdjustmentSimpleDto
    {
        public int TrashGeneratingUnitAdjustmentID { get; set; }
        public int? AdjustedDelineationID { get; set; }
        public int? AdjustedOnlandVisualTrashAssessmentAreaID { get; set; }
        public DateTime AdjustmentDate { get; set; }
        public int AdjustedByPersonID { get; set; }
        public bool IsProcessed { get; set; }
        public DateTime? ProcessedDate { get; set; }
    }
}