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
            var currentFundingEventFundingSources = treatmentBMP.FundingEvents.SelectMany(x=>x.FundingEventFundingSources).ToList();
            var viewModel = new EditViewModel(currentFundingEventFundingSources);
            return ViewEditFundingEventFundingSources(treatmentBMP, viewModel);
        }

        [HttpPost]
        [TreatmentBMPManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditFundingEventFundingSourcesForTreatmentBMP(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey, EditViewModel viewModel)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var currentFundingEventFundingSources = treatmentBMP.FundingEvents.SelectMany(x => x.FundingEventFundingSources).ToList();
            if (!ModelState.IsValid)
            {
                return ViewEditFundingEventFundingSources(treatmentBMP, viewModel);
            }
            return UpdateFundingEventFundingSources(viewModel, currentFundingEventFundingSources);
        }

        [HttpGet]
        [FundingSourceEditFeature]
        public PartialViewResult EditFundingEventFundingSourcesForFundingSource(FundingSourcePrimaryKey fundingSourcePrimaryKey)
        {
            var fundingSource = fundingSourcePrimaryKey.EntityObject;
            var currentFundingEventFundingSources = fundingSource.FundingEventFundingSources.ToList();
            var viewModel = new EditViewModel(currentFundingEventFundingSources);
            return ViewEditFundingEventFundingSources(fundingSource, viewModel);
        }

        [HttpPost]
        [FundingSourceEditFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditFundingEventFundingSourcesForFundingSource(FundingSourcePrimaryKey fundingSourcePrimaryKey, EditViewModel viewModel)
        {
            var fundingSource = fundingSourcePrimaryKey.EntityObject;
            var currentFundingEventFundingSources = fundingSource.FundingEventFundingSources.ToList();
            if (!ModelState.IsValid)
            {
                return ViewEditFundingEventFundingSources(fundingSource, viewModel);
            }
            return UpdateFundingEventFundingSources(viewModel, currentFundingEventFundingSources);
        }

        private static ActionResult UpdateFundingEventFundingSources(EditViewModel viewModel,
            List<FundingEventFundingSource> currentFundingEventFundingSources)
        {
            HttpRequestStorage.DatabaseEntities.FundingEventFundingSources.Load();
            var allFundingEventFundingSources = HttpRequestStorage.DatabaseEntities.AllFundingEventFundingSources.Local;
            viewModel.UpdateModel(currentFundingEventFundingSources, allFundingEventFundingSources);


            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEditFundingEventFundingSources(FundingSource fundingSource, EditViewModel viewModel)
        {
            var allTreatmentBMPs = HttpRequestStorage.DatabaseEntities.TreatmentBMPs.ToList().Select(x => new TreatmentBMPSimple(x)).OrderBy(p => p.DisplayName).ToList();
            var viewData = new EditViewData(new FundingSourceSimple(fundingSource), allTreatmentBMPs);
            return RazorPartialView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }

        private PartialViewResult ViewEditFundingEventFundingSources(TreatmentBMP treatmentBMP, EditViewModel viewModel)
        {
            var allFundingSources = HttpRequestStorage.DatabaseEntities.FundingSources.ToList().Select(x => new FundingSourceSimple(x)).OrderBy(p => p.DisplayName).ToList();
            var viewData = new EditViewData(new TreatmentBMPSimple(treatmentBMP), allFundingSources);
            return RazorPartialView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }
    }
}