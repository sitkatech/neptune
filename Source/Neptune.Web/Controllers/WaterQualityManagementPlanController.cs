using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.Models;
using LtInfo.Common.MvcResults;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.Shared;
using Neptune.Web.Views.WaterQualityManagementPlan;
using TreatmentBMPGridSpec = Neptune.Web.Views.TreatmentBMP.TreatmentBMPGridSpec;

namespace Neptune.Web.Controllers
{
    public class WaterQualityManagementPlanController : NeptuneBaseController
    {
        [HttpGet]
        [WaterQualityManagementPlanViewFeature]
        public ViewResult Index()
        {
            var neptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.WaterQualityMaintenancePlan);
            var gridSpec = GetWaterQualityManagementPlanIndexGridSpec();
            var viewData = new IndexViewData(CurrentPerson, neptunePage, gridSpec);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [HttpGet]
        [WaterQualityManagementPlanViewFeature]
        public GridJsonNetJObjectResult<WaterQualityManagementPlan> WaterQualityManagementPlanIndexGridData()
        {
            var waterQualityManagementPlans = HttpRequestStorage.DatabaseEntities.WaterQualityManagementPlans.ToList();

            var gridSpec = GetWaterQualityManagementPlanIndexGridSpec();
            return new GridJsonNetJObjectResult<WaterQualityManagementPlan>(waterQualityManagementPlans, gridSpec);
        }

        private WaterQualityManagementPlanIndexGridSpec GetWaterQualityManagementPlanIndexGridSpec()
        {
            var currentUserCanManage = new WaterQualityManagementPlanManageFeature().HasPermissionByPerson(CurrentPerson);
            return new WaterQualityManagementPlanIndexGridSpec(currentUserCanManage);
        }

        [HttpGet]
        [WaterQualityManagementPlanViewFeature]
        public ViewResult Detail(WaterQualityManagementPlanPrimaryKey waterQualityManagementPlanPrimaryKey)
        {
            var waterQualityManagementPlan = waterQualityManagementPlanPrimaryKey.EntityObject;
            var gridSpec = new TreatmentBMPGridSpec(CurrentPerson);
            var mapInitJson = new MapInitJson("waterQualityManagementPlanMap", 0, new List<LayerGeoJson>(),
                BoundingBox.MakeNewDefaultBoundingBox());

            var viewData = new DetailViewData(CurrentPerson, waterQualityManagementPlan, gridSpec, mapInitJson);
            return RazorView<Detail, DetailViewData>(viewData);
        }
        
        [HttpGet]
        [WaterQualityManagementPlanViewFeature]
        public GridJsonNetJObjectResult<TreatmentBMP> TreatmentBmpsForWaterQualityManagementPlanGridData(WaterQualityManagementPlanPrimaryKey waterQualityManagementPlanPrimaryKey)
        {
            var waterQualityManagementPlan = waterQualityManagementPlanPrimaryKey.EntityObject;
            var treatmentBmPs = waterQualityManagementPlan.TreatmentBMPs.ToList();
            var gridSpec = new TreatmentBMPGridSpec(CurrentPerson);
            return new GridJsonNetJObjectResult<TreatmentBMP>(treatmentBmPs, gridSpec);
        }

        [HttpGet]
        [WaterQualityManagementPlanCreateFeature]
        public PartialViewResult New()
        {
            var viewModel = new EditViewModel
            {
                WaterQualityManagementPlanID = ModelObjectHelpers.NotYetAssignedID
            };
            return ViewEdit(viewModel);
        }

        [HttpPost]
        [WaterQualityManagementPlanCreateFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult New(EditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel);
            }

            var waterQualityManagementPlan = new WaterQualityManagementPlan(ModelObjectHelpers.NotYetAssignedID,
                ModelObjectHelpers.NotYetAssignedID, ModelObjectHelpers.NotYetAssignedID,
                ModelObjectHelpers.NotYetAssignedID, ModelObjectHelpers.NotYetAssignedID,
                ModelObjectHelpers.NotYetAssignedID, ModelObjectHelpers.NotYetAssignedID,
                ModelObjectHelpers.NotYetAssignedID, null);
            viewModel.UpdateModels(waterQualityManagementPlan);
            HttpRequestStorage.DatabaseEntities.AllWaterQualityManagementPlans.Add(waterQualityManagementPlan);

            SetMessageForDisplay($"Successfully created \"{waterQualityManagementPlan.WaterQualityManagementPlanName}\".");

            return new ModalDialogFormJsonResult();
        }

        [HttpGet]
        [WaterQualityManagementPlanManageFeature]
        public PartialViewResult Edit(WaterQualityManagementPlanPrimaryKey waterQualityManagementPlanPrimaryKey)
        {
            var waterQualityManagementPlan = waterQualityManagementPlanPrimaryKey.EntityObject;
            var viewModel = new EditViewModel(waterQualityManagementPlan);
            return ViewEdit(viewModel);
        }

        [HttpPost]
        [WaterQualityManagementPlanManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Edit(WaterQualityManagementPlanPrimaryKey waterQualityManagementPlanPrimaryKey, EditViewModel viewModel)
        {
            var waterQualityManagementPlan = waterQualityManagementPlanPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel);
            }

            viewModel.UpdateModels(waterQualityManagementPlan);
            SetMessageForDisplay($"Successfully updated \"{waterQualityManagementPlan.WaterQualityManagementPlanName}\".");

            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEdit(EditViewModel viewModel)
        {
            var stormwaterJurisdictions = HttpRequestStorage.DatabaseEntities.StormwaterJurisdictions.ToList();
            var people = HttpRequestStorage.DatabaseEntities.People.ToList();
            var organizations = HttpRequestStorage.DatabaseEntities.Organizations.ToList().Where(x => !x.IsUnknown).ToList();

            var viewData = new EditViewData(stormwaterJurisdictions, people, organizations);
            return RazorPartialView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [WaterQualityManagementPlanManageFeature]
        public PartialViewResult Delete(WaterQualityManagementPlanPrimaryKey waterQualityManagementPlanPrimaryKey)
        {
            var waterQualityManagementPlan = waterQualityManagementPlanPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(waterQualityManagementPlan.WaterQualityManagementPlanID);
            return ViewDelete(waterQualityManagementPlan, viewModel);
        }

        [HttpPost]
        [WaterQualityManagementPlanManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Delete(WaterQualityManagementPlanPrimaryKey waterQualityManagementPlanPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var waterQualityManagementPlan = waterQualityManagementPlanPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDelete(waterQualityManagementPlan, viewModel);
            }

            waterQualityManagementPlan.DeleteFull();
            SetMessageForDisplay($"Successfully delete \"{waterQualityManagementPlan.WaterQualityManagementPlanName}\".");

            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewDelete(WaterQualityManagementPlan waterQualityManagementPlan, ConfirmDialogFormViewModel viewModel)
        {
            var viewData = new ConfirmDialogFormViewData(
                $"Are you sure you want to delete \"{waterQualityManagementPlan.WaterQualityManagementPlanName}\"?");
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }
    }
}
