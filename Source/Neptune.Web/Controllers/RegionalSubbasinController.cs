using GeoJSON.Net.Feature;
using Hangfire;
using LtInfo.Common;
using LtInfo.Common.DbSpatial;
using LtInfo.Common.GeoJson;
using LtInfo.Common.MvcResults;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.ScheduledJobs;
using Neptune.Web.Security;
using Neptune.Web.Views.RegionalSubbasin;
using Neptune.Web.Views.Shared;
using Neptune.Web.Views.Shared.HRUCharacteristics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web.Mvc;
using Index = Neptune.Web.Views.RegionalSubbasin.Index;
using IndexViewData = Neptune.Web.Views.RegionalSubbasin.IndexViewData;

namespace Neptune.Web.Controllers
{
    public class RegionalSubbasinController : NeptuneBaseController
    {
        [HttpGet]
        [NeptuneAdminFeature]
        public ViewResult Index()
        {
            var geoServerUrl = NeptuneWebConfiguration.ParcelMapServiceUrl;
            var regionalSubbasinLayerName = NeptuneWebConfiguration.RegionalSubbasinLayerName;

            var viewData = new IndexViewData(CurrentPerson, new RegionalSubbasinMapInitJson("regionalSubbasinMap"), geoServerUrl, regionalSubbasinLayerName);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [HttpGet]
        [NeptuneViewFeature]
        public JsonResult UpstreamCatchments(RegionalSubbasinPrimaryKey regionalSubbasinPrimaryKey)
        {
            var regionalSubbasin = regionalSubbasinPrimaryKey.EntityObject;
            return Json(new {regionalSubbasinIDs = regionalSubbasin.TraceUpstreamCatchmentsReturnIDList()}, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [NeptuneViewFeature]
        public ContentResult UpstreamDelineation(RegionalSubbasinPrimaryKey regionalSubbasinPrimaryKey)
        {
            var regionalSubbasin = regionalSubbasinPrimaryKey.EntityObject;
            var regionalSubbasinIDs = regionalSubbasin.TraceUpstreamCatchmentsReturnIDList();

            regionalSubbasinIDs.Add(regionalSubbasin.RegionalSubbasinID);

            var unionOfUpstreamRegionalSubbasins = HttpRequestStorage.DatabaseEntities.RegionalSubbasins
                .Where(x => regionalSubbasinIDs.Contains(x.RegionalSubbasinID)).Select(x => x.CatchmentGeometry)
                .ToList().UnionListGeometries();

            // Remove interior slivers introduced in the case that the non-cascading union strategy is used (see UnionListGeometries for more info)
            var dbGeometry = unionOfUpstreamRegionalSubbasins.Buffer(0);

            var feature = DbGeometryToGeoJsonHelper.FromDbGeometryWithReprojectionCheck(dbGeometry);

            return Content(JObject.FromObject(feature).ToString(Formatting.None));
        }

        [HttpGet]
        [NeptuneAdminFeature]
        public PartialViewResult RefreshHRUCharacteristics(RegionalSubbasinPrimaryKey regionalSubbasinPrimaryKey)
        {
            var regionalSubbasin = regionalSubbasinPrimaryKey.EntityObject;
            return ViewRefreshHRUCharacteristics(regionalSubbasin, new ConfirmDialogFormViewModel());
        }


        [HttpPost]
        [NeptuneAdminFeature]
        public ActionResult RefreshHRUCharacteristics(RegionalSubbasinPrimaryKey regionalSubbasinPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var regionalSubbasin = regionalSubbasinPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewRefreshHRUCharacteristics(regionalSubbasin, viewModel);
            }

            HRUHelper.RetrieveAndSaveHRUCharacteristics(regionalSubbasin, x => x.RegionalSubbasinID = regionalSubbasin.RegionalSubbasinID);
            SetMessageForDisplay($"Successfully updated HRU Characteristics for {regionalSubbasin.Watershed} {regionalSubbasin.DrainID}: {regionalSubbasin.RegionalSubbasinID}");
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewRefreshHRUCharacteristics(RegionalSubbasin regionalSubbasin, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = $"Are you sure you want to refresh the HRU Statistics for {regionalSubbasin.Watershed} {regionalSubbasin.DrainID}: {regionalSubbasin.RegionalSubbasinID}?<br /><br />This can take a little while to run.";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }


        [HttpGet]
        [NeptuneAdminFeature]
        public ViewResult Detail(RegionalSubbasinPrimaryKey regionalSubbasinPrimaryKey)
        {
            //HttpRequestStorage.DatabaseEntities.RegionalSubbasins.Load();

            var regionalSubbasin = //regionalSubbasinPrimaryKey.EntityObject;

            HttpRequestStorage.DatabaseEntities.RegionalSubbasins.Find(regionalSubbasinPrimaryKey.PrimaryKeyValue);

            var regionalSubbasinCatchmentGeometry4326 = regionalSubbasin.CatchmentGeometry4326;

            var geoJson = DbGeometryToGeoJsonHelper.FromDbGeometryNoReproject(regionalSubbasinCatchmentGeometry4326);
            var geoJsonFeatureCollection = new FeatureCollection(new List<Feature> {geoJson});
            var layerGeoJson = new LayerGeoJson("Catchment Boundary", geoJsonFeatureCollection,"#000000", 1, LayerInitialVisibility.Show, false );
            var stormwaterMapInitJson = new StormwaterMapInitJson("map", MapInitJson.DefaultZoomLevel, new List<LayerGeoJson>{layerGeoJson}, new BoundingBox(regionalSubbasinCatchmentGeometry4326));


            return RazorView<Detail, DetailViewData>(new DetailViewData(CurrentPerson,
                regionalSubbasin,
                new HRUCharacteristicsViewData(regionalSubbasin),
                stormwaterMapInitJson));
        }

        [HttpGet]
        [NeptuneAdminFeature]
        public ActionResult RefreshFromOCSurvey()
        {
            //return Content(RegionalSubbasinRefreshScheduledBackgroundJob.RunRefresh(HttpRequestStorage.DatabaseEntities));

            BackgroundJob.Enqueue(() => ScheduledBackgroundJobLaunchHelper.RunRegionalSubbasinRefreshBackgroundJob(CurrentPerson.PersonID));

            SetMessageForDisplay("Regional Subbasin refresh will run in the background.");
            return Redirect(SitkaRoute<RegionalSubbasinController>.BuildUrlFromExpression(x => x.Grid()));
        }


        [NeptuneAdminFeature]
        public ViewResult Grid()
        {
            var viewData = new Views.RegionalSubbasin.GridViewData(CurrentPerson);
            return RazorView<Views.RegionalSubbasin.Grid, Views.RegionalSubbasin.GridViewData>(viewData);
        }

        [NeptuneAdminFeature]
        public GridJsonNetJObjectResult<RegionalSubbasin> RegionalSubbasinGridJsonData()
        {
            // ReSharper disable once InconsistentNaming
            List<RegionalSubbasin> regionalSubbasins = GetRegionalSubbasinsAndGridSpec(out var gridSpec);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<RegionalSubbasin>(regionalSubbasins, gridSpec);
            return gridJsonNetJObjectResult;
        }

        private List<RegionalSubbasin> GetRegionalSubbasinsAndGridSpec(out RegionalSubbasinGridSpec gridSpec)
        {
            gridSpec = new RegionalSubbasinGridSpec();

            return HttpRequestStorage.DatabaseEntities.RegionalSubbasins.ToList();
        }
    }
}
