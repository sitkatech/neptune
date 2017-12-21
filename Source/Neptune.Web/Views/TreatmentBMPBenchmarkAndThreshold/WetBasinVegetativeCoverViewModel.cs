/*-----------------------------------------------------------------------
<copyright file="WetBasinVegetativeCoverViewModel.cs" company="Tahoe Regional Planning Agency">
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
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LtInfo.Common;
using LtInfo.Common.Models;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMPBenchmarkAndThreshold
{
    public class WetBasinVegetativeCoverViewModel : FormViewModel, IValidatableObject
    {
        public int TreatmentBMPID { get; set; }

        [Required]
        [Range(0, 100)]
        [DisplayName("Minimum Vegetative Cover (%)")]
        public double? ThresholdValueMin { get; set; }

        [Required]
        [Range(0, 100)]
        [DisplayName("Maximum Vegetative Cover (%)")]
        public double? ThresholdValueMax { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public  WetBasinVegetativeCoverViewModel()
        {
        }

        public WetBasinVegetativeCoverViewModel(Models.TreatmentBMP treatmentBMP)
        {
            TreatmentBMPID = treatmentBMP.TreatmentBMPID;
            var benchmarkAndThreshold = treatmentBMP.TreatmentBMPBenchmarkAndThresholds.FirstOrDefault(x => x.ObservationTypeID == ObservationType.WetBasinVegetativeCover.ObservationTypeID);

            if (benchmarkAndThreshold != null)
            {
                ThresholdValueMin = Models.TreatmentBMPType.WetBasin.CalculateVegetativeCoverMinThresholdFromBenchmarkAndDeviation(benchmarkAndThreshold.BenchmarkValue, benchmarkAndThreshold.ThresholdValue);
                ThresholdValueMax = Models.TreatmentBMPType.WetBasin.CalculateVegetativeCoverMaxThresholdFromBenchmarkAndDeviation(benchmarkAndThreshold.BenchmarkValue, benchmarkAndThreshold.ThresholdValue);
            }
            
        }

        public void UpdateModel(Models.TreatmentBMP treatmentBMP, Person currentPerson)
        {
            var benchmark = Models.TreatmentBMPType.WetBasin.CalculateVegetativeCoverBenchmarkFromMinAndMaxThresholds(ThresholdValueMin.Value, ThresholdValueMax.Value);
            var threshold = Models.TreatmentBMPType.WetBasin.CalculateVegetativeCoverDeviationFromMinAndMaxThresholds(ThresholdValueMin.Value, ThresholdValueMax.Value);

            var benchmarkAndThreshold = treatmentBMP.TreatmentBMPBenchmarkAndThresholds.FirstOrDefault(x => x.ObservationTypeID == ObservationType.WetBasinVegetativeCover.ObservationTypeID) ??
                                        new Models.TreatmentBMPBenchmarkAndThreshold(treatmentBMP, ObservationType.WetBasinVegetativeCover, benchmark, threshold);

            benchmarkAndThreshold.BenchmarkValue = benchmark;
            benchmarkAndThreshold.ThresholdValue = threshold;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            if (ThresholdValueMax <= ThresholdValueMin)
            {
                validationResults.Add(new SitkaValidationResult<WetBasinVegetativeCoverViewModel, double?>("Threshold max value should be greater than Threshold min value", m => m.ThresholdValueMax));
            }

            return validationResults;
        }
    }
}
