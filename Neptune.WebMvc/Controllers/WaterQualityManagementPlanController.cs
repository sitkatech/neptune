using Neptune.WebMvc.Common;
using Neptune.WebMvc.Models;
using Neptune.WebMvc.Security;
using Neptune.WebMvc.Views.Shared;
using Neptune.WebMvc.Views.Shared.HRUCharacteristics;
using Neptune.WebMvc.Views.Shared.ModeledPerformance;
using Neptune.WebMvc.Views.WaterQualityManagementPlan;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Neptune.Common;
using Neptune.Common.GeoSpatial;
using Neptune.EFModels.Entities;
using Neptune.EFModels.Nereid;
using Neptune.Models.DataTransferObjects;
using Neptune.WebMvc.Common.Models;
using Neptune.WebMvc.Common.MvcResults;
using Neptune.WebMvc.Services;
using Neptune.WebMvc.Services.Filters;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;

namespace Neptune.WebMvc.Controllers
{
    public class WaterQualityManagementPlanController : NeptuneBaseController<WaterQualityManagementPlanController>
    {
        private readonly FileResourceService _fileResourceService;

        public WaterQualityManagementPlanController(NeptuneDbContext dbContext, ILogger<WaterQualityManagementPlanController> logger, IOptions<WebConfiguration> webConfiguration, LinkGenerator linkGenerator, FileResourceService fileResourceService) : base(dbContext, logger, linkGenerator, webConfiguration)
        {
            _fileResourceService = fileResourceService;
        }

