/*-----------------------------------------------------------------------
<copyright file="InfiltrationRateViewModel.cs" company="Tahoe Regional Planning Agency">
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
using LtInfo.Common.Models;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMPBenchmarkAndThreshold
{
    public class InfiltrationRateViewModel : FormViewModel, IValidatableObject
    {
        public int TreatmentBMPID { get; set; }

        [Required]
        [DisplayName("Infiltration (in/hr)")]
        [Range(0, int.MaxValue, ErrorMessage = "Must be a positive number")]
        public double? BenchmarkValue { get; set; }

        [Required]
        [Range(0, 100)]
        [DisplayName("Percent Decline (%)")]
        public double? ThresholdValue { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public InfiltrationRateViewModel()
        {
        }

        public InfiltrationRateViewModel(Models.TreatmentBMP treatmentBMP)
        {
            TreatmentBMPID = treatmentBMP.TreatmentBMPID;
            var benchmarkAndThreshold = treatmentBMP.TreatmentBMPBenchmarkAndThresholds.FirstOrDefault(x => x.ObservationTypeID == ObservationType.InfiltrationRate.ObservationTypeID);

            if (benchmarkAndThreshold != null)
            {
                BenchmarkValue = benchmarkAndThreshold.BenchmarkValue;
                ThresholdValue = benchmarkAndThreshold.ThresholdValue;    
            }
            
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
