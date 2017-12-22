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

namespace Neptune.Web.Models
{
    public partial class ObservationType
    {
        public abstract string AssessmentUrl(int treatmentBMPAssessmentID);
        public abstract string BenchmarkAndThresholdUrl(int treatmentBMPID);

        public bool IsComplete(TreatmentBMPAssessment treatmentBMPAssessment)
        {
            var treatmentBMPObservation = treatmentBMPAssessment.TreatmentBMPObservations.ToList().Find(x => x.ObservationType.ObservationTypeID == ObservationTypeID);

            if (treatmentBMPObservation == null)
            {
                return false;
            }

            return IsComplete(treatmentBMPObservation);
        }
        public abstract bool IsComplete(TreatmentBMPObservation treatmentBMPObservation);

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

        public abstract double CalculateScore(TreatmentBMPObservation treatmentBMPObservation);

        public double GetObservationValue(TreatmentBMPObservation treatmentBMPObservation)
        {
            return GetObservationValueImpl(treatmentBMPObservation);
        }

        protected abstract double GetObservationValueImpl(TreatmentBMPObservation treatmentBMPObservation);

        public bool IsBenchmarkAndThresholdComplete(TreatmentBMP treatmentBMP)
        {
            return treatmentBMP.TreatmentBMPBenchmarkAndThresholds.SingleOrDefault(x => x.ObservationTypeID == ObservationTypeID) != null;
        }
    }

    #region overriding implementations

    public partial class ObservationTypeConveyanceFunction
    {
        public override string AssessmentUrl(int treatmentBMPAssessmentID)
        {
            return SitkaRoute<TreatmentBMPAssessmentController>.BuildUrlFromExpression(t => t.ConveyanceFunction(treatmentBMPAssessmentID));
        }
        public override string BenchmarkAndThresholdUrl(int treatmentBMPID)
        {
            return string.Empty;
        }

        public override bool IsComplete(TreatmentBMPObservation treatmentBMPObservation)
        {
            var numObs =
                treatmentBMPObservation.TreatmentBMPObservationDetails.Where(
                    x => x.TreatmentBMPObservationDetailType == TreatmentBMPObservationDetailType.Inlet || x.TreatmentBMPObservationDetailType == TreatmentBMPObservationDetailType.Outlet).ToList();
            
            return numObs.Count == (treatmentBMPObservation.TreatmentBMPAssessment.TreatmentBMP.InletCount + treatmentBMPObservation.TreatmentBMPAssessment.TreatmentBMP.OutletCount);
        }

        public override double CalculateScore(TreatmentBMPObservation treatmentBMPObservation)
        {
            var conveyanceFails = treatmentBMPObservation.TreatmentBMPObservationDetails.Any(x => Math.Abs((int) (x.TreatmentBMPObservationValue - 0)) < 0.01);
            return conveyanceFails ? 2 : 5; 
        }

        protected override double GetObservationValueImpl(TreatmentBMPObservation treatmentBMPObservation)
        {
            return treatmentBMPObservation.TreatmentBMPObservationDetails.Any(x => Math.Abs((int) (x.TreatmentBMPObservationValue - 0)) < 0.01) ? 1 : 0;
        }
    }

    public partial class ObservationTypeInfiltrationRate
    {
        public override string AssessmentUrl(int treatmentBMPAssessmentID)
        {
            return SitkaRoute<TreatmentBMPAssessmentController>.BuildUrlFromExpression(t => t.InfiltrationRate(treatmentBMPAssessmentID));
        }
        public override string BenchmarkAndThresholdUrl(int treatmentBMPID)
        {
            return SitkaRoute<TreatmentBMPBenchmarkAndThresholdController>.BuildUrlFromExpression(t => t.InfiltrationRate(treatmentBMPID));
        }

        public override bool IsComplete(TreatmentBMPObservation treatmentBMPObservation)
        {
            return treatmentBMPObservation.TreatmentBMPObservationDetails.Any();
        }

