using GeoJSON.Net.Feature;
using LtInfo.Common.DbSpatial;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.GeoJson;
using LtInfo.Common.MvcResults;
using Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.Shared;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Spatial;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using System.Xml.XPath;
using LtInfo.Common.Mvc;
using Neptune.Web.Areas.Trash.Views;
using Index = Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment.Index;
using IndexViewData = Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment.IndexViewData;
using OVTASection = Neptune.Web.Models.OVTASection;

namespace Neptune.Web.Areas.Trash.Controllers
{
    public class OnlandVisualTrashAssessmentController : NeptuneBaseController
    {
        [HttpGet]
        [NeptuneViewFeature]
        public ViewResult Detail(OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey)
        {
            var onlandVisualTrashAssessment = onlandVisualTrashAssessmentPrimaryKey.EntityObject;
            Check.Assert(onlandVisualTrashAssessment.OnlandVisualTrashAssessmentStatus == OnlandVisualTrashAssessmentStatus.Complete, "No details are available for this assessment because it has not been completed.");

            var observationsLayerGeoJson = onlandVisualTrashAssessment.OnlandVisualTrashAssessmentObservations.MakeObservationsLayerGeoJson();
            var assessmentAreaLayerGeoJson = GetAssessmentAreaLayerGeoJson(onlandVisualTrashAssessment, false);

            var transsectLineLayerGeoJson = onlandVisualTrashAssessment.GetTransectLineLayerGeoJson();

            var ovtaSummaryMapInitJson = new OVTASummaryMapInitJson("summaryMap", observationsLayerGeoJson, assessmentAreaLayerGeoJson, transsectLineLayerGeoJson);

            return RazorView<Detail, DetailViewData>(new DetailViewData(CurrentPerson, onlandVisualTrashAssessment, new TrashAssessmentSummaryMapViewData(ovtaSummaryMapInitJson, onlandVisualTrashAssessment.OnlandVisualTrashAssessmentObservations, NeptuneWebConfiguration.ParcelMapServiceUrl)));
        }

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
            var onlandVisualTrashAssessments = GetOVTAsAndGridSpec(out var gridSpec, CurrentPerson);
            var gridJsonNetJObjectResult =
                new GridJsonNetJObjectResult<OnlandVisualTrashAssessment>(onlandVisualTrashAssessments, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [NeptuneViewFeature]
        public GridJsonNetJObjectResult<OnlandVisualTrashAssessmentArea> OnlandVisualTrashAssessmentAreaGridData()
        {
            var gridSpec = new OnlandVisualTrashAssessmentAreaIndexGridSpec(CurrentPerson);
            var onlandVisualTrashAssessmentAreas = HttpRequestStorage.DatabaseEntities.OnlandVisualTrashAssessmentAreas.ToList()
                .Where(x => x.StormwaterJurisdiction == null ||
                            CurrentPerson.CanEditStormwaterJurisdiction(x.StormwaterJurisdiction))
                .OrderByDescending(x => x.GetLastAssessmentDate());
            return new GridJsonNetJObjectResult<OnlandVisualTrashAssessmentArea>(onlandVisualTrashAssessmentAreas.ToList(), gridSpec);
        }

        [NeptuneViewFeature]
        public GridJsonNetJObjectResult<OnlandVisualTrashAssessment> OVTAGridJsonDataForAreaDetails(OnlandVisualTrashAssessmentAreaPrimaryKey onlandVisualTrashAssessmentAreaPrimaryKey)
        {
            var onlandVisualTrashAssessments = GetOVTAsAndGridSpec(out var gridSpec, CurrentPerson, onlandVisualTrashAssessmentAreaPrimaryKey.EntityObject);
            var gridJsonNetJObjectResult =
                new GridJsonNetJObjectResult<OnlandVisualTrashAssessment>(onlandVisualTrashAssessments, gridSpec);
            return gridJsonNetJObjectResult;
        }

        private List<OnlandVisualTrashAssessment> GetOVTAsAndGridSpec(out OnlandVisualTrashAssessmentIndexGridSpec gridSpec, Person currentPerson, OnlandVisualTrashAssessmentArea onlandVisualTrashAssessmentArea)
        {
            var showDelete = new JurisdictionManageFeature().HasPermissionByPerson(currentPerson);
            var showEdit = new JurisdictionEditFeature().HasPermissionByPerson(currentPerson);
            gridSpec = new OnlandVisualTrashAssessmentIndexGridSpec(currentPerson, showDelete, showEdit, false);
            return HttpRequestStorage.DatabaseEntities.OnlandVisualTrashAssessments.Where(x=>x.OnlandVisualTrashAssessmentAreaID == onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaID).OrderByDescending(x=>x.CompletedDate).ToList();
        }

        private List<OnlandVisualTrashAssessment> GetOVTAsAndGridSpec(out OnlandVisualTrashAssessmentIndexGridSpec gridSpec,
            Person currentPerson)
        {
            var showDelete = new JurisdictionManageFeature().HasPermissionByPerson(currentPerson);
            var showEdit = new JurisdictionEditFeature().HasPermissionByPerson(currentPerson);
            gridSpec = new OnlandVisualTrashAssessmentIndexGridSpec(currentPerson, showDelete, showEdit, true);

            // if Stormwater Jurisdiction is null, it means the OVTA workflow was started but no data was saved, so it's okay to allow the record to be visible/editable
            // a future release will rearrange the OVTA workflow so records are not created in a jurisdiction-less state
            return HttpRequestStorage.DatabaseEntities.OnlandVisualTrashAssessments.ToList()
                .Where(x => x.StormwaterJurisdiction == null ||
                            CurrentPerson.CanEditStormwaterJurisdiction(x.StormwaterJurisdiction))
                .OrderByDescending(x => x.CompletedDate).ThenBy(x => x.OnlandVisualTrashAssessmentArea == null)
                .ThenBy(x => x.OnlandVisualTrashAssessmentArea?.OnlandVisualTrashAssessmentAreaName).ToList();
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

        private ViewResult ViewInstructions(InstructionsViewModel viewModel, OnlandVisualTrashAssessment ovta)
        {
            var viewData = new InstructionsViewData(CurrentPerson,
                NeptunePage.GetNeptunePageByPageType(NeptunePageType.OVTAInstructions), ovta);
            return RazorView<Instructions, InstructionsViewData, InstructionsViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [NeptuneViewFeature]
        public ViewResult InitiateOVTA(int? onlandVisualTrashAssessmentPrimaryKey)
        {
            var onlandVisualTrashAssessment = onlandVisualTrashAssessmentPrimaryKey.HasValue
                ? HttpRequestStorage.DatabaseEntities.OnlandVisualTrashAssessments.GetOnlandVisualTrashAssessment(
                    onlandVisualTrashAssessmentPrimaryKey.Value)
                : null;
            var viewModel = new InitiateOVTAViewModel(onlandVisualTrashAssessment, CurrentPerson);

            return ViewInitiateOVTA(onlandVisualTrashAssessment, viewModel);
        }

        [HttpPost]
        [NeptuneViewFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult InitiateOVTA(int? onlandVisualTrashAssessmentPrimaryKey,
            InitiateOVTAViewModel viewModel)
        {
            var onlandVisualTrashAssessment =  onlandVisualTrashAssessmentPrimaryKey.HasValue
                ? HttpRequestStorage.DatabaseEntities.OnlandVisualTrashAssessments.GetOnlandVisualTrashAssessment(
                    onlandVisualTrashAssessmentPrimaryKey.Value)
                : null;
            if (!ModelState.IsValid)
            {
                return ViewInitiateOVTA(onlandVisualTrashAssessment, viewModel);
            }

            if (onlandVisualTrashAssessment == null)
            {
                onlandVisualTrashAssessment = new OnlandVisualTrashAssessment(CurrentPerson, DateTime.Now, OnlandVisualTrashAssessmentStatus.InProgress);
                HttpRequestStorage.DatabaseEntities.OnlandVisualTrashAssessments.Add(onlandVisualTrashAssessment);
                HttpRequestStorage.DatabaseEntities.SaveChanges();
            }

            viewModel.UpdateModel(onlandVisualTrashAssessment);

            return RedirectToAppropriateStep(viewModel, OVTASection.InitiateOVTA, onlandVisualTrashAssessment);
        }

        private ViewResult ViewInitiateOVTA(OnlandVisualTrashAssessment onlandVisualTrashAssessment,
            InitiateOVTAViewModel viewModel)
        {
            var stormwaterJurisdictionsPersonCanEdit = CurrentPerson.GetStormwaterJurisdictionsPersonCanEdit().ToList();

            // do not offer a drop-down menu if the user can only edit one jurisdiction
            var defaultJurisdiction = stormwaterJurisdictionsPersonCanEdit.Count == 1
                ? stormwaterJurisdictionsPersonCanEdit.Single()
                : null;

            var onlandVisualTrashAssessmentAreas = HttpRequestStorage.DatabaseEntities.OnlandVisualTrashAssessmentAreas
                .ToList()
                .Where(x => stormwaterJurisdictionsPersonCanEdit.Contains(x.StormwaterJurisdiction)).ToList();

            var mapInitJson = new SelectOVTAAreaMapInitJson("selectOVTAAreaMap",
                onlandVisualTrashAssessmentAreas.MakeAssessmentAreasLayerGeoJson());

            var viewData = new InitiateOVTAViewData(CurrentPerson,
                onlandVisualTrashAssessment, stormwaterJurisdictionsPersonCanEdit, mapInitJson, onlandVisualTrashAssessmentAreas,
                defaultJurisdiction, NeptuneWebConfiguration.ParcelMapServiceUrl);
            return RazorView<InitiateOVTA, InitiateOVTAViewData, InitiateOVTAViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [OnlandVisualTrashAssessmentEditFeature]
        public ViewResult RecordObservations(
            OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey)
        {
            var onlandVisualTrashAssessment = onlandVisualTrashAssessmentPrimaryKey.EntityObject;
            var viewModel = new RecordObservationsViewModel(onlandVisualTrashAssessment);
            return ViewRecordObservations(onlandVisualTrashAssessment, viewModel);
        }

        [HttpPost]
        [OnlandVisualTrashAssessmentEditFeature]
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

            var assessmentAreaLayerGeoJson = GetAssessmentAreaLayerGeoJson(onlandVisualTrashAssessment, false);

            var transectLineLayerGeoJson = onlandVisualTrashAssessment.GetTransectLineLayerGeoJson();

            var ovtaObservationsMapInitJson = new OVTAObservationsMapInitJson("observationsMap",
                observationsLayerGeoJson, assessmentAreaLayerGeoJson, transectLineLayerGeoJson);

            var viewData = new RecordObservationsViewData(CurrentPerson,
                onlandVisualTrashAssessment, ovtaObservationsMapInitJson, NeptuneWebConfiguration.ParcelMapServiceUrl);
            return RazorView<RecordObservations, RecordObservationsViewData, RecordObservationsViewModel>(viewData,
                viewModel);
        }

        [HttpGet]
        [OnlandVisualTrashAssessmentEditFeature]
        public ViewResult AddOrRemoveParcels(
            OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey)
        {
            var onlandVisualTrashAssessment = onlandVisualTrashAssessmentPrimaryKey.EntityObject;

            var parcelIDs = onlandVisualTrashAssessment.GetParcelIDsForAddOrRemoveParcels();

            var viewModel = new AddOrRemoveParcelsViewModel(parcelIDs);
            return ViewAddOrRemoveParcels(onlandVisualTrashAssessment, viewModel);
        }

        [HttpPost]
        [OnlandVisualTrashAssessmentEditFeature]
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
                onlandVisualTrashAssessment.OnlandVisualTrashAssessmentObservations.MakeObservationsLayerGeoJson(), onlandVisualTrashAssessment.GetTransectLineLayerGeoJson());

            var viewData = new AddOrRemoveParcelsViewData(CurrentPerson, OVTASection.AddOrRemoveParcels,
                onlandVisualTrashAssessment, addOrRemoveParcelsMapIntJson);
            return RazorView<AddOrRemoveParcels, AddOrRemoveParcelsViewData, AddOrRemoveParcelsViewModel>(viewData,
                viewModel);
        }

        [HttpGet]
        [OnlandVisualTrashAssessmentEditFeature]
        public ViewResult RefineAssessmentArea(OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey)
        {
            var onlandVisualTrashAssessment = onlandVisualTrashAssessmentPrimaryKey.EntityObject;
            var viewModel = new RefineAssessmentAreaViewModel();
            return ViewRefineAssessmentArea(onlandVisualTrashAssessment, viewModel);
        }

        [HttpPost]
        [OnlandVisualTrashAssessmentEditFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult RefineAssessmentArea(OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey, RefineAssessmentAreaViewModel viewModel)
        {
            var onlandVisualTrashAssessment = onlandVisualTrashAssessmentPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewRefineAssessmentArea(onlandVisualTrashAssessment, viewModel);
            }

            var dbGeometrys = viewModel.WktAndAnnotations.Select(x => DbGeometry.FromText(x.Wkt, 4326).ToSqlGeometry().MakeValid().ToDbGeometry());
            var unionListGeometries = dbGeometrys.ToList().UnionListGeometries();

            onlandVisualTrashAssessment.DraftGeometry = unionListGeometries;
            onlandVisualTrashAssessment.IsDraftGeometryManuallyRefined = true;
            HttpRequestStorage.DatabaseEntities.SaveChanges();

            return RedirectToAppropriateStep(viewModel, OVTASection.RefineAssessmentArea, onlandVisualTrashAssessment);
        }

        private ViewResult ViewRefineAssessmentArea(OnlandVisualTrashAssessment onlandVisualTrashAssessment, RefineAssessmentAreaViewModel viewModel)
        {
            var observationsLayerGeoJson = onlandVisualTrashAssessment.OnlandVisualTrashAssessmentObservations.MakeObservationsLayerGeoJson();
            var assessmentAreaLayerGeoJson = GetAssessmentAreaLayerGeoJson(onlandVisualTrashAssessment, true);
            var transectLineLayerGeoJson = onlandVisualTrashAssessment.GetTransectLineLayerGeoJson();
            var refineAssessmentAreaMapInitJson = new RefineAssessmentAreaMapInitJson("refineAssessmentAreaMap", observationsLayerGeoJson, assessmentAreaLayerGeoJson, transectLineLayerGeoJson);

            var viewData = new RefineAssessmentAreaViewData(CurrentPerson, OVTASection.RefineAssessmentArea, onlandVisualTrashAssessment, refineAssessmentAreaMapInitJson, NeptuneWebConfiguration.ParcelMapServiceUrl);
            return RazorView<RefineAssessmentArea, RefineAssessmentAreaViewData, RefineAssessmentAreaViewModel>(
                viewData, viewModel);
        }

        [HttpGet]
        [OnlandVisualTrashAssessmentEditFeature]
        public ViewResult FinalizeOVTA(OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey)
        {
            var onlandVisualTrashAssessment = onlandVisualTrashAssessmentPrimaryKey.EntityObject;
            var viewModel = new FinalizeOVTAViewModel(onlandVisualTrashAssessment);

            return ViewFinalizeOVTA(onlandVisualTrashAssessment, viewModel);
        }

        private ViewResult ViewFinalizeOVTA(OnlandVisualTrashAssessment onlandVisualTrashAssessment,
            FinalizeOVTAViewModel viewModel)
        {
            var observationsLayerGeoJson = onlandVisualTrashAssessment.OnlandVisualTrashAssessmentObservations.MakeObservationsLayerGeoJson();
            var assessmentAreaLayerGeoJson = GetAssessmentAreaLayerGeoJson(onlandVisualTrashAssessment, false);

            var transsectLineLayerGeoJson = onlandVisualTrashAssessment.GetTransectLineLayerGeoJson();

            var ovtaSummaryMapInitJson = new OVTASummaryMapInitJson("summaryMap", observationsLayerGeoJson, assessmentAreaLayerGeoJson, transsectLineLayerGeoJson);

            var scoresSelectList = OnlandVisualTrashAssessmentScore.All.ToSelectListWithDisabledEmptyFirstRow(x => x.OnlandVisualTrashAssessmentScoreID.ToString(CultureInfo.InvariantCulture), x => x.OnlandVisualTrashAssessmentScoreDisplayName.ToString(CultureInfo.InvariantCulture),
                "Choose a score");
            var viewData = new FinalizeOVTAViewData(CurrentPerson,
                onlandVisualTrashAssessment, ovtaSummaryMapInitJson, scoresSelectList, NeptuneWebConfiguration.ParcelMapServiceUrl);
            return RazorView<FinalizeOVTA, FinalizeOVTAViewData, FinalizeOVTAViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [OnlandVisualTrashAssessmentEditFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult FinalizeOVTA(OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey,
            FinalizeOVTAViewModel viewModel)
        {
            var onlandVisualTrashAssessment = onlandVisualTrashAssessmentPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewFinalizeOVTA(onlandVisualTrashAssessment, viewModel);
            }

            HttpRequestStorage.DatabaseEntities.OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypes.Load();
            viewModel.UpdateModel(onlandVisualTrashAssessment, HttpRequestStorage.DatabaseEntities.OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypes.Local);

            if (viewModel.Finalize.GetValueOrDefault())
            {
                return Redirect(
                    SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(x => x.Detail(onlandVisualTrashAssessment)));
            }
            else
            {
                return Redirect(
                    SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(x =>
                        x.FinalizeOVTA(onlandVisualTrashAssessment)));
            }
        }

        [HttpGet]
        [OnlandVisualTrashAssessmentEditFeature]
        public PartialViewResult RefreshParcels(
            OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey)
        {
            var viewModel = new ConfirmDialogFormViewModel(onlandVisualTrashAssessmentPrimaryKey.PrimaryKeyValue);

            return ViewRefreshParcels(viewModel);
        }

        private PartialViewResult ViewRefreshParcels(ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage =
                "Are you sure you want to reset the Assessment Area? Any manual changes to the Assessment Area, including adding/removing Parcels or adjusting boundaries, will be discarded. The Assessment Area will be reset to just the parcels transacted by the observation points. This action cannot be undone.";

            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData,
                viewModel);
        }

        [HttpPost]
        [OnlandVisualTrashAssessmentEditFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult RefreshParcels(OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey,
            ConfirmDialogFormViewModel viewModel) //note that the viewModel is not actually used; we only need it to satisfy our opinionated routetablebuilder
        {
            var onlandVisualTrashAssessment = onlandVisualTrashAssessmentPrimaryKey.EntityObject;

            if (!ModelState.IsValid)
            {
                return ViewRefreshParcels(viewModel);
            }
            onlandVisualTrashAssessment.IsDraftGeometryManuallyRefined = false;
            onlandVisualTrashAssessment.DraftGeometry = null;

            return new ModalDialogFormJsonResult(
                SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(x =>
                    x.AddOrRemoveParcels(onlandVisualTrashAssessment)));
        }

        [HttpGet]
        [OnlandVisualTrashAssessmentDeleteFeature]
        public PartialViewResult Delete(OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey)
        {
            var onlandVisualTrashAssessment = onlandVisualTrashAssessmentPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(onlandVisualTrashAssessment.OnlandVisualTrashAssessmentID);
            return ViewDeleteOnlandVisualTrashAssessment(onlandVisualTrashAssessment, viewModel);
        }

        [HttpPost]
        [OnlandVisualTrashAssessmentDeleteFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Delete(OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var onlandVisualTrashAssessment = onlandVisualTrashAssessmentPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteOnlandVisualTrashAssessment(onlandVisualTrashAssessment, viewModel);
            }

            var onlandVisualTrashAssessmentArea = onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea;
            onlandVisualTrashAssessment.DeleteFull(HttpRequestStorage.DatabaseEntities);
            HttpRequestStorage.DatabaseEntities.SaveChanges();
            if (onlandVisualTrashAssessmentArea != null)
            {
                onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentScoreID = onlandVisualTrashAssessmentArea
                    .CalculateScoreFromBackingData()?.OnlandVisualTrashAssessmentScoreID;
            }

            SetMessageForDisplay("Successfully deleted the assessment.");
            return new ModalDialogFormJsonResult(SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(c => c.Index()));
        }

        private PartialViewResult ViewDeleteOnlandVisualTrashAssessment(OnlandVisualTrashAssessment onlandVisualTrashAssessment, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = $"Are you sure you want to delete the assessment from {onlandVisualTrashAssessment.CreatedDate.ToShortDateString()}?";

            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpGet]
        public PartialViewResult ScoreDescriptions()
        {
            var viewData = new TrashModuleViewData(CurrentPerson);
            return RazorPartialView<ScoreDescriptions, TrashModuleViewData>(viewData);
        }

        // helpers

        // assumes that we are not looking for the parcels-via-transect area
        private static LayerGeoJson GetAssessmentAreaLayerGeoJson(OnlandVisualTrashAssessment onlandVisualTrashAssessment, bool reduce)
        {
            FeatureCollection geoJsonFeatureCollection;
            if (onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea != null)
            {
                geoJsonFeatureCollection =
                    new List<OnlandVisualTrashAssessmentArea> { onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea }
                        .ToGeoJsonFeatureCollection();
            }
            else if (onlandVisualTrashAssessment.DraftGeometry != null)
            {
                var draftGeometry = onlandVisualTrashAssessment.DraftGeometry;
                geoJsonFeatureCollection = new FeatureCollection();

                // Leaflet.Draw does not support multipolgyon editing because its dev team decided it wasn't necessary.
                // Unless https://github.com/Leaflet/Leaflet.draw/issues/268 is resolved, we have to break into separate polys.
                // On an unrelated note, DbGeometry.ElementAt is 1-indexed instead of 0-indexed, which is terrible.
                for (var i = 1; i <= draftGeometry.ElementCount.GetValueOrDefault(); i++)
                {
                    var dbGeometry = draftGeometry.ElementAt(i);
                    if (reduce)
                    {
                        dbGeometry = dbGeometry.ToSqlGeometry().Reduce(.0000025).ToDbGeometry();
                    }
                    var feature = DbGeometryToGeoJsonHelper.FromDbGeometry(dbGeometry);
                    geoJsonFeatureCollection.Features.Add(feature);
                }
            }
            else
            {
                geoJsonFeatureCollection = new FeatureCollection();
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
                ? ovtaSection.GetNextSectionUrl(ovta)
                : ovtaSection.GetSectionUrl(ovta));
        }
    }
}
