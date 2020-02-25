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
            var treatmentBMPAssessmentObservationTypes = HttpRequestStorage.DatabaseEntities.TreatmentBMPAssessmentObservationTypes.ToList();
            var viewData = new IndexViewData(CurrentPerson, neptunePage, treatmentBMPAssessmentObservationTypes);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [NeptuneViewFeature]
        public GridJsonNetJObjectResult<TreatmentBMPAssessment> TreatmentBMPAssessmentsGridJsonData()
        {
            var stormwaterJurisdictionIDsPersonCanView = CurrentPerson.GetStormwaterJurisdictionIDsPersonCanView();
            var treatmentBMPAssessmentObservationTypes = HttpRequestStorage.DatabaseEntities.TreatmentBMPAssessmentObservationTypes.Include(x => x.TreatmentBMPTypeAssessmentObservationTypes).ToList();
            var gridSpec = new TreatmentBMPAssessmentGridSpec(CurrentPerson, treatmentBMPAssessmentObservationTypes);
            var bmpAssessments = HttpRequestStorage.DatabaseEntities.TreatmentBMPAssessments
                .Include(x => x.FieldVisit)
                .Include(x => x.FieldVisit.PerformedByPerson)
                .Include(x => x.TreatmentBMP)
                .Include(x => x.TreatmentBMP.StormwaterJurisdiction)
                .Include(x => x.TreatmentBMP.StormwaterJurisdiction.Organization)
                .Include(x => x.TreatmentBMPType)
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