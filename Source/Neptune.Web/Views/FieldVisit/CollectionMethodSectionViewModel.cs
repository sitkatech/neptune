using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LtInfo.Common.Models;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.Views.FieldVisit
{
    public class CollectionMethodSectionViewModel : FieldVisitSectionViewModel, IValidatableObject
    {
        public int? TreatmentBMPAssessmentID { get; set; }
        public int? TreatmentBMPAssessmentObservationTypeID { get; set; }
        public string ObservationData { get; set; }

        protected CollectionMethodSectionViewModel()
        {
        }

        protected CollectionMethodSectionViewModel(TreatmentBMPObservation treatmentBMPObservation, Models.TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType)
        {
            TreatmentBMPAssessmentID = treatmentBMPObservation?.TreatmentBMPAssessmentID;
            TreatmentBMPAssessmentObservationTypeID = treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID;
            ObservationData = treatmentBMPObservation?.ObservationData;
        }

        public void UpdateModel(TreatmentBMPObservation treatmentBMPObservation)
        {
            treatmentBMPObservation.ObservationData = ObservationData;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            var treatmentBMPAssessmentObservationType =
                HttpRequestStorage.DatabaseEntities.TreatmentBMPAssessmentObservationTypes.SingleOrDefault(x =>
                    x.TreatmentBMPAssessmentObservationTypeID == TreatmentBMPAssessmentObservationTypeID);
            var observationTypeCollectionMethod = ObservationTypeCollectionMethod.AllLookupDictionary[treatmentBMPAssessmentObservationType.ObservationTypeSpecification.ObservationTypeCollectionMethodID];
            validationResults.AddRange(observationTypeCollectionMethod.ValidateObservationDataJson(ObservationData));

            return validationResults;
        }
    }
}
