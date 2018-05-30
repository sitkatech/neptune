using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Views.TreatmentBMPAssessmentObservationType;
using Newtonsoft.Json;

namespace Neptune.Web.Models
{
    public partial class ObservationTypeCollectionMethod
    {
        public abstract bool ValidateObservationTypeJson(string json);
        public abstract List<ValidationResult> ValidateObservationType(string json);
        public abstract bool ValidateObservationDataJson(string json);

        public abstract string ViewSchemaDetailUrl(TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType);
        public abstract string GetAssessmentUrl(FieldVisit fieldVisit, FieldVisitAssessmentType fieldVisitAssessmentType, TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType);

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
            TreatmentBMPAssessmentObservationTypeHelper.ValidatePropertiesToObserve(propertiesToObserve, validationErrors);
            TreatmentBMPAssessmentObservationTypeHelper.ValidateNumberOfObservations(schema.MinimumNumberOfObservations, schema.MaximumNumberOfObservations, validationErrors);
            TreatmentBMPAssessmentObservationTypeHelper.ValidateValueOfObservations(schema.MinimumValueOfObservations, schema.MaximumValueOfObservations, validationErrors);
            TreatmentBMPAssessmentObservationTypeHelper.ValidateMeasurementUnitLabel(schema.MeasurementUnitLabel, validationErrors);
            TreatmentBMPAssessmentObservationTypeHelper.ValidateMeasurementUnitTypeID(schema.MeasurementUnitTypeID, validationErrors);
            TreatmentBMPAssessmentObservationTypeHelper.ValidateAssessmentInstructions(schema.AssessmentDescription, validationErrors);
            TreatmentBMPAssessmentObservationTypeHelper.ValidateBenchmarkAndThresholdDescription(schema.BenchmarkDescription, schema.ThresholdDescription, validationErrors);

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

        public override string ViewSchemaDetailUrl(TreatmentBMPAssessmentObservationType TreatmentBMPAssessmentObservationType)
        {
            return SitkaRoute<TreatmentBMPAssessmentObservationTypeController>.BuildUrlFromExpression(c => c.DiscreteDetailSchema(TreatmentBMPAssessmentObservationType));
        }

        public override string GetAssessmentUrl(FieldVisit fieldVisit, FieldVisitAssessmentType fieldVisitAssessmentType,
            TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType)
        {
            return SitkaRoute<FieldVisitController>.BuildUrlFromExpression(c => c.DiscreteCollectionMethod(fieldVisit, treatmentBMPAssessmentObservationType, (int)fieldVisitAssessmentType));
        }

        public override double? GetObservationValueFromObservationData(string observationData)
        {
            var observation = JsonConvert.DeserializeObject<DiscreteObservationSchema>(observationData);
            return observation.SingleValueObservations.Average(x => x.ObservationValue);
        }

