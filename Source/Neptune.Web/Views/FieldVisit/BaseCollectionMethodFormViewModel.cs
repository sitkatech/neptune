using Neptune.Web.Models;

namespace Neptune.Web.Views.FieldVisit
{
    public class BaseCollectionMethodFormViewModel : CollectionMethodSectionViewModel
    {
        public BaseCollectionMethodFormViewModel()
        {
        }

        public BaseCollectionMethodFormViewModel(TreatmentBMPObservation treatmentBMPObservation,
            Models.TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType) : base(treatmentBMPObservation, treatmentBMPAssessmentObservationType)
        {
        }
    }
}
