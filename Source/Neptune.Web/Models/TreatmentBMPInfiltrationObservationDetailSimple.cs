/*-----------------------------------------------------------------------
<copyright file="TreatmentBMPInfiltrationObservationDetailSimple.cs" company="Tahoe Regional Planning Agency">
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
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Neptune.Web.Models
{
    public class TreatmentBMPInfiltrationObservationDetailSimple : IValidatableObject
    {
        public int? TreatmentBMPObservationDetailID { get; set; }
        [Required]
        public int TreatmentBMPObservationDetailTypeID { get; set; }
        
        public double? TreatmentBMPObservationValue { get; set; }
        public string Notes { get; set; }
        public List<TreatmentBMPInfiltrationReadingSimple> TreatmentBMPInfiltrationReadingSimples { get; set; }
        [Required]
        public bool HasReadings { get; set; }

        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public TreatmentBMPInfiltrationObservationDetailSimple()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMPInfiltrationObservationDetailSimple(int treatmentBMPObservationDetaiID, int treatmentBMPObservationDetailTypeID, double? treatmentBMPObservationValue, string notes, List<TreatmentBMPInfiltrationReadingSimple> treatmentBMPInfiltrationReadingSimples)
        {
            TreatmentBMPObservationDetailID = treatmentBMPObservationDetaiID;
            TreatmentBMPObservationDetailTypeID = treatmentBMPObservationDetailTypeID;
            TreatmentBMPObservationValue = treatmentBMPObservationValue;
            Notes = notes;
            TreatmentBMPInfiltrationReadingSimples = treatmentBMPInfiltrationReadingSimples;
            HasReadings = treatmentBMPInfiltrationReadingSimples.Any();
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public TreatmentBMPInfiltrationObservationDetailSimple(TreatmentBMPObservationDetail treatmentBMPObservationDetail)
        {
            TreatmentBMPObservationDetailID = treatmentBMPObservationDetail.TreatmentBMPObservationDetailID;
            TreatmentBMPObservationDetailTypeID = treatmentBMPObservationDetail.TreatmentBMPObservationDetailTypeID;

            TreatmentBMPObservationValue = treatmentBMPObservationDetail.TreatmentBMPObservationValue;
            Notes = treatmentBMPObservationDetail.Notes;  

            TreatmentBMPInfiltrationReadingSimples = treatmentBMPObservationDetail.TreatmentBMPInfiltrationReadings.Select(x => new TreatmentBMPInfiltrationReadingSimple(x)).OrderBy(x => x.ReadingTime).ToList();
            HasReadings = treatmentBMPObservationDetail.TreatmentBMPInfiltrationReadings.Any();
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();           

            if (!HasReadings && TreatmentBMPObservationValue < 0)
            {
                validationResults.Add(new ValidationResult("Infiltration Rate must not be negative."));
            }
            return validationResults;

        }
    }
}
