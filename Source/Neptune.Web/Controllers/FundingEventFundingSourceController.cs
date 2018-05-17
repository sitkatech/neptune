using System.Collections.Generic;
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
        public PartialViewResult EditFundingEventFundingSourcesForTreatmentBMP(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var currentFundingEvents = treatmentBMP.FundingEvents.ToList();
            var viewModel = new EditViewModel(currentFundingEvents);
            return ViewEditFundingEventFundingSources(treatmentBMP, viewModel);
        }

        [HttpPost]
        [TreatmentBMPManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditFundingEventFundingSourcesForTreatmentBMP(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey, EditViewModel viewModel)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var currentFundingEvents = treatmentBMP.FundingEvents.ToList();
            if (!ModelState.IsValid)
            {
                return ViewEditFundingEventFundingSources(treatmentBMP, viewModel);
            }
            return UpdateFundingEventFundingSources(viewModel, currentFundingEvents);
        }

        [HttpGet]
        [FundingSourceEditFeature]
        public PartialViewResult EditFundingEventFundingSourcesForFundingSource(FundingSourcePrimaryKey fundingSourcePrimaryKey)
        {
            var fundingSource = fundingSourcePrimaryKey.EntityObject;
            var currentFundingEvents = fundingSource.FundingEventFundingSources.Select(x=>x.FundingEvent).Distinct().ToList();
            var viewModel = new EditViewModel(currentFundingEvents);
            return ViewEditFundingEventFundingSources(fundingSource, viewModel);
        }

        [HttpPost]
        [FundingSourceEditFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditFundingEventFundingSourcesForFundingSource(FundingSourcePrimaryKey fundingSourcePrimaryKey, EditViewModel viewModel)
        {
            var fundingSource = fundingSourcePrimaryKey.EntityObject;
            var currentFundingEvents = fundingSource.FundingEventFundingSources.Select(x=>x.FundingEvent).Distinct().ToList();
            if (!ModelState.IsValid)
            {
                return ViewEditFundingEventFundingSources(fundingSource, viewModel);
            }
            return UpdateFundingEventFundingSources(viewModel, currentFundingEvents);
        }

        private static ActionResult UpdateFundingEventFundingSources(EditViewModel viewModel,
            List<FundingEvent> currentFundingEvents)
        {
            HttpRequestStorage.DatabaseEntities.FundingEventFundingSources.Load();
            HttpRequestStorage.DatabaseEntities.FundingEvents.Load();
            var allFundingEventFundingSources = HttpRequestStorage.DatabaseEntities.AllFundingEventFundingSources.Local;
            var allFundingEvents = HttpRequestStorage.DatabaseEntities.AllFundingEvents.Local;
            viewModel.UpdateModel(currentFundingEvents, allFundingEvents, allFundingEventFundingSources);


            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEditFundingEventFundingSources(FundingSource fundingSource, EditViewModel viewModel)
        {
            var allTreatmentBMPs = HttpRequestStorage.DatabaseEntities.TreatmentBMPs.ToList().Select(x => new TreatmentBMPSimple(x)).OrderBy(p => p.DisplayName).ToList();
            var viewData = new EditViewData(new FundingSourceSimple(fundingSource), allTreatmentBMPs, FundingEventType.All);
            return RazorPartialView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }

        private PartialViewResult ViewEditFundingEventFundingSources(TreatmentBMP treatmentBMP, EditViewModel viewModel)
        {
            var allFundingSources = HttpRequestStorage.DatabaseEntities.FundingSources.ToList().Select(x => new FundingSourceSimple(x)).OrderBy(p => p.DisplayName).ToList();
            var viewData = new EditViewData(new TreatmentBMPSimple(treatmentBMP), allFundingSources, FundingEventType.All);
            return RazorPartialView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }
    }
}