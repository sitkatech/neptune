﻿using System;
using GeoJSON.Net.Feature;
using Hangfire;
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
            return Json(new {regionalSubbasinIDs = regionalSubbasin.TraceUpstreamCatchmentsReturnIDList(HttpRequestStorage.DatabaseEntities) }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [NeptuneViewFeature]
        public ContentResult UpstreamDelineation(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var dbGeometry = treatmentBMPPrimaryKey.EntityObject.GetCentralizedDelineationGeometry4326(HttpRequestStorage.DatabaseEntities);

            var feature = DbGeometryToGeoJsonHelper.FromDbGeometryWithNoReproject(dbGeometry);

            return Content(JObject.FromObject(feature).ToString(Formatting.None));
        }

        
        [HttpGet]
        [NeptuneAdminFeature]
        public ViewResult Detail(RegionalSubbasinPrimaryKey regionalSubbasinPrimaryKey)
        {
            var regionalSubbasin = regionalSubbasinPrimaryKey.EntityObject;
            var regionalSubbasinCatchmentGeometry4326 = regionalSubbasin.CatchmentGeometry4326;

            var geoJson = DbGeometryToGeoJsonHelper.FromDbGeometryWithNoReproject(regionalSubbasinCatchmentGeometry4326);
            var geoJsonFeatureCollection = new FeatureCollection(new List<Feature> {geoJson});
            var layerGeoJson = new LayerGeoJson("Catchment Boundary", geoJsonFeatureCollection,"#000000", 1, LayerInitialVisibility.Show, false );
            var stormwaterMapInitJson = new StormwaterMapInitJson("map", MapInitJson.DefaultZoomLevel, new List<LayerGeoJson>{layerGeoJson}, new BoundingBox(regionalSubbasinCatchmentGeometry4326));


            var hruCharacteristics = regionalSubbasin.GetHRUCharacteristics().ToList();
            return RazorView<Detail, DetailViewData>(new DetailViewData(CurrentPerson,
                regionalSubbasin,
                new HRUCharacteristicsViewData(regionalSubbasin, hruCharacteristics),
                stormwaterMapInitJson, hruCharacteristics.Any()));
        }

        [HttpGet]
        [NeptuneAdminFeature]
        public PartialViewResult RefreshFromOCSurvey()
        {
            return ViewRefreshFromOCSurvey(new ConfirmDialogFormViewModel());
        }

        [HttpPost]
        [NeptuneAdminFeature]
        public ActionResult RefreshFromOCSurvey(ConfirmDialogFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewRefreshFromOCSurvey(viewModel);
            }

            BackgroundJob.Enqueue(() => ScheduledBackgroundJobLaunchHelper.RunRegionalSubbasinRefreshBackgroundJob(CurrentPerson.PersonID, false));
            SetMessageForDisplay("Regional Subbasins refresh will run in the background.");
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewRefreshFromOCSurvey(ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = "Are you sure you want to refresh the Regional Subbasins layer from OC Survey?<br /><br />This can take a little while to run.";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [NeptuneAdminFeature]
        public ViewResult Grid()
        {
            var viewData = new GridViewData(CurrentPerson);
            return RazorView<Grid, GridViewData>(viewData);
        }

        [NeptuneAdminFeature]
        public GridJsonNetJObjectResult<RegionalSubbasin> RegionalSubbasinGridJsonData()
        {
            var gridSpec = new RegionalSubbasinGridSpec();
            var regionalSubbasins = HttpRequestStorage.DatabaseEntities.RegionalSubbasins.ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<RegionalSubbasin>(regionalSubbasins, gridSpec);
            return gridJsonNetJObjectResult;
        }
    }
}
