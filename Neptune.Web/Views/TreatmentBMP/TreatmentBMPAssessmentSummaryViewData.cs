using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMP
{
    public class TreatmentBMPAssessmentSummaryViewData : NeptuneViewData
    {
        public string GridDataUrl { get; }
        public string GridName { get; }
        public TreatmentBMPAssessmentSummaryGridSpec GridSpec { get; }

        public TreatmentBMPAssessmentSummaryViewData(HttpContext httpContext, LinkGenerator linkGenerator,
            Person currentPerson, NeptunePage neptunePage) : base(httpContext, linkGenerator, currentPerson, neptunePage, NeptuneArea.OCStormwaterTools)
        {
            
            PageTitle = "Recent Treatment BMP Assessments";
            EntityName = $"{FieldDefinitionType.TreatmentBMP.GetFieldDefinitionLabelPluralized()}";
            GridSpec = new TreatmentBMPAssessmentSummaryGridSpec(linkGenerator) { ObjectNameSingular = "Recent Treatment BMP Assessment", ObjectNamePlural = "Recent Treatment BMP Assessments", SaveFiltersInCookie = true };
            GridName = "treatmentBMPsGrid";
            GridDataUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(LinkGenerator , x => x.TreatmentBMPAssessmentSummaryGridJsonData());
        }
    }
}