using System;
using System.ComponentModel.DataAnnotations;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Views.ObservationType;
using Newtonsoft.Json;

namespace Neptune.Web.Models
{
    public partial class ObservationTypeCollectionMethod
    {
        public abstract bool ValidateJson(string json);

        public abstract string ViewSchemaDetailUrl(ObservationType observationType);
        public abstract string GetAssessmentUrl(TreatmentBMPAssessment treatmentBMPAssessment, ObservationType observationType);
    }

    public partial class ObservationTypeCollectionMethodDiscreteValue
    {
        public override bool ValidateJson(string json)
        {
            try
            {
                DiscreteObservationTypeSchema schema = JsonConvert.DeserializeObject<DiscreteObservationTypeSchema>(json);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public override string ViewSchemaDetailUrl(ObservationType observationType)
        {
            return SitkaRoute<ObservationTypeController>.BuildUrlFromExpression(c => c.DiscreteDetailSchema(observationType));
        }

        public override string GetAssessmentUrl(TreatmentBMPAssessment treatmentBMPAssessment,
            ObservationType observationType)
        {
            return SitkaRoute<TreatmentBMPAssessmentController>.BuildUrlFromExpression(c => c.DiscreteCollectionMethod(treatmentBMPAssessment, observationType));
        }
    }

    public partial class ObservationTypeCollectionMethodRate
    {
        public override bool ValidateJson(string json)
        {
            try
            {
                RateObservationTypeSchema schema = JsonConvert.DeserializeObject<RateObservationTypeSchema>(json);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
        public override string ViewSchemaDetailUrl(ObservationType observationType)
        {
            return SitkaRoute<ObservationTypeController>.BuildUrlFromExpression(c => c.RateDetailSchema(observationType));
        }

        public override string GetAssessmentUrl(TreatmentBMPAssessment treatmentBMPAssessment,
            ObservationType observationType)
        {
            throw new NotImplementedException();
        }
    }

    public partial class ObservationTypeCollectionMethodPassFail
    {
        public override bool ValidateJson(string json)
        {
            try
            {
                PassFailObservationTypeSchema schema = JsonConvert.DeserializeObject<PassFailObservationTypeSchema>(json);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public override string ViewSchemaDetailUrl(ObservationType observationType)
        {
            return SitkaRoute<ObservationTypeController>.BuildUrlFromExpression(c => c.PassFailDetailSchema(observationType));
        }

        public override string GetAssessmentUrl(TreatmentBMPAssessment treatmentBMPAssessment,
            ObservationType observationType)
        {
            throw new NotImplementedException();
        }
    }

    public partial class ObservationTypeCollectionMethodPercentage
    {
        public override bool ValidateJson(string json)
        {
            try
            {
                PercentageObservationTypeSchema schema = JsonConvert.DeserializeObject<PercentageObservationTypeSchema>(json);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
        public override string ViewSchemaDetailUrl(ObservationType observationType)
        {
            return SitkaRoute<ObservationTypeController>.BuildUrlFromExpression(c => c.PercentageDetailSchema(observationType));
        }

        public override string GetAssessmentUrl(TreatmentBMPAssessment treatmentBMPAssessment,
            ObservationType observationType)
        {
            throw new NotImplementedException();
        }
    }
}