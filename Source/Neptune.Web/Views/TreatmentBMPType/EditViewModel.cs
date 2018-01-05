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
using LtInfo.Common;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMPType
{
    public class EditViewModel : FormViewModel, IValidatableObject
    {
        public int TreatmentBMPTypeID { get; set; }

        [Required]
        [StringLength(Models.TreatmentBMPType.FieldLengths.TreatmentBMPTypeName)]
        [DisplayName("Name of Treatment BMP Type")]
        public string TreatmentBMPTypeName { get; set; }

        [Required]
        public List<TreatmentBMPTypeObservationTypeSimple> TreatmentBMPTypeObservationTypeSimples { get; set; }


        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditViewModel()
        {
        }

        public EditViewModel(Models.TreatmentBMPType treatmentBMPType)
        {
            TreatmentBMPTypeID = treatmentBMPType.TreatmentBMPTypeID;
            TreatmentBMPTypeName = treatmentBMPType.TreatmentBMPTypeName;
            TreatmentBMPTypeObservationTypeSimples = treatmentBMPType.TreatmentBMPTypeObservationTypes
                .Select(x => new TreatmentBMPTypeObservationTypeSimple(x)).ToList();
        }


        public void UpdateModel(Models.TreatmentBMPType treatmentBMPType, List<TreatmentBMPTypeObservationType> currentTreatmentBMPTypeObservationTypes,
            IList<TreatmentBMPTypeObservationType> allTreatmentBMPTypeObservationTypes)
        {
            treatmentBMPType.TreatmentBMPTypeName = TreatmentBMPTypeName;

            var updatedTreatmentBMPTypeObservationTypes = new List<TreatmentBMPTypeObservationType>();
            if (TreatmentBMPTypeObservationTypeSimples != null)
            {
                // Completely rebuild the list
                updatedTreatmentBMPTypeObservationTypes = TreatmentBMPTypeObservationTypeSimples.Select(x => new TreatmentBMPTypeObservationType(treatmentBMPType.TreatmentBMPTypeID, x.ObservationTypeID, x.AssessmentScoreWeight)).ToList();
            }

            currentTreatmentBMPTypeObservationTypes.Merge(updatedTreatmentBMPTypeObservationTypes,
                allTreatmentBMPTypeObservationTypes,
                (x, y) => x.TreatmentBMPTypeID == y.TreatmentBMPTypeID && x.ObservationTypeID == y.ObservationTypeID,
                (x, y) =>
                {
                    x.AssessmentScoreWeight = y.AssessmentScoreWeight;
                });


        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();


            return validationResults;
        }
    }

    public class TreatmentBMPTypeObservationTypeSimple
    {
        public int TreatmentBMPTypeID { get; }
        public int ObservationTypeID { get; }
        public double AssessmentScoreWeight { get; }
        public TreatmentBMPTypeObservationTypeSimple(TreatmentBMPTypeObservationType treatmentBMPTypeObservationType)
        {
            TreatmentBMPTypeID = treatmentBMPTypeObservationType.TreatmentBMPTypeID;
            ObservationTypeID = treatmentBMPTypeObservationType.ObservationTypeID;
            AssessmentScoreWeight = treatmentBMPTypeObservationType.AssessmentScoreWeight;
        }
    }
}
