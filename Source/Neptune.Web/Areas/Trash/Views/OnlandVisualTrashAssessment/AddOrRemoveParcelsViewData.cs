using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class AddOrRemoveParcelsViewData: OVTASectionViewData
    {
        public AddOrRemoveParcelsViewData(Person currentPerson, Models.OVTASection ovtaSection, Models.OnlandVisualTrashAssessment ovta) : base(currentPerson, ovtaSection, ovta)
        {
        }
    }
}