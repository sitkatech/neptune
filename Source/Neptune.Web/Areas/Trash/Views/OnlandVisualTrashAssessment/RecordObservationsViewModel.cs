using System.Collections.Generic;
using System.Linq;
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

        public RecordObservationsViewModel(Models.OnlandVisualTrashAssessment ovta)
        {
            Observations = ovta.OnlandVisualTrashAssessmentObservations
                .Select(x => new OnlandVisualTrashAssessmentObservationSimple(x)).ToList();
        }

    }
}