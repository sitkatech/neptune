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
        [Required]
        public int TreatmentBMPTypeID { get; set; }

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
            TreatmentBMPTypeID = treatmentBMP.TreatmentBMPTypeID;
            TreatmentBMPAttributes = treatmentBMP.TreatmentBMPAttributes
                .Select(x => new TreatmentBMPAttributeSimple
                {
                    TreatmentBMPAttributeTypeID = x.TreatmentBMPAttributeTypeID,
                    TreatmentBMPAttributeValue = x.TreatmentBMPAttributeValue
                })
                .ToList();
        }

        public void UpdateModel(Models.TreatmentBMP treatmentBMP, Person currentPerson)
        {
            var treatmentBMPAttributesToUpdate = TreatmentBMPAttributes.Where(x => !string.IsNullOrWhiteSpace(x.TreatmentBMPAttributeValue))
                .Select(x => new TreatmentBMPAttribute(treatmentBMP.TreatmentBMPID, treatmentBMP.TreatmentBMPTypeID, x.TreatmentBMPAttributeTypeID, x.TreatmentBMPAttributeValue))
                .ToList();
            var treatmentBMPAttributesInDatabase = HttpRequestStorage.DatabaseEntities.AllTreatmentBMPAttributes.Local;
            treatmentBMP.TreatmentBMPAttributes.Merge(treatmentBMPAttributesToUpdate, treatmentBMPAttributesInDatabase,
                (x, y) => x.TreatmentBMPID == y.TreatmentBMPID && x.TreatmentBMPTypeID == y.TreatmentBMPTypeID &&
                          x.TreatmentBMPAttributeTypeID == y.TreatmentBMPAttributeTypeID,
                (x, y) => { x.TreatmentBMPAttributeValue = y.TreatmentBMPAttributeValue; });
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            var treatmentBMPAttributeTypeIDs = TreatmentBMPAttributes.Select(x => x.TreatmentBMPAttributeTypeID).ToList();
            var treatmentBMPTypeAttributeTypes = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypeAttributeTypes.Where(x => x.TreatmentBMPTypeID == TreatmentBMPTypeID && treatmentBMPAttributeTypeIDs.Contains(x.TreatmentBMPAttributeTypeID)).ToList();
            if (treatmentBMPTypeAttributeTypes.Any(x =>
                TreatmentBMPAttributes.SingleOrDefault(y => y.TreatmentBMPAttributeTypeID == x.TreatmentBMPAttributeTypeID && x.TreatmentBMPAttributeType.IsRequired && string.IsNullOrWhiteSpace(y.TreatmentBMPAttributeValue)) != null))
            {
                errors.Add(new SitkaValidationResult<EditAttributesViewModel, List<TreatmentBMPAttributeSimple>>("Must enter all required fields.", m => m.TreatmentBMPAttributes));
            }

            return errors;
        }
    }
}
