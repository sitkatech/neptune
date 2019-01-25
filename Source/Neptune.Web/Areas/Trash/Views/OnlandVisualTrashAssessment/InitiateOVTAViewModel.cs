using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using LtInfo.Common;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class InitiateOVTAViewModel : OnlandVisualTrashAssessmentViewModel, IValidatableObject
    {
        [Required]
        [FieldDefinitionDisplay(
            FieldDefinitionEnum.Jurisdiction)]
        public int? StormwaterJurisdictionID { get; set; }

        [StringLength(Models.OnlandVisualTrashAssessment.FieldLengths.Notes)]
        public string Notes { get; set; }

        public int? OnlandVisualTrashAssessmentAreaID { get; set; }

        [Required]
        [DisplayName("Assessing a new area?")]
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
                yield return new SitkaValidationResult<InitiateOVTAViewModel,int?>("You must choose an area to assess or check the box at the bottom of the page.", m=>m.OnlandVisualTrashAssessmentAreaID);
            }
        }
    }
}
