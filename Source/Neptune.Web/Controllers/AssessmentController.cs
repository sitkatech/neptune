using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using LtInfo.Common.MvcResults;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Index = Neptune.Web.Views.Assessment.Index;
using IndexViewData = Neptune.Web.Views.Assessment.IndexViewData;
using TreatmentBMPAssessmentGridSpec = Neptune.Web.Views.Assessment.TreatmentBMPAssessmentGridSpec;

namespace Neptune.Web.Controllers
{
    public class AssessmentController : NeptuneBaseController
    {
        [NeptuneViewFeature]
        public ViewResult Index()
        {
            var neptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.Assessment);
            var viewData = new IndexViewData(CurrentPerson, neptunePage, HttpRequestStorage.DatabaseEntities.TreatmentBMPAssessmentObservationTypes);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [NeptuneViewFeature]
        public GridJsonNetJObjectResult<TreatmentBMPAssessment> TreatmentBMPAssessmentsGridJsonData()
        {
            var stormwaterJurisdictionIDsPersonCanView = CurrentPerson.GetStormwaterJurisdictionIDsPersonCanView();
            var gridSpec = new TreatmentBMPAssessmentGridSpec(CurrentPerson, HttpRequestStorage.DatabaseEntities.TreatmentBMPAssessmentObservationTypes);
            var bmpAssessments = HttpRequestStorage.DatabaseEntities.TreatmentBMPAssessments
                .Include(x => x.FieldVisit.PerformedByPerson)
                .Include(x => x.TreatmentBMP)
                .Include(x => x.TreatmentBMP.TreatmentBMPBenchmarkAndThresholds)
                .Include(x => x.TreatmentBMPType)
                .Include(x => x.TreatmentBMPType.TreatmentBMPTypeAssessmentObservationTypes)
                .Include(x => x.TreatmentBMPObservations)
                .Include(x => x.TreatmentBMPObservations.Select(y => y.TreatmentBMPAssessmentObservationType))
                .Where(x => stormwaterJurisdictionIDsPersonCanView.Contains(x.TreatmentBMP.StormwaterJurisdictionID)).ToList()
                .OrderByDescending(x => x.GetAssessmentDate()).ToList();
            var gridJsonNetJObjectResult =
                new GridJsonNetJObjectResult<TreatmentBMPAssessment>(bmpAssessments, gridSpec);
            return gridJsonNetJObjectResult;
        }
    }
}