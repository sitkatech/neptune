using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class FinalizeOVTAViewData : OVTASectionViewData
    {


        public FinalizeOVTAViewData(Person currentPerson, StormwaterBreadCrumbEntity stormwaterBreadCrumbEntity, Models.OnlandVisualTrashAssessment ovta)
            : base(currentPerson, stormwaterBreadCrumbEntity, Models.OVTASection.FinalizeOVTA, ovta)
        {

        }
    }
}