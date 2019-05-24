using System;

namespace Neptune.Web.Models
{
    public static class TrashGeneratingUnitAdjustmentModelExtensions
    {
        // The TGU adjustment's entity may have been deleted by the time we get to its spot on the queue, so we have to allow returning nulls from these methods
        public static Delineation GetAdjustedDelineation(this TrashGeneratingUnitAdjustment trashGeneratingUnitAdjustment, DatabaseEntities dbContext)
        {
            return dbContext.Delineations.Find(trashGeneratingUnitAdjustment.AdjustedDelineationID);
        }

        public static OnlandVisualTrashAssessmentArea GetAdjustedOnlandVisualTrashAssessmentArea(this TrashGeneratingUnitAdjustment trashGeneratingUnitAdjustment, DatabaseEntities dbContext)
        {
            return dbContext.OnlandVisualTrashAssessmentAreas.Find(trashGeneratingUnitAdjustment.AdjustedOnlandVisualTrashAssessmentAreaID);
        }
    }
}
