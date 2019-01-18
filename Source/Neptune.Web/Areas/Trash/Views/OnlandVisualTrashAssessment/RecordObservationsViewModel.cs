using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using LtInfo.Common.DesignByContract;
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
            ICollection<OnlandVisualTrashAssessmentObservation> updatedList =
                Observations?.Select(x => x.ToOnlandVisualTrashAssessmentObservation()).ToList() ??
                new List<OnlandVisualTrashAssessmentObservation>();

            ovta.OnlandVisualTrashAssessmentObservations.Merge(updatedList, allOnlandVisualTrashAssessmentObservations,
                (x, y) => x.OnlandVisualTrashAssessmentObservationID == y.OnlandVisualTrashAssessmentObservationID,
                (x, y) =>
                {
                    //location can only be changed by deleting and redropping the pin, so we only need to update the note when matched
                    x.Note = y.Note;
                });
        }
    }
}
