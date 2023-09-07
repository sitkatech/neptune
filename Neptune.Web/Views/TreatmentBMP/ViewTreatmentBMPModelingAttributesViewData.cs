using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMP
{
    public class ViewTreatmentBMPModelingAttributesViewData : NeptuneViewData
    {
        public ViewTreatmentBMPModelingAttributesGridSpec GridSpec { get; }
        public string GridName { get; }
        public string GridDataUrl { get; }

        public ViewTreatmentBMPModelingAttributesViewData(HttpContext httpContext, LinkGenerator linkGenerator,
            Person currentPerson, NeptunePage neptunePage) : base(httpContext, linkGenerator, currentPerson, neptunePage, NeptuneArea.OCStormwaterTools)
        {
            PageTitle = "Modeling Attributes";
            EntityName = $"{FieldDefinitionType.TreatmentBMP.GetFieldDefinitionLabelPluralized()}";
            GridSpec = new ViewTreatmentBMPModelingAttributesGridSpec(linkGenerator);
            GridName = "treatmentBMPModelingAttributeGrid";
            GridDataUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(LinkGenerator, x => x.ViewTreatmentBMPModelingAttributesGridJsonData());
        }
    }
}