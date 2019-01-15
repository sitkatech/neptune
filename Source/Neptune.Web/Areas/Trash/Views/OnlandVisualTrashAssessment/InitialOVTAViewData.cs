using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class InitialOVTAViewData : OVTASectionViewData
    {


        public InitialOVTAViewData(Person currentPerson, StormwaterBreadCrumbEntity stormwaterBreadCrumbEntity, Models.OnlandVisualTrashAssessment ovta)
            : base(currentPerson, stormwaterBreadCrumbEntity, Models.OVTASection.InitialOVTA, ovta)
        {

        }
    }
}

