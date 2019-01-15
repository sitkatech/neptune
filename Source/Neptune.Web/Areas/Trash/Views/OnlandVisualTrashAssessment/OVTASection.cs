using LtInfo.Common.Models;
using LtInfo.Common.Mvc;
using Neptune.Web.Models;
using Neptune.Web.Views;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public abstract class OVTASection :TypedWebViewPage<OVTASectionViewData, FormViewModel>
    {
        
    }

    public class OVTASectionViewData : NeptuneViewData
    {
        public string SectionName { get; }
        public string SubsectionName { get; }
        public Models.OnlandVisualTrashAssessment OVTA { get; }
        public string SectionHeader { get; }


        protected OVTASectionViewData(Person currentPerson, NeptunePage neptunePage) : base(currentPerson, neptunePage)
        {
        }
    }
}
