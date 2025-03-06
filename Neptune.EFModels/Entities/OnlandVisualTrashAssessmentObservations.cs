using Microsoft.EntityFrameworkCore;
using Neptune.Common.DesignByContract;
using Neptune.Common.GeoSpatial;
using Neptune.Models.DataTransferObjects;

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

        public static async Task UpdateObservations(NeptuneDbContext dbContext, int onlandVisualTrashAssessmentID,
            List<OnlandVisualTrashAssessmentObservationWithPhotoDto>
                onlandVisualTrashAssessmentObservationWithPhotoDtos)
        {
            var currentObservations = dbContext.OnlandVisualTrashAssessmentObservations
                .Include(x => x.OnlandVisualTrashAssessmentObservationPhotos)
                .Where(x => x.OnlandVisualTrashAssessmentID == onlandVisualTrashAssessmentID).ToList();

            await dbContext.OnlandVisualTrashAssessmentObservationPhotos
                .Include(x => x.OnlandVisualTrashAssessmentObservation).Where(x =>
                    x.OnlandVisualTrashAssessmentObservation.OnlandVisualTrashAssessmentID ==
                    onlandVisualTrashAssessmentID &&
                    currentObservations.Select(y => y.OnlandVisualTrashAssessmentObservationID)
                        .Contains(x.OnlandVisualTrashAssessmentObservationID)).ExecuteDeleteAsync();
            await dbContext.OnlandVisualTrashAssessmentObservations.Where(x =>
                x.OnlandVisualTrashAssessmentID == onlandVisualTrashAssessmentID && currentObservations
                    .Select(y => y.OnlandVisualTrashAssessmentObservationID)
                    .Contains(x.OnlandVisualTrashAssessmentObservationID)).ExecuteDeleteAsync();

            var updatedObservations = onlandVisualTrashAssessmentObservationWithPhotoDtos.Select(x =>
                new OnlandVisualTrashAssessmentObservation()
                {
                    OnlandVisualTrashAssessmentID = onlandVisualTrashAssessmentID,
                    Note = x.Note,
                    ObservationDatetime = x.ObservationDatetime ?? DateTime.UtcNow,
                    LocationPoint4326 = GeometryHelper.CreateLocationPoint4326FromLatLong(x.Latitude, x.Longitude),
                    LocationPoint = GeometryHelper.CreateLocationPoint4326FromLatLong(x.Latitude, x.Longitude).ProjectTo2771(),
                    //add photo
                });
            await dbContext.OnlandVisualTrashAssessmentObservations.AddRangeAsync(updatedObservations);
            await dbContext.SaveChangesAsync();
        }
    }
}
