/*-----------------------------------------------------------------------
<copyright file="ModeledCatchmentController.cs" company="Tahoe Regional Planning Agency">
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

using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using LtInfo.Common;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.MvcResults;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.DelineationUpload;
using Neptune.Web.Views.Shared;
using Newtonsoft.Json;
using Index = Neptune.Web.Views.DelineationUpload.Index;
using IndexGridSpec = Neptune.Web.Views.DelineationUpload.IndexGridSpec;
using IndexViewData = Neptune.Web.Views.DelineationUpload.IndexViewData;

namespace Neptune.Web.Controllers
{
    public class DelineationUploadController : NeptuneBaseController
    {
        [NeptuneViewFeature]
        public ViewResult Index()
        {
            var modeledCatchments = HttpRequestStorage.DatabaseEntities.ModeledCatchments.ToList();
            var modeledCatchmentLayerGeoJson = StormwaterMapInitJson.MakeModeledCatchmentLayerGeoJson(modeledCatchments, false, false);
            var mapInitJson = new SearchMapInitJson("StormwaterDetailMap", modeledCatchmentLayerGeoJson);

            var corralPage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.ModeledCatchment);
            var updateModeledCatchmentGeometryUrl = SitkaRoute<DelineationUploadController>.BuildUrlFromExpression(c => c.UpdateModeledCatchmentGeometry());
            var viewData = new IndexViewData(CurrentPerson, mapInitJson, corralPage, updateModeledCatchmentGeometryUrl);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [NeptuneViewFeature]
        public GridJsonNetJObjectResult<ModeledCatchment> IndexGridJsonData()
        {
            IndexGridSpec gridSpec;
            var modeledCatchments = GetModeledCatchmentsAndGridSpec(out gridSpec, CurrentPerson);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<ModeledCatchment>(modeledCatchments, gridSpec);
            return gridJsonNetJObjectResult;
        }

        private List<ModeledCatchment> GetModeledCatchmentsAndGridSpec(out IndexGridSpec gridSpec, Person currentPerson)
        {
            gridSpec = new IndexGridSpec(currentPerson);
            return HttpRequestStorage.DatabaseEntities.ModeledCatchments.ToList();
        }

        [NeptuneViewFeature]
        public ViewResult Detail(ModeledCatchmentPrimaryKey modeledCatchmentPrimaryKey)
        {
            var modeledCatchment = modeledCatchmentPrimaryKey.EntityObject;
            var layerGeoJsons = MapInitJsonHelpers.GetJurisdictionMapLayers().ToList();
            var mapInitJson = new StormwaterMapInitJson("StormwaterDetailMap", 1, layerGeoJsons, new BoundingBox(modeledCatchment.ModeledCatchmentGeometry));

            if (modeledCatchment.ModeledCatchmentGeometry != null)
            {
                mapInitJson.Layers.Add(StormwaterMapInitJson.MakeModeledCatchmentLayerGeoJson(new[] {modeledCatchment}, false, false));
            }

            var viewData = new DetailViewData(CurrentPerson, modeledCatchment, mapInitJson);
            return RazorView<Detail, DetailViewData>(viewData);
        }

        [HttpGet]
        [ModeledCatchmentManageFeature]
        public ViewResult Edit(ModeledCatchmentPrimaryKey modeledCatchmentPrimaryKey)
        {
            var modeledCatchment = modeledCatchmentPrimaryKey.EntityObject;
            var viewModel = new EditViewModel(modeledCatchment);
            return ViewEdit(viewModel);
        }

        [HttpPost]
        [ModeledCatchmentManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Edit(ModeledCatchmentPrimaryKey modeledCatchmentPrimaryKey, EditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel);
            }

            var modeledCatchment = modeledCatchmentPrimaryKey.EntityObject;
            viewModel.UpdateModel(modeledCatchment, CurrentPerson);
            return RedirectToAction(new SitkaRoute<DelineationUploadController>(c => c.Detail(modeledCatchment.PrimaryKey)));
        }

        private ViewResult ViewEdit(EditViewModel viewModel)
        {
            var modeledCatchment = HttpRequestStorage.DatabaseEntities.ModeledCatchments.SingleOrDefault(x => x.ModeledCatchmentID == viewModel.ModeledCatchmentID);
            var viewData = new EditViewData(CurrentPerson, modeledCatchment);
            return RazorView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }

        [NeptuneViewFeature]
        public PartialViewResult SummaryForMap(ModeledCatchmentPrimaryKey modeledCatchmentPrimaryKey)
        {
            var modeledCatchment = modeledCatchmentPrimaryKey.EntityObject;
            var deleteModeledCatchmentUrl = modeledCatchment.GetDeleteUrl(); //todo add this when the route get created
            var canDeleteCatchment = modeledCatchment.CanDelete(CurrentPerson);
            var viewData = new SummaryForMapViewData(CurrentPerson, modeledCatchment, deleteModeledCatchmentUrl, canDeleteCatchment);
            return RazorPartialView<SummaryForMap, SummaryForMapViewData>(viewData);
        }

        [NeptuneViewFeature]
        public JsonResult FindByName(string term)
        {
            var searchString = term.Trim();
            var allModeledCatchmentsMatchingSearchString = HttpRequestStorage.DatabaseEntities.ModeledCatchments.Where(x => x.ModeledCatchmentName.Contains(searchString)).ToList();

            var listItems = allModeledCatchmentsMatchingSearchString.OrderBy(x => x.ModeledCatchmentName).Take(20).Select(mc =>
            {
                var modeledCatchmentMapSummaryData = new SearchMapSummaryData(mc.GetMapSummaryUrl(),
                    mc.ModeledCatchmentGeometry,
                    mc.ModeledCatchmentGeometry.Centroid.YCoordinate,
                    mc.ModeledCatchmentGeometry.Centroid.XCoordinate,
                    mc.ModeledCatchmentID);
                var listItem = new ListItem(mc.ModeledCatchmentName, JsonConvert.SerializeObject(modeledCatchmentMapSummaryData));
                return listItem;
            }).ToList();

            return Json(listItems, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [JurisdictionManageFeature]
        public ViewResult UpdateModeledCatchmentGeometry()
        {
            var viewModel = new UpdateDelineationGeometryViewModel();
            return ViewUpdateModeledCatchmentGeometry(viewModel);
        }

        [HttpPost]
        [JurisdictionManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult UpdateModeledCatchmentGeometry(UpdateDelineationGeometryViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var viewData = new UpdateDelineationGeometryViewData(CurrentPerson, null, null);
                return RazorPartialView<UpdateDelineationGeometryErrors, UpdateDelineationGeometryViewData, UpdateDelineationGeometryViewModel>(viewData, viewModel);
            }
            viewModel.UpdateModel(CurrentPerson);

            return RedirectToAction(new SitkaRoute<DelineationUploadController>(c => c.ApproveModeledCatchmentGisUpload()));
        }

        private ViewResult ViewUpdateModeledCatchmentGeometry(UpdateDelineationGeometryViewModel viewModel)
        {
            var newGisUploadUrl = SitkaRoute<DelineationUploadController>.BuildUrlFromExpression(c => c.UpdateModeledCatchmentGeometry());
            var approveGisUploadUrl = SitkaRoute<DelineationUploadController>.BuildUrlFromExpression(c => c.ApproveModeledCatchmentGisUpload());

            var viewData = new UpdateDelineationGeometryViewData(CurrentPerson, newGisUploadUrl, approveGisUploadUrl);
            return RazorView<UpdateDelineationGeometry, UpdateDelineationGeometryViewData, UpdateDelineationGeometryViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [JurisdictionManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult ApproveModeledCatchmentGisUpload()
        {
            var viewModel = new ApproveDelineationGisUploadViewModel(CurrentPerson);
            return ViewApproveModeledCatchmentGisUpload(viewModel);
        }

        [HttpPost]
        [JurisdictionManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult ApproveModeledCatchmentGisUpload(ApproveDelineationGisUploadViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewUpdateModeledCatchmentGeometry(new UpdateDelineationGeometryViewModel());
            }
            viewModel.UpdateModel(CurrentPerson);

            return RedirectToAction(new SitkaRoute<DelineationUploadController>(c => c.Index()));
        }

        private PartialViewResult ViewApproveModeledCatchmentGisUpload(ApproveDelineationGisUploadViewModel viewModel)
        {
            var modeledCatchmentGeometryStagings = CurrentPerson.ModeledCatchmentGeometryStagings.ToList();
            var layerColors = modeledCatchmentGeometryStagings.Select((value, index) => new {index, value})
                .ToDictionary(x => x.value.ModeledCatchmentGeometryStagingID, x => NeptuneHelpers.DefaultColorRange[x.index]);
            var layers =
                modeledCatchmentGeometryStagings.Select(
                    (modeledCatchmentGeometryStaging, i) =>
                        new LayerGeoJson(modeledCatchmentGeometryStaging.FeatureClassName,
                            modeledCatchmentGeometryStaging.ToGeoJsonFeatureCollection(),
                            layerColors[modeledCatchmentGeometryStaging.ModeledCatchmentGeometryStagingID],
                            1,
                            LayerInitialVisibility.Show)).ToList();
            var boundingBox = BoundingBox.MakeBoundingBoxFromLayerGeoJsonList(layers);
            var mapInitJson = new StormwaterMapInitJson("modeledCatchmentGeometryPreviewMap", 10, layers, boundingBox) {AllowFullScreen = false};
            var stormwaterJurisdictions = CurrentPerson.StormwaterJurisdictionPeople.Select(x => x.StormwaterJurisdiction);
            var uploadGisReportUrlTemplate =
                new UrlTemplate<int, int, string>(
                    SitkaRoute<DelineationUploadController>.BuildUrlFromExpression(c => c.UploadGisReport(UrlTemplate.Parameter1Int, UrlTemplate.Parameter2Int, UrlTemplate.Parameter3String))).UrlTemplateString;
            var modeledCatchmentIndexUrl = SitkaRoute<DelineationUploadController>.BuildUrlFromExpression(c => c.Index());

            var viewData = new ApproveDelineationGisUploadViewData(CurrentPerson, mapInitJson, layerColors, stormwaterJurisdictions, uploadGisReportUrlTemplate, modeledCatchmentIndexUrl);
            return RazorPartialView<ApproveDelineationGisUpload, ApproveDelineationGisUploadViewData, ApproveDelineationGisUploadViewModel>(viewData, viewModel);
        }

        [JurisdictionManageFeature]
        public JsonResult UploadGisReport(StormwaterJurisdictionPrimaryKey stormwaterJurisdictionPrimaryKey,
            ModeledCatchmentGeometryStagingPrimaryKey modeledCatchmentGeometryStagingPrimaryKey,
            string selectedProperty)
        {
            var stormwaterJurisdiction = stormwaterJurisdictionPrimaryKey.EntityObject;
            var modeledCatchmentGeometryStaging = modeledCatchmentGeometryStagingPrimaryKey.EntityObject;

            Check.Assert(modeledCatchmentGeometryStaging.PersonID == CurrentPerson.PersonID, "Modeled Catchment Geometry Staging must belong to the current person");

            return Json(ModeledCatchmentUploadGisReportJsonResult.GetModeledCatchmentUpoadGisReportFromStaging(CurrentPerson, stormwaterJurisdiction, modeledCatchmentGeometryStaging, selectedProperty));
        }

        [HttpGet]
        [ModeledCatchmentDeleteFeature]
        public PartialViewResult Delete(ModeledCatchmentPrimaryKey modeledCatchmentPrimaryKey)
        {
            var modeledCatchment = modeledCatchmentPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(modeledCatchment.ModeledCatchmentID);
            return ViewDelete(modeledCatchment, viewModel);
        }

        [HttpPost]
        [ModeledCatchmentDeleteFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Delete(ModeledCatchmentPrimaryKey modeledCatchmentPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var modeledCatchment = modeledCatchmentPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDelete(modeledCatchment, viewModel);
            }

            HttpRequestStorage.DatabaseEntities.ModeledCatchments.DeleteModeledCatchment(modeledCatchment);
            return new ModalDialogFormJsonResult(SitkaRoute<DelineationUploadController>.BuildUrlFromExpression(c => c.Index()));
        }

        private PartialViewResult ViewDelete(ModeledCatchment modeledCatchment, ConfirmDialogFormViewModel viewModel)
        {
            var canDelete = modeledCatchment.CanDelete(CurrentPerson);
            var confirmMessage = canDelete
                ? "Are you sure you want to delete the Modeled Catchment?"
                : ConfirmDialogFormViewData.GetStandardCannotDeleteMessage("Modeled Catchment", SitkaRoute<DelineationUploadController>.BuildLinkFromExpression(x => x.Detail(modeledCatchment), "here"));

            var viewData = new ConfirmDialogFormViewData(confirmMessage, canDelete);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }
    }
}