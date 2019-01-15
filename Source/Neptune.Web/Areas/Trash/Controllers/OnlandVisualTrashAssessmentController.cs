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
        public ViewResult Instructions(OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey)
        {
            var viewData = new InstructionsViewData(CurrentPerson, NeptunePage.GetNeptunePageByPageType(NeptunePageType.OVTAInstructions), StormwaterBreadCrumbEntity.OnlandVisualTrashAssessment);
            return RazorView<Instructions, InstructionsViewData>(viewData);
        }

        [HttpPost]
        [NeptuneViewFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Instructions(OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey, InstructionsViewModel viewModel)
        {
            var onlandVisualTrashAssessment = new OnlandVisualTrashAssessment(CurrentPerson, DateTime.Now);

            HttpRequestStorage.DatabaseEntities.OnlandVisualTrashAssessments.Add(onlandVisualTrashAssessment);

            return RedirectToAppropriateStep(viewModel, Models.OVTASection.Instructions, onlandVisualTrashAssessment);
        }

        [HttpPost]
        [NeptuneViewFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult RecordObservations(OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey, RecordObservationsViewModel viewModel)
        {
          

            return RedirectToAppropriateStep(viewModel, Models.OVTASection.RecordObservations, onlandVisualTrashAssessmentPrimaryKey.EntityObject);
        }

        [HttpPost]
        [NeptuneViewFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult VerifyOVTAArea(OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey, VerifyOVTAAreaViewModel viewModel)
        {
          

            return RedirectToAppropriateStep(viewModel, Models.OVTASection.VerifyOVTAArea, onlandVisualTrashAssessmentPrimaryKey.EntityObject);
        }

        [HttpPost]
        [NeptuneViewFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult FinalizeOVTA(OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey, FinalizeOVTAViewModel viewModel)
        {
          

            return RedirectToAppropriateStep(viewModel, Models.OVTASection.FinalizeOVTA, onlandVisualTrashAssessmentPrimaryKey.EntityObject);
        }

        private ActionResult RedirectToAppropriateStep(OnlandVisualTrashAssessmentViewModel viewModel, Models.OVTASection ovtaSection, OnlandVisualTrashAssessment ovta)
        {
            return Redirect(viewModel.Advance
                ? ovtaSection.GetNextSection().GetSectionUrl(ovta)
                : ovtaSection.GetSectionUrl(ovta));
        }

        [HttpGet]
        [NeptuneViewFeature]
        public ViewResult RecordObservations(OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey)
        {
            var viewData = new RecordObservationsViewData(CurrentPerson, StormwaterBreadCrumbEntity.OnlandVisualTrashAssessment);
            return RazorView<RecordObservations, RecordObservationsViewData>(viewData);
        }

        [HttpGet]
        [NeptuneViewFeature]
        public ViewResult VerifyOVTAArea(OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey)
        {
            var viewData = new VerifyOVTAAreaViewData(CurrentPerson, StormwaterBreadCrumbEntity.OnlandVisualTrashAssessment);
            return RazorView<VerifyOVTAArea, VerifyOVTAAreaViewData>(viewData);
        }

        [HttpGet]
        [NeptuneViewFeature]
        public ViewResult FinalizeOVTA(OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey)
        {
            var viewData = new FinalizeOVTAViewData(CurrentPerson, StormwaterBreadCrumbEntity.OnlandVisualTrashAssessment);
            return RazorView<FinalizeOVTA, FinalizeOVTAViewData>(viewData);
        }
    }
}
