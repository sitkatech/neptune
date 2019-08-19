using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Spatial;
using System.Linq;
using LtInfo.Common;
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
                    x.Note = y.Note;
                    // as we know, when we come FROM the browser, we normally transform TO state plane
                    // not here though, since Observations don't actually get used in any spatial analysis and therefore the projection doesn't matter.
                    x.LocationPoint = CoordinateSystemHelper.ProjectWebMercatorToCaliforniaStatePlaneVI(y.LocationPoint);
                });

            updatedDict.ForEach(x =>
            {
                var dto = x.Key;
                // have to do this weird lookup otherwise line 63 will create a brand new ovtao
                var entityID = x.Value.OnlandVisualTrashAssessmentObservationID;
                var actualEntity = HttpRequestStorage.DatabaseEntities.OnlandVisualTrashAssessmentObservations.Find(entityID);
                if (!dto.PhotoStagingID.HasValue)
                {
                    return; // no one cares
                }

                var photoStaging = ((OnlandVisualTrashAssessmentObservationPhotoStagingPrimaryKey) dto.PhotoStagingID.Value).EntityObject;


                // ReSharper disable once ObjectCreationAsStatement
                new OnlandVisualTrashAssessmentObservationPhoto(photoStaging.FileResource, actualEntity);
                
            });

            HttpRequestStorage.DatabaseEntities.SaveChanges();

            HttpRequestStorage.DatabaseEntities.OnlandVisualTrashAssessmentObservationPhotoStagings
                .DeleteOnlandVisualTrashAssessmentObservationPhotoStaging(ovta
                    .OnlandVisualTrashAssessmentObservationPhotoStagings);
        }
    }
}
