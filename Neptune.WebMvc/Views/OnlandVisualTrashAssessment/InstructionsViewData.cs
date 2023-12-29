using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;

namespace Neptune.WebMvc.Views.OnlandVisualTrashAssessment
{
    public class InstructionsViewData : OVTASectionViewData
    {
        public InstructionsViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson, WebConfiguration webConfiguration, EFModels.Entities.NeptunePage neptunePage, EFModels.Entities.OnlandVisualTrashAssessment ovta)
            : base(httpContext, linkGenerator, currentPerson, webConfiguration, neptunePage, EFModels.Entities.OVTASection.Instructions, ovta)
        {

        }
    }
}
