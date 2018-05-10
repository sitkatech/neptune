using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMPAssessment
{
    public class BaseCollectionMethodFormViewModel : CollectionMethodSectionViewModel
    {
        public BaseCollectionMethodFormViewModel()
        {
        }

        public BaseCollectionMethodFormViewModel(TreatmentBMPObservation treatmentBMPObservation,
            Models.TreatmentBMPAssessmentObservationType TreatmentBMPAssessmentObservationType) : base(treatmentBMPObservation, TreatmentBMPAssessmentObservationType)
        {
        }
    }
}
