﻿using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Controllers;

namespace Neptune.Web.Models;

public static class ObservationTypeCollectionMethodModelExtensions
{
    public static string ViewSchemaDetailUrl(this ObservationTypeCollectionMethod observationTypeCollectionMethod,
        TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType, LinkGenerator linkGenerator)
    {
        var observationTypeCollectionMethodEnum = observationTypeCollectionMethod.ToEnum;
        switch(observationTypeCollectionMethodEnum)
        {
            case ObservationTypeCollectionMethodEnum.DiscreteValue:
                return SitkaRoute<TreatmentBMPAssessmentObservationTypeController>.BuildUrlFromExpression(linkGenerator, x => x.DiscreteDetailSchema(treatmentBMPAssessmentObservationType));
            case ObservationTypeCollectionMethodEnum.PassFail:
                return SitkaRoute<TreatmentBMPAssessmentObservationTypeController>.BuildUrlFromExpression(linkGenerator, x => x.PassFailDetailSchema(treatmentBMPAssessmentObservationType));
            case ObservationTypeCollectionMethodEnum.Percentage:
                return SitkaRoute<TreatmentBMPAssessmentObservationTypeController>.BuildUrlFromExpression(linkGenerator, x => x.PercentageDetailSchema(treatmentBMPAssessmentObservationType));
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

}