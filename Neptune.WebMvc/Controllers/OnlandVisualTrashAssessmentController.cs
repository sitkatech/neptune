﻿using System.Data;
using System.Globalization;
using ClosedXML.Excel;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Neptune.Common;
using Neptune.Common.DesignByContract;
using Neptune.Common.GeoSpatial;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.Models;
using Neptune.WebMvc.Common.Mvc;
using Neptune.WebMvc.Common.MvcResults;
using Neptune.WebMvc.Models;
using Neptune.WebMvc.Security;
using Neptune.WebMvc.Services;
using Neptune.WebMvc.Services.Filters;
using Neptune.WebMvc.Views;
using Neptune.WebMvc.Views.OnlandVisualTrashAssessment;
using Neptune.WebMvc.Views.OnlandVisualTrashAssessment.MapInitJson;
using Neptune.WebMvc.Views.Shared;
using Index = Neptune.WebMvc.Views.OnlandVisualTrashAssessment.Index;
using IndexViewData = Neptune.WebMvc.Views.OnlandVisualTrashAssessment.IndexViewData;
using OVTASection = Neptune.EFModels.Entities.OVTASection;

namespace Neptune.WebMvc.Controllers
{
    //[Area("Trash")]
    //[Route("[area]/[controller]/[action]", Name = "[area]_[controller]_[action]")]
    public class OnlandVisualTrashAssessmentController : NeptuneBaseController<OnlandVisualTrashAssessmentController>
    {
        private readonly AzureBlobStorageService _azureBlobStorageService;
        public OnlandVisualTrashAssessmentController(NeptuneDbContext dbContext, ILogger<OnlandVisualTrashAssessmentController> logger, IOptions<WebConfiguration> webConfiguration, LinkGenerator linkGenerator, AzureBlobStorageService azureBlobStorageService) : base(dbContext, logger, linkGenerator, webConfiguration)
        {
            _azureBlobStorageService = azureBlobStorageService;
        }

        [HttpGet("{onlandVisualTrashAssessmentPrimaryKey}")]
        [NeptuneViewFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("onlandVisualTrashAssessmentPrimaryKey")]
        public ViewResult Detail([FromRoute] OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey)
        {
            var onlandVisualTrashAssessment = OnlandVisualTrashAssessments.GetByID(_dbContext, onlandVisualTrashAssessmentPrimaryKey);
            Check.Assert(onlandVisualTrashAssessment.OnlandVisualTrashAssessmentStatus == OnlandVisualTrashAssessmentStatus.Complete, "No details are available for this assessment because it has not been completed.");

            var onlandVisualTrashAssessmentObservations = OnlandVisualTrashAssessmentObservations.ListByOnlandVisualTrashAssessmentID(_dbContext, onlandVisualTrashAssessment.OnlandVisualTrashAssessmentID);
            var observationsLayerGeoJson = onlandVisualTrashAssessmentObservations.MakeObservationsLayerGeoJson();
            var assessmentAreaLayerGeoJson = onlandVisualTrashAssessment.GetAssessmentAreaLayerGeoJson(false);

            var transectLineLayerGeoJson = onlandVisualTrashAssessment.GetTransectLineLayerGeoJson();
            var layerGeoJsons = MapInitJsonHelpers.GetJurisdictionMapLayers(_dbContext).ToList();
            var boundingBoxDto = new BoundingBoxDto(onlandVisualTrashAssessment.GetOnlandVisualTrashAssessmentGeometry());
            var ovtaSummaryMapInitJson = new OVTASummaryMapInitJson("summaryMap", observationsLayerGeoJson, assessmentAreaLayerGeoJson, transectLineLayerGeoJson, layerGeoJsons, boundingBoxDto);
            var returnToEditUrl =
                SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(_linkGenerator, x =>
                    x.EditStatusToAllowEdit(onlandVisualTrashAssessment));
            var userHasPermissionToReturnToEdit = new OnlandVisualTrashAssessmentEditStatusFeature().HasPermission(CurrentPerson, onlandVisualTrashAssessment).HasPermission;
            var trashAssessmentSummaryMapViewData = new TrashAssessmentSummaryMapViewData(ovtaSummaryMapInitJson, onlandVisualTrashAssessmentObservations, _webConfiguration.MapServiceUrl);
            var viewData = new DetailViewData(HttpContext, _linkGenerator, CurrentPerson, _webConfiguration, onlandVisualTrashAssessment, trashAssessmentSummaryMapViewData, returnToEditUrl, userHasPermissionToReturnToEdit, new OnlandVisualTrashAssessmentAreaViewFeature()
                .HasPermission(CurrentPerson, onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea)
                .HasPermission);
            return RazorView<Detail, DetailViewData>(viewData);
        }

        [HttpGet]
        [NeptuneViewFeature]
        public ViewResult Index()
        {
            var viewData = new IndexViewData(HttpContext, _linkGenerator, CurrentPerson, _webConfiguration,
                NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.OVTAIndex), SitkaRoute<OnlandVisualTrashAssessmentExportController>.BuildUrlFromExpression(_linkGenerator, x => x.ExportAssessmentGeospatialData()));
            return RazorView<Index, IndexViewData>(viewData);
        }

        [HttpGet]
        [NeptuneViewFeature]
        public GridJsonNetJObjectResult<OnlandVisualTrashAssessment> OVTAGridJsonData()
        {
            var stormwaterJurisdictionIDsPersonCanView = StormwaterJurisdictionPeople.ListViewableStormwaterJurisdictionIDsByPersonForBMPs(_dbContext, CurrentPerson).ToList();

            if (!stormwaterJurisdictionIDsPersonCanView.Any())
            {
                throw new SitkaRecordNotAuthorizedException(
                    "You are not assigned to any Jurisdictions. Please log out and log in as a different user or request additional permissions");
            }

            var gridSpec = new OnlandVisualTrashAssessmentIndexGridSpec(_linkGenerator, CurrentPerson, true);
            var onlandVisualTrashAssessments = OnlandVisualTrashAssessments.ListByStormwaterJurisdictionIDList(_dbContext, stormwaterJurisdictionIDsPersonCanView);
            return new GridJsonNetJObjectResult<OnlandVisualTrashAssessment>(onlandVisualTrashAssessments, gridSpec);
        }

