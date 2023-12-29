using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Neptune.Common;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using Neptune.WebMvc.Common;

namespace Neptune.WebMvc.Views.OnlandVisualTrashAssessment
{
    public class InitiateOVTAViewModel : OnlandVisualTrashAssessmentViewModel, IValidatableObject
    {
        [Required]
        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.Jurisdiction)]
        public StormwaterJurisdictionDisplayDto? StormwaterJurisdiction { get; set; }

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

        public InitiateOVTAViewModel(Neptune.EFModels.Entities.OnlandVisualTrashAssessment? onlandVisualTrashAssessment)
        {
            StormwaterJurisdiction = onlandVisualTrashAssessment?.StormwaterJurisdiction.AsDisplayDto();
            OnlandVisualTrashAssessmentAreaID = onlandVisualTrashAssessment?.OnlandVisualTrashAssessmentAreaID;
            AssessingNewArea = onlandVisualTrashAssessment?.AssessingNewArea ?? false;
        }

        public InitiateOVTAViewModel(EFModels.Entities.OnlandVisualTrashAssessment onlandVisualTrashAssessment, List<StormwaterJurisdiction> stormwaterJurisdictionsPersonCanEdit) : this(onlandVisualTrashAssessment)
        {
            if (stormwaterJurisdictionsPersonCanEdit.Count == 1)
            {
                StormwaterJurisdiction = stormwaterJurisdictionsPersonCanEdit.Single().AsDisplayDto();
            }
        }

        public void UpdateModel(NeptuneDbContext dbContext, Neptune.EFModels.Entities.OnlandVisualTrashAssessment onlandVisualTrashAssessment)
        {
            onlandVisualTrashAssessment.StormwaterJurisdictionID = StormwaterJurisdiction.StormwaterJurisdictionID;

            if (OnlandVisualTrashAssessmentAreaID.HasValue)
            {
                var transectBackingAssessment =
                    OnlandVisualTrashAssessments.GetTransectBackingAssessment(dbContext,
                        OnlandVisualTrashAssessmentAreaID);

                // ensure the area to which this assessment is assigned ends up with only one transect-backing assessment
                if (transectBackingAssessment != null)
                {
                    if (transectBackingAssessment.OnlandVisualTrashAssessmentID != onlandVisualTrashAssessment.OnlandVisualTrashAssessmentID)
                    {
                        onlandVisualTrashAssessment.IsTransectBackingAssessment = false;
                    }
                }
                else
                {
                    onlandVisualTrashAssessment.IsTransectBackingAssessment = true;
                }
            }
            else
            {
                onlandVisualTrashAssessment.IsTransectBackingAssessment = false;
            }

            onlandVisualTrashAssessment.OnlandVisualTrashAssessmentAreaID = OnlandVisualTrashAssessmentAreaID;
            onlandVisualTrashAssessment.AssessingNewArea = AssessingNewArea;
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
