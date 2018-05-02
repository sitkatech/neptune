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
            Models.ObservationType observationType)
            : base(currentPerson, treatmentBMP, observationType)
        {
            BenchmarkMeasurementUnitTypeDisplayName = observationType.BenchmarkMeasurementUnitType().MeasurementUnitTypeDisplayName;
            ThresholdMeasurementUnitTypeDisplayName = observationType.ThresholdMeasurementUnitType().MeasurementUnitTypeDisplayName;

            BenchmarkMeasurementUnitLabel = observationType.BenchmarkMeasurementUnitLabel();
            ThresholdMeasurementUnitLabel = observationType.ThresholdMeasurementUnitLabel();

            BenchmarkDescription = observationType.BenchmarkDescription();
            ThresholdDescription = observationType.ThresholdDescription();

            var treatmentBMPTypeObservationType = treatmentBMP.TreatmentBMPType.GetTreatmentBMPTypeObservationType(observationType);

            DefaultBenchmarkPlaceholder = treatmentBMPTypeObservationType.DefaultBenchmarkValue.HasValue ? "default is " + treatmentBMPTypeObservationType.DefaultBenchmarkValue.Value : string.Empty;
            DefaultBenchmarkText = treatmentBMPTypeObservationType.DefaultBenchmarkValue.HasValue ? $"The default value is {treatmentBMPTypeObservationType.DefaultBenchmarkValue} {observationType.BenchmarkMeasurementUnitType().MeasurementUnitTypeDisplayName}." : string.Empty;

            DefaultThresholdPlaceholder = treatmentBMPTypeObservationType.DefaultThresholdValue.HasValue ? "default is " + treatmentBMPTypeObservationType.DefaultThresholdValue.Value : string.Empty;
            TargetIsSweetSpot = observationType.TargetIsSweetSpot;
            var optionalPlusMinus = observationType.TargetIsSweetSpot ? "+/-" : "";
            DefaultThresholdText = treatmentBMPTypeObservationType.DefaultThresholdValue.HasValue ? $"The default value is {optionalPlusMinus} {treatmentBMPTypeObservationType.DefaultThresholdValue} {observationType.ThresholdMeasurementUnitType().MeasurementUnitTypeDisplayName}." : string.Empty;
        }
    }
}
