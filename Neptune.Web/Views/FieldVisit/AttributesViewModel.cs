/*-----------------------------------------------------------------------
<copyright file="AttributesViewModel.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
Copyright (c) Tahoe Regional Planning Agency and Sitka Technology Group. All rights reserved.
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

using Neptune.EFModels.Entities;
using Neptune.Web.Views.Shared.EditAttributes;

namespace Neptune.Web.Views.FieldVisit
{
    public class AttributesViewModel : EditAttributesViewModel
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public AttributesViewModel()
        {
        }

        public AttributesViewModel(EFModels.Entities.FieldVisit fieldVisit)
        {
            var treatmentBMP = fieldVisit.TreatmentBMP;
            CustomAttributes = treatmentBMP.CustomAttributes.Where(x => x.CustomAttributeType.CustomAttributeTypePurposeID != CustomAttributeTypePurpose.Maintenance.CustomAttributeTypePurposeID).Select(x => x.AsUpsertDto()).ToList();
        }

        public async Task UpdateModel(EFModels.Entities.FieldVisit fieldVisit, Person currentPerson, NeptuneDbContext dbContext)
        {
            var treatmentBMP = fieldVisit.TreatmentBMP;
            var existingCustomAttributes = treatmentBMP.CustomAttributes.ToList();
            dbContext.RemoveRange(existingCustomAttributes.SelectMany(x => x.CustomAttributeValues));
            await dbContext.SaveChangesAsync();

            var customAttributeUpsertDtos = CustomAttributes.Where(x =>
                x.CustomAttributeValues != null && x.CustomAttributeValues.Count > 0).ToList();
            var customAttributeValuesToUpdate = new List<CustomAttributeValue>();
            foreach (var customAttributeUpsertDto in customAttributeUpsertDtos)
            {
                var customAttribute = treatmentBMP.CustomAttributes.SingleOrDefault(x =>
                    x.TreatmentBMPID == treatmentBMP.TreatmentBMPID
                    && x.TreatmentBMPTypeID == treatmentBMP.TreatmentBMPTypeID
                    && x.CustomAttributeTypeID == customAttributeUpsertDto.CustomAttributeTypeID);
                if (customAttribute == null)
                {
                    customAttribute = new CustomAttribute()
                    {
                        TreatmentBMPID = treatmentBMP.TreatmentBMPID,
                        TreatmentBMPTypeCustomAttributeTypeID =
                            customAttributeUpsertDto.TreatmentBMPTypeCustomAttributeTypeID,
                        TreatmentBMPTypeID = treatmentBMP.TreatmentBMPTypeID,
                        CustomAttributeTypeID = customAttributeUpsertDto.CustomAttributeTypeID
                    };
                    dbContext.CustomAttributes.Add(customAttribute);
                }

                customAttributeValuesToUpdate.AddRange(customAttributeUpsertDto.CustomAttributeValues.Where(y => !string.IsNullOrWhiteSpace(y)).Select(value => new CustomAttributeValue { CustomAttribute = customAttribute, AttributeValue = value }));
            }

            foreach (var customAttribute in treatmentBMP.CustomAttributes)
            {
                var customAttributeUpsertDto = customAttributeUpsertDtos.SingleOrDefault(y =>
                    customAttribute.TreatmentBMPTypeCustomAttributeTypeID == y.TreatmentBMPTypeCustomAttributeTypeID);
                if (customAttributeUpsertDto == null)
                {
                    dbContext.Remove(customAttribute);
                }
            }
            dbContext.AddRange(customAttributeValuesToUpdate);
        }
    }
}