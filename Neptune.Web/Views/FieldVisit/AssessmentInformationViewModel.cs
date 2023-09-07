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

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Neptune.EFModels.Entities;

namespace Neptune.Web.Views.FieldVisit
{
    public class AssessmentInformationViewModel : FieldVisitViewModel
    {
        public int TreatmentBMPID { get; set; }
        public int TreatmentBMPAssessmentID { get; set; }
        public int CurrentPersonID { get; set; }

        [StringLength(EFModels.Entities.TreatmentBMPAssessment.FieldLengths.Notes)]
        [DisplayName("Field Notes")]
        public string AssessmentNotes { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public AssessmentInformationViewModel()
        {            
        }

        public AssessmentInformationViewModel(EFModels.Entities.TreatmentBMPAssessment treatmentBMPAssessment, Person currentPerson)
        {
            CurrentPersonID = currentPerson.PersonID;
            TreatmentBMPID = treatmentBMPAssessment.TreatmentBMPID;
            TreatmentBMPAssessmentID = treatmentBMPAssessment.TreatmentBMPAssessmentID;
            AssessmentNotes = treatmentBMPAssessment.Notes;

        }

        public void UpdateModel(EFModels.Entities.TreatmentBMPAssessment treatmentBMPAssessment, Person currentPerson)
        {
            treatmentBMPAssessment.Notes = AssessmentNotes;
        }
    }
}
