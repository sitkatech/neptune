using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class InitiateOVTAViewData : OVTASectionViewData
    {


        public InitiateOVTAViewData(Person currentPerson, StormwaterBreadCrumbEntity stormwaterBreadCrumbEntity, Models.OnlandVisualTrashAssessment ovta)
            : base(currentPerson, stormwaterBreadCrumbEntity, Models.OVTASection.InitiateOVTA, ovta)
        {

        }
    }
}

