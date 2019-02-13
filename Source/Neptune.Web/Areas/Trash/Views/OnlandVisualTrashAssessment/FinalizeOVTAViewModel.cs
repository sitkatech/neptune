using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using LtInfo.Common.DesignByContract;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class FinalizeOVTAViewModel : OnlandVisualTrashAssessmentViewModel
    {
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

        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public FinalizeOVTAViewModel()
        {

        }

        public FinalizeOVTAViewModel(Models.OnlandVisualTrashAssessment ovta)
        {
            AssessmentAreaName = ovta.OnlandVisualTrashAssessmentArea?.OnlandVisualTrashAssessmentAreaName;
        }

        public void UpdateModel(Models.OnlandVisualTrashAssessment onlandVisualTrashAssessment)
        {
            onlandVisualTrashAssessment.OnlandVisualTrashAssessmentScoreID = ScoreID;
            onlandVisualTrashAssessment.Notes = Notes;

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
    }
}