/*-----------------------------------------------------------------------
<copyright file="TreatmentBMPObservationDetailTypeSimple.cs" company="Tahoe Regional Planning Agency">
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
namespace Neptune.Web.Models
{
    public class TreatmentBMPObservationDetailTypeSimple
    {
         /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public TreatmentBMPObservationDetailTypeSimple()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMPObservationDetailTypeSimple(int treatmentBMPObservationDetailTypeID, string treatmentBMPObservationDetailTypeName, string treatmentBMPObservationDetailTypeDisplayName)
            : this()
        {
            TreatmentBMPObservationDetailTypeID = treatmentBMPObservationDetailTypeID;
            TreatmentBMPObservationDetailTypeName = treatmentBMPObservationDetailTypeName;
            TreatmentBMPObservationDetailTypeDisplayName = treatmentBMPObservationDetailTypeDisplayName;            
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public TreatmentBMPObservationDetailTypeSimple(TreatmentBMPObservationDetailType treatmentBMPObservationDetailType)
            : this()
        {
            TreatmentBMPObservationDetailTypeID = treatmentBMPObservationDetailType.TreatmentBMPObservationDetailTypeID;
            TreatmentBMPObservationDetailTypeName = treatmentBMPObservationDetailType.TreatmentBMPObservationDetailTypeName;
            TreatmentBMPObservationDetailTypeDisplayName = treatmentBMPObservationDetailType.TreatmentBMPObservationDetailTypeDisplayName;  
        }

        public int TreatmentBMPObservationDetailTypeID { get; set; }
        public string TreatmentBMPObservationDetailTypeName { get; set; }
        public string TreatmentBMPObservationDetailTypeDisplayName { get; set; }

    }
    
}
