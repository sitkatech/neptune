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

using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LtInfo.Common.Models;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMPAttributeType
{
    public class EditViewModel : FormViewModel, IValidatableObject
    {
        public int TreatmentBMPAttributeTypeID { get; set; }

        [Required]
        [StringLength(Models.TreatmentBMPAttributeType.FieldLengths.TreatmentBMPAttributeTypeName)]
        [DisplayName("Name of Attribute")]
        public string TreatmentBMPAttributeTypeName { get; set; }      

        [Required]
        [FieldDefinitionDisplay(FieldDefinitionEnum.TreatmentBMPAttributeDataType)]
        public int? TreatmentBMPAttributeDataTypeID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.MeasurementUnit)]
        public int? MeasurementUnitTypeID { get; set; }

        [DisplayName("Options")]
        public string TreatmentBMPAttributeTypeOptionsSchema { get; set; }

        [Required]
        [DisplayName("Required?")]
        public bool IsRequired { get; set; }

        [Required]
        [FieldDefinitionDisplay(Models.FieldDefinitionEnum.AttributeTypePurpose)]
        public int TreatmentBMPAttributeTypePurposeID { get; set; }

        [DisplayName("Description")]
        [StringLength(Models.TreatmentBMPAttributeType.FieldLengths.TreatmentBMPAttributeTypeDescription)]
        public string TreatmentBMPAttributeTypeDesription { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditViewModel()
        {
        }

        public EditViewModel(Models.TreatmentBMPAttributeType treatmentBMPAttributeType)
        {
            TreatmentBMPAttributeTypeID = treatmentBMPAttributeType.TreatmentBMPAttributeTypeID;
            TreatmentBMPAttributeTypeName = treatmentBMPAttributeType.TreatmentBMPAttributeTypeName;
            TreatmentBMPAttributeDataTypeID = treatmentBMPAttributeType.TreatmentBMPAttributeDataTypeID;
            MeasurementUnitTypeID = treatmentBMPAttributeType.MeasurementUnitTypeID;
            TreatmentBMPAttributeTypeOptionsSchema = treatmentBMPAttributeType.TreatmentBMPAttributeTypeOptionsSchema;
            IsRequired = treatmentBMPAttributeType.IsRequired;
            TreatmentBMPAttributeTypePurposeID = treatmentBMPAttributeType.TreatmentBMPAttributeTypePurposeID;
            TreatmentBMPAttributeTypeDesription = treatmentBMPAttributeType.TreatmentBMPAttributeTypeDescription;
        }


        public void UpdateModel(Models.TreatmentBMPAttributeType treatmentBMPAttributeType, Person currentPerson)
        {
            treatmentBMPAttributeType.TreatmentBMPAttributeTypeName = TreatmentBMPAttributeTypeName;
            treatmentBMPAttributeType.TreatmentBMPAttributeDataTypeID = TreatmentBMPAttributeDataTypeID.Value;
            treatmentBMPAttributeType.MeasurementUnitTypeID = MeasurementUnitTypeID;
            treatmentBMPAttributeType.IsRequired = IsRequired;
            treatmentBMPAttributeType.TreatmentBMPAttributeTypePurposeID = TreatmentBMPAttributeTypePurposeID;
            treatmentBMPAttributeType.TreatmentBMPAttributeTypeDescription = TreatmentBMPAttributeTypeDesription;
            if (TreatmentBMPAttributeDataTypeID.Value ==
                TreatmentBMPAttributeDataType.PickFromList.TreatmentBMPAttributeDataTypeID)
            {
                treatmentBMPAttributeType.TreatmentBMPAttributeTypeOptionsSchema = TreatmentBMPAttributeTypeOptionsSchema;
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            var treatmentBMPAttributeTypesWithSameName = HttpRequestStorage.DatabaseEntities.TreatmentBMPAttributeTypes.Where(x => x.TreatmentBMPAttributeTypeName == TreatmentBMPAttributeTypeName);
            if (treatmentBMPAttributeTypesWithSameName.Any(x => x.TreatmentBMPAttributeTypeID != TreatmentBMPAttributeTypeID))
            {
                validationResults.Add(new ValidationResult("A Treatment BMP Attribute Type with this name already exists"));
            }

            if (TreatmentBMPAttributeDataTypeID.Value == TreatmentBMPAttributeDataType.PickFromList.TreatmentBMPAttributeDataTypeID && string.IsNullOrEmpty(TreatmentBMPAttributeTypeOptionsSchema))
            {
                validationResults.Add(new ValidationResult("A Pick from List attribute must have options defined"));
            }

            return validationResults;
        }
    }
}