        public override double? CalculateScore(TreatmentBMPObservation treatmentBMPObservation)
        {            
            var observationValue = GetObservationValueFromObservationData(treatmentBMPObservation.ObservationData);
            var benchmarkValue = treatmentBMPObservation.TreatmentBMPAssessmentObservationType.GetBenchmarkValue(treatmentBMPObservation.TreatmentBMPAssessment.TreatmentBMP);
            var thresholdValue = treatmentBMPObservation.TreatmentBMPAssessmentObservationType.GetThresholdValue(treatmentBMPObservation.TreatmentBMPAssessment.TreatmentBMP);

            var useUpperValue = treatmentBMPObservation.TreatmentBMPAssessmentObservationType.UseUpperValueForThreshold(benchmarkValue, observationValue);
            var thresholdValueInBenchmarkUnits = treatmentBMPObservation.TreatmentBMPAssessmentObservationType.GetThresholdValueInBenchmarkUnits(benchmarkValue, thresholdValue, useUpperValue);

            if (observationValue == null || benchmarkValue == null || thresholdValueInBenchmarkUnits == null)
            {
                return null;
            }

            return TreatmentBMPAssessmentObservationTypeHelper.LinearInterpolation(observationValue.Value, benchmarkValue.Value, thresholdValueInBenchmarkUnits.Value);
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
            TreatmentBMPAssessmentObservationTypeHelper.ValidatePropertiesToObserve(propertiesToObserve, validationErrors);
            TreatmentBMPAssessmentObservationTypeHelper.ValidateNumberOfObservations(schema.DiscreteRateMinimumNumberOfObservations, schema.DiscreteRateMaximumNumberOfObservations, validationErrors);
            TreatmentBMPAssessmentObservationTypeHelper.ValidateNumberOfObservations(schema.TimeReadingMinimumNumberOfObservations, schema.TimeReadingMaximumNumberOfObservations, validationErrors);
            TreatmentBMPAssessmentObservationTypeHelper.ValidateValueOfObservations(schema.DiscreteRateMinimumValueOfObservations, schema.DiscreteRateMaximumValueOfObservations, validationErrors);
            TreatmentBMPAssessmentObservationTypeHelper.ValidateValueOfObservations(schema.TimeReadingMinimumValueOfObservations, schema.TimeReadingMaximumValueOfObservations, validationErrors);
            TreatmentBMPAssessmentObservationTypeHelper.ValidateMeasurementUnitLabel(schema.DiscreteRateMeasurementUnitLabel, validationErrors);
            TreatmentBMPAssessmentObservationTypeHelper.ValidateMeasurementUnitLabel(schema.ReadingMeasurementUnitLabel, validationErrors);
            TreatmentBMPAssessmentObservationTypeHelper.ValidateMeasurementUnitLabel(schema.TimeMeasurementUnitLabel, validationErrors);
            TreatmentBMPAssessmentObservationTypeHelper.ValidateMeasurementUnitTypeID(schema.DiscreteRateMeasurementUnitTypeID, validationErrors);
            TreatmentBMPAssessmentObservationTypeHelper.ValidateMeasurementUnitTypeID(schema.TimeMeasurementUnitTypeID, validationErrors);
            TreatmentBMPAssessmentObservationTypeHelper.ValidateMeasurementUnitTypeID(schema.ReadingMeasurementUnitTypeID, validationErrors);
            TreatmentBMPAssessmentObservationTypeHelper.ValidateAssessmentInstructions(schema.AssessmentDescription, validationErrors);
            TreatmentBMPAssessmentObservationTypeHelper.ValidateBenchmarkAndThresholdDescription(schema.BenchmarkDescription, schema.ThresholdDescription, validationErrors);

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

        public override string ViewSchemaDetailUrl(TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType)
        {
            return SitkaRoute<TreatmentBMPAssessmentObservationTypeController>.BuildUrlFromExpression(c => c.RateDetailSchema(treatmentBMPAssessmentObservationType));
        }

        public override string GetAssessmentUrl(FieldVisit fieldVisit, FieldVisitAssessmentType fieldVisitAssessmentType,
            TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType)
        {
            return SitkaRoute<FieldVisitController>.BuildUrlFromExpression(c => c.RateCollectionMethod(fieldVisit, treatmentBMPAssessmentObservationType, (int)fieldVisitAssessmentType));
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
            TreatmentBMPAssessmentObservationTypeHelper.ValidatePropertiesToObserve(propertiesToObserve, validationErrors);
            TreatmentBMPAssessmentObservationTypeHelper.ValidateAssessmentInstructions(schema.AssessmentDescription, validationErrors);
            TreatmentBMPAssessmentObservationTypeHelper.ValidateRequiredStringField(schema.PassingScoreLabel, "Passing Score Label must have a name and cannot be blank", validationErrors);
            TreatmentBMPAssessmentObservationTypeHelper.ValidateRequiredStringField(schema.FailingScoreLabel, "Failing Score Label must have a name and cannot be blank", validationErrors);

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

        public override string ViewSchemaDetailUrl(TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType)
        {
            return SitkaRoute<TreatmentBMPAssessmentObservationTypeController>.BuildUrlFromExpression(c => c.PassFailDetailSchema(treatmentBMPAssessmentObservationType));
        }

        public override string GetAssessmentUrl(FieldVisit fieldVisit, FieldVisitAssessmentType fieldVisitAssessmentType,
            TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType)
        {
            return SitkaRoute<FieldVisitController>.BuildUrlFromExpression(c => c.PassFailCollectionMethod(fieldVisit, treatmentBMPAssessmentObservationType, (int)fieldVisitAssessmentType));
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
            TreatmentBMPAssessmentObservationTypeHelper.ValidatePropertiesToObserve(propertiesToObserve, validationErrors);
            TreatmentBMPAssessmentObservationTypeHelper.ValidateMeasurementUnitLabel(schema.MeasurementUnitLabel, validationErrors);
            TreatmentBMPAssessmentObservationTypeHelper.ValidateAssessmentInstructions(schema.AssessmentDescription, validationErrors);
            TreatmentBMPAssessmentObservationTypeHelper.ValidateBenchmarkAndThresholdDescription(schema.BenchmarkDescription, schema.ThresholdDescription, validationErrors);

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

        public override string ViewSchemaDetailUrl(TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType)
        {
            return SitkaRoute<TreatmentBMPAssessmentObservationTypeController>.BuildUrlFromExpression(c => c.PercentageDetailSchema(treatmentBMPAssessmentObservationType));
        }

        public override string GetAssessmentUrl(FieldVisit fieldVisit, FieldVisitAssessmentType fieldVisitAssessmentType,
            TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType)
        {
            return SitkaRoute<FieldVisitController>.BuildUrlFromExpression(c => c.PercentageCollectionMethod(fieldVisit, treatmentBMPAssessmentObservationType, (int)fieldVisitAssessmentType));
        }

        public override double? GetObservationValueFromObservationData(string observationData)
        {
            var observation = JsonConvert.DeserializeObject<PercentageObservationSchema>(observationData);
            return observation.SingleValueObservations.Sum(x => x.ObservationValue);
        }

        public override double? CalculateScore(TreatmentBMPObservation treatmentBMPObservation)
        {
            var observationValue = GetObservationValueFromObservationData(treatmentBMPObservation.ObservationData);
            var benchmarkValue = treatmentBMPObservation.TreatmentBMPAssessmentObservationType.GetBenchmarkValue(treatmentBMPObservation.TreatmentBMPAssessment.TreatmentBMP);
            var thresholdValue = treatmentBMPObservation.TreatmentBMPAssessmentObservationType.GetThresholdValue(treatmentBMPObservation.TreatmentBMPAssessment.TreatmentBMP);
            var useUpperValue = treatmentBMPObservation.TreatmentBMPAssessmentObservationType.UseUpperValueForThreshold(benchmarkValue, observationValue);

            var thresholdValueInBenchmarkUnits = treatmentBMPObservation.TreatmentBMPAssessmentObservationType.GetThresholdValueInBenchmarkUnits(benchmarkValue, thresholdValue, useUpperValue);

            if (observationValue == null || benchmarkValue == null || thresholdValueInBenchmarkUnits == null)
            {
                return null;
            }

            return TreatmentBMPAssessmentObservationTypeHelper.LinearInterpolation(observationValue.Value, benchmarkValue.Value, thresholdValueInBenchmarkUnits.Value);
        }
    }
}