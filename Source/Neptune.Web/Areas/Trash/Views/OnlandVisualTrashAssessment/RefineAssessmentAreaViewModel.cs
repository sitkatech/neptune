using Neptune.Web.Views.Shared;
using System.Collections.Generic;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class RefineAssessmentAreaViewModel : OnlandVisualTrashAssessmentViewModel//, IValidatableObject
    {
        public List<WktAndAnnotation> WktAndAnnotations { get; set; }

        // needed by the ModelBinder
        public RefineAssessmentAreaViewModel()
        {

        }
    }
}
