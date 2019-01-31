using Neptune.Web.Areas.Trash.Controllers;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class OVTASectionViewData : TrashModuleViewData
    {
        public string SectionName { get; private set; }
        public Models.OnlandVisualTrashAssessment OVTA { get; private set; }
        public string SectionHeader { get; private set; }


        public OVTASectionViewData(Person currentPerson, NeptunePage neptunePage, Models.OVTASection ovtaSection, Models.OnlandVisualTrashAssessment ovta) : base(currentPerson, neptunePage)
        {
            AssignParameters(ovtaSection, ovta);
        }

        public OVTASectionViewData(Person currentPerson, Models.OVTASection ovtaSection, Models.OnlandVisualTrashAssessment ovta) : base(currentPerson)
        {
            AssignParameters(ovtaSection, ovta);
        }

        private void AssignParameters(Models.OVTASection ovtaSection, Models.OnlandVisualTrashAssessment ovta)
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