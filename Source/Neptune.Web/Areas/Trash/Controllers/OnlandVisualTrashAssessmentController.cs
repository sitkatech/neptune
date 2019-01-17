using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.MvcResults;
using Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment;
using Neptune.Web.Controllers;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Index = Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment.Index;
using IndexViewData = Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment.IndexViewData;

namespace Neptune.Web.Areas.Trash.Controllers
{
    public class OnlandVisualTrashAssessmentController : NeptuneBaseController
    {
        [HttpGet]
        [NeptuneViewFeature]
        public ViewResult Index()
        {
            var viewData = new IndexViewData(CurrentPerson, NeptunePage.GetNeptunePageByPageType(NeptunePageType.OVTAIndex));
            return RazorView<Index, IndexViewData>(viewData);
        }

        [NeptuneViewFeature]
        public GridJsonNetJObjectResult<OnlandVisualTrashAssessment> OVTAGridJsonData()
        {
            var treatmentBMPs = GetOVTAsAndGridSpec(out var gridSpec, CurrentPerson);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<OnlandVisualTrashAssessment>(treatmentBMPs, gridSpec);
            return gridJsonNetJObjectResult;
        }

        private List<OnlandVisualTrashAssessment> GetOVTAsAndGridSpec(out OVTAIndexGridSpec gridSpec, Person currentPerson)
        {
            var showDelete = new JurisdictionManageFeature().HasPermissionByPerson(currentPerson);
            var showEdit = new JurisdictionEditFeature().HasPermissionByPerson(currentPerson);
            gridSpec = new OVTAIndexGridSpec(currentPerson, showDelete, showEdit);
            return HttpRequestStorage.DatabaseEntities.OnlandVisualTrashAssessments.ToList();
        }

        [HttpGet]
        [NeptuneViewFeature]
        public ViewResult Instructions(int? ovtaID) // route "overloaded" so we can handle revisiting and starting anew with the same route.
        {
            var viewModel = new InstructionsViewModel();

            var onlandVisualTrashAssessment = ovtaID.HasValue
                ? HttpRequestStorage.DatabaseEntities.OnlandVisualTrashAssessments.GetOnlandVisualTrashAssessment(
                    ovtaID.Value)
                : null;

            return ViewInstructions(viewModel, onlandVisualTrashAssessment);
        }

        [HttpPost]
        [NeptuneViewFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Instructions(int? ovtaID, InstructionsViewModel viewModel)
        {
            OnlandVisualTrashAssessment onlandVisualTrashAssessment;
            if (ovtaID.HasValue)
            {
                onlandVisualTrashAssessment =
                    HttpRequestStorage.DatabaseEntities.OnlandVisualTrashAssessments.GetOnlandVisualTrashAssessment(
                        ovtaID.Value);
            }
            else
            {
                onlandVisualTrashAssessment = new OnlandVisualTrashAssessment(CurrentPerson, DateTime.Now);
                HttpRequestStorage.DatabaseEntities.OnlandVisualTrashAssessments.Add(onlandVisualTrashAssessment);
                HttpRequestStorage.DatabaseEntities.SaveChanges();
            }

            return RedirectToAppropriateStep(viewModel, Models.OVTASection.Instructions, onlandVisualTrashAssessment);
        }

        private ViewResult ViewInstructions(InstructionsViewModel viewModel, OnlandVisualTrashAssessment ovta)
        {
            var viewData = new InstructionsViewData(CurrentPerson,
                NeptunePage.GetNeptunePageByPageType(NeptunePageType.OVTAInstructions),
                StormwaterBreadCrumbEntity.OnlandVisualTrashAssessment, ovta);
            return RazorView<Instructions, InstructionsViewData, InstructionsViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [NeptuneViewFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult FinalizeOVTA(OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey, FinalizeOVTAViewModel viewModel)
        {

            return Redirect(SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(x => x.Index()));
        }

        private ActionResult RedirectToAppropriateStep(OnlandVisualTrashAssessmentViewModel viewModel, Models.OVTASection ovtaSection, OnlandVisualTrashAssessment ovta)
        {
            return Redirect(viewModel.AutoAdvance
                ? ovtaSection.GetNextSection().GetSectionUrl(ovta)
                : ovtaSection.GetSectionUrl(ovta));
        }

        [HttpGet]
        [NeptuneViewFeature]
        public ViewResult RecordObservations(OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey)
        {
            return ViewRecordObservations(onlandVisualTrashAssessmentPrimaryKey, new RecordObservationsViewModel());
        }

        [HttpPost]
        [NeptuneViewFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult RecordObservations(OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey, RecordObservationsViewModel viewModel)
        {
            return RedirectToAppropriateStep(viewModel, Models.OVTASection.RecordObservations, onlandVisualTrashAssessmentPrimaryKey.EntityObject);
        }

        private ViewResult ViewRecordObservations(OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey, RecordObservationsViewModel viewModel)
        {
            var viewData = new RecordObservationsViewData(CurrentPerson, StormwaterBreadCrumbEntity.OnlandVisualTrashAssessment,
                onlandVisualTrashAssessmentPrimaryKey.EntityObject, new OVTAObservationsMapInitJson("observationsMap"));
            return RazorView<RecordObservations, RecordObservationsViewData, RecordObservationsViewModel>(viewData,
                viewModel);
        }

        [HttpGet]
        [NeptuneViewFeature]
        public ViewResult InitiateOVTA(OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey)
        {
            var viewData = new InitiateOVTAViewData(CurrentPerson, StormwaterBreadCrumbEntity.OnlandVisualTrashAssessment, onlandVisualTrashAssessmentPrimaryKey.EntityObject);
            return RazorView<InitiateOVTA, InitiateOVTAViewData, InitiateOVTAViewModel>(viewData, new InitiateOVTAViewModel());
        }

        [HttpPost]
        [NeptuneViewFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult InitiateOVTA(OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey, InitiateOVTAViewModel viewModel)
        {


            return RedirectToAppropriateStep(viewModel, Models.OVTASection.InitiateOVTA, onlandVisualTrashAssessmentPrimaryKey.EntityObject);
        }

        [HttpGet]
        [NeptuneViewFeature]
        public ViewResult FinalizeOVTA(OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey)
        {
            var viewData = new FinalizeOVTAViewData(CurrentPerson, StormwaterBreadCrumbEntity.OnlandVisualTrashAssessment, onlandVisualTrashAssessmentPrimaryKey.EntityObject);
            return RazorView<FinalizeOVTA, FinalizeOVTAViewData, FinalizeOVTAViewModel>(viewData, new FinalizeOVTAViewModel());
        }
    }
}
