﻿/*-----------------------------------------------------------------------
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

using System.ComponentModel.DataAnnotations;
using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Views.Shared.EditAttributes;

namespace Neptune.Web.Views.FieldVisit
{
    public class AttributesViewModel : EditAttributesViewModel, IValidatableObject
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

        public void UpdateModel(EFModels.Entities.FieldVisit fieldVisit, Person currentPerson, NeptuneDbContext dbContext)
        {
            var treatmentBMP = fieldVisit.TreatmentBMP;
            var customAttributeUpsertDtos = CustomAttributes.Where(x =>
                x.CustomAttributeValues != null && x.CustomAttributeValues.Count > 0);
            var customAttributesToUpdate = new List<CustomAttribute>();
            var customAttributeValuesToUpdate = new List<CustomAttributeValue>();
            foreach (var x in customAttributeUpsertDtos)
            {
                var customAttribute = new CustomAttribute()
                {
                    TreatmentBMPID = treatmentBMP.TreatmentBMPID,
                    TreatmentBMPTypeCustomAttributeTypeID = x.TreatmentBMPTypeCustomAttributeTypeID,
                    TreatmentBMPTypeID = treatmentBMP.TreatmentBMPTypeID,
                    CustomAttributeTypeID = x.CustomAttributeTypeID
                };
                customAttributesToUpdate.Add(customAttribute);
                foreach (var value in x.CustomAttributeValues)
                {
                    var customAttributeValue = new CustomAttributeValue(){CustomAttribute = customAttribute, AttributeValue = value};
                    customAttributeValuesToUpdate.Add(customAttributeValue);
                }
            }

            var customAttributesInDatabase = dbContext.CustomAttributes;
            var customAttributeValuesInDatabase = dbContext.CustomAttributeValues;

            var existingCustomAttributes = treatmentBMP.CustomAttributes.ToList();

            var existingCustomAttributeValues =
                existingCustomAttributes.SelectMany(x => x.CustomAttributeValues).ToList();

            existingCustomAttributes.Merge(customAttributesToUpdate, customAttributesInDatabase,
                (x, y) => x.TreatmentBMPID == y.TreatmentBMPID
                          && x.TreatmentBMPTypeID == y.TreatmentBMPTypeID
                          && x.CustomAttributeTypeID == y.CustomAttributeTypeID
                          && x.CustomAttributeID == y.CustomAttributeID);

            existingCustomAttributeValues.Merge(customAttributeValuesToUpdate, customAttributeValuesInDatabase,
                (x, y) => x.CustomAttributeValueID == y.CustomAttributeValueID
                          && x.CustomAttributeID == y.CustomAttributeID,
                (x, y) => { x.AttributeValue = y.AttributeValue; });
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var dbContext = validationContext.GetService<NeptuneDbContext>();
            return CustomAttributeTypeModelExtensions.CheckCustomAttributeTypeExpectations(CustomAttributes, dbContext);
        }
    }
}