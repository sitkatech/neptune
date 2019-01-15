using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class FinalizeOVTAViewData : OVTASectionViewData
    {


        public FinalizeOVTAViewData(Person currentPerson, StormwaterBreadCrumbEntity stormwaterBreadCrumbEntity)
            : base(currentPerson, stormwaterBreadCrumbEntity, Models.OVTASection.FinalizeOVTA)
        {

        }
    }
}