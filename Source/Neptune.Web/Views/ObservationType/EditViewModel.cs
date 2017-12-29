/*-----------------------------------------------------------------------
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
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Models;

namespace Neptune.Web.Views.ObservationType
{
    public class EditViewModel : FormViewModel, IValidatableObject
    {
        public int ObservationTypeID { get; set; }

        [Required]
        [StringLength(Models.ObservationType.FieldLengths.ObservationTypeName)]
        [DisplayName("Name")]
        public string ObservationTypeName { get; set; }

        [Required]
        [DisplayName("Measurement Unit")]
        public int? MeasurementUnitTypeID { get; set; }

        [Required]
        [DisplayName("Threshold Type")]
        public int? ObservationThresholdTypeID { get; set; }

        [Required]
        [DisplayName("Observation Target Type")]
        public int? ObservationTargetTypeID { get; set; }

        [Required]
        [DisplayName("Collection Method")]
        public int? ObservationTypeCollectionMethodID { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditViewModel()
        {
        }

        public EditViewModel(Models.ObservationType observationType)
        {
            ObservationTypeID = observationType.ObservationTypeID;
            ObservationTypeName = observationType.ObservationTypeName;
            MeasurementUnitTypeID = observationType.MeasurementUnitTypeID;
            ObservationThresholdTypeID = observationType.ObservationTypeSpecification?.ObservationThresholdTypeID;
            ObservationTargetTypeID = observationType.ObservationTypeSpecification?.ObservationTargetTypeID;
            ObservationTypeCollectionMethodID = observationType.ObservationTypeSpecification?.ObservationTypeCollectionMethodID;
        }

        public void UpdateModel(Models.ObservationType observationType, Person currentPerson)
        {
            observationType.ObservationTypeName = ObservationTypeName;
            observationType.MeasurementUnitTypeID = MeasurementUnitTypeID.Value;

            var observationTypeSpecification = ObservationTypeSpecification.All.FirstOrDefault(x => 
                x.ObservationTargetTypeID == ObservationTargetTypeID && 
                x.ObservationThresholdTypeID == ObservationThresholdTypeID && 
                x.ObservationTypeCollectionMethodID == ObservationTypeCollectionMethodID);

            Check.Require(observationTypeSpecification != null, "No valid combination of Target Type, Threshold Type and Collection Method found");

            observationType.ObservationTypeSpecificationID = observationTypeSpecification.ObservationTypeSpecificationID;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            var observationTypeSpecification = ObservationTypeSpecification.All.FirstOrDefault(x =>
                x.ObservationTargetTypeID == ObservationTargetTypeID &&
                x.ObservationThresholdTypeID == ObservationThresholdTypeID &&
                x.ObservationTypeCollectionMethodID == ObservationTypeCollectionMethodID);
            if (observationTypeSpecification == null)
            {
                validationResults.Add(new ValidationResult("Enter a valid combination of Target Type, Threshold Type and Collection Method."));
            }

            return validationResults;
        }

    }
}
