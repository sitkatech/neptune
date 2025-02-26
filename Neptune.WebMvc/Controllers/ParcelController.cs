using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Neptune.Common.GeoSpatial;
using Neptune.EFModels.Entities;
using Neptune.Jobs.Services;
using Neptune.Models.DataTransferObjects;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.Models;
using Neptune.WebMvc.Common.MvcResults;
using Neptune.WebMvc.Models;
using Neptune.WebMvc.Security;
using Neptune.WebMvc.Services.Filters;
using Neptune.WebMvc.Views.Parcel;
using Neptune.WebMvc.Views.Shared;
using NetTopologySuite.Features;

namespace Neptune.WebMvc.Controllers
{
    public class ParcelController : NeptuneBaseController<ParcelController>
    {
        public ParcelController(NeptuneDbContext dbContext, ILogger<ParcelController> logger, IOptions<WebConfiguration> webConfiguration, LinkGenerator linkGenerator) : base(dbContext, logger, linkGenerator, webConfiguration)
        {
        }

        [HttpGet]
        [NeptuneViewFeature]
        public ActionResult Index()
        {
            var neptunePageHome = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.ParcelList);
            var findParcelByAddressUrl = SitkaRoute<ParcelController>.BuildUrlFromExpression(_linkGenerator, x => x.FindByAddress(null));
            var findParcelByAPNUrl = SitkaRoute<ParcelController>.BuildUrlFromExpression(_linkGenerator, x => x.FindByAPN(null));
            var parcelMapServiceUrl = _webConfiguration.MapServiceUrl;
            var parcelSummaryForMapUrl = SitkaRoute<ParcelController>.BuildUrlFromExpression(_linkGenerator, x => x.SummaryForMap(ModelObjectHelpers.NotYetAssignedID)).Replace("/-1", "");
            var mapInitJson = GetMapInitJson();