        [HttpGet]
        [NeptuneViewFeature]
        public GridJsonNetJObjectResult<OnlandVisualTrashAssessmentArea> OnlandVisualTrashAssessmentAreaGridData()
        {
            var stormwaterJurisdictionIDsPersonCanView = StormwaterJurisdictionPeople.ListViewableStormwaterJurisdictionIDsByPersonForBMPs(_dbContext, CurrentPerson).ToList();

            if (!stormwaterJurisdictionIDsPersonCanView.Any())
            {
                throw new SitkaRecordNotAuthorizedException(
                    "You are not assigned to any Jurisdictions. Please log out and log in as a different user or request additional permissions");
            }
            var onlandVisualTrashAssessmentAreas = OnlandVisualTrashAssessmentAreas.ListByStormwaterJurisdictionIDList(_dbContext, stormwaterJurisdictionIDsPersonCanView);
            var ovtaAssessmentsLookup = _dbContext.OnlandVisualTrashAssessments.AsNoTracking().Where(x => stormwaterJurisdictionIDsPersonCanView.Contains(x.StormwaterJurisdictionID) && x.OnlandVisualTrashAssessmentAreaID.HasValue).ToLookup(x => x.OnlandVisualTrashAssessmentAreaID.Value);
            var gridSpec = new OnlandVisualTrashAssessmentAreaIndexGridSpec(_linkGenerator, CurrentPerson, ovtaAssessmentsLookup);
            return new GridJsonNetJObjectResult<OnlandVisualTrashAssessmentArea>(onlandVisualTrashAssessmentAreas, gridSpec);
        }

        [HttpGet("{onlandVisualTrashAssessmentAreaPrimaryKey}")]
        [OnlandVisualTrashAssessmentAreaViewFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("onlandVisualTrashAssessmentAreaPrimaryKey")]
        public GridJsonNetJObjectResult<OnlandVisualTrashAssessment> OVTAGridJsonDataForAreaDetails([FromRoute] OnlandVisualTrashAssessmentAreaPrimaryKey onlandVisualTrashAssessmentAreaPrimaryKey)
        {
            var onlandVisualTrashAssessments = GetOVTAsAndGridSpec(out var gridSpec, CurrentPerson, onlandVisualTrashAssessmentAreaPrimaryKey.EntityObject);
            var gridJsonNetJObjectResult =
                new GridJsonNetJObjectResult<OnlandVisualTrashAssessment>(onlandVisualTrashAssessments, gridSpec);
            return gridJsonNetJObjectResult;
        }

        private List<OnlandVisualTrashAssessment> GetOVTAsAndGridSpec(out OnlandVisualTrashAssessmentIndexGridSpec gridSpec, Person currentPerson, OnlandVisualTrashAssessmentArea onlandVisualTrashAssessmentArea)
        {
            gridSpec = new OnlandVisualTrashAssessmentIndexGridSpec(_linkGenerator, currentPerson, false);
            return OnlandVisualTrashAssessments.ListByOnlandVisualTrashAssessmentAreaID(_dbContext, onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaID);
        }

        [HttpGet("{onlandVisualTrashAssessmentPrimaryKey}")]
        [NeptuneViewFeature]
        public ViewResult Instructions([FromRoute]
            OnlandVisualTrashAssessmentPrimaryKey? onlandVisualTrashAssessmentPrimaryKey)
        {
            var onlandVisualTrashAssessment = onlandVisualTrashAssessmentPrimaryKey is { PrimaryKeyValue: > 0 }
                ? OnlandVisualTrashAssessments.GetByID(_dbContext, onlandVisualTrashAssessmentPrimaryKey)
                : null;
            var viewModel = new InstructionsViewModel();
            return ViewInstructions(viewModel, onlandVisualTrashAssessment);
        }

