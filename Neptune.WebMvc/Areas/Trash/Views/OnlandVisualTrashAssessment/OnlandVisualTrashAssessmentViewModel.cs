using Neptune.WebMvc.Common.Models;

namespace Neptune.WebMvc.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class OnlandVisualTrashAssessmentViewModel : FormViewModel
    {

        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public OnlandVisualTrashAssessmentViewModel()
        {
        }

        protected OnlandVisualTrashAssessmentViewModel(Neptune.EFModels.Entities.OnlandVisualTrashAssessment onlandVisualTrashAssessment)
        {
            OVTAID = onlandVisualTrashAssessment.OnlandVisualTrashAssessmentID;
        }

        public int? OVTAID { get; set; }
    }
}