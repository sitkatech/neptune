﻿/*-----------------------------------------------------------------------
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

using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LtInfo.Common;
using LtInfo.Common.Models;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMPBenchmarkAndThreshold
{
    public class EditBenchmarkAndThresholdViewModel : FormViewModel, IValidatableObject
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

        public EditBenchmarkAndThresholdViewModel(Models.TreatmentBMP treatmentBMP, Models.TreatmentBMPAssessmentObservationType TreatmentBMPAssessmentObservationType)
        {
            TreatmentBMPID = treatmentBMP.TreatmentBMPID;
            TreatmentBMPAssessmentObservationTypeID = TreatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID;
            var benchmarkAndThreshold = treatmentBMP.TreatmentBMPBenchmarkAndThresholds.FirstOrDefault(x => x.TreatmentBMPAssessmentObservationTypeID == TreatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID);

            BenchmarkValue = benchmarkAndThreshold?.BenchmarkValue;
            ThresholdValue = benchmarkAndThreshold?.ThresholdValue;             
        }

        public void UpdateModel(Models.TreatmentBMPBenchmarkAndThreshold benchmarkAndThreshold, Person currentPerson)
        {
            benchmarkAndThreshold.BenchmarkValue = BenchmarkValue.Value;
            benchmarkAndThreshold.ThresholdValue = ThresholdValue.Value;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            
            return validationResults;
        }
    }
}
