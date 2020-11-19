using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using HomeController = Neptune.Web.Controllers.HomeController;

namespace Neptune.Web.Views.Shared.ModeledPerformance
{
    public class ModeledPerformanceViewData: NeptuneViewData
    {
        public Models.TreatmentBMP TreatmentBMP { get; }
        public string AboutModelingBMPPerformanceURL { get; }
        public string ModelingResultsUrl { get; }
        public Models.WaterQualityManagementPlan WaterQualityManagementPlan { get; }

        public ModeledPerformanceViewData(Models.TreatmentBMP treatmentBMP, Person person): base(person, NeptuneArea.OCStormwaterTools)
        {
            TreatmentBMP = treatmentBMP;
            AboutModelingBMPPerformanceURL =
                SitkaRoute<HomeController>.BuildUrlFromExpression(x => x.AboutModelingBMPPerformance());

            ModelingResultsUrl =
                SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(x =>
                    x.GetModelResults(treatmentBMP));
        }

        public ModeledPerformanceViewData(Models.WaterQualityManagementPlan waterQualityManagementPlan, Person currentPerson) : base(currentPerson, NeptuneArea.OCStormwaterTools)
        {
            WaterQualityManagementPlan = waterQualityManagementPlan;
            AboutModelingBMPPerformanceURL =
                SitkaRoute<HomeController>.BuildUrlFromExpression(x => x.AboutModelingBMPPerformance());

            ModelingResultsUrl =
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(x =>
                    x.GetModelResults(waterQualityManagementPlan));
        }
    }
}