using Neptune.Web.Models;

namespace Neptune.Web.Views.FieldVisit
{
    public class BaseCollectionMethodFormViewData : FieldVisitSectionViewData
    {
        public BaseCollectionMethodFormViewData(Person currentPerson, Models.FieldVisit fieldVisit,
            Models.FieldVisitSection fieldVisitSection, Models.TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType) : base(currentPerson, fieldVisit, fieldVisitSection)
        {
            SubsectionName = treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeName;
            SectionHeader = $"{SectionHeader} - {SubsectionName}";
        }
    }
}
