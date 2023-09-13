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
using Neptune.Models.DataTransferObjects;
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

        public EditMaintenanceRecordViewModel(EFModels.Entities.MaintenanceRecord maintenanceRecord, List<CustomAttributeUpsertDto> customAttributeUpsertDtos)
        {
            MaintenanceRecordTypeID = maintenanceRecord.MaintenanceRecordTypeID;
            MaintenanceRecordDescription = maintenanceRecord.MaintenanceRecordDescription;
            CustomAttributes = customAttributeUpsertDtos;
        }

        public async Task UpdateModel(NeptuneDbContext dbContext, EFModels.Entities.MaintenanceRecord? maintenanceRecord, List<MaintenanceRecordObservation> existingMaintenanceRecordObservations, List<CustomAttributeType> allCustomAttributeTypes)
        {
            maintenanceRecord.MaintenanceRecordTypeID = MaintenanceRecordTypeID.Value;
            maintenanceRecord.MaintenanceRecordDescription = MaintenanceRecordDescription;

            dbContext.RemoveRange(existingMaintenanceRecordObservations.SelectMany(x => x.MaintenanceRecordObservationValues));
            await dbContext.SaveChangesAsync();

            var customAttributeUpsertDtos = CustomAttributes.Where(x => x.CustomAttributeValues != null && x.CustomAttributeValues.Count > 0).ToList();
            var maintenanceRecordObservationValuesToUpdate = new List<MaintenanceRecordObservationValue>();
            foreach (var customAttributeUpsertDto in customAttributeUpsertDtos)
            {
                var maintenanceRecordObservation = existingMaintenanceRecordObservations.SingleOrDefault(x =>
                    x.MaintenanceRecordID == maintenanceRecord.MaintenanceRecordID
                    && x.TreatmentBMPTypeID == maintenanceRecord.TreatmentBMPTypeID
                    && x.CustomAttributeTypeID == customAttributeUpsertDto.CustomAttributeTypeID);
                if (maintenanceRecordObservation == null)
                {
                    maintenanceRecordObservation = new MaintenanceRecordObservation
                    {
                        MaintenanceRecordID = maintenanceRecord.MaintenanceRecordID,
                        TreatmentBMPTypeCustomAttributeTypeID = customAttributeUpsertDto.TreatmentBMPTypeCustomAttributeTypeID,
                        TreatmentBMPTypeID = maintenanceRecord.TreatmentBMP.TreatmentBMPTypeID,
                        CustomAttributeTypeID = customAttributeUpsertDto.CustomAttributeTypeID
                    };
                    dbContext.MaintenanceRecordObservations.Add(maintenanceRecordObservation);
                }

                foreach (var value in customAttributeUpsertDto.CustomAttributeValues)
                {
                    var valueParsedForDataType = allCustomAttributeTypes.Single(y => y.CustomAttributeTypeID == customAttributeUpsertDto.CustomAttributeTypeID).CustomAttributeDataType.ValueParsedForDataType(value);
                    var maintenanceRecordObservationValue = new MaintenanceRecordObservationValue
                    {
                        MaintenanceRecordObservation = maintenanceRecordObservation,
                        ObservationValue = valueParsedForDataType
                    };
                    maintenanceRecordObservationValuesToUpdate.Add(maintenanceRecordObservationValue);
                }
            }

            foreach (var customAttribute in existingMaintenanceRecordObservations)
            {
                var customAttributeUpsertDto = customAttributeUpsertDtos.SingleOrDefault(y =>
                    customAttribute.TreatmentBMPTypeCustomAttributeTypeID == y.TreatmentBMPTypeCustomAttributeTypeID);
                if (customAttributeUpsertDto == null)
                {
                    dbContext.Remove(customAttribute);
                }
            }
            dbContext.AddRange(maintenanceRecordObservationValuesToUpdate);
        }
    }
}
