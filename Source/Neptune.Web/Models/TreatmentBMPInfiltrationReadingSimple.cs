/*-----------------------------------------------------------------------
<copyright file="TreatmentBMPInfiltrationReadingSimple.cs" company="Tahoe Regional Planning Agency">
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
    public class TreatmentBMPInfiltrationReadingSimple
    {
        public int? TreatmentBMPInfiltrationReadingID { get; set; }

        public int? TreatmentBMPObservationDetailID { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Value must be greater than zero.")]
        [Required]
        public double? ReadingValue { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Value must be greater than zero.")]
        [Required]
        public double? ReadingTime { get; set; }
        
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public TreatmentBMPInfiltrationReadingSimple()
        {
        }

        public TreatmentBMPInfiltrationReadingSimple(int? treatmentBMPInfiltrationReadingID, int? treatmentBMPObservationDetailID, double? readingValue, double? readingTime)
        {            
            TreatmentBMPInfiltrationReadingID = treatmentBMPInfiltrationReadingID;
            TreatmentBMPObservationDetailID = treatmentBMPObservationDetailID;
            ReadingValue = readingValue;
            ReadingTime = readingTime;
        }        
    }
}
