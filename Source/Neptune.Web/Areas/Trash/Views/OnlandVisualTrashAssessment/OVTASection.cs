using LtInfo.Common.Models;
using LtInfo.Common.Mvc;
using Neptune.Web.Areas.Trash.Controllers;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Views;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public abstract class OVTASection :TypedWebViewPage<OVTASectionViewData, FormViewModel>
    {
        
    }

    public class OVTASectionViewData : NeptuneViewData
    {
        public string SectionName { get; private set; }
        public string SubsectionName { get; }
        public Models.OnlandVisualTrashAssessment OVTA { get; private set; }
        public string SectionHeader { get; private set; }


        public OVTASectionViewData(Person currentPerson, NeptunePage neptunePage, StormwaterBreadCrumbEntity stormwaterBreadCrumbEntity, Models.OVTASection ovtaSection, Models.OnlandVisualTrashAssessment ovta) : base(currentPerson, stormwaterBreadCrumbEntity, neptunePage)
        {
            AssignParameters(currentPerson, ovtaSection, ovta);
        }

        public OVTASectionViewData(Person currentPerson, StormwaterBreadCrumbEntity stormwaterBreadCrumbEntity, Models.OVTASection ovtaSection, Models.OnlandVisualTrashAssessment ovta) : base(currentPerson, stormwaterBreadCrumbEntity)
        {
            AssignParameters(currentPerson, ovtaSection, ovta);
        }

        private void AssignParameters(Person currentPerson, Models.OVTASection ovtaSection, Models.OnlandVisualTrashAssessment ovta)
        {
            EntityName = "On-land Visual Trash Assessment";
            EntityUrl = SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(x => x.Index());
            OVTA = ovta;
            SectionName = ovtaSection.OVTASectionName;
            SectionHeader = ovtaSection.SectionHeader;
            PageTitle = ovtaSection.OVTASectionDisplayName;
        }
    }
}
