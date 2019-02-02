using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using LtInfo.Common.DesignByContract;
using MoreLinq;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
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

        public RecordObservationsViewModel(Models.OnlandVisualTrashAssessment ovta) : base(ovta)
        {
            Observations = ovta.OnlandVisualTrashAssessmentObservations
                .Select(x => new OnlandVisualTrashAssessmentObservationSimple(x)).ToList();
        }

        public void UpdateModel(Models.OnlandVisualTrashAssessment ovta,
            ObservableCollection<OnlandVisualTrashAssessmentObservation> allOnlandVisualTrashAssessmentObservations)
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
                    //location can only be changed by deleting and redropping the pin, so we only need to update the note when matched
                    x.Note = y.Note;
                });

            updatedDict.ForEach(x =>
            {
                var dto = x.Key;
                var entity = x.Value;
                if (!dto.PhotoStagingID.HasValue)
                {
                    return; // no one cares
                }

                var photoStaging = ((OnlandVisualTrashAssessmentObservationPhotoStagingPrimaryKey) dto.PhotoStagingID.Value).EntityObject;

                // We don't care about OCAS because EF will add the created entity to our changeset regardless
                // ReSharper disable once ObjectCreationAsStatement
                new OnlandVisualTrashAssessmentObservationPhoto(photoStaging.FileResource, entity);
            });

            HttpRequestStorage.DatabaseEntities.SaveChanges();

            HttpRequestStorage.DatabaseEntities.OnlandVisualTrashAssessmentObservationPhotoStagings
                .DeleteOnlandVisualTrashAssessmentObservationPhotoStaging(ovta
                    .OnlandVisualTrashAssessmentObservationPhotoStagings);
        }
    }
}
