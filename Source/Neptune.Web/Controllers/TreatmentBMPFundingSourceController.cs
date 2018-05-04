using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.MvcResults;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.TreatmentBMPFundingSource;

namespace Neptune.Web.Controllers
{
    public class TreatmentBMPFundingSourceController : NeptuneBaseController
    {
        [HttpGet]
        [TreatmentBMPManageFeature]
        public PartialViewResult EditTreatmentBMPFundingSourcesForTreatmentBMP(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var currentTreatmentBMPFundingSources = treatmentBMP.TreatmentBMPFundingSources.ToList();
            var viewModel = new EditViewModel(currentTreatmentBMPFundingSources);
            return ViewEditTreatmentBMPFundingSources(treatmentBMP, viewModel);
        }

        [HttpPost]
        [TreatmentBMPManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditTreatmentBMPFundingSourcesForTreatmentBMP(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey, EditViewModel viewModel)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var currentTreatmentBMPFundingSources = treatmentBMP.TreatmentBMPFundingSources.ToList();
            if (!ModelState.IsValid)
            {
                return ViewEditTreatmentBMPFundingSources(treatmentBMP, viewModel);
            }
            return UpdateTreatmentBMPFundingSources(viewModel, currentTreatmentBMPFundingSources);
        }

        [HttpGet]
        [FundingSourceEditFeature]
        public PartialViewResult EditTreatmentBMPFundingSourcesForFundingSource(FundingSourcePrimaryKey fundingSourcePrimaryKey)
        {
            var fundingSource = fundingSourcePrimaryKey.EntityObject;
            var currentTreatmentBMPFundingSources = fundingSource.TreatmentBMPFundingSources.ToList();
            var viewModel = new EditViewModel(currentTreatmentBMPFundingSources);
            return ViewEditTreatmentBMPFundingSources(fundingSource, viewModel);
        }

        [HttpPost]
        [FundingSourceEditFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditTreatmentBMPFundingSourcesForFundingSource(FundingSourcePrimaryKey fundingSourcePrimaryKey, EditViewModel viewModel)
        {
            var fundingSource = fundingSourcePrimaryKey.EntityObject;
            var currentTreatmentBMPFundingSources = fundingSource.TreatmentBMPFundingSources.ToList();
            if (!ModelState.IsValid)
            {
                return ViewEditTreatmentBMPFundingSources(fundingSource, viewModel);
            }
            return UpdateTreatmentBMPFundingSources(viewModel, currentTreatmentBMPFundingSources);
        }

        private static ActionResult UpdateTreatmentBMPFundingSources(EditViewModel viewModel,
            List<TreatmentBMPFundingSource> currentTreatmentBMPFundingSources)
        {
            HttpRequestStorage.DatabaseEntities.TreatmentBMPFundingSources.Load();
            var allTreatmentBMPFundingSources = HttpRequestStorage.DatabaseEntities.AllTreatmentBMPFundingSources.Local;
            viewModel.UpdateModel(currentTreatmentBMPFundingSources, allTreatmentBMPFundingSources);


            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEditTreatmentBMPFundingSources(FundingSource fundingSource, EditViewModel viewModel)
        {
            var allTreatmentBMPs = HttpRequestStorage.DatabaseEntities.TreatmentBMPs.ToList().Select(x => new TreatmentBMPSimple(x)).OrderBy(p => p.DisplayName).ToList();
            var viewData = new EditViewData(new FundingSourceSimple(fundingSource), allTreatmentBMPs);
            return RazorPartialView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }

        private PartialViewResult ViewEditTreatmentBMPFundingSources(TreatmentBMP treatmentBMP, EditViewModel viewModel)
        {
            var allFundingSources = HttpRequestStorage.DatabaseEntities.FundingSources.ToList().Select(x => new FundingSourceSimple(x)).OrderBy(p => p.DisplayName).ToList();
            var viewData = new EditViewData(new TreatmentBMPSimple(treatmentBMP), allFundingSources);
            return RazorPartialView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }
    }
}