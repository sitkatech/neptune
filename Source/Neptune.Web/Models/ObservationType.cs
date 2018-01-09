/*-----------------------------------------------------------------------
<copyright file="ObservationType.cs" company="Tahoe Regional Planning Agency">
Copyright (c) Tahoe Regional Planning Agency. All rights reserved.
<author>Sitka Technology Group</author>
</copyright>

<license>
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License <http://www.gnu.org/licenses/> for more details.

Source code is available upon request via <support@sitkatech.com>.
</license>
-----------------------------------------------------------------------*/

using System;
using System.Linq;
using LtInfo.Common.Views;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Views.ObservationType;
using Newtonsoft.Json;

namespace Neptune.Web.Models
{
    public partial class ObservationType : IAuditableEntity
    {
        public string AssessmentUrl(int treatmentBMPAssessmentID)
        {
            throw new NotImplementedException();
        }

        public string BenchmarkAndThresholdUrl(TreatmentBMP treatmentBMP)
        {
            return ObservationTypeSpecification.ObservationThresholdType.GetBenchmarkAndThresholdUrl(treatmentBMP, this);
        }

        public bool HasBenchmarkAndThreshold => ObservationTypeSpecification.ObservationThresholdType != ObservationThresholdType.None;
        public bool ThresholdIsPercentFromBenchmark => ObservationTypeSpecification.ObservationThresholdType == ObservationThresholdType.PercentFromBenchmark;

        public MeasurementUnitType MeasurementUnitType => BenchmarkMeasurementUnitType();        
        public DiscreteValueSchema DiscreteValueSchema => JsonConvert.DeserializeObject<DiscreteValueSchema>(ObservationTypeSchema);
        public RateSchema RateSchema => JsonConvert.DeserializeObject<RateSchema>(ObservationTypeSchema);
        public PassFailSchema PassFailSchema => JsonConvert.DeserializeObject<PassFailSchema>(ObservationTypeSchema);
        public PercentageSchema PercentageSchema => JsonConvert.DeserializeObject<PercentageSchema>(ObservationTypeSchema);

        public MeasurementUnitType BenchmarkMeasurementUnitType()
        {
            var observationTypeCollectionMethod = ObservationTypeSpecification.ObservationTypeCollectionMethod;
            switch (observationTypeCollectionMethod.ToEnum)
            {
                case ObservationTypeCollectionMethodEnum.DiscreteValue:
                    return MeasurementUnitType.All
                        .SingleOrDefault(x => x.MeasurementUnitTypeID == DiscreteValueSchema.MeasurementUnitTypeID);
                case ObservationTypeCollectionMethodEnum.Rate:
                    return MeasurementUnitType.All
                        .SingleOrDefault(x => x.MeasurementUnitTypeID == RateSchema.DiscreteRateMeasurementUnitTypeID);
                case ObservationTypeCollectionMethodEnum.PassFail:
                    return null;
                case ObservationTypeCollectionMethodEnum.Percentage:
                    return MeasurementUnitType.Percent;
                default:
                    return null;
            }
        }

        public string BenchmarkMeasurementUnitLabel()
        {
            var observationTypeCollectionMethod = ObservationTypeSpecification.ObservationTypeCollectionMethod;
            switch (observationTypeCollectionMethod.ToEnum)
            {
                case ObservationTypeCollectionMethodEnum.DiscreteValue:
                    return DiscreteValueSchema.MeasurementUnitLabel;
                case ObservationTypeCollectionMethodEnum.Rate:
                    return RateSchema.DiscreteRateMeasurementUnitLabel;
                case ObservationTypeCollectionMethodEnum.PassFail:
                    return null;
                case ObservationTypeCollectionMethodEnum.Percentage:
                    return PercentageSchema.MeasurementUnitLabel;
                default:
                    return null;
            }
        }

        public string BenchmarkDescription()
        {
            var observationTypeCollectionMethod = ObservationTypeSpecification.ObservationTypeCollectionMethod;
            switch (observationTypeCollectionMethod.ToEnum)
            {
                case ObservationTypeCollectionMethodEnum.DiscreteValue:
                    return DiscreteValueSchema.BenchmarkDescription;
                case ObservationTypeCollectionMethodEnum.Rate:
                    return RateSchema.BenchmarkDescription;
                case ObservationTypeCollectionMethodEnum.PassFail:
                    return null;
                case ObservationTypeCollectionMethodEnum.Percentage:
                    return PercentageSchema.BenchmarkDescription;
                default:
                    return null;
            }
        }

        public string ThresholdMeasurementUnitLabel()
        {
            var observationThresholdType = ObservationTypeSpecification.ObservationThresholdType;
            switch (observationThresholdType.ToEnum)
            {
                case ObservationThresholdTypeEnum.DiscreteValue:
                    return BenchmarkMeasurementUnitLabel();
                case ObservationThresholdTypeEnum.PercentFromBenchmark:
                    return ThresholdMeasurementUnitForPercentFromBenchmark().MeasurementUnitTypeDisplayName;
                case ObservationThresholdTypeEnum.None:
                    return null;
                default:
                    return null;
            }
        }

