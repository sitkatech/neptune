/*-----------------------------------------------------------------------
<copyright file="NewFieldVisitViewModel.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using LtInfo.Common.Models;
using Neptune.Web.Models;

namespace Neptune.Web.Views.FieldVisit
{
    public class NewFieldVisitViewModel : FormViewModel, IValidatableObject
    {
        [Required]
        public bool? InProgressFieldVisitExists { get; set; }
        public bool? Continue { get; set; }
        [Required]
        [DisplayName("Field Visit Type")]
        public int? FieldVisitTypeID { get; set; }
        [Required]
        [DisplayName("Field Visit Date")]
        public DateTime FieldVisitDate { get; set; }

        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public NewFieldVisitViewModel()
        {
        }

        public NewFieldVisitViewModel(Models.FieldVisit inProgressFieldVisit)
        {
            InProgressFieldVisitExists = inProgressFieldVisit != null;
            FieldVisitTypeID = inProgressFieldVisit?.FieldVisitTypeID ?? FieldVisitType.DryWeather.FieldVisitTypeID;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (InProgressFieldVisitExists.Value && Continue == null)
            {
                errors.Add(new ValidationResult("You must select an option."));
            }

            return errors;
        }
    }
}