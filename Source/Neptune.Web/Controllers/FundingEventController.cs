using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.MvcResults;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.FundingEvent;

namespace Neptune.Web.Controllers
{
    public class FundingEventController : NeptuneBaseController
    {
        [HttpGet]
        [TreatmentBMPManageFeature]
        public PartialViewResult EditFundingEventsForTreatmentBMP(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var currentFundingEvents = treatmentBMP.FundingEvents.ToList();
            var viewModel = new EditViewModel(currentFundingEvents);
            return ViewEditFundingEvents(treatmentBMP, viewModel);
        }

        [HttpPost]
        [TreatmentBMPManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditFundingEventsForTreatmentBMP(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey, EditViewModel viewModel)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var currentFundingEvents = treatmentBMP.FundingEvents.ToList();
            if (!ModelState.IsValid)
            {
                return ViewEditFundingEvents(treatmentBMP, viewModel);
            }
            return UpdateFundingEvents(viewModel, currentFundingEvents);
        }

        [HttpGet]
        [FundingSourceEditFeature]
        public PartialViewResult EditFundingEventsForFundingSource(FundingSourcePrimaryKey fundingSourcePrimaryKey)
        {
            var fundingSource = fundingSourcePrimaryKey.EntityObject;
            var currentFundingEvents = fundingSource.FundingEvents.ToList();
            var viewModel = new EditViewModel(currentFundingEvents);
            return ViewEditFundingEvents(fundingSource, viewModel);
        }

        [HttpPost]
        [FundingSourceEditFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditFundingEventsForFundingSource(FundingSourcePrimaryKey fundingSourcePrimaryKey, EditViewModel viewModel)
        {
            var fundingSource = fundingSourcePrimaryKey.EntityObject;
            var currentFundingEvents = fundingSource.FundingEvents.ToList();
            if (!ModelState.IsValid)
            {
                return ViewEditFundingEvents(fundingSource, viewModel);
            }
            return UpdateFundingEvents(viewModel, currentFundingEvents);
        }

        private static ActionResult UpdateFundingEvents(EditViewModel viewModel,
            List<FundingEvent> currentFundingEvents)
        {
            HttpRequestStorage.DatabaseEntities.FundingEvents.Load();
            var allFundingEvents = HttpRequestStorage.DatabaseEntities.AllFundingEvents.Local;
            viewModel.UpdateModel(currentFundingEvents, allFundingEvents);


            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEditFundingEvents(FundingSource fundingSource, EditViewModel viewModel)
        {
            var allTreatmentBMPs = HttpRequestStorage.DatabaseEntities.TreatmentBMPs.ToList().Select(x => new TreatmentBMPSimple(x)).OrderBy(p => p.DisplayName).ToList();
            var viewData = new EditViewData(new FundingSourceSimple(fundingSource), allTreatmentBMPs);
            return RazorPartialView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }

        private PartialViewResult ViewEditFundingEvents(TreatmentBMP treatmentBMP, EditViewModel viewModel)
        {
            var allFundingSources = HttpRequestStorage.DatabaseEntities.FundingSources.ToList().Select(x => new FundingSourceSimple(x)).OrderBy(p => p.DisplayName).ToList();
            var viewData = new EditViewData(new TreatmentBMPSimple(treatmentBMP), allFundingSources);
            return RazorPartialView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }
    }
}