using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using LtInfo.Common.MvcResults;
using Neptune.Web.Controllers;
using Neptune.Web.Areas.Trash.Views.Home;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.Shared;

namespace Neptune.Web.Areas.Trash.Controllers
{
    public class HomeController : NeptuneBaseController
    {
        [HttpGet]
        [NeptuneViewAndRequiresJurisdictionsFeature]
        public ViewResult Index()
        {
            var stormwaterJurisdictionsPersonCanView = CurrentPerson.GetStormwaterJurisdictionsPersonCanView().ToList();
            var stormwaterJurisdictionIDsPersonCanView = stormwaterJurisdictionsPersonCanView.Select(x => x.StormwaterJurisdictionID);
            var treatmentBmps = HttpRequestStorage.DatabaseEntities.TreatmentBMPs.Where(x => stormwaterJurisdictionIDsPersonCanView.Contains(x.StormwaterJurisdictionID)).ToList();

            var treatmentBMPLayerGeoJson = new LayerGeoJson("Treatment BMPs", treatmentBmps.ToGeoJsonFeatureCollectionForTrashMap(), "blue", 1, LayerInitialVisibility.Show) {EnablePopups = false};

            var parcels = HttpRequestStorage.DatabaseEntities.Parcels.Include(x => x.WaterQualityManagementPlanParcels).Where(x => x.WaterQualityManagementPlanParcels.Any()).ToList();

            var parcelLayerGeoJson = new LayerGeoJson("Parcels", parcels.ToGeoJsonFeatureCollectionForTrashMap(), "blue", 1, LayerInitialVisibility.Show) {EnablePopups = false};

            var boundingBox = BoundingBox.GetBoundingBox(stormwaterJurisdictionsPersonCanView);
            var ovtaBasedMapInitJson = new TrashModuleMapInitJson("ovtaBasedResultsMap", treatmentBMPLayerGeoJson, parcelLayerGeoJson, boundingBox)
                {LayerControlClass = "ovta-based-map-layer-control"};
            var jurisdictions = HttpRequestStorage.DatabaseEntities.StormwaterJurisdictionGeometries
                .Select(x => x.StormwaterJurisdiction).ToList();
            var geoJsonForJurisdictions = StormwaterJurisdiction.ToGeoJsonFeatureCollection(jurisdictions);
            var layerGeoJsons = new List<LayerGeoJson>() { new LayerGeoJson(MapInitJsonHelpers.CountyCityLayerName, geoJsonForJurisdictions, "#FF6C2D", 0m, LayerInitialVisibility.Hide)}.ToList();
            var areaBasedMapInitJson = new StormwaterMapInitJson("areaBasedResultsMap", boundingBox, layerGeoJsons) { LayerControlClass = "area-based-map-layer-control" };
            var loadBasedMapInitJson= new StormwaterMapInitJson("loadBasedResultsMap", boundingBox, layerGeoJsons) { LayerControlClass = "load-based-map-layer-control" };

            var neptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.TrashHomePage);
            var viewData = new IndexViewData(CurrentPerson, neptunePage, ovtaBasedMapInitJson, areaBasedMapInitJson,
                loadBasedMapInitJson, treatmentBmps, TrashCaptureStatusType.All, parcels, stormwaterJurisdictionsPersonCanView);

            return RazorView<Index, IndexViewData>(viewData);
        }

        [HttpGet]
        [NeptuneAdminFeature]
        public PartialViewResult RefreshTrashGeneratingUnits()
        {
            return ViewRefreshTrashGeneratingUnits(new ConfirmDialogFormViewModel());
        }

        private PartialViewResult ViewRefreshTrashGeneratingUnits(ConfirmDialogFormViewModel viewModel)
        {
            const string confirmMessage = "This operation will take several minutes to run. Updated data will not be available until the operation is complete.";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [NeptuneAdminFeature]
        public async Task<ActionResult> RefreshTrashGeneratingUnits(ConfirmDialogFormViewModel viewModel)
        {
            HttpRequestStorage.DatabaseEntities.Database.CommandTimeout = 600;
            await HttpRequestStorage.DatabaseEntities.Database.ExecuteSqlCommandAsync("execute dbo.pRebuildTrashGeneratingUnitTable");

            return new ModalDialogFormJsonResult();
        }
    }
}
