using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class RefineAssessmentAreaViewData : OVTASectionViewData
    {
        public RefineAssessmentAreaViewData(Person currentPerson, Models.OVTASection ovtaSection, Models.OnlandVisualTrashAssessment ovta) : base(currentPerson, ovtaSection, ovta)
        {
        }
    }
}