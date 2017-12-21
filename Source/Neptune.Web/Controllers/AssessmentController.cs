using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.MvcResults;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.TreatmentBMPAssessment;
using Index = Neptune.Web.Views.Assessment.Index;
using IndexViewData = Neptune.Web.Views.Assessment.IndexViewData;

namespace Neptune.Web.Controllers
{
    public class AssessmentController : NeptuneBaseController
    {
        [NeptuneViewFeature]
        public ViewResult Index()
        {
            var neptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.Assessment);
            var viewData = new IndexViewData(CurrentPerson, neptunePage);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [NeptuneViewFeature]
        public GridJsonNetJObjectResult<TreatmentBMPAssessment> TreatmentBMPAssessmentsGridJsonData()
        {
            var gridSpec = new TreatmentBMPAssessmentGridSpec(CurrentPerson);
            var bmpAssessments = HttpRequestStorage.DatabaseEntities.TreatmentBMPAssessments.ToList().Where(x => x.IsPublicRegularAssessment()).OrderByDescending(x => x.AssessmentDate).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<TreatmentBMPAssessment>(bmpAssessments, gridSpec);
            return gridJsonNetJObjectResult;
        }
        
    }
}