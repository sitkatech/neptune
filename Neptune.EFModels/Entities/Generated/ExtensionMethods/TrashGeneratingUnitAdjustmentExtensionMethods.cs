//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TrashGeneratingUnitAdjustment]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class TrashGeneratingUnitAdjustmentExtensionMethods
    {
        public static TrashGeneratingUnitAdjustmentSimpleDto AsSimpleDto(this TrashGeneratingUnitAdjustment trashGeneratingUnitAdjustment)
        {
            var dto = new TrashGeneratingUnitAdjustmentSimpleDto()
            {
                TrashGeneratingUnitAdjustmentID = trashGeneratingUnitAdjustment.TrashGeneratingUnitAdjustmentID,
                AdjustedDelineationID = trashGeneratingUnitAdjustment.AdjustedDelineationID,
                AdjustedOnlandVisualTrashAssessmentAreaID = trashGeneratingUnitAdjustment.AdjustedOnlandVisualTrashAssessmentAreaID,
                AdjustmentDate = trashGeneratingUnitAdjustment.AdjustmentDate,
                AdjustedByPersonID = trashGeneratingUnitAdjustment.AdjustedByPersonID,
                IsProcessed = trashGeneratingUnitAdjustment.IsProcessed,
                ProcessedDate = trashGeneratingUnitAdjustment.ProcessedDate
            };
            return dto;
        }
    }
}