using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Neptune.Common;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Models;
using Neptune.WebMvc.Views.TrashHome;
using NetTopologySuite.Features;
using Index = Neptune.WebMvc.Views.TrashHome.Index;

namespace Neptune.WebMvc.Controllers
{
    [Route("Trash/Home/[action]", Name = "Trash_Home_[action]")]
    public class TrashHomeController : NeptuneBaseController<TrashHomeController>
    {
        public TrashHomeController(NeptuneDbContext dbContext, ILogger<TrashHomeController> logger, IOptions<WebConfiguration> webConfiguration, LinkGenerator linkGenerator) : base(dbContext, logger, linkGenerator, webConfiguration)
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

            var parcelLayerGeoJson = new LayerGeoJson("Parcels", parcels.ToGeoJsonFeatureCollectionForTrashMap(), "blue", 1, LayerInitialVisibility.Show) {EnablePopups = false};

            var boundingBox = StormwaterJurisdictions.GetBoundingBoxDtoByJurisdictionIDList(_dbContext, stormwaterJurisdictionIDsPersonCanView);

            var geoJsonForJurisdictions = GetGeoJsonForJurisdictions(stormwaterJurisdictionIDsPersonCanView);
            var ovtaBasedMapInitJson = new TrashModuleMapInitJson("ovtaBasedResultsMap", treatmentBMPLayerGeoJson, parcelLayerGeoJson, boundingBox, new List<LayerGeoJson>()) {LayerControlClass = "ovta-based-map-layer-control"};
            var areaBasedMapInitJson = new StormwaterMapInitJson("areaBasedResultsMap", boundingBox, new List<LayerGeoJson>()) { LayerControlClass = "area-based-map-layer-control" };
            var loadBasedMapInitJson= new StormwaterMapInitJson("loadBasedResultsMap", boundingBox, new List<LayerGeoJson>()) { LayerControlClass = "load-based-map-layer-control" };
            var neptunePage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.TrashHomePage);
            var neptunePageTrashModuleProgramOverview = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.TrashModuleProgramOverview);
            var showDropdown = stormwaterJurisdictionsPersonCanView.Count > 1;
            var currentUserIsAnonymousOrUnassigned = CurrentPerson.IsAnonymousOrUnassigned();
            var stormwaterJurisdictionCqlFilter = CurrentPerson.GetStormwaterJurisdictionCqlFilter(stormwaterJurisdictionIDsPersonCanView);
            var negativeStormwaterJurisdictionCqlFilter = CurrentPerson.GetNegativeStormwaterJurisdictionCqlFilter(stormwaterJurisdictionIDsPersonCanView);

            var viewDataForAngularClass = new IndexViewData.ViewDataForAngularClass(_linkGenerator, ovtaBasedMapInitJson, areaBasedMapInitJson,
                loadBasedMapInitJson,
                treatmentBmps, TrashCaptureStatusType.All, parcels, stormwaterJurisdictionCqlFilter, showDropdown,
                negativeStormwaterJurisdictionCqlFilter, geoJsonForJurisdictions, currentUserIsAnonymousOrUnassigned, _webConfiguration.MapServiceUrl);
            var viewData = new IndexViewData(HttpContext, _linkGenerator, CurrentPerson, _webConfiguration, neptunePage, stormwaterJurisdictionsPersonCanView, neptunePageTrashModuleProgramOverview, viewDataForAngularClass);

            return RazorView<Index, IndexViewData>(viewData);
        }

        private FeatureCollection GetGeoJsonForJurisdictions(List<int> stormwaterJurisdictionIDsPersonCanView)
        {
            var stormwaterJurisdictionGeometries = _dbContext.StormwaterJurisdictions.AsNoTracking()
                .Include(x => x.StormwaterJurisdictionGeometry)
                .Include(x => x.StateProvince)
                .Include(x => x.Organization)
                .Where(x => x.StormwaterJurisdictionGeometry != null &&
                            stormwaterJurisdictionIDsPersonCanView.Contains(x.StormwaterJurisdictionID))
                .ToList();
            var geoJsonForJurisdictions = StormwaterJurisdictionModelExtensions.ToGeoJsonFeatureCollection(stormwaterJurisdictionGeometries);
            return geoJsonForJurisdictions;
        }
    }
}
