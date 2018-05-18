using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.MvcResults;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.FundingEventFundingSource;

namespace Neptune.Web.Controllers
{
    public class FundingEventFundingSourceController : NeptuneBaseController
    {
        [HttpGet]
        [TreatmentBMPManageFeature]
        public PartialViewResult EditFundingEvent(FundingEventPrimaryKey fundingEventPrimaryKey)
        {
            var fundingEvent = fundingEventPrimaryKey.EntityObject;
            
            var viewModel = new EditViewModel(fundingEvent);
            return ViewEditFundingEventFundingSources(fundingEvent, viewModel);
        }

        [HttpPost]
        [TreatmentBMPManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditFundingEvent(FundingEventPrimaryKey fundingEventPrimaryKey, EditViewModel viewModel)
        {
            var fundingEvent = fundingEventPrimaryKey.EntityObject;
            
            if (!ModelState.IsValid)
            {
                return ViewEditFundingEventFundingSources(fundingEvent, viewModel);
            }
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
            var viewData = new EditViewData(allFundingSources, FundingEventType.All, treatmentBMP);
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
                viewModel.FundingEvent.FundingEventTypeID, viewModel.FundingEvent.FundingEventTypeID)
            {
                FundingEventFundingSources = viewModel.FundingEvent.FundingEventFundingSources
                    .Select(x => x.ToFundingEventFundingSource()).ToList()
            };

            HttpRequestStorage.DatabaseEntities.AllFundingEvents.Add(fundingEvent);
            HttpRequestStorage.DatabaseEntities.SaveChanges();
            return new ModalDialogFormJsonResult();
        }

        private static ActionResult UpdateFundingEventFundingSources(EditViewModel viewModel,
            FundingEvent currentFundingEvent)
        {
            HttpRequestStorage.DatabaseEntities.FundingEventFundingSources.Load();
            HttpRequestStorage.DatabaseEntities.FundingEvents.Load();
            var allFundingEventFundingSources = HttpRequestStorage.DatabaseEntities.AllFundingEventFundingSources.Local;
            
            viewModel.UpdateModel(currentFundingEvent, allFundingEventFundingSources);
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEditFundingEventFundingSources(FundingEvent fundingEvent, EditViewModel viewModel)
        {
            var allFundingSources = HttpRequestStorage.DatabaseEntities.FundingSources.ToList().Select(x => new FundingSourceSimple(x)).OrderBy(p => p.DisplayName).ToList();
            var viewData = new EditViewData(fundingEvent, allFundingSources, FundingEventType.All);
            return RazorPartialView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }
    }
}