using Hangfire;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Models;
using Neptune.WebMvc.Security;
using Neptune.WebMvc.Views.RegionalSubbasin;
using Neptune.WebMvc.Views.Shared;
using Neptune.WebMvc.Views.Shared.HRUCharacteristics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Neptune.Common.GeoSpatial;
using Neptune.Common.Services;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using Neptune.WebMvc.Common.MvcResults;
using Neptune.WebMvc.Services.Filters;
using NetTopologySuite.Features;
using Index = Neptune.WebMvc.Views.RegionalSubbasin.Index;
using IndexViewData = Neptune.WebMvc.Views.RegionalSubbasin.IndexViewData;

namespace Neptune.WebMvc.Controllers
{
    public class RegionalSubbasinController : NeptuneBaseController<RegionalSubbasinController>
    {
        private readonly OCGISService _ocgisService;

        public RegionalSubbasinController(NeptuneDbContext dbContext, ILogger<RegionalSubbasinController> logger, IOptions<WebConfiguration> webConfiguration, LinkGenerator linkGenerator, OCGISService ocgisService) : base(dbContext, logger, linkGenerator, webConfiguration)
        {
            _ocgisService = ocgisService;
        }

        [HttpGet]
        [NeptuneAdminFeature]
        public ViewResult Index()
        {
            var geoServerUrl = _webConfiguration.ParcelMapServiceUrl;
            var regionalSubbasinLayerName = _webConfiguration.MapServiceLayerNameRegionalSubbasin;

            var viewData = new IndexViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, new RegionalSubbasinMapInitJson("regionalSubbasinMap"), geoServerUrl, regionalSubbasinLayerName);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [HttpGet("{regionalSubbasinPrimaryKey}")]
        [NeptuneViewFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("regionalSubbasinPrimaryKey")]
        public JsonResult UpstreamCatchments([FromRoute] RegionalSubbasinPrimaryKey regionalSubbasinPrimaryKey)
        {
            var regionalSubbasin = regionalSubbasinPrimaryKey.EntityObject;
            return Json(new {regionalSubbasinIDs = regionalSubbasin.TraceUpstreamCatchmentsReturnIDList(_dbContext) });
        }

        [HttpGet("{treatmentBMPPrimaryKey}")]
        [NeptuneViewFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        public JsonResult UpstreamDelineation([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var dbGeometry = treatmentBMPPrimaryKey.EntityObject.GetCentralizedDelineationGeometry4326(_dbContext);
            var feature = new Feature(dbGeometry, new AttributesTable());
            return Json(feature);
        }

        
        [HttpGet("{regionalSubbasinPrimaryKey}")]
        [NeptuneAdminFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("regionalSubbasinPrimaryKey")]
        public ViewResult Detail([FromRoute] RegionalSubbasinPrimaryKey regionalSubbasinPrimaryKey)
        {
            var regionalSubbasin = regionalSubbasinPrimaryKey.EntityObject;
            var regionalSubbasinCatchmentGeometry4326 = regionalSubbasin.CatchmentGeometry4326;

            var feature = new Feature(regionalSubbasinCatchmentGeometry4326, new AttributesTable());
            var featureCollection = new FeatureCollection { feature };
            var layerGeoJson = new LayerGeoJson("Catchment Boundary", featureCollection,"#000000", 1, LayerInitialVisibility.Show, false );
            var stormwaterMapInitJson = new StormwaterMapInitJson("map", MapInitJson.DefaultZoomLevel, new List<LayerGeoJson>{layerGeoJson}, new BoundingBoxDto(regionalSubbasinCatchmentGeometry4326));

            var hruCharacteristics = regionalSubbasin.GetHRUCharacteristics(_dbContext).ToList();
            var hruCharacteristicsViewData = new HRUCharacteristicsViewData(hruCharacteristics);
            var ocSurveyDownstreamCatchment = regionalSubbasin.OCSurveyDownstreamCatchmentID != null ? RegionalSubbasins.GetByOCSurveyCatchmentID(_dbContext, regionalSubbasin.OCSurveyDownstreamCatchmentID.Value) : null;
            var viewData = new DetailViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, regionalSubbasin, hruCharacteristicsViewData, stormwaterMapInitJson, hruCharacteristics.Any(), ocSurveyDownstreamCatchment);
            return RazorView<Detail, DetailViewData>(viewData);
        }

        [HttpGet]
        [NeptuneAdminFeature]
        public PartialViewResult RefreshFromOCSurvey()
        {
            return ViewRefreshFromOCSurvey(new ConfirmDialogFormViewModel());
        }

        [HttpPost]
        [NeptuneAdminFeature]
        public ActionResult RefreshFromOCSurvey(ConfirmDialogFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewRefreshFromOCSurvey(viewModel);
            }

            BackgroundJob.Enqueue(() => RefreshFromOCSurveyImpl());
            SetMessageForDisplay("Regional Subbasins refresh will run in the background.");
            return new ModalDialogFormJsonResult();
        }

