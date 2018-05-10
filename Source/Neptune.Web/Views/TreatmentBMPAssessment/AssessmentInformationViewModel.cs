/*-----------------------------------------------------------------------
<copyright file="AssessmentInformationViewModel.cs" company="Tahoe Regional Planning Agency">
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
using LtInfo.Common;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMPAssessment
{
    public class AssessmentInformationViewModel : AssessmentSectionViewModel, IValidatableObject
    {
        public int TreatmentBMPID { get; set; }
        public int TreatmentBMPAssessmentID { get; set; }
        public int CurrentPersonID { get; set; }

        [Required]
        [DisplayName("Conducted By")]
        public int AssessmentPersonID { get; set; }

        [Required]
        [DisplayName("Date of Assessment")]
        public DateTime AssessmentDate { get; set; }

        [StringLength(Models.TreatmentBMPAssessment.FieldLengths.Notes)]
        [DisplayName("Field Notes")]
        public string AssessmentNotes { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.AssessmentForInternalUseOnly)]
        public bool? IsPrivate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.IsPostMaintenanceAssessment)]
        [Required]
        public bool? IsPostMaintenanceAssessment { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.TypeOfAssessment)]
        public int? AssessmentTypeID { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public AssessmentInformationViewModel()
        {            
        }

        public AssessmentInformationViewModel(Models.TreatmentBMPAssessment treatmentBMPAssessment, Person currentPerson)
        {
            CurrentPersonID = currentPerson.PersonID;
            TreatmentBMPID = treatmentBMPAssessment.TreatmentBMPID;
            TreatmentBMPAssessmentID = treatmentBMPAssessment.TreatmentBMPAssessmentID;
            AssessmentPersonID = treatmentBMPAssessment.Person.PersonID;
            AssessmentDate = treatmentBMPAssessment.AssessmentDate;
            AssessmentNotes = treatmentBMPAssessment.Notes;
            IsPrivate = treatmentBMPAssessment.IsPrivate;
            AssessmentTypeID = treatmentBMPAssessment.StormwaterAssessmentTypeID;
            IsPostMaintenanceAssessment = treatmentBMPAssessment.IsPostMaintenanceAssessment;

        }

        public void UpdateModel(Models.TreatmentBMPAssessment treatmentBMPAssessment, Person currentPerson)
        {
            treatmentBMPAssessment.PersonID = AssessmentPersonID;
            treatmentBMPAssessment.AssessmentDate = AssessmentDate;
            treatmentBMPAssessment.Notes = AssessmentNotes;
            treatmentBMPAssessment.IsPostMaintenanceAssessment = IsPostMaintenanceAssessment.Value;

            if (currentPerson.Role == Models.Role.Admin)
            {
                treatmentBMPAssessment.IsPrivate = false;
            }
            else
            {
                treatmentBMPAssessment.IsPrivate = IsPrivate.Value;
            }

            if (currentPerson.Role != Models.Role.Admin)
            {
                treatmentBMPAssessment.StormwaterAssessmentTypeID = StormwaterAssessmentType.Regular.StormwaterAssessmentTypeID;
            }
            else
            {
                treatmentBMPAssessment.StormwaterAssessmentTypeID = AssessmentTypeID.Value;
            }  
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();
            var isAssessmentPersonAdmin = HttpRequestStorage.DatabaseEntities.People.GetPerson(AssessmentPersonID).Role == Models.Role.Admin;
            var isCurrentPersonAdmin = HttpRequestStorage.DatabaseEntities.People.GetPerson(CurrentPersonID).Role == Models.Role.Admin;
            
            if (!AssessmentTypeID.HasValue && isAssessmentPersonAdmin)
            {
                validationResults.Add(new SitkaValidationResult<AssessmentInformationViewModel, int?>("Type of Assessment is required.", x => x.AssessmentTypeID));
            }

            if (!IsPrivate.HasValue && !isAssessmentPersonAdmin && !isCurrentPersonAdmin)
            {
                validationResults.Add(new SitkaValidationResult<AssessmentInformationViewModel, bool?>("For Internal Use Only is required.", x => x.IsPrivate));
            }

            if (isCurrentPersonAdmin && !isAssessmentPersonAdmin && StormwaterAssessmentType.ToType(AssessmentTypeID.Value) == StormwaterAssessmentType.Validation)
            {
                validationResults.Add(new SitkaValidationResult<AssessmentInformationViewModel, int?>("Type of Assessment must be Regular.", x => x.AssessmentTypeID));             
            }

            return validationResults;

        }
    }
}
