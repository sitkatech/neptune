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
        public string SectionName { get; }
        public string SubsectionName { get; }
        public Models.OnlandVisualTrashAssessment OVTA { get; }
        public string SectionHeader { get; }


        public OVTASectionViewData(Person currentPerson, NeptunePage neptunePage, StormwaterBreadCrumbEntity stormwaterBreadCrumbEntity, Models.OVTASection ovtaSection) : base(currentPerson, stormwaterBreadCrumbEntity, neptunePage)
        {
            EntityName = "Onland Visual Trash Assessment";
            EntityUrl = SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(x => x.Index());
            
            SectionName = ovtaSection.OVTASectionName;
            PageTitle = ovtaSection.OVTASectionDisplayName;
            OVTA = Models.OnlandVisualTrashAssessment.CreateNewBlank(currentPerson);
        }

        public OVTASectionViewData(Person currentPerson, StormwaterBreadCrumbEntity stormwaterBreadCrumbEntity, Models.OVTASection ovtaSection) : base(currentPerson, stormwaterBreadCrumbEntity)
        {
            EntityName = "Onland Visual Trash Assessment";
            EntityUrl = SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(x => x.Index());
            
            SectionName = ovtaSection.OVTASectionName;
            PageTitle = ovtaSection.OVTASectionDisplayName;
            OVTA = Models.OnlandVisualTrashAssessment.CreateNewBlank(currentPerson);

        }
    }
}
