using System;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Views;

namespace Neptune.Web.Views.TreatmentBMP
{
    public class ViewTreatmentBMPModelingAttributesViewData : NeptuneViewData
    {
        public ViewTreatmentBMPModelingAttributesGridSpec GridSpec { get; }
        public string GridName { get; }
        public string GridDataUrl { get; }

        public ViewTreatmentBMPModelingAttributesViewData(Person currentPerson, Models.NeptunePage neptunePage) : base(currentPerson, neptunePage, NeptuneArea.OCStormwaterTools)
        {
            PageTitle = "Modeling Attributes";
            EntityName = $"{FieldDefinitionType.TreatmentBMP.GetFieldDefinitionLabelPluralized()}";
            GridSpec = new ViewTreatmentBMPModelingAttributesGridSpec();
            GridName = "treatmentBMPModelingAttributeGrid";
            GridDataUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(j => j.ViewTreatmentBMPModelingAttributesGridJsonData());
        }
    }
}