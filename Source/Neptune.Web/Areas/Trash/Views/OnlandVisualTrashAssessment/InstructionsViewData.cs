using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class InstructionsViewData : OVTASectionViewData
    {
        public InstructionsViewData(Person currentPerson, NeptunePage neptunePage, Models.OnlandVisualTrashAssessment ovta)
            : base(currentPerson, neptunePage, Models.OVTASection.Instructions, ovta)
        {

        }
    }
}
