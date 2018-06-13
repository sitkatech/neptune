﻿/*-----------------------------------------------------------------------
<copyright file="TreatmentBMPController.cs" company="Tahoe Regional Planning Agency">
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
using LtInfo.Common.Models;
using LtInfo.Common.MvcResults;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.Shared;
using Neptune.Web.Views.TreatmentBMP;
using Newtonsoft.Json;
using FindABMP = Neptune.Web.Views.TreatmentBMP.FindABMP;

namespace Neptune.Web.Controllers
{
    public class TreatmentBMPController : NeptuneBaseController
    {
        [NeptuneViewFeature]
        // ReSharper disable once InconsistentNaming
        public ViewResult FindABMP()
        {
            var treatmentBmps = HttpRequestStorage.DatabaseEntities.TreatmentBMPs.ToList().Where(x => x.CanView(CurrentPerson)).ToList();
            var mapInitJson = new SearchMapInitJson("StormwaterIndexMap", StormwaterMapInitJson.MakeTreatmentBMPLayerGeoJson(treatmentBmps, false, false));
            var jurisdictionLayerGeoJson = mapInitJson.Layers.Single(x => x.LayerName == MapInitJson.CountyCityLayerName);
            jurisdictionLayerGeoJson.LayerOpacity = 0;
            jurisdictionLayerGeoJson.LayerInitialVisibility = LayerInitialVisibility.Show;


            var neptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.FindABMP);
            var viewData = new FindABMPViewData(CurrentPerson, mapInitJson, neptunePage, treatmentBmps);
            return RazorView<FindABMP, FindABMPViewData>(viewData);
        }
        [NeptuneViewFeature]
        public ViewResult Index()
        {
            var neptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.TreatmentBMP);
            var viewData = new IndexViewData(CurrentPerson, neptunePage);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [NeptuneViewFeature]
        public GridJsonNetJObjectResult<TreatmentBMP> TreatmentBMPGridJsonData()
        {
            var treatmentBmps = GetTreatmentBmpsAndGridSpec(out var gridSpec, CurrentPerson);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<TreatmentBMP>(treatmentBmps, gridSpec);
            return gridJsonNetJObjectResult;
        }

        private List<TreatmentBMP> GetTreatmentBmpsAndGridSpec(out TreatmentBMPGridSpec gridSpec, Person currentPerson)
        {
            gridSpec = new TreatmentBMPGridSpec(currentPerson);
            return HttpRequestStorage.DatabaseEntities.TreatmentBMPs.ToList().Where(x => x.CanView(CurrentPerson)).ToList();
        }

        [NeptuneViewFeature]
        public ViewResult Detail(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var mapInitJson = new StormwaterMapInitJson("StormwaterDetailMap", treatmentBMP.LocationPoint);
            mapInitJson.Layers.Add(StormwaterMapInitJson.MakeTreatmentBMPLayerGeoJson(new[] {treatmentBMP}, false, true));
            var carouselImages = treatmentBMP.TreatmentBMPImages.OrderBy(x => x.TreatmentBMPImageID).ToList();
            var imageCarouselViewData = new ImageCarouselViewData(carouselImages, 400);

            var viewData = new DetailViewData(CurrentPerson, treatmentBMP, mapInitJson, imageCarouselViewData);
            return RazorView<Detail, DetailViewData>(viewData);
        }

        [HttpGet]
        [JurisdictionEditFeature]
        public ViewResult New()
        {
            var viewModel = new EditViewModel();
            return ViewEdit(viewModel);
        }

        [HttpPost]
        [JurisdictionEditFeature]
        public ActionResult New(EditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel);
            }
            
            var treatmentBMP = MakePlaceholderTreatmentBMP(viewModel, CurrentPerson);
            viewModel.UpdateModel(treatmentBMP, CurrentPerson);
            HttpRequestStorage.DatabaseEntities.AllTreatmentBMPs.Add(treatmentBMP);
            HttpRequestStorage.DatabaseEntities.SaveChanges(CurrentPerson);

            SetMessageForDisplay("Treatment BMP successfully saved.");

            return RedirectToAction(new SitkaRoute<TreatmentBMPController>(c => c.Detail(treatmentBMP.PrimaryKey)));
        }

        private static TreatmentBMP MakePlaceholderTreatmentBMP(EditViewModel viewModel, Person currentPerson)
        {
            return new TreatmentBMP(string.Empty, viewModel.TreatmentBMPTypeID ?? ModelObjectHelpers.NotYetAssignedID,
                viewModel.StormwaterJurisdictionID ?? ModelObjectHelpers.NotYetAssignedID,
                currentPerson.OrganizationID);
        }

        [HttpGet]
        [TreatmentBMPEditFeature]
        public ViewResult Edit(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var viewModel = new EditViewModel(treatmentBMP);
            return ViewEdit(viewModel);
        }

        [HttpPost]
        [TreatmentBMPEditFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Edit(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey, EditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel);
            }

            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            viewModel.UpdateModel(treatmentBMP, CurrentPerson);

            SetMessageForDisplay("Treatment BMP successfully saved.");

            return RedirectToAction(new SitkaRoute<TreatmentBMPController>(c => c.Detail(treatmentBMP.PrimaryKey)));
        }

        private ViewResult ViewEdit(EditViewModel viewModel)
        {
            var treatmentBMP = ModelObjectHelpers.IsRealPrimaryKeyValue(viewModel.TreatmentBMPID) ? HttpRequestStorage.DatabaseEntities.TreatmentBMPs.GetTreatmentBMP(viewModel.TreatmentBMPID) : null;
            var stormwaterJurisdictions = HttpRequestStorage.DatabaseEntities.StormwaterJurisdictions.ToList().Where(x => CurrentPerson.IsAssignedToStormwaterJurisdiction(x)).ToList();
            var treatmentBMPTypes = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.ToList();
            var organizations = HttpRequestStorage.DatabaseEntities.Organizations.ToList();
            var waterQualityManagementPlans = HttpRequestStorage.DatabaseEntities.WaterQualityManagementPlans.Where(x => x.StormwaterJurisdictionID == treatmentBMP.StormwaterJurisdictionID).ToList();

            if (ModelObjectHelpers.IsRealPrimaryKeyValue(viewModel.StormwaterJurisdictionID))
            {
                var currentJurisdiction =
                    HttpRequestStorage.DatabaseEntities.StormwaterJurisdictions.GetStormwaterJurisdiction(
                        viewModel.StormwaterJurisdictionID ?? ModelObjectHelpers.NotYetAssignedID);
                if (!stormwaterJurisdictions.Contains(currentJurisdiction))
                {
                    stormwaterJurisdictions.Add(currentJurisdiction);
                }
            }

            var viewData = new EditViewData(CurrentPerson, treatmentBMP, stormwaterJurisdictions, treatmentBMPTypes, organizations, waterQualityManagementPlans);
            return RazorView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [TreatmentBMPDeleteFeature]
        public PartialViewResult Delete(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(treatmentBMP.TreatmentBMPID);
            return ViewDeleteTreatmentBMP(treatmentBMP, viewModel);
        }

        [HttpPost]
        [TreatmentBMPDeleteFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Delete(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteTreatmentBMP(treatmentBMP, viewModel);
            }
            if (treatmentBMP.TreatmentBMPBenchmarkAndThresholds.Any())
            {
                treatmentBMP.TreatmentBMPBenchmarkAndThresholds.DeleteTreatmentBMPBenchmarkAndThreshold();
            }

            treatmentBMP.DeleteFull();
            return new ModalDialogFormJsonResult(SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(c => c.FindABMP()));
        }

        private PartialViewResult ViewDeleteTreatmentBMP(TreatmentBMP treatmentBMP, ConfirmDialogFormViewModel viewModel)
        {
            var viewData = new ConfirmDialogFormViewData($"Are you sure you want to delete the '{treatmentBMP.TreatmentBMPName}' treatment BMP?");
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [NeptuneViewFeature]
        public PartialViewResult SummaryForMap(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var viewData = new SummaryForMapViewData(CurrentPerson, treatmentBMP);
            return RazorPartialView<SummaryForMap, SummaryForMapViewData>(viewData);
        }

        [NeptuneViewFeature]
        public JsonResult FindByName(string term)
        {
            var searchString = term.Trim();
            var allTreatmentBmPsMatchingSearchString =
                HttpRequestStorage.DatabaseEntities.TreatmentBMPs.Where(
                    x => x.TreatmentBMPName.Contains(searchString)).ToList();

            var listItems = allTreatmentBmPsMatchingSearchString.OrderBy(x => x.TreatmentBMPName).Take(20).Select(bmp =>
            {
                var treatmentBMPMapSummaryData = new SearchMapSummaryData(bmp.GetMapSummaryUrl(), bmp.LocationPoint, bmp.LocationPoint.YCoordinate.Value, bmp.LocationPoint.XCoordinate.Value, bmp.TreatmentBMPID);
                var listItem = new ListItem(bmp.TreatmentBMPName, JsonConvert.SerializeObject(treatmentBMPMapSummaryData));
                return listItem;
            }).ToList();

            return Json(listItems, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [TreatmentBMPEditFeature]
        public ViewResult EditAttributes(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey, CustomAttributeTypePurposePrimaryKey customAttributeTypePurposePrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var customAttributeTypePurpose = customAttributeTypePurposePrimaryKey.EntityObject;
            var viewModel = new EditAttributesViewModel(treatmentBMP, customAttributeTypePurpose);
            return ViewEditAttributes(viewModel, treatmentBMP, customAttributeTypePurpose);
        }

        [HttpPost]
        [TreatmentBMPEditFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditAttributes(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey, CustomAttributeTypePurposePrimaryKey customAttributeTypePurposePrimaryKey, EditAttributesViewModel viewModel)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var customAttributeTypePurpose = customAttributeTypePurposePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEditAttributes(viewModel, treatmentBMP, customAttributeTypePurpose);
            }

            var allCustomAttributeTypes = HttpRequestStorage.DatabaseEntities.CustomAttributeTypes.ToList();
            viewModel.UpdateModel(treatmentBMP, CurrentPerson, customAttributeTypePurpose, allCustomAttributeTypes);
            SetMessageForDisplay("Custom Attributes successfully saved.");
            return RedirectToAction(new SitkaRoute<TreatmentBMPController>(c => c.Detail(treatmentBMP.PrimaryKey)));
        }

        private ViewResult ViewEditAttributes(EditAttributesViewModel viewModel, TreatmentBMP treatmentBMP,
            CustomAttributeTypePurpose customAttributeTypePurpose)
        {
            var missingRequiredAttributes = treatmentBMP.TreatmentBMPType.TreatmentBMPTypeCustomAttributeTypes.Any(x =>
                                                x.CustomAttributeType.CustomAttributeTypePurposeID ==
                                                customAttributeTypePurpose.CustomAttributeTypePurposeID &&
                                                x.CustomAttributeType.IsRequired &&
                                                !treatmentBMP
                                                    .CustomAttributes
                                                    .Select(
                                                        y =>
                                                            y.CustomAttributeTypeID)
                                                    .Contains(
                                                        x.CustomAttributeTypeID)) ||
                                            treatmentBMP.CustomAttributes.Any(x =>
                                                x.CustomAttributeType.CustomAttributeTypePurposeID ==
                                                customAttributeTypePurpose.CustomAttributeTypePurposeID &&
                                                x.CustomAttributeType.IsRequired &&
                                                (x.CustomAttributeValues == null ||
                                                 x.CustomAttributeValues.All(
                                                     y => string.IsNullOrEmpty(y.AttributeValue)))
                                            );
            var viewData = new EditAttributesViewData(CurrentPerson, treatmentBMP, customAttributeTypePurpose,
                missingRequiredAttributes);
            return RazorView<EditAttributes, EditAttributesViewData, EditAttributesViewModel>(viewData, viewModel);
        }



        [HttpGet]
        [TreatmentBMPEditFeature]
        public ViewResult EditLocation(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var viewModel = new EditLocationViewModel(treatmentBMP);

            return ViewEditLocation(treatmentBMP, viewModel);
        }

        private ViewResult ViewEditLocation(TreatmentBMP treatmentBMP, EditLocationViewModel viewModel)
        {
            var mapFormID = "treatmentBMPEditLocation";
            var layerGeoJsons = MapInitJson.GetJurisdictionMapLayers();
            var boundingBox = treatmentBMP?.LocationPoint != null
                ? new BoundingBox(treatmentBMP.LocationPoint)
                : BoundingBox.MakeNewDefaultBoundingBox();
            var mapInitJson =
                new MapInitJson($"BMP_{CurrentPerson.PersonID}_EditBMP", 10, layerGeoJsons, boundingBox, false)
                {
                    AllowFullScreen = false
                };
            var viewData = new EditLocationViewData(CurrentPerson, treatmentBMP,mapInitJson, mapFormID);

            return RazorView<EditLocation, EditLocationViewData, EditLocationViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [TreatmentBMPEditFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditLocation(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey, EditLocationViewModel viewModel)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;

            if (!ModelState.IsValid)
            {
                return ViewEditLocation(treatmentBMP, viewModel);
            }

            viewModel.UpdateModel(treatmentBMP, CurrentPerson);

            SetMessageForDisplay("Successfully updated Treatment BMP Location.");

            return new RedirectResult(
                SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(x =>
                    x.Detail(treatmentBMPPrimaryKey)));
        }
    }
}
