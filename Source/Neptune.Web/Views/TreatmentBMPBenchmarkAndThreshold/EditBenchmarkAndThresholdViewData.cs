/*-----------------------------------------------------------------------
<copyright file="RunoffViewData.cs" company="Tahoe Regional Planning Agency">
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

using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMPBenchmarkAndThreshold
{
    public class EditBenchmarkAndThresholdViewData : BenchmarkAndThresholdViewData
    {
        public string BenchmarkMeasurementUnitTypeDisplayName { get; }
        public string ThresholdMeasurementUnitTypeDisplayName { get; }
        public string BenchmarkMeasurementUnitLabel { get; }
        public string ThresholdMeasurementUnitLabel { get; }
        public string BenchmarkDescription { get; }
        public string ThresholdDescription { get; }

        public string DefaultBenchmarkText { get; }
        public string DefaultThresholdText { get; }
        public string DefaultBenchmarkPlaceholder { get; }
        public string DefaultThresholdPlaceholder { get; }
        public bool TargetIsSweetSpot { get; }

        public EditBenchmarkAndThresholdViewData(Person currentPerson, Models.TreatmentBMP treatmentBMP,
            Models.TreatmentBMPAssessmentObservationType TreatmentBMPAssessmentObservationType)
            : base(currentPerson, treatmentBMP, TreatmentBMPAssessmentObservationType)
        {
            BenchmarkMeasurementUnitTypeDisplayName = TreatmentBMPAssessmentObservationType.BenchmarkMeasurementUnitType().MeasurementUnitTypeDisplayName;
            ThresholdMeasurementUnitTypeDisplayName = TreatmentBMPAssessmentObservationType.ThresholdMeasurementUnitType().MeasurementUnitTypeDisplayName;

            BenchmarkMeasurementUnitLabel = TreatmentBMPAssessmentObservationType.BenchmarkMeasurementUnitLabel();
            ThresholdMeasurementUnitLabel = TreatmentBMPAssessmentObservationType.ThresholdMeasurementUnitLabel();

            BenchmarkDescription = TreatmentBMPAssessmentObservationType.BenchmarkDescription();
            ThresholdDescription = TreatmentBMPAssessmentObservationType.ThresholdDescription();

            var TreatmentBMPTypeAssessmentObservationType = treatmentBMP.TreatmentBMPType.GetTreatmentBMPTypeObservationType(TreatmentBMPAssessmentObservationType);

            DefaultBenchmarkPlaceholder = TreatmentBMPTypeAssessmentObservationType.DefaultBenchmarkValue.HasValue ? "default is " + TreatmentBMPTypeAssessmentObservationType.DefaultBenchmarkValue.Value : string.Empty;
            DefaultBenchmarkText = TreatmentBMPTypeAssessmentObservationType.DefaultBenchmarkValue.HasValue ? $"The default value is {TreatmentBMPTypeAssessmentObservationType.DefaultBenchmarkValue} {TreatmentBMPAssessmentObservationType.BenchmarkMeasurementUnitType().MeasurementUnitTypeDisplayName}." : string.Empty;

            DefaultThresholdPlaceholder = TreatmentBMPTypeAssessmentObservationType.DefaultThresholdValue.HasValue ? "default is " + TreatmentBMPTypeAssessmentObservationType.DefaultThresholdValue.Value : string.Empty;
            TargetIsSweetSpot = TreatmentBMPAssessmentObservationType.TargetIsSweetSpot;
            var optionalPlusMinus = TreatmentBMPAssessmentObservationType.TargetIsSweetSpot ? "+/-" : "";
            DefaultThresholdText = TreatmentBMPTypeAssessmentObservationType.DefaultThresholdValue.HasValue ? $"The default value is {optionalPlusMinus} {TreatmentBMPTypeAssessmentObservationType.DefaultThresholdValue} {TreatmentBMPAssessmentObservationType.ThresholdMeasurementUnitType().MeasurementUnitTypeDisplayName}." : string.Empty;
        }
    }
}
