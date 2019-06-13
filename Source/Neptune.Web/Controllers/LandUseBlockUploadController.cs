/*-----------------------------------------------------------------------
<copyright file="LandUseBlockController.cs" company="Tahoe Regional Planning Agency">
Copyright (c) Tahoe Regional Planning Agency. All rights reserved.
<author>Sitka Technology Group</author>
</copyright>

<license>
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License <http://www.gnu.org/licenses/> for more details.

Source code is available upon request via <support@sitkatech.com>.
</license>
-----------------------------------------------------------------------*/

using LtInfo.Common;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.MvcResults;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.LandUseBlockUpload;
using Neptune.Web.Views.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Neptune.Web.Controllers
{
    public class LandUseBlockUploadController : NeptuneBaseController
    {
        [NeptuneViewFeature]
        public PartialViewResult SummaryForMap(LandUseBlockPrimaryKey landUseBlockPrimaryKey)
        {
            var landUseBlock = landUseBlockPrimaryKey.EntityObject;
            //var deleteLandUseBlockUrl = landUseBlock.GetDeleteUrl(); //todo add this when the route get created
            //var canDeleteCatchment = landUseBlock.CanDelete(CurrentPerson);
            var viewData = new SummaryForMapViewData(CurrentPerson, landUseBlock);
            return RazorPartialView<SummaryForMap, SummaryForMapViewData>(viewData);
        }

        [HttpGet]
        [JurisdictionManageFeature]
        public ViewResult UpdateLandUseBlockGeometry()
        {
            var viewModel = new UpdateLandUseBlockGeometryViewModel();
            return ViewUpdateLandUseBlockGeometry(viewModel);
        }

        [HttpPost]
        [JurisdictionManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult UpdateLandUseBlockGeometry(UpdateLandUseBlockGeometryViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var viewData = new UpdateLandUseBlockGeometryViewData(CurrentPerson, null, null);
                return RazorPartialView<UpdateLandUseBlockGeometryErrors, UpdateLandUseBlockGeometryViewData, UpdateLandUseBlockGeometryViewModel>(viewData, viewModel);
            }
            viewModel.UpdateModel(CurrentPerson);

            return RedirectToAction(new SitkaRoute<LandUseBlockUploadController>(c => c.ApproveLandUseBlockGisUpload()));
        }

        private ViewResult ViewUpdateLandUseBlockGeometry(UpdateLandUseBlockGeometryViewModel viewModel)
        {
            var newGisUploadUrl = SitkaRoute<LandUseBlockUploadController>.BuildUrlFromExpression(c => c.UpdateLandUseBlockGeometry());
            var approveGisUploadUrl = SitkaRoute<LandUseBlockUploadController>.BuildUrlFromExpression(c => c.ApproveLandUseBlockGisUpload());

            var viewData = new UpdateLandUseBlockGeometryViewData(CurrentPerson, newGisUploadUrl, approveGisUploadUrl);
            return RazorView<UpdateLandUseBlockGeometry, UpdateLandUseBlockGeometryViewData, UpdateLandUseBlockGeometryViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [JurisdictionManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult ApproveLandUseBlockGisUpload()
        {
            var viewModel = new ApproveLandUseBlockGisUploadViewModel(CurrentPerson);
            return ViewApproveLandUseBlockGisUpload(viewModel);
        }

        [HttpPost]
        [JurisdictionManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult ApproveLandUseBlockGisUpload(ApproveLandUseBlockGisUploadViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewUpdateLandUseBlockGeometry(new UpdateLandUseBlockGeometryViewModel());
            }
            viewModel.UpdateModel(CurrentPerson);

            return RedirectToAction(new SitkaRoute<LandUseBlockController>(c => c.Index()));
        }

        private PartialViewResult ViewApproveLandUseBlockGisUpload(ApproveLandUseBlockGisUploadViewModel viewModel)
        {
            var landUseBlockGeometryStagings = CurrentPerson.LandUseBlockGeometryStagings.ToList();
            var layerColors = landUseBlockGeometryStagings.Select((value, index) => new {index, value})
                .ToDictionary(x => x.value.LandUseBlockGeometryStagingID, x => NeptuneHelpers.DefaultColorRange[x.index]);
            var layers =
                landUseBlockGeometryStagings.Select(
                    (landUseBlockGeometryStaging, i) =>
                        new LayerGeoJson(landUseBlockGeometryStaging.FeatureClassName,
                            landUseBlockGeometryStaging.ToGeoJsonFeatureCollection(),
                            layerColors[landUseBlockGeometryStaging.LandUseBlockGeometryStagingID],
                            1,
                            LayerInitialVisibility.Show)).ToList();
            var boundingBox = BoundingBox.MakeBoundingBoxFromLayerGeoJsonList(layers);
            var mapInitJson = new StormwaterMapInitJson("landUseBlockGeometryPreviewMap", 10, layers, boundingBox) {AllowFullScreen = false};
            var stormwaterJurisdictions = CurrentPerson.GetStormwaterJurisdictionsPersonCanEdit();
            var uploadGisReportUrlTemplate =
                new UrlTemplate<int, int, string>(
                    SitkaRoute<LandUseBlockUploadController>.BuildUrlFromExpression(c => c.UploadGisReport(UrlTemplate.Parameter1Int, UrlTemplate.Parameter2Int, UrlTemplate.Parameter3String))).UrlTemplateString;
            var landUseBlockIndexUrl =
                SitkaRoute<LandUseBlockController>.BuildUrlFromExpression(c => c.Index());

            var viewData = new ApproveLandUseBlockGisUploadViewData(CurrentPerson, mapInitJson, layerColors, stormwaterJurisdictions, uploadGisReportUrlTemplate, landUseBlockIndexUrl);
            return RazorPartialView<ApproveLandUseBlockGisUpload, ApproveLandUseBlockGisUploadViewData, ApproveLandUseBlockGisUploadViewModel>(viewData, viewModel);
        }

        [JurisdictionManageFeature]
        public JsonResult UploadGisReport(StormwaterJurisdictionPrimaryKey stormwaterJurisdictionPrimaryKey,
            LandUseBlockGeometryStagingPrimaryKey landUseBlockGeometryStagingPrimaryKey,
            string selectedProperty)
        {
            var stormwaterJurisdiction = stormwaterJurisdictionPrimaryKey.EntityObject;
            var landUseBlockGeometryStaging = landUseBlockGeometryStagingPrimaryKey.EntityObject;

            Check.Assert(landUseBlockGeometryStaging.PersonID == CurrentPerson.PersonID, "LandUseBlock Geometry Staging must belong to the current person");

            return Json(LandUseBlockUploadGisReportJsonResult.GetLandUseBlockUpoadGisReportFromStaging(CurrentPerson, stormwaterJurisdiction, landUseBlockGeometryStaging, selectedProperty));
        }
    }
}