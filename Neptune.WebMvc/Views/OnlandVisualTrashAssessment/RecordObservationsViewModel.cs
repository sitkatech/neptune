using Microsoft.EntityFrameworkCore;
using Neptune.Common;
using Neptune.EFModels.Entities;

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

        public RecordObservationsViewModel(Neptune.EFModels.Entities.OnlandVisualTrashAssessment onlandVisualTrashAssessment) : base(onlandVisualTrashAssessment)
        {
            Observations = onlandVisualTrashAssessment.OnlandVisualTrashAssessmentObservations
                .Select(x => new OnlandVisualTrashAssessmentObservationSimple(x)).ToList();
        }

        public async Task UpdateModel(NeptuneDbContext dbContext, Neptune.EFModels.Entities.OnlandVisualTrashAssessment ovta,
            DbSet<OnlandVisualTrashAssessmentObservation> allOnlandVisualTrashAssessmentObservations)
        {
            // this is a dict instead of the usual list so that we can permanentize the staged photos later.
            var updatedDict =
                Observations?.Select(x =>
                        new KeyValuePair<OnlandVisualTrashAssessmentObservationSimple,
                            OnlandVisualTrashAssessmentObservation>(x, x.ToOnlandVisualTrashAssessmentObservation()))
                    .ToList().ToDictionary(x => x.Key, x => x.Value) ??
                new Dictionary<OnlandVisualTrashAssessmentObservationSimple, OnlandVisualTrashAssessmentObservation>();

            ovta.OnlandVisualTrashAssessmentObservations.Merge(updatedDict.Values, allOnlandVisualTrashAssessmentObservations,
                (x, y) => x.OnlandVisualTrashAssessmentObservationID == y.OnlandVisualTrashAssessmentObservationID,
                (x, y) =>
                {
                    x.Note = y.Note;
                    x.LocationPoint = y.LocationPoint;
                    x.LocationPoint4326 = y.LocationPoint4326;
                });

            foreach (var x in updatedDict)
            {
                var dto = x.Key;
                // have to do this weird lookup otherwise line 63 will create a brand new ovtao
                var entityID = x.Value.OnlandVisualTrashAssessmentObservationID;
                var actualEntity = await dbContext.OnlandVisualTrashAssessmentObservations.FindAsync(entityID);
                if (!dto.PhotoStagingID.HasValue)
                {
                    return; // no one cares
                }

                var photoStaging = ((OnlandVisualTrashAssessmentObservationPhotoStagingPrimaryKey)dto.PhotoStagingID.Value).EntityObject;


                // ReSharper disable once ObjectCreationAsStatement
                new OnlandVisualTrashAssessmentObservationPhoto
                {
                    FileResource = photoStaging.FileResource,
                    OnlandVisualTrashAssessmentObservation = actualEntity
                };
            }

            await dbContext.SaveChangesAsync();

            await dbContext.OnlandVisualTrashAssessmentObservationPhotoStagings.Where(x => x.OnlandVisualTrashAssessmentID == ovta.OnlandVisualTrashAssessmentID).ExecuteDeleteAsync();
        }
    }
}
