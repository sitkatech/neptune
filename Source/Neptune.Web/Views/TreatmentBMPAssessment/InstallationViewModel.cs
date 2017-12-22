/*-----------------------------------------------------------------------
<copyright file="InstallationViewModel.cs" company="Tahoe Regional Planning Agency">
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
using System.Linq;
using LtInfo.Common;
using LtInfo.Common.Models;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMPAssessment
{
    public class InstallationViewModel : FormViewModel, IValidatableObject
    {
        public int TreatmentBMPAssessmentID { get; set; }
        
        public int? InstallationObservationDetailID { get; set; }
        [Required]
        [DisplayName("Observation")]
        public bool? InstallationObservationValue { get; set; }
        [DisplayName("Observation Notes")]
        public string InstallationObservationNotes { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public InstallationViewModel()
        {
        }

        public InstallationViewModel(Models.TreatmentBMPAssessment treatmentBMPAssessment)
        {
            TreatmentBMPAssessmentID = treatmentBMPAssessment.TreatmentBMPAssessmentID;
            var treatmentBMPObservationDetails =
                treatmentBMPAssessment.TreatmentBMPObservations.Where(x => x.ObservationType.ToEnum == ObservationTypeEnum.Installation)
                    .SelectMany(x => x.TreatmentBMPObservationDetails)
                    .OrderBy(x => x.TreatmentBMPObservationDetailID)
                    .ToList();
            
            var initialTreatmentBMPObservationDetail = treatmentBMPObservationDetails.FirstOrDefault();
            InstallationObservationDetailID = initialTreatmentBMPObservationDetail != null ? (int?)initialTreatmentBMPObservationDetail.TreatmentBMPObservationDetailID : null;
            InstallationObservationValue = initialTreatmentBMPObservationDetail != null
                ? (bool?) (Math.Abs(initialTreatmentBMPObservationDetail.TreatmentBMPObservationValue) > double.Epsilon)
                : null;
            InstallationObservationNotes = initialTreatmentBMPObservationDetail?.Notes;            
        }

        public virtual void UpdateModel(TreatmentBMPObservation treatmentBMPObservation, IList<TreatmentBMPObservationDetail> allTreatmentBMPObservationDetails)
        {
            var treatmentBMPObservationDetailUpdated = new List<TreatmentBMPObservationDetail>
            {
                new TreatmentBMPObservationDetail(InstallationObservationDetailID ?? ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue(),
                    treatmentBMPObservation.TreatmentBMPObservationID,
                    TreatmentBMPObservationDetailType.Installation.TreatmentBMPObservationDetailTypeID,
                    InstallationObservationValue.Value ? 1 : 0,
                    InstallationObservationNotes)
            };
           
            treatmentBMPObservation.TreatmentBMPObservationDetails.Merge(treatmentBMPObservationDetailUpdated,
                allTreatmentBMPObservationDetails,
                (x, y) => x.TreatmentBMPObservationDetailID == y.TreatmentBMPObservationDetailID,
                (x, y) =>
                {
                    x.TreatmentBMPObservationValue = y.TreatmentBMPObservationValue;
                    x.Notes = y.Notes;
                });
        }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            return validationResults;
        }
    }
}
