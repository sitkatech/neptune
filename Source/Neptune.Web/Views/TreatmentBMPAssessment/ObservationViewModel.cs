using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LtInfo.Common.Models;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMPAssessment
{
    public abstract class ObservationViewModel : FormViewModel, IValidatableObject
    {
        public int? TreatmentBMPAssessmentID { get; set; }
        public int? ObservationTypeID { get; set; }
        public string ObservationData { get; set; }

        protected ObservationViewModel()
        {
        }

        protected ObservationViewModel(TreatmentBMPObservation treatmentBMPObservation, Models.ObservationType observationType)
        {
            TreatmentBMPAssessmentID = treatmentBMPObservation?.TreatmentBMPAssessmentID;
            ObservationTypeID = observationType.ObservationTypeID;
            ObservationData = treatmentBMPObservation?.ObservationData;
        }

        public void UpdateModel(TreatmentBMPObservation treatmentBMPObservation)
        {
            treatmentBMPObservation.ObservationData = ObservationData;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            var observationType =
                HttpRequestStorage.DatabaseEntities.ObservationTypes.SingleOrDefault(x =>
                    x.ObservationTypeID == ObservationTypeID);
            var observationTypeCollectionMethod = ObservationTypeCollectionMethod.AllLookupDictionary[observationType.ObservationTypeSpecification.ObservationTypeCollectionMethodID];
            if (!observationTypeCollectionMethod.ValidateObservationDataJson(ObservationData))
            {
                validationResults.Add(new ValidationResult("Schema invalid."));
            }

            return validationResults;
        }
    }
}
