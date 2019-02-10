using LtInfo.Common.DbSpatial;
using LtInfo.Common.Mvc;
using LtInfo.Common.MvcResults;
using Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.Shared;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using GeoJSON.Net.Feature;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.GeoJson;
using Index = Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment.Index;
using IndexViewData = Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment.IndexViewData;
using OVTASection = Neptune.Web.Models.OVTASection;

namespace Neptune.Web.Areas.Trash.Controllers
{
    public class OnlandVisualTrashAssessmentController : NeptuneBaseController
    {
        [HttpGet]
        [NeptuneViewFeature]
        public ViewResult Index()
        {
            var viewData = new IndexViewData(CurrentPerson,
                NeptunePage.GetNeptunePageByPageType(NeptunePageType.OVTAIndex));
            return RazorView<Index, IndexViewData>(viewData);
        }

        [NeptuneViewFeature]
        public GridJsonNetJObjectResult<OnlandVisualTrashAssessment> OVTAGridJsonData()
        {
            var treatmentBMPs = GetOVTAsAndGridSpec(out var gridSpec, CurrentPerson);
            var gridJsonNetJObjectResult =
                new GridJsonNetJObjectResult<OnlandVisualTrashAssessment>(treatmentBMPs, gridSpec);
            return gridJsonNetJObjectResult;
        }

        private List<OnlandVisualTrashAssessment> GetOVTAsAndGridSpec(out OVTAIndexGridSpec gridSpec,
            Person currentPerson)
        {
            var showDelete = new JurisdictionManageFeature().HasPermissionByPerson(currentPerson);
            var showEdit = new JurisdictionEditFeature().HasPermissionByPerson(currentPerson);
            gridSpec = new OVTAIndexGridSpec(currentPerson, showDelete, showEdit);
            return HttpRequestStorage.DatabaseEntities.OnlandVisualTrashAssessments.ToList();
        }

        [HttpGet]
        [NeptuneViewFeature]
        public ViewResult
            Instructions(
                int? ovtaID) // route "overloaded" so we can handle revisiting and starting anew with the same route.
        {
            var viewModel = new InstructionsViewModel();

            var onlandVisualTrashAssessment = ovtaID.HasValue
                ? HttpRequestStorage.DatabaseEntities.OnlandVisualTrashAssessments.GetOnlandVisualTrashAssessment(
                    ovtaID.Value)
                : null;

            return ViewInstructions(viewModel, onlandVisualTrashAssessment);
        }

