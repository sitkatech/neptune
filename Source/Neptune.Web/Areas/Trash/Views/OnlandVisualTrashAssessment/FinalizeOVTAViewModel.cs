using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using LtInfo.Common.DesignByContract;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class FinalizeOVTAViewModel : OnlandVisualTrashAssessmentViewModel
    {
        [Required]
        [StringLength(Models.OnlandVisualTrashAssessmentArea.FieldLengths.OnlandVisualTrashAssessmentAreaName)]
        [DisplayName("Assessment Area Name")]
        public string AssessmentAreaName { get; set; }

        [StringLength(Models.OnlandVisualTrashAssessment.FieldLengths.Notes)]
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
            throw new NotImplementedException();
        }
    }
}