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
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMPAssessmentObservationType
{
    public class EditViewModel : FormViewModel, IValidatableObject
    {
        public int TreatmentBMPAssessmentObservationTypeID { get; set; }

        [Required]
        [StringLength(Models.TreatmentBMPAssessmentObservationType.FieldLengths.TreatmentBMPAssessmentObservationTypeName)]
        [DisplayName("Name of Observation Type")]
        public string TreatmentBMPAssessmentObservationTypeName { get; set; }      

        [Required]
        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.ObservationThresholdType)]
        public int? ObservationThresholdTypeID { get; set; }

        [Required]
        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.ObservationTargetType)]
        public int? ObservationTargetTypeID { get; set; }

        [Required]
        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.ObservationCollectionMethod)]
        public int? ObservationTypeCollectionMethodID { get; set; }

        [Required]
        public string TreatmentBMPAssessmentObservationTypeSchema { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditViewModel()
        {
        }

        public EditViewModel(Models.TreatmentBMPAssessmentObservationType TreatmentBMPAssessmentObservationType)
        {
            TreatmentBMPAssessmentObservationTypeID = TreatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID;
            TreatmentBMPAssessmentObservationTypeName = TreatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeName;

            ObservationThresholdTypeID = TreatmentBMPAssessmentObservationType.ObservationTypeSpecification?.ObservationThresholdTypeID;
            ObservationTargetTypeID = TreatmentBMPAssessmentObservationType.ObservationTypeSpecification?.ObservationTargetTypeID;
            ObservationTypeCollectionMethodID = TreatmentBMPAssessmentObservationType.ObservationTypeSpecification?.ObservationTypeCollectionMethodID;

            TreatmentBMPAssessmentObservationTypeSchema = TreatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeSchema;
        }


        public void UpdateModel(Models.TreatmentBMPAssessmentObservationType TreatmentBMPAssessmentObservationType, Person currentPerson)
        {
            TreatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeName = TreatmentBMPAssessmentObservationTypeName;

            var observationTypeSpecification = ObservationTypeSpecification.All.FirstOrDefault(x => 
                x.ObservationTargetTypeID == ObservationTargetTypeID && 
                x.ObservationThresholdTypeID == ObservationThresholdTypeID && 
                x.ObservationTypeCollectionMethodID == ObservationTypeCollectionMethodID);

            Check.Require(observationTypeSpecification != null, "No valid combination of Target Type, Threshold Type and Collection Method found");

            TreatmentBMPAssessmentObservationType.ObservationTypeSpecificationID = observationTypeSpecification.ObservationTypeSpecificationID;
            TreatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeSchema = TreatmentBMPAssessmentObservationTypeSchema;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            var observationTypesWithSameName = HttpRequestStorage.DatabaseEntities.TreatmentBMPAssessmentObservationTypes.Where(x => x.TreatmentBMPAssessmentObservationTypeName == TreatmentBMPAssessmentObservationTypeName);

            if (observationTypesWithSameName.Any(x => x.TreatmentBMPAssessmentObservationTypeID != TreatmentBMPAssessmentObservationTypeID))
            {
                validationResults.Add(new ValidationResult("An Observation Type with this name already exists"));
            }

            var observationTypeSpecification = ObservationTypeSpecification.All.FirstOrDefault(x =>
                x.ObservationTargetTypeID == ObservationTargetTypeID &&
                x.ObservationThresholdTypeID == ObservationThresholdTypeID &&
                x.ObservationTypeCollectionMethodID == ObservationTypeCollectionMethodID);
            if (observationTypeSpecification == null)
            {
                validationResults.Add(new ValidationResult("Enter a valid combination of Target Type, Threshold Type and Collection Method"));
            }
           
            var observationTypeCollectionMethod = ObservationTypeCollectionMethod.AllLookupDictionary[ObservationTypeCollectionMethodID.Value];
            if (!observationTypeCollectionMethod.ValidateObservationTypeJson(TreatmentBMPAssessmentObservationTypeSchema))
            {
                validationResults.Add(new ValidationResult("Incomplete information about the observation type. Complete each required field and try again."));
                return validationResults;
            }            

            validationResults.AddRange(observationTypeCollectionMethod.ValidateObservationType(TreatmentBMPAssessmentObservationTypeSchema));

            return validationResults;
        }
    }
}
