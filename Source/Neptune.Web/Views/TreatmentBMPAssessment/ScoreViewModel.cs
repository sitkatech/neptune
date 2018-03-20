/*-----------------------------------------------------------------------
<copyright file="ScoreViewModel.cs" company="Tahoe Regional Planning Agency">
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
using LtInfo.Common;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMPAssessment
{
    public class ScoreViewModel : AssessmentSectionViewModel, IValidatableObject
    {
        public int TreatmentBMPID { get; set; }
        public int TreatmentBMPAssessmentID { get; set; }

        [DisplayName("Use an Alternative Score?")]
        public bool UseAlternateScore { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.AlternativeScore)]
        [Range(0, 5, ErrorMessage = "Score must be between 0-5")]
        public double? AlternativeAssessmentScore { get; set; }

        [DisplayName("Reason for Alternative Score")]
        [StringLength(Models.TreatmentBMPAssessment.FieldLengths.AlternateAssessmentRationale)]
        public string AlternativeAssessmentScoreRationale { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public ScoreViewModel()
        {            
        }

        public ScoreViewModel(Models.TreatmentBMPAssessment treatmentBMPAssessment)
        {
            TreatmentBMPID = treatmentBMPAssessment.TreatmentBMPID;
            TreatmentBMPAssessmentID = treatmentBMPAssessment.TreatmentBMPAssessmentID;
            if (treatmentBMPAssessment.AlternateAssessmentScore != null)
            {
                UseAlternateScore = true;
                AlternativeAssessmentScore = treatmentBMPAssessment.AlternateAssessmentScore;
                AlternativeAssessmentScoreRationale = treatmentBMPAssessment.AlternateAssessmentRationale;
            }
            else
            {
                UseAlternateScore = false;
            }
            
        }

        public void UpdateModel(Models.TreatmentBMPAssessment treatmentBMPAssessment, Person currentPerson)
        {
            if (UseAlternateScore)
            {
                treatmentBMPAssessment.AlternateAssessmentScore = AlternativeAssessmentScore;
                treatmentBMPAssessment.AlternateAssessmentRationale = AlternativeAssessmentScoreRationale;
            }
            else
            {
                treatmentBMPAssessment.AlternateAssessmentScore = null;
                treatmentBMPAssessment.AlternateAssessmentRationale = null;
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            if ((UseAlternateScore && AlternativeAssessmentScore == null) || (UseAlternateScore && AlternativeAssessmentScoreRationale == null))
            {
                validationResults.Add(new SitkaValidationResult<ScoreViewModel, bool>("Both Score and Rationale are required to use an optional Alternative Score.", x => x.UseAlternateScore));
            }

            if (AlternativeAssessmentScore.ToString().Length > 3)
            {
                validationResults.Add(new SitkaValidationResult<ScoreViewModel, double?>("Alternative Score can only have one decimal value.", x => x.AlternativeAssessmentScore));
            }

            return validationResults;
        }
    }
}
