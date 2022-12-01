using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Areas.Trash.Views.Home;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security.Shared;

namespace Neptune.Web.Areas.Trash.Controllers
{
    public class HomeController : NeptuneBaseController
    {
        [HttpGet]
        [AnonymousUnclassifiedFeature]
        public ViewResult Index()
        {
            var stormwaterJurisdictionsPersonCanView = CurrentPerson.GetStormwaterJurisdictionsPersonCanView().ToList();

            if (!stormwaterJurisdictionsPersonCanView.Any())
            {
                throw new SitkaRecordNotAuthorizedException(
                    "You are not assigned to any Jurisdictions. Please log out and log in as a different user or request additional permissions");
            }

            var treatmentBmps = CurrentPerson.GetTreatmentBmpsPersonCanView(stormwaterJurisdictionsPersonCanView);
            var treatmentBMPLayerGeoJson = new LayerGeoJson("Treatment BMPs", treatmentBmps.ToGeoJsonFeatureCollectionForTrashMap(), "blue", 1, LayerInitialVisibility.Show) {EnablePopups = false};

            var parcels = HttpRequestStorage.DatabaseEntities.Parcels.Include(x => x.WaterQualityManagementPlanParcels).Include(x => x.WaterQualityManagementPlanParcels.Select(y => y.WaterQualityManagementPlan)).Where(x => x.WaterQualityManagementPlanParcels.Any()).ToList();

            var parcelLayerGeoJson = new LayerGeoJson("Parcels", parcels.ToGeoJsonFeatureCollectionForTrashMap(), "blue", 1, LayerInitialVisibility.Show) {EnablePopups = false};

            var boundingBox = BoundingBox.GetBoundingBox(stormwaterJurisdictionsPersonCanView);
            var geoJsonForJurisdictions = StormwaterJurisdiction.ToGeoJsonFeatureCollection(stormwaterJurisdictionsPersonCanView);
            var jurisdictionLayersGeoJson = new List<LayerGeoJson> { new LayerGeoJson(MapInitJsonHelpers.CountyCityLayerName, geoJsonForJurisdictions, "#FF6C2D", 0m, LayerInitialVisibility.Hide) }.ToList(); 
            var ovtaBasedMapInitJson = new TrashModuleMapInitJson("ovtaBasedResultsMap", treatmentBMPLayerGeoJson, parcelLayerGeoJson, boundingBox, jurisdictionLayersGeoJson) {LayerControlClass = "ovta-based-map-layer-control"};
            var areaBasedMapInitJson = new StormwaterMapInitJson("areaBasedResultsMap", boundingBox, jurisdictionLayersGeoJson) { LayerControlClass = "area-based-map-layer-control" };
            var loadBasedMapInitJson= new StormwaterMapInitJson("loadBasedResultsMap", boundingBox, jurisdictionLayersGeoJson) { LayerControlClass = "load-based-map-layer-control" };
            var neptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.TrashHomePage);
            var viewData = new IndexViewData(CurrentPerson, neptunePage, ovtaBasedMapInitJson, areaBasedMapInitJson,
                loadBasedMapInitJson, treatmentBmps, TrashCaptureStatusType.All, parcels, stormwaterJurisdictionsPersonCanView, geoJsonForJurisdictions);

            return RazorView<Index, IndexViewData>(viewData);
        }
    }
}
