using Microsoft.EntityFrameworkCore;
using Neptune.Common.GeoSpatial;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.Models;

namespace Neptune.WebMvc.Views.OnlandVisualTrashAssessment
{
    public class RecordObservationsViewModel : OnlandVisualTrashAssessmentViewModel
    {
        public List<OnlandVisualTrashAssessmentObservationSimple> Observations { get; set; }

        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public RecordObservationsViewModel()
        {

        }

        public RecordObservationsViewModel(Neptune.EFModels.Entities.OnlandVisualTrashAssessment onlandVisualTrashAssessment, IEnumerable<OnlandVisualTrashAssessmentObservation> onlandVisualTrashAssessmentObservations) : base(onlandVisualTrashAssessment)
        {
            Observations = onlandVisualTrashAssessmentObservations
                .Select(x => new OnlandVisualTrashAssessmentObservationSimple(x)).ToList();
        }

        public async Task UpdateModel(NeptuneDbContext dbContext, Neptune.EFModels.Entities.OnlandVisualTrashAssessment ovta,
            DbSet<OnlandVisualTrashAssessmentObservation> allOnlandVisualTrashAssessmentObservations)
        {
            var observations = Observations ?? new List<OnlandVisualTrashAssessmentObservationSimple>();

            //delete
            await dbContext.OnlandVisualTrashAssessmentObservationPhotos.Include(x => x.OnlandVisualTrashAssessmentObservation).Where(x => x.OnlandVisualTrashAssessmentObservation.OnlandVisualTrashAssessmentID == ovta.OnlandVisualTrashAssessmentID && !observations.Select(y => y.OnlandVisualTrashAssessmentObservationID).Contains(x.OnlandVisualTrashAssessmentObservationID)).ExecuteDeleteAsync();
            await dbContext.OnlandVisualTrashAssessmentObservations.Where(x => x.OnlandVisualTrashAssessmentID == ovta.OnlandVisualTrashAssessmentID && !observations.Select(y => y.OnlandVisualTrashAssessmentObservationID).Contains(x.OnlandVisualTrashAssessmentObservationID)).ExecuteDeleteAsync();

            //add
            foreach (var onlandVisualTrashAssessmentObservationSimple in observations.Where(x => !ModelObjectHelpers.IsRealPrimaryKeyValue(x.OnlandVisualTrashAssessmentObservationID)))
            {
                var locationPoint4326 = GeometryHelper.CreateLocationPoint4326FromLatLong(onlandVisualTrashAssessmentObservationSimple.LocationY.GetValueOrDefault(), onlandVisualTrashAssessmentObservationSimple.LocationX.GetValueOrDefault());

                var locationPoint2771 = locationPoint4326.ProjectTo2771();

                var onlandVisualTrashAssessmentObservation = new OnlandVisualTrashAssessmentObservation
                {
                    OnlandVisualTrashAssessmentID = onlandVisualTrashAssessmentObservationSimple.OnlandVisualTrashAssessmentID, 
                    LocationPoint = locationPoint2771,
                    LocationPoint4326 = locationPoint4326,
                    Note = onlandVisualTrashAssessmentObservationSimple.Note, 
                    ObservationDatetime = onlandVisualTrashAssessmentObservationSimple.ObservationDateTime
                };
                if (onlandVisualTrashAssessmentObservationSimple.PhotoStagingID.HasValue)
                {
                    var photoStaging =
                        await dbContext.OnlandVisualTrashAssessmentObservationPhotoStagings.FindAsync(
                            onlandVisualTrashAssessmentObservationSimple.PhotoStagingID.Value);

                    var onlandVisualTrashAssessmentObservationPhoto = new OnlandVisualTrashAssessmentObservationPhoto
                    {
                        FileResourceID = photoStaging.FileResourceID,
                        OnlandVisualTrashAssessmentObservation = onlandVisualTrashAssessmentObservation
                    };
                    await dbContext.OnlandVisualTrashAssessmentObservationPhotos.AddAsync(onlandVisualTrashAssessmentObservationPhoto);
                }

                await dbContext.OnlandVisualTrashAssessmentObservations.AddAsync(
                    onlandVisualTrashAssessmentObservation);
            }

            //update
            foreach (var onlandVisualTrashAssessmentObservationSimple in observations.Where(x =>
                         ModelObjectHelpers.IsRealPrimaryKeyValue(x.OnlandVisualTrashAssessmentObservationID)))
            {
                var onlandVisualTrashAssessmentObservation = await dbContext.OnlandVisualTrashAssessmentObservations.FindAsync(onlandVisualTrashAssessmentObservationSimple.OnlandVisualTrashAssessmentObservationID);
                var locationPoint4326 = GeometryHelper.CreateLocationPoint4326FromLatLong(onlandVisualTrashAssessmentObservationSimple.LocationY.GetValueOrDefault(),
                    onlandVisualTrashAssessmentObservationSimple.LocationX.GetValueOrDefault());

                var locationPoint2771 = locationPoint4326.ProjectTo2771();
                onlandVisualTrashAssessmentObservation.LocationPoint = locationPoint2771;
                onlandVisualTrashAssessmentObservation.Note = onlandVisualTrashAssessmentObservationSimple.Note;
                onlandVisualTrashAssessmentObservation.ObservationDatetime = onlandVisualTrashAssessmentObservationSimple.ObservationDateTime;
                onlandVisualTrashAssessmentObservation.LocationPoint4326 = locationPoint4326;

                if (onlandVisualTrashAssessmentObservationSimple.PhotoStagingID.HasValue)
                {

                    var photoStaging = await dbContext.OnlandVisualTrashAssessmentObservationPhotoStagings.FindAsync(onlandVisualTrashAssessmentObservationSimple.PhotoStagingID.Value);
                    // ReSharper disable once ObjectCreationAsStatement
                    var onlandVisualTrashAssessmentObservationPhoto = new OnlandVisualTrashAssessmentObservationPhoto
                    {
                        FileResourceID = photoStaging.FileResourceID,
                        OnlandVisualTrashAssessmentObservationID = onlandVisualTrashAssessmentObservation
                            .OnlandVisualTrashAssessmentObservationID
                    };
                    await dbContext.OnlandVisualTrashAssessmentObservationPhotos.AddAsync(
                        onlandVisualTrashAssessmentObservationPhoto);
                }
            }

            await dbContext.SaveChangesAsync();

            await dbContext.OnlandVisualTrashAssessmentObservationPhotoStagings.Where(x => x.OnlandVisualTrashAssessmentID == ovta.OnlandVisualTrashAssessmentID).ExecuteDeleteAsync();
        }
    }
}
