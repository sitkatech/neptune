/*-----------------------------------------------------------------------
<copyright file="TreatmentBMP.cs" company="Tahoe Regional Planning Agency">
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

using System.Collections.Generic;
using System.Linq;
using System.Web;
using LtInfo.Common;
using LtInfo.Common.Views;
using Neptune.Web.Security;

namespace Neptune.Web.Models
{
    public partial class TreatmentBMP : IAuditableEntity
    {
        public bool CanView(Person person)
        {
            return new NeptuneViewFeature().HasPermissionByPerson(person);
        }

        public bool CanEdit(Person person)
        {
            return new TreatmentBMPManageFeature().HasPermission(person, this).HasPermission;
        }

        public bool CanDelete()
        {
            return TreatmentBMPAssessments.Count.Equals(0);
        }
       
        public string AuditDescriptionString => TreatmentBMPName; public string FormattedNameAndType => $"{TreatmentBMPName} ({TreatmentBMPType.TreatmentBMPTypeName})";

        public double? GetBenchmarkValue(ObservationType observationType)
        {
            var treatmentBMPBenchmarkAndThreshold = TreatmentBMPBenchmarkAndThresholds
               .SingleOrDefault(x => x.ObservationType == observationType);

            return treatmentBMPBenchmarkAndThreshold?.BenchmarkValue;
        }

        public double? GetThresholdValue(ObservationType observationType)
        {
            var treatmentBMPBenchmarkAndThreshold = TreatmentBMPBenchmarkAndThresholds
                .SingleOrDefault(x => x.ObservationType == observationType);

            return treatmentBMPBenchmarkAndThreshold?.ThresholdValue;
        }

        public double? GetThresholdValueInObservedUnits(ObservationType observationType)
        {
            var thresholdValue = GetThresholdValue(observationType);

            if (thresholdValue == null)
            {
                return null;
            }
           
            return observationType.ObservationTypeSpecification.ObservationThresholdType == ObservationThresholdType.PercentFromBenchmark
                ? ObservationTypeHelper.ThresholdValueFromThresholdPercentDecline(GetBenchmarkValue(observationType).Value, thresholdValue.Value)
                : thresholdValue.Value;
        }

        public string GetFormattedBenchmarkValue(ObservationType observationType)
        {
            if (!observationType.HasBenchmarkAndThreshold)
            {
                return ViewUtilities.NaString;
            }

            var benchmarkValue = GetBenchmarkValue(observationType);
            if (benchmarkValue == null)
            {
                return "-";
            }

            var optionalSpace = observationType.MeasurementUnitType == MeasurementUnitType.Percent ? "" : " ";
            return $"{benchmarkValue}{optionalSpace}{observationType.MeasurementUnitType.LegendDisplayName}";
        }

        public string GetFormattedThresholdValue(ObservationType observationType, bool inObservedUnits)
        {
            if (!observationType.HasBenchmarkAndThreshold)
            {
                return ViewUtilities.NaString;
            }

            var valueInObservedUnits = GetThresholdValueInObservedUnits(observationType);
            var formattedThresholdValueInObservedUnits = $"{valueInObservedUnits} {observationType.MeasurementUnitType.LegendDisplayName}";
            if (inObservedUnits)
            {
                return formattedThresholdValueInObservedUnits;
            }

            var formattedThresholdValueAsPercent = ObservationTypeHelper.ApplyThresholdFormatting(observationType, GetThresholdValue(observationType));
            var optionalFormattedValueInObservedUnits = valueInObservedUnits.HasValue && observationType.MeasurementUnitType != MeasurementUnitType.Percent ? $" ({formattedThresholdValueInObservedUnits})"
                : string.Empty;
            return $"{formattedThresholdValueAsPercent}{optionalFormattedValueInObservedUnits}";
        }

        public bool IsBenchmarkAndThresholdsComplete()
        {
            var observationTypesIDs = TreatmentBMPType.GetObservationTypes().Where(x => x.HasBenchmarkAndThreshold).Select(x => x.ObservationTypeID).ToList();
            var benchmarkAndThresholdObservationTypeIDs = TreatmentBMPBenchmarkAndThresholds.Select(x => x.ObservationTypeID).ToList();

            return !observationTypesIDs.Except(benchmarkAndThresholdObservationTypeIDs).Any();
        }

        public bool HasSettableBenchmarkAndThresholdValues()
        {
            return TreatmentBMPType.GetObservationTypes().Any(x => x.HasBenchmarkAndThreshold);
        }

        public string GetMostRecentScoreAsString()
        {
            var latestAssessment = GetMostRecentAssessment();
            if (latestAssessment == null)
            {
                return string.Empty;
            }
            return latestAssessment.AlternateAssessmentScore.HasValue ? latestAssessment.AlternateAssessmentScore.ToString() : latestAssessment.FormattedScore();
        }

        public TreatmentBMPAssessment GetMostRecentAssessment()
        {
            var latestAssessment = TreatmentBMPAssessments.OrderByDescending(x => x.AssessmentDate).FirstOrDefault(x => x.HasCalculatedOrAlternateScore());
            return latestAssessment;
        }     

        public double? GetWetBasinThresholdValueInObservedUnits(TreatmentBMPObservation treatmentBMPObservation)
        {
            var observationValue = treatmentBMPObservation.ObservationType.GetObservationValue(treatmentBMPObservation);
            var benchmarkValue = GetBenchmarkValue(treatmentBMPObservation.ObservationType).Value;
            var thresholdValue = GetThresholdValue(treatmentBMPObservation.ObservationType).Value;
            return observationValue < benchmarkValue ? benchmarkValue - thresholdValue : benchmarkValue + thresholdValue;
        }

        public bool HasDependentObjectsBesidesBenchmarksAndThresholds()
        {
            return TreatmentBMPAssessments.Any();
        }
    }
}
