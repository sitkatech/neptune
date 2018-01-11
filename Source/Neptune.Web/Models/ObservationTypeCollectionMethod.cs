using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LtInfo.Common.DesignByContract;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Views.ObservationType;
using Newtonsoft.Json;

namespace Neptune.Web.Models
{
    public partial class ObservationTypeCollectionMethod
    {
        public abstract bool ValidateObservationTypeJson(string json);
        public abstract List<ValidationResult> ValidateObservationType(string json);
        public abstract bool ValidateObservationDataJson(string json);

        public abstract string ViewSchemaDetailUrl(ObservationType observationType);
        public abstract string GetAssessmentUrl(TreatmentBMPAssessment treatmentBMPAssessment, ObservationType observationType);

        public abstract double? GetObservationValueFromObservationData(string observationData);

        public abstract double? CalculateScore(TreatmentBMPObservation treatmentBMPObservation);
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

        public override List<ValidationResult> ValidateObservationType(string json)
        {
            var validationErrors = new List<ValidationResult>();
            DiscreteObservationTypeSchema schema = JsonConvert.DeserializeObject<DiscreteObservationTypeSchema>(json);

            var propertiesToObserve = schema.PropertiesToObserve;
            ObservationTypeHelper.ValidatePropertiesToObserve(propertiesToObserve, validationErrors);
            ObservationTypeHelper.ValidateNumberOfObservations(schema.MinimumNumberOfObservations, schema.MaximumNumberOfObservations, validationErrors);
            ObservationTypeHelper.ValidateValueOfObservations(schema.MinimumValueOfObservations, schema.MaximumValueOfObservations, validationErrors);

            return validationErrors;
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

        public override double? CalculateScore(TreatmentBMPObservation treatmentBMPObservation)
        {            
            var observationValue = GetObservationValueFromObservationData(treatmentBMPObservation.ObservationData);
            var benchmarkValue = treatmentBMPObservation.ObservationType.GetBenchmarkValue(treatmentBMPObservation.TreatmentBMPAssessment.TreatmentBMP);
            var thresholdValue = treatmentBMPObservation.ObservationType.GetThresholdValue(treatmentBMPObservation.TreatmentBMPAssessment.TreatmentBMP);
            var thresholdValueInBenchmarkUnits = treatmentBMPObservation.ObservationType.GetThresholdValueInBenchmarkUnits(benchmarkValue, thresholdValue, treatmentBMPObservation.ObservationType.ThresholdMeasurementUnitType() == MeasurementUnitType.PercentIncrease);

            if (observationValue == null || benchmarkValue == null || thresholdValueInBenchmarkUnits == null)
            {
                return null;
            }

            return ObservationTypeHelper.LinearInterpolation(observationValue.Value, benchmarkValue.Value, thresholdValueInBenchmarkUnits.Value);
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

        public override List<ValidationResult> ValidateObservationType(string json)
        {
            var validationErrors = new List<ValidationResult>();
            RateObservationTypeSchema schema = JsonConvert.DeserializeObject<RateObservationTypeSchema>(json);

            var propertiesToObserve = schema.PropertiesToObserve;
            ObservationTypeHelper.ValidatePropertiesToObserve(propertiesToObserve, validationErrors);
            ObservationTypeHelper.ValidateNumberOfObservations(schema.DiscreteRateMinimumNumberOfObservations, schema.DiscreteRateMaximumNumberOfObservations, validationErrors);
            ObservationTypeHelper.ValidateNumberOfObservations(schema.TimeReadingMinimumNumberOfObservations, schema.TimeReadingMaximumNumberOfObservations, validationErrors);
            ObservationTypeHelper.ValidateValueOfObservations(schema.DiscreteRateMinimumValueOfObservations, schema.DiscreteRateMaximumValueOfObservations, validationErrors);
            ObservationTypeHelper.ValidateValueOfObservations(schema.TimeReadingMinimumValueOfObservations, schema.TimeReadingMaximumValueOfObservations, validationErrors);

            return validationErrors;
        }

        public override bool ValidateObservationDataJson(string json)
        {

            try
            {
                RateObservationSchema schema = JsonConvert.DeserializeObject<RateObservationSchema>(json);
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
            return SitkaRoute<TreatmentBMPAssessmentController>.BuildUrlFromExpression(c => c.RateCollectionMethod(treatmentBMPAssessment, observationType));
        }

        public override double? GetObservationValueFromObservationData(string observationData)
        {
            RateObservationSchema observation = JsonConvert.DeserializeObject<RateObservationSchema>(observationData);
            return 0; //todo
        }

        public override double? CalculateScore(TreatmentBMPObservation treatmentBMPObservation)
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

        public override List<ValidationResult> ValidateObservationType(string json)
        {
            var validationErrors = new List<ValidationResult>();
            PassFailObservationTypeSchema schema = JsonConvert.DeserializeObject<PassFailObservationTypeSchema>(json);

            var propertiesToObserve = schema.PropertiesToObserve;
            ObservationTypeHelper.ValidatePropertiesToObserve(propertiesToObserve, validationErrors);

            return validationErrors;
        }

        public override bool ValidateObservationDataJson(string json)
        {

            try
            {
                PassFailObservationSchema schema = JsonConvert.DeserializeObject<PassFailObservationSchema>(json);
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
            return SitkaRoute<TreatmentBMPAssessmentController>.BuildUrlFromExpression(c => c.PassFailCollectionMethod(treatmentBMPAssessment, observationType));
        }

        public override double? GetObservationValueFromObservationData(string observationData)
        {
            PassFailObservationSchema observation = JsonConvert.DeserializeObject<PassFailObservationSchema>(observationData);
            return 0; //todo
        }

        public override double? CalculateScore(TreatmentBMPObservation treatmentBMPObservation)
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

        public override List<ValidationResult> ValidateObservationType(string json)
        {
            var validationErrors = new List<ValidationResult>();
            PercentageObservationTypeSchema schema = JsonConvert.DeserializeObject<PercentageObservationTypeSchema>(json);

            var propertiesToObserve = schema.PropertiesToObserve;
            ObservationTypeHelper.ValidatePropertiesToObserve(propertiesToObserve, validationErrors);

            return validationErrors;
        }

        public override bool ValidateObservationDataJson(string json)
        {
            try
            {
                PercentageObservationSchema schema = JsonConvert.DeserializeObject<PercentageObservationSchema>(json);
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
            return SitkaRoute<TreatmentBMPAssessmentController>.BuildUrlFromExpression(c => c.PercentageCollectionMethod(treatmentBMPAssessment, observationType));
        }

        public override double? GetObservationValueFromObservationData(string observationData)
        {
            PercentageObservationSchema observation = JsonConvert.DeserializeObject<PercentageObservationSchema>(observationData);
            return 0; //todo
        }

        public override double? CalculateScore(TreatmentBMPObservation treatmentBMPObservation)
        {
            throw new NotImplementedException();
        }
    }
}