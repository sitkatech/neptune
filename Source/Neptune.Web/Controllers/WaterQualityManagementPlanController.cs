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

namespace Neptune.Web.Controllers
{
    public class WaterQualityManagementPlanController : NeptuneBaseController
    {
        [HttpGet]
        [WaterQualityManagementPlanViewFeature]
        public ViewResult Index()
        {
            var neptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.WaterQualityMaintenancePlan);
            var wqmpGridSpec = new WaterQualityManagementPlanIndexGridSpec(CurrentPerson);
            var verificationNeptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.WaterQualityMaintenancePlanOandMVerifications);
            var verificationGridSpec = new WaterQualityManagementPlanVerificationGridSpec(CurrentPerson);
            var viewData = new IndexViewData(CurrentPerson, neptunePage, wqmpGridSpec, verificationNeptunePage, verificationGridSpec);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [HttpGet]
        [WaterQualityManagementPlanViewFeature]
        public GridJsonNetJObjectResult<WaterQualityManagementPlan> WaterQualityManagementPlanIndexGridData()
        {
            var waterQualityManagementPlans = HttpRequestStorage.DatabaseEntities.WaterQualityManagementPlans.Where(x => x.StormwaterJurisdiction.OrganizationID == CurrentPerson.OrganizationID).ToList();

            var gridSpec = new WaterQualityManagementPlanIndexGridSpec(CurrentPerson);
            return new GridJsonNetJObjectResult<WaterQualityManagementPlan>(waterQualityManagementPlans, gridSpec);
        }

        [HttpGet]
        [WaterQualityManagementPlanViewFeature]
        public GridJsonNetJObjectResult<WaterQualityManagementPlanVerify> WaterQualityManagementPlanVerificationGridData()
        {
            var waterQualityManagementPlanVerifications = HttpRequestStorage.DatabaseEntities
                .WaterQualityManagementPlanVerifies.Where(x => x.WaterQualityManagementPlan.StormwaterJurisdiction.OrganizationID == CurrentPerson.OrganizationID)
                .OrderBy(x => x.WaterQualityManagementPlan.StormwaterJurisdiction.Organization.OrganizationName)
                .ThenBy(x => x.WaterQualityManagementPlan.WaterQualityManagementPlanName)
                .ThenByDescending(x => x.LastEditedDate).ToList();

            var gridSpec = new WaterQualityManagementPlanVerificationGridSpec(CurrentPerson);
            return new GridJsonNetJObjectResult<WaterQualityManagementPlanVerify>(waterQualityManagementPlanVerifications, gridSpec);
        }

