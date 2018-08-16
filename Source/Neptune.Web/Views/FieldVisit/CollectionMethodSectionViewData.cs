using Neptune.Web.Models;

namespace Neptune.Web.Views.FieldVisit
{
    public class CollectionMethodSectionViewData : FieldVisitSectionViewData
    {
        public bool ForObservationTypePreview { get; }

        /// <summary>
        /// Only called when we're invoking a derived type from the PreviewObservationType route
        /// </summary>
        /// <param name="currentPerson"></param>
        public CollectionMethodSectionViewData(Person currentPerson) : base(currentPerson)
        {
            ForObservationTypePreview = true;
        }

        public CollectionMethodSectionViewData(Person currentPerson, Models.FieldVisit fieldVisit,
            Models.FieldVisitSection fieldVisitSection, Models.TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType) : base(currentPerson, fieldVisit, fieldVisitSection)
        {
            SubsectionName = treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeName;
            SectionHeader = $"{SectionHeader} - {SubsectionName}";
            ForObservationTypePreview = false;
        }
    }
}
