using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Common.MvcResults;
using Neptune.Web.Security;
using Neptune.Web.Services.Filters;
using Neptune.Web.Views.FundingEvent;
using Neptune.Web.Views.Shared;

namespace Neptune.Web.Controllers
{
    public class FundingEventController : NeptuneBaseController<FundingEventController>
    {
        public FundingEventController(NeptuneDbContext dbContext, ILogger<FundingEventController> logger, LinkGenerator linkGenerator) : base(dbContext, logger, linkGenerator)
        {
        }

        [HttpGet("{fundingEventPrimaryKey}")]
        [FundingEventManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("fundingEventPrimaryKey")]
        public PartialViewResult Edit([FromRoute] FundingEventPrimaryKey fundingEventPrimaryKey)
        {
            var fundingEvent = fundingEventPrimaryKey.EntityObject;
            
            var viewModel = new EditViewModel(fundingEvent);
            return ViewEditFundingEventFundingSources(fundingEvent, viewModel);
        }

        [HttpPost("{fundingEventPrimaryKey}")]
        [FundingEventManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("fundingEventPrimaryKey")]
        public async Task<IActionResult> Edit([FromRoute] FundingEventPrimaryKey fundingEventPrimaryKey, EditViewModel viewModel)
        {
            var fundingEvent = fundingEventPrimaryKey.EntityObject;
            
            if (!ModelState.IsValid)
            {
                return ViewEditFundingEventFundingSources(fundingEvent, viewModel);
            }

            SetMessageForDisplay($"{FieldDefinitionType.FundingEvent.GetFieldDefinitionLabel()} successfully updated.");

            return await UpdateFundingEventFundingSources(viewModel, fundingEvent);
        }
        [HttpGet("{treatmentBMPPrimaryKey}")]
        [TreatmentBMPManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        public PartialViewResult New([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var viewModel = new EditViewModel(treatmentBMP);
            return ViewNewFundingEventFundingSources(viewModel, treatmentBMP);
        }

        private PartialViewResult ViewNewFundingEventFundingSources(EditViewModel viewModel, TreatmentBMP treatmentBMP)
        {
            var allFundingSources = FundingSources.List(_dbContext).Select(x => x.AsSimpleDto()).OrderBy(p => p.FundingSourceName).ToList();
            var viewData = new EditViewData(allFundingSources, FundingEventType.All.OrderBy(x => x.FundingEventTypeName).ToList(), treatmentBMP);
            return RazorPartialView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }

        [HttpPost("{treatmentBMPPrimaryKey}")]
        [TreatmentBMPManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        public async Task<IActionResult> New([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey, EditViewModel viewModel)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewNewFundingEventFundingSources(viewModel, treatmentBMP);
            }

            var fundingEvent = new FundingEvent()
            {
                TreatmentBMPID = treatmentBMPPrimaryKey.EntityObject.TreatmentBMPID,
                FundingEventTypeID = viewModel.FundingEvent.FundingEventTypeID,
                Year = viewModel.FundingEvent.Year,
                FundingEventFundingSources = viewModel.FundingEvent.FundingEventFundingSources.Select(x => x.ToFundingEventFundingSource()).ToList(),
                Description = viewModel.FundingEvent.Description
            };

            _dbContext.FundingEvents.Add(fundingEvent);
            await _dbContext.SaveChangesAsync();

            SetMessageForDisplay($"{FieldDefinitionType.FundingEvent.GetFieldDefinitionLabel()} successfully added.");

            return new ModalDialogFormJsonResult();
        }

        private async Task<IActionResult> UpdateFundingEventFundingSources(EditViewModel viewModel,
            FundingEvent currentFundingEvent)
        {
            await _dbContext.FundingEventFundingSources.LoadAsync();
            await _dbContext.FundingEvents.LoadAsync();
            var allFundingEventFundingSources = _dbContext.FundingEventFundingSources;
            
            viewModel.UpdateModel(currentFundingEvent, allFundingEventFundingSources);
            await _dbContext.SaveChangesAsync();
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEditFundingEventFundingSources(FundingEvent fundingEvent, EditViewModel viewModel)
        {
            var allFundingSources = FundingSources.List(_dbContext).Select(x => x.AsSimpleDto()).OrderBy(p => p.FundingSourceName).ToList();
            var viewData = new EditViewData(fundingEvent, allFundingSources, FundingEventType.All.OrderBy(x => x.FundingEventTypeName).ToList());
            return RazorPartialView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }

        [HttpGet("{fundingEventPrimaryKey}")]
        [FundingEventManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("fundingEventPrimaryKey")]
        public ActionResult Delete([FromRoute] FundingEventPrimaryKey fundingEventPrimaryKey)
        {
            var fundingEvent = fundingEventPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(fundingEventPrimaryKey.PrimaryKeyValue);
            return ViewDelete(viewModel, fundingEvent);
        }

        [HttpPost("{fundingEventPrimaryKey}")]
        [FundingEventManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("fundingEventPrimaryKey")]
        public async Task<IActionResult> Delete([FromRoute] FundingEventPrimaryKey fundingEventPrimaryKey,
            ConfirmDialogFormViewModel viewModel)
        {
            var fundingEvent = fundingEventPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDelete(viewModel, fundingEvent);
            }

            fundingEvent.DeleteFull(_dbContext);
            await _dbContext.SaveChangesAsync();

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