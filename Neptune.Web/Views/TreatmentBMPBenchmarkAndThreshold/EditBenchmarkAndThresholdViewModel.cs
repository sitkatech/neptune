/*-----------------------------------------------------------------------
<copyright file="RunoffViewModel.cs" company="Tahoe Regional Planning Agency">
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

using System.ComponentModel.DataAnnotations;
using Neptune.EFModels.Entities;
using Neptune.Web.Common.Models;

namespace Neptune.Web.Views.TreatmentBMPBenchmarkAndThreshold
{
    public class EditBenchmarkAndThresholdViewModel : FormViewModel
    {
        public int TreatmentBMPID { get; set; }
        public int TreatmentBMPAssessmentObservationTypeID { get; set; }

        [Required]
        public double? BenchmarkValue { get; set; }

        [Required]
        public double? ThresholdValue { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditBenchmarkAndThresholdViewModel()
        {
        }

        public EditBenchmarkAndThresholdViewModel(EFModels.Entities.TreatmentBMP treatmentBMP, EFModels.Entities.TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType)
        {
            TreatmentBMPID = treatmentBMP.TreatmentBMPID;
            TreatmentBMPAssessmentObservationTypeID = treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID;
            var benchmarkAndThreshold = treatmentBMP.TreatmentBMPBenchmarkAndThresholds.FirstOrDefault(x => x.TreatmentBMPAssessmentObservationTypeID == treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID);

            BenchmarkValue = benchmarkAndThreshold?.BenchmarkValue;
            ThresholdValue = benchmarkAndThreshold?.ThresholdValue;             
        }

        public void UpdateModel(EFModels.Entities.TreatmentBMPBenchmarkAndThreshold treatmentBMPBenchmarkAndThreshold, Person currentPerson)
        {
            treatmentBMPBenchmarkAndThreshold.BenchmarkValue = BenchmarkValue.Value;
            treatmentBMPBenchmarkAndThreshold.ThresholdValue = ThresholdValue.Value;
        }
    }
}
