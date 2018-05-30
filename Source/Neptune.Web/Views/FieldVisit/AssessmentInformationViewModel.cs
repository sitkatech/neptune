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
using LtInfo.Common.Models;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.Views.FieldVisit
{
    public class AssessmentInformationViewModel : FieldVisitSectionViewModel, IValidatableObject
    {
        public int TreatmentBMPID { get; set; }
        public int TreatmentBMPAssessmentID { get; set; }
        public int CurrentPersonID { get; set; }

        [StringLength(Models.TreatmentBMPAssessment.FieldLengths.Notes)]
        [DisplayName("Field Notes")]
        public string AssessmentNotes { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.AssessmentForInternalUseOnly)]
        public bool? IsPrivate { get; set; }

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
            AssessmentNotes = treatmentBMPAssessment.Notes;
            IsPrivate = treatmentBMPAssessment.IsPrivate;

        }

        public void UpdateModel(Models.TreatmentBMPAssessment treatmentBMPAssessment, Person currentPerson)
        {
            treatmentBMPAssessment.Notes = AssessmentNotes;

            if (currentPerson.Role == Models.Role.Admin)
            {
                treatmentBMPAssessment.IsPrivate = false;
            }
            else
            {
                treatmentBMPAssessment.IsPrivate = IsPrivate.Value;
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();
            var isCurrentPersonAdmin = HttpRequestStorage.DatabaseEntities.People.GetPerson(CurrentPersonID).Role == Models.Role.Admin;

            if (!IsPrivate.HasValue && !isCurrentPersonAdmin)
            {
                validationResults.Add(new SitkaValidationResult<AssessmentInformationViewModel, bool?>("For Internal Use Only is required.", x => x.IsPrivate));
            }

            return validationResults;

        }
    }
}
