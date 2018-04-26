﻿/*-----------------------------------------------------------------------
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

        public EditAttributesViewModel(Models.TreatmentBMP treatmentBMP)
        {
            TreatmentBMPAttributes = treatmentBMP.TreatmentBMPAttributes.Where(x => x.TreatmentBMPAttributeType.TreatmentBMPAttributeDataType != TreatmentBMPAttributeDataType.MultiSelect)
                .Select(x => new TreatmentBMPAttributeSimple
                {
                    TreatmentBMPTypeAttributeTypeID = x.TreatmentBMPTypeAttributeTypeID,
                    TreatmentBMPAttributeTypeID = x.TreatmentBMPAttributeTypeID,
                    TreatmentBMPAttributeValues = new List<string>{x.TreatmentBMPAttributeValue}
                })
                .ToList();
            var multiselectValuesCollapsedToSimple = treatmentBMP.TreatmentBMPAttributes.Where(x => x.TreatmentBMPAttributeType.TreatmentBMPAttributeDataType == TreatmentBMPAttributeDataType.MultiSelect).GroupBy(x => x.TreatmentBMPAttributeType)
                .Select(x =>
                {
                    var treatmentBMPTypeAttributeTypeID = x.Key.TreatmentBMPTypeAttributeTypes.ToList().First().TreatmentBMPTypeAttributeTypeID;
                    var treatmentBMPAttributeTypeID = x.Key.TreatmentBMPAttributeTypeID;
                    var selectedOptions = x.ToList().Select(y => y.TreatmentBMPAttributeValue).ToList();
                    
                    var treatmentBMPAttributeSimpleMultiselect = new TreatmentBMPAttributeSimple(treatmentBMPTypeAttributeTypeID, treatmentBMPAttributeTypeID, selectedOptions);
                    return treatmentBMPAttributeSimpleMultiselect;
                })
                .ToList();

            TreatmentBMPAttributes.AddRange(multiselectValuesCollapsedToSimple);
        }

        public void UpdateModel(Models.TreatmentBMP treatmentBMP, Person currentPerson)
        {
            var treatmentBMPAttributeSimplesWithValues = TreatmentBMPAttributes.Where(x => x.TreatmentBMPAttributeValues.Count > 0);
            var treatmentBMPAttributesToUpdate = new List<TreatmentBMPAttribute>();
            foreach (var x in treatmentBMPAttributeSimplesWithValues)
            {
                foreach (var value in x.TreatmentBMPAttributeValues)
                {
                    treatmentBMPAttributesToUpdate.Add(new TreatmentBMPAttribute(treatmentBMP.TreatmentBMPID, x.TreatmentBMPTypeAttributeTypeID, treatmentBMP.TreatmentBMPTypeID, x.TreatmentBMPAttributeTypeID, value));
                }
            }

            var treatmentBMPAttributesInDatabase = HttpRequestStorage.DatabaseEntities.AllTreatmentBMPAttributes.Local;
            treatmentBMP.TreatmentBMPAttributes.Merge(treatmentBMPAttributesToUpdate, treatmentBMPAttributesInDatabase,
                (x, y) => x.TreatmentBMPID == y.TreatmentBMPID 
                          && x.TreatmentBMPTypeID == y.TreatmentBMPTypeID 
                          && x.TreatmentBMPAttributeTypeID == y.TreatmentBMPAttributeTypeID
                          && x.TreatmentBMPAttributeID == y.TreatmentBMPAttributeID,
                (x, y) => { x.TreatmentBMPAttributeValue = y.TreatmentBMPAttributeValue; });
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            var treatmentBMPTypeAttributeTypeIDs = TreatmentBMPAttributes.Select(x => x.TreatmentBMPTypeAttributeTypeID).ToList();
            var treatmentBMPTypeAttributeTypes = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypeAttributeTypes.Where(x => treatmentBMPTypeAttributeTypeIDs.Contains(x.TreatmentBMPTypeAttributeTypeID)).ToList();
            if (treatmentBMPTypeAttributeTypes.Any(x =>
                TreatmentBMPAttributes.SingleOrDefault(y => y.TreatmentBMPTypeAttributeTypeID == x.TreatmentBMPTypeAttributeTypeID && x.TreatmentBMPAttributeType.IsRequired && y.TreatmentBMPAttributeValues.Count == 0) != null))
            {
                errors.Add(new SitkaValidationResult<EditAttributesViewModel, List<TreatmentBMPAttributeSimple>>("Must enter all required fields.", m => m.TreatmentBMPAttributes));
            }

            foreach (var treatmentBMPAttributeSimple in TreatmentBMPAttributes.Where(x => x.TreatmentBMPAttributeValues.Count > 0))
            {
                var treatmentBMPTypeAttributeType = treatmentBMPTypeAttributeTypes.Single(x =>
                    x.TreatmentBMPTypeAttributeTypeID == treatmentBMPAttributeSimple.TreatmentBMPTypeAttributeTypeID);

                var treatmentBMPAttributeDataType = treatmentBMPTypeAttributeType.TreatmentBMPAttributeType.TreatmentBMPAttributeDataType;

                foreach (var value in treatmentBMPAttributeSimple.TreatmentBMPAttributeValues)
                {
                    if (!treatmentBMPAttributeDataType.ValueIsCorrectDataType(value))
                    {
                        errors.Add(new SitkaValidationResult<EditAttributesViewModel, List<TreatmentBMPAttributeSimple>>(
                            $"Entered value for {treatmentBMPTypeAttributeType.TreatmentBMPAttributeType.TreatmentBMPAttributeTypeName} does not match expected type ({treatmentBMPAttributeDataType.TreatmentBMPAttributeDataTypeDisplayName}).", m => m.TreatmentBMPAttributes));
                    }
                }
                
            }

            return errors;
        }
    }
}
