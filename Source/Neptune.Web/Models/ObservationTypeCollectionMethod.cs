using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Views.ObservationType;
using Newtonsoft.Json;

namespace Neptune.Web.Models
{
    public partial class ObservationTypeCollectionMethod
    {
        public abstract bool ValidateObservationTypeJson(string json);
        public abstract bool ValidateObservationDataJson(string json);

        public abstract string ViewSchemaDetailUrl(ObservationType observationType);
        public abstract string GetAssessmentUrl(TreatmentBMPAssessment treatmentBMPAssessment, ObservationType observationType);

        public abstract double? GetObservationValueFromObservationData(string observationData);
    }

    public partial class ObservationTypeCollectionMethodDiscreteValue
    {
        public override bool ValidateObservationTypeJson(string json)
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

        public override bool ValidateObservationDataJson(string json)
        {
            try
            {
                DiscreteObservationSchema schema = JsonConvert.DeserializeObject<DiscreteObservationSchema>(json);
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

        public override double? GetObservationValueFromObservationData(string observationData)
        {
            DiscreteObservationSchema observation = JsonConvert.DeserializeObject<DiscreteObservationSchema>(observationData);
            return observation.SingleValueObservations.Average(x => x.ObservationValue);
        }
    }

    public partial class ObservationTypeCollectionMethodRate
    {
        public override bool ValidateObservationTypeJson(string json)
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

        public override bool ValidateObservationDataJson(string json)
        {
            throw new NotImplementedException();
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

        public override double? GetObservationValueFromObservationData(string observationData)
        {
            throw new NotImplementedException();
        }
    }

    public partial class ObservationTypeCollectionMethodPassFail
    {
        public override bool ValidateObservationTypeJson(string json)
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

        public override bool ValidateObservationDataJson(string json)
        {
            throw new NotImplementedException();
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

        public override double? GetObservationValueFromObservationData(string observationData)
        {
            throw new NotImplementedException();
        }
    }

    public partial class ObservationTypeCollectionMethodPercentage
    {
        public override bool ValidateObservationTypeJson(string json)
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

        public override bool ValidateObservationDataJson(string json)
        {
            throw new NotImplementedException();
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

        public override double? GetObservationValueFromObservationData(string observationData)
        {
            throw new NotImplementedException();
        }
    }
}