        [HttpGet]
        [WaterQualityManagementPlanViewFeature]
        public ViewResult Detail(WaterQualityManagementPlanPrimaryKey waterQualityManagementPlanPrimaryKey)
        {
            var waterQualityManagementPlan = waterQualityManagementPlanPrimaryKey.EntityObject;

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

            var waterQualityManagementPlanVerifies = HttpRequestStorage.DatabaseEntities.WaterQualityManagementPlanVerifies.Where(x =>
                x.WaterQualityManagementPlanID == waterQualityManagementPlan.PrimaryKey).OrderByDescending(x => x.LastEditedDate).ToList();
            var waterQualityManagementPlanVerifyDraft = waterQualityManagementPlanVerifies.SingleOrDefault(x => x.IsDraft);

            var waterQualityManagementPlanVerifyQuickBMP =
                HttpRequestStorage.DatabaseEntities.WaterQualityManagementPlanVerifyQuickBMPs.Where(x =>
                    x.WaterQualityManagementPlanVerify.WaterQualityManagementPlanID ==
                    waterQualityManagementPlan.WaterQualityManagementPlanID).ToList();
            var waterQualityManagementPlanVerifyTreatmentBMP =
                HttpRequestStorage.DatabaseEntities.WaterQualityManagementPlanVerifyTreatmentBMPs.Where(x =>
            x.WaterQualityManagementPlanVerify.WaterQualityManagementPlanID ==
                waterQualityManagementPlan.WaterQualityManagementPlanID).ToList();

            var viewData = new DetailViewData(CurrentPerson, waterQualityManagementPlan, waterQualityManagementPlanVerifyDraft, mapInitJson, new ParcelGridSpec(), waterQualityManagementPlanVerifies, waterQualityManagementPlanVerifyQuickBMP, waterQualityManagementPlanVerifyTreatmentBMP);

            return RazorView<Detail, DetailViewData>(viewData);
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

            var waterQualityManagementPlan = new WaterQualityManagementPlan(ModelObjectHelpers.NotYetAssignedID, null, TrashCaptureStatusType.NotProvided.TrashCaptureStatusTypeID);
            viewModel.UpdateModels(waterQualityManagementPlan);
            HttpRequestStorage.DatabaseEntities.WaterQualityManagementPlans.Add(waterQualityManagementPlan);
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
            var viewData = new NewViewData(stormwaterJurisdictions, hydrologicSubareas, TrashCaptureStatusType.All);
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
            var viewData = new EditViewData(stormwaterJurisdictions, hydrologicSubareas, TrashCaptureStatusType.All);
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

            waterQualityManagementPlan.DeleteFull(HttpRequestStorage.DatabaseEntities);
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
        [WaterQualityManagementPlanViewFeature]
        public ViewResult EditWqmpBmps(
            WaterQualityManagementPlanPrimaryKey waterQualityManagementPlanPrimaryKey)
        {
            var waterQualityManagementPlan = waterQualityManagementPlanPrimaryKey.EntityObject;
            var sourceControlBMPAttributes = HttpRequestStorage.DatabaseEntities.SourceControlBMPAttributes.ToList();
            var viewModel = new EditWqmpBmpsViewModel(waterQualityManagementPlan, sourceControlBMPAttributes);
            return ViewEditWqmpBmps(waterQualityManagementPlan, viewModel);
        }

        [HttpPost]
        [WaterQualityManagementPlanViewFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditWqmpBmps(
            WaterQualityManagementPlanPrimaryKey waterQualityManagementPlanPrimaryKey,
            EditWqmpBmpsViewModel viewModel)
        {
            var waterQualityManagementPlan = waterQualityManagementPlanPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEditWqmpBmps(waterQualityManagementPlan, viewModel);
            }

            viewModel.UpdateModels(waterQualityManagementPlan, viewModel.QuickBmpSimples, viewModel.SourceControlBMPSimples);
            SetMessageForDisplay(
                $"Successfully updated BMPs for {waterQualityManagementPlan.WaterQualityManagementPlanName}");

            return RedirectToAction(new SitkaRoute<WaterQualityManagementPlanController>(c => c.Detail(waterQualityManagementPlanPrimaryKey)));
        }

        private ViewResult ViewEditWqmpBmps(WaterQualityManagementPlan waterQualityManagementPlan,
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
            SetMessageForDisplay($"Successfully edited {FieldDefinition.Parcel.GetFieldDefinitionLabelPluralized()} for {FieldDefinition.WaterQualityManagementPlan.GetFieldDefinitionLabel()}."); 

            return RedirectToAction(new SitkaRoute<WaterQualityManagementPlanController>(c => c.Detail(waterQualityManagementPlan)));
        }

        private ViewResult ViewEditWqmpParcels(WaterQualityManagementPlan waterQualityManagementPlan, EditWqmpParcelsViewModel viewModel)
        {
            var layerGeoJsons = MapInitJsonHelpers.GetParcelMapLayers(LayerInitialVisibility.Show)
                .ToList();
            var mapInitJson = new MapInitJson("editWqmpParcelMap", 0, layerGeoJsons, BoundingBox.MakeNewDefaultBoundingBox());
            var viewData = new EditWqmpParcelsViewData(CurrentPerson, waterQualityManagementPlan, mapInitJson);
            return RazorView<EditWqmpParcels, EditWqmpParcelsViewData, EditWqmpParcelsViewModel>(viewData, viewModel);
        }

        #endregion

        #region WQMP O&M Verification Record




        [HttpGet]
        [WaterQualityManagementPlanViewFeature]
        public ViewResult WqmpVerify(WaterQualityManagementPlanVerifyPrimaryKey waterQualityManagementPlanVerifyPrimaryKey)
        {
            var waterQualityManagementPlanVerify = waterQualityManagementPlanVerifyPrimaryKey.EntityObject;


            var waterQualityManagementPlanVerifyQuickBMP =
                HttpRequestStorage.DatabaseEntities.WaterQualityManagementPlanVerifyQuickBMPs.Where(x =>
                    x.WaterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyID ==
                    waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyID).ToList();
            var waterQualityManagementPlanVerifyTreatmentBMP =
                HttpRequestStorage.DatabaseEntities.WaterQualityManagementPlanVerifyTreatmentBMPs.Where(x =>
            x.WaterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyID ==
            waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyID).ToList();

            var viewData = new WqmpVerifyViewData(CurrentPerson, waterQualityManagementPlanVerify, waterQualityManagementPlanVerifyQuickBMP, waterQualityManagementPlanVerifyTreatmentBMP);

            return RazorView<WqmpVerify, WqmpVerifyViewData>(viewData);
        }

        [HttpGet]
        [WaterQualityManagementPlanManageFeature]
        public ViewResult NewWqmpVerify(WaterQualityManagementPlanPrimaryKey waterQualityManagementPlanPrimaryKey)
        {
            var waterQualityManagementPlan = waterQualityManagementPlanPrimaryKey.EntityObject;
            var quickBMPs = waterQualityManagementPlan.QuickBMPs.ToList();
            var treatmentBMPs = waterQualityManagementPlan.TreatmentBMPs.ToList();
            var waterQualityManagementPlanVerify = new WaterQualityManagementPlanVerify(
                waterQualityManagementPlan.WaterQualityManagementPlanID,
                ModelObjectHelpers.NotYetAssignedID,
                ModelObjectHelpers.NotYetAssignedID,
                CurrentPerson.PersonID,
                DateTime.Now,
                true);
            var viewModel = new NewWqmpVerifyViewModel(waterQualityManagementPlan, waterQualityManagementPlanVerify, quickBMPs, treatmentBMPs);
            return ViewNewWqmpVerify(waterQualityManagementPlan, viewModel);
        }

        [HttpPost]
        [WaterQualityManagementPlanManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult NewWqmpVerify( WaterQualityManagementPlanPrimaryKey waterQualityManagementPlanPrimaryKey, NewWqmpVerifyViewModel viewModel)
        {
            var waterQualityManagementPlan = waterQualityManagementPlanPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewNewWqmpVerify(waterQualityManagementPlan, viewModel);
            }

            var waterQualityManagementPlanVerify = new WaterQualityManagementPlanVerify(
                waterQualityManagementPlan.WaterQualityManagementPlanID,
                viewModel.WaterQualityManagementPlanVerifyTypeID,
                viewModel.WaterQualityManagementPlanVisitStatusID,
                CurrentPerson.PersonID,
                DateTime.Now,
                !viewModel.HiddenIsFinalizeVerificationInput);

            viewModel.UpdateModels(waterQualityManagementPlan, waterQualityManagementPlanVerify, viewModel.WaterQualityManagementPlanVerifyQuickBMPSimples, viewModel.WaterQualityManagementPlanVerifyTreatmentBMPSimples, CurrentPerson);

            HttpRequestStorage.DatabaseEntities.WaterQualityManagementPlanVerifies.Add(waterQualityManagementPlanVerify);
            HttpRequestStorage.DatabaseEntities.SaveChanges();

            SetMessageForDisplay(
                $"Successfully updated {FieldDefinition.TreatmentBMP.GetFieldDefinitionLabelPluralized()} " + $"for {waterQualityManagementPlan.WaterQualityManagementPlanName}");

            return RedirectToAction(new SitkaRoute<WaterQualityManagementPlanController>(c => c.Detail(waterQualityManagementPlan)));
        }

        private ViewResult ViewNewWqmpVerify(WaterQualityManagementPlan waterQualityManagementPlan, NewWqmpVerifyViewModel viewModel)
        {
            var waterQualityManagementPlanVerifyTypes = HttpRequestStorage.DatabaseEntities.WaterQualityManagementPlanVerifyTypes.ToList();
            var waterQualityManagementPlanVisitStatuses = HttpRequestStorage.DatabaseEntities.WaterQualityManagementPlanVisitStatuses.ToList();
            var waterQualityManagementPlanVerifyStatuses = HttpRequestStorage.DatabaseEntities.WaterQualityManagementPlanVerifyStatuses.ToList();
            var viewData = new NewWqmpVerifyViewData(CurrentPerson, waterQualityManagementPlan, waterQualityManagementPlanVerifyTypes, waterQualityManagementPlanVisitStatuses, waterQualityManagementPlanVerifyStatuses);
            return RazorView<NewWqmpVerify, NewWqmpVerifyViewData, NewWqmpVerifyViewModel>(viewData, viewModel);
        }


        [HttpGet]
        [WaterQualityManagementPlanVerifyManageFeature]
        public ViewResult EditWqmpVerify(WaterQualityManagementPlanVerifyPrimaryKey waterQualityManagementPlanPrimaryKey)
        {
            var waterQualityManagementPlanVerify = waterQualityManagementPlanPrimaryKey.EntityObject;

            var waterQualityManagementPlanVerifyQuickBMPs =
                HttpRequestStorage.DatabaseEntities.WaterQualityManagementPlanVerifyQuickBMPs.Where(x =>
                    x.WaterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyID == waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyID).ToList();
            var waterQualityManagementPlanVerifyTreatmentBMPs = HttpRequestStorage.DatabaseEntities.WaterQualityManagementPlanVerifyTreatmentBMPs.Where(x =>
                x.WaterQualityManagementPlanVerifyID == waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyID).ToList();

            var viewModel = new EditWqmpVerifyViewModel(waterQualityManagementPlanVerify, waterQualityManagementPlanVerifyQuickBMPs, waterQualityManagementPlanVerifyTreatmentBMPs, CurrentPerson);
            return ViewEditWqmpVerify(waterQualityManagementPlanVerify.WaterQualityManagementPlan, viewModel);
        }

        [HttpPost]
        [WaterQualityManagementPlanVerifyManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditWqmpVerify(WaterQualityManagementPlanVerifyPrimaryKey waterQualityManagementPlanPrimaryKey, EditWqmpVerifyViewModel viewModel)
        {
            var waterQualityManagementPlanVerify = waterQualityManagementPlanPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEditWqmpVerify(waterQualityManagementPlanVerify.WaterQualityManagementPlan, viewModel);
            }
            var waterQualityManagementPlan = waterQualityManagementPlanVerify.WaterQualityManagementPlan;
            waterQualityManagementPlanVerify.IsDraft = !viewModel.HiddenIsFinalizeVerificationInput;
            viewModel.UpdateModels(waterQualityManagementPlan, waterQualityManagementPlanVerify, viewModel.DeleteStructuralDocumentFile, viewModel.WaterQualityManagementPlanVerifyQuickBMPSimples, viewModel.WaterQualityManagementPlanVerifyTreatmentBMPSimples, CurrentPerson);

            

            SetMessageForDisplay(
                $"Successfully updated Verification for {waterQualityManagementPlan.WaterQualityManagementPlanName}");

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

        


        [HttpGet]
        [WaterQualityManagementPlanVerifyDeleteFeature]
        public PartialViewResult DeleteVerify(WaterQualityManagementPlanVerifyPrimaryKey waterQualityManagementPlanVerifyPrimaryKey)
        {
            var waterQualityManagementPlanVerify = waterQualityManagementPlanVerifyPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyID);
            return ViewDeleteVerify(waterQualityManagementPlanVerify, viewModel);
        }

        [HttpPost]
        [WaterQualityManagementPlanVerifyDeleteFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteVerify(WaterQualityManagementPlanVerifyPrimaryKey waterQualityManagementPlanVerifyPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var waterQualityManagementPlanVerify = waterQualityManagementPlanVerifyPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteVerify(waterQualityManagementPlanVerify, viewModel);
            }

            var waterQualityManagementPlanQuickBMP = HttpRequestStorage.DatabaseEntities
                .WaterQualityManagementPlanVerifyQuickBMPs.Where(x =>
                    x.WaterQualityManagementPlanVerifyID ==
                    waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyID).ToList();

            var waterQualityManagementPlanTreatmentBMP = HttpRequestStorage.DatabaseEntities
                .WaterQualityManagementPlanVerifyTreatmentBMPs.Where(x =>
                    x.WaterQualityManagementPlanVerifyID ==
                    waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyID).ToList();

            var lastEditedDate = waterQualityManagementPlanVerify.LastEditedDate.ToShortDateString();

            HttpRequestStorage.DatabaseEntities.WaterQualityManagementPlanVerifyQuickBMPs
                .DeleteWaterQualityManagementPlanVerifyQuickBMP(waterQualityManagementPlanQuickBMP);
            HttpRequestStorage.DatabaseEntities.WaterQualityManagementPlanVerifyTreatmentBMPs
                .DeleteWaterQualityManagementPlanVerifyTreatmentBMP(waterQualityManagementPlanTreatmentBMP);

            waterQualityManagementPlanVerify.DeleteFull(HttpRequestStorage.DatabaseEntities);
            SetMessageForDisplay($"Successfully deleted \"{lastEditedDate}\".");

            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewDeleteVerify(WaterQualityManagementPlanVerify waterQualityManagementPlanVerify, ConfirmDialogFormViewModel viewModel)
        {
            var viewData = new ConfirmDialogFormViewData(
                $"Are you sure you want to delete the O&M Verification last edited on {waterQualityManagementPlanVerify.LastEditedDate.ToShortDateString()}?");
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }


        [HttpGet]
        [WaterQualityManagementPlanVerifyManageFeature]
        public PartialViewResult EditWqmpVerifyModal(WaterQualityManagementPlanVerifyPrimaryKey waterQualityManagementPlanVerifyPrimaryKey)
        {
            var waterQualityManagementPlanVerify = waterQualityManagementPlanVerifyPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyID);
            return ViewEditWqmpVerifyModal(viewModel);
        }

        [HttpPost]
        [WaterQualityManagementPlanVerifyDeleteFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditWqmpVerifyModal(WaterQualityManagementPlanVerifyPrimaryKey waterQualityManagementPlanVerifyPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEditWqmpVerifyModal(viewModel);
            }

            return new ModalDialogFormJsonResult(
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(c => c.EditWqmpVerify(waterQualityManagementPlanVerifyPrimaryKey)));
        }

        private PartialViewResult ViewEditWqmpVerifyModal(ConfirmDialogFormViewModel viewModel)
        {
            var viewData = new ConfirmDialogFormViewData(
                "There is a verification in progress. Click OK to resume the existing verification record. Alternately, delete the in-progress verification from the verification panel on the WQMP page");
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }
        #endregion

    }
}
