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
using LtInfo.Common.Models;
using Neptune.Web.Models;

namespace Neptune.Web.Views.ModeledCatchment
{
    public class EditViewModel : FormViewModel, IValidatableObject
    {
        [Required]
        public int ModeledCatchmentID { get; set; }

        [Required]
        [DisplayName("Catchment Name")]
        public string ModeledCatchmentName { get; set; }

        [StringLength(Models.ModeledCatchment.FieldLengths.Notes)]
        [DisplayName("Notes")]
        public string Notes { get; set; }
    
        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditViewModel()
        {
        }

        public EditViewModel(Models.ModeledCatchment modeledCatchment)
        {
            ModeledCatchmentID = modeledCatchment.ModeledCatchmentID;
            ModeledCatchmentName = modeledCatchment.ModeledCatchmentName;
            Notes = modeledCatchment.Notes;
        }

        public void UpdateModel(Models.ModeledCatchment modeledCatchment, Person currentPerson)
        {
            modeledCatchment.ModeledCatchmentName = ModeledCatchmentName;
            modeledCatchment.Notes = Notes;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            return validationResults;
        }

    }
}
