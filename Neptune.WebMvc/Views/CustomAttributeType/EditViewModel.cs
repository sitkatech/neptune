﻿/*-----------------------------------------------------------------------
<copyright file="EditViewModel.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Neptune.Common;
using Neptune.Common.GeoSpatial;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.Models;
using Newtonsoft.Json;

namespace Neptune.WebMvc.Views.CustomAttributeType
{
    public class EditViewModel : FormViewModel, IValidatableObject
    {
        public int CustomAttributeTypeID { get; set; }

        [Required]
        [StringLength(EFModels.Entities.CustomAttributeType.FieldLengths.CustomAttributeTypeName)]
        [DisplayName("Name of Attribute")]
        public string CustomAttributeTypeName { get; set; }      

        [Required]
        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.CustomAttributeDataType)]
        public int? CustomAttributeDataTypeID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.MeasurementUnit)]
        public int? MeasurementUnitTypeID { get; set; }

        [DisplayName("Options")]
        public string CustomAttributeTypeOptionsSchema { get; set; }

        // backing fields for drop-down lists have to be nullable in order to get the "default option" behavior that we want
        [Required]
        [DisplayName("Required?")]
        public bool? IsRequired { get; set; }

        [Required]
        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.AttributeTypePurpose)]
        public int? CustomAttributeTypePurposeID { get; set; }

        [DisplayName("Description")]
        [StringLength(EFModels.Entities.CustomAttributeType.FieldLengths.CustomAttributeTypeDescription)]
        public string CustomAttributeTypeDesription { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditViewModel()
        {
        }

        public EditViewModel(EFModels.Entities.CustomAttributeType customAttributeType)
        {
            CustomAttributeTypeID = customAttributeType.CustomAttributeTypeID;
            CustomAttributeTypeName = customAttributeType.CustomAttributeTypeName;
            CustomAttributeDataTypeID = customAttributeType.CustomAttributeDataTypeID;
            MeasurementUnitTypeID = customAttributeType.MeasurementUnitTypeID;
            CustomAttributeTypeOptionsSchema = customAttributeType.CustomAttributeTypeOptionsSchema;
            IsRequired = customAttributeType.IsRequired;
            CustomAttributeTypePurposeID = customAttributeType.CustomAttributeTypePurposeID;
            CustomAttributeTypeDesription = customAttributeType.CustomAttributeTypeDescription;
        }


        public void UpdateModel(EFModels.Entities.CustomAttributeType customAttributeType, Person currentPerson)
        {
            if (customAttributeType.CustomAttributeTypePurposeID == (int)CustomAttributeTypePurposeEnum.Modeling)
            {
                customAttributeType.CustomAttributeTypeDescription = CustomAttributeTypeDesription;
                return;
            }
            customAttributeType.CustomAttributeTypeName = CustomAttributeTypeName;
            customAttributeType.CustomAttributeDataTypeID = CustomAttributeDataTypeID.Value;
            customAttributeType.MeasurementUnitTypeID = MeasurementUnitTypeID;
            customAttributeType.IsRequired = IsRequired.GetValueOrDefault();
            customAttributeType.CustomAttributeTypePurposeID = CustomAttributeTypePurposeID != (int)CustomAttributeTypePurposeEnum.Modeling ? CustomAttributeTypePurposeID.GetValueOrDefault() : customAttributeType.CustomAttributeTypePurposeID;
            customAttributeType.CustomAttributeTypeDescription = CustomAttributeTypeDesription;

            var customAttributeDataType = CustomAttributeDataType.AllLookupDictionary[CustomAttributeDataTypeID.Value];
            if (customAttributeDataType.HasOptions)
            {
                customAttributeType.CustomAttributeTypeOptionsSchema = CustomAttributeTypeOptionsSchema;
            }
            else
            {
                customAttributeType.CustomAttributeTypeOptionsSchema = null;
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var dbContext = validationContext.GetService<NeptuneDbContext>();

            var validationResults = new List<ValidationResult>();

            var customAttributeTypesWithSameName = dbContext.CustomAttributeTypes.Where(x => x.CustomAttributeTypeName == CustomAttributeTypeName);
            if (customAttributeTypesWithSameName.Any(x => x.CustomAttributeTypeID != CustomAttributeTypeID))
            {
                validationResults.Add(new ValidationResult("A Custom Attribute Type with this name already exists"));
            }


            if (ModelObjectHelpers.IsRealPrimaryKeyValue(CustomAttributeTypeID))
            {
                var customAttributeType = CustomAttributeTypes.GetByID(dbContext, CustomAttributeTypeID);
                var isStringType = customAttributeType.CustomAttributeDataType == CustomAttributeDataType.String;
                if (!isStringType && customAttributeType.CustomAttributeDataTypeID != CustomAttributeDataTypeID)
                {
                    validationResults.Add(new ValidationResult("You cannot change the type of attribute"));
                }

                var updatedTypeIsStringPickFromListOrMultiselect = (CustomAttributeDataTypeID == CustomAttributeDataType.String.CustomAttributeDataTypeID ||
                         CustomAttributeDataTypeID == CustomAttributeDataType.PickFromList.CustomAttributeDataTypeID ||
                         CustomAttributeDataTypeID == CustomAttributeDataType.MultiSelect.CustomAttributeDataTypeID);
                if (isStringType && !updatedTypeIsStringPickFromListOrMultiselect)
                {
                    validationResults.Add(new ValidationResult("You cannot change a String attribute to any other than a single or multi select list"));
                }
            }

            var customAttributeDataType = CustomAttributeDataType.AllLookupDictionary[CustomAttributeDataTypeID.Value];
            if (customAttributeDataType.HasOptions)
            {
                try
                {
                    var test = GeoJsonSerializer.Deserialize<List<string>>(CustomAttributeTypeOptionsSchema);
                }
                catch
                {
                    validationResults.Add(new ValidationResult("Options cannot be saved"));
                    return validationResults;
                }

                var options = JsonConvert.DeserializeObject<List<string>>(CustomAttributeTypeOptionsSchema);
                if (options.Any(string.IsNullOrEmpty))
                {
                    validationResults.Add(new ValidationResult("Options cannot be empty"));
                }

                if (options.Count.Equals(0))
                {
                    validationResults.Add(new ValidationResult("This type of attribute must have options defined"));
                }

                if (customAttributeDataType == CustomAttributeDataType.MultiSelect && options.Count == 1)
                {
                    validationResults.Add(new ValidationResult("This type of attribute must have more than one option defined"));
                }

                if (options.Select(x => x.ToLower()).HasDuplicates())
                {
                    validationResults.Add(new ValidationResult("Options must be unique, remove duplicates"));
                }
            }


            return validationResults;
        }
    }
}
