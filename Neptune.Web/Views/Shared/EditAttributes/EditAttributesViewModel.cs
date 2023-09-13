/*-----------------------------------------------------------------------
<copyright file="EditViewModel.cs" company="Tahoe Regional Planning Agency">
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

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Views.FieldVisit;

namespace Neptune.Web.Views.Shared.EditAttributes
{
    public class EditAttributesViewModel : FieldVisitViewModel, IValidatableObject
    {
        [DisplayName("Metadata")]
        public List<CustomAttributeUpsertDto> CustomAttributes { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditAttributesViewModel()
        {
        }

        public EditAttributesViewModel(List<CustomAttributeUpsertDto> customAttributeUpsertDtos)
        {
            CustomAttributes = customAttributeUpsertDtos;
        }

        public async Task UpdateModel(NeptuneDbContext dbContext, EFModels.Entities.TreatmentBMP treatmentBMP, List<CustomAttribute> existingCustomAttributes, List<CustomAttributeType> allCustomAttributeTypes)
        {
            dbContext.RemoveRange(existingCustomAttributes.SelectMany(x => x.CustomAttributeValues));
            await dbContext.SaveChangesAsync();

            var customAttributeUpsertDtos = CustomAttributes.Where(x => x.CustomAttributeValues != null && x.CustomAttributeValues.Count > 0).ToList();
            var customAttributeValuesToUpdate = new List<CustomAttributeValue>();
            foreach (var customAttributeUpsertDto in customAttributeUpsertDtos)
            {
                var customAttribute = existingCustomAttributes.SingleOrDefault(x =>
                    x.TreatmentBMPID == treatmentBMP.TreatmentBMPID
                    && x.TreatmentBMPTypeID == treatmentBMP.TreatmentBMPTypeID
                    && x.CustomAttributeTypeID == customAttributeUpsertDto.CustomAttributeTypeID);
                if (customAttribute == null)
                {
                    customAttribute = new CustomAttribute()
                    {
                        TreatmentBMPID = treatmentBMP.TreatmentBMPID,
                        TreatmentBMPTypeCustomAttributeTypeID = customAttributeUpsertDto.TreatmentBMPTypeCustomAttributeTypeID,
                        TreatmentBMPTypeID = treatmentBMP.TreatmentBMPTypeID,
                        CustomAttributeTypeID = customAttributeUpsertDto.CustomAttributeTypeID
                    };
                    dbContext.CustomAttributes.Add(customAttribute);
                }

                foreach (var value in customAttributeUpsertDto.CustomAttributeValues)
                {
                    var valueParsedForDataType = allCustomAttributeTypes.Single(y => y.CustomAttributeTypeID == customAttributeUpsertDto.CustomAttributeTypeID).CustomAttributeDataType.ValueParsedForDataType(value);
                    var customAttributeValue = new CustomAttributeValue
                    {
                        CustomAttribute = customAttribute,
                        AttributeValue = valueParsedForDataType
                    };
                    customAttributeValuesToUpdate.Add(customAttributeValue);
                }
            }

            foreach (var customAttribute in existingCustomAttributes)
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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var dbContext = validationContext.GetService<NeptuneDbContext>();
            return CustomAttributeTypeModelExtensions.CheckCustomAttributeTypeExpectations(CustomAttributes, dbContext);
        }
    }
}
