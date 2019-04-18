using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LtInfo.Common;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class InitiateOVTAViewModel : OnlandVisualTrashAssessmentViewModel, IValidatableObject
    {
        [Required]
        [FieldDefinitionDisplay(
            FieldDefinitionEnum.Jurisdiction)]
        public StormwaterJurisdictionSimple StormwaterJurisdiction { get; set; }

        [DisplayName("Assessment Area")]
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
            StormwaterJurisdiction = ovta?.StormwaterJurisdiction != null ? new StormwaterJurisdictionSimple(ovta.StormwaterJurisdiction) : null;
            OnlandVisualTrashAssessmentAreaID = ovta?.OnlandVisualTrashAssessmentAreaID;
            AssessingNewArea = ovta?.AssessingNewArea ?? false;
        }

        public InitiateOVTAViewModel(Models.OnlandVisualTrashAssessment ovta, Person currentPerson) : this(ovta)
        {
            var stormwaterJurisdictionsPersonCanEdit = currentPerson.GetStormwaterJurisdictionsPersonCanEdit().ToList();
            if (stormwaterJurisdictionsPersonCanEdit.Count() == 1)
            {
                StormwaterJurisdiction =
                    new StormwaterJurisdictionSimple(stormwaterJurisdictionsPersonCanEdit.Single());
            }
        }

        public void UpdateModel(Models.OnlandVisualTrashAssessment ovta)
        {
            Check.Require(ModelObjectHelpers.IsRealPrimaryKeyValue(ovta.OnlandVisualTrashAssessmentID));

            ovta.StormwaterJurisdictionID = StormwaterJurisdiction?.StormwaterJurisdictionID;

            var transectBackingAssessment = HttpRequestStorage.DatabaseEntities.OnlandVisualTrashAssessmentAreas.Find(OnlandVisualTrashAssessmentAreaID).GetTransectBackingAssessment();
            
            // ensure the area to which this assessment is assigned ends up with only one transect-backing assessment
            if (transectBackingAssessment != null)
            {
                if (transectBackingAssessment.OnlandVisualTrashAssessmentID != ovta.OnlandVisualTrashAssessmentID)
                {
                    ovta.IsTransectBackingAssessment = false;
                }
            }
            else
            {
                ovta.IsTransectBackingAssessment = true;
            }

            ovta.OnlandVisualTrashAssessmentAreaID = OnlandVisualTrashAssessmentAreaID;
            ovta.AssessingNewArea = AssessingNewArea;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return GetValidationResults();
        }

        public IEnumerable<ValidationResult> GetValidationResults()
        {
            if (!AssessingNewArea.GetValueOrDefault() && !OnlandVisualTrashAssessmentAreaID.HasValue)
            {
                yield return new SitkaValidationResult<InitiateOVTAViewModel,int?>("You must choose an area to assess.", m=>m.OnlandVisualTrashAssessmentAreaID);
            }

            if (AssessingNewArea.GetValueOrDefault() && OnlandVisualTrashAssessmentAreaID.HasValue)
            {
                yield return new SitkaValidationResult<InitiateOVTAViewModel, bool?>(
                    "You cannot be assessing a new area if you have selected an existing area to assess.",
                    m => m.AssessingNewArea);
            }
        }
    }
}
