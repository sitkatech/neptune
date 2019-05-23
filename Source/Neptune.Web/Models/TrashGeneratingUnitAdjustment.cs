using System;

namespace Neptune.Web.Models
{
    public static class TrashGeneratingUnitAdjustmentModelExtensions
    {
        public static Delineation GetAdjustedDelineation(this TrashGeneratingUnitAdjustment trashGeneratingUnitAdjustment, DatabaseEntities dbContext)
        {
            if (trashGeneratingUnitAdjustment.AdjustedDelineationID == null)
            {
                throw new InvalidOperationException(
                    "Attempted to retrieve Adjusted Delineation without null-checking.");
            }
            return dbContext.Delineations.Find(trashGeneratingUnitAdjustment.AdjustedDelineationID);
        }

        public static OnlandVisualTrashAssessmentArea GetAdjustedOnlandVisualTrashAssessmentArea(this TrashGeneratingUnitAdjustment trashGeneratingUnitAdjustment, DatabaseEntities dbContext)
        {
            if (trashGeneratingUnitAdjustment.AdjustedOnlandVisualTrashAssessmentAreaID == null)
            {
                throw new InvalidOperationException(
                    "Attempted to retrieve Adjusted Assessment Area without null-checking.");
            }
            return dbContext.OnlandVisualTrashAssessmentAreas.Find(trashGeneratingUnitAdjustment.AdjustedOnlandVisualTrashAssessmentAreaID);
        }
    }
}
