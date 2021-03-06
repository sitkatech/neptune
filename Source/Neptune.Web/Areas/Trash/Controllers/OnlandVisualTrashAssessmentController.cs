﻿using LtInfo.Common.DbSpatial;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Mvc;
using LtInfo.Common.MvcResults;
using Neptune.Web.Areas.Trash.Views;
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
using LtInfo.Common;
using Neptune.Web.Security.Shared;
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
            var assessmentAreaLayerGeoJson = onlandVisualTrashAssessment.GetAssessmentAreaLayerGeoJson(false);

            var transsectLineLayerGeoJson = onlandVisualTrashAssessment.GetTransectLineLayerGeoJson();

            var ovtaSummaryMapInitJson = new OVTASummaryMapInitJson("summaryMap", observationsLayerGeoJson, assessmentAreaLayerGeoJson, transsectLineLayerGeoJson);
            var returnToEditUrl =
                SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(x =>
                    x.EditStatusToAllowEdit(onlandVisualTrashAssessment));
            var userHasPermissionToReturnToEdit = new OnlandVisualTrashAssessmentEditStatusFeature().HasPermission(CurrentPerson, onlandVisualTrashAssessment).HasPermission;
            return RazorView<Detail, DetailViewData>(new DetailViewData(CurrentPerson, onlandVisualTrashAssessment, new TrashAssessmentSummaryMapViewData(ovtaSummaryMapInitJson, onlandVisualTrashAssessment.OnlandVisualTrashAssessmentObservations, NeptuneWebConfiguration.ParcelMapServiceUrl), returnToEditUrl, userHasPermissionToReturnToEdit));
        }

        [HttpGet]
        [NeptuneViewFeature]
        public ViewResult Index()
        {
            var viewData = new IndexViewData(CurrentPerson,
                NeptunePage.GetNeptunePageByPageType(NeptunePageType.OVTAIndex), SitkaRoute<OnlandVisualTrashAssessmentExportController>.BuildUrlFromExpression(x=>x.ExportAssessmentGeospatialData()));
            return RazorView<Index, IndexViewData>(viewData);
        }

        [NeptuneViewFeature]
        public GridJsonNetJObjectResult<OnlandVisualTrashAssessment> OVTAGridJsonData()
        {
            var stormwaterJurisdictionIDsPersonCanView = CurrentPerson.GetStormwaterJurisdictionIDsPersonCanView();

            if (!stormwaterJurisdictionIDsPersonCanView.Any())
            {
                throw new SitkaRecordNotAuthorizedException(
                    "You are not assigned to any Jurisdictions. Please log out and log in as a different user or request additional permissions");
            }

            var gridSpec = new OnlandVisualTrashAssessmentIndexGridSpec(CurrentPerson,true);
            var onlandVisualTrashAssessments = HttpRequestStorage.DatabaseEntities.OnlandVisualTrashAssessments
                .Where(x => stormwaterJurisdictionIDsPersonCanView.Contains(x.StormwaterJurisdictionID)).ToList()
                .OrderByDescending(x => x.CompletedDate).ThenBy(x => x.OnlandVisualTrashAssessmentArea == null)
                .ThenBy(x => x.OnlandVisualTrashAssessmentArea?.OnlandVisualTrashAssessmentAreaName).ToList();
            return new GridJsonNetJObjectResult<OnlandVisualTrashAssessment>(onlandVisualTrashAssessments, gridSpec);
        }

        [NeptuneViewFeature]
        public GridJsonNetJObjectResult<OnlandVisualTrashAssessmentArea> OnlandVisualTrashAssessmentAreaGridData()
        {
            var stormwaterJurisdictionIDsPersonCanView = CurrentPerson.GetStormwaterJurisdictionIDsPersonCanView();

            if (!stormwaterJurisdictionIDsPersonCanView.Any())
            {
                throw new SitkaRecordNotAuthorizedException(
                    "You are not assigned to any Jurisdictions. Please log out and log in as a different user or request additional permissions");
            }

            var gridSpec = new OnlandVisualTrashAssessmentAreaIndexGridSpec(CurrentPerson);
            var onlandVisualTrashAssessmentAreas = HttpRequestStorage.DatabaseEntities.OnlandVisualTrashAssessmentAreas.Where(x => stormwaterJurisdictionIDsPersonCanView.Contains(x.StormwaterJurisdictionID)).ToList()
                .OrderByDescending(x => x.GetLastAssessmentDate()).ToList();
            return new GridJsonNetJObjectResult<OnlandVisualTrashAssessmentArea>(onlandVisualTrashAssessmentAreas, gridSpec);
        }

        [OnlandVisualTrashAssessmentAreaViewFeature]
        public GridJsonNetJObjectResult<OnlandVisualTrashAssessment> OVTAGridJsonDataForAreaDetails(OnlandVisualTrashAssessmentAreaPrimaryKey onlandVisualTrashAssessmentAreaPrimaryKey)
        {
            var onlandVisualTrashAssessments = GetOVTAsAndGridSpec(out var gridSpec, CurrentPerson, onlandVisualTrashAssessmentAreaPrimaryKey.EntityObject);
            var gridJsonNetJObjectResult =
                new GridJsonNetJObjectResult<OnlandVisualTrashAssessment>(onlandVisualTrashAssessments, gridSpec);
            return gridJsonNetJObjectResult;
        }

        private List<OnlandVisualTrashAssessment> GetOVTAsAndGridSpec(out OnlandVisualTrashAssessmentIndexGridSpec gridSpec, Person currentPerson, OnlandVisualTrashAssessmentArea onlandVisualTrashAssessmentArea)
        {
            gridSpec = new OnlandVisualTrashAssessmentIndexGridSpec(currentPerson, false);
            return HttpRequestStorage.DatabaseEntities.OnlandVisualTrashAssessments.Where(x => x.OnlandVisualTrashAssessmentAreaID == onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaID).OrderByDescending(x => x.CompletedDate).ToList();
        }

        [HttpGet]
        [NeptuneViewFeature]
        public ViewResult Instructions(int? ovtaID) // route "overloaded" so we can handle revisiting and starting anew with the same route.
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
        public ActionResult InitiateOVTA(int? onlandVisualTrashAssessmentPrimaryKey, InitiateOVTAViewModel viewModel)
        {
            var onlandVisualTrashAssessment = onlandVisualTrashAssessmentPrimaryKey.HasValue
                ? HttpRequestStorage.DatabaseEntities.OnlandVisualTrashAssessments.GetOnlandVisualTrashAssessment(
                    onlandVisualTrashAssessmentPrimaryKey.Value)
                : null;
            if (!ModelState.IsValid)
            {
                return ViewInitiateOVTA(onlandVisualTrashAssessment, viewModel);
            }

            if (onlandVisualTrashAssessment == null)
            {
                onlandVisualTrashAssessment = new OnlandVisualTrashAssessment(CurrentPerson.PersonID, DateTime.Now, viewModel.StormwaterJurisdiction.StormwaterJurisdictionID, OnlandVisualTrashAssessmentStatus.InProgress.OnlandVisualTrashAssessmentStatusID, false, false);
                HttpRequestStorage.DatabaseEntities.OnlandVisualTrashAssessments.Add(onlandVisualTrashAssessment);
                HttpRequestStorage.DatabaseEntities.SaveChanges();
            }

            viewModel.UpdateModel(onlandVisualTrashAssessment);

            return RedirectToAppropriateStep(viewModel, OVTASection.InitiateOVTA, onlandVisualTrashAssessment);
        }

        private ViewResult ViewInitiateOVTA(OnlandVisualTrashAssessment onlandVisualTrashAssessment,
            InitiateOVTAViewModel viewModel)
        {
            var stormwaterJurisdictionsPersonCanEdit = CurrentPerson.GetStormwaterJurisdictionsPersonCanView().ToList();

            // do not offer a drop-down menu if the user can only edit one jurisdiction
            var defaultJurisdiction = stormwaterJurisdictionsPersonCanEdit.Count == 1
                ? stormwaterJurisdictionsPersonCanEdit.Single()
                : null;

            var onlandVisualTrashAssessmentAreas = stormwaterJurisdictionsPersonCanEdit.SelectMany(x => x.OnlandVisualTrashAssessmentAreas).ToList();

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

            var assessmentAreaLayerGeoJson = onlandVisualTrashAssessment.GetAssessmentAreaLayerGeoJson(false);

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
                .UnionListGeometries().FixSrid(CoordinateSystemHelper.NAD_83_HARN_CA_ZONE_VI_SRID);
            
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

            // these come in in web mercator...
            var dbGeometrys = viewModel.WktAndAnnotations.Select(x => DbGeometry.FromText(x.Wkt, CoordinateSystemHelper.WGS_1984_SRID).ToSqlGeometry().MakeValid().ToDbGeometry());
            var unionListGeometries = dbGeometrys.ToList().UnionListGeometries();
            
            // ...and then get fixed here
            onlandVisualTrashAssessment.DraftGeometry = CoordinateSystemHelper.ProjectWebMercatorToCaliforniaStatePlaneVI(unionListGeometries);
            onlandVisualTrashAssessment.IsDraftGeometryManuallyRefined = true;
            HttpRequestStorage.DatabaseEntities.SaveChanges();

            return RedirectToAppropriateStep(viewModel, OVTASection.RefineAssessmentArea, onlandVisualTrashAssessment);
        }

        private ViewResult ViewRefineAssessmentArea(OnlandVisualTrashAssessment onlandVisualTrashAssessment, RefineAssessmentAreaViewModel viewModel)
        {
            var observationsLayerGeoJson = onlandVisualTrashAssessment.OnlandVisualTrashAssessmentObservations.MakeObservationsLayerGeoJson();
            var assessmentAreaLayerGeoJson = onlandVisualTrashAssessment.GetAssessmentAreaLayerGeoJson(true);
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
            var assessmentAreaLayerGeoJson = onlandVisualTrashAssessment.GetAssessmentAreaLayerGeoJson(false);

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

            SetMessageForDisplay("The OVTA was successfully finalized");


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
        [OnlandVisualTrashAssessmentEditStatusFeature]
        public PartialViewResult EditStatusToAllowEdit(
            OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey)
        {
            var ovta = onlandVisualTrashAssessmentPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(onlandVisualTrashAssessmentPrimaryKey.PrimaryKeyValue);

            return ViewEditStatusToAllowEdit(ovta, viewModel);
        }

        private PartialViewResult ViewEditStatusToAllowEdit(OnlandVisualTrashAssessment ovta, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage =
                $"This OVTA was finalized on {ovta.CompletedDate}. Are you sure you want to revert this OVTA to the \"In Progress\" status?";

            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData,
                viewModel);
        }

        [HttpPost]
        [OnlandVisualTrashAssessmentEditStatusFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditStatusToAllowEdit(OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey,
            ConfirmDialogFormViewModel viewModel)
        {
            var onlandVisualTrashAssessment = onlandVisualTrashAssessmentPrimaryKey.EntityObject;

            if (!ModelState.IsValid)
            {
                return ViewEditStatusToAllowEdit(onlandVisualTrashAssessment, viewModel);
            }
            onlandVisualTrashAssessment.OnlandVisualTrashAssessmentStatusID = OnlandVisualTrashAssessmentStatus.InProgress.OnlandVisualTrashAssessmentStatusID;
            onlandVisualTrashAssessment.AssessingNewArea = false;



            // update the assessment area scores now that this assessment no longer contributes
            onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentBaselineScoreID =
                onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea.CalculateScoreFromBackingData(false)?
                    .OnlandVisualTrashAssessmentScoreID;

            onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentProgressScoreID =
                onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea.CalculateProgressScore()?
                    .OnlandVisualTrashAssessmentScoreID;

            if (onlandVisualTrashAssessment.IsTransectBackingAssessment)
            {
                onlandVisualTrashAssessment.IsTransectBackingAssessment = false;
                onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea.TransectLine = null;

                HttpRequestStorage.DatabaseEntities.SaveChanges();

                onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea.TransectLine = onlandVisualTrashAssessment
                    .OnlandVisualTrashAssessmentArea.RecomputeTransectLine(out var transectBackingAssessment);


                if (transectBackingAssessment != null)
                {
                    transectBackingAssessment.IsTransectBackingAssessment = true;
                }
            }

            SetMessageForDisplay("The OVTA was successfully returned to the \"In Progress\" status");


            return new ModalDialogFormJsonResult(SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(x =>
                x.RecordObservations(onlandVisualTrashAssessment.OnlandVisualTrashAssessmentID)));

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

            var isProgressAssessment = onlandVisualTrashAssessment.IsProgressAssessment;
            onlandVisualTrashAssessment.DeleteFull(HttpRequestStorage.DatabaseEntities);
            HttpRequestStorage.DatabaseEntities.SaveChanges();
            if (onlandVisualTrashAssessmentArea != null)
            {
                onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentBaselineScoreID = onlandVisualTrashAssessmentArea
                    .CalculateScoreFromBackingData(false)?.OnlandVisualTrashAssessmentScoreID;

                if (isProgressAssessment)
                {
                    onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentProgressScoreID =
                        onlandVisualTrashAssessmentArea.CalculateProgressScore()
                            ?.OnlandVisualTrashAssessmentScoreID;
                }

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

        private ActionResult RedirectToAppropriateStep(OnlandVisualTrashAssessmentViewModel viewModel,
            OVTASection ovtaSection, OnlandVisualTrashAssessment ovta)
        {
            return Redirect(viewModel.AutoAdvance
                ? ovtaSection.GetNextSectionUrl(ovta)
                : ovtaSection.GetSectionUrl(ovta));
        }
    }
}
