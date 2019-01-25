using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class InitiateOVTAViewModel : OnlandVisualTrashAssessmentViewModel, IValidatableObject
    {
        [Required]
        public int? StormwaterJurisdictionID { get; set; }

        [StringLength(Models.OnlandVisualTrashAssessment.FieldLengths.Notes)]
        public string Notes { get; set; }

        public int? OnlandVisualTrashAssessmentAreaID { get; set; }

        [Required]
        public bool? AssessingNewArea { get; set; }

        /// <summary>
        /// needed by modelbinder
        /// </summary>
        public InitiateOVTAViewModel()
        {

        }

        public InitiateOVTAViewModel(Models.OnlandVisualTrashAssessment ovta)
        {
            StormwaterJurisdictionID = ovta.StormwaterJurisdictionID;
            Notes = ovta.Notes;
            OnlandVisualTrashAssessmentAreaID = ovta.OnlandVisualTrashAssessmentAreaID;
            AssessingNewArea = ovta.AssessingNewArea;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return GetValidationResults();
        }

        public IEnumerable<ValidationResult> GetValidationResults()
        {
            if (!AssessingNewArea.GetValueOrDefault() && !OnlandVisualTrashAssessmentAreaID.HasValue)
            {
                yield return new ValidationResult("You must choose an area to assess.");
            }
        }
    }
}
