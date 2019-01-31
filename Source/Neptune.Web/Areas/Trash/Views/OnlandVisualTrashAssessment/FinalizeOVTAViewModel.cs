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

        public FinalizeOVTAViewModel()
        {

        }

        public FinalizeOVTAViewModel(Models.OnlandVisualTrashAssessment ovta)
        {
            AssessmentAreaName = ovta.OnlandVisualTrashAssessmentArea?.OnlandVisualTrashAssessmentAreaName;
        }

        public void UpdateModel(Models.OnlandVisualTrashAssessment onlandVisualTrashAssessment)
        {
            Check.Require(onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea != null, "Cannot call FinalizeOVTAViewModel.UpdateModel() if Assessment Area is null.");

            // ReSharper disable once PossibleNullReferenceException (there's no null-ref because we literally just checked)
            onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaName =
                AssessmentAreaName;
        }
    }
}