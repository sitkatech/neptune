using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class VerifyOVTAAreaViewData : OVTASectionViewData
    {


        public VerifyOVTAAreaViewData(Person currentPerson, StormwaterBreadCrumbEntity stormwaterBreadCrumbEntity)
            : base(currentPerson, stormwaterBreadCrumbEntity, Models.OVTASection.VerifyOVTAArea)
        {

        }
    }
}