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
using Microsoft.Ajax.Utilities;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Views.ObservationType;
using Newtonsoft.Json;

namespace Neptune.Web.Models
{
    public partial class ObservationType : IAuditableEntity
    {
        public string AssessmentUrl(TreatmentBMPAssessment treatmentBMPAssessment)
        {
            return ObservationTypeSpecification.ObservationTypeCollectionMethod.GetAssessmentUrl(treatmentBMPAssessment, this);
        }

        public string BenchmarkAndThresholdUrl(TreatmentBMP treatmentBMP)
        {
            return ObservationTypeSpecification.ObservationThresholdType.GetBenchmarkAndThresholdUrl(treatmentBMP, this);
        }

        public bool HasBenchmarkAndThreshold => ObservationTypeSpecification.ObservationThresholdType != ObservationThresholdType.None;
        public bool ThresholdIsPercentFromBenchmark => ObservationTypeSpecification.ObservationThresholdType == ObservationThresholdType.PercentFromBenchmark;

        public MeasurementUnitType MeasurementUnitType => BenchmarkMeasurementUnitType();        
        public DiscreteObservationTypeSchema DiscreteObservationTypeSchema => JsonConvert.DeserializeObject<DiscreteObservationTypeSchema>(ObservationTypeSchema);
        public RateObservationTypeSchema RateObservationTypeSchema => JsonConvert.DeserializeObject<RateObservationTypeSchema>(ObservationTypeSchema);
        public PassFailObservationTypeSchema PassFailSchema => JsonConvert.DeserializeObject<PassFailObservationTypeSchema>(ObservationTypeSchema);
        public PercentageObservationTypeSchema PercentageSchema => JsonConvert.DeserializeObject<PercentageObservationTypeSchema>(ObservationTypeSchema);

        public MeasurementUnitType BenchmarkMeasurementUnitType()
        {
            var observationTypeCollectionMethod = ObservationTypeSpecification.ObservationTypeCollectionMethod;
            switch (observationTypeCollectionMethod.ToEnum)
            {
                case ObservationTypeCollectionMethodEnum.DiscreteValue:
                    return MeasurementUnitType.All
                        .SingleOrDefault(x => x.MeasurementUnitTypeID == DiscreteObservationTypeSchema.MeasurementUnitTypeID);
                case ObservationTypeCollectionMethodEnum.Rate:
                    return MeasurementUnitType.All
                        .SingleOrDefault(x => x.MeasurementUnitTypeID == RateObservationTypeSchema.DiscreteRateMeasurementUnitTypeID);
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
                    return DiscreteObservationTypeSchema.MeasurementUnitLabel;
                case ObservationTypeCollectionMethodEnum.Rate:
                    return RateObservationTypeSchema.DiscreteRateMeasurementUnitLabel;
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
                    return DiscreteObservationTypeSchema.BenchmarkDescription;
                case ObservationTypeCollectionMethodEnum.Rate:
                    return RateObservationTypeSchema.BenchmarkDescription;
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
                    return DiscreteObservationTypeSchema.ThresholdDescription;
                case ObservationTypeCollectionMethodEnum.Rate:
                    return RateObservationTypeSchema.ThresholdDescription;
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

        public double? GetThresholdValueInBenchmarkUnits(double? benchmarkValue, double? thresholdValue, bool useUpperValue)
        {
            if (benchmarkValue == null || thresholdValue == null)
            {
                return null;
            }

            var thresholdMeasurementUnitType = ThresholdMeasurementUnitType();
            switch (thresholdMeasurementUnitType.ToEnum)
            {
                case MeasurementUnitTypeEnum.Acres:
                case MeasurementUnitTypeEnum.SquareFeet:
                case MeasurementUnitTypeEnum.Kilogram:
                case MeasurementUnitTypeEnum.Count:
                case MeasurementUnitTypeEnum.Percent:
                case MeasurementUnitTypeEnum.MilligamsPerLiter:
                case MeasurementUnitTypeEnum.Meters:
                case MeasurementUnitTypeEnum.Feet:
                case MeasurementUnitTypeEnum.Inches:
                case MeasurementUnitTypeEnum.InchesPerHour:
                case MeasurementUnitTypeEnum.Seconds:                    
                    return thresholdValue;
                case MeasurementUnitTypeEnum.PercentDecline:
                    return benchmarkValue - (thresholdValue / 100) * benchmarkValue;
                case MeasurementUnitTypeEnum.PercentIncrease:
                    return benchmarkValue + (thresholdValue / 100) * benchmarkValue;
                case MeasurementUnitTypeEnum.PercentDeviation:
                    if (useUpperValue)
                    {
                        return benchmarkValue + (thresholdValue / 100) * benchmarkValue;
                    }
                   return benchmarkValue - (thresholdValue / 100) * benchmarkValue;
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

            if (ThresholdMeasurementUnitType() == MeasurementUnitType.PercentDeviation)
            {
                var upperValueInBenchmarkUnits = GetThresholdValueInBenchmarkUnits(benchmarkValue, thresholdValue, true);
                var lowerValueInBenchmarkUnits = GetThresholdValueInBenchmarkUnits(benchmarkValue, thresholdValue, false);
                return $"{formattedThresholdValue} ({upperValueInBenchmarkUnits}{BenchmarkMeasurementUnitType().LegendDisplayName}/{lowerValueInBenchmarkUnits}{BenchmarkMeasurementUnitType().LegendDisplayName})";
            }

            var thresholdValueInBenchmarkUnits = GetThresholdValueInBenchmarkUnits(benchmarkValue, thresholdValue, ThresholdMeasurementUnitType() == MeasurementUnitType.PercentIncrease);

            return $"{formattedThresholdValue} ({thresholdValueInBenchmarkUnits} {BenchmarkMeasurementUnitType().LegendDisplayName})";
        }

        public string AuditDescriptionString => $"Observation Type {ObservationTypeName}";
    }


}