        public override double CalculateScore(TreatmentBMPObservation treatmentBMPObservation)
        {
            var observationValue = GetObservationValueImpl(treatmentBMPObservation);
            var benchmarkValue = treatmentBMPObservation.TreatmentBMPAssessment.TreatmentBMP.GetBenchmarkValue(treatmentBMPObservation.ObservationType).Value;
            var thresholdValue = treatmentBMPObservation.TreatmentBMPAssessment.TreatmentBMP.GetThresholdValueInObservedUnits(treatmentBMPObservation.ObservationType).Value;
            return ObservationTypeHelper.LinearInterpolation(observationValue, benchmarkValue, thresholdValue);
        }

        protected override double GetObservationValueImpl(TreatmentBMPObservation treatmentBMPObservation)
        {
            return treatmentBMPObservation.TreatmentBMPObservationDetails.Average(x => x.TreatmentBMPObservationValue);
        }
    }
    
    public partial class ObservationTypeMaterialAccumulation
    {
        public override string AssessmentUrl(int treatmentBMPAssessmentID)
        {
            return SitkaRoute<TreatmentBMPAssessmentController>.BuildUrlFromExpression(t => t.MaterialAccumulation(treatmentBMPAssessmentID));
        }
        public override string BenchmarkAndThresholdUrl(int treatmentBMPID)
        {
            return SitkaRoute<TreatmentBMPBenchmarkAndThresholdController>.BuildUrlFromExpression(t => t.MaterialAccumulation(treatmentBMPID));
        }

        public override bool IsComplete(TreatmentBMPObservation treatmentBMPObservation)
        {
            return treatmentBMPObservation.TreatmentBMPObservationDetails.Any();
        }

        public override double CalculateScore(TreatmentBMPObservation treatmentBMPObservation)
        {
            var observationValue = GetObservationValueImpl(treatmentBMPObservation);
            var benchmarkValue = treatmentBMPObservation.TreatmentBMPAssessment.TreatmentBMP.GetBenchmarkValue(treatmentBMPObservation.ObservationType).Value; 
            var thresholdValue = ObservationTypeHelper.ThresholdValueFromThresholdPercentDeclineInDesignDepth(treatmentBMPObservation);
            return ObservationTypeHelper.LinearInterpolation(observationValue, benchmarkValue, thresholdValue);
        }

        protected override double GetObservationValueImpl(TreatmentBMPObservation treatmentBMPObservation)
        {
            return treatmentBMPObservation.TreatmentBMPObservationDetails.Average(x => x.TreatmentBMPObservationValue);
        }
    }
    
    public partial class ObservationTypeRunoff
    {
        public override string AssessmentUrl(int treatmentBMPAssessmentID)
        {
            return SitkaRoute<TreatmentBMPAssessmentController>.BuildUrlFromExpression(t => t.Runoff(treatmentBMPAssessmentID));
        }
        public override string BenchmarkAndThresholdUrl(int treatmentBMPID)
        {
            return SitkaRoute<TreatmentBMPBenchmarkAndThresholdController>.BuildUrlFromExpression(t => t.Runoff(treatmentBMPID));
        }

        public override bool IsComplete(TreatmentBMPObservation treatmentBMPObservation)
        {
            return treatmentBMPObservation.TreatmentBMPObservationDetails.Count > 2;
        }

        public override double CalculateScore(TreatmentBMPObservation treatmentBMPObservation)
        {
            var observationValue = GetObservationValueImpl(treatmentBMPObservation);
            var benchmarkValue = treatmentBMPObservation.TreatmentBMPAssessment.TreatmentBMP.GetBenchmarkValue(treatmentBMPObservation.ObservationType).Value;
            var thresholdValue = treatmentBMPObservation.TreatmentBMPAssessment.TreatmentBMP.GetThresholdValueInObservedUnits(treatmentBMPObservation.ObservationType).Value;
            return ObservationTypeHelper.LinearInterpolation(observationValue, benchmarkValue, thresholdValue);
        }

