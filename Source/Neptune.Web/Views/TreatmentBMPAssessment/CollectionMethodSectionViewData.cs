using System;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMPAssessment
{
    public class CollectionMethodSectionViewData : AssessmentSectionViewData
    {
        public readonly ObservationTypeCollectionMethod ObservationTypeCollectionMethod;
        public readonly BaseCollectionMethodFormViewData ObservationPartialViewData;

        public CollectionMethodSectionViewData(Person currentPerson, Models.TreatmentBMPAssessment treatmentBMPAssessment, ObservationTypeCollectionMethod observationTypeCollectionMethod, Models.TreatmentBMPAssessmentObservationType TreatmentBMPAssessmentObservationType)
            : base(currentPerson, treatmentBMPAssessment, TreatmentBMPAssessmentObservationType.ObservationTypeName)
        {
            ObservationTypeCollectionMethod = observationTypeCollectionMethod;
            switch (observationTypeCollectionMethod.ToEnum)
            {
                case ObservationTypeCollectionMethodEnum.DiscreteValue:
                    ObservationPartialViewData = new DiscreteCollectionMethodViewData(treatmentBMPAssessment, TreatmentBMPAssessmentObservationType);
                    break;
                case ObservationTypeCollectionMethodEnum.PassFail:
                    ObservationPartialViewData = new PassFailCollectionMethodViewData(treatmentBMPAssessment, TreatmentBMPAssessmentObservationType);
                    break;
                case ObservationTypeCollectionMethodEnum.Percentage:
                    ObservationPartialViewData = new PercentageCollectionMethodViewData(treatmentBMPAssessment, TreatmentBMPAssessmentObservationType);
                    break;
                case ObservationTypeCollectionMethodEnum.Rate:
                    ObservationPartialViewData = new RateCollectionMethodViewData(treatmentBMPAssessment, TreatmentBMPAssessmentObservationType);
                    break;
                default:
                    throw new ArgumentException(
                        $"Observation Type Collection Method {observationTypeCollectionMethod.ObservationTypeCollectionMethodDisplayName} is not supported by the Observation View Data.");
            }
        }
    }
}
