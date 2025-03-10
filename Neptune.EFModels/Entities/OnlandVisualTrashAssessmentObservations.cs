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

        public static async Task Update(NeptuneDbContext dbContext, int onlandVisualTrashAssessmentID,
        List<OnlandVisualTrashAssessmentObservationUpsertDto> onlandVisualTrashAssessmentObservationUpsertDtos)
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

            foreach (var onlandVisualTrashAssessmentObservationUpsertDto in onlandVisualTrashAssessmentObservationUpsertDtos)
            {
                var onlandVisualTrashAssessmentObservationPhoto = new OnlandVisualTrashAssessmentObservationPhoto();

                if (onlandVisualTrashAssessmentObservationUpsertDto.FileResourceID != null)
                {
                    onlandVisualTrashAssessmentObservationPhoto.FileResourceID =
                        (int)onlandVisualTrashAssessmentObservationUpsertDto.FileResourceID;
                }


                var onlandVisualTrashAssessmentObservation = new OnlandVisualTrashAssessmentObservation()
                {
                    OnlandVisualTrashAssessmentID = onlandVisualTrashAssessmentID,
                    Note = onlandVisualTrashAssessmentObservationUpsertDto.Note,
                    ObservationDatetime = onlandVisualTrashAssessmentObservationUpsertDto.ObservationDatetime ?? DateTime.UtcNow,
                    LocationPoint4326 = GeometryHelper.CreateLocationPoint4326FromLatLong(onlandVisualTrashAssessmentObservationUpsertDto.Latitude, onlandVisualTrashAssessmentObservationUpsertDto.Longitude),
                    LocationPoint = GeometryHelper.CreateLocationPoint4326FromLatLong(onlandVisualTrashAssessmentObservationUpsertDto.Latitude, onlandVisualTrashAssessmentObservationUpsertDto.Longitude).ProjectTo2771(),
                    OnlandVisualTrashAssessmentObservationPhotos = [onlandVisualTrashAssessmentObservationPhoto]
                };


                dbContext.OnlandVisualTrashAssessmentObservations.Add(onlandVisualTrashAssessmentObservation);
            }

            await dbContext.OnlandVisualTrashAssessmentObservationPhotoStagings
                .Where(x => x.OnlandVisualTrashAssessmentID == onlandVisualTrashAssessmentID).ExecuteDeleteAsync();

            await dbContext.SaveChangesAsync();
        }
    }
}
