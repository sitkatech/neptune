using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LtInfo.Common;
using LtInfo.Common.DesignByContract;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class FinalizeOVTAViewModel : OnlandVisualTrashAssessmentViewModel, IValidatableObject
    {
        public bool? Finalize { get; set; }

        [Required]
        [StringLength(Models.OnlandVisualTrashAssessmentArea.FieldLengths.OnlandVisualTrashAssessmentAreaName)]
        [DisplayName("Assessment Area Name")]
        public string AssessmentAreaName { get; set; }

        [StringLength(Models.OnlandVisualTrashAssessment.FieldLengths.Notes)]
        [FieldDefinitionDisplay(FieldDefinitionEnum.OnlandVisualTrashAssessmentNotes)]
        public string Notes { get; set; }

        [Required]
        [DisplayName("Assessment Score")]
        public int? ScoreID { get; set; }

        [Required]
        public int? StormwaterJurisdictionID { get; set; }

        public int? AssessmentAreaID { get; set; }

        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public FinalizeOVTAViewModel()
        {

        }

        public FinalizeOVTAViewModel(Models.OnlandVisualTrashAssessment ovta)
        {
            AssessmentAreaName = ovta.OnlandVisualTrashAssessmentArea?.OnlandVisualTrashAssessmentAreaName ?? ovta.DraftAreaName;
            ScoreID = ovta.OnlandVisualTrashAssessmentScoreID;
            Notes = ovta.Notes;
            StormwaterJurisdictionID = ovta.StormwaterJurisdictionID;
            AssessmentAreaID = ovta.OnlandVisualTrashAssessmentAreaID;
        }

        public void UpdateModel(Models.OnlandVisualTrashAssessment onlandVisualTrashAssessment)
        {
            if (Finalize.GetValueOrDefault())
            {
                onlandVisualTrashAssessment.OnlandVisualTrashAssessmentScoreID = ScoreID;
                onlandVisualTrashAssessment.Notes = Notes;
                onlandVisualTrashAssessment.CompletedDate = DateTime.Now;

                // create the assessment area
                if (onlandVisualTrashAssessment.AssessingNewArea.GetValueOrDefault())
                {
                    var onlandVisualTrashAssessmentArea = new OnlandVisualTrashAssessmentArea(AssessmentAreaName,
                        onlandVisualTrashAssessment.StormwaterJurisdiction, onlandVisualTrashAssessment.DraftGeometry);
                    HttpRequestStorage.DatabaseEntities.SaveChanges();

                    onlandVisualTrashAssessment.OnlandVisualTrashAssessmentAreaID =
                        onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaID;
                    onlandVisualTrashAssessment.DraftGeometry = null;
                }

                onlandVisualTrashAssessment.OnlandVisualTrashAssessmentStatusID =
                    OnlandVisualTrashAssessmentStatus.Complete.OnlandVisualTrashAssessmentStatusID;
            }
            else
            {
                onlandVisualTrashAssessment.OnlandVisualTrashAssessmentScoreID = ScoreID;
                onlandVisualTrashAssessment.Notes = Notes;
                onlandVisualTrashAssessment.DraftAreaName = AssessmentAreaName;
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var assessmentAreaID = AssessmentAreaID.GetValueOrDefault();

            if (HttpRequestStorage.DatabaseEntities.OnlandVisualTrashAssessmentAreas.Where(x=>x.OnlandVisualTrashAssessmentAreaID != assessmentAreaID).Any(x => x.OnlandVisualTrashAssessmentAreaName == AssessmentAreaName && x.StormwaterJurisdictionID == StormwaterJurisdictionID))
            {
                yield return new SitkaValidationResult<FinalizeOVTAViewModel, string>(
                    "There is already an Assessment Area with this name in the selected jurisdiction. Please choose another name",
                    m => m.AssessmentAreaName);
            }
        }
    }
}