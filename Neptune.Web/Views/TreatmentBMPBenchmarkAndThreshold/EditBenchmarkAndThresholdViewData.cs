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

using Neptune.EFModels.Entities;
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

        public EditBenchmarkAndThresholdViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson, EFModels.Entities.TreatmentBMP treatmentBMP,
            EFModels.Entities.TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType, EFModels.Entities.TreatmentBMPType treatmentBMPType, List<EFModels.Entities.TreatmentBMPBenchmarkAndThreshold> treatmentBMPBenchmarkAndThresholds)
            : base(httpContext, linkGenerator, currentPerson, treatmentBMP, treatmentBMPAssessmentObservationType, treatmentBMPType, treatmentBMPBenchmarkAndThresholds)
        {
            BenchmarkMeasurementUnitTypeDisplayName = treatmentBMPAssessmentObservationType.BenchmarkMeasurementUnitType().MeasurementUnitTypeDisplayName;
            ThresholdMeasurementUnitTypeDisplayName = treatmentBMPAssessmentObservationType.ThresholdMeasurementUnitType().MeasurementUnitTypeDisplayName;

            BenchmarkMeasurementUnitLabel = treatmentBMPAssessmentObservationType.BenchmarkMeasurementUnitLabel();
            ThresholdMeasurementUnitLabel = treatmentBMPAssessmentObservationType.ThresholdMeasurementUnitLabel();

            BenchmarkDescription = treatmentBMPAssessmentObservationType.BenchmarkDescription();
            ThresholdDescription = treatmentBMPAssessmentObservationType.ThresholdDescription();

            var treatmentBMPTypeAssessmentObservationType = treatmentBMPType.GetTreatmentBMPTypeObservationType(treatmentBMPAssessmentObservationType);

            DefaultBenchmarkPlaceholder = treatmentBMPTypeAssessmentObservationType.DefaultBenchmarkValue.HasValue ? "default is " + treatmentBMPTypeAssessmentObservationType.DefaultBenchmarkValue.Value : string.Empty;
            DefaultBenchmarkText = treatmentBMPTypeAssessmentObservationType.DefaultBenchmarkValue.HasValue ? $"The default value is {treatmentBMPTypeAssessmentObservationType.DefaultBenchmarkValue} {treatmentBMPAssessmentObservationType.BenchmarkMeasurementUnitType().MeasurementUnitTypeDisplayName}." : string.Empty;

            DefaultThresholdPlaceholder = treatmentBMPTypeAssessmentObservationType.DefaultThresholdValue.HasValue ? "default is " + treatmentBMPTypeAssessmentObservationType.DefaultThresholdValue.Value : string.Empty;
            TargetIsSweetSpot = treatmentBMPAssessmentObservationType.GetTargetIsSweetSpot();
            var optionalPlusMinus = treatmentBMPAssessmentObservationType.GetTargetIsSweetSpot() ? "+/-" : "";
            DefaultThresholdText = treatmentBMPTypeAssessmentObservationType.DefaultThresholdValue.HasValue ? $"The default value is {optionalPlusMinus} {treatmentBMPTypeAssessmentObservationType.DefaultThresholdValue} {treatmentBMPAssessmentObservationType.ThresholdMeasurementUnitType().MeasurementUnitTypeDisplayName}." : string.Empty;
        }
    }
}
