using Microsoft.EntityFrameworkCore;
using Neptune.Common.DesignByContract;

namespace Neptune.EFModels.Entities
{
    public static class OnlandVisualTrashAssessmentObservations
    {
        private static IQueryable<OnlandVisualTrashAssessmentObservation> GetImpl(NeptuneDbContext dbContext)
        {
            return dbContext.OnlandVisualTrashAssessmentObservations
                    .Include(x => x.OnlandVisualTrashAssessment)
                    .ThenInclude(x => x.OnlandVisualTrashAssessmentArea)
                    .Include(x => x.OnlandVisualTrashAssessmentObservationPhotos)
                    .ThenInclude(x => x.FileResource)
                ;
        }

        public static OnlandVisualTrashAssessmentObservation GetByIDWithChangeTracking(NeptuneDbContext dbContext, int onlandVisualTrashAssessmentObservationID)
        {
            var onlandVisualTrashAssessmentObservation = GetImpl(dbContext)
                .SingleOrDefault(x => x.OnlandVisualTrashAssessmentObservationID == onlandVisualTrashAssessmentObservationID);
            Check.RequireNotNull(onlandVisualTrashAssessmentObservation, $"OnlandVisualTrashAssessmentObservation with ID {onlandVisualTrashAssessmentObservationID} not found!");
            return onlandVisualTrashAssessmentObservation;
        }

        public static OnlandVisualTrashAssessmentObservation GetByIDWithChangeTracking(NeptuneDbContext dbContext, OnlandVisualTrashAssessmentObservationPrimaryKey onlandVisualTrashAssessmentObservationPrimaryKey)
        {
            return GetByIDWithChangeTracking(dbContext, onlandVisualTrashAssessmentObservationPrimaryKey.PrimaryKeyValue);
        }

        public static OnlandVisualTrashAssessmentObservation GetByID(NeptuneDbContext dbContext, int onlandVisualTrashAssessmentObservationID)
        {
            var onlandVisualTrashAssessmentObservation = GetImpl(dbContext).AsNoTracking()
                .SingleOrDefault(x => x.OnlandVisualTrashAssessmentObservationID == onlandVisualTrashAssessmentObservationID);
            Check.RequireNotNull(onlandVisualTrashAssessmentObservation, $"OnlandVisualTrashAssessmentObservation with ID {onlandVisualTrashAssessmentObservationID} not found!");
            return onlandVisualTrashAssessmentObservation;
        }

        public static OnlandVisualTrashAssessmentObservation GetByID(NeptuneDbContext dbContext, OnlandVisualTrashAssessmentObservationPrimaryKey onlandVisualTrashAssessmentObservationPrimaryKey)
        {
            return GetByID(dbContext, onlandVisualTrashAssessmentObservationPrimaryKey.PrimaryKeyValue);
        }

        public static List<OnlandVisualTrashAssessmentObservation> ListByOnlandVisualTrashAssessmentID(NeptuneDbContext dbContext, int onlandVisualTrashAssessmentID)
        {
            return GetImpl(dbContext).AsNoTracking().Where(x => x.OnlandVisualTrashAssessmentID == onlandVisualTrashAssessmentID).OrderBy(x => x.ObservationDatetime).ToList();
        }
    }
}
