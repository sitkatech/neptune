using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.MvcResults;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.MaintenanceActivity;

namespace Neptune.Web.Controllers
{
    public class MaintenanceActivityController : NeptuneBaseController
    {
        [TreatmentBMPManageFeature]
        public GridJsonNetJObjectResult<MaintenanceActivity> MaintenanceActivitysGridJsonData(
            TreatmentBMPPrimaryKey treatmentBmpPrimaryKey)
        {
            var treatmentBmp = treatmentBmpPrimaryKey.EntityObject;
            var gridSpec = new MaintenanceActivityGridSpec(CurrentPerson, treatmentBmp);
            var bmpMaintenanceActivitys = treatmentBmp.MaintenanceActivities.ToList()
                .OrderByDescending(x => x.MaintenanceActivityDate).ToList();
            var gridJsonNetJObjectResult =
                new GridJsonNetJObjectResult<MaintenanceActivity>(bmpMaintenanceActivitys, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpGet]
        [TreatmentBMPManageFeature]
        public PartialViewResult New(TreatmentBMPPrimaryKey treatmentBmpPrimaryKey)
        {
            return ViewNew(new EditMaintenanceActivityViewModel());
        }

        //todo : possibly an unnecessary abstraction
        private PartialViewResult ViewNew(EditMaintenanceActivityViewModel viewModel)
        {
            return ViewEdit(viewModel);
        }

        [HttpGet]
        [TreatmentBMPManageFeature]
        public PartialViewResult Edit(MaintenanceActivityPrimaryKey maintenanceActivityPrimaryKey)
        {
            var maintenanceActivity = maintenanceActivityPrimaryKey.EntityObject;
            var viewModel = new EditMaintenanceActivityViewModel(maintenanceActivity);
            return ViewEdit(viewModel);
        }

        [HttpPost]
        [TreatmentBMPManageFeature]
        public ActionResult Edit(MaintenanceActivityPrimaryKey maintenanceActivityPrimaryKey,
            EditMaintenanceActivityViewModel viewModel)
        {
            throw new System.NotImplementedException();
        }

        private PartialViewResult ViewEdit(EditMaintenanceActivityViewModel viewModel)
        {
            var viewData = new EditMaintenanceActivityViewData();
            return RazorPartialView<EditMaintenanceActivity, EditMaintenanceActivityViewData,
                EditMaintenanceActivityViewModel>(viewData, viewModel);
        }

        [TreatmentBMPManageFeature]
        public ActionResult Delete(MaintenanceActivityPrimaryKey maintenanceActivityPrimaryKey)
        {
            throw new System.NotImplementedException();
        }
    }
}
