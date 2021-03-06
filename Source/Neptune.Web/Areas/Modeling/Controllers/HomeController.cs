﻿
using System.Linq;
using System.Web.Mvc;
using Neptune.Web.Areas.Modeling.Views.Home;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Security.Shared;
using Neptune.Web.Views.Map;
using Neptune.Web.Views.Shared.JurisdictionControls;

namespace Neptune.Web.Areas.Modeling.Controllers
{
    public class HomeController : NeptuneBaseController
    {
        [HttpGet]
        [AnonymousUnclassifiedFeature]
        public ViewResult Index()
        {
            var layerGeoJsons = HttpRequestStorage.DatabaseEntities.StormwaterJurisdictions.GetBoundaryLayerGeoJson(true)
                .Where(x => x.LayerInitialVisibility == LayerInitialVisibility.Show)
                .ToList();

            var projectLocationsMapInitJson = new JurisdictionsMapInitJson("JurisdictionsMap")
            {
                AllowFullScreen = false,
                Layers = layerGeoJsons
            };
            var projectLocationsMapViewData = new JurisdictionsMapViewData(projectLocationsMapInitJson.MapDivID);

            var neptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.ModelingHomePage);
            IndexViewData viewData = new IndexViewData(CurrentPerson, neptunePage, projectLocationsMapViewData, projectLocationsMapInitJson);
            return RazorView<Index, IndexViewData>(viewData);
        }   
    }
}
