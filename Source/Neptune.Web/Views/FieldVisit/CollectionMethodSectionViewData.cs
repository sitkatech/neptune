using System;
using Neptune.Web.Controllers;
using Neptune.Web.Models;

namespace Neptune.Web.Views.FieldVisit
{
    public class CollectionMethodSectionViewData : FieldVisitSectionViewData
    {
        public readonly ObservationTypeCollectionMethod ObservationTypeCollectionMethod;
        public readonly BaseCollectionMethodFormViewData ObservationPartialViewData;

        public CollectionMethodSectionViewData(Person currentPerson, Models.FieldVisit treatmentBMPAssessment,
            ObservationTypeCollectionMethod observationTypeCollectionMethod,
            Models.TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType,
            FieldVisitAssessmentType fieldVisitAssessmentType)
            : base(currentPerson, treatmentBMPAssessment,
                fieldVisitAssessmentType == FieldVisitAssessmentType.Initial ? (Models.FieldVisitSection) Models.FieldVisitSection.Assessment : Models.FieldVisitSection.PostMaintenanceAssessment)
        {
            ObservationTypeCollectionMethod = observationTypeCollectionMethod;
            switch (observationTypeCollectionMethod.ToEnum)
            {
                case ObservationTypeCollectionMethodEnum.DiscreteValue:
                    ObservationPartialViewData = new DiscreteCollectionMethodViewData(treatmentBMPAssessment,
                        treatmentBMPAssessmentObservationType, fieldVisitAssessmentType);
                    break;
                case ObservationTypeCollectionMethodEnum.PassFail:
                    ObservationPartialViewData = new PassFailCollectionMethodViewData(treatmentBMPAssessment,
                        treatmentBMPAssessmentObservationType, fieldVisitAssessmentType);
                    break;
                case ObservationTypeCollectionMethodEnum.Percentage:
                    ObservationPartialViewData = new PercentageCollectionMethodViewData(treatmentBMPAssessment,
                        treatmentBMPAssessmentObservationType, fieldVisitAssessmentType);
                    break;
                case ObservationTypeCollectionMethodEnum.Rate:
                    ObservationPartialViewData = new RateCollectionMethodViewData(treatmentBMPAssessment,
                        treatmentBMPAssessmentObservationType, fieldVisitAssessmentType);
                    break;
                default:
                    throw new ArgumentException(
                        $"Observation Type Collection Method {observationTypeCollectionMethod.ObservationTypeCollectionMethodDisplayName} is not supported by the Observation View Data.");
            }
        }
    }
}