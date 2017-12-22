/*-----------------------------------------------------------------------
<copyright file="VegetativeCoverViewModel.cs" company="Tahoe Regional Planning Agency">
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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LtInfo.Common;
using LtInfo.Common.Models;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMPAssessment
{
    public class VegetativeCoverViewModel : ObservationViewModel
    {
        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public VegetativeCoverViewModel()
        {            
        }

        public VegetativeCoverViewModel(Models.TreatmentBMPAssessment treatmentBMPAssessment)
            : base(treatmentBMPAssessment, ObservationTypeEnum.VegetativeCover)
        {
            var observation = treatmentBMPAssessment.TreatmentBMPObservations.SingleOrDefault(x => x.ObservationType.ToEnum == ObservationTypeEnum.VegetativeCover);

            if (observation == null)
            {
                TreatmentBMPObservationDetailSimples.Add(new TreatmentBMPObservationDetailSimple(ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue(),
                    TreatmentBMPObservationDetailType.VegetativeCoverWetlandAndRiparianSpecies.TreatmentBMPObservationDetailTypeID,
                    null,
                    string.Empty));
                TreatmentBMPObservationDetailSimples.Add(new TreatmentBMPObservationDetailSimple(ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue(),
                    TreatmentBMPObservationDetailType.VegetativeCoverTreeSpecies.TreatmentBMPObservationDetailTypeID,
                    null,
                    string.Empty));
                TreatmentBMPObservationDetailSimples.Add(new TreatmentBMPObservationDetailSimple(ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue(),
                    TreatmentBMPObservationDetailType.VegetativeCoverGrassSpecies.TreatmentBMPObservationDetailTypeID,
                    null,
                    string.Empty));
            }
            else
            {
                if (observation.TreatmentBMPObservationDetails.FirstOrDefault(x => x.TreatmentBMPObservationDetailType == TreatmentBMPObservationDetailType.VegetativeCoverWetlandAndRiparianSpecies) == null)
                {
                    TreatmentBMPObservationDetailSimples.Add(new TreatmentBMPObservationDetailSimple(ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue(),
                    TreatmentBMPObservationDetailType.VegetativeCoverWetlandAndRiparianSpecies.TreatmentBMPObservationDetailTypeID,
                    null,
                    string.Empty));
                }
                if (observation.TreatmentBMPObservationDetails.FirstOrDefault(x => x.TreatmentBMPObservationDetailType == TreatmentBMPObservationDetailType.VegetativeCoverTreeSpecies) == null)
                {
                    TreatmentBMPObservationDetailSimples.Add(new TreatmentBMPObservationDetailSimple(ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue(),
                    TreatmentBMPObservationDetailType.VegetativeCoverTreeSpecies.TreatmentBMPObservationDetailTypeID,
                    null,
                    string.Empty));
                }
                if (observation.TreatmentBMPObservationDetails.FirstOrDefault(x => x.TreatmentBMPObservationDetailType == TreatmentBMPObservationDetailType.VegetativeCoverGrassSpecies) == null)
                {
                    TreatmentBMPObservationDetailSimples.Add(new TreatmentBMPObservationDetailSimple(ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue(),
                    TreatmentBMPObservationDetailType.VegetativeCoverGrassSpecies.TreatmentBMPObservationDetailTypeID,
                    null,
                    string.Empty));
                }
            } 
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            if (TreatmentBMPObservationDetailSimples.Sum(x => x.TreatmentBMPObservationValue) > 100)
            {
                validationResults.Add(new SitkaValidationResult<VegetativeCoverViewModel, List<TreatmentBMPObservationDetailSimple>>("Total species coverage must be less than or equal to 100%", m => m.TreatmentBMPObservationDetailSimples));
            }

            return validationResults;
        }
    }
}
