/*-----------------------------------------------------------------------
<copyright file="ConvertTreatmentBMPTypeViewModel.cs" company="Tahoe Regional Planning Agency">
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
using System.Linq;
using LtInfo.Common.Models;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMP
{
    public class ConvertTreatmentBMPTypeViewModel : FormViewModel
    {
        [Required(ErrorMessage = "Choose a BMP Type to convert to")]
        [FieldDefinitionDisplay(FieldDefinitionEnum.TreatmentBMPType)]
        public int? TreatmentBMPTypeID { get; set; }


        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public ConvertTreatmentBMPTypeViewModel()
        {
        }


        public void UpdateModel(Models.TreatmentBMP treatmentBMP, Person currentPerson, DatabaseEntities databaseEntities)
        {
            // delete any assessment, benchmark, and maintenance records
            foreach (var maintenanceRecord in treatmentBMP.MaintenanceRecords.ToList())
            {
                maintenanceRecord.DeleteFull(databaseEntities);
            }

            treatmentBMP.MaintenanceRecords = null;
            foreach (var treatmentBMPAssessment in treatmentBMP.TreatmentBMPAssessments.ToList())
            {
                treatmentBMPAssessment.DeleteFull(databaseEntities);
            }
            treatmentBMP.TreatmentBMPAssessments = null;

            foreach (var treatmentBMPBenchmarkAndThreshold in treatmentBMP.TreatmentBMPBenchmarkAndThresholds.ToList())
            {
                treatmentBMPBenchmarkAndThreshold.DeleteFull(databaseEntities);
            }

            treatmentBMP.TreatmentBMPBenchmarkAndThresholds = null;

            // delete any custom attributes that are not valid for the new treatment bmp type
            var newTreatmentBMPType = databaseEntities.TreatmentBMPTypes.GetTreatmentBMPType(TreatmentBMPTypeID.Value);
            var validCustomAttributeTypes = newTreatmentBMPType.TreatmentBMPTypeCustomAttributeTypes.Select(x => x.CustomAttributeTypeID);
            var customAttributesToDelete = treatmentBMP.CustomAttributes.Where(x => !validCustomAttributeTypes.Contains(x.CustomAttributeTypeID))
                .ToList();

            foreach (var customAttribute in customAttributesToDelete.ToList())
            {
                customAttribute.DeleteFull(databaseEntities);
            }

            treatmentBMP.TreatmentBMPTypeID = TreatmentBMPTypeID.Value;
        }
    }
}