        public MeasurementUnitType ThresholdMeasurementUnitType()
        {            
            var observationThresholdType = ObservationTypeSpecification.ObservationThresholdType;
            switch (observationThresholdType.ToEnum)
            {
                case ObservationThresholdTypeEnum.DiscreteValue:
                    return BenchmarkMeasurementUnitType();
                case ObservationThresholdTypeEnum.PercentFromBenchmark:
                    return ThresholdMeasurementUnitForPercentFromBenchmark();
                case ObservationThresholdTypeEnum.None:
                    return null;
                default:
                    return null;
            }
        }

        public string ThresholdDescription()
        {
            var observationTypeCollectionMethod = ObservationTypeSpecification.ObservationTypeCollectionMethod;
            switch (observationTypeCollectionMethod.ToEnum)
            {
                case ObservationTypeCollectionMethodEnum.DiscreteValue:
                    return DiscreteValueSchema.ThresholdDescription;
                case ObservationTypeCollectionMethodEnum.Rate:
                    return RateSchema.ThresholdDescription;
                case ObservationTypeCollectionMethodEnum.PassFail:
                    return null;
                case ObservationTypeCollectionMethodEnum.Percentage:
                    return PercentageSchema.ThresholdDescription;
                default:
                    return null;
            }
        }

        private MeasurementUnitType ThresholdMeasurementUnitForPercentFromBenchmark()
        {
            var observationTargetType = ObservationTypeSpecification.ObservationTargetType;
            switch (observationTargetType.ToEnum)
            {
                case ObservationTargetTypeEnum.PassFail:
                    return null;
                case ObservationTargetTypeEnum.High:
                    return MeasurementUnitType.PercentDecline;
                case ObservationTargetTypeEnum.Low:
                    return MeasurementUnitType.PercentIncrease;
                case ObservationTargetTypeEnum.SpecificValue:
                    return MeasurementUnitType.PercentDeviation;
                default:
                    return null;
            }
        }
       

        public double? GetBenchmarkValue(TreatmentBMP treatmentBMP)
        {
            var treatmentBMPBenchmarkAndThreshold = treatmentBMP.TreatmentBMPBenchmarkAndThresholds
                .SingleOrDefault(x => x.ObservationType == this);

            return treatmentBMPBenchmarkAndThreshold?.BenchmarkValue;
        }

        public double? GetThresholdValue(TreatmentBMP treatmentBMP)
        {
            var treatmentBMPBenchmarkAndThreshold = treatmentBMP.TreatmentBMPBenchmarkAndThresholds
                .SingleOrDefault(x => x.ObservationType == this);

            return treatmentBMPBenchmarkAndThreshold?.ThresholdValue;
        }

        public string GetFormattedBenchmarkValue(double? benchmarkValue)
        {
            if (!HasBenchmarkAndThreshold)
            {
                return ViewUtilities.NaString;
            }

            if (benchmarkValue == null)
            {
                return "-";
            }

            var optionalSpace = MeasurementUnitType == MeasurementUnitType.Percent ? "" : " ";
            return $"{benchmarkValue}{optionalSpace}{MeasurementUnitType.LegendDisplayName}";
        }

        public string GetFormattedThresholdValue(double? thresholdValue, double? benchmarkValue)
        {

            if (!HasBenchmarkAndThreshold)
            {
                return ViewUtilities.NaString;
            }

            if (thresholdValue == null)
            {
                return "-";
            }

            var optionalSpace = ThresholdMeasurementUnitType().IncludeSpaceBeforeLegendLabel() ? "" : " ";
            var formattedThresholdValue = $"{thresholdValue}{optionalSpace}{ThresholdMeasurementUnitType().LegendDisplayName}";

            if (!ThresholdIsPercentFromBenchmark || benchmarkValue == null)
            {                
                return formattedThresholdValue;
            }

            //todo % decline
            if (ThresholdMeasurementUnitType() == MeasurementUnitType.PercentDecline)
            {
                var thresholdValueInBenchmarkUnits = benchmarkValue - (thresholdValue / 100) * benchmarkValue;
                return $"{formattedThresholdValue} ({thresholdValueInBenchmarkUnits} {BenchmarkMeasurementUnitType().LegendDisplayName})";
            }

            //todo % increase
            if (ThresholdMeasurementUnitType() == MeasurementUnitType.PercentIncrease)
            {
                var thresholdValueInBenchmarkUnits = benchmarkValue + (thresholdValue / 100) * benchmarkValue;
                return $"{formattedThresholdValue} ({thresholdValueInBenchmarkUnits} {BenchmarkMeasurementUnitType().LegendDisplayName})";
            }

            //todo % deviation
            if (ThresholdMeasurementUnitType() == MeasurementUnitType.PercentDeviation)
            {
                var upperValueInBenchmarkUnits = benchmarkValue + (thresholdValue / 100) * benchmarkValue;
                var lowerValueInBenchmarkUnits = benchmarkValue - (thresholdValue / 100) * benchmarkValue;
                return $"{formattedThresholdValue} ({upperValueInBenchmarkUnits}{BenchmarkMeasurementUnitType().LegendDisplayName}/{lowerValueInBenchmarkUnits}{BenchmarkMeasurementUnitType().LegendDisplayName})";
            }

            return string.Empty;
        }

        public string AuditDescriptionString => $"Observation Type {ObservationTypeName}";
    }


}
