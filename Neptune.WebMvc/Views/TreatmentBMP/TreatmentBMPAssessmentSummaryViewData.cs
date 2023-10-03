using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Models;

namespace Neptune.WebMvc.Views.TreatmentBMP
{
    public class TreatmentBMPAssessmentSummaryViewData : NeptuneViewData
    {
        public string GridDataUrl { get; }
        public string GridName { get; }
        public TreatmentBMPAssessmentSummaryGridSpec GridSpec { get; }

        public TreatmentBMPAssessmentSummaryViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson, EFModels.Entities.NeptunePage neptunePage) : base(httpContext, linkGenerator, currentPerson, neptunePage, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            
            PageTitle = "Recent Treatment BMP Assessments";
            EntityName = $"{FieldDefinitionType.TreatmentBMP.GetFieldDefinitionLabelPluralized()}";
            GridSpec = new TreatmentBMPAssessmentSummaryGridSpec(linkGenerator) { ObjectNameSingular = "Recent Treatment BMP Assessment", ObjectNamePlural = "Recent Treatment BMP Assessments", SaveFiltersInCookie = true };
            GridName = "treatmentBMPsGrid";
            GridDataUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(LinkGenerator , x => x.TreatmentBMPAssessmentSummaryGridJsonData());
        }
    }
}