        [HttpGet]
        public ViewResult Index()
        {
            var neptunePage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.WaterQualityMaintenancePlan);
            var gridSpec = new IndexGridSpec(_linkGenerator, CurrentPerson);
            var verificationNeptunePage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.WaterQualityMaintenancePlanOandMVerifications);
            var verificationGridSpec = new WaterQualityManagementPlanVerificationGridSpec(_linkGenerator, CurrentPerson);
            var viewData = new IndexViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, neptunePage, gridSpec, verificationNeptunePage, verificationGridSpec);
            return RazorView< Views.WaterQualityManagementPlan.Index, IndexViewData>(viewData);
        }

        [HttpGet]
        public GridJsonNetJObjectResult<WaterQualityManagementPlanDetailedWithTreatmentBMPsAndQuickBMPs> WaterQualityManagementPlanIndexGridData()
        {
            var gridSpec = new IndexGridSpec(_linkGenerator, CurrentPerson);
            var treatmentBmps = _dbContext.TreatmentBMPs.Include(x => x.TreatmentBMPType)
                .Include(x => x.TreatmentBMPModelingAttributeTreatmentBMP).AsNoTracking()
                .Where(x => x.WaterQualityManagementPlanID != null).ToList();
            var treatmentBMPDetaileds = treatmentBmps.Join(
                    vTreatmentBMPUpstreams.ListWithDelineationAsDictionary(_dbContext),
                    x => x.TreatmentBMPID,
                    y => y.Key,
                    (x, y) => new TreatmentBMPWithModelingAttributesAndDelineation(x, y.Value)).ToList();
            var quickBMPs = QuickBMPs.List(_dbContext);
            var waterQualityManagementPlans =
                vWaterQualityManagementPlanDetaileds.ListViewableByPerson(_dbContext, CurrentPerson).OrderBy(x => x.WaterQualityManagementPlanName)
                    .ToList();
            var query = waterQualityManagementPlans
                .GroupJoin(treatmentBMPDetaileds,
                    waterQualityManagementPlanDetailed =>
                        waterQualityManagementPlanDetailed.WaterQualityManagementPlanID,
                    treatmentBMPWithModelingAttributesAndDelineation => treatmentBMPWithModelingAttributesAndDelineation
                        .TreatmentBMP.WaterQualityManagementPlanID,
                    (waterQualityManagementPlanDetailed, gj) => new { waterQualityManagementPlanDetailed, gj })
                .GroupJoin(quickBMPs, wqmp => wqmp.waterQualityManagementPlanDetailed.WaterQualityManagementPlanID,
                    quickBMP => quickBMP.WaterQualityManagementPlanID,
                    (wqmp, gj2) =>
                        new WaterQualityManagementPlanDetailedWithTreatmentBMPsAndQuickBMPs(
                            wqmp.waterQualityManagementPlanDetailed, wqmp.gj, gj2)).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<WaterQualityManagementPlanDetailedWithTreatmentBMPsAndQuickBMPs>(query, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpGet]
        public GridJsonNetJObjectResult<WaterQualityManagementPlanVerify> WaterQualityManagementPlanVerificationGridData()
        {
            var stormwaterJurisdictionIDsPersonCanView = StormwaterJurisdictionPeople.ListViewableStormwaterJurisdictionIDsByPersonForWQMPs(_dbContext, CurrentPerson);
            var waterQualityManagementPlanVerifications = WaterQualityManagementPlanVerifies.ListViewable(_dbContext, stormwaterJurisdictionIDsPersonCanView);
            var gridSpec = new WaterQualityManagementPlanVerificationGridSpec(_linkGenerator, CurrentPerson);
            return new GridJsonNetJObjectResult<WaterQualityManagementPlanVerify>(waterQualityManagementPlanVerifications, gridSpec);
        }

        [HttpGet]
        [SitkaAdminFeature]
        public ViewResult LGUAudit()
        {
            var wqmpGridSpec = new WaterQualityManagementPlanLGUAuditGridSpec(_linkGenerator);
            var viewData = new LGUAuditViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, wqmpGridSpec);
            return RazorView<LGUAudit, LGUAuditViewData>(viewData);
        }

        [HttpGet]
        [SitkaAdminFeature]
        public GridJsonNetJObjectResult<vWaterQualityManagementPlanLGUAudit> WaterQualityManagementPlanLGUAuditGridData()
        {
            var gridSpec = new WaterQualityManagementPlanLGUAuditGridSpec(_linkGenerator);
            return new GridJsonNetJObjectResult<vWaterQualityManagementPlanLGUAudit>(
                _dbContext.vWaterQualityManagementPlanLGUAudits.ToList(), gridSpec);
        }

        [HttpGet("{waterQualityManagementPlanPrimaryKey}")]
        [WaterQualityManagementPlanViewFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("waterQualityManagementPlanPrimaryKey")]
        public ViewResult Detail([FromRoute] WaterQualityManagementPlanPrimaryKey waterQualityManagementPlanPrimaryKey)
        {
            var waterQualityManagementPlan = WaterQualityManagementPlans.GetByID(_dbContext, waterQualityManagementPlanPrimaryKey);

            var wqmpBMPs = TreatmentBMPs.ListByWaterQualityManagementPlanID(_dbContext, waterQualityManagementPlan.WaterQualityManagementPlanID);
            var treatmentBMPs = CurrentPerson.GetInventoriedBMPsForWQMP(waterQualityManagementPlan, wqmpBMPs);
            var treatmentBmpGeoJsonFeatureCollection = treatmentBMPs.ToGeoJsonFeatureCollection(_linkGenerator);
            var boundingBoxGeometries = new List<Geometry>();

            foreach (var feature in treatmentBmpGeoJsonFeatureCollection)
            {
                var treatmentBmpID = feature.Attributes.Exists("TreatmentBMPID")
                    ? (int) feature.Attributes["TreatmentBMPID"]
                    : (int?)null;
                if (treatmentBmpID != null)
                {
                    feature.Attributes.Add("PopupUrl", SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(_linkGenerator, x => x.MapPopup(treatmentBmpID)));
                }
                boundingBoxGeometries.Add(feature.Geometry);
            }

            var boundaryAreaFeatureCollection = new FeatureCollection();

            var waterQualityManagementPlanBoundary = WaterQualityManagementPlanBoundaries.GetByWaterQualityManagementPlanID(_dbContext, waterQualityManagementPlan.WaterQualityManagementPlanID);
            var wqmpGeometry = waterQualityManagementPlanBoundary?.Geometry4326;
            var hasWaterQualityManagementPlanBoundary = wqmpGeometry != null;
            if (hasWaterQualityManagementPlanBoundary)
            {
                var feature = new Feature(wqmpGeometry, new AttributesTable());
                boundaryAreaFeatureCollection.Add(feature);
                boundingBoxGeometries.Add(wqmpGeometry);
            }

            var layerGeoJsons = new List<LayerGeoJson>
            {
                new("wqmpBoundary", boundaryAreaFeatureCollection, "#fb00be",
                    1,
                    LayerInitialVisibility.Show),
                new(FieldDefinitionType.TreatmentBMP.GetFieldDefinitionLabelPluralized(),
                    treatmentBmpGeoJsonFeatureCollection,
                    "#935f59",
                    1,
                    LayerInitialVisibility.Show),

            };

            var wqmpJurisdiction = waterQualityManagementPlan.StormwaterJurisdiction;
            var boundingBoxDto = hasWaterQualityManagementPlanBoundary ? new BoundingBoxDto(boundingBoxGeometries) : new BoundingBoxDto(wqmpJurisdiction.StormwaterJurisdictionGeometry?.Geometry4326);
            var mapInitJson = new MapInitJson("waterQualityManagementPlanMap", 0, layerGeoJsons,
                boundingBoxDto);

            var treatmentBMPIDList = treatmentBMPs.Select(x => x.TreatmentBMPID).ToList();
            var delineationsDict = vTreatmentBMPUpstreams.ListWithDelineationAsDictionaryForTreatmentBMPIDList(_dbContext, treatmentBMPIDList);

            var treatmentBMPDelineations = delineationsDict.Where(x => x.Value != null).Select(x => x.Value).ToList();
            if (treatmentBMPDelineations.Any())
            {
                mapInitJson.Layers.Add(StormwaterMapInitJson.MakeDelineationLayerGeoJson(treatmentBMPDelineations));
            }

            var waterQualityManagementPlanVerifies = WaterQualityManagementPlanVerifies.ListByWaterQualityManagementPlanID(_dbContext, waterQualityManagementPlan.PrimaryKey);
            var waterQualityManagementPlanVerifyDraft = waterQualityManagementPlanVerifies.SingleOrDefault(x => x.IsDraft);

            var waterQualityManagementPlanVerifyQuickBMP =
                WaterQualityManagementPlanVerifyQuickBMPs.ListByWaterQualityManagementPlanID(_dbContext, waterQualityManagementPlan.WaterQualityManagementPlanID);
            var waterQualityManagementPlanVerifyTreatmentBMP =
                WaterQualityManagementPlanVerifyTreatmentBMPs.ListByWaterQualityManagementPlanID(_dbContext, waterQualityManagementPlan.WaterQualityManagementPlanID);

            var waterQualityManagementPlanModelingApproaches = WaterQualityManagementPlanModelingApproach.All;

            var hruCharacteristics = vHRUCharacteristics.ListByWaterQualityManagementPlanID(_dbContext, waterQualityManagementPlan.WaterQualityManagementPlanID);
            var hruCharacteristicsViewData = new HRUCharacteristicsViewData(hruCharacteristics);
            var sourceControlBMPs = SourceControlBMPs.ListByWaterQualityManagementPlanID(_dbContext, waterQualityManagementPlan.WaterQualityManagementPlanID)
                .Where(x => x.SourceControlBMPNote != null || (x.IsPresent == true))
                .OrderBy(x => x.SourceControlBMPAttributeID)
                .GroupBy(x => x.SourceControlBMPAttribute.SourceControlBMPAttributeCategoryID);
            var quickBmps = QuickBMPs.ListByWaterQualityManagementPlanID(_dbContext, waterQualityManagementPlan.WaterQualityManagementPlanID);
            var modelingResultsUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(_linkGenerator, x => x.GetModelResults(waterQualityManagementPlan));
            var modeledPerformanceViewData = new ModeledPerformanceViewData(_linkGenerator, modelingResultsUrl, "Site Runoff");
            var parcelGridSpec = new ParcelGridSpec();

            var waterQualityManagementPlanDocuments = WaterQualityManagementPlanDocuments.ListByWaterQualityManagementPlanID(_dbContext, waterQualityManagementPlan.WaterQualityManagementPlanID);
            var calculateTotalAcreage = (waterQualityManagementPlanBoundary?.GeometryNative?.Area ?? 0) * Constants.SquareMetersToAcres;
            var viewData = new DetailViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, waterQualityManagementPlan,
                waterQualityManagementPlanVerifyDraft, mapInitJson, treatmentBMPs, parcelGridSpec,
                waterQualityManagementPlanVerifies, waterQualityManagementPlanVerifyQuickBMP,
                waterQualityManagementPlanVerifyTreatmentBMP,
                hruCharacteristicsViewData, waterQualityManagementPlanModelingApproaches, modeledPerformanceViewData, sourceControlBMPs, quickBmps, hasWaterQualityManagementPlanBoundary, delineationsDict, waterQualityManagementPlanDocuments, calculateTotalAcreage);

            return RazorView<Detail, DetailViewData>(viewData);
        }


        [HttpGet("{waterQualityManagementPlanPrimaryKey}")]
        [WaterQualityManagementPlanViewFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("waterQualityManagementPlanPrimaryKey")]
        public GridJsonNetJObjectResult<Parcel> ParcelsForWaterQualityManagementPlanGridData([FromRoute] WaterQualityManagementPlanPrimaryKey waterQualityManagementPlanPrimaryKey)
        {
            var parcels = WaterQualityManagementPlanParcels.ListByWaterQualityManagementPlanID(_dbContext, waterQualityManagementPlanPrimaryKey.PrimaryKeyValue).Select(x => x.Parcel).OrderBy(x => x.ParcelNumber).ToList();
            var gridSpec = new ParcelGridSpec();
            return new GridJsonNetJObjectResult<Parcel>(parcels, gridSpec);
        }

        #region CRUD Water Quality Management Plan
        [HttpGet]
        [WaterQualityManagementPlanCreateFeature]
        public PartialViewResult New()
        {
            var viewModel = new NewViewModel()
            {
                WaterQualityManagementPlanID = ModelObjectHelpers.NotYetAssignedID
            };
            return ViewNew(viewModel);
        }

        [HttpPost]
        [WaterQualityManagementPlanCreateFeature]
        public async Task<IActionResult> New(NewViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewNew(viewModel);
            }

            var waterQualityManagementPlan = new WaterQualityManagementPlan
            {
                TrashCaptureStatusTypeID = TrashCaptureStatusType.NotProvided.TrashCaptureStatusTypeID,
                WaterQualityManagementPlanModelingApproachID = WaterQualityManagementPlanModelingApproach.Detailed
                    .WaterQualityManagementPlanModelingApproachID
            };
            viewModel.UpdateModel(waterQualityManagementPlan);
            await _dbContext.WaterQualityManagementPlans.AddAsync(waterQualityManagementPlan);
            await _dbContext.SaveChangesAsync();

            SetMessageForDisplay($"Successfully created \"{waterQualityManagementPlan.WaterQualityManagementPlanName}\".");

            return new ModalDialogFormJsonResult(
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(_linkGenerator, x => x.Detail(waterQualityManagementPlan)));
        }

        private PartialViewResult ViewNew(NewViewModel viewModel)
        {
            var stormwaterJurisdictions = new List<Role> {Role.Admin, Role.SitkaAdmin}.Contains(CurrentPerson.Role)
                ? StormwaterJurisdictions.List(_dbContext)
                : StormwaterJurisdictions.ListViewableByPersonForWQMPs(_dbContext, CurrentPerson);
            var hydrologicSubareas = _dbContext.HydrologicSubareas.ToList();
            var viewData = new NewViewData(stormwaterJurisdictions, hydrologicSubareas, TrashCaptureStatusType.All);
            return RazorPartialView<New, NewViewData, NewViewModel>(viewData, viewModel);
        }

        [HttpGet("{waterQualityManagementPlanPrimaryKey}")]
        [WaterQualityManagementPlanManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("waterQualityManagementPlanPrimaryKey")]
        public PartialViewResult Edit([FromRoute] WaterQualityManagementPlanPrimaryKey waterQualityManagementPlanPrimaryKey)
        {
            var waterQualityManagementPlan = WaterQualityManagementPlans.GetByID(_dbContext, waterQualityManagementPlanPrimaryKey);
            var viewModel = new EditViewModel(waterQualityManagementPlan);
            return ViewEdit(viewModel);
        }

        [HttpPost("{waterQualityManagementPlanPrimaryKey}")]
        [WaterQualityManagementPlanManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("waterQualityManagementPlanPrimaryKey")]
        public async Task<IActionResult> Edit([FromRoute] WaterQualityManagementPlanPrimaryKey waterQualityManagementPlanPrimaryKey, EditViewModel viewModel)
        {
            var waterQualityManagementPlan = WaterQualityManagementPlans.GetByIDWithChangeTracking(_dbContext, waterQualityManagementPlanPrimaryKey);
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel);
            }

            viewModel.UpdateModel(waterQualityManagementPlan);
            SetMessageForDisplay($"Successfully updated \"{waterQualityManagementPlan.WaterQualityManagementPlanName}\".");
            await _dbContext.SaveChangesAsync();

            return new ModalDialogFormJsonResult(
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(_linkGenerator, x => x.Detail(waterQualityManagementPlan)));
        }

        private PartialViewResult ViewEdit(EditViewModel viewModel)
        {
            var hydrologicSubareas = _dbContext.HydrologicSubareas.ToList();
            var viewData = new EditViewData(hydrologicSubareas, TrashCaptureStatusType.All);
            return RazorPartialView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }

        [HttpGet("{waterQualityManagementPlanPrimaryKey}")]
        [WaterQualityManagementPlanDeleteFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("waterQualityManagementPlanPrimaryKey")]
        public PartialViewResult Delete([FromRoute] WaterQualityManagementPlanPrimaryKey waterQualityManagementPlanPrimaryKey)
        {
            var waterQualityManagementPlan = waterQualityManagementPlanPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(waterQualityManagementPlan.WaterQualityManagementPlanID);
            return ViewDelete(waterQualityManagementPlan, viewModel);
        }

        [HttpPost("{waterQualityManagementPlanPrimaryKey}")]
        [WaterQualityManagementPlanDeleteFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("waterQualityManagementPlanPrimaryKey")]
        public async Task<IActionResult> Delete([FromRoute] WaterQualityManagementPlanPrimaryKey waterQualityManagementPlanPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var waterQualityManagementPlan = WaterQualityManagementPlans.GetByIDWithChangeTracking(_dbContext, waterQualityManagementPlanPrimaryKey);
            if (!ModelState.IsValid)
            {
                return ViewDelete(waterQualityManagementPlan, viewModel);
            }

            await NereidUtilities.MarkDownstreamNodeDirty(waterQualityManagementPlan, _dbContext);

            await waterQualityManagementPlan.DeleteFull(_dbContext);
            await _dbContext.SaveChangesAsync();

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
        [HttpGet("{waterQualityManagementPlanPrimaryKey}")]
        [WaterQualityManagementPlanViewFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("waterQualityManagementPlanPrimaryKey")]
        public ViewResult EditTreatmentBMPs([FromRoute] WaterQualityManagementPlanPrimaryKey waterQualityManagementPlanPrimaryKey)
        {
            var waterQualityManagementPlan = WaterQualityManagementPlans.GetByID(_dbContext, waterQualityManagementPlanPrimaryKey);
            var treatmentBmpIDs = TreatmentBMPs.ListByWaterQualityManagementPlanID(_dbContext, waterQualityManagementPlan.WaterQualityManagementPlanID).Select(x => x.TreatmentBMPID).ToList();
            var viewModel = new EditTreatmentBMPsViewModel(treatmentBmpIDs);
            return ViewEditTreatmentBMPs(waterQualityManagementPlan, viewModel);
        }

        [HttpPost("{waterQualityManagementPlanPrimaryKey}")]
        [WaterQualityManagementPlanViewFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("waterQualityManagementPlanPrimaryKey")]
        public async Task<IActionResult> EditTreatmentBMPs([FromRoute] WaterQualityManagementPlanPrimaryKey waterQualityManagementPlanPrimaryKey, EditTreatmentBMPsViewModel viewModel)
        {
            var waterQualityManagementPlan = WaterQualityManagementPlans.GetByIDWithChangeTracking(_dbContext, waterQualityManagementPlanPrimaryKey);
            if (!ModelState.IsValid)
            {
                return ViewEditTreatmentBMPs(waterQualityManagementPlan, viewModel);
            }

            var treatmentBMPs = TreatmentBMPs.ListByWaterQualityManagementPlanIDWithChangeTracking(_dbContext, waterQualityManagementPlan.WaterQualityManagementPlanID);
            viewModel.UpdateModel(waterQualityManagementPlan, _dbContext, treatmentBMPs);
            await _dbContext.SaveChangesAsync();
            SetMessageForDisplay($"Successfully updated BMPs for {waterQualityManagementPlan.WaterQualityManagementPlanName}");

            await NereidUtilities.MarkWqmpDirty(waterQualityManagementPlan, _dbContext);

            return RedirectToAction(new SitkaRoute<WaterQualityManagementPlanController>(_linkGenerator, x => x.Detail(waterQualityManagementPlanPrimaryKey)));
        }

        private ViewResult ViewEditTreatmentBMPs(WaterQualityManagementPlan waterQualityManagementPlan,
            EditTreatmentBMPsViewModel viewModel)
        {
            var treatmentBmps = TreatmentBMPs.ListByStormwaterJurisdictionID(_dbContext, waterQualityManagementPlan.StormwaterJurisdictionID)
                .Where(x => x.WaterQualityManagementPlanID == null ||
                            x.WaterQualityManagementPlanID == waterQualityManagementPlan.WaterQualityManagementPlanID);
            var viewData = new EditTreatmentBMPsViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, waterQualityManagementPlan, treatmentBmps
                .Select(x => x.AsDisplayDto())
                .OrderBy(x => x.DisplayName)
                .ToList());
            return RazorView<EditTreatmentBMPs, EditTreatmentBMPsViewData, EditTreatmentBMPsViewModel>(viewData, viewModel);
        }

        [HttpGet("{waterQualityManagementPlanPrimaryKey}")]
        [WaterQualityManagementPlanViewFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("waterQualityManagementPlanPrimaryKey")]
        public ViewResult EditSimplifiedStructuralBMPs([FromRoute] WaterQualityManagementPlanPrimaryKey waterQualityManagementPlanPrimaryKey)
        {
            var waterQualityManagementPlan = WaterQualityManagementPlans.GetByID(_dbContext, waterQualityManagementPlanPrimaryKey);
            var quickBMPSimpleDtos = QuickBMPs.ListByWaterQualityManagementPlanID(_dbContext, waterQualityManagementPlan.WaterQualityManagementPlanID).Select(x => x.AsUpsertDto()).ToList();
            var viewModel = new EditSimplifiedStructuralBMPsViewModel(quickBMPSimpleDtos);
            return ViewEditSimplifiedStructuralBMPs(waterQualityManagementPlan, viewModel);
        }

        [HttpPost("{waterQualityManagementPlanPrimaryKey}")]
        [WaterQualityManagementPlanViewFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("waterQualityManagementPlanPrimaryKey")]
        public async Task<IActionResult> EditSimplifiedStructuralBMPs([FromRoute] WaterQualityManagementPlanPrimaryKey waterQualityManagementPlanPrimaryKey, EditSimplifiedStructuralBMPsViewModel viewModel)
        {
            var waterQualityManagementPlan = WaterQualityManagementPlans.GetByIDWithChangeTracking(_dbContext, waterQualityManagementPlanPrimaryKey);
            if (!ModelState.IsValid)
            {
                return ViewEditSimplifiedStructuralBMPs(waterQualityManagementPlan, viewModel);
            }

            var existingQuickBMPs = QuickBMPs.ListByWaterQualityManagementPlanIDWithChangeTracking(_dbContext, waterQualityManagementPlan.WaterQualityManagementPlanID);
            viewModel.UpdateModel(waterQualityManagementPlan, _dbContext, existingQuickBMPs);
            await _dbContext.SaveChangesAsync();
            SetMessageForDisplay(
                $"Successfully updated BMPs for {waterQualityManagementPlan.WaterQualityManagementPlanName}");

            await NereidUtilities.MarkWqmpDirty(waterQualityManagementPlan, _dbContext);

            return RedirectToAction(new SitkaRoute<WaterQualityManagementPlanController>(_linkGenerator, x => x.Detail(waterQualityManagementPlanPrimaryKey)));
        }

        private ViewResult ViewEditSimplifiedStructuralBMPs(WaterQualityManagementPlan waterQualityManagementPlan,
            EditSimplifiedStructuralBMPsViewModel viewModel)
        {
            var treatmentBMPTypes = TreatmentBMPTypes.List(_dbContext).Select(x => x.AsSimpleDto());
            var dryWeatherFlowOverrides = DryWeatherFlowOverride.All;
            var dryWeatherFlowOverrideDefaultID = DryWeatherFlowOverride.No.DryWeatherFlowOverrideID;
            var dryWeatherFlowOverrideYesID = DryWeatherFlowOverride.Yes.DryWeatherFlowOverrideID;
            var viewData = new EditSimplifiedStructuralBMPsViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, waterQualityManagementPlan, treatmentBMPTypes, dryWeatherFlowOverrides, dryWeatherFlowOverrideDefaultID, dryWeatherFlowOverrideYesID);
            return RazorView<EditSimplifiedStructuralBMPs, EditSimplifiedStructuralBMPsViewData, EditSimplifiedStructuralBMPsViewModel>(viewData, viewModel);
        }

        [HttpGet("{waterQualityManagementPlanPrimaryKey}")]
        [WaterQualityManagementPlanViewFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("waterQualityManagementPlanPrimaryKey")]
        public ViewResult EditSourceControlBMPs([FromRoute] WaterQualityManagementPlanPrimaryKey waterQualityManagementPlanPrimaryKey)
        {
            var waterQualityManagementPlan = WaterQualityManagementPlans.GetByID(_dbContext, waterQualityManagementPlanPrimaryKey);
            var sourceControlBMPAttributes = SourceControlBMPAttributes.List(_dbContext);
            var sourceControlBMPs = SourceControlBMPs.ListByWaterQualityManagementPlanID(_dbContext, waterQualityManagementPlan.WaterQualityManagementPlanID);
            var sourceControlBMPUpsertDtos = new List<SourceControlBMPUpsertDto>();
            if (!sourceControlBMPs.Any())
            {
                sourceControlBMPUpsertDtos.AddRange(sourceControlBMPAttributes.OrderBy(x => x.SourceControlBMPAttributeID).Select(sourceControlBMPAttribute => sourceControlBMPAttribute.AsUpsertDto()));
            }
            else
            {
                sourceControlBMPUpsertDtos = sourceControlBMPs.Select(x => x.AsUpsertDto()).ToList();
            }
            var viewModel = new EditSourceControlBMPsViewModel(sourceControlBMPUpsertDtos);
            return ViewEditSourceControlBMPs(waterQualityManagementPlan, viewModel);
        }

        [HttpPost("{waterQualityManagementPlanPrimaryKey}")]
        [WaterQualityManagementPlanViewFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("waterQualityManagementPlanPrimaryKey")]
        public async Task<IActionResult> EditSourceControlBMPs([FromRoute] WaterQualityManagementPlanPrimaryKey waterQualityManagementPlanPrimaryKey, EditSourceControlBMPsViewModel viewModel)
        {
            var waterQualityManagementPlan = WaterQualityManagementPlans.GetByIDWithChangeTracking(_dbContext, waterQualityManagementPlanPrimaryKey);
            if (!ModelState.IsValid)
            {
                return ViewEditSourceControlBMPs(waterQualityManagementPlan, viewModel);
            }

            var existingSourceControlBMPs = SourceControlBMPs.ListByWaterQualityManagementPlanIDWithChangeTracking(_dbContext, waterQualityManagementPlan.WaterQualityManagementPlanID);
            viewModel.UpdateModel(_dbContext, waterQualityManagementPlan, existingSourceControlBMPs);
            SetMessageForDisplay($"Successfully updated BMPs for {waterQualityManagementPlan.WaterQualityManagementPlanName}");

            await NereidUtilities.MarkWqmpDirty(waterQualityManagementPlan, _dbContext);

            return RedirectToAction(new SitkaRoute<WaterQualityManagementPlanController>(_linkGenerator ,x => x.Detail(waterQualityManagementPlanPrimaryKey)));
        }

        private ViewResult ViewEditSourceControlBMPs(WaterQualityManagementPlan waterQualityManagementPlan,
            EditSourceControlBMPsViewModel viewModel)
        {
            var viewData = new EditSourceControlBMPsViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, waterQualityManagementPlan);
            return RazorView<EditSourceControlBMPs, EditSourceControlBMPsViewData, EditSourceControlBMPsViewModel>(viewData, viewModel);
        }


        #endregion

        #region WQMP Parcels

        [HttpGet("{waterQualityManagementPlanPrimaryKey}")]
        [WaterQualityManagementPlanManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("waterQualityManagementPlanPrimaryKey")]
        public ViewResult EditParcels([FromRoute] WaterQualityManagementPlanPrimaryKey waterQualityManagementPlanPrimaryKey)
        {
            var waterQualityManagementPlan = WaterQualityManagementPlans.GetByID(_dbContext, waterQualityManagementPlanPrimaryKey);
            var waterQualityManagementPlanParcels = WaterQualityManagementPlanParcels.ListByWaterQualityManagementPlanID(_dbContext, waterQualityManagementPlan.WaterQualityManagementPlanID);
            var parcelIDs = waterQualityManagementPlanParcels.Select(x => x.ParcelID).ToList();
            var viewModel = new EditParcelsViewModel(parcelIDs);
            return ViewEditParcels(waterQualityManagementPlan, viewModel, waterQualityManagementPlanParcels);
        }

        [HttpPost("{waterQualityManagementPlanPrimaryKey}")]
        [WaterQualityManagementPlanManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("waterQualityManagementPlanPrimaryKey")]
        public async Task<IActionResult> EditParcels([FromRoute] WaterQualityManagementPlanPrimaryKey waterQualityManagementPlanPrimaryKey, EditParcelsViewModel viewModel)
        {
            var waterQualityManagementPlan = WaterQualityManagementPlans.GetByIDWithChangeTracking(_dbContext, waterQualityManagementPlanPrimaryKey);
            var waterQualityManagementPlanParcels = WaterQualityManagementPlanParcels.ListByWaterQualityManagementPlanIDWithChangeTracking(_dbContext, waterQualityManagementPlan.WaterQualityManagementPlanID);
            if (!ModelState.IsValid)
            {
                return ViewEditParcels(waterQualityManagementPlan, viewModel, waterQualityManagementPlanParcels);
            }

            var waterQualityManagementPlanBoundary = WaterQualityManagementPlanBoundaries.GetByWaterQualityManagementPlanIDWithChangeTracking(_dbContext, waterQualityManagementPlan.WaterQualityManagementPlanID);
            var oldBoundary = waterQualityManagementPlanBoundary?.GeometryNative;

            await viewModel.UpdateModels(waterQualityManagementPlan, _dbContext, waterQualityManagementPlanParcels, waterQualityManagementPlanBoundary);
            SetMessageForDisplay($"Successfully edited {FieldDefinitionType.Parcel.GetFieldDefinitionLabelPluralized()} for {FieldDefinitionType.WaterQualityManagementPlan.GetFieldDefinitionLabel()}.");
            await _dbContext.SaveChangesAsync();

            var newBoundary = waterQualityManagementPlan.WaterQualityManagementPlanBoundary?.GeometryNative;

            if (!(oldBoundary == null && newBoundary == null))
            {
                await ModelingEngineUtilities.QueueLGURefreshForArea(oldBoundary, newBoundary, _dbContext);
            }

            await NereidUtilities.MarkWqmpDirty(waterQualityManagementPlan, _dbContext);

            return RedirectToAction(new SitkaRoute<WaterQualityManagementPlanController>(_linkGenerator, x => x.Detail(waterQualityManagementPlan)));
        }

        private ViewResult ViewEditParcels(WaterQualityManagementPlan waterQualityManagementPlan, EditParcelsViewModel viewModel, ICollection<WaterQualityManagementPlanParcel> waterQualityManagementPlanParcels)
        {
            var wqmpParcelGeometries =
                waterQualityManagementPlanParcels.Select(x => x.Parcel.ParcelGeometry?.Geometry4326);
            var wqmpJurisdiction = waterQualityManagementPlan.StormwaterJurisdiction;
            var mapInitJson = new MapInitJson("editWqmpParcelMap", 0, new List<LayerGeoJson>(), wqmpParcelGeometries.Any() ?
                    new BoundingBoxDto(wqmpParcelGeometries) : new BoundingBoxDto(wqmpJurisdiction.StormwaterJurisdictionGeometry?.Geometry4326));
            var viewData = new EditParcelsViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, waterQualityManagementPlan, mapInitJson, _webConfiguration.MapServiceUrl, Constants.MapServiceLayerNameParcel, waterQualityManagementPlanParcels);
            return RazorView<EditParcels, EditParcelsViewData, EditParcelsViewModel>(viewData, viewModel);
        }

        #endregion


        private ViewResult ViewRefineArea(WaterQualityManagementPlan waterQualityManagementPlan, RefineAreaViewModel viewModel)
        {
            var waterQualityManagementPlanBoundary = WaterQualityManagementPlanBoundaries.GetByWaterQualityManagementPlanID(_dbContext, waterQualityManagementPlan.WaterQualityManagementPlanID);
            var geometry4326 = waterQualityManagementPlanBoundary?.Geometry4326;
            var featureCollection = geometry4326?.MultiPolygonToFeatureCollection();
            var boundingBoxDto = new BoundingBoxDto(geometry4326);
            var mapInitJson = new MapInitJson("EditWQMPBoundaryMap", 10, new List<LayerGeoJson>(), boundingBoxDto);
            var neptunePage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.EditWQMPBoundary);
            var viewData = new RefineAreaViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, neptunePage, waterQualityManagementPlan, mapInitJson, _webConfiguration.MapServiceUrl, featureCollection);
            return RazorView<RefineArea, RefineAreaViewData, RefineAreaViewModel>(viewData, viewModel);
        }

        [HttpGet("{waterQualityManagementPlanPrimaryKey}")]
        [WaterQualityManagementPlanManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("waterQualityManagementPlanPrimaryKey")]
        public ViewResult RefineArea([FromRoute] WaterQualityManagementPlanPrimaryKey waterQualityManagementPlanPrimaryKey)
        {
            var waterQualityManagementPlan = WaterQualityManagementPlans.GetByID(_dbContext, waterQualityManagementPlanPrimaryKey);
            var viewModel = new RefineAreaViewModel();
            return ViewRefineArea(waterQualityManagementPlan, viewModel);
        }

        [HttpPost("{waterQualityManagementPlanPrimaryKey}")]
        [WaterQualityManagementPlanManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("waterQualityManagementPlanPrimaryKey")]
        public async Task<IActionResult> RefineArea([FromRoute] WaterQualityManagementPlanPrimaryKey waterQualityManagementPlanPrimaryKey, RefineAreaViewModel viewModel)
        {
            var waterQualityManagementPlan = WaterQualityManagementPlans.GetByIDWithChangeTracking(_dbContext, waterQualityManagementPlanPrimaryKey);
            if (!ModelState.IsValid)
            {
                return ViewRefineArea(waterQualityManagementPlan, viewModel);
            }

            var waterQualityManagementPlanBoundary = WaterQualityManagementPlanBoundaries.GetByWaterQualityManagementPlanIDWithChangeTracking(_dbContext, waterQualityManagementPlan.WaterQualityManagementPlanID);
            var oldBoundary = waterQualityManagementPlanBoundary?.GeometryNative;

            viewModel.UpdateModel(waterQualityManagementPlan, _dbContext, waterQualityManagementPlanBoundary);
            await _dbContext.SaveChangesAsync();
            SetMessageForDisplay($"Successfully edited boundary for {FieldDefinitionType.WaterQualityManagementPlan.GetFieldDefinitionLabel()}.");

            var newBoundary = waterQualityManagementPlanBoundary?.GeometryNative;

            if (!(oldBoundary == null && newBoundary == null))
            {
                await ModelingEngineUtilities.QueueLGURefreshForArea(oldBoundary, newBoundary, _dbContext);
            }

            await NereidUtilities.MarkWqmpDirty(waterQualityManagementPlan, _dbContext);

            return RedirectToAction(new SitkaRoute<WaterQualityManagementPlanController>(_linkGenerator,
                x => x.Detail(waterQualityManagementPlan)));
        }
        #region WQMP O&M Verification Record




        [HttpGet("{waterQualityManagementPlanVerifyPrimaryKey}")]
        [WaterQualityManagementPlanVerifyViewFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("waterQualityManagementPlanVerifyPrimaryKey")]
        public ViewResult WqmpVerify([FromRoute] WaterQualityManagementPlanVerifyPrimaryKey waterQualityManagementPlanVerifyPrimaryKey)
        {
            var waterQualityManagementPlanVerify = WaterQualityManagementPlanVerifies.GetByID(_dbContext, waterQualityManagementPlanVerifyPrimaryKey);
            var waterQualityManagementPlanVerifyQuickBMPs =
                WaterQualityManagementPlanVerifyQuickBMPs.ListByWaterQualityManagementPlanVerifyID(_dbContext, waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyID);
            var waterQualityManagementPlanVerifyTreatmentBMPs =
                WaterQualityManagementPlanVerifyTreatmentBMPs.ListByWaterQualityManagementPlanVerifyID(_dbContext, waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyID);

            var viewData = new WqmpVerifyViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, waterQualityManagementPlanVerify, waterQualityManagementPlanVerifyQuickBMPs, waterQualityManagementPlanVerifyTreatmentBMPs);

            return RazorView<WqmpVerify, WqmpVerifyViewData>(viewData);
        }

        [HttpGet("{waterQualityManagementPlanPrimaryKey}")]
        [WaterQualityManagementPlanManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("waterQualityManagementPlanPrimaryKey")]
        public ViewResult NewWqmpVerify([FromRoute] WaterQualityManagementPlanPrimaryKey waterQualityManagementPlanPrimaryKey)
        {
            var waterQualityManagementPlan = WaterQualityManagementPlans.GetByID(_dbContext, waterQualityManagementPlanPrimaryKey);
            var quickBMPs = QuickBMPs.ListByWaterQualityManagementPlanID(_dbContext, waterQualityManagementPlan.WaterQualityManagementPlanID);
            var treatmentBMPs = TreatmentBMPs.ListByWaterQualityManagementPlanID(_dbContext, waterQualityManagementPlan.WaterQualityManagementPlanID);
            var waterQualityManagementPlanVerify = new WaterQualityManagementPlanVerify
            {
                WaterQualityManagementPlanID = waterQualityManagementPlan.WaterQualityManagementPlanID,
                LastEditedByPersonID = CurrentPerson.PersonID,
                LastEditedDate = DateTime.Now,
                IsDraft = true,
                VerificationDate = DateTime.Now
            };
            var viewModel = new NewWqmpVerifyViewModel(waterQualityManagementPlan, waterQualityManagementPlanVerify, quickBMPs, treatmentBMPs);
            return ViewNewWqmpVerify(waterQualityManagementPlan, viewModel);
        }

        [HttpPost("{waterQualityManagementPlanPrimaryKey}")]
        [WaterQualityManagementPlanManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("waterQualityManagementPlanPrimaryKey")]
        public async Task<IActionResult> NewWqmpVerify([FromRoute] WaterQualityManagementPlanPrimaryKey waterQualityManagementPlanPrimaryKey, NewWqmpVerifyViewModel viewModel)
        {
            var waterQualityManagementPlan = WaterQualityManagementPlans.GetByIDWithChangeTracking(_dbContext, waterQualityManagementPlanPrimaryKey);
            if (!ModelState.IsValid)
            {
                return ViewNewWqmpVerify(waterQualityManagementPlan, viewModel);
            }

            var waterQualityManagementPlanVerify = new WaterQualityManagementPlanVerify{
                WaterQualityManagementPlanID = waterQualityManagementPlan.WaterQualityManagementPlanID,
                WaterQualityManagementPlanVerifyTypeID = viewModel.WaterQualityManagementPlanVerifyTypeID,
                WaterQualityManagementPlanVisitStatusID = viewModel.WaterQualityManagementPlanVisitStatusID,
                LastEditedByPersonID = CurrentPerson.PersonID,
                LastEditedDate = DateTime.Now,
                IsDraft = !viewModel.HiddenIsFinalizeVerificationInput,
                VerificationDate = viewModel.VerificationDate
            };
            await _dbContext.WaterQualityManagementPlanVerifies.AddAsync(waterQualityManagementPlanVerify);
            await _dbContext.SaveChangesAsync();

            await viewModel.UpdateModel(waterQualityManagementPlanVerify, CurrentPerson, _dbContext, _fileResourceService, new List<WaterQualityManagementPlanVerifyQuickBMP>(), new List<WaterQualityManagementPlanVerifyTreatmentBMP>());

            await _dbContext.SaveChangesAsync();

            SetMessageForDisplay(
                $"Successfully updated {FieldDefinitionType.TreatmentBMP.GetFieldDefinitionLabelPluralized()} for {waterQualityManagementPlan.WaterQualityManagementPlanName}");

            return RedirectToAction(new SitkaRoute<WaterQualityManagementPlanController>(_linkGenerator, x => x.Detail(waterQualityManagementPlan)));
        }

        private ViewResult ViewNewWqmpVerify(WaterQualityManagementPlan waterQualityManagementPlan, NewWqmpVerifyViewModel viewModel)
        {
            var waterQualityManagementPlanVerifyTypes = WaterQualityManagementPlanVerifyType.All;
            var waterQualityManagementPlanVisitStatuses = WaterQualityManagementPlanVisitStatus.All;
            var waterQualityManagementPlanVerifyStatuses = WaterQualityManagementPlanVerifyStatus.All;
            var viewData = new NewWqmpVerifyViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, waterQualityManagementPlan, waterQualityManagementPlanVerifyTypes, waterQualityManagementPlanVisitStatuses, waterQualityManagementPlanVerifyStatuses);
            return RazorView<NewWqmpVerify, NewWqmpVerifyViewData, NewWqmpVerifyViewModel>(viewData, viewModel);
        }

        [HttpGet("{waterQualityManagementPlanVerifyPrimaryKey}")]
        [WaterQualityManagementPlanVerifyManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("waterQualityManagementPlanVerifyPrimaryKey")]
        public ViewResult EditWqmpVerify([FromRoute] WaterQualityManagementPlanVerifyPrimaryKey waterQualityManagementPlanVerifyPrimaryKey)
        {
            var waterQualityManagementPlanVerify = WaterQualityManagementPlanVerifies.GetByID(_dbContext, waterQualityManagementPlanVerifyPrimaryKey);

            var waterQualityManagementPlanVerifyQuickBMPs =
                WaterQualityManagementPlanVerifyQuickBMPs.ListByWaterQualityManagementPlanVerifyID(_dbContext, waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyID);
            var waterQualityManagementPlanVerifyTreatmentBMPs =
                WaterQualityManagementPlanVerifyTreatmentBMPs.ListByWaterQualityManagementPlanVerifyID(_dbContext, waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyID);

            var viewModel = new EditWqmpVerifyViewModel(waterQualityManagementPlanVerify, waterQualityManagementPlanVerifyQuickBMPs, waterQualityManagementPlanVerifyTreatmentBMPs);
            return ViewEditWqmpVerify(waterQualityManagementPlanVerify.WaterQualityManagementPlan, viewModel);
        }

        [HttpPost("{waterQualityManagementPlanVerifyPrimaryKey}")]
        [WaterQualityManagementPlanVerifyManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("waterQualityManagementPlanVerifyPrimaryKey")]
        public async Task<IActionResult> EditWqmpVerify([FromRoute] WaterQualityManagementPlanVerifyPrimaryKey waterQualityManagementPlanVerifyPrimaryKey, EditWqmpVerifyViewModel viewModel)
        {
            var waterQualityManagementPlanVerify = WaterQualityManagementPlanVerifies.GetByIDWithChangeTracking(_dbContext, waterQualityManagementPlanVerifyPrimaryKey);
            if (!ModelState.IsValid)
            {
                return ViewEditWqmpVerify(waterQualityManagementPlanVerify.WaterQualityManagementPlan, viewModel);
            }
            var waterQualityManagementPlan = waterQualityManagementPlanVerify.WaterQualityManagementPlan;
            waterQualityManagementPlanVerify.IsDraft = !viewModel.HiddenIsFinalizeVerificationInput;
            var waterQualityManagementPlanVerifyQuickBMPs = WaterQualityManagementPlanVerifyQuickBMPs.ListByWaterQualityManagementPlanVerifyIDWithChangeTracking(_dbContext, waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyID);
            var waterQualityManagementPlanVerifyTreatmentBMPs = WaterQualityManagementPlanVerifyTreatmentBMPs.ListByWaterQualityManagementPlanVerifyIDWithChangeTracking(_dbContext, waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyID);
            await viewModel.UpdateModel(waterQualityManagementPlanVerify, CurrentPerson, _dbContext, _fileResourceService, waterQualityManagementPlanVerifyQuickBMPs, waterQualityManagementPlanVerifyTreatmentBMPs);

            await _dbContext.SaveChangesAsync();

            SetMessageForDisplay(
                $"Successfully updated Verification for {waterQualityManagementPlan.WaterQualityManagementPlanName}");

            return RedirectToAction(new SitkaRoute<WaterQualityManagementPlanController>(_linkGenerator, x => x.Detail(waterQualityManagementPlan)));
        }

        private ViewResult ViewEditWqmpVerify(WaterQualityManagementPlan waterQualityManagementPlan, EditWqmpVerifyViewModel viewModel)
        {
            var waterQualityManagementPlanVerifyTypes = WaterQualityManagementPlanVerifyType.All;
            var waterQualityManagementPlanVisitStatuses = WaterQualityManagementPlanVisitStatus.All;
            var waterQualityManagementPlanVerifyStatuses = WaterQualityManagementPlanVerifyStatus.All;
            var viewData = new EditWqmpVerifyViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, waterQualityManagementPlan, waterQualityManagementPlanVerifyTypes, waterQualityManagementPlanVisitStatuses, waterQualityManagementPlanVerifyStatuses);
            return RazorView<EditWqmpVerify, EditWqmpVerifyViewData, EditWqmpVerifyViewModel>(viewData, viewModel);
        }

        [HttpGet("{waterQualityManagementPlanVerifyPrimaryKey}")]
        [WaterQualityManagementPlanVerifyDeleteFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("waterQualityManagementPlanVerifyPrimaryKey")]
        public PartialViewResult DeleteVerify([FromRoute] WaterQualityManagementPlanVerifyPrimaryKey waterQualityManagementPlanVerifyPrimaryKey)
        {
            var waterQualityManagementPlanVerify = waterQualityManagementPlanVerifyPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyID);
            return ViewDeleteVerify(waterQualityManagementPlanVerify, viewModel);
        }

        [HttpPost("{waterQualityManagementPlanVerifyPrimaryKey}")]
        [WaterQualityManagementPlanVerifyDeleteFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("waterQualityManagementPlanVerifyPrimaryKey")]
        public async Task<IActionResult> DeleteVerify([FromRoute] WaterQualityManagementPlanVerifyPrimaryKey waterQualityManagementPlanVerifyPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var waterQualityManagementPlanVerify = waterQualityManagementPlanVerifyPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteVerify(waterQualityManagementPlanVerify, viewModel);
            }

            var lastEditedDate = waterQualityManagementPlanVerify.LastEditedDate.ToShortDateString();

            waterQualityManagementPlanVerify.DeleteFull(_dbContext);
            await _dbContext.SaveChangesAsync();

            SetMessageForDisplay($"Successfully deleted \"{lastEditedDate}\".");

            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewDeleteVerify(WaterQualityManagementPlanVerify waterQualityManagementPlanVerify, ConfirmDialogFormViewModel viewModel)
        {
            var viewData = new ConfirmDialogFormViewData(
                $"Are you sure you want to delete the O&M Verification last edited on {waterQualityManagementPlanVerify.LastEditedDate.ToShortDateString()}?");
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }


        [HttpGet("{waterQualityManagementPlanVerifyPrimaryKey}")]
        [WaterQualityManagementPlanVerifyManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("waterQualityManagementPlanVerifyPrimaryKey")]
        public PartialViewResult EditWqmpVerifyModal([FromRoute] WaterQualityManagementPlanVerifyPrimaryKey waterQualityManagementPlanVerifyPrimaryKey)
        {
            var waterQualityManagementPlanVerify = waterQualityManagementPlanVerifyPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyID);
            return ViewEditWqmpVerifyModal(viewModel);
        }

        [HttpPost("{waterQualityManagementPlanVerifyPrimaryKey}")]
        [WaterQualityManagementPlanVerifyDeleteFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("waterQualityManagementPlanVerifyPrimaryKey")]
        public ActionResult EditWqmpVerifyModal([FromRoute] WaterQualityManagementPlanVerifyPrimaryKey waterQualityManagementPlanVerifyPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEditWqmpVerifyModal(viewModel);
            }

            return new ModalDialogFormJsonResult(
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(_linkGenerator, x => x.EditWqmpVerify(waterQualityManagementPlanVerifyPrimaryKey)));
        }

        private PartialViewResult ViewEditWqmpVerifyModal(ConfirmDialogFormViewModel viewModel)
        {
            var viewData = new ConfirmDialogFormViewData(
                "There is a verification in progress. Click OK to resume the existing verification record. Alternately, delete the in-progress verification from the verification panel on the WQMP page");
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }
        #endregion

        [HttpGet("{waterQualityManagementPlanPrimaryKey}")]
        [WaterQualityManagementPlanViewFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("waterQualityManagementPlanPrimaryKey")]
        public PartialViewResult EditModelingApproach([FromRoute] WaterQualityManagementPlanPrimaryKey waterQualityManagementPlanPrimaryKey)
        {
            var waterQualityManagementPlan = WaterQualityManagementPlans.GetByID(_dbContext, waterQualityManagementPlanPrimaryKey);
            var viewModel = new EditModelingApproachViewModel(waterQualityManagementPlan);
            return ViewEditModelingApproach(viewModel);
        }

        [HttpPost("{waterQualityManagementPlanPrimaryKey}")]
        [WaterQualityManagementPlanViewFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("waterQualityManagementPlanPrimaryKey")]
        public async Task<IActionResult> EditModelingApproach([FromRoute] WaterQualityManagementPlanPrimaryKey waterQualityManagementPlanPrimaryKey, EditModelingApproachViewModel viewModel)
        {
            var waterQualityManagementPlan = WaterQualityManagementPlans.GetByIDWithChangeTracking(_dbContext, waterQualityManagementPlanPrimaryKey);
            if (!ModelState.IsValid)
            {
                return ViewEditModelingApproach(viewModel);
            }
            viewModel.UpdateModel(waterQualityManagementPlan);
            await _dbContext.SaveChangesAsync();

            var waterQualityManagementPlanBoundary = WaterQualityManagementPlanBoundaries.GetByWaterQualityManagementPlanID(_dbContext, waterQualityManagementPlan.WaterQualityManagementPlanID);
            if (waterQualityManagementPlanBoundary != null)
            {
                await ModelingEngineUtilities.QueueLGURefreshForArea(waterQualityManagementPlanBoundary.GeometryNative, null, _dbContext);
                await NereidUtilities.MarkWqmpDirty(waterQualityManagementPlan, _dbContext);
            }

            SetMessageForDisplay($"Modeling Approach successfully changed for {waterQualityManagementPlan.WaterQualityManagementPlanName}.");
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEditModelingApproach(EditModelingApproachViewModel viewModel)
        {
            var viewData = new EditModelingApproachViewData(WaterQualityManagementPlanModelingApproach.All);
            return RazorPartialView<EditModelingApproach, EditModelingApproachViewData, EditModelingApproachViewModel>(viewData, viewModel);
        }

        [HttpGet("{waterQualityManagementPlanPrimaryKey}")]
        [WaterQualityManagementPlanViewFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("waterQualityManagementPlanPrimaryKey")]
        public JsonResult GetModelResults([FromRoute] WaterQualityManagementPlanPrimaryKey waterQualityManagementPlanPrimaryKey)
        {
            var waterQualityManagementPlan = WaterQualityManagementPlans.GetByID(_dbContext, waterQualityManagementPlanPrimaryKey);
            var modeledPerformanceResultSimple = new ModeledPerformanceResultDto(_dbContext, waterQualityManagementPlan);
            return Json(modeledPerformanceResultSimple);
        }

        [HttpGet]
        [NeptuneAdminFeature]
        public ViewResult UploadWqmps()
        {
            var viewModel = new UploadWqmpsViewModel();
            var errorList = new List<string>();
            return ViewUploadWqmps(viewModel, errorList);
        }
        
        [HttpPost]
        [NeptuneAdminFeature]
        public async Task<IActionResult> UploadWqmps(UploadWqmpsViewModel viewModel)
        {
            //todo: bulk upload wqmps
            //var uploadedCSVFile = viewModel.UploadCSV;
            //var wqmps = WQMPCsvParserHelper.CSVUpload(uploadedCSVFile.InputStream, out var errorList);

            //if (errorList.Any())
            //{
            //    return ViewUploadWqmps(viewModel, errorList);
            //}

            //var wqmpsAdded = wqmps.Where(x => !ModelObjectHelpers.IsRealPrimaryKeyValue(x.PrimaryKey)).ToList();
            //var wqmpsUpdated = wqmps.Where(x => ModelObjectHelpers.IsRealPrimaryKeyValue(x.PrimaryKey)).ToList();

            //_dbContext.WaterQualityManagementPlans.AddRange(wqmpsAdded);
            //await _dbContext.SaveChangesAsync();

            //var message = $"Upload Successful: {wqmpsAdded.Count} records added, {wqmpsUpdated.Count} records updated!";
            //SetMessageForDisplay(message);
            return new RedirectResult(SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(_linkGenerator, x => x.Index()));
        }

        private ViewResult ViewUploadWqmps(UploadWqmpsViewModel viewModel, List<string> errorList)
        {
            var neptunePage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.UploadWQMPs);
            var viewData = new UploadWqmpsViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, errorList, neptunePage,
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(_linkGenerator, x => x.UploadWqmps()));
            return RazorView<UploadWqmps, UploadWqmpsViewData, UploadWqmpsViewModel>(viewData,
                viewModel);
        }
    }
}
