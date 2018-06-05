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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LtInfo.Common;
using LtInfo.Common.Models;
using Microsoft.Ajax.Utilities;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.Views.Shared.EditAttributes
{
    public class EditAttributesViewModel : FormViewModel, IValidatableObject
    {
        [DisplayName("Metadata")]
        public List<CustomAttributeSimple> CustomAttributes { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditAttributesViewModel()
        {
        }

        public EditAttributesViewModel(Models.TreatmentBMP treatmentBMP,
            CustomAttributeTypePurpose customAttributeTypePurpose)
        {
            CustomAttributes = treatmentBMP.CustomAttributes.Where(x => x.CustomAttributeType.CustomAttributeTypePurposeID == customAttributeTypePurpose.CustomAttributeTypePurposeID).Select(x => new CustomAttributeSimple(x)).ToList();
        }

        public void UpdateModel(Models.TreatmentBMP treatmentBMP, Person currentPerson,
            CustomAttributeTypePurpose customAttributeTypePurpose)
        {
            var customAttributeSimplesWithValues = CustomAttributes.Where(x => x.CustomAttributeValues != null && x.CustomAttributeValues.Count > 0);
            var customAttributesToUpdate = new List<CustomAttribute>();
            var customAttributeValuesToUpdate = new List<CustomAttributeValue>();
            foreach (var x in customAttributeSimplesWithValues)
            {
                var customAttribute = new CustomAttribute(treatmentBMP.TreatmentBMPID, x.TreatmentBMPTypeCustomAttributeTypeID, treatmentBMP.TreatmentBMPTypeID, x.CustomAttributeTypeID);
                customAttributesToUpdate.Add(customAttribute);
                foreach (var value in x.CustomAttributeValues)
                {                    
                    var customAttributeValue = new CustomAttributeValue(customAttribute, value);                   
                    customAttributeValuesToUpdate.Add(customAttributeValue);
                }
            }

            var customAttributesInDatabase = HttpRequestStorage.DatabaseEntities.AllCustomAttributes.Local;
            var customAttributeValuesInDatabase = HttpRequestStorage.DatabaseEntities.AllCustomAttributeValues.Local;

            var existingCustomAttributes = treatmentBMP.CustomAttributes.Where(x =>
                x.CustomAttributeType.CustomAttributeTypePurposeID ==
                customAttributeTypePurpose.CustomAttributeTypePurposeID).ToList();

            var existingCustomAttributeValues = existingCustomAttributes.SelectMany(x => x.CustomAttributeValues).ToList();

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
            var errors = new List<ValidationResult>();

            var customAttributeTypes = GetCustomAttributeTypes();


            var requiredAttributeDoesNotHaveValue = customAttributeTypes.Any(x =>
            {

                var customAttributeSimple = CustomAttributes.SingleOrDefault(y =>
                    y.CustomAttributeTypeID == x.CustomAttributeTypeID 
                    && x.IsRequired 
                    && (y.CustomAttributeValues == null || y.CustomAttributeValues.All(string.IsNullOrEmpty)));

                return customAttributeSimple != null;
            });

            if (requiredAttributeDoesNotHaveValue)
            {
                errors.Add(new SitkaValidationResult<EditAttributesViewModel, List<CustomAttributeSimple>>("Must enter all required fields.", m => m.CustomAttributes));
                return errors;
            }

            CheckTypeExpectations(customAttributeTypes, errors);

            return errors;
        }

        protected List<Models.CustomAttributeType> GetCustomAttributeTypes()
        {
            var customAttributeTypeIDs = CustomAttributes.Select(x => x.CustomAttributeTypeID).ToList();
            var customAttributeTypes = HttpRequestStorage.DatabaseEntities.CustomAttributeTypes
                .Where(x => customAttributeTypeIDs.Contains(x.CustomAttributeTypeID)).ToList();
            return customAttributeTypes;
        }

        protected void CheckTypeExpectations(List<Models.CustomAttributeType> customAttributeTypes, List<ValidationResult> errors)
        {
            foreach (var customAttributeSimple in CustomAttributes.Where(x =>
                x.CustomAttributeValues != null && x.CustomAttributeValues.Count > 0))
            {
                var customAttributeType = customAttributeTypes.Single(x =>
                    x.CustomAttributeTypeID == customAttributeSimple.CustomAttributeTypeID);

                var customAttributeDataType = customAttributeType.CustomAttributeDataType;

                foreach (var value in customAttributeSimple.CustomAttributeValues)
                {
                    if (!string.IsNullOrWhiteSpace(value) && !customAttributeDataType.ValueIsCorrectDataType(value))
                    {
                        errors.Add(new SitkaValidationResult<EditAttributesViewModel, List<CustomAttributeSimple>>(
                            $"Entered value for {customAttributeType.CustomAttributeTypeName} does not match expected type ({customAttributeDataType.CustomAttributeDataTypeDisplayName}).",
                            m => m.CustomAttributes));
                    }
                }
            }
        }
    }
}
