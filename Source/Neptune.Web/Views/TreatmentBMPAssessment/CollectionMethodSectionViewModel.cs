﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LtInfo.Common.Models;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMPAssessment
{
    public class CollectionMethodSectionViewModel : AssessmentSectionViewModel
    {
        public int? TreatmentBMPAssessmentID { get; set; }
        public int? TreatmentBMPAssessmentObservationTypeID { get; set; }
        public string ObservationData { get; set; }

        protected CollectionMethodSectionViewModel()
        {
        }

        protected CollectionMethodSectionViewModel(TreatmentBMPObservation treatmentBMPObservation, Models.TreatmentBMPAssessmentObservationType TreatmentBMPAssessmentObservationType)
        {
            TreatmentBMPAssessmentID = treatmentBMPObservation?.TreatmentBMPAssessmentID;
            TreatmentBMPAssessmentObservationTypeID = TreatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID;
            ObservationData = treatmentBMPObservation?.ObservationData;
        }

        public void UpdateModel(TreatmentBMPObservation treatmentBMPObservation)
        {
            treatmentBMPObservation.ObservationData = ObservationData;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            var TreatmentBMPAssessmentObservationType =
                HttpRequestStorage.DatabaseEntities.TreatmentBMPAssessmentObservationTypes.SingleOrDefault(x =>
                    x.TreatmentBMPAssessmentObservationTypeID == TreatmentBMPAssessmentObservationTypeID);
            var observationTypeCollectionMethod = ObservationTypeCollectionMethod.AllLookupDictionary[TreatmentBMPAssessmentObservationType.ObservationTypeSpecification.ObservationTypeCollectionMethodID];
            if (!observationTypeCollectionMethod.ValidateObservationDataJson(ObservationData))
            {
                validationResults.Add(new ValidationResult("Schema invalid."));
            }

            return validationResults;
        }
    }
}
