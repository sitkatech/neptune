/*-----------------------------------------------------------------------
<copyright file="EditMaintenanceRecordViewModel.cs" company="Tahoe Regional Planning Agency">
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
using LtInfo.Common;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Views.Shared.EditAttributes;

namespace Neptune.Web.Views.FieldVisit
{
    public class EditMaintenanceRecordViewModel : EditAttributesViewModel
    {
        [StringLength(Models.MaintenanceRecord.FieldLengths.MaintenanceRecordDescription)]
        [Display(Name = "Description")]
        public string MaintenanceRecordDescription { get; set; }

        [Required]
        [FieldDefinitionDisplay(FieldDefinitionEnum.MaintenanceRecordType)]
        public int? MaintenanceRecordTypeID { get; set; }

        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public EditMaintenanceRecordViewModel()
        {
        }

        public EditMaintenanceRecordViewModel(Models.MaintenanceRecord maintenanceRecord) : base(maintenanceRecord.TreatmentBMP, CustomAttributeTypePurpose.Maintenance)
        {
            MaintenanceRecordTypeID = maintenanceRecord.MaintenanceRecordTypeID;
            MaintenanceRecordDescription = maintenanceRecord.MaintenanceRecordDescription;
            CustomAttributes = maintenanceRecord.MaintenanceRecordObservations.Select(x => new CustomAttributeSimple(x)).ToList();
        }

        public void UpdateModel(Models.FieldVisit fieldVisit)
        {
            var maintenanceRecord = fieldVisit.MaintenanceRecord;
            maintenanceRecord.MaintenanceRecordTypeID = MaintenanceRecordTypeID.Value;
            maintenanceRecord.MaintenanceRecordDescription = MaintenanceRecordDescription;

            var treatmentBMPTypeCustomAttributeTypes = maintenanceRecord.TreatmentBMP.TreatmentBMPType.TreatmentBMPTypeCustomAttributeTypes.ToList();
            var customAttributeSimplesWithValues = CustomAttributes.Where(x => x.CustomAttributeValues != null && x.CustomAttributeValues.Count > 0);
            var customAttributesToUpdate = new List<MaintenanceRecordObservation>();
            var customAttributeValuesToUpdate = new List<MaintenanceRecordObservationValue>();
            foreach (var x in customAttributeSimplesWithValues)
            {

                var customAttribute = new MaintenanceRecordObservation(maintenanceRecord.MaintenanceRecordID,
                    treatmentBMPTypeCustomAttributeTypes.Single(y => y.CustomAttributeTypeID == x.CustomAttributeTypeID)
                        .TreatmentBMPTypeCustomAttributeTypeID, maintenanceRecord.TreatmentBMP.TreatmentBMPTypeID,
                    x.CustomAttributeTypeID);
                customAttributesToUpdate.Add(customAttribute);

                foreach (var value in x.CustomAttributeValues)
                {
                    var customAttributeValue = new MaintenanceRecordObservationValue(customAttribute, value);
                    customAttributeValuesToUpdate.Add(customAttributeValue);
                }
            }

            var maintenanceRecordObservationsInDatabase = HttpRequestStorage.DatabaseEntities.AllMaintenanceRecordObservations.Local;
            var maintenanceRecordObservationValuesInDatabase = HttpRequestStorage.DatabaseEntities.AllMaintenanceRecordObservationValues.Local;

            var existingMaintenanceRecordObservations = maintenanceRecord.MaintenanceRecordObservations.Where(x =>
                x.CustomAttributeType.CustomAttributeTypePurposeID ==
                CustomAttributeTypePurpose.Maintenance.CustomAttributeTypePurposeID).ToList();

            var existingMaintenanceRecordObservationValues = existingMaintenanceRecordObservations.SelectMany(x => x.MaintenanceRecordObservationValues).ToList();

            existingMaintenanceRecordObservations.Merge(customAttributesToUpdate, maintenanceRecordObservationsInDatabase,
                (x, y) => x.MaintenanceRecordID == y.MaintenanceRecordID
                          && x.TreatmentBMPTypeID == y.TreatmentBMPTypeID
                          && x.CustomAttributeTypeID == y.CustomAttributeTypeID
                          && x.MaintenanceRecordObservationID == y.MaintenanceRecordObservationID,
                (x, y) => { });

            existingMaintenanceRecordObservationValues.Merge(customAttributeValuesToUpdate, maintenanceRecordObservationValuesInDatabase,
                (x, y) => x.MaintenanceRecordObservationValueID == y.MaintenanceRecordObservationValueID
                          && x.MaintenanceRecordObservationID == y.MaintenanceRecordObservationID,
                (x, y) => { x.ObservationValue = y.ObservationValue; });
        }
    }
}
