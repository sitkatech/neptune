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

using System.ComponentModel.DataAnnotations;
using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Views.Shared.EditAttributes;

namespace Neptune.Web.Views.FieldVisit
{
    public class EditMaintenanceRecordViewModel : EditAttributesViewModel
    {
        [StringLength(EFModels.Entities.MaintenanceRecord.FieldLengths.MaintenanceRecordDescription)]
        [Display(Name = "Description")]
        public string MaintenanceRecordDescription { get; set; }

        [Required]
        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.MaintenanceRecordType)]
        public int? MaintenanceRecordTypeID { get; set; }

        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public EditMaintenanceRecordViewModel()
        {
        }

        public EditMaintenanceRecordViewModel(EFModels.Entities.MaintenanceRecord maintenanceRecord)
        {
            var treatmentBMP = maintenanceRecord.TreatmentBMP;
            CustomAttributes = treatmentBMP.CustomAttributes.Where(x => x.CustomAttributeType.CustomAttributeTypePurposeID == CustomAttributeTypePurpose.Maintenance.CustomAttributeTypePurposeID).Select(x => x.AsUpsertDto()).ToList();

            MaintenanceRecordTypeID = maintenanceRecord.MaintenanceRecordTypeID;
            MaintenanceRecordDescription = maintenanceRecord.MaintenanceRecordDescription;
            CustomAttributes = maintenanceRecord.MaintenanceRecordObservations.Select(x => x.AsUpsertDto()).ToList();
        }

        public void UpdateModel(EFModels.Entities.FieldVisit fieldVisit, List<CustomAttributeType> allCustomAttributeTypes, NeptuneDbContext dbContext)
        {
            var maintenanceRecord = fieldVisit.MaintenanceRecord;
            maintenanceRecord.MaintenanceRecordTypeID = MaintenanceRecordTypeID.Value;
            maintenanceRecord.MaintenanceRecordDescription = MaintenanceRecordDescription;

            var treatmentBMPTypeCustomAttributeTypes = maintenanceRecord.TreatmentBMP.TreatmentBMPType.TreatmentBMPTypeCustomAttributeTypes.ToList();
            var customAttributeSimplesWithValues = CustomAttributes.Where(x => x.CustomAttributeValues != null && x.CustomAttributeValues.Count > 0);
            var maintenanceRecordObservationsToUpdate = new List<MaintenanceRecordObservation>();
            var maintenanceRecordObservationValuesToUpdate = new List<MaintenanceRecordObservationValue>();
            foreach (var x in customAttributeSimplesWithValues)
            {

                var maintenanceRecordObservation = new MaintenanceRecordObservation()
                {
                    MaintenanceRecordID = maintenanceRecord.MaintenanceRecordID,
                    TreatmentBMPTypeCustomAttributeTypeID = treatmentBMPTypeCustomAttributeTypes.Single(y => y.CustomAttributeTypeID == x.CustomAttributeTypeID)
                        .TreatmentBMPTypeCustomAttributeTypeID,
                    TreatmentBMPTypeID = maintenanceRecord.TreatmentBMP.TreatmentBMPTypeID,
                    CustomAttributeTypeID = x.CustomAttributeTypeID
                };
                maintenanceRecordObservationsToUpdate.Add(maintenanceRecordObservation);

                foreach (var value in x.CustomAttributeValues)
                {
                    var valueParsedForDataType = allCustomAttributeTypes.Single(y => y.CustomAttributeTypeID == x.CustomAttributeTypeID).CustomAttributeDataType.ValueParsedForDataType(value);
                    var maintenanceRecordObservationValue =
                        new MaintenanceRecordObservationValue
                        {
                            MaintenanceRecordObservation = maintenanceRecordObservation,
                            ObservationValue = valueParsedForDataType
                        };

                    maintenanceRecordObservationValuesToUpdate.Add(maintenanceRecordObservationValue);
                }
            }

            var maintenanceRecordObservationsInDatabase = dbContext.MaintenanceRecordObservations;
            var maintenanceRecordObservationValuesInDatabase = dbContext.MaintenanceRecordObservationValues;

            var existingMaintenanceRecordObservations = maintenanceRecord.MaintenanceRecordObservations.Where(x =>
                x.CustomAttributeType.CustomAttributeTypePurposeID ==
                CustomAttributeTypePurpose.Maintenance.CustomAttributeTypePurposeID).ToList();

            var existingMaintenanceRecordObservationValues = existingMaintenanceRecordObservations.SelectMany(x => x.MaintenanceRecordObservationValues).ToList();

            existingMaintenanceRecordObservations.Merge(maintenanceRecordObservationsToUpdate, maintenanceRecordObservationsInDatabase,
                (x, y) => x.MaintenanceRecordID == y.MaintenanceRecordID
                          && x.TreatmentBMPTypeID == y.TreatmentBMPTypeID
                          && x.CustomAttributeTypeID == y.CustomAttributeTypeID
                          && x.MaintenanceRecordObservationID == y.MaintenanceRecordObservationID,
                (x, y) => { });

            existingMaintenanceRecordObservationValues.Merge(maintenanceRecordObservationValuesToUpdate, maintenanceRecordObservationValuesInDatabase,
                (x, y) => x.MaintenanceRecordObservationValueID == y.MaintenanceRecordObservationValueID
                          && x.MaintenanceRecordObservationID == y.MaintenanceRecordObservationID,
                (x, y) => { x.ObservationValue = y.ObservationValue; });
        }
    }
}