        protected override double GetObservationValueImpl(TreatmentBMPObservation treatmentBMPObservation)
        {
            return treatmentBMPObservation.TreatmentBMPObservationDetails.Average(x => x.TreatmentBMPObservationValue);
        }
    }
    
    public partial class ObservationTypeSedimentTrapCapacity
    {
        public override string AssessmentUrl(int treatmentBMPAssessmentID)
        {
            return SitkaRoute<TreatmentBMPAssessmentController>.BuildUrlFromExpression(t => t.SedimentTrapCapacity(treatmentBMPAssessmentID));
        }
        public override string BenchmarkAndThresholdUrl(int treatmentBMPID)
        {
            return SitkaRoute<TreatmentBMPBenchmarkAndThresholdController>.BuildUrlFromExpression(t => t.SedimentTrapCapacity(treatmentBMPID));
        }

        public override bool IsComplete(TreatmentBMPObservation treatmentBMPObservation)
        {
            return treatmentBMPObservation.TreatmentBMPObservationDetails.Any();
        }

        public override double CalculateScore(TreatmentBMPObservation treatmentBMPObservation)
        {
            var observationValue = GetObservationValueImpl(treatmentBMPObservation);            
            var benchmarkValue = treatmentBMPObservation.TreatmentBMPAssessment.TreatmentBMP.GetBenchmarkValue(treatmentBMPObservation.ObservationType).Value;
            var thresholdDecline = treatmentBMPObservation.TreatmentBMPAssessment.TreatmentBMP.GetThresholdValue(treatmentBMPObservation.ObservationType).Value;           

            var thresholdValue = benchmarkValue - thresholdDecline;
            return ObservationTypeHelper.LinearInterpolation(observationValue, benchmarkValue, thresholdValue); 
        }

        protected override double GetObservationValueImpl(TreatmentBMPObservation treatmentBMPObservation)
        {
            return treatmentBMPObservation.TreatmentBMPObservationDetails.Average(x => x.TreatmentBMPObservationValue);
        }
    }
    
    public partial class ObservationTypeStandingWater
    {
        public override string AssessmentUrl(int treatmentBMPAssessmentID)
        {
            return SitkaRoute<TreatmentBMPAssessmentController>.BuildUrlFromExpression(t => t.StandingWater(treatmentBMPAssessmentID));
        }
        public override string BenchmarkAndThresholdUrl(int treatmentBMPID)
        {
            return string.Empty;
        }

        public override bool IsComplete(TreatmentBMPObservation treatmentBMPObservation)
        {
            return treatmentBMPObservation.TreatmentBMPObservationDetails.Any();
        }

        public override double CalculateScore(TreatmentBMPObservation treatmentBMPObservation)
        {
            var standingWaterFails = treatmentBMPObservation.TreatmentBMPObservationDetails.Count(x => Math.Abs((int) (x.TreatmentBMPObservationValue - 1)) < 0.01) > 1;
            return standingWaterFails ? 2 : 5;
        }

        protected override double GetObservationValueImpl(TreatmentBMPObservation treatmentBMPObservation)
        {
            return treatmentBMPObservation.TreatmentBMPObservationDetails.Any(x => Math.Abs((int) (x.TreatmentBMPObservationValue - 0)) < 0.01) ? 1 : 0;
        }
    }
    
    public partial class ObservationTypeVaultCapacity
    {
        public override string AssessmentUrl(int treatmentBMPAssessmentID)
        {
            return SitkaRoute<TreatmentBMPAssessmentController>.BuildUrlFromExpression(t => t.VaultCapacity(treatmentBMPAssessmentID));
        }
        public override string BenchmarkAndThresholdUrl(int treatmentBMPID)
        {
            return SitkaRoute<TreatmentBMPBenchmarkAndThresholdController>.BuildUrlFromExpression(t => t.VaultCapacity(treatmentBMPID));
        }

        public override bool IsComplete(TreatmentBMPObservation treatmentBMPObservation)
        {
            return treatmentBMPObservation.TreatmentBMPObservationDetails.Any();
        }

