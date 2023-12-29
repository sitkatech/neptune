using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.MvcResults;
using Neptune.WebMvc.Security;
using Index = Neptune.WebMvc.Views.Assessment.Index;
using IndexViewData = Neptune.WebMvc.Views.Assessment.IndexViewData;
using TreatmentBMPAssessmentGridSpec = Neptune.WebMvc.Views.Assessment.TreatmentBMPAssessmentGridSpec;

namespace Neptune.WebMvc.Controllers
{
    public class AssessmentController : NeptuneBaseController<AssessmentController>
    {
        public AssessmentController(NeptuneDbContext dbContext, ILogger<AssessmentController> logger, IOptions<WebConfiguration> webConfiguration, LinkGenerator linkGenerator) : base(dbContext, logger, linkGenerator, webConfiguration)
        {
        }

        [HttpGet]
        [NeptuneViewFeature]
        public ViewResult Index()
        {
            var neptunePage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.Assessment);
            var treatmentBMPAssessmentObservationTypes = _dbContext.TreatmentBMPAssessmentObservationTypes.ToList();
            var viewData = new IndexViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, neptunePage, treatmentBMPAssessmentObservationTypes);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [HttpGet]
        [NeptuneViewFeature]
        public GridJsonNetJObjectResult<TreatmentBMPAssessmentDetailedWithObservations> TreatmentBMPAssessmentsGridJsonData()
        {
            var treatmentBMPAssessmentObservationTypes = TreatmentBMPAssessmentObservationTypes.List(_dbContext);
            var gridSpec = new TreatmentBMPAssessmentGridSpec(CurrentPerson, treatmentBMPAssessmentObservationTypes, _linkGenerator);
            var treatmentBMPObservations = _dbContext.vTreatmentBMPObservations.AsNoTracking().ToList();
            var treatmentBMPAssessmentDetaileds = vTreatmentBMPAssessmentDetaileds.ListViewableByPerson(_dbContext, CurrentPerson)
                .OrderByDescending(x => x.VisitDate).ToList();


            var treatmentBMPAssessmentDetailedWithObservations = treatmentBMPAssessmentDetaileds
                .GroupJoin(treatmentBMPObservations,
                    treatmentBMPAssessmentDetailed =>
                        treatmentBMPAssessmentDetailed.TreatmentBMPAssessmentID,
                    treatmentBMPObservation => treatmentBMPObservation.TreatmentBMPAssessmentID,
                    (treatmentBMPAssessmentDetailed, vTreatmentBMPObservations) => new TreatmentBMPAssessmentDetailedWithObservations(treatmentBMPAssessmentDetailed, vTreatmentBMPObservations)).ToList();

            var gridJsonNetJObjectResult =
                new GridJsonNetJObjectResult<TreatmentBMPAssessmentDetailedWithObservations>(treatmentBMPAssessmentDetailedWithObservations, gridSpec);
            return gridJsonNetJObjectResult;
        }
    }
}