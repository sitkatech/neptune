using GeoJSON.Net.Feature;
using LtInfo.Common.DbSpatial;
using LtInfo.Common.GeoJson;
using Neptune.Web.Areas.DroolTool.Security;
using Neptune.Web.Areas.DroolTool.Views.Home;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Neptune.Web.Areas.DroolTool.Controllers
{
    public class HomeController : NeptuneBaseController
    {
        [HttpGet]
        [DroolToolViewFeature]
        public ViewResult Index()
        {
            //var visitedCookie = Request.Cookies["visitedDroolTool"];
            //var firstTimeVisit = visitedCookie == null;

            //if (firstTimeVisit)
            //{
            //    HttpCookie myCookie = new HttpCookie("visitedDroolTool");
            //    myCookie.Values.Add("firstVisitDate", DateTime.Now.ToShortDateString());
            //    myCookie.Expires = DateTime.Now.AddMonths(12);
            //    Response.Cookies.Add(myCookie);
            //}

            var droolToolWatersheds = HttpRequestStorage.DatabaseEntities.DroolToolWatersheds.Select(x => x.DroolToolWatershedGeometry4326);
            var watershedCoverage = droolToolWatersheds.ToList().UnionListGeometries();
            var feature = DbGeometryToGeoJsonHelper.FromDbGeometryWithNoReproject(watershedCoverage);
            var watershedCoverageLayerGeoJson = new LayerGeoJson("WatershedCoverage", new FeatureCollection(new List<Feature> {feature}), "#ffffff", 0,
                LayerInitialVisibility.Hide);
            var stormwaterMapInitJson = new DroolToolMapInitJson("droolToolMap", MapInitJson.DefaultZoomLevel, MapInitJsonHelpers.GetJurisdictionMapLayers().ToList(),
                new BoundingBox(droolToolWatersheds))
            {
                WatershedCoverage = watershedCoverageLayerGeoJson
            };

            var neptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.DroolToolHomePage);
            var viewData = new IndexViewData(CurrentPerson, neptunePage, false, stormwaterMapInitJson);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [HttpGet]
        [DroolToolViewFeature]
        public ViewResult About()
        {
            var neptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.DroolToolAboutPage);
            var viewData = new AboutViewData(CurrentPerson, neptunePage);
            return RazorView<About, AboutViewData>(viewData);
        }
    }

    public class DroolToolMapInitJson : StormwaterMapInitJson
    {
        public LayerGeoJson WatershedCoverage { get; set; }

        public DroolToolMapInitJson(string mapDivID, int zoomLevel, List<LayerGeoJson> layers, BoundingBox boundingBox) : base(mapDivID, zoomLevel, layers, boundingBox)
        {
        }
    }
}