        public override double CalculateScore(TreatmentBMPObservation treatmentBMPObservation)
        {
            var observationValue = GetObservationValueImpl(treatmentBMPObservation);
            var benchmarkValue = treatmentBMPObservation.TreatmentBMPAssessment.TreatmentBMP.GetBenchmarkValue(treatmentBMPObservation.ObservationType).Value;
            var thresholdValue = treatmentBMPObservation.TreatmentBMPAssessment.TreatmentBMP.GetThresholdValueInObservedUnits(treatmentBMPObservation.ObservationType).Value;
            return ObservationTypeHelper.LinearInterpolation(observationValue, benchmarkValue, thresholdValue);
        }

        protected override double GetObservationValueImpl(TreatmentBMPObservation treatmentBMPObservation)
        {
            return treatmentBMPObservation.TreatmentBMPObservationDetails.Average(x => x.TreatmentBMPObservationValue);
        }
    }
    
    public partial class ObservationTypeVegetativeCover
    {
        public override string AssessmentUrl(int treatmentBMPAssessmentID)
        {
            return SitkaRoute<TreatmentBMPAssessmentController>.BuildUrlFromExpression(t => t.VegetativeCover(treatmentBMPAssessmentID));
        }
        public override string BenchmarkAndThresholdUrl(int treatmentBMPID)
        {
            return SitkaRoute<TreatmentBMPBenchmarkAndThresholdController>.BuildUrlFromExpression(t => t.VegetativeCover(treatmentBMPID));
        }

        public override bool IsComplete(TreatmentBMPObservation treatmentBMPObservation)
        {
            var treatmentBMPType = treatmentBMPObservation.TreatmentBMPAssessment.TreatmentBMP.TreatmentBMPType;
            var relevantObservationDetailTypes = TreatmentBMPObservationDetailType.GetRelevantDetailTypesForTreatmentBMPType(treatmentBMPType).Where(x => x.ObservationType == treatmentBMPObservation.ObservationType).ToList();
            var observedObservationDetailTypes = treatmentBMPObservation.TreatmentBMPObservationDetails.Select(x => x.TreatmentBMPObservationDetailType).ToList();
            return relevantObservationDetailTypes.Select(relevantObservationDetailType => observedObservationDetailTypes.Any(x => x == relevantObservationDetailType)).All(exists => exists);
        }

        public override double CalculateScore(TreatmentBMPObservation treatmentBMPObservation)
        {
            var observationValue = GetObservationValueImpl(treatmentBMPObservation);
            var benchmarkValue = treatmentBMPObservation.TreatmentBMPAssessment.TreatmentBMP.GetBenchmarkValue(treatmentBMPObservation.ObservationType).Value;
            var thresholdValue = treatmentBMPObservation.TreatmentBMPAssessment.TreatmentBMP.GetThresholdValueInObservedUnits(treatmentBMPObservation.ObservationType).Value;
            return ObservationTypeHelper.LinearInterpolation(observationValue, benchmarkValue, thresholdValue);           
        }

        protected override double GetObservationValueImpl(TreatmentBMPObservation treatmentBMPObservation)
        {
            var treatmentBMPType = treatmentBMPObservation.TreatmentBMPAssessment.TreatmentBMP.TreatmentBMPType;
            var treatmentBMPObservationDetailTypesForScoring = TreatmentBMPObservationDetailType.GetScoredDetailTypesForTreatmentBMPType(treatmentBMPType);
            var treatmentBMPObservationDetailsForScoring = treatmentBMPObservation.TreatmentBMPObservationDetails.Where(x => treatmentBMPObservationDetailTypesForScoring.Contains(x.TreatmentBMPObservationDetailType));

            return treatmentBMPObservationDetailsForScoring.Sum(x => x.TreatmentBMPObservationValue);
        }
    }
    
