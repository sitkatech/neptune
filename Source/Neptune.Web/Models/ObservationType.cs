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

        public bool IsComplete(TreatmentBMPAssessment treatmentBMPAssessment)
        {
            var treatmentBMPObservation = treatmentBMPAssessment.TreatmentBMPObservations.ToList().Find(x => x.ObservationType.ObservationTypeID == ObservationTypeID);

            if (treatmentBMPObservation == null)
            {
                return false;
            }

            return IsComplete(treatmentBMPObservation);
        }
        public bool IsComplete(TreatmentBMPObservation treatmentBMPObservation)
        {
            throw new NotImplementedException();
        }

        public double CalculateScore(TreatmentBMPAssessment treatmentBMPAssessment)
        {
            var treatmentBMPObservation = treatmentBMPAssessment.TreatmentBMPObservations.ToList().Find(x => x.ObservationType.ObservationTypeID == ObservationTypeID);

            if (!IsComplete(treatmentBMPObservation))
            {
                throw new Exception("Observation not complete, cannot calculate score");
            }
            return CalculateScore(treatmentBMPObservation);
        }

        public string FormattedScore(TreatmentBMPObservation treatmentBMPObservation)
        {
            var score = CalculateScore(treatmentBMPObservation);
            return score.ToString("0.0");
        }

        public double CalculateScore(TreatmentBMPObservation treatmentBMPObservation)
        {
            throw new NotImplementedException();
        }

        public double GetObservationValue(TreatmentBMPObservation treatmentBMPObservation)
        {
            return GetObservationValueImpl(treatmentBMPObservation);
        }

        protected double GetObservationValueImpl(TreatmentBMPObservation treatmentBMPObservation)
        {
            throw new NotImplementedException();
        }

        public bool IsBenchmarkAndThresholdComplete(TreatmentBMP treatmentBMP)
        {
            return treatmentBMP.TreatmentBMPBenchmarkAndThresholds.SingleOrDefault(x => x.ObservationTypeID == ObservationTypeID) != null;
        }

        public bool HasBenchmarkAndThreshold => ObservationTypeSpecification.ObservationThresholdType != ObservationThresholdType.None;
        public bool ThresholdPercentDecline => ObservationTypeSpecification.ObservationThresholdType == ObservationThresholdType.PercentFromBenchmark;

        public MeasurementUnitType MeasurementUnitType => MeasurementUnitType.Feet; //todo

        public bool IsObservationTypeCollectionMethodDiscreteValue => ObservationTypeSpecification.ObservationTypeCollectionMethod == ObservationTypeCollectionMethod.DiscreteValue;
        public bool IsObservationTypeCollectionMethodRate => ObservationTypeSpecification.ObservationTypeCollectionMethod == ObservationTypeCollectionMethod.Rate;
        public bool IsObservationTypeCollectionMethodPassFail => ObservationTypeSpecification.ObservationTypeCollectionMethod == ObservationTypeCollectionMethod.PassFail;
        public bool IsObservationTypeCollectionMethodPercentage => ObservationTypeSpecification.ObservationTypeCollectionMethod == ObservationTypeCollectionMethod.Percentage;

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

        public string AuditDescriptionString => $"Observation Type {ObservationTypeName}";
    }


}
