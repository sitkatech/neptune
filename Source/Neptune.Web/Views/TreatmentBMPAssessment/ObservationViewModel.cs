/*-----------------------------------------------------------------------
<copyright file="ObservationViewModel.cs" company="Tahoe Regional Planning Agency">
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
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LtInfo.Common;
using LtInfo.Common.Models;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMPAssessment
{
    public abstract class ObservationViewModel : FormViewModel, IValidatableObject
    {
        public int TreatmentBMPAssessmentID { get; set; }
        public List<TreatmentBMPObservationDetailSimple> TreatmentBMPObservationDetailSimples { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        protected ObservationViewModel()
        {
        }

        protected ObservationViewModel(Models.TreatmentBMPAssessment treatmentBMPAssessment)            
        {
           TreatmentBMPAssessmentID = treatmentBMPAssessment.TreatmentBMPAssessmentID;
           TreatmentBMPObservationDetailSimples = new List<TreatmentBMPObservationDetailSimple>();           
        }

        public virtual void UpdateModel(TreatmentBMPObservation treatmentBMPObservation)
        {
            
        }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            return validationResults;
        }
    }
}
