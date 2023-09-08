﻿using System.ComponentModel.DataAnnotations;
using Neptune.Common.GeoSpatial;
using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public partial class ObservationTypeCollectionMethod
    {
        public abstract bool ValidateObservationTypeJson(string json);
        public abstract List<ValidationResult> ValidateObservationType(string json);
        public abstract List<ValidationResult> ValidateObservationDataJson(
            TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType, string json);

        public abstract string ViewSchemaDetailUrl(TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType);

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
                var schema = GeoJsonSerializer.Deserialize<DiscreteObservationTypeSchema>(json);
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
            var schema = GeoJsonSerializer.Deserialize<DiscreteObservationTypeSchema>(json);

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

        public override List<ValidationResult> ValidateObservationDataJson(
            TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType, string json)
        {
            var validationResults = new List<ValidationResult>();
            try
            {
                var schema = GeoJsonSerializer.Deserialize<DiscreteObservationSchema>(json);
                var treatmentBMPAssessmentObservationTypeName = treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeName;
                if (!(schema.SingleValueObservations.Count > 0))
                {
                    validationResults.Add(new ValidationResult($"You must enter at least one observation for '{treatmentBMPAssessmentObservationTypeName}'."));
                }

                if (schema.SingleValueObservations.Any(x => x.ObservationValue == null))
                {
                    validationResults.Add(new ValidationResult($"Values for the observation '{treatmentBMPAssessmentObservationTypeName}' cannot be blank."));
                }
            }
            catch (Exception)
            {
                validationResults.Add(new ValidationResult("Schema invalid"));
            }

            return validationResults;
        }

        public override string ViewSchemaDetailUrl(TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType)
        {
            return "";//todo: SitkaRoute<TreatmentBMPAssessmentObservationTypeController>.BuildUrlFromExpression(LinkGenerator, x => x.DiscreteDetailSchema(treatmentBMPAssessmentObservationType));
        }

        public override double? GetObservationValueFromObservationData(string observationData)
        {
            var observation = GeoJsonSerializer.Deserialize<DiscreteObservationSchema>(observationData);
            return observation.SingleValueObservations.Average(x => double.Parse(x.ObservationValue.ToString()));
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

    public partial class ObservationTypeCollectionMethodPassFail
    {
        public override bool ValidateObservationTypeJson(string json)
        {
            try
            {
                var schema = GeoJsonSerializer.Deserialize<PassFailObservationTypeSchema>(json);
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
            var schema = GeoJsonSerializer.Deserialize<PassFailObservationTypeSchema>(json);

            var propertiesToObserve = schema.PropertiesToObserve;
            TreatmentBMPAssessmentObservationTypeHelper.ValidatePropertiesToObserve(propertiesToObserve, validationErrors);
            TreatmentBMPAssessmentObservationTypeHelper.ValidateAssessmentInstructions(schema.AssessmentDescription, validationErrors);
            TreatmentBMPAssessmentObservationTypeHelper.ValidateRequiredStringField(schema.PassingScoreLabel, "Passing Score Label must have a name and cannot be blank", validationErrors);
            TreatmentBMPAssessmentObservationTypeHelper.ValidateRequiredStringField(schema.FailingScoreLabel, "Failing Score Label must have a name and cannot be blank", validationErrors);

            return validationErrors;
        }

        public override List<ValidationResult> ValidateObservationDataJson(
            TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType, string json)
        {
            var validationResults = new List<ValidationResult>();
            try
            {
                var schema = GeoJsonSerializer.Deserialize<PassFailObservationSchema>(json);
                var treatmentBMPAssessmentObservationTypeName = treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeName;
                if (schema.SingleValueObservations.Any(x => x.ObservationValue == null))
                {
                    validationResults.Add(new ValidationResult($"Values for the observation '{treatmentBMPAssessmentObservationTypeName}' cannot be blank."));
                }
            }
            catch (Exception)
            {
                validationResults.Add(new ValidationResult("Schema invalid"));
            }

            return validationResults;
        }

        public override string ViewSchemaDetailUrl(TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType)
        {
            return ""; //todo: SitkaRoute<TreatmentBMPAssessmentObservationTypeController>.BuildUrlFromExpression(LinkGenerator, x => x.PassFailDetailSchema(treatmentBMPAssessmentObservationType));
        }

        public override double? GetObservationValueFromObservationData(string observationData)
        {
            var observation = GeoJsonSerializer.Deserialize<PassFailObservationSchema>(observationData);
            var conveyanceFails = observation.SingleValueObservations.Any(x => bool.Parse(x.ObservationValue.ToString()) == false);
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
            var observation = GeoJsonSerializer.Deserialize<PassFailObservationSchema>(observationData);
            var conveyanceFails = observation.SingleValueObservations.Any(x => bool.Parse(x.ObservationValue.ToString()) == false);
            var schema = GeoJsonSerializer.Deserialize<PassFailObservationTypeSchema>(observationTypeSchema);
            return conveyanceFails ? schema.FailingScoreLabel : schema.PassingScoreLabel;
        }
    }

    public partial class ObservationTypeCollectionMethodPercentage
    {
        public override bool ValidateObservationTypeJson(string json)
        {
            try
            {
                var schema = GeoJsonSerializer.Deserialize<PercentageObservationTypeSchema>(json);
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
            var schema = GeoJsonSerializer.Deserialize<PercentageObservationTypeSchema>(json);

            var propertiesToObserve = schema.PropertiesToObserve;
            TreatmentBMPAssessmentObservationTypeHelper.ValidatePropertiesToObserve(propertiesToObserve, validationErrors);
            TreatmentBMPAssessmentObservationTypeHelper.ValidateMeasurementUnitLabel(schema.MeasurementUnitLabel, validationErrors);
            TreatmentBMPAssessmentObservationTypeHelper.ValidateAssessmentInstructions(schema.AssessmentDescription, validationErrors);
            TreatmentBMPAssessmentObservationTypeHelper.ValidateBenchmarkAndThresholdDescription(schema.BenchmarkDescription, schema.ThresholdDescription, validationErrors);

            return validationErrors;
        }
        public override List<ValidationResult> ValidateObservationDataJson(
            TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType, string json)
        {
            var validationResults = new List<ValidationResult>();
            try
            {
                var schema = GeoJsonSerializer.Deserialize<DiscreteObservationSchema>(json);
                var treatmentBMPAssessmentObservationTypeName = treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeName;
                if (!(schema.SingleValueObservations.Count > 0))
                {
                    validationResults.Add(new ValidationResult($"You must enter at least one observation for '{treatmentBMPAssessmentObservationTypeName}'."));
                }

                if (schema.SingleValueObservations.Any(x => x.ObservationValue == null))
                {
                    validationResults.Add(new ValidationResult($"Values for the observation '{treatmentBMPAssessmentObservationTypeName}' cannot be blank."));
                }
            }
            catch (Exception)
            {
                validationResults.Add(new ValidationResult("Schema invalid"));
            }

            return validationResults;
        }

        public override string ViewSchemaDetailUrl(TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType)
        {
            return "";//todo: SitkaRoute<TreatmentBMPAssessmentObservationTypeController>.BuildUrlFromExpression(LinkGenerator, x => x.PercentageDetailSchema(treatmentBMPAssessmentObservationType));
        }

        public override double? GetObservationValueFromObservationData(string observationData)
        {
            var observation = GeoJsonSerializer.Deserialize<PercentageObservationSchema>(observationData);
            return observation.SingleValueObservations.Sum(x => Convert.ToDouble(x.ObservationValue));
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