        [HttpPost]
        [NeptuneViewFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Instructions(int? ovtaID, InstructionsViewModel viewModel)
        {
            OnlandVisualTrashAssessment onlandVisualTrashAssessment;
            if (ovtaID.HasValue)
            {
                onlandVisualTrashAssessment =
                    HttpRequestStorage.DatabaseEntities.OnlandVisualTrashAssessments.GetOnlandVisualTrashAssessment(
                        ovtaID.Value);
            }
            else
            {
                onlandVisualTrashAssessment = new OnlandVisualTrashAssessment(CurrentPerson, DateTime.Now, OnlandVisualTrashAssessmentStatus.InProgress);
                HttpRequestStorage.DatabaseEntities.OnlandVisualTrashAssessments.Add(onlandVisualTrashAssessment);
                HttpRequestStorage.DatabaseEntities.SaveChanges();
            }

            return RedirectToAppropriateStep(viewModel, OVTASection.Instructions, onlandVisualTrashAssessment);
        }

        private ViewResult ViewInstructions(InstructionsViewModel viewModel, OnlandVisualTrashAssessment ovta)
        {
            var viewData = new InstructionsViewData(CurrentPerson,
                NeptunePage.GetNeptunePageByPageType(NeptunePageType.OVTAInstructions), ovta);
            return RazorView<Instructions, InstructionsViewData, InstructionsViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [NeptuneViewFeature]
        public ViewResult InitiateOVTA(OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey)
        {
            var onlandVisualTrashAssessment = onlandVisualTrashAssessmentPrimaryKey.EntityObject;
            var viewModel = new InitiateOVTAViewModel(onlandVisualTrashAssessment);

            return ViewInitiateOVTA(onlandVisualTrashAssessment, viewModel);
        }

        private ViewResult ViewInitiateOVTA(OnlandVisualTrashAssessment onlandVisualTrashAssessment,
            InitiateOVTAViewModel viewModel)
        {
            var stormwaterJurisdictionsPersonCanEdit = CurrentPerson.GetStormwaterJurisdictionsPersonCanEdit().ToList();

            // do not offer a drop-down menu if the user can only edit one jurisdiction
            var defaultJurisdiction = stormwaterJurisdictionsPersonCanEdit.Count == 1
                ? stormwaterJurisdictionsPersonCanEdit.Single()
                : null;

            var jurisdictionsSelectList = stormwaterJurisdictionsPersonCanEdit
                .ToSelectListWithDisabledEmptyFirstRow(
                    j => j.StormwaterJurisdictionID.ToString(CultureInfo.InvariantCulture),
                    j => j.GetOrganizationDisplayName(), "Choose a Jurisdiction");

            var onlandVisualTrashAssessmentAreas = HttpRequestStorage.DatabaseEntities.OnlandVisualTrashAssessmentAreas
                .ToList()
                .Where(x => stormwaterJurisdictionsPersonCanEdit.Contains(x.StormwaterJurisdiction)).ToList();

            var mapInitJson = new SelectOVTAAreaMapInitJson("selectOVTAAreaMap",
                onlandVisualTrashAssessmentAreas.MakeAssessmentAreasLayerGeoJson());

            var viewData = new InitiateOVTAViewData(CurrentPerson,
                onlandVisualTrashAssessment, jurisdictionsSelectList, mapInitJson, onlandVisualTrashAssessmentAreas,
                defaultJurisdiction);
            return RazorView<InitiateOVTA, InitiateOVTAViewData, InitiateOVTAViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [NeptuneViewFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult InitiateOVTA(OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey,
            InitiateOVTAViewModel viewModel)
        {
            var onlandVisualTrashAssessment = onlandVisualTrashAssessmentPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                viewModel.StormwaterJurisdictionID = onlandVisualTrashAssessment.StormwaterJurisdictionID;
                return ViewInitiateOVTA(onlandVisualTrashAssessment, viewModel);
            }

            viewModel.UpdateModel(onlandVisualTrashAssessment);

            return RedirectToAppropriateStep(viewModel, OVTASection.InitiateOVTA, onlandVisualTrashAssessment);
        }

        [HttpGet]
        [NeptuneViewFeature]
        public ViewResult RecordObservations(
            OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey)
        {
            var onlandVisualTrashAssessment = onlandVisualTrashAssessmentPrimaryKey.EntityObject;
            var viewModel = new RecordObservationsViewModel(onlandVisualTrashAssessment);
            return ViewRecordObservations(onlandVisualTrashAssessment, viewModel);
        }

        [HttpPost]
        [NeptuneViewFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult RecordObservations(
            OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey,
            RecordObservationsViewModel viewModel)
        {
            var onlandVisualTrashAssessment = onlandVisualTrashAssessmentPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewRecordObservations(onlandVisualTrashAssessment, viewModel);
            }

            var allOnlandVisualTrashAssessmentObservations =
                HttpRequestStorage.DatabaseEntities.OnlandVisualTrashAssessmentObservations;
            viewModel.UpdateModel(onlandVisualTrashAssessment, allOnlandVisualTrashAssessmentObservations.Local);

            return RedirectToAppropriateStep(viewModel, OVTASection.RecordObservations,
                onlandVisualTrashAssessmentPrimaryKey.EntityObject);
        }

        private ViewResult ViewRecordObservations(OnlandVisualTrashAssessment onlandVisualTrashAssessment,
            RecordObservationsViewModel viewModel)
        {
            var observationsLayerGeoJson =
                onlandVisualTrashAssessment
                    .OnlandVisualTrashAssessmentObservations.MakeObservationsLayerGeoJson();

            var assessmentAreaLayerGeoJson = GetAssessmentAreaLayerGeoJson(onlandVisualTrashAssessment);

            var ovtaObservationsMapInitJson = new OVTAObservationsMapInitJson("observationsMap",
                observationsLayerGeoJson, assessmentAreaLayerGeoJson);

            var viewData = new RecordObservationsViewData(CurrentPerson,
                onlandVisualTrashAssessment, ovtaObservationsMapInitJson);
            return RazorView<RecordObservations, RecordObservationsViewData, RecordObservationsViewModel>(viewData,
                viewModel);
        }

        [HttpGet]
        [NeptuneViewFeature]
        public ViewResult AddOrRemoveParcels(
            OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey)
        {
            var onlandVisualTrashAssessment = onlandVisualTrashAssessmentPrimaryKey.EntityObject;

            var parcelIDs = onlandVisualTrashAssessment.GetParcelIDsForAddOrRemoveParcels();

            var viewModel = new AddOrRemoveParcelsViewModel(parcelIDs);
            return ViewAddOrRemoveParcels(onlandVisualTrashAssessment, viewModel);
        }

        [HttpPost]
        [NeptuneViewFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult AddOrRemoveParcels(
            OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey,
            AddOrRemoveParcelsViewModel viewModel)
        {
            var onlandVisualTrashAssessment = onlandVisualTrashAssessmentPrimaryKey.EntityObject;

            if (!ModelState.IsValid)
            {
                return ViewAddOrRemoveParcels(onlandVisualTrashAssessment, viewModel);
            }

            var unionOfSelectedParcelGeometries = HttpRequestStorage.DatabaseEntities.Parcels
                .Where(x => viewModel.ParcelIDs.Contains(x.ParcelID)).Select(x => x.ParcelGeometry).ToList()
                .UnionListGeometries();
            onlandVisualTrashAssessment.DraftGeometry = unionOfSelectedParcelGeometries;

            return RedirectToAppropriateStep(viewModel, OVTASection.AddOrRemoveParcels,
                onlandVisualTrashAssessment);
        }

        private ViewResult ViewAddOrRemoveParcels(OnlandVisualTrashAssessment onlandVisualTrashAssessment,
            AddOrRemoveParcelsViewModel viewModel)
        {
            var addOrRemoveParcelsMapIntJson = new AddOrRemoveParcelsMapIntJson("addOrRemoveParcelsMap",
                onlandVisualTrashAssessment.OnlandVisualTrashAssessmentObservations.MakeObservationsLayerGeoJson());

            var viewData = new AddOrRemoveParcelsViewData(CurrentPerson, OVTASection.AddOrRemoveParcels,
                onlandVisualTrashAssessment, addOrRemoveParcelsMapIntJson);
            return RazorView<AddOrRemoveParcels, AddOrRemoveParcelsViewData, AddOrRemoveParcelsViewModel>(viewData,
                viewModel);
        }

        [HttpGet]
        [NeptuneViewFeature]
        public ViewResult RefineAssessmentArea(OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey)
        {
            var onlandVisualTrashAssessment = onlandVisualTrashAssessmentPrimaryKey.EntityObject;
            var viewModel = new RefineAssessmentAreaViewModel();
            return ViewRefineAssessmentArea(onlandVisualTrashAssessment, viewModel);
        }

        [HttpPost]
        [NeptuneViewFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult RefineAssessmentArea(OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey, RefineAssessmentAreaViewModel viewModel)
        {
            var onlandVisualTrashAssessment = onlandVisualTrashAssessmentPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewRefineAssessmentArea(onlandVisualTrashAssessment, viewModel);
            }
            
            

            return RedirectToAppropriateStep(viewModel, OVTASection.RefineAssessmentArea, onlandVisualTrashAssessment);
        }

        private ViewResult ViewRefineAssessmentArea(OnlandVisualTrashAssessment onlandVisualTrashAssessment, RefineAssessmentAreaViewModel viewModel)
        {
            var viewData = new RefineAssessmentAreaViewData(CurrentPerson, OVTASection.RefineAssessmentArea, onlandVisualTrashAssessment);

            return RazorView<RefineAssessmentArea, RefineAssessmentAreaViewData, RefineAssessmentAreaViewModel>(
                viewData, viewModel);
        }

        [HttpGet]
        [NeptuneViewFeature]
        public ViewResult FinalizeOVTA(OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey)
        {
            var onlandVisualTrashAssessment = onlandVisualTrashAssessmentPrimaryKey.EntityObject;
            var viewModel = new FinalizeOVTAViewModel(onlandVisualTrashAssessment);

            return ViewFinalizeOVTA(onlandVisualTrashAssessment, viewModel);
        }

        private ViewResult ViewFinalizeOVTA(OnlandVisualTrashAssessment onlandVisualTrashAssessment,
            FinalizeOVTAViewModel viewModel)
        {
            var ovtaSummaryMapInitJson = GetOVTASummaryMapInitJson(onlandVisualTrashAssessment);

            var viewData = new FinalizeOVTAViewData(CurrentPerson,
                onlandVisualTrashAssessment, ovtaSummaryMapInitJson);
            return RazorView<FinalizeOVTA, FinalizeOVTAViewData, FinalizeOVTAViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [NeptuneViewFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult FinalizeOVTA(OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey,
            FinalizeOVTAViewModel viewModel)
        {
            var onlandVisualTrashAssessment = onlandVisualTrashAssessmentPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewFinalizeOVTA(onlandVisualTrashAssessment, viewModel);
            }

            if (onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea == null)
            {
                var assessmentAreaGeometry = onlandVisualTrashAssessment.GetAreaViaTransect();
                var onlandVisualTrashAssessmentArea = new OnlandVisualTrashAssessmentArea(viewModel.AssessmentAreaName,
                    onlandVisualTrashAssessment.StormwaterJurisdiction, assessmentAreaGeometry);

                HttpRequestStorage.DatabaseEntities.OnlandVisualTrashAssessmentAreas.Add(
                    onlandVisualTrashAssessmentArea);
                HttpRequestStorage.DatabaseEntities.SaveChanges();
                onlandVisualTrashAssessment.OnlandVisualTrashAssessmentAreaID =
                    onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaID;
                onlandVisualTrashAssessment.AssessingNewArea = false;
            }
            else
            {
                viewModel.UpdateModel(onlandVisualTrashAssessment);
            }

            return Redirect(SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(x => x.Index()));
        }

        [HttpGet]
        [NeptuneViewFeature]
        public PartialViewResult RefreshParcels(
            OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey)
        {
            var viewModel = new ConfirmDialogFormViewModel(onlandVisualTrashAssessmentPrimaryKey.PrimaryKeyValue);

            return ViewRefreshParcels(viewModel);
        }

        private PartialViewResult ViewRefreshParcels(ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = "Are you sure you want to reset the assessment area? This action cannot be undone.";

            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData,
                viewModel);
        }

        [HttpPost]
        [NeptuneViewFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult RefreshParcels(OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey,
            ConfirmDialogFormViewModel viewModel) //note that the viewModel is not actually used; we only need it to satisfy our opinionated routetablebuilder
        {
            var onlandVisualTrashAssessment = onlandVisualTrashAssessmentPrimaryKey.EntityObject;
            Check.RequireNotNull(onlandVisualTrashAssessment.DraftGeometry,
                "Cannot refresh Assessment Area: Assessment Area not yet created");

            if (!ModelState.IsValid)
            {
                return ViewRefreshParcels(viewModel);
            }

            var onlandVisualTrashAssessmentAreaToDelete = onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea;
            onlandVisualTrashAssessment.OnlandVisualTrashAssessmentAreaID = null;
            HttpRequestStorage.DatabaseEntities.SaveChanges();
            HttpRequestStorage.DatabaseEntities.OnlandVisualTrashAssessmentAreas.DeleteOnlandVisualTrashAssessmentArea(
                onlandVisualTrashAssessmentAreaToDelete);

            return new ModalDialogFormJsonResult(
                SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(x =>
                    x.AddOrRemoveParcels(onlandVisualTrashAssessment)));
        }

        // helpers

        private static OVTASummaryMapInitJson GetOVTASummaryMapInitJson(
            OnlandVisualTrashAssessment onlandVisualTrashAssessment)
        {
            var observationsLayerGeoJson = onlandVisualTrashAssessment.OnlandVisualTrashAssessmentObservations.MakeObservationsLayerGeoJson();
            
            var assessmentAreaLayerGeoJson = GetAssessmentAreaLayerGeoJson(onlandVisualTrashAssessment);

            var ovtaSummaryMapInitJson = new OVTASummaryMapInitJson("summaryMap", observationsLayerGeoJson, assessmentAreaLayerGeoJson);

            return ovtaSummaryMapInitJson;
        }

        // assumes that we are not looking for the parcels-via-transect area
        private static LayerGeoJson GetAssessmentAreaLayerGeoJson(OnlandVisualTrashAssessment onlandVisualTrashAssessment)
        {
            FeatureCollection geoJsonFeatureCollection;
            if (onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea != null)
            {
                geoJsonFeatureCollection =
                    new List<OnlandVisualTrashAssessmentArea> {onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea}
                        .ToGeoJsonFeatureCollection();
            }
            else
            {
                var draftGeometry = onlandVisualTrashAssessment.DraftGeometry;
                var feature = DbGeometryToGeoJsonHelper.FromDbGeometry(draftGeometry);
                geoJsonFeatureCollection = new FeatureCollection();
                geoJsonFeatureCollection.Features.Add(feature);
            }

            var assessmentAreaLayerGeoJson = new LayerGeoJson("parcels", geoJsonFeatureCollection,
                "#ffff00", .5m,
                LayerInitialVisibility.Show);
            return assessmentAreaLayerGeoJson;
        }

        private ActionResult RedirectToAppropriateStep(OnlandVisualTrashAssessmentViewModel viewModel,
            OVTASection ovtaSection, OnlandVisualTrashAssessment ovta)
        {
            return Redirect(viewModel.AutoAdvance
                ? ovtaSection.GetNextSection().GetSectionUrl(ovta)
                : ovtaSection.GetSectionUrl(ovta));
        }
    }
}