            var viewData = new IndexViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson,
                neptunePageHome,
                neptunePageHome.NeptunePageContent,
                mapInitJson,
                findParcelByAddressUrl,
                findParcelByAPNUrl,
                parcelMapServiceUrl,
                parcelSummaryForMapUrl);
            return RazorView<Views.Parcel.Index, IndexViewData>(viewData);
        }

        private MapInitJson GetMapInitJson()
        {
            const string mapDivID = "parcelSearchMap";
            var layers = new List<LayerGeoJson>();
            layers.AddRange(MapInitJsonHelpers.GetJurisdictionMapLayers(_dbContext));
            var boundingBox = new BoundingBoxDto();
            var mapInitJson = new MapInitJson(mapDivID, 10, layers, boundingBox);
            return mapInitJson;
        }

        [HttpGet]
        [NeptuneViewFeature]
        public JsonResult FindByAddress([FromQuery] string term)
        {
            var searchString = term.Trim();
            var allParcelsMatchingSearchString =
                _dbContext.ParcelGeometries.AsNoTracking().Include(x => x.Parcel).Where(
                    x => (x.Parcel.ParcelAddress + ", "+ x.Parcel.ParcelZipCode).Contains(searchString)).OrderBy(x => x.Parcel.ParcelAddress + ", " + x.Parcel.ParcelZipCode).ThenBy(x => x.Parcel.ParcelNumber).Take(10).Select(x => x.Parcel).ToList();

            var listItems = allParcelsMatchingSearchString.Select(pfr =>
            {
                var listItem = new SelectListItem(pfr.GetParcelAddress(), pfr.ParcelNumber);
                return listItem;
            }).ToList();
            return Json(listItems);
        }

        [HttpGet]
        [NeptuneViewFeature]
        public JsonResult FindSimpleByAddress([FromQuery] string term)
        {
            var searchString = term.Trim();
            var listItems = _dbContext.ParcelGeometries.AsNoTracking().Include(x => x.Parcel)
                .Where(x => x.Parcel.ParcelAddress.Contains(searchString))
                .OrderBy(x => x.Parcel.ParcelAddress + ", " + x.Parcel.ParcelZipCode)
                .ThenBy(x => x.Parcel.ParcelNumber)
                .Take(10).Select(x => x.Parcel)
                .ToList()
                .Select(x => x.AsSimpleDto())
                .ToList();
            return Json(listItems);
        }

        [HttpGet]
        [NeptuneViewFeature]
        public JsonResult FindByAPN([FromQuery] string term)
        {
            var searchString = term.Trim();
            var allParcelsMatchingSearchString =
                _dbContext.ParcelGeometries.AsNoTracking().Include(x => x.Parcel).Where(x => x.Parcel.ParcelNumber.Contains(searchString)).OrderBy(x => x.Parcel.ParcelAddress + ", " + x.Parcel.ParcelZipCode).ThenBy(x => x.Parcel.ParcelNumber).Take(10).Select(x => x.Parcel).ToList();

            var listItems = allParcelsMatchingSearchString.Select(pfr =>
            {
                var listItem = new SelectListItem(pfr.ParcelNumber, pfr.ParcelNumber);
                return listItem;
            }).ToList();
            return Json(listItems);
        }

        [HttpGet]
        [NeptuneViewFeature]
        public JsonResult FindSimpleByAPN([FromQuery] string term)
        {
            var searchString = term.Trim();
            var listItems = _dbContext.ParcelGeometries.AsNoTracking().Include(x => x.Parcel)
                .Where(x => x.Parcel.ParcelNumber.Contains(searchString))
                .OrderBy(x => x.Parcel.ParcelAddress + ", " + x.Parcel.ParcelZipCode)
                .ThenBy(x => x.Parcel.ParcelNumber)
                .Take(10).Select(x => x.Parcel)
                .ToList()
                .Select(x => x.AsSimpleDto())
                .ToList();
            return Json(listItems);
        }

        [HttpGet("{parcelPrimaryKey}")]
        [NeptuneViewFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("parcelPrimaryKey")]
        public PartialViewResult SummaryForMap([FromRoute] ParcelPrimaryKey parcelPrimaryKey)
        {
            var parcel = parcelPrimaryKey.EntityObject;
            var viewData = new SummaryForMapViewData(CurrentPerson, parcel);
            return RazorPartialView<SummaryForMap, SummaryForMapViewData>(viewData);
        }

        [HttpGet("{parcelPrimaryKey}")]
        [NeptuneViewAndRequiresJurisdictionsFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("parcelPrimaryKey")]
        public PartialViewResult TrashMapAssetPanel([FromRoute] ParcelPrimaryKey parcelPrimaryKey)
        {
            var parcel = parcelPrimaryKey.EntityObject;
            var viewData = new TrashMapAssetPanelViewData(parcel);
            return RazorPartialView<TrashMapAssetPanel, TrashMapAssetPanelViewData>(viewData);
        }

        [HttpGet]
        [NeptuneViewAndRequiresJurisdictionsFeature]
        public ContentResult Union()
        {
            return Content("");
        }

        [HttpPost]
        [NeptuneViewAndRequiresJurisdictionsFeature]
        public FeatureCollection Union(UnionOfParcelsViewModel viewModel)
        {
            var unionOfParcels = ParcelGeometries.UnionAggregateByParcelIDs(_dbContext, viewModel.ParcelIDs);
            var featureCollection = unionOfParcels.MultiPolygonToFeatureCollection();
            return featureCollection;
        }

        [HttpGet]
        [NeptuneAdminFeature]
        public PartialViewResult RefreshParcelsFromOCSurvey()
        {
            return ViewRefreshParcelsFromOCSurvey(new ConfirmDialogFormViewModel());
        }

        [HttpPost]
        [NeptuneAdminFeature]
        public ActionResult RefreshParcelsFromOCSurvey(ConfirmDialogFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewRefreshParcelsFromOCSurvey(viewModel);
            }

            BackgroundJob.Enqueue<OCGISService>(x => x.RefreshParcels());
            SetMessageForDisplay("Parcels refresh will run in the background.");
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewRefreshParcelsFromOCSurvey(ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = "Are you sure you want to refresh the Parcels layer from OC Survey?<br /><br />This can take a little while to run.";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }
    }
}
