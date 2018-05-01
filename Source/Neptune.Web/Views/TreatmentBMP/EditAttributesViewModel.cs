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

using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LtInfo.Common;
using LtInfo.Common.Models;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMP
{
    public class EditAttributesViewModel : FormViewModel, IValidatableObject
    {
        [DisplayName("Metadata")]
        public List<TreatmentBMPAttributeSimple> TreatmentBMPAttributes { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditAttributesViewModel()
        {
        }

        public EditAttributesViewModel(Models.TreatmentBMP treatmentBMP,
            TreatmentBMPAttributeTypePurpose treatmentBmpAttributeTypePurpose)
        {
            TreatmentBMPAttributes = treatmentBMP.TreatmentBMPAttributes.Where(x => x.TreatmentBMPAttributeType.TreatmentBMPAttributeTypePurposeID == treatmentBmpAttributeTypePurpose.TreatmentBMPAttributeTypePurposeID).Select(x => new TreatmentBMPAttributeSimple(x)).ToList();
        }

        public void UpdateModel(Models.TreatmentBMP treatmentBMP, Person currentPerson,
            TreatmentBMPAttributeTypePurpose treatmentBmpAttributeTypePurpose)
        {
            var treatmentBMPAttributeSimplesWithValues = TreatmentBMPAttributes.Where(x => x.TreatmentBMPAttributeValues != null && x.TreatmentBMPAttributeValues.Count > 0);
            var treatmentBMPAttributesToUpdate = new List<TreatmentBMPAttribute>();
            var treatmentBMPAttributeValuesToUpdate = new List<TreatmentBMPAttributeValue>();
            foreach (var x in treatmentBMPAttributeSimplesWithValues)
            {
                var treatmentBMPAttribute = new TreatmentBMPAttribute(treatmentBMP.TreatmentBMPID, x.TreatmentBMPTypeAttributeTypeID, treatmentBMP.TreatmentBMPTypeID, x.TreatmentBMPAttributeTypeID);
                treatmentBMPAttributesToUpdate.Add(treatmentBMPAttribute);
                foreach (var value in x.TreatmentBMPAttributeValues)
                {                    
                    var treatmentBMPAttributeValue = new TreatmentBMPAttributeValue(treatmentBMPAttribute, value);                   
                    treatmentBMPAttributeValuesToUpdate.Add(treatmentBMPAttributeValue);
                }
            }

            var treatmentBMPAttributesInDatabase = HttpRequestStorage.DatabaseEntities.AllTreatmentBMPAttributes.Local;
            var treatmentBMPAttributeValuesInDatabase = HttpRequestStorage.DatabaseEntities.AllTreatmentBMPAttributeValues.Local;

            var existingTreatmentBMPAttributes = treatmentBMP.TreatmentBMPAttributes.Where(x =>
                x.TreatmentBMPAttributeType.TreatmentBMPAttributeTypePurposeID ==
                treatmentBmpAttributeTypePurpose.TreatmentBMPAttributeTypePurposeID).ToList();

            var existingTreatmentBMPAttributeValues = existingTreatmentBMPAttributes.SelectMany(x => x.TreatmentBMPAttributeValues).ToList();

            existingTreatmentBMPAttributes.Merge(treatmentBMPAttributesToUpdate, treatmentBMPAttributesInDatabase,
                (x, y) => x.TreatmentBMPID == y.TreatmentBMPID 
                          && x.TreatmentBMPTypeID == y.TreatmentBMPTypeID 
                          && x.TreatmentBMPAttributeTypeID == y.TreatmentBMPAttributeTypeID
                          && x.TreatmentBMPAttributeID == y.TreatmentBMPAttributeID,
                (x, y) => { });

            existingTreatmentBMPAttributeValues.Merge(treatmentBMPAttributeValuesToUpdate, treatmentBMPAttributeValuesInDatabase,
                (x, y) => x.TreatmentBMPAttributeValueID == y.TreatmentBMPAttributeValueID
                          && x.TreatmentBMPAttributeID == y.TreatmentBMPAttributeID,
                (x, y) => { x.AttributeValue = y.AttributeValue; });
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            var treatmentBMPAttributeTypeIDs = TreatmentBMPAttributes.Select(x => x.TreatmentBMPAttributeTypeID).ToList();
            var treatmentBMPAttributeTypes = HttpRequestStorage.DatabaseEntities.TreatmentBMPAttributeTypes.Where(x => treatmentBMPAttributeTypeIDs.Contains(x.TreatmentBMPAttributeTypeID)).ToList();


            var requiredAttributeDoesNotHaveValue = treatmentBMPAttributeTypes.Any(x =>
            {

                var treatmentBMPAttributeSimple = TreatmentBMPAttributes.SingleOrDefault(y =>
                    y.TreatmentBMPAttributeTypeID == x.TreatmentBMPAttributeTypeID 
                    && x.IsRequired 
                    && (y.TreatmentBMPAttributeValues == null || y.TreatmentBMPAttributeValues.All(string.IsNullOrEmpty)));

                return treatmentBMPAttributeSimple != null;
            });

            if (requiredAttributeDoesNotHaveValue)
            {
                errors.Add(new SitkaValidationResult<EditAttributesViewModel, List<TreatmentBMPAttributeSimple>>("Must enter all required fields.", m => m.TreatmentBMPAttributes));
                return errors;
            }

            foreach (var treatmentBMPAttributeSimple in TreatmentBMPAttributes.Where(x => x.TreatmentBMPAttributeValues != null && x.TreatmentBMPAttributeValues.Count > 0))
            {
                var treatmentBMPAttributeType = treatmentBMPAttributeTypes.Single(x =>
                    x.TreatmentBMPAttributeTypeID == treatmentBMPAttributeSimple.TreatmentBMPAttributeTypeID);

                var treatmentBMPAttributeDataType = treatmentBMPAttributeType.TreatmentBMPAttributeDataType;

                foreach (var value in treatmentBMPAttributeSimple.TreatmentBMPAttributeValues)
                {
                    if (!treatmentBMPAttributeDataType.ValueIsCorrectDataType(value))
                    {
                        errors.Add(new SitkaValidationResult<EditAttributesViewModel, List<TreatmentBMPAttributeSimple>>(
                            $"Entered value for {treatmentBMPAttributeType.TreatmentBMPAttributeTypeName} does not match expected type ({treatmentBMPAttributeDataType.TreatmentBMPAttributeDataTypeDisplayName}).", m => m.TreatmentBMPAttributes));
                    }
                }
                
            }

            return errors;
        }
    }
}
