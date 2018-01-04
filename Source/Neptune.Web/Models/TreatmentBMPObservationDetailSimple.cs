/*-----------------------------------------------------------------------
<copyright file="TreatmentBMPObservationDetailSimple.cs" company="Tahoe Regional Planning Agency">
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

namespace Neptune.Web.Models
{
    public class TreatmentBMPObservationDetailSimple
    {
        public int? TreatmentBMPObservationDetailID { get; set; }
        [Required]
        public int TreatmentBMPObservationDetailTypeID { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Value must be greater than zero.")]
        public double? TreatmentBMPObservationValue { get; set; }
        public string Notes { get; set; }

          /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public TreatmentBMPObservationDetailSimple()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMPObservationDetailSimple(int treatmentBMPObservationDetaiID, int treatmentBMPObservationDetailTypeID, double? treatmentBMPObservationValue, string notes)
            : this()
        {
            TreatmentBMPObservationDetailID = treatmentBMPObservationDetaiID;
            TreatmentBMPObservationDetailTypeID = treatmentBMPObservationDetailTypeID;
            TreatmentBMPObservationValue = treatmentBMPObservationValue;
            Notes = notes;
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMPObservationDetailSimple(int treatmentBMPObservationDetailTypeID, double? treatmentBMPObservationValue, string notes)
            : this()
        {
            TreatmentBMPObservationDetailTypeID = treatmentBMPObservationDetailTypeID;
            TreatmentBMPObservationValue = treatmentBMPObservationValue;
            Notes = notes;
        }
      
    }
}
