using Neptune.Web.Areas.Modeling.Controllers;
using Neptune.Web.Common;
using Neptune.Web.Models;
using HomeController = Neptune.Web.Controllers.HomeController;

namespace Neptune.Web.Views.TreatmentBMP
{
    public class ModeledBMPPerformanceViewData: NeptuneViewData
    {
        public Models.TreatmentBMP TreatmentBMP { get; }
        public string AboutModelingBMPPerformanceURL { get; }
        public string ModelingResultsUrl { get; }

        public ModeledBMPPerformanceViewData(Models.TreatmentBMP treatmentBMP, Models.Person person): base(person, NeptuneArea.OCStormwaterTools)
        {
            TreatmentBMP = treatmentBMP;
            AboutModelingBMPPerformanceURL =
                SitkaRoute<HomeController>.BuildUrlFromExpression(x => x.AboutModelingBMPPerformance());

            ModelingResultsUrl =
                SitkaRoute<NereidController>.BuildUrlFromExpression(x =>
                    x.GetTreatmentBMPResult(treatmentBMP.TreatmentBMPID));
        }
    }
}