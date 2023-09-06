using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.RegionalSubbasin;
using Neptune.Web.Views.Shared;
using Neptune.Web.Views.Shared.HRUCharacteristics;
using Microsoft.AspNetCore.Mvc;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using Neptune.Web.Common.MvcResults;
using NetTopologySuite.Features;
using Index = Neptune.Web.Views.RegionalSubbasin.Index;
using IndexViewData = Neptune.Web.Views.RegionalSubbasin.IndexViewData;

namespace Neptune.Web.Controllers
{
    public class RegionalSubbasinController : NeptuneBaseController<RegionalSubbasinController>
    {
        public RegionalSubbasinController(NeptuneDbContext dbContext, ILogger<RegionalSubbasinController> logger, LinkGenerator linkGenerator) : base(dbContext, logger, linkGenerator)
        {
        }

        [HttpGet]
        [NeptuneAdminFeature]
        public ViewResult Index()
        {
            var geoServerUrl = "";//todo: NeptuneWebConfiguration.ParcelMapServiceUrl;
            var regionalSubbasinLayerName = ""; //todo: NeptuneWebConfiguration.RegionalSubbasinLayerName;

            var viewData = new IndexViewData(HttpContext, _linkGenerator, CurrentPerson, new RegionalSubbasinMapInitJson("regionalSubbasinMap"), geoServerUrl, regionalSubbasinLayerName);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [HttpGet]
        [NeptuneViewFeature]
        public JsonResult UpstreamCatchments(RegionalSubbasinPrimaryKey regionalSubbasinPrimaryKey)
        {
            var regionalSubbasin = regionalSubbasinPrimaryKey.EntityObject;
            return Json(new {regionalSubbasinIDs = regionalSubbasin.TraceUpstreamCatchmentsReturnIDList(_dbContext) });
        }

        [HttpGet]
        [NeptuneViewFeature]
        public JsonResult UpstreamDelineation(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var dbGeometry = treatmentBMPPrimaryKey.EntityObject.GetCentralizedDelineationGeometry4326(_dbContext);
            var feature = new Feature(dbGeometry, new AttributesTable());
            return Json(feature);
        }

        
        [HttpGet]
        [NeptuneAdminFeature]
        public ViewResult Detail(RegionalSubbasinPrimaryKey regionalSubbasinPrimaryKey)
        {
            var regionalSubbasin = regionalSubbasinPrimaryKey.EntityObject;
            var regionalSubbasinCatchmentGeometry4326 = regionalSubbasin.CatchmentGeometry4326;

            var feature = new Feature(regionalSubbasinCatchmentGeometry4326, new AttributesTable());
            var featureCollection = new FeatureCollection { feature };
            var layerGeoJson = new LayerGeoJson("Catchment Boundary", featureCollection,"#000000", 1, LayerInitialVisibility.Show, false );
            var stormwaterMapInitJson = new StormwaterMapInitJson("map", MapInitJson.DefaultZoomLevel, new List<LayerGeoJson>{layerGeoJson}, new BoundingBoxDto(regionalSubbasinCatchmentGeometry4326));


            var hruCharacteristics = regionalSubbasin.GetHRUCharacteristics(_dbContext).ToList();
            var hruCharacteristicsViewData = new HRUCharacteristicsViewData(regionalSubbasin, hruCharacteristics);
            var viewData = new DetailViewData(HttpContext, _linkGenerator, CurrentPerson, regionalSubbasin, hruCharacteristicsViewData, stormwaterMapInitJson, hruCharacteristics.Any());
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

            // todo: BackgroundJob.Enqueue(() => ScheduledBackgroundJobLaunchHelper.RunRegionalSubbasinRefreshBackgroundJob(CurrentPerson.PersonID, false));
            SetMessageForDisplay("Regional Subbasins refresh will run in the background.");
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewRefreshFromOCSurvey(ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = "Are you sure you want to refresh the Regional Subbasins layer from OC Survey?<br /><br />This can take a little while to run.";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [NeptuneAdminFeature]
        public ViewResult Grid()
        {
            var neptunePage = NeptunePages.GetNeptunePageByPageType(_dbContext,NeptunePageType.RegionalSubbasins);
            var viewData = new GridViewData(HttpContext, _linkGenerator, CurrentPerson, neptunePage);
            return RazorView<Grid, GridViewData>(viewData);
        }

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
