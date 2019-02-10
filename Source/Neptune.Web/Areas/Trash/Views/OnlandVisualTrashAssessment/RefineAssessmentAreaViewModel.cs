using System.Collections.Generic;
using Neptune.Web.Views.Shared;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class RefineAssessmentAreaViewModel : OnlandVisualTrashAssessmentViewModel
    {
        public List<WktAndAnnotation> WktAndAnnotations { get; set; }

        // needed by the ModelBinder
        public RefineAssessmentAreaViewModel()
        {

        }
    }
}