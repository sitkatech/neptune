using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LtInfo.Common.Models;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.Views.FieldVisit
{
    public class CollectionMethodSectionViewModel : FormViewModel, IValidatableObject
    {
        public int? TreatmentBMPAssessmentObservationTypeID { get; set; }
        public string ObservationData { get; set; }

        public CollectionMethodSectionViewModel()
        {
        }

        public CollectionMethodSectionViewModel(TreatmentBMPObservation treatmentBMPObservation, Models.TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType)
        {
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

            // TODO: There should probably be a null check here, or a required attribute on the TBMPAOTID field
            var observationTypeCollectionMethod = ObservationTypeCollectionMethod.AllLookupDictionary[treatmentBMPAssessmentObservationType.ObservationTypeSpecification.ObservationTypeCollectionMethodID];

            validationResults.AddRange(observationTypeCollectionMethod.ValidateObservationDataJson(treatmentBMPAssessmentObservationType, ObservationData));

            return validationResults;
        }
    }
}
