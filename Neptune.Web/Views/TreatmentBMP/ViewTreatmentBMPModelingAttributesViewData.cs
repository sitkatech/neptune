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

        public ViewTreatmentBMPModelingAttributesViewData(Person currentPerson, EFModels.Entities.NeptunePage neptunePage, LinkGenerator linkGenerator, HttpContext httpContext) : base(currentPerson, neptunePage, NeptuneArea.OCStormwaterTools, linkGenerator, httpContext)
        {
            PageTitle = "Modeling Attributes";
            EntityName = $"{FieldDefinitionType.TreatmentBMP.GetFieldDefinitionLabelPluralized()}";
            GridSpec = new ViewTreatmentBMPModelingAttributesGridSpec(linkGenerator);
            GridName = "treatmentBMPModelingAttributeGrid";
            GridDataUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(_linkGenerator, j => j.ViewTreatmentBMPModelingAttributesGridJsonData());
        }
    }
}