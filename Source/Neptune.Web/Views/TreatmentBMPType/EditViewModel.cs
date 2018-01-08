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
        [StringLength(Models.TreatmentBMPType.FieldLengths.TreatmentBMPTypeDescription)]
        [DisplayName("Description")]
        public string TreatmentBMPTypeDescription { get; set; }

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
            TreatmentBMPTypeDescription = treatmentBMPType.TreatmentBMPTypeDescription;
            TreatmentBMPTypeObservationTypeSimples = treatmentBMPType.TreatmentBMPTypeObservationTypes
                .Select(x => new TreatmentBMPTypeObservationTypeSimple(x)).ToList();
        }


        public void UpdateModel(Models.TreatmentBMPType treatmentBMPType, List<TreatmentBMPTypeObservationType> 
            currentTreatmentBMPTypeObservationTypes,
            IList<TreatmentBMPTypeObservationType> allTreatmentBMPTypeObservationTypes)
        {
            treatmentBMPType.TreatmentBMPTypeName = TreatmentBMPTypeName;
            treatmentBMPType.TreatmentBMPTypeDescription = TreatmentBMPTypeDescription;

            var updatedTreatmentBMPTypeObservationTypes = new List<TreatmentBMPTypeObservationType>();
            if (TreatmentBMPTypeObservationTypeSimples != null)
            {
                // Completely rebuild the list
                updatedTreatmentBMPTypeObservationTypes = TreatmentBMPTypeObservationTypeSimples.Select(x =>
                {
                    return new TreatmentBMPTypeObservationType(ModelObjectHelpers.NotYetAssignedID,
                        treatmentBMPType.TreatmentBMPTypeID,
                        x.ObservationTypeID,
                        x.OverrideAssessmentScoreIfFailing != null && x.OverrideAssessmentScoreIfFailing.Value ? null : x.AssessmentScoreWeight,
                        x.DefaultThresholdValue,
                        x.DefaultBenchmarkValue,
                        x.OverrideAssessmentScoreIfFailing);
                }).ToList();
            }

            currentTreatmentBMPTypeObservationTypes.Merge(updatedTreatmentBMPTypeObservationTypes,
                allTreatmentBMPTypeObservationTypes,
                (x, y) => x.TreatmentBMPTypeID == y.TreatmentBMPTypeID && x.ObservationTypeID == y.ObservationTypeID,
                (x, y) =>
                {
                    x.AssessmentScoreWeight = y.AssessmentScoreWeight;
                    x.DefaultThresholdValue = y.DefaultThresholdValue;
                    x.DefaultBenchmarkValue = y.DefaultBenchmarkValue;
                    x.OverrideAssessmentScoreIfFailing = y.OverrideAssessmentScoreIfFailing;
                });
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            var treatmentBMPTypesWithSameName = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Where(x => x.TreatmentBMPTypeName == TreatmentBMPTypeName);

            if (treatmentBMPTypesWithSameName.Any(x => x.TreatmentBMPTypeID != TreatmentBMPTypeID))
            {
                validationResults.Add(new SitkaValidationResult<EditViewModel, string>("A Treatment BMP Type with this name already exists.", x => x.TreatmentBMPTypeName));
            }

            if (TreatmentBMPTypeObservationTypeSimples == null || !TreatmentBMPTypeObservationTypeSimples.Any())
            {
                validationResults.Add(new ValidationResult("A Treatment BMP Type must have at least one Observation Type."));
                return validationResults;
            }

            var hasBenchmarkAndThresholdsSimples = TreatmentBMPTypeObservationTypeSimples.Where(y => HttpRequestStorage.DatabaseEntities.ObservationTypes.ToList().Where(x => x.HasBenchmarkAndThreshold).ToList().Select(x => x.ObservationTypeID).Contains(y.ObservationTypeID)).ToList();

            var noBenchmarkAndThresholdsSimples = TreatmentBMPTypeObservationTypeSimples.Where(y => HttpRequestStorage.DatabaseEntities.ObservationTypes.ToList().Where(x => !x.HasBenchmarkAndThreshold).Select(x => x.ObservationTypeID).ToList().Contains(y.ObservationTypeID)).ToList();

            var requiresAssessmentWeightSimples = new List<TreatmentBMPTypeObservationTypeSimple>();
            requiresAssessmentWeightSimples.AddRange(hasBenchmarkAndThresholdsSimples);
            requiresAssessmentWeightSimples.AddRange(noBenchmarkAndThresholdsSimples.Where(x => !x.OverrideAssessmentScoreIfFailing.HasValue || !x.OverrideAssessmentScoreIfFailing.Value));           

            if (requiresAssessmentWeightSimples.Any(x => x.AssessmentScoreWeight == null))
            {
                validationResults.Add(new ValidationResult("Each Observation Type that does not override the Assessment Score if failing must have an Assessment Score Weight."));
            }

            if (requiresAssessmentWeightSimples.Any() && TreatmentBMPTypeObservationTypeSimples.Sum(x => x.AssessmentScoreWeight) != 1)
            {
                validationResults.Add(new ValidationResult("The total Assessment Score Weight for all Observation Types must equal 1."));
            }

            return validationResults;
        }
    }
}
