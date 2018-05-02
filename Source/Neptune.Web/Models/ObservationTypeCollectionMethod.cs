using System;
using System.Collections.Generic;
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
        public abstract List<ValidationResult> ValidateObservationType(string json);
        public abstract bool ValidateObservationDataJson(string json);

        public abstract string ViewSchemaDetailUrl(ObservationType observationType);
        public abstract string GetAssessmentUrl(TreatmentBMPAssessment treatmentBMPAssessment, ObservationType observationType);

        public abstract double? GetObservationValueFromObservationData(string observationData);

        public abstract double? CalculateScore(TreatmentBMPObservation treatmentBMPObservation);

        public virtual string CalculateOverrideScoreText(string assessmentScoreIfFailing, string observationTypeSchema, bool overrideAssessmentScoreIfFailing)
        {
            return string.Empty;
        }
    }

    public partial class ObservationTypeCollectionMethodDiscreteValue
    {
        public override bool ValidateObservationTypeJson(string json)
        {
            try
            {
                var schema = JsonConvert.DeserializeObject<DiscreteObservationTypeSchema>(json);
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
            var schema = JsonConvert.DeserializeObject<DiscreteObservationTypeSchema>(json);

            var propertiesToObserve = schema.PropertiesToObserve;
            ObservationTypeHelper.ValidatePropertiesToObserve(propertiesToObserve, validationErrors);
            ObservationTypeHelper.ValidateNumberOfObservations(schema.MinimumNumberOfObservations, schema.MaximumNumberOfObservations, validationErrors);
            ObservationTypeHelper.ValidateValueOfObservations(schema.MinimumValueOfObservations, schema.MaximumValueOfObservations, validationErrors);
            ObservationTypeHelper.ValidateMeasurementUnitLabel(schema.MeasurementUnitLabel, validationErrors);
            ObservationTypeHelper.ValidateMeasurementUnitTypeID(schema.MeasurementUnitTypeID, validationErrors);
            ObservationTypeHelper.ValidateAssessmentInstructions(schema.AssessmentDescription, validationErrors);
            ObservationTypeHelper.ValidateBenchmarkAndThresholdDescription(schema.BenchmarkDescription, schema.ThresholdDescription, validationErrors);

            return validationErrors;
        }

        public override bool ValidateObservationDataJson(string json)
        {
            try
            {
                var schema = JsonConvert.DeserializeObject<DiscreteObservationSchema>(json);
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
            var observation = JsonConvert.DeserializeObject<DiscreteObservationSchema>(observationData);
            return observation.SingleValueObservations.Average(x => x.ObservationValue);
        }

        public override double? CalculateScore(TreatmentBMPObservation treatmentBMPObservation)
        {            
            var observationValue = GetObservationValueFromObservationData(treatmentBMPObservation.ObservationData);
            var benchmarkValue = treatmentBMPObservation.ObservationType.GetBenchmarkValue(treatmentBMPObservation.TreatmentBMPAssessment.TreatmentBMP);
            var thresholdValue = treatmentBMPObservation.ObservationType.GetThresholdValue(treatmentBMPObservation.TreatmentBMPAssessment.TreatmentBMP);
            var useUpperValue = treatmentBMPObservation.ObservationType.ThresholdMeasurementUnitType() == MeasurementUnitType.PercentIncrease || (treatmentBMPObservation.ObservationType.ThresholdMeasurementUnitType() == MeasurementUnitType.PercentDeviation && observationValue > benchmarkValue);
            var thresholdValueInBenchmarkUnits = treatmentBMPObservation.ObservationType.GetThresholdValueInBenchmarkUnits(benchmarkValue, thresholdValue, useUpperValue);

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
                var schema = JsonConvert.DeserializeObject<RateObservationTypeSchema>(json);
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
            var schema = JsonConvert.DeserializeObject<RateObservationTypeSchema>(json);

            var propertiesToObserve = schema.PropertiesToObserve;
            ObservationTypeHelper.ValidatePropertiesToObserve(propertiesToObserve, validationErrors);
            ObservationTypeHelper.ValidateNumberOfObservations(schema.DiscreteRateMinimumNumberOfObservations, schema.DiscreteRateMaximumNumberOfObservations, validationErrors);
            ObservationTypeHelper.ValidateNumberOfObservations(schema.TimeReadingMinimumNumberOfObservations, schema.TimeReadingMaximumNumberOfObservations, validationErrors);
            ObservationTypeHelper.ValidateValueOfObservations(schema.DiscreteRateMinimumValueOfObservations, schema.DiscreteRateMaximumValueOfObservations, validationErrors);
            ObservationTypeHelper.ValidateValueOfObservations(schema.TimeReadingMinimumValueOfObservations, schema.TimeReadingMaximumValueOfObservations, validationErrors);
            ObservationTypeHelper.ValidateMeasurementUnitLabel(schema.DiscreteRateMeasurementUnitLabel, validationErrors);
            ObservationTypeHelper.ValidateMeasurementUnitLabel(schema.ReadingMeasurementUnitLabel, validationErrors);
            ObservationTypeHelper.ValidateMeasurementUnitLabel(schema.TimeMeasurementUnitLabel, validationErrors);
            ObservationTypeHelper.ValidateMeasurementUnitTypeID(schema.DiscreteRateMeasurementUnitTypeID, validationErrors);
            ObservationTypeHelper.ValidateMeasurementUnitTypeID(schema.TimeMeasurementUnitTypeID, validationErrors);
            ObservationTypeHelper.ValidateMeasurementUnitTypeID(schema.ReadingMeasurementUnitTypeID, validationErrors);
            ObservationTypeHelper.ValidateAssessmentInstructions(schema.AssessmentDescription, validationErrors);
            ObservationTypeHelper.ValidateBenchmarkAndThresholdDescription(schema.BenchmarkDescription, schema.ThresholdDescription, validationErrors);

            return validationErrors;
        }

        public override bool ValidateObservationDataJson(string json)
        {
            try
            {
                var schema = JsonConvert.DeserializeObject<RateObservationSchema>(json);
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
            var observation = JsonConvert.DeserializeObject<RateObservationSchema>(observationData);
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
                var schema = JsonConvert.DeserializeObject<PassFailObservationTypeSchema>(json);
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
            var schema = JsonConvert.DeserializeObject<PassFailObservationTypeSchema>(json);

            var propertiesToObserve = schema.PropertiesToObserve;
            ObservationTypeHelper.ValidatePropertiesToObserve(propertiesToObserve, validationErrors);
            ObservationTypeHelper.ValidateAssessmentInstructions(schema.AssessmentDescription, validationErrors);
            ObservationTypeHelper.ValidateRequiredStringField(schema.PassingScoreLabel, "Passing Score Label must have a name and cannot be blank", validationErrors);
            ObservationTypeHelper.ValidateRequiredStringField(schema.FailingScoreLabel, "Failing Score Label must have a name and cannot be blank", validationErrors);

            return validationErrors;
        }

        public override bool ValidateObservationDataJson(string json)
        {

            try
            {
                var schema = JsonConvert.DeserializeObject<PassFailObservationSchema>(json);
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
            var observation = JsonConvert.DeserializeObject<PassFailObservationSchema>(observationData);
            var conveyanceFails = observation.PassFailObservations.Any(x => !x.ObservationValue);
            return conveyanceFails ? 0 : 5;
        }

        public override double? CalculateScore(TreatmentBMPObservation treatmentBMPObservation)
        {
            var observationValue = GetObservationValueFromObservationData(treatmentBMPObservation.ObservationData);
            return observationValue;
        }

        public override string CalculateOverrideScoreText(string observationData,
            string observationTypeSchema,
            bool overrideAssessmentScoreIfFailing)
        {
            var observation = JsonConvert.DeserializeObject<PassFailObservationSchema>(observationData);
            var conveyanceFails = observation.PassFailObservations.Any(x => !x.ObservationValue);
            var schema = JsonConvert.DeserializeObject<PassFailObservationTypeSchema>(observationTypeSchema);
            return conveyanceFails ? schema.FailingScoreLabel : schema.PassingScoreLabel;
        }
    }

    public partial class ObservationTypeCollectionMethodPercentage
    {
        public override bool ValidateObservationTypeJson(string json)
        {
            try
            {
                var schema = JsonConvert.DeserializeObject<PercentageObservationTypeSchema>(json);
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
            var schema = JsonConvert.DeserializeObject<PercentageObservationTypeSchema>(json);

            var propertiesToObserve = schema.PropertiesToObserve;
            ObservationTypeHelper.ValidatePropertiesToObserve(propertiesToObserve, validationErrors);
            ObservationTypeHelper.ValidateMeasurementUnitLabel(schema.MeasurementUnitLabel, validationErrors);
            ObservationTypeHelper.ValidateAssessmentInstructions(schema.AssessmentDescription, validationErrors);
            ObservationTypeHelper.ValidateBenchmarkAndThresholdDescription(schema.BenchmarkDescription, schema.ThresholdDescription, validationErrors);

            return validationErrors;
        }

        public override bool ValidateObservationDataJson(string json)
        {
            try
            {
                var schema = JsonConvert.DeserializeObject<PercentageObservationSchema>(json);
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
            var observation = JsonConvert.DeserializeObject<PercentageObservationSchema>(observationData);
            return observation.SingleValueObservations.Sum(x => x.ObservationValue);
        }

        public override double? CalculateScore(TreatmentBMPObservation treatmentBMPObservation)
        {
            var observationValue = GetObservationValueFromObservationData(treatmentBMPObservation.ObservationData);
            var benchmarkValue = treatmentBMPObservation.ObservationType.GetBenchmarkValue(treatmentBMPObservation.TreatmentBMPAssessment.TreatmentBMP);
            var thresholdValue = treatmentBMPObservation.ObservationType.GetThresholdValue(treatmentBMPObservation.TreatmentBMPAssessment.TreatmentBMP);
            var useUpperValue = treatmentBMPObservation.ObservationType.ThresholdMeasurementUnitType() == MeasurementUnitType.PercentIncrease || (treatmentBMPObservation.ObservationType.TargetIsSweetSpot && observationValue > benchmarkValue);

            // only use this if Threshold is reltive to benchmark
            var thresholdValueInBenchmarkUnits = treatmentBMPObservation.ObservationType.GetThresholdValueInBenchmarkUnits(benchmarkValue, thresholdValue, useUpperValue);

            if (observationValue == null || benchmarkValue == null || thresholdValueInBenchmarkUnits == null)
            {
                return null;
            }

            return ObservationTypeHelper.LinearInterpolation(observationValue.Value, benchmarkValue.Value, thresholdValueInBenchmarkUnits.Value);
        }
    }
}