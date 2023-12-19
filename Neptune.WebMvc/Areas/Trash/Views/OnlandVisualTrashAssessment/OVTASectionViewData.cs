using Neptune.EFModels.Entities;
using Neptune.WebMvc.Areas.Trash.Controllers;
using Neptune.WebMvc.Common;

namespace Neptune.WebMvc.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class OVTASectionViewData : TrashModuleViewData
    {
        public string SectionName { get; private set; }
        public Neptune.EFModels.Entities.OnlandVisualTrashAssessment? OVTA { get; private set; }
        public string OnlandVisualTrashAssessmentAreaDetailUrl { get; private set; }
        public string OnlandVisualTrashAssessmentAreaDeleteUrl { get; private set; }
        public string SectionHeader { get; private set; }
        public Neptune.EFModels.Entities.OVTASection OVTASection { get; private set; }


        public OVTASectionViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson, WebConfiguration webConfiguration, NeptunePage neptunePage, Neptune.EFModels.Entities.OVTASection ovtaSection, Neptune.EFModels.Entities.OnlandVisualTrashAssessment? ovta) : base(httpContext, linkGenerator, currentPerson, webConfiguration, neptunePage)
        {
            AssignParameters(ovtaSection, ovta);
        }

        public OVTASectionViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson, WebConfiguration webConfiguration, Neptune.EFModels.Entities.OVTASection ovtaSection, Neptune.EFModels.Entities.OnlandVisualTrashAssessment? ovta) : base(httpContext, linkGenerator, currentPerson, webConfiguration)
        {
            AssignParameters(ovtaSection, ovta);
        }

        private void AssignParameters(Neptune.EFModels.Entities.OVTASection ovtaSection, Neptune.EFModels.Entities.OnlandVisualTrashAssessment? ovta)
        {
            EntityName = "On-land Visual Trash Assessment";
            EntityUrl = SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(LinkGenerator, x => x.Index());
            OVTA = ovta;
            OVTASection = ovtaSection;
            SectionName = ovtaSection.OVTASectionName;
            SectionHeader = ovtaSection.SectionHeader;
            PageTitle = ovtaSection.OVTASectionDisplayName;
            if (ovta?.OnlandVisualTrashAssessmentAreaID != null)
            {
                OnlandVisualTrashAssessmentAreaDetailUrl = SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(LinkGenerator, x => x.Detail(ovta.OnlandVisualTrashAssessmentAreaID.Value));

                OnlandVisualTrashAssessmentAreaDeleteUrl = SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(LinkGenerator, x => x.Delete(ovta.OnlandVisualTrashAssessmentAreaID.Value));
            }
        }
    }
}