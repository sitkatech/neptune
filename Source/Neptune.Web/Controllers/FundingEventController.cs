using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common;
using LtInfo.Common.MvcResults;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.FundingEvent;
using Neptune.Web.Views.Shared;

namespace Neptune.Web.Controllers
{
    public class FundingEventController : NeptuneBaseController
    {
        [HttpGet]
        [FundingEventManageFeature]
        public PartialViewResult EditFundingEvent(FundingEventPrimaryKey fundingEventPrimaryKey)
        {
            var fundingEvent = fundingEventPrimaryKey.EntityObject;
            
            var viewModel = new EditViewModel(fundingEvent);
            return ViewEditFundingEventFundingSources(fundingEvent, viewModel);
        }

        [HttpPost]
        [FundingEventManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditFundingEvent(FundingEventPrimaryKey fundingEventPrimaryKey, EditViewModel viewModel)
        {
            var fundingEvent = fundingEventPrimaryKey.EntityObject;
            
            if (!ModelState.IsValid)
            {
                return ViewEditFundingEventFundingSources(fundingEvent, viewModel);
            }

            SetMessageForDisplay($"{FieldDefinitionType.FundingEvent.GetFieldDefinitionLabel()} successfully updated.");

            return UpdateFundingEventFundingSources(viewModel, fundingEvent);
        }
        [HttpGet]
        [TreatmentBMPManageFeature]
        public PartialViewResult NewFundingEvent(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var viewModel = new EditViewModel(treatmentBMP);
            return ViewNewFundingEventFundingSources(viewModel, treatmentBMP);
        }

        private PartialViewResult ViewNewFundingEventFundingSources(EditViewModel viewModel, TreatmentBMP treatmentBMP)
        {
            var allFundingSources = HttpRequestStorage.DatabaseEntities.FundingSources.ToList().Select(x => new FundingSourceSimple(x)).OrderBy(p => p.DisplayName).ToList();
            var viewData = new EditViewData(allFundingSources, FundingEventType.All.OrderBy(x => x.SortOrder).ToList(), treatmentBMP);
            return RazorPartialView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [TreatmentBMPManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult NewFundingEvent(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey, EditViewModel viewModel)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewNewFundingEventFundingSources(viewModel, treatmentBMP);
            }

            var fundingEvent = new FundingEvent(treatmentBMPPrimaryKey.EntityObject.TreatmentBMPID,
                viewModel.FundingEvent.FundingEventTypeID, viewModel.FundingEvent.Year)
            {
                FundingEventFundingSources = viewModel.FundingEvent.FundingEventFundingSources?
                    .Select(x => x.ToFundingEventFundingSource()).ToList(),
                Description = viewModel.FundingEvent.Description
            };

            HttpRequestStorage.DatabaseEntities.FundingEvents.Add(fundingEvent);
            HttpRequestStorage.DatabaseEntities.SaveChanges();

            SetMessageForDisplay($"{FieldDefinitionType.FundingEvent.GetFieldDefinitionLabel()} successfully added.");

            return new ModalDialogFormJsonResult();
        }

        private static ActionResult UpdateFundingEventFundingSources(EditViewModel viewModel,
            FundingEvent currentFundingEvent)
        {
            HttpRequestStorage.DatabaseEntities.FundingEventFundingSources.Load();
            HttpRequestStorage.DatabaseEntities.FundingEvents.Load();
            var allFundingEventFundingSources = HttpRequestStorage.DatabaseEntities.FundingEventFundingSources.Local;
            
            viewModel.UpdateModel(currentFundingEvent, allFundingEventFundingSources);
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEditFundingEventFundingSources(FundingEvent fundingEvent, EditViewModel viewModel)
        {
            var allFundingSources = HttpRequestStorage.DatabaseEntities.FundingSources.ToList().Select(x => new FundingSourceSimple(x)).OrderBy(p => p.DisplayName).ToList();
            var viewData = new EditViewData(fundingEvent, allFundingSources, FundingEventType.All.OrderBy(x => x.SortOrder).ToList());
            return RazorPartialView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [FundingEventManageFeature]
        public ActionResult DeleteFundingEvent(FundingEventPrimaryKey fundingEventPrimaryKey)
        {
            var fundingEvent = fundingEventPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(fundingEventPrimaryKey.PrimaryKeyValue);
            return ViewDelete(viewModel, fundingEvent);
        }

        [HttpPost]
        [FundingEventManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteFundingEvent(FundingEventPrimaryKey fundingEventPrimaryKey,
            ConfirmDialogFormViewModel viewModel)
        {
            var fundingEvent = fundingEventPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDelete(viewModel, fundingEvent);
            }

            fundingEvent.DeleteFull(HttpRequestStorage.DatabaseEntities);
            HttpRequestStorage.DatabaseEntities.SaveChanges();

            SetMessageForDisplay($"{FieldDefinitionType.FundingEvent.GetFieldDefinitionLabel()} successfully deleted");
            return new ModalDialogFormJsonResult();
        }

        private ActionResult ViewDelete(ConfirmDialogFormViewModel viewModel, FundingEvent fundingEvent)
        {
            var confirmMessage =
                $"Are you sure you want to delete this {FieldDefinitionType.FundingEvent.GetFieldDefinitionLabel()}? This will remove {fundingEvent.FundingEventFundingSources.Sum(x=>x.Amount).ToStringCurrency()} of expenditures from this BMP.";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData,
                viewModel);
        }
    }
}