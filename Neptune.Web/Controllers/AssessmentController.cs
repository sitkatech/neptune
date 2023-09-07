using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Neptune.EFModels.Entities;
using Neptune.Web.Common.MvcResults;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Index = Neptune.Web.Views.Assessment.Index;
using IndexViewData = Neptune.Web.Views.Assessment.IndexViewData;
using TreatmentBMPAssessmentGridSpec = Neptune.Web.Views.Assessment.TreatmentBMPAssessmentGridSpec;

namespace Neptune.Web.Controllers
{
    public class AssessmentController : NeptuneBaseController<AssessmentController>
    {
        public AssessmentController(NeptuneDbContext dbContext, ILogger<AssessmentController> logger, LinkGenerator linkGenerator) : base(dbContext, logger, linkGenerator)
        {
        }

        [HttpGet]
        [NeptuneViewFeature]
        public ViewResult Index()
        {
            var neptunePage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.Assessment);
            var treatmentBMPAssessmentObservationTypes = _dbContext.TreatmentBMPAssessmentObservationTypes.ToList();
            var viewData = new IndexViewData(HttpContext, _linkGenerator, CurrentPerson, neptunePage, treatmentBMPAssessmentObservationTypes);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [HttpGet]
        [NeptuneViewFeature]
        public GridJsonNetJObjectResult<TreatmentBMPAssessment> TreatmentBMPAssessmentsGridJsonData()
        {
            var stormwaterJurisdictionIDsPersonCanView = CurrentPerson.GetStormwaterJurisdictionIDsPersonCanViewWithContext(_dbContext);
            var treatmentBMPAssessmentObservationTypes = _dbContext.TreatmentBMPAssessmentObservationTypes.Include(x => x.TreatmentBMPTypeAssessmentObservationTypes).ToList();
            var gridSpec = new TreatmentBMPAssessmentGridSpec(CurrentPerson, treatmentBMPAssessmentObservationTypes, _linkGenerator);
            var bmpAssessments = _dbContext.TreatmentBMPAssessments
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