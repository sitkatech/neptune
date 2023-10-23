using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Neptune.Common.Services;
using Neptune.Common.Services.GDAL;
using Neptune.EFModels.Entities;
using Neptune.Jobs.Hangfire;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.MvcResults;
using Neptune.WebMvc.Security;
using Neptune.WebMvc.Views.HRUCharacteristic;
using Neptune.WebMvc.Views.Shared;

namespace Neptune.WebMvc.Controllers
{
    public class HRUCharacteristicController : NeptuneBaseController<HRUCharacteristicController>
    {
        public HRUCharacteristicController(NeptuneDbContext dbContext, ILogger<HRUCharacteristicController> logger, IOptions<WebConfiguration> webConfiguration, LinkGenerator linkGenerator) : base(dbContext, logger, linkGenerator, webConfiguration)
        {
        }

        [HttpGet]
        [NeptuneAdminFeature]
        public ViewResult Index()
        {
            var neptunePage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.HRUCharacteristics);
            var viewData = new IndexViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, neptunePage);
            return RazorView<Views.HRUCharacteristic.Index, IndexViewData>(viewData);
        }

        [HttpGet]
        [NeptuneAdminFeature]
        public GridJsonNetJObjectResult<vHRUCharacteristic> HRUCharacteristicGridJsonData()
        {
            var gridSpec = new HRUCharacteristicGridSpec(_linkGenerator);
            var hruCharacteristics = vHRUCharacteristics.List(_dbContext);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<vHRUCharacteristic>(hruCharacteristics, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpGet]
        [NeptuneAdminFeature]
        public PartialViewResult RefreshHRUCharacteristics()
        {
            return ViewRefreshHRUCharacteristics(new ConfirmDialogFormViewModel());
        }

        [HttpPost]
        [NeptuneAdminFeature]
        public ActionResult RefreshHRUCharacteristics(ConfirmDialogFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewRefreshHRUCharacteristics(viewModel);
            }

            BackgroundJob.Enqueue<LoadGeneratingUnitRefreshJob>(x => x.RunJob(null, true));

            SetMessageForDisplay("HRU Characteristic refresh will run in the background.");
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewRefreshHRUCharacteristics(ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = "Are you sure you want to refresh the HRU Characteristics?<br /><br />This can take several hours and will prevent other scheduled jobs from running in the meantime.";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }
    }
}