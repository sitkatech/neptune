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
        public EFModels.Entities.WaterQualityManagementPlan WaterQualityManagementPlan { get; }
        public string InflowLabel { get; }

        public ModeledPerformanceViewData(HttpContext httpContext, LinkGenerator linkGenerator,
            EFModels.Entities.TreatmentBMP treatmentBMP, Person person): base(httpContext, linkGenerator, person, NeptuneArea.OCStormwaterTools)
        {
            TreatmentBMP = treatmentBMP;
            InflowLabel = "To BMP";
            AboutModelingBMPPerformanceURL = SitkaRoute<HomeController>.BuildUrlFromExpression(linkGenerator, x => x.AboutModelingBMPPerformance());
            ModelingResultsUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.GetModelResults(treatmentBMP));
        }

        public ModeledPerformanceViewData(HttpContext httpContext, LinkGenerator linkGenerator,
            EFModels.Entities.WaterQualityManagementPlan waterQualityManagementPlan, Person currentPerson) : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools)
        {
            WaterQualityManagementPlan = waterQualityManagementPlan;
            InflowLabel = "Site Runoff";
            AboutModelingBMPPerformanceURL =
                SitkaRoute<HomeController>.BuildUrlFromExpression(LinkGenerator, x => x.AboutModelingBMPPerformance());

            ModelingResultsUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator, x => x.GetModelResults(waterQualityManagementPlan));
        }
    }
}