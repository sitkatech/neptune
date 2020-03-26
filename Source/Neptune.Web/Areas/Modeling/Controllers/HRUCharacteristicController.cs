using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Hangfire;
using LtInfo.Common.MvcResults;
using Neptune.Web.Areas.Modeling.Views.HRUCharacteristic;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.ScheduledJobs;
using Neptune.Web.Security;
using Neptune.Web.Views.Shared;

namespace Neptune.Web.Areas.Modeling.Controllers
{
    public class HRUCharacteristicController : NeptuneBaseController
    {
        [NeptuneAdminFeature]
        public ViewResult Index()
        {
            var viewData = new IndexViewData(CurrentPerson);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [NeptuneAdminFeature]
        public GridJsonNetJObjectResult<HRUCharacteristic> HRUCharacteristicGridJsonData()
        {
            // ReSharper disable once InconsistentNaming
            List<HRUCharacteristic> treatmentBMPs = GetHRUCharacteristicsAndGridSpec(out var gridSpec);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<HRUCharacteristic>(treatmentBMPs, gridSpec);
            return gridJsonNetJObjectResult;
        }

        private List<HRUCharacteristic> GetHRUCharacteristicsAndGridSpec(out HRUCharacteristicGridSpec gridSpec)
        {
            gridSpec = new HRUCharacteristicGridSpec();

            return HttpRequestStorage.DatabaseEntities.HRUCharacteristics.ToList();
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

            BackgroundJob.Enqueue(() => ScheduledBackgroundJobLaunchHelper.RunLoadGeneratingUnitRefreshJob(null));
            BackgroundJob.Enqueue(() => ScheduledBackgroundJobLaunchHelper.RunHRURefreshJob());

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