        private ViewResult ViewInstructions(InstructionsViewModel viewModel, OnlandVisualTrashAssessment? ovta)
        {
            var neptunePage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.OVTAInstructions);
            var viewData = new InstructionsViewData(HttpContext, _linkGenerator, CurrentPerson, _webConfiguration, neptunePage, ovta);
            return RazorView<Instructions, InstructionsViewData, InstructionsViewModel>(viewData, viewModel);
        }

        [HttpGet("{onlandVisualTrashAssessmentPrimaryKey}")]
        [NeptuneViewFeature]
        public ViewResult InitiateOVTA([FromRoute] OnlandVisualTrashAssessmentPrimaryKey? onlandVisualTrashAssessmentPrimaryKey)
        {
            var onlandVisualTrashAssessment = onlandVisualTrashAssessmentPrimaryKey is { PrimaryKeyValue: > 0 }
                ? OnlandVisualTrashAssessments.GetByID(_dbContext, onlandVisualTrashAssessmentPrimaryKey)
                : null;
            var stormwaterJurisdictionsPersonCanEdit = StormwaterJurisdictions.ListViewableByPersonForBMPs(_dbContext, CurrentPerson);
            var viewModel = new InitiateOVTAViewModel(onlandVisualTrashAssessment, stormwaterJurisdictionsPersonCanEdit);

            return ViewInitiateOVTA(onlandVisualTrashAssessment, viewModel);
        }

        [HttpPost("{onlandVisualTrashAssessmentPrimaryKey}")]
        [NeptuneViewFeature]
        public async Task<ActionResult> InitiateOVTA([FromRoute] OnlandVisualTrashAssessmentPrimaryKey? onlandVisualTrashAssessmentPrimaryKey, InitiateOVTAViewModel viewModel)
        {
            var onlandVisualTrashAssessment = onlandVisualTrashAssessmentPrimaryKey is { PrimaryKeyValue: > 0 }
                ? OnlandVisualTrashAssessments.GetByIDWithChangeTracking(_dbContext, onlandVisualTrashAssessmentPrimaryKey)
                : null;
            if (!ModelState.IsValid)
            {
                return ViewInitiateOVTA(onlandVisualTrashAssessment, viewModel);
            }

            if (onlandVisualTrashAssessment == null)
            {
                onlandVisualTrashAssessment = new OnlandVisualTrashAssessment
                {
                    CreatedByPersonID = CurrentPerson.PersonID, CreatedDate = DateTime.UtcNow,
                    StormwaterJurisdictionID = viewModel.StormwaterJurisdiction.StormwaterJurisdictionID,
                    OnlandVisualTrashAssessmentStatusID =
                        (int)OnlandVisualTrashAssessmentStatusEnum.InProgress,
                    IsProgressAssessment = false, IsTransectBackingAssessment = false
                };
                _dbContext.OnlandVisualTrashAssessments.Add(onlandVisualTrashAssessment);
            }


            viewModel.UpdateModel(_dbContext, onlandVisualTrashAssessment);
            await _dbContext.SaveChangesAsync();

            return RedirectToAppropriateStep(viewModel, OVTASection.InitiateOVTA, onlandVisualTrashAssessment);
        }

        private ViewResult ViewInitiateOVTA(OnlandVisualTrashAssessment? onlandVisualTrashAssessment,
            InitiateOVTAViewModel viewModel)
        {
            var stormwaterJurisdictionsPersonCanEdit = StormwaterJurisdictions.ListViewableByPersonForBMPs(_dbContext, CurrentPerson);

            // do not offer a drop-down menu if the user can only edit one jurisdiction
            var defaultJurisdiction = stormwaterJurisdictionsPersonCanEdit.Count == 1
                ? stormwaterJurisdictionsPersonCanEdit.Single()
                : null;

            var onlandVisualTrashAssessmentAreas = OnlandVisualTrashAssessmentAreas.ListByStormwaterJurisdictionIDList(_dbContext, stormwaterJurisdictionsPersonCanEdit.Select(x => x.StormwaterJurisdictionID));

            var layerGeoJsons = MapInitJsonHelpers.GetJurisdictionMapLayers(_dbContext).ToList();
            var boundingBoxDto = new BoundingBoxDto(onlandVisualTrashAssessment?.GetOnlandVisualTrashAssessmentGeometry());
            var mapInitJson = new SelectOVTAAreaMapInitJson("selectOVTAAreaMap",
                onlandVisualTrashAssessmentAreas.MakeAssessmentAreasLayerGeoJson(), layerGeoJsons, boundingBoxDto);

            var viewData = new InitiateOVTAViewData(HttpContext, _linkGenerator, CurrentPerson, _webConfiguration,
                onlandVisualTrashAssessment, stormwaterJurisdictionsPersonCanEdit, mapInitJson, onlandVisualTrashAssessmentAreas,
                defaultJurisdiction, _webConfiguration.MapServiceUrl);
            return RazorView<InitiateOVTA, InitiateOVTAViewData, InitiateOVTAViewModel>(viewData, viewModel);
        }

        [HttpGet("{onlandVisualTrashAssessmentPrimaryKey}")]
        [OnlandVisualTrashAssessmentEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("onlandVisualTrashAssessmentPrimaryKey")]
        public ViewResult RecordObservations([FromRoute]
            OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey)
        {
            var onlandVisualTrashAssessment = OnlandVisualTrashAssessments.GetByID(_dbContext, onlandVisualTrashAssessmentPrimaryKey);
            var onlandVisualTrashAssessmentObservations = OnlandVisualTrashAssessmentObservations.ListByOnlandVisualTrashAssessmentID(_dbContext, onlandVisualTrashAssessment.OnlandVisualTrashAssessmentID);
            var viewModel = new RecordObservationsViewModel(onlandVisualTrashAssessment, onlandVisualTrashAssessmentObservations);
            return ViewRecordObservations(onlandVisualTrashAssessment, viewModel);
        }

        [HttpPost("{onlandVisualTrashAssessmentPrimaryKey}")]
        [OnlandVisualTrashAssessmentEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("onlandVisualTrashAssessmentPrimaryKey")]
        public async Task<ActionResult> RecordObservations([FromRoute]
            OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey,
            RecordObservationsViewModel viewModel)
        {
            var onlandVisualTrashAssessment = OnlandVisualTrashAssessments.GetByIDWithChangeTracking(_dbContext, onlandVisualTrashAssessmentPrimaryKey);
            if (!ModelState.IsValid)
            {
                return ViewRecordObservations(onlandVisualTrashAssessment, viewModel);
            }

            var allOnlandVisualTrashAssessmentObservations =
                _dbContext.OnlandVisualTrashAssessmentObservations;
            await viewModel.UpdateModel(_dbContext, onlandVisualTrashAssessment, allOnlandVisualTrashAssessmentObservations);
            return RedirectToAppropriateStep(viewModel, OVTASection.RecordObservations, onlandVisualTrashAssessment);
        }

        private ViewResult ViewRecordObservations(OnlandVisualTrashAssessment onlandVisualTrashAssessment,
            RecordObservationsViewModel viewModel)
        {
            var observationsLayerGeoJson = onlandVisualTrashAssessment.OnlandVisualTrashAssessmentObservations.MakeObservationsLayerGeoJson();
            var assessmentAreaLayerGeoJson = onlandVisualTrashAssessment.GetAssessmentAreaLayerGeoJson(false);
            var transectLineLayerGeoJson = onlandVisualTrashAssessment.GetTransectLineLayerGeoJson();
            var layerGeoJsons = MapInitJsonHelpers.GetJurisdictionMapLayers(_dbContext).ToList();
            var boundingBoxGeometries = onlandVisualTrashAssessment.OnlandVisualTrashAssessmentObservations.Select(x => x.LocationPoint4326).ToList();
            var ovtaGeometry = onlandVisualTrashAssessment.GetOnlandVisualTrashAssessmentGeometry();
            if (ovtaGeometry != null)
            {
                boundingBoxGeometries.Add(ovtaGeometry);
            }
            var boundingBoxDto = new BoundingBoxDto(boundingBoxGeometries);
            var ovtaObservationsMapInitJson = new OVTAObservationsMapInitJson("observationsMap",
                observationsLayerGeoJson, assessmentAreaLayerGeoJson, transectLineLayerGeoJson, layerGeoJsons, boundingBoxDto);

            var viewData = new RecordObservationsViewData(HttpContext, _linkGenerator, CurrentPerson, _webConfiguration,
                onlandVisualTrashAssessment, ovtaObservationsMapInitJson, _webConfiguration.MapServiceUrl);
            return RazorView<RecordObservations, RecordObservationsViewData, RecordObservationsViewModel>(viewData,
                viewModel);
        }

        [HttpGet("{onlandVisualTrashAssessmentPrimaryKey}")]
        [OnlandVisualTrashAssessmentEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("onlandVisualTrashAssessmentPrimaryKey")]
        public ViewResult AddOrRemoveParcels([FromRoute] OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey)
        {
            var onlandVisualTrashAssessment = OnlandVisualTrashAssessments.GetByID(_dbContext, onlandVisualTrashAssessmentPrimaryKey);
            var parcelIDs = onlandVisualTrashAssessment.GetParcelIDsForAddOrRemoveParcels(_dbContext);
            var viewModel = new AddOrRemoveParcelsViewModel(parcelIDs);
            return ViewAddOrRemoveParcels(onlandVisualTrashAssessment, viewModel);
        }

        [HttpPost("{onlandVisualTrashAssessmentPrimaryKey}")]
        [OnlandVisualTrashAssessmentEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("onlandVisualTrashAssessmentPrimaryKey")]
        public async Task<ActionResult> AddOrRemoveParcels([FromRoute]
            OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey,
            AddOrRemoveParcelsViewModel viewModel)
        {
            var onlandVisualTrashAssessment = OnlandVisualTrashAssessments.GetByIDWithChangeTracking(_dbContext, onlandVisualTrashAssessmentPrimaryKey);

            if (!ModelState.IsValid)
            {
                return ViewAddOrRemoveParcels(onlandVisualTrashAssessment, viewModel);
            }

            var unionOfSelectedParcelGeometries = ParcelGeometries.UnionAggregateByParcelIDs(_dbContext, viewModel.ParcelIDs);
            
            onlandVisualTrashAssessment.DraftGeometry = unionOfSelectedParcelGeometries;
            await _dbContext.SaveChangesAsync();

            return RedirectToAppropriateStep(viewModel, OVTASection.AddOrRemoveParcels,
                onlandVisualTrashAssessment);
        }

        private ViewResult ViewAddOrRemoveParcels(OnlandVisualTrashAssessment onlandVisualTrashAssessment,
            AddOrRemoveParcelsViewModel viewModel)
        {
            var layerGeoJsons = MapInitJsonHelpers.GetJurisdictionMapLayers(_dbContext).ToList();
            var onlandVisualTrashAssessmentObservations = onlandVisualTrashAssessment.OnlandVisualTrashAssessmentObservations;
            var makeObservationsLayerGeoJson = onlandVisualTrashAssessmentObservations.MakeObservationsLayerGeoJson();
            var transectLineLayerGeoJson = onlandVisualTrashAssessment.GetTransectLineLayerGeoJson();
            var boundingBoxDto = new BoundingBoxDto(onlandVisualTrashAssessmentObservations.Select(x => x.LocationPoint4326));
            var addOrRemoveParcelsMapIntJson = new AddOrRemoveParcelsMapIntJson("addOrRemoveParcelsMap",
                makeObservationsLayerGeoJson, transectLineLayerGeoJson, layerGeoJsons, boundingBoxDto);

            var viewData = new AddOrRemoveParcelsViewData(HttpContext, _linkGenerator, CurrentPerson, _webConfiguration, OVTASection.AddOrRemoveParcels, onlandVisualTrashAssessment, addOrRemoveParcelsMapIntJson, _webConfiguration.MapServiceUrl);
            return RazorView<AddOrRemoveParcels, AddOrRemoveParcelsViewData, AddOrRemoveParcelsViewModel>(viewData,
                viewModel);
        }

        [HttpGet("{onlandVisualTrashAssessmentPrimaryKey}")]
        [OnlandVisualTrashAssessmentEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("onlandVisualTrashAssessmentPrimaryKey")]
        public ViewResult RefineAssessmentArea([FromRoute] OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey)
        {
            var onlandVisualTrashAssessment = OnlandVisualTrashAssessments.GetByID(_dbContext, onlandVisualTrashAssessmentPrimaryKey);
            var viewModel = new RefineAssessmentAreaViewModel();
            return ViewRefineAssessmentArea(onlandVisualTrashAssessment, viewModel);
        }

        [HttpPost("{onlandVisualTrashAssessmentPrimaryKey}")]
        [OnlandVisualTrashAssessmentEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("onlandVisualTrashAssessmentPrimaryKey")]
        public async Task<ActionResult> RefineAssessmentArea([FromRoute] OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey, RefineAssessmentAreaViewModel viewModel)
        {
            var onlandVisualTrashAssessment = OnlandVisualTrashAssessments.GetByIDWithChangeTracking(_dbContext, onlandVisualTrashAssessmentPrimaryKey);
            if (!ModelState.IsValid)
            {
                return ViewRefineAssessmentArea(onlandVisualTrashAssessment, viewModel);
            }

            // these come in in web mercator...
            var geometries = viewModel.WktAndAnnotations.Select(x => GeometryHelper.FromWKT(x.Wkt, Proj4NetHelper.WEB_MERCATOR)).Where(x => x != null).ToList();
            var unionListGeometries = geometries.UnionListGeometries();
            
            // ...and then get fixed here
            onlandVisualTrashAssessment.DraftGeometry = unionListGeometries.ProjectTo2771();
            onlandVisualTrashAssessment.IsDraftGeometryManuallyRefined = true;
            await _dbContext.SaveChangesAsync();

            return RedirectToAppropriateStep(viewModel, OVTASection.RefineAssessmentArea, onlandVisualTrashAssessment);
        }

        private ViewResult ViewRefineAssessmentArea(OnlandVisualTrashAssessment onlandVisualTrashAssessment, RefineAssessmentAreaViewModel viewModel)
        {
            var observationsLayerGeoJson = onlandVisualTrashAssessment.OnlandVisualTrashAssessmentObservations.MakeObservationsLayerGeoJson();
            var assessmentAreaLayerGeoJson = onlandVisualTrashAssessment.GetAssessmentAreaLayerGeoJson(true);
            var transectLineLayerGeoJson = onlandVisualTrashAssessment.GetTransectLineLayerGeoJson();
            var layerGeoJsons = MapInitJsonHelpers.GetJurisdictionMapLayers(_dbContext).ToList();
            var boundingBoxDto = new BoundingBoxDto(onlandVisualTrashAssessment.GetOnlandVisualTrashAssessmentGeometry());
            var refineAssessmentAreaMapInitJson = new RefineAssessmentAreaMapInitJson("refineAssessmentAreaMap", observationsLayerGeoJson, assessmentAreaLayerGeoJson, transectLineLayerGeoJson, layerGeoJsons, boundingBoxDto);

            var viewData = new RefineAssessmentAreaViewData(HttpContext, _linkGenerator, CurrentPerson, _webConfiguration, OVTASection.RefineAssessmentArea, onlandVisualTrashAssessment, refineAssessmentAreaMapInitJson, _webConfiguration.MapServiceUrl);
            return RazorView<RefineAssessmentArea, RefineAssessmentAreaViewData, RefineAssessmentAreaViewModel>(
                viewData, viewModel);
        }

        [HttpGet("{onlandVisualTrashAssessmentPrimaryKey}")]
        [OnlandVisualTrashAssessmentEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("onlandVisualTrashAssessmentPrimaryKey")]
        public ViewResult FinalizeOVTA([FromRoute] OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey)
        {
            var onlandVisualTrashAssessment = OnlandVisualTrashAssessments.GetByID(_dbContext, onlandVisualTrashAssessmentPrimaryKey);
            var viewModel = new FinalizeOVTAViewModel(onlandVisualTrashAssessment);
            return ViewFinalizeOVTA(onlandVisualTrashAssessment, viewModel);
        }

        private ViewResult ViewFinalizeOVTA(OnlandVisualTrashAssessment onlandVisualTrashAssessment,
            FinalizeOVTAViewModel viewModel)
        {
            var onlandVisualTrashAssessmentObservations = OnlandVisualTrashAssessmentObservations.ListByOnlandVisualTrashAssessmentID(_dbContext, onlandVisualTrashAssessment.OnlandVisualTrashAssessmentID);
            var observationsLayerGeoJson = onlandVisualTrashAssessmentObservations.MakeObservationsLayerGeoJson();
            var assessmentAreaLayerGeoJson = onlandVisualTrashAssessment.GetAssessmentAreaLayerGeoJson(false);
            var transectLineLayerGeoJson = onlandVisualTrashAssessment.GetTransectLineLayerGeoJson();
            var layerGeoJsons = MapInitJsonHelpers.GetJurisdictionMapLayers(_dbContext).ToList();
            var boundingBoxDto = new BoundingBoxDto(onlandVisualTrashAssessment.GetOnlandVisualTrashAssessmentGeometry());
            var ovtaSummaryMapInitJson = new OVTASummaryMapInitJson("summaryMap", observationsLayerGeoJson, assessmentAreaLayerGeoJson, transectLineLayerGeoJson, layerGeoJsons, boundingBoxDto);

            var scoresSelectList = OnlandVisualTrashAssessmentScore.All.ToSelectListWithDisabledEmptyFirstRow(x => x.OnlandVisualTrashAssessmentScoreID.ToString(CultureInfo.InvariantCulture), x => x.OnlandVisualTrashAssessmentScoreDisplayName.ToString(CultureInfo.InvariantCulture),
                "Choose a score");
            var viewData = new FinalizeOVTAViewData(HttpContext, _linkGenerator, CurrentPerson, _webConfiguration,
                onlandVisualTrashAssessment, ovtaSummaryMapInitJson, scoresSelectList, _webConfiguration.MapServiceUrl, onlandVisualTrashAssessmentObservations);
            return RazorView<FinalizeOVTA, FinalizeOVTAViewData, FinalizeOVTAViewModel>(viewData, viewModel);
        }

        [HttpPost("{onlandVisualTrashAssessmentPrimaryKey}")]
        [OnlandVisualTrashAssessmentEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("onlandVisualTrashAssessmentPrimaryKey")]
        public async Task<ActionResult> FinalizeOVTA([FromRoute] OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey,
            FinalizeOVTAViewModel viewModel)
        {
            var onlandVisualTrashAssessment = OnlandVisualTrashAssessments.GetByIDWithChangeTracking(_dbContext, onlandVisualTrashAssessmentPrimaryKey);
            if (!ModelState.IsValid)
            {
                return ViewFinalizeOVTA(onlandVisualTrashAssessment, viewModel);
            }

            await viewModel.UpdateModel(_dbContext, onlandVisualTrashAssessment);
            SetMessageForDisplay("The OVTA was successfully finalized");


            if (viewModel.Finalize.GetValueOrDefault())
            {
                return Redirect(
                    SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(_linkGenerator, x => x.Detail(onlandVisualTrashAssessment)));
            }
            else
            {
                return Redirect(
                    SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(_linkGenerator, x =>
                        x.FinalizeOVTA(onlandVisualTrashAssessment)));
            }
        }

        [HttpGet("{onlandVisualTrashAssessmentPrimaryKey}")]
        [OnlandVisualTrashAssessmentEditStatusFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("onlandVisualTrashAssessmentPrimaryKey")]
        public PartialViewResult EditStatusToAllowEdit([FromRoute] OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey)
        {
            var onlandVisualTrashAssessment = OnlandVisualTrashAssessments.GetByID(_dbContext, onlandVisualTrashAssessmentPrimaryKey);
            var viewModel = new ConfirmDialogFormViewModel(onlandVisualTrashAssessmentPrimaryKey.PrimaryKeyValue);
            return ViewEditStatusToAllowEdit(onlandVisualTrashAssessment, viewModel);
        }

        private PartialViewResult ViewEditStatusToAllowEdit(OnlandVisualTrashAssessment ovta, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage =
                $"This OVTA was finalized on {ovta.CompletedDate}. Are you sure you want to revert this OVTA to the \"In Progress\" status?";

            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData,
                viewModel);
        }

        [HttpPost("{onlandVisualTrashAssessmentPrimaryKey}")]
        [OnlandVisualTrashAssessmentEditStatusFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("onlandVisualTrashAssessmentPrimaryKey")]
        public async Task<ActionResult> EditStatusToAllowEdit([FromRoute] OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey,
            ConfirmDialogFormViewModel viewModel)
        {
            var onlandVisualTrashAssessment = OnlandVisualTrashAssessments.GetByIDWithChangeTracking(_dbContext, onlandVisualTrashAssessmentPrimaryKey);

            if (!ModelState.IsValid)
            {
                return ViewEditStatusToAllowEdit(onlandVisualTrashAssessment, viewModel);
            }
            onlandVisualTrashAssessment.OnlandVisualTrashAssessmentStatusID = OnlandVisualTrashAssessmentStatus.InProgress.OnlandVisualTrashAssessmentStatusID;
            onlandVisualTrashAssessment.AssessingNewArea = false;

            // update the assessment area scores now that this assessment no longer contributes
            var onlandVisualTrashAssessments =
                OnlandVisualTrashAssessments.ListByOnlandVisualTrashAssessmentAreaID(_dbContext,
                    onlandVisualTrashAssessment.OnlandVisualTrashAssessmentAreaID.Value);
            var onlandVisualTrashAssessmentArea = onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea;
            onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentBaselineScoreID =
                OnlandVisualTrashAssessmentAreaModelExtensions.CalculateScoreFromBackingData(onlandVisualTrashAssessments, false)?
                    .OnlandVisualTrashAssessmentScoreID;

            onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentProgressScoreID =
                OnlandVisualTrashAssessmentAreaModelExtensions.CalculateProgressScore(onlandVisualTrashAssessments)?
                    .OnlandVisualTrashAssessmentScoreID;

            if (onlandVisualTrashAssessment.IsTransectBackingAssessment)
            {
                onlandVisualTrashAssessment.IsTransectBackingAssessment = false;
                onlandVisualTrashAssessmentArea.TransectLine = null;
                onlandVisualTrashAssessmentArea.TransectLine4326 = null;

                await _dbContext.SaveChangesAsync();

                var transectLine = OnlandVisualTrashAssessmentAreaModelExtensions.RecomputeTransectLine(onlandVisualTrashAssessments, out var transectBackingAssessment);
                onlandVisualTrashAssessmentArea.TransectLine = transectLine;
                onlandVisualTrashAssessmentArea.TransectLine4326 = transectLine.ProjectTo4326();

                if (transectBackingAssessment != null)
                {
                    transectBackingAssessment.IsTransectBackingAssessment = true;
                }
            }

            await _dbContext.SaveChangesAsync();
            SetMessageForDisplay("The OVTA was successfully returned to the \"In Progress\" status");


            return new ModalDialogFormJsonResult(SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(_linkGenerator, x =>
                x.RecordObservations(onlandVisualTrashAssessment.OnlandVisualTrashAssessmentID)));

        }

        [HttpGet("{onlandVisualTrashAssessmentPrimaryKey}")]
        [OnlandVisualTrashAssessmentEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("onlandVisualTrashAssessmentPrimaryKey")]
        public PartialViewResult RefreshParcels([FromRoute] OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey)
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

        [HttpPost("{onlandVisualTrashAssessmentPrimaryKey}")]
        [OnlandVisualTrashAssessmentEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("onlandVisualTrashAssessmentPrimaryKey")]
        public async Task<ActionResult> RefreshParcels([FromRoute] OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey,
            ConfirmDialogFormViewModel viewModel)
        {
            var onlandVisualTrashAssessment = OnlandVisualTrashAssessments.GetByIDWithChangeTracking(_dbContext, onlandVisualTrashAssessmentPrimaryKey);

            if (!ModelState.IsValid)
            {
                return ViewRefreshParcels(viewModel);
            }

            onlandVisualTrashAssessment.IsDraftGeometryManuallyRefined = false;
            onlandVisualTrashAssessment.DraftGeometry = null;

            await _dbContext.SaveChangesAsync();

            return new ModalDialogFormJsonResult(
                SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(_linkGenerator, x =>
                    x.AddOrRemoveParcels(onlandVisualTrashAssessment)));
        }

        [HttpGet("{onlandVisualTrashAssessmentPrimaryKey}")]
        [OnlandVisualTrashAssessmentDeleteFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("onlandVisualTrashAssessmentPrimaryKey")]
        public PartialViewResult Delete([FromRoute] OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey)
        {
            var onlandVisualTrashAssessment = OnlandVisualTrashAssessments.GetByID(_dbContext, onlandVisualTrashAssessmentPrimaryKey);
            var viewModel = new ConfirmDialogFormViewModel(onlandVisualTrashAssessment.OnlandVisualTrashAssessmentID);
            return ViewDeleteOnlandVisualTrashAssessment(onlandVisualTrashAssessment, viewModel);
        }

        [HttpPost("{onlandVisualTrashAssessmentPrimaryKey}")]
        [OnlandVisualTrashAssessmentDeleteFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("onlandVisualTrashAssessmentPrimaryKey")]
        public async Task<ActionResult> Delete([FromRoute] OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var onlandVisualTrashAssessment = OnlandVisualTrashAssessments.GetByIDWithChangeTracking(_dbContext, onlandVisualTrashAssessmentPrimaryKey);
            if (!ModelState.IsValid)
            {
                return ViewDeleteOnlandVisualTrashAssessment(onlandVisualTrashAssessment, viewModel);
            }

            var onlandVisualTrashAssessmentArea = onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea;

            var isProgressAssessment = onlandVisualTrashAssessment.IsProgressAssessment;
            await onlandVisualTrashAssessment.DeleteFull(_dbContext);

            if (onlandVisualTrashAssessmentArea != null)
            {
                var onlandVisualTrashAssessments = OnlandVisualTrashAssessments.ListByOnlandVisualTrashAssessmentAreaID(_dbContext, onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaID);
                onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentBaselineScoreID = OnlandVisualTrashAssessmentAreaModelExtensions.CalculateScoreFromBackingData(onlandVisualTrashAssessments, false)?.OnlandVisualTrashAssessmentScoreID;

                if (isProgressAssessment)
                {
                    onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentProgressScoreID =
                        OnlandVisualTrashAssessmentAreaModelExtensions.CalculateProgressScore(onlandVisualTrashAssessments)
                            ?.OnlandVisualTrashAssessmentScoreID;
                }

            }

            SetMessageForDisplay("Successfully deleted the assessment.");
            return new ModalDialogFormJsonResult(SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(_linkGenerator, x => x.Index()));
        }

        private PartialViewResult ViewDeleteOnlandVisualTrashAssessment(OnlandVisualTrashAssessment onlandVisualTrashAssessment, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = $"Are you sure you want to delete the assessment from {onlandVisualTrashAssessment.CreatedDate.ToShortDateString()}?";

            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [NeptuneViewFeature]
        public PartialViewResult ScoreDescriptions()
        {
            var viewData = new TrashModuleViewData(HttpContext, _linkGenerator, CurrentPerson, _webConfiguration);
            return RazorPartialView<ScoreDescriptions, TrashModuleViewData>(viewData);
        }

        // helpers

        // assumes that we are not looking for the parcels-via-transect area

        [HttpGet]
        [JurisdictionManageFeature]
        public ViewResult BulkUploadOTVAs()
        {
            var bulkUploadTrashScreenVisitViewModel = new BulkUploadOVTAsViewModel();

            return ViewBulkUploadOTVAs(bulkUploadTrashScreenVisitViewModel);
        }

        private ViewResult ViewBulkUploadOTVAs(
            BulkUploadOVTAsViewModel bulkUploadTrashScreenVisitViewModel)
        {
            var neptunePage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.UploadOVTAs);
            var bulkUploadTrashScreenVisitViewData = new BulkUploadOVTAsViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, neptunePage, StormwaterJurisdictions.ListViewableByPersonForBMPs(_dbContext, CurrentPerson));

            return RazorView<BulkUploadOVTAs, BulkUploadOVTAsViewData,
                BulkUploadOVTAsViewModel>(bulkUploadTrashScreenVisitViewData,
                bulkUploadTrashScreenVisitViewModel);
        }

        [HttpPost]
        [JurisdictionManageFeature]
        [RequestSizeLimit(100_000_000_000)]
        [RequestFormLimits(MultipartBodyLengthLimit = 100_000_000_000)]
        public async Task<IActionResult> BulkUploadOTVAs(BulkUploadOVTAsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewBulkUploadOTVAs(viewModel);
            }

            var uploadXlsxInputStream = viewModel.UploadXLSX.OpenReadStream();

            DataTable dataTableFromExcel;

            try
            {
                dataTableFromExcel = GetDataTableFromExcel(uploadXlsxInputStream, "OVTA Assessments");
            }
            catch (Exception)
            {
                SetErrorForDisplay("Unexpected error parsing Excel Spreadsheet upload. Make sure the file matches the provided template and try again.");
                return ViewBulkUploadOTVAs(viewModel);
            }

            var numRows = dataTableFromExcel.Rows.Count;

            var stormwaterJurisdictionsPersonCanView = StormwaterJurisdictions.ListViewableByPersonForBMPs(_dbContext, CurrentPerson);
            var ovtaAreas = _dbContext.OnlandVisualTrashAssessmentAreas.ToList();
            var users = _dbContext.People.ToList();
            if (!CurrentPerson.IsAdministrator())
            {
                foreach (DataRow row in dataTableFromExcel.Rows)
                {
                    var rowJurisdiction = row["Jurisdiction Name"].ToString();
                    if (!stormwaterJurisdictionsPersonCanView.Select(x => x.Organization.OrganizationName)
                            .Contains(rowJurisdiction))
                    {
                        SetErrorForDisplay(
                            $"You attempted to upload a spreadsheet containing BMPs in Jurisdiction {rowJurisdiction}, which you do not have permission to manage.");
                        return ViewBulkUploadOTVAs(viewModel);
                    }
                }
            }

            var numColumns = dataTableFromExcel.Columns.Count;

            var errors = new List<string>();

            try
            {
                for (var i = 0; i < numRows; i++)
                {
                    try
                    {
                        var row = dataTableFromExcel.Rows[i];
                        var rowEmpty = true;
                        for (var j = 0; j < numColumns; j++)
                        {
                            rowEmpty = string.IsNullOrWhiteSpace(row[j].ToString());
                            if (!rowEmpty)
                            {
                                break;
                            }
                        }

                        if (rowEmpty)
                        {
                            continue;
                        }

                        var areaName = ovtaAreas.SingleOrDefault(x => x.OnlandVisualTrashAssessmentAreaName == row["Area Name"].ToString())?.OnlandVisualTrashAssessmentAreaID;
                        var createdByPersonID = users.SingleOrDefault(x => x.Email == row["Created By Person"].ToString().Trim())?.PersonID;

                        var newErrors = CheckDataFromRow(areaName, i, createdByPersonID, row);

                        if (newErrors.Count > 0)
                        {
                            errors.AddRange(newErrors);
                            continue;
                        }

                        var categories = PreliminarySourceIdentificationCategory.All.Select(x =>
                            x.PreliminarySourceIdentificationCategoryDisplayName).ToList();
                        var assessmentPreliminarySourceIdentificationTypes = new List<OnlandVisualTrashAssessmentPreliminarySourceIdentificationType>();
                        foreach (var category in categories)
                        {
                            if (!row[category].ToString().IsNullOrEmpty())
                            {
                                var identificationTypes = row[category].ToString().Trim().Split(',');
                                foreach (var identificationType in identificationTypes)
                                {
                                    if (identificationType.ToLower().Contains("other"))
                                    {
                                        errors.Add($"Cannot use {identificationType.Trim()} in row {i+1} as bulk uploader does not allow for Other as a preliminary type.");
                                        continue;
                                    }
                                    var id = PreliminarySourceIdentificationType.All.SingleOrDefault(x =>
                                        x.PreliminarySourceIdentificationTypeDisplayName == identificationType.Trim() 
                                        && x.PreliminarySourceIdentificationCategory.PreliminarySourceIdentificationCategoryDisplayName == category);
                                    if (id == null)
                                    {
                                        errors.Add($"{identificationType.Trim()} is not a valid Preliminary Source Identification Type for {category} in row {i + 1}");
                                        continue;
                                    }
                                    assessmentPreliminarySourceIdentificationTypes.Add(new OnlandVisualTrashAssessmentPreliminarySourceIdentificationType()
                                    {
                                        PreliminarySourceIdentificationTypeID = id.PreliminarySourceIdentificationTypeID
                                    });
                                }
                            }

                        }


                        // check to make sure values are valid
                        var onlandTrashVisualAssessment = new OnlandVisualTrashAssessment()
                        {
                            OnlandVisualTrashAssessmentAreaID = areaName,
                            CreatedByPersonID = (int)createdByPersonID,
                            StormwaterJurisdictionID = stormwaterJurisdictionsPersonCanView.Single(x => x.Organization.OrganizationName == row["Jurisdiction Name"].ToString() || x.Organization.OrganizationShortName == row["Jurisdiction Name"].ToString()).StormwaterJurisdictionID,
                            OnlandVisualTrashAssessmentStatusID = row["Status"].ToString().Trim() == "Finalized" ? (int)OnlandVisualTrashAssessmentStatusEnum.Complete : (int)OnlandVisualTrashAssessmentStatusEnum.InProgress,
                            CreatedDate = DateTime.Parse(row["Created Date"].ToString().Trim()),
                            CompletedDate = DateTime.Parse(row["Completed Date"].ToString().Trim()),
                            OnlandVisualTrashAssessmentScoreID = OnlandVisualTrashAssessmentScore.All.Single(x => x.OnlandVisualTrashAssessmentScoreDisplayName == row["Score"].ToString().Trim()).OnlandVisualTrashAssessmentScoreID,
                            IsProgressAssessment = row["Is Progress Assessment"].ToString().Trim() == "Yes",
                            OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypes = assessmentPreliminarySourceIdentificationTypes,
                            IsTransectBackingAssessment = false
                        };

                        _dbContext.Add(onlandTrashVisualAssessment);

                    }
                    catch (InvalidOperationException ioe)
                    {
                        errors.Add(ioe.Message);
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorForDisplay("Unexpected error parsing Excel Spreadsheet upload. Make sure the file matches the provided template and try again.");
                return ViewBulkUploadOTVAs(viewModel);
            }

            if (errors.Count > 0)
            {
                SetErrorForDisplay(string.Join("<br/>", errors));
                return ViewBulkUploadOTVAs(viewModel);
            }

            await _dbContext.SaveChangesAsync();

            SetMessageForDisplay("Successfully bulk uploaded OVTAs");

            return RedirectToAction(new SitkaRoute<OnlandVisualTrashAssessmentController>(_linkGenerator, x => x.Index()));
        }

        private static List<string> CheckDataFromRow(int? areaName, int i, int? createdByPersonID, DataRow row)
        {
            var errors = new List<string>();
            if (areaName == null)
            {
                errors.Add($"Cannot find OVTA area name in row {i + 1}");
            }

            if (createdByPersonID == null)
            {
                errors.Add($"Cannot find Person in row {i + 1}");
            }

            if (row["Is Progress Assessment"].ToString().IsNullOrEmpty() ||
                (row["Is Progress Assessment"].ToString().Trim() != "Yes" && row["Is Progress Assessment"].ToString().Trim() != "No"))
            {
                errors.Add($"Is Progress Assessment is not a valid value in row {i + 1}. It must be either Yes or No.");
            }

            if (row["Status"].ToString().IsNullOrEmpty() || 
                (row["Status"].ToString().Trim() != "Finalized" && row["Status"].ToString().Trim() != "Draft"))
            {
                errors.Add($"Status is not a valid value in row {i + 1}. It must be either Finalized or Draft.");
            }

            if (row["Score"].ToString().IsNullOrEmpty() ||
                !(OnlandVisualTrashAssessmentScore.All
                    .Select(x => x.OnlandVisualTrashAssessmentScoreDisplayName).ToList()
                    .Contains(row["Score"].ToString().Trim())))
            {
                errors.Add($"Score is not a valid value in row {i + 1}. It must be one of the following A, B, C or D.");
            }

            return errors;
        }

        [HttpGet]
        [JurisdictionManageFeature]
        public async Task<FileResult> OVTABulkUploadTemplate()
        {
            using var disposableTempFile = DisposableTempFile.MakeDisposableTempFileEndingIn(".xlsx");
            await _azureBlobStorageService.DownloadBlobToFileAsync(_webConfiguration.PathToOVTAUploadTemplate, disposableTempFile.FileInfo.FullName);
            using var workbook = new XLWorkbook(disposableTempFile.FileInfo.FullName);

            var row = 2;
            var worksheet = workbook.Worksheet("OVTA Assessments");
            var stormwaterJurisdictionsPersonCanEdit =StormwaterJurisdictions.ListViewableByPersonForBMPs(_dbContext, CurrentPerson).Select(x => x.StormwaterJurisdictionID);
            var ovtaAreas = _dbContext.OnlandVisualTrashAssessmentAreas
                .Include(x => x.StormwaterJurisdiction)
                .ThenInclude(x => x.Organization)
                .Where(x => stormwaterJurisdictionsPersonCanEdit.Contains(x.StormwaterJurisdictionID)).ToList();
            

            foreach (var ovtaArea in ovtaAreas)
            {
                worksheet.Cells($"A{row}").Value = ovtaArea.OnlandVisualTrashAssessmentAreaName;
                worksheet.Cells($"B{row}").Value = ovtaArea.StormwaterJurisdiction.GetOrganizationDisplayName();
                row++;
            }

            using var stream2 = new MemoryStream();
            workbook.SaveAs(stream2);
            return File(stream2.ToArray(), @"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"OVTAsBulkUploadTemplate_{CurrentPerson.LastName}{CurrentPerson.FirstName}.xlsx");
        }


        private ActionResult RedirectToAppropriateStep(FormViewModel viewModel,
            OVTASection ovtaSection, OnlandVisualTrashAssessment ovta)
        {
            var nextSectionUrl = viewModel.AutoAdvance
                ? ovtaSection.GetNextSectionUrl(ovta, _linkGenerator)
                : ovtaSection.GetSectionUrl(ovta, _linkGenerator);
            return Redirect(nextSectionUrl);
        }

        private static DataTable GetDataTableFromExcel(Stream inputStream, dynamic worksheet)
        {
            var dataTable = new DataTable();
            using var workBook = new XLWorkbook(inputStream);
            IXLWorksheet workSheet = workBook.Worksheet(worksheet);

            //Loop through the Worksheet rows.
            var firstRow = true;
            foreach (var row in workSheet.Rows())
            {
                //Use the first row to add columns to DataTable.
                if (firstRow)
                {
                    foreach (var cell in row.Cells())
                    {
                        if (!string.IsNullOrEmpty(cell.Value.ToString()))
                        {
                            dataTable.Columns.Add(cell.Value.ToString());
                        }
                        else
                        {
                            break;
                        }
                    }
                    firstRow = false;
                }
                else
                {
                    var i = 0;
                    var toInsert = dataTable.NewRow();
                    foreach (var cell in row.Cells(1, dataTable.Columns.Count))
                    {
                        toInsert[i] = cell.Value.ToString();
                        i++;
                    }
                    dataTable.Rows.Add(toInsert);
                }
            }

            return dataTable;
        }
    }
}
