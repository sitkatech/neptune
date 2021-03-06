﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Security.Shared;
using Neptune.Web.Views.Parcel;

namespace Neptune.Web.Controllers
{
    public class ParcelController : NeptuneBaseController
    {
        [HttpGet]
        [NeptuneViewFeature]
        public ActionResult Index()
        {
            var neptunePageHome = NeptunePage.GetNeptunePageByPageType(NeptunePageType.ParcelList);
            var findParcelByAddressUrl = SitkaRoute<ParcelController>.BuildUrlFromExpression(x => x.FindByAddress(null));
            var findParcelByAPNUrl = SitkaRoute<ParcelController>.BuildUrlFromExpression(x => x.FindByAPN(null));
            var parcelMapServiceUrl = NeptuneWebConfiguration.ParcelMapServiceUrl;
            var parcelSummaryForMapUrl = SitkaRoute<ParcelController>.BuildUrlFromExpression(x => x.SummaryForMap(null));
            var mapInitJson = GetMapInitJson();

            var viewData = new IndexViewData(CurrentPerson,
                neptunePageHome,
                neptunePageHome.NeptunePageContentHtmlString,
                mapInitJson,
                findParcelByAddressUrl,
                findParcelByAPNUrl,
                parcelMapServiceUrl,
                parcelSummaryForMapUrl);
            return RazorView<Index, IndexViewData>(viewData);
        }

        private static MapInitJson GetMapInitJson()
        {
            const string mapDivID = "parcelSearchMap";
            var layers = new List<LayerGeoJson>();
            layers.AddRange(MapInitJsonHelpers.GetJurisdictionMapLayers());
            var boundingBox = BoundingBox.MakeNewDefaultBoundingBox();
            var mapInitJson = new MapInitJson(mapDivID, 10, layers, boundingBox);
            return mapInitJson;
        }

        [NeptuneViewFeature]
        public JsonResult FindByAddress(string term)
        {
            var searchString = term.Trim();
            var allParcelsMatchingSearchString =
                HttpRequestStorage.DatabaseEntities.Parcels.Where(
                    x => x.ParcelGeometry != null && (x.ParcelAddress + ", "+ x.ParcelZipCode).Contains(searchString)).OrderBy(x => x.ParcelAddress + ", " + x.ParcelZipCode).ThenBy(x => x.ParcelNumber).Take(10).ToList();

            var listItems = allParcelsMatchingSearchString.Select(pfr =>
            {
                var listItem = new ListItem(pfr.GetParcelAddress(), pfr.ParcelNumber);
                return listItem;
            }).ToList();
            return Json(listItems, JsonRequestBehavior.AllowGet);
        }

        [NeptuneViewFeature]
        public JsonResult FindSimpleByAddress(string term)
        {
            var searchString = term.Trim();
            var listItems = HttpRequestStorage.DatabaseEntities.Parcels
                .Where(x => x.ParcelGeometry != null && x.ParcelAddress.Contains(searchString))
                .OrderBy(x => x.ParcelAddress + ", " + x.ParcelZipCode)
                .ThenBy(x => x.ParcelNumber)
                .Take(10)
                .ToList()
                .Select(x => new ParcelSimple(x))
                .ToList();
            return Json(listItems, JsonRequestBehavior.AllowGet);
        }

        [NeptuneViewFeature]
        // ReSharper disable once InconsistentNaming
        public JsonResult FindByAPN(string term)
        {
            var searchString = term.Trim();
            var allParcelsMatchingSearchString =
                HttpRequestStorage.DatabaseEntities.Parcels.Where(x => x.ParcelGeometry != null && x.ParcelNumber.Contains(searchString)).OrderBy(x => x.ParcelAddress + ", " + x.ParcelZipCode).ThenBy(x => x.ParcelNumber).Take(10).ToList();

            var listItems = allParcelsMatchingSearchString.Select(pfr =>
            {
                var listItem = new ListItem(pfr.ParcelNumber, pfr.ParcelNumber);
                return listItem;
            }).ToList();
            return Json(listItems, JsonRequestBehavior.AllowGet);
        }

        [NeptuneViewFeature]        
        // ReSharper disable once InconsistentNaming
        public JsonResult FindSimpleByAPN(string term)
        {
            var searchString = term.Trim();
            var listItems = HttpRequestStorage.DatabaseEntities.Parcels
                .Where(x => x.ParcelGeometry != null && x.ParcelNumber.Contains(searchString))
                .OrderBy(x => x.ParcelAddress + ", " + x.ParcelZipCode)
                .ThenBy(x => x.ParcelNumber)
                .Take(10)
                .ToList()
                .Select(x => new ParcelSimple(x))
                .ToList();
            return Json(listItems, JsonRequestBehavior.AllowGet);
        }

        [NeptuneViewFeature]
        public PartialViewResult SummaryForMap(string parcelNumber)
        {
            var parcel = HttpRequestStorage.DatabaseEntities.Parcels.GetParcelByParcelNumber(parcelNumber);
            var viewData = new SummaryForMapViewData(CurrentPerson, parcel);
            return RazorPartialView<SummaryForMap, SummaryForMapViewData>(viewData);
        }
    }
}
