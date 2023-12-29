using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Models;

namespace Neptune.WebMvc.Views.TreatmentBMP
{
    public class ViewTreatmentBMPModelingAttributesViewData : NeptuneViewData
    {
        public ViewTreatmentBMPModelingAttributesGridSpec GridSpec { get; }
        public string GridName { get; }
        public string GridDataUrl { get; }

        public ViewTreatmentBMPModelingAttributesViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson, EFModels.Entities.NeptunePage neptunePage) : base(httpContext, linkGenerator, currentPerson, neptunePage, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            PageTitle = "Modeling Attributes";
            EntityName = $"{FieldDefinitionType.TreatmentBMP.GetFieldDefinitionLabelPluralized()}";
            GridSpec = new ViewTreatmentBMPModelingAttributesGridSpec(linkGenerator, new Dictionary<int, EFModels.Entities.Delineation?>(), new Dictionary<int, string?>(), new Dictionary<int, double>());
            GridName = "treatmentBMPModelingAttributeGrid";
            GridDataUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(LinkGenerator, x => x.ViewTreatmentBMPModelingAttributesGridJsonData());
        }
    }
}