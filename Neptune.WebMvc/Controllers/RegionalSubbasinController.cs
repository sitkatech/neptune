using Hangfire;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Security;
using Neptune.WebMvc.Views.RegionalSubbasin;
using Neptune.WebMvc.Views.Shared;
using Neptune.WebMvc.Views.Shared.HRUCharacteristics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Neptune.Common;
using Neptune.EFModels.Entities;
using Neptune.Jobs.Services;
using Neptune.Models.DataTransferObjects;
using Neptune.WebMvc.Common.MvcResults;
using Neptune.WebMvc.Services.Filters;
using NetTopologySuite.Features;
using Index = Neptune.WebMvc.Views.RegionalSubbasin.Index;
using IndexViewData = Neptune.WebMvc.Views.RegionalSubbasin.IndexViewData;
using Neptune.Jobs.Hangfire;

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
            var geoServerUrl = _webConfiguration.MapServiceUrl;
            var regionalSubbasinLayerName = Constants.MapServiceLayerNameRegionalSubbasin;

            var viewData = new IndexViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, new RegionalSubbasinMapInitJson("regionalSubbasinMap"), geoServerUrl, regionalSubbasinLayerName);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [HttpGet("{regionalSubbasinPrimaryKey}")]
        [NeptuneViewFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("regionalSubbasinPrimaryKey")]
        public JsonResult UpstreamCatchments([FromRoute] RegionalSubbasinPrimaryKey regionalSubbasinPrimaryKey)
        {
            var regionalSubbasin = regionalSubbasinPrimaryKey.EntityObject;
            return Json(new {regionalSubbasinIDs = vRegionalSubbasinUpstreams.ListUpstreamRegionalBasinIDs(_dbContext, regionalSubbasin) });
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

            var hruCharacteristics = vHRUCharacteristics.ListByRegionalSubbasinID(_dbContext, regionalSubbasin.RegionalSubbasinID).ToList();
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

            BackgroundJob.Enqueue<RegionalSubbasinRefreshJob>(x => x.RunJob());
            SetMessageForDisplay("Regional Subbasins refresh will run in the background.");
            return new ModalDialogFormJsonResult();
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
