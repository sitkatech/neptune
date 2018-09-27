using System;
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
using QuickBMPGridSpec = Neptune.Web.Views.TreatmentBMP.QuickBMPGridSpec;

namespace Neptune.Web.Controllers
{
    public class WaterQualityManagementPlanController : NeptuneBaseController
    {
        [HttpGet]
        [WaterQualityManagementPlanViewFeature]
        public ViewResult Index()
        {
            var neptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.WaterQualityMaintenancePlan);
            var gridSpec = new WaterQualityManagementPlanIndexGridSpec(CurrentPerson);
            var viewData = new IndexViewData(CurrentPerson, neptunePage, gridSpec);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [HttpGet]
        [WaterQualityManagementPlanViewFeature]
        public GridJsonNetJObjectResult<WaterQualityManagementPlan> WaterQualityManagementPlanIndexGridData()
        {
            var waterQualityManagementPlans = HttpRequestStorage.DatabaseEntities.WaterQualityManagementPlans.ToList();

            var gridSpec = new WaterQualityManagementPlanIndexGridSpec(CurrentPerson);
            return new GridJsonNetJObjectResult<WaterQualityManagementPlan>(waterQualityManagementPlans, gridSpec);
        }

        [HttpGet]
        [WaterQualityManagementPlanViewFeature]
        public ViewResult Detail(WaterQualityManagementPlanPrimaryKey waterQualityManagementPlanPrimaryKey)
        {
            var waterQualityManagementPlan = waterQualityManagementPlanPrimaryKey.EntityObject;
            var treatmentBMPGridSpec = new TreatmentBMPGridSpec(CurrentPerson, false, false);
            var quickBMPGridSpec = new QuickBMPGridSpec();

            var parcelGeoJsonFeatureCollection = waterQualityManagementPlan.WaterQualityManagementPlanParcels
                .Select(x => x.Parcel).ToGeoJsonFeatureCollection();
            var treatmentBmpGeoJsonFeatureCollection =
                waterQualityManagementPlan.TreatmentBMPs.ToGeoJsonFeatureCollection();
            treatmentBmpGeoJsonFeatureCollection.Features.ForEach(x =>
            {
                var treatmentBmpID = x.Properties.ContainsKey("TreatmentBMPID")
                    ? int.Parse(x.Properties["TreatmentBMPID"].ToString())
                    : (int?) null;
                if (treatmentBmpID != null)
                {
                    x.Properties.Add("PopupUrl", SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(c => c.MapPopup(treatmentBmpID)));
                }
            });

            var layerGeoJsons = new List<LayerGeoJson>
            {
                new LayerGeoJson(FieldDefinition.Parcel.GetFieldDefinitionLabelPluralized(),
                    parcelGeoJsonFeatureCollection,
                    ParcelModelExtensions.ParcelColor,
                    1,
                    LayerInitialVisibility.Show),
                new LayerGeoJson(FieldDefinition.TreatmentBMP.GetFieldDefinitionLabelPluralized(),
                    treatmentBmpGeoJsonFeatureCollection,
                    "#935f59",
                    1,
                    LayerInitialVisibility.Show)
            };
            var mapInitJson = new MapInitJson("waterQualityManagementPlanMap", 0, layerGeoJsons,
                BoundingBox.MakeBoundingBoxFromLayerGeoJsonList(layerGeoJsons));

            var viewData = new DetailViewData(CurrentPerson, waterQualityManagementPlan, treatmentBMPGridSpec, quickBMPGridSpec, mapInitJson, new ParcelGridSpec());
            return RazorView<Detail, DetailViewData>(viewData);
        }
        
        [HttpGet]
        [WaterQualityManagementPlanViewFeature]
        public GridJsonNetJObjectResult<TreatmentBMP> TreatmentBmpsForWaterQualityManagementPlanGridData(WaterQualityManagementPlanPrimaryKey waterQualityManagementPlanPrimaryKey)
        {
            var waterQualityManagementPlan = waterQualityManagementPlanPrimaryKey.EntityObject;
            var treatmentBmPs = waterQualityManagementPlan.TreatmentBMPs.ToList();
            var gridSpec = new TreatmentBMPGridSpec(CurrentPerson, false, false);
            return new GridJsonNetJObjectResult<TreatmentBMP>(treatmentBmPs, gridSpec);
        }


