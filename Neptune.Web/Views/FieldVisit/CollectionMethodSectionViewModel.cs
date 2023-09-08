using System.ComponentModel.DataAnnotations;
using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Common.Models;

namespace Neptune.Web.Views.FieldVisit
{
    public class CollectionMethodSectionViewModel : FormViewModel, IValidatableObject
    {
        [Required]
        public int? TreatmentBMPAssessmentObservationTypeID { get; set; }
        public string ObservationData { get; set; }

        public CollectionMethodSectionViewModel()
        {
        }

        public CollectionMethodSectionViewModel(TreatmentBMPObservation treatmentBMPObservation, EFModels.Entities.TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType)
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
            var dbContext = validationContext.GetService<NeptuneDbContext>();

            var validationResults = new List<ValidationResult>();

            var treatmentBMPAssessmentObservationType = TreatmentBMPAssessmentObservationTypes.GetByID(dbContext, TreatmentBMPAssessmentObservationTypeID);

            var observationTypeCollectionMethod = ObservationTypeCollectionMethod.AllLookupDictionary[treatmentBMPAssessmentObservationType.ObservationTypeSpecification.ObservationTypeCollectionMethodID];

            validationResults.AddRange(observationTypeCollectionMethod.ValidateObservationDataJson(treatmentBMPAssessmentObservationType, ObservationData));

            return validationResults;
        }
    }
}
