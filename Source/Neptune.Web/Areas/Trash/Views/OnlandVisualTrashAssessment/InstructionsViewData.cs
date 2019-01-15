using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class InstructionsViewData : OVTASectionViewData
    {


        public InstructionsViewData(Person currentPerson, NeptunePage neptunePage, StormwaterBreadCrumbEntity stormwaterBreadCrumbEntity)
            : base(currentPerson, neptunePage, stormwaterBreadCrumbEntity, Models.OVTASection.Instructions)
        {

        }
    }
}