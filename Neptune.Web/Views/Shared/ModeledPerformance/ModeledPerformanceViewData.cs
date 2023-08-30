using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Controllers;

namespace Neptune.Web.Views.Shared.ModeledPerformance
{
    public class ModeledPerformanceViewData: NeptuneViewData
    {
        public EFModels.Entities.TreatmentBMP TreatmentBMP { get; }
        public string AboutModelingBMPPerformanceURL { get; }
        public string ModelingResultsUrl { get; }
        public WaterQualityManagementPlan WaterQualityManagementPlan { get; }
        public string InflowLabel { get; }

        public ModeledPerformanceViewData(EFModels.Entities.TreatmentBMP treatmentBMP, Person person, LinkGenerator linkGenerator, HttpContext httpContext): base(person, NeptuneArea.OCStormwaterTools, linkGenerator, httpContext)
        {
            TreatmentBMP = treatmentBMP;
            InflowLabel = "To BMP";
            AboutModelingBMPPerformanceURL = SitkaRoute<HomeController>.BuildUrlFromExpression(linkGenerator, x => x.AboutModelingBMPPerformance());
            ModelingResultsUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.GetModelResults(treatmentBMP));
        }

        public ModeledPerformanceViewData(WaterQualityManagementPlan waterQualityManagementPlan, Person currentPerson, LinkGenerator linkGenerator, HttpContext httpContext) : base(currentPerson, NeptuneArea.OCStormwaterTools, linkGenerator, httpContext)
        {
            WaterQualityManagementPlan = waterQualityManagementPlan;
            InflowLabel = "Site Runoff";
            AboutModelingBMPPerformanceURL =
                SitkaRoute<HomeController>.BuildUrlFromExpression(linkGenerator, x => x.AboutModelingBMPPerformance());

            ModelingResultsUrl = "";// todo: SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(x => x.GetModelResults(waterQualityManagementPlan));
        }
    }
}