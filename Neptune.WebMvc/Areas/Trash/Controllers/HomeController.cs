using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Neptune.Common;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Areas.Trash.Views.Home;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Models;

namespace Neptune.WebMvc.Areas.Trash.Controllers
{
    [Area("Trash")]
    [Route("[area]/[controller]/[action]", Name = "[area]_[controller]_[action]")]
    public class HomeController : NeptuneBaseController<HomeController>
    {
        public HomeController(NeptuneDbContext dbContext, ILogger<HomeController> logger, IOptions<WebConfiguration> webConfiguration, LinkGenerator linkGenerator) : base(dbContext, logger, linkGenerator, webConfiguration)
        {
        }

        [HttpGet]
        public ViewResult Index()
        {
            var stormwaterJurisdictionsPersonCanView = StormwaterJurisdictions.ListViewableByPersonForBMPs(_dbContext, CurrentPerson);

            if (!stormwaterJurisdictionsPersonCanView.Any())
            {
                throw new SitkaRecordNotAuthorizedException(
                    "You are not assigned to any Jurisdictions. Please log out and log in as a different user or request additional permissions");
            }

            var stormwaterJurisdictionIDsPersonCanView = stormwaterJurisdictionsPersonCanView.Select(x => x.StormwaterJurisdictionID).ToList();
            var treatmentBmps = TreatmentBMPs.ListByStormwaterJurisdictionIDList(_dbContext, stormwaterJurisdictionIDsPersonCanView.ToList());
            var treatmentBMPLayerGeoJson = new LayerGeoJson("Treatment BMPs", treatmentBmps.ToGeoJsonFeatureCollectionForTrashMap(_linkGenerator), "blue", 1, LayerInitialVisibility.Show) {EnablePopups = false};

            var parcels = _dbContext.Parcels
                .Include(x => x.ParcelGeometry)
                .Include(x => x.WaterQualityManagementPlanParcels).ThenInclude(x => x.WaterQualityManagementPlan)
                .AsNoTracking()
                .Where(x => x.WaterQualityManagementPlanParcels.Any()).ToList();

            var parcelLayerGeoJson = new LayerGeoJson("Parcels", parcels.ToGeoJsonFeatureCollectionForTrashMap(_linkGenerator), "blue", 1, LayerInitialVisibility.Show) {EnablePopups = false};

            var boundingBox = StormwaterJurisdictions.GetBoundingBoxDtoByJurisdictionIDList(_dbContext, stormwaterJurisdictionIDsPersonCanView);

            var stormwaterJurisdictionGeometries = _dbContext.StormwaterJurisdictions.AsNoTracking()
                .Include(x => x.StormwaterJurisdictionGeometry)
                .Include(x => x.StateProvince)
                .Include(x => x.Organization)
                .Where(x => x.StormwaterJurisdictionGeometry != null && stormwaterJurisdictionIDsPersonCanView.Contains(x.StormwaterJurisdictionID))
                .ToList();
            var geoJsonForJurisdictions = StormwaterJurisdictionModelExtensions.ToGeoJsonFeatureCollection(stormwaterJurisdictionGeometries);
            var jurisdictionLayersGeoJson = MapInitJsonHelpers.GetJurisdictionMapLayers(_dbContext);
            var ovtaBasedMapInitJson = new TrashModuleMapInitJson("ovtaBasedResultsMap", treatmentBMPLayerGeoJson, parcelLayerGeoJson, boundingBox, jurisdictionLayersGeoJson) {LayerControlClass = "ovta-based-map-layer-control"};
            var areaBasedMapInitJson = new StormwaterMapInitJson("areaBasedResultsMap", boundingBox, jurisdictionLayersGeoJson) { LayerControlClass = "area-based-map-layer-control" };
            var loadBasedMapInitJson= new StormwaterMapInitJson("loadBasedResultsMap", boundingBox, jurisdictionLayersGeoJson) { LayerControlClass = "load-based-map-layer-control" };
            var neptunePage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.TrashHomePage);
            var neptunePageTrashModuleProgramOverview = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.TrashModuleProgramOverview);
            var viewData = new IndexViewData(HttpContext, _linkGenerator, CurrentPerson, _webConfiguration, neptunePage, ovtaBasedMapInitJson, areaBasedMapInitJson,
                loadBasedMapInitJson, treatmentBmps, TrashCaptureStatusType.All, parcels, stormwaterJurisdictionsPersonCanView, geoJsonForJurisdictions, neptunePageTrashModuleProgramOverview);

            return RazorView<Views.Home.Index, IndexViewData>(viewData);
        }
    }
}
