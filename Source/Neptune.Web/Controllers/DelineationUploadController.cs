/*-----------------------------------------------------------------------
<copyright file="DelineationController.cs" company="Tahoe Regional Planning Agency">
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

using LtInfo.Common.MvcResults;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.DelineationUpload;
using Neptune.Web.Views.Shared;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Neptune.Web.Controllers
{
    public class DelineationUploadController : NeptuneBaseController
    {
        [NeptuneViewFeature]
        public PartialViewResult SummaryForMap(DelineationPrimaryKey delineationPrimaryKey)
        {
            var delineation = delineationPrimaryKey.EntityObject;
            var deleteDelineationUrl = delineation.GetDeleteUrl();
            var canDeleteCatchment = delineation.CanDelete(CurrentPerson);
            var viewData = new SummaryForMapViewData(CurrentPerson, delineation, deleteDelineationUrl, canDeleteCatchment);
            return RazorPartialView<SummaryForMap, SummaryForMapViewData>(viewData);
        }

        [HttpGet]
        [JurisdictionManageFeature]
        public ViewResult UpdateDelineationGeometry()
        {
            var viewModel = new UpdateDelineationGeometryViewModel();
            return ViewUpdateDelineationGeometry(viewModel);
        }

        [HttpPost]
        [JurisdictionManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult UpdateDelineationGeometry(UpdateDelineationGeometryViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var viewData = new UpdateDelineationGeometryViewData(CurrentPerson, null, null);
                return RazorPartialView<UpdateDelineationGeometryErrors, UpdateDelineationGeometryViewData, UpdateDelineationGeometryViewModel>(viewData, viewModel);
            }

            viewModel.UpdateModel(CurrentPerson);

            return RedirectToAction(new SitkaRoute<DelineationUploadController>(c => c.ApproveDelineationGisUpload()));
        }

        private ViewResult ViewUpdateDelineationGeometry(UpdateDelineationGeometryViewModel viewModel)
        {
            var newGisUploadUrl = SitkaRoute<DelineationUploadController>.BuildUrlFromExpression(c => c.UpdateDelineationGeometry());
            var approveGisUploadUrl = SitkaRoute<DelineationUploadController>.BuildUrlFromExpression(c => c.ApproveDelineationGisUpload());

            var viewData = new UpdateDelineationGeometryViewData(CurrentPerson, newGisUploadUrl, approveGisUploadUrl);
            return RazorView<UpdateDelineationGeometry, UpdateDelineationGeometryViewData, UpdateDelineationGeometryViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [JurisdictionManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult ApproveDelineationGisUpload()
        {
            var viewModel = new ApproveDelineationGisUploadViewModel(CurrentPerson);
            return ViewApproveDelineationGisUpload(viewModel);
        }

        [HttpPost]
        [JurisdictionManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult ApproveDelineationGisUpload(ApproveDelineationGisUploadViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewUpdateDelineationGeometry(new UpdateDelineationGeometryViewModel());
            }

            var successfulUploadCount = viewModel.UpdateModel(CurrentPerson);
            var organizationDisplayName = HttpRequestStorage.DatabaseEntities.StormwaterJurisdictions.Single(x => x.StormwaterJurisdictionID == viewModel.StormwaterJurisdictionID).GetOrganizationDisplayName();
            SetMessageForDisplay($"{successfulUploadCount} Delineations were successfully uploaded for Jurisdiction {organizationDisplayName}");

            return RedirectToAction(new SitkaRoute<ManagerDashboardController>(c => c.Index()));
        }

        private PartialViewResult ViewApproveDelineationGisUpload(ApproveDelineationGisUploadViewModel viewModel)
        {
            //var delineationGeometryStagings = CurrentPerson.DelineationGeometryStagings.ToList();
            //var layerColors = delineationGeometryStagings.Select((value, index) => new { index, value })
            //    .ToDictionary(x => x.value.DelineationGeometryStagingID, x => NeptuneHelpers.DefaultColorRange[x.index]);
            //var layers =
            //    delineationGeometryStagings.Select(
            //        (delineationGeometryStaging, i) =>
            //            new LayerGeoJson(delineationGeometryStaging.FeatureClassName,
            //                delineationGeometryStaging.ToGeoJsonFeatureCollection(),
            //                layerColors[delineationGeometryStaging.DelineationGeometryStagingID],
            //                1,
            //                LayerInitialVisibility.Show)).ToList();
            //var boundingBox = BoundingBox.MakeBoundingBoxFromLayerGeoJsonList(layers);
            //var mapInitJson = new StormwaterMapInitJson("delineationGeometryPreviewMap", 10, layers, boundingBox) { AllowFullScreen = false };
            //var stormwaterJurisdictions = CurrentPerson.GetStormwaterJurisdictionsPersonCanEdit();
            //var uploadGisReportUrlTemplate =
            //    new UrlTemplate<int, int, string>(
            //        SitkaRoute<DelineationUploadController>.BuildUrlFromExpression(c => c.UploadGisReport(UrlTemplate.Parameter1Int, UrlTemplate.Parameter2Int, UrlTemplate.Parameter3String))).UrlTemplateString;
            //var delineationIndexUrl =
            //    SitkaRoute<DelineationController>.BuildUrlFromExpression(c => c.DelineationMap(null));

            //var viewData = new ApproveDelineationGisUploadViewData(CurrentPerson, mapInitJson, layerColors, stormwaterJurisdictions, uploadGisReportUrlTemplate, delineationIndexUrl);
            //return RazorPartialView<ApproveDelineationGisUpload, ApproveDelineationGisUploadViewData, ApproveDelineationGisUploadViewModel>(viewData, viewModel);

            throw new NotImplementedException();
        }

        [JurisdictionManageFeature]
        public JsonResult UploadGisReport(StormwaterJurisdictionPrimaryKey stormwaterJurisdictionPrimaryKey,
            //DelineationGeometryStagingPrimaryKey delineationGeometryStagingPrimaryKey
            string selectedProperty)
        {
            //var stormwaterJurisdiction = stormwaterJurisdictionPrimaryKey.EntityObject;
            //var delineationGeometryStaging = delineationGeometryStagingPrimaryKey.EntityObject;

            //Check.Assert(delineationGeometryStaging.PersonID == CurrentPerson.PersonID, "Delineation Geometry Staging must belong to the current person");

            //return Json(DelineationUploadGisReportJsonResult.GetDelineationUpoadGisReportFromStaging(CurrentPerson, stormwaterJurisdiction, delineationGeometryStaging, selectedProperty));
            throw new NotImplementedException();

        }

        [HttpGet]
        [DelineationDeleteFeature]
        public PartialViewResult Delete(DelineationPrimaryKey delineationPrimaryKey)
        {
            var delineation = delineationPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(delineation.DelineationID);
            return ViewDelete(delineation, viewModel);
        }

        [HttpPost]
        [DelineationDeleteFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Delete(DelineationPrimaryKey delineationPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var delineation = delineationPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDelete(delineation, viewModel);
            }

            HttpRequestStorage.DatabaseEntities.Delineations.DeleteDelineation(delineation);
            return new ModalDialogFormJsonResult(SitkaRoute<DelineationController>.BuildUrlFromExpression(c => c.DelineationMap(null)));
        }

        private PartialViewResult ViewDelete(Delineation delineation, ConfirmDialogFormViewModel viewModel)
        {
            var canDelete = delineation.CanDelete(CurrentPerson);
            var confirmMessage = canDelete
                ? "Are you sure you want to delete the Delineation?"
                : ConfirmDialogFormViewData.GetStandardCannotDeleteMessage("Delineation", SitkaRoute<TreatmentBMPController>.BuildLinkFromExpression(x => x.Index(), "here"));

            var viewData = new ConfirmDialogFormViewData(confirmMessage, canDelete);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }
    }
}
