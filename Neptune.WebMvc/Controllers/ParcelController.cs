﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Neptune.Common.GeoSpatial;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Models;
using Neptune.WebMvc.Security;
using Neptune.WebMvc.Services.Filters;
using Neptune.WebMvc.Views.Parcel;
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
            var parcelSummaryForMapUrl = SitkaRoute<ParcelController>.BuildUrlFromExpression(_linkGenerator, x => x.SummaryForMap(null));
            var parcelLayerUploadUrl =
                SitkaRoute<ParcelLayerUploadController>.BuildUrlFromExpression(_linkGenerator, x => x.UpdateParcelLayerGeometry(null));
            var mapInitJson = GetMapInitJson();

            var viewData = new IndexViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson,
                neptunePageHome,
                neptunePageHome.NeptunePageContent,
                mapInitJson,
                findParcelByAddressUrl,
                findParcelByAPNUrl,
                parcelMapServiceUrl,
                parcelSummaryForMapUrl,
                parcelLayerUploadUrl);
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

        [HttpGet("{term}")]
        [NeptuneViewFeature]
        public JsonResult FindByAddress(string term)
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

        [HttpGet("{term}")]
        [NeptuneViewFeature]
        public JsonResult FindByAPN(string term)
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

        [HttpGet("{parcelNumber}")]
        [NeptuneViewFeature]
        public PartialViewResult SummaryForMap(string parcelNumber)
        {
            var parcel = Parcels.GetParcelByParcelNumber(_dbContext, parcelNumber);
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
    }
}
