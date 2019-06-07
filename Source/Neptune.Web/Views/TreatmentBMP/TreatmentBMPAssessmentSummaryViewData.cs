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

        public TreatmentBMPAssessmentSummaryViewData(Person currentPerson, Models.NeptunePage neptunePage) : base(currentPerson, neptunePage, NeptuneArea.OCStormwaterTools)
        {
            
            PageTitle = "Recent Treatment BMP Assessments";
            EntityName = $"{Models.FieldDefinition.TreatmentBMP.GetFieldDefinitionLabelPluralized()}";
            GridSpec = new TreatmentBMPAssessmentSummaryGridSpec() { ObjectNameSingular = "Recent Treatment BMP Assessment", ObjectNamePlural = "Recent Treatment BMP Assessments", SaveFiltersInCookie = true };
            GridName = "treatmentBMPsGrid";
            GridDataUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(j => j.TreatmentBMPAssessmentSummaryGridJsonData());
        }
    }
}