    public partial class ObservationTypeWetBasinVegetativeCover
    {
        public override string AssessmentUrl(int treatmentBMPAssessmentID)
        {
            return SitkaRoute<TreatmentBMPAssessmentController>.BuildUrlFromExpression(t => t.WetBasinVegetativeCover(treatmentBMPAssessmentID));
        }
        public override string BenchmarkAndThresholdUrl(int treatmentBMPID)
        {
            return SitkaRoute<TreatmentBMPBenchmarkAndThresholdController>.BuildUrlFromExpression(t => t.WetBasinVegetativeCover(treatmentBMPID));
        }

        public override bool IsComplete(TreatmentBMPObservation treatmentBMPObservation)
        {
            var treatmentBMPType = treatmentBMPObservation.TreatmentBMPAssessment.TreatmentBMP.TreatmentBMPType;
            var relevantObservationDetailTypes = TreatmentBMPObservationDetailType.GetRelevantDetailTypesForTreatmentBMPType(treatmentBMPType).Where(x => x.ObservationType == treatmentBMPObservation.ObservationType);
            var observedObservationDetailTypes = treatmentBMPObservation.TreatmentBMPObservationDetails.Select(x => x.TreatmentBMPObservationDetailType).ToList();
            return relevantObservationDetailTypes.Select(relevantObservationDetailType => observedObservationDetailTypes.Any(x => x == relevantObservationDetailType)).All(exists => exists);
        }

        public override double CalculateScore(TreatmentBMPObservation treatmentBMPObservation)
        {
            var observationValue = GetObservationValueImpl(treatmentBMPObservation);
            var benchmarkValue = treatmentBMPObservation.TreatmentBMPAssessment.TreatmentBMP.GetBenchmarkValue(treatmentBMPObservation.ObservationType).Value;
            var thresholdValue = treatmentBMPObservation.TreatmentBMPAssessment.TreatmentBMP.GetWetBasinThresholdValueInObservedUnits(treatmentBMPObservation).Value;
            return ObservationTypeHelper.WetBasinBipolarLinearInterpolation(observationValue, benchmarkValue, thresholdValue);
        }

        protected override double GetObservationValueImpl(TreatmentBMPObservation treatmentBMPObservation)
        {
            var treatmentBMPType = treatmentBMPObservation.TreatmentBMPAssessment.TreatmentBMP.TreatmentBMPType;
            var treatmentBMPObservationDetailTypesForScoring = TreatmentBMPObservationDetailType.GetScoredDetailTypesForTreatmentBMPType(treatmentBMPType);
            var treatmentBMPObservationDetailsForScoring = treatmentBMPObservation.TreatmentBMPObservationDetails.Where(x => treatmentBMPObservationDetailTypesForScoring.Contains(x.TreatmentBMPObservationDetailType));

            return treatmentBMPObservationDetailsForScoring.Sum(x => x.TreatmentBMPObservationValue);
        }
    }

    public partial class ObservationTypeInstallation
    {
        public override string AssessmentUrl(int treatmentBMPAssessmentID)
        {
            return SitkaRoute<TreatmentBMPAssessmentController>.BuildUrlFromExpression(t => t.Installation(treatmentBMPAssessmentID));
        }
        public override string BenchmarkAndThresholdUrl(int treatmentBMPID)
        {
            return string.Empty;
        }

        public override bool IsComplete(TreatmentBMPObservation treatmentBMPObservation)
        {
            return treatmentBMPObservation.TreatmentBMPObservationDetails.Any();
        }

        public override double CalculateScore(TreatmentBMPObservation treatmentBMPObservation)
        {
            var installationFails = treatmentBMPObservation.TreatmentBMPObservationDetails.Any(x => Math.Abs((int) (x.TreatmentBMPObservationValue - 0)) < 0.01);
            return installationFails ? 2 : 5;
        }

        protected override double GetObservationValueImpl(TreatmentBMPObservation treatmentBMPObservation)
        {
            return treatmentBMPObservation.TreatmentBMPObservationDetails.Any(x => Math.Abs((int) (x.TreatmentBMPObservationValue - 0)) < 0.01) ? 1 : 0;
        }
    }

    #endregion
}