        [HttpGet]
        [WaterQualityManagementPlanViewFeature]
        public GridJsonNetJObjectResult<QuickBMP> QuickBmpsForWaterQualityManagementPlanGridData(WaterQualityManagementPlanPrimaryKey waterQualityManagementPlanPrimaryKey)
        {
            var waterQualityManagementPlan = waterQualityManagementPlanPrimaryKey.EntityObject;
            var quickBmps = waterQualityManagementPlan.QuickBMPs.ToList();
            var gridSpec = new QuickBMPGridSpec();
            return new GridJsonNetJObjectResult<QuickBMP>(quickBmps, gridSpec);
        }

        [HttpGet]
        [WaterQualityManagementPlanViewFeature]
        public GridJsonNetJObjectResult<Parcel> ParcelsForWaterQualityManagementPlanGridData(WaterQualityManagementPlanPrimaryKey waterQualityManagementPlanPlanPrimaryKey)
        {
            var waterQualityManagementPlan = waterQualityManagementPlanPlanPrimaryKey.EntityObject;
            var parcels = waterQualityManagementPlan.WaterQualityManagementPlanParcels.Select(x => x.Parcel).OrderBy(x => x.ParcelNumber).ToList();
            var gridSpec = new ParcelGridSpec();
            return new GridJsonNetJObjectResult<Parcel>(parcels, gridSpec);
        }


        #region CRUD Water Quality Management Plan
        [HttpGet]
        [WaterQualityManagementPlanCreateFeature]
        public PartialViewResult New()
        {
            var viewModel = new NewViewModel();
            return ViewNew(viewModel);
        }

        [HttpPost]
        [WaterQualityManagementPlanCreateFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult New(NewViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewNew(viewModel);
            }

            var waterQualityManagementPlan = new WaterQualityManagementPlan(ModelObjectHelpers.NotYetAssignedID,
                ModelObjectHelpers.NotYetAssignedID, ModelObjectHelpers.NotYetAssignedID,
                ModelObjectHelpers.NotYetAssignedID, ModelObjectHelpers.NotYetAssignedID, null);
            viewModel.UpdateModels(waterQualityManagementPlan);
            HttpRequestStorage.DatabaseEntities.AllWaterQualityManagementPlans.Add(waterQualityManagementPlan);
            HttpRequestStorage.DatabaseEntities.SaveChanges();

            SetMessageForDisplay($"Successfully created \"{waterQualityManagementPlan.WaterQualityManagementPlanName}\".");

            return new ModalDialogFormJsonResult(
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(c => c.Detail(waterQualityManagementPlan)));
        }

        private PartialViewResult ViewNew(NewViewModel viewModel)
        {
            var stormwaterJurisdictions = new List<Role> {Role.Admin, Role.SitkaAdmin}.Contains(CurrentPerson.Role)
                ? HttpRequestStorage.DatabaseEntities.StormwaterJurisdictions.ToList()
                : CurrentPerson.StormwaterJurisdictionPeople.Select(x => x.StormwaterJurisdiction).ToList();
            var hydrologicSubareas = HttpRequestStorage.DatabaseEntities.HydrologicSubareas.ToList();
            var viewData = new NewViewData(stormwaterJurisdictions, hydrologicSubareas);
            return RazorPartialView<New, NewViewData, NewViewModel>(viewData, viewModel);
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

            return new ModalDialogFormJsonResult(
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(c => c.Detail(waterQualityManagementPlan)));
        }

