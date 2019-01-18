using LtInfo.Common.Models;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class OnlandVisualTrashAssessmentViewModel : FormViewModel
    {

        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public OnlandVisualTrashAssessmentViewModel()
        {
        }

        protected OnlandVisualTrashAssessmentViewModel(Models.OnlandVisualTrashAssessment ovta)
        {
            OVTAID = ovta.OnlandVisualTrashAssessmentID;
        }

        public int? OVTAID { get; set; }
    }
}