        /// <summary>
        /// this is the same code in RegionalSubbasinRefreshScheduledBackgroundJob.RunRefresh except it doesn't queue a LGURefresh
        /// for now duplicating it since don't have a better way of sharing it
        /// </summary>
        public async Task RefreshFromOCSurveyImpl()
        {
            _dbContext.Database.SetCommandTimeout(30000);
            await _dbContext.RegionalSubbasinStagings.ExecuteDeleteAsync();
            var regionalSubbasinFromEsris = await _ocgisService.RetrieveRegionalSubbasins();
            await SaveToStagingTable(regionalSubbasinFromEsris);
            await _dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.pDeleteLoadGeneratingUnitsPriorToTotalRefresh");
            await MergeAndProjectTo4326(_dbContext);
            await RefreshCentralizedDelineations(_dbContext);
            await _dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.pDelineationMarkThoseThatHaveDiscrepancies");
        }

        /// <summary>
        /// this is the same code in RegionalSubbasinRefreshScheduledBackgroundJob.SaveToStagingTable
        /// for now duplicating it since don't have a better way of sharing it
        /// </summary>
        private async Task SaveToStagingTable(IEnumerable<OCGISService.RegionalSubbasinFromEsri> regionalSubbasinFromEsris)
        {
            var regionalSubbasinStagings = regionalSubbasinFromEsris.Select(feature => new RegionalSubbasinStaging()
            {
                CatchmentGeometry = feature.Geometry,
                Watershed = feature.Watershed,
                OCSurveyCatchmentID = feature.OCSurveyCatchmentID,
                OCSurveyDownstreamCatchmentID = feature.OCSurveyDownstreamCatchmentID,
                DrainID = feature.DrainID
            })
                .ToList();
            await _dbContext.RegionalSubbasinStagings.AddRangeAsync(regionalSubbasinStagings);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// this is the same code in RegionalSubbasinRefreshScheduledBackgroundJob.RefreshCentralizedDelineations
        /// for now duplicating it since don't have a better way of sharing it
        /// </summary>
        private static async Task RefreshCentralizedDelineations(NeptuneDbContext dbContext)
        {
            foreach (var delineation in dbContext.Delineations.Where(x => x.DelineationTypeID == DelineationType.Centralized.DelineationTypeID))
            {
                var centralizedDelineationGeometry2771 = delineation.TreatmentBMP.GetCentralizedDelineationGeometry2771(dbContext);
                var centralizedDelineationGeometry4326 = delineation.TreatmentBMP.GetCentralizedDelineationGeometry4326(dbContext);

                delineation.DelineationGeometry = centralizedDelineationGeometry2771;
                delineation.DelineationGeometry4326 = centralizedDelineationGeometry4326;
                delineation.DateLastModified = DateTime.Now;
            }

            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// this is the same code in RegionalSubbasinRefreshScheduledBackgroundJob.MergeAndProjectTo4326
        /// for now duplicating it since don't have a better way of sharing it
        /// </summary>
        private static async Task MergeAndProjectTo4326(NeptuneDbContext dbContext)
        {
            await dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.pUpdateRegionalSubbasinLiveFromStaging");
            await dbContext.RegionalSubbasins.LoadAsync();
            await dbContext.Watersheds.LoadAsync();
            foreach (var regionalSubbasin in dbContext.RegionalSubbasins)
            {
                regionalSubbasin.CatchmentGeometry4326 = regionalSubbasin.CatchmentGeometry.ProjectTo4326();
            }

            // Watershed table is made up from the dissolves/ aggregation of the Regional Subbasins feature layer, so we need to update it when Regional Subbasins are updated
            foreach (var watershed in dbContext.Watersheds)
            {
                watershed.WatershedGeometry4326 = watershed.WatershedGeometry.ProjectTo4326();
            }
            await dbContext.SaveChangesAsync();
            await dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.pTreatmentBMPUpdateWatershed");
            await dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.pUpdateRegionalSubbasinIntersectionCache");
        }

        private PartialViewResult ViewRefreshFromOCSurvey(ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = "Are you sure you want to refresh the Regional Subbasins layer from OC Survey?<br /><br />This can take a little while to run.";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [NeptuneAdminFeature]
        public ViewResult Grid()
        {
            var neptunePage = NeptunePages.GetNeptunePageByPageType(_dbContext,NeptunePageType.RegionalSubbasins);
            var viewData = new GridViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, neptunePage);
            return RazorView<Grid, GridViewData>(viewData);
        }

        [HttpGet]
        [NeptuneAdminFeature]
        public GridJsonNetJObjectResult<RegionalSubbasin> RegionalSubbasinGridJsonData()
        {
            var gridSpec = new RegionalSubbasinGridSpec(_linkGenerator);
            var regionalSubbasins = _dbContext.RegionalSubbasins.ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<RegionalSubbasin>(regionalSubbasins, gridSpec);
            return gridJsonNetJObjectResult;
        }
    }
}