        private PartialViewResult ViewEdit(EditViewModel viewModel)
        {
            var stormwaterJurisdictions = HttpRequestStorage.DatabaseEntities.StormwaterJurisdictions.ToList();
            var hydrologicSubareas = HttpRequestStorage.DatabaseEntities.HydrologicSubareas.ToList();
            var viewData = new EditViewData(stormwaterJurisdictions, hydrologicSubareas);
            return RazorPartialView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [WaterQualityManagementPlanDeleteFeature]
        public PartialViewResult Delete(WaterQualityManagementPlanPrimaryKey waterQualityManagementPlanPrimaryKey)
        {
            var waterQualityManagementPlan = waterQualityManagementPlanPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(waterQualityManagementPlan.WaterQualityManagementPlanID);
            return ViewDelete(waterQualityManagementPlan, viewModel);
        }

        [HttpPost]
        [WaterQualityManagementPlanDeleteFeature]
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

        private PartialViewResult ViewDelete(WaterQualityManagementPlan waterQualityManagementPlan,
            ConfirmDialogFormViewModel viewModel)
        {
            var viewData = new ConfirmDialogFormViewData(
                $"Are you sure you want to delete \"{waterQualityManagementPlan.WaterQualityManagementPlanName}\"?");
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        #endregion

        #region WQMP Treatment BMPs
        [HttpGet]
        [JurisdictionManageFeature]
        public ViewResult EditWqmpTreatmentBmps(
            WaterQualityManagementPlanPrimaryKey waterQualityManagementPlanPrimaryKey)
        {
            var waterQualityManagementPlan = waterQualityManagementPlanPrimaryKey.EntityObject;
            var sourceControlBMPAttributes = HttpRequestStorage.DatabaseEntities.SourceControlBMPAttributes.ToList();
            var viewModel = new EditWqmpBmpsViewModel(waterQualityManagementPlan, sourceControlBMPAttributes);
            return ViewEditWqmpTreatmentBmps(waterQualityManagementPlan, viewModel);
        }

        [HttpPost]
        [JurisdictionManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditWqmpTreatmentBmps(
            WaterQualityManagementPlanPrimaryKey waterQualityManagementPlanPrimaryKey,
            EditWqmpBmpsViewModel viewModel)
        {
            var waterQualityManagementPlan = waterQualityManagementPlanPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEditWqmpTreatmentBmps(waterQualityManagementPlan, viewModel);
            }

            viewModel.UpdateModels(waterQualityManagementPlan, viewModel.QuickBmpSimples, viewModel.SourceControlBMPSimples);
            SetMessageForDisplay(
                $"Successfully updated BMPs for {waterQualityManagementPlan.WaterQualityManagementPlanName}");

            return RedirectToAction(new SitkaRoute<WaterQualityManagementPlanController>(c => c.Detail(waterQualityManagementPlanPrimaryKey)));
        }

        private ViewResult ViewEditWqmpTreatmentBmps(WaterQualityManagementPlan waterQualityManagementPlan,
            EditWqmpBmpsViewModel viewModel)
        {
            var treatmentBMPTypes = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.OrderBy(x => x.TreatmentBMPTypeName).ToList().Select(x => new TreatmentBMPTypeSimple(x));
            var viewData = new EditWqmpBmpsViewData(CurrentPerson, waterQualityManagementPlan, treatmentBMPTypes);
            return RazorView<EditWqmpBmps, EditWqmpBmpsViewData, EditWqmpBmpsViewModel>(viewData, viewModel);
        }

        #endregion

        #region WQMP Parcels

        [HttpGet]
        [WaterQualityManagementPlanManageFeature]
        public ViewResult EditWqmpParcels(WaterQualityManagementPlanPrimaryKey waterQualityManagementPlanPrimaryKey)
        {
            var waterQualityManagementPlan = waterQualityManagementPlanPrimaryKey.EntityObject;
            var viewModel = new EditWqmpParcelsViewModel(waterQualityManagementPlan);
            return ViewEditWqmpParcels(waterQualityManagementPlan, viewModel);
        }

        [HttpPost]
        [WaterQualityManagementPlanManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditWqmpParcels(WaterQualityManagementPlanPrimaryKey waterQualityManagementPlanPrimaryKey, EditWqmpParcelsViewModel viewModel)
        {
            var waterQualityManagementPlan = waterQualityManagementPlanPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEditWqmpParcels(waterQualityManagementPlan, viewModel);
            }

            viewModel.UpdateModels(waterQualityManagementPlan);
            SetMessageForDisplay($"Successfully edited {FieldDefinition.Parcel.GetFieldDefinitionLabelPluralized()} for {FieldDefinition.WaterQualityManagementPlan.GetFieldDefinitionLabel()}."); // TODO set message for displaty

            return RedirectToAction(new SitkaRoute<WaterQualityManagementPlanController>(c => c.Detail(waterQualityManagementPlan)));
        }

        private ViewResult ViewEditWqmpParcels(WaterQualityManagementPlan waterQualityManagementPlan, EditWqmpParcelsViewModel viewModel)
        {
            var tenantAttribute = HttpRequestStorage.Tenant.GetTenantAttribute();
            var layerGeoJsons = MapInitJsonHelpers.GetParcelMapLayers(tenantAttribute, LayerInitialVisibility.Show)
                .ToList();
            var mapInitJson = new MapInitJson("editWqmpParcelMap", 0, layerGeoJsons, BoundingBox.MakeNewDefaultBoundingBox());
            var viewData = new EditWqmpParcelsViewData(CurrentPerson, waterQualityManagementPlan, mapInitJson, tenantAttribute);
            return RazorView<EditWqmpParcels, EditWqmpParcelsViewData, EditWqmpParcelsViewModel>(viewData, viewModel);
        }

        #endregion

        #region BWMP O&M Verification Record

        [HttpGet]
        [WaterQualityManagementPlanManageFeature]
        public ViewResult EditWqmpVerify(WaterQualityManagementPlanPrimaryKey waterQualityManagementPlanPrimaryKey)
        {
            var waterQualityManagementPlan = waterQualityManagementPlanPrimaryKey.EntityObject;
            var viewModel = new EditWqmpVerifyViewModel(waterQualityManagementPlan);
            return ViewEditWqmpVerify(waterQualityManagementPlan, viewModel);
        }

        [HttpPost]
        [WaterQualityManagementPlanManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditWqmpVerify( WaterQualityManagementPlanPrimaryKey waterQualityManagementPlanPrimaryKey, EditWqmpVerifyViewModel viewModel)
        {
            var waterQualityManagementPlan = waterQualityManagementPlanPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEditWqmpVerify(waterQualityManagementPlan, viewModel);
            }

            var waterQualityManagementPlanVerify = new WaterQualityManagementPlanVerify(
                waterQualityManagementPlan.WaterQualityManagementPlanID, 
                viewModel.WaterQualityManagementPlanVerifyTypeID,
                viewModel.WaterQualityManagementPlanVisitStatusID,
                viewModel.WaterQualityManagementPlanVerifyStatusID,
                CurrentPerson.PersonID,
                DateTime.Now);
            viewModel.UpdateModels(waterQualityManagementPlan, waterQualityManagementPlanVerify);



            HttpRequestStorage.DatabaseEntities.AllWaterQualityManagementPlanVerifies.Add(waterQualityManagementPlanVerify);
            HttpRequestStorage.DatabaseEntities.SaveChanges();

            SetMessageForDisplay(
                $"Successfully updated {FieldDefinition.TreatmentBMP.GetFieldDefinitionLabelPluralized()} " + $"for {waterQualityManagementPlan.WaterQualityManagementPlanName}");

            return RedirectToAction(new SitkaRoute<WaterQualityManagementPlanController>(c => c.Detail(waterQualityManagementPlan)));
        }

        private ViewResult ViewEditWqmpVerify(WaterQualityManagementPlan waterQualityManagementPlan, EditWqmpVerifyViewModel viewModel)
        {
            var waterQualityManagementPlanVerifyTypes = HttpRequestStorage.DatabaseEntities.WaterQualityManagementPlanVerifyTypes.ToList();
            var waterQualityManagementPlanVisitStatuses = HttpRequestStorage.DatabaseEntities.WaterQualityManagementPlanVisitStatuses.ToList();
            var waterQualityManagementPlanVerifyStatuses = HttpRequestStorage.DatabaseEntities.WaterQualityManagementPlanVerifyStatuses.ToList();
            var viewData = new EditWqmpVerifyViewData(CurrentPerson, waterQualityManagementPlan, waterQualityManagementPlanVerifyTypes, waterQualityManagementPlanVisitStatuses, waterQualityManagementPlanVerifyStatuses);
            return RazorView<EditWqmpVerify, EditWqmpVerifyViewData, EditWqmpVerifyViewModel>(viewData, viewModel);
        }

        #endregion

    }
}
