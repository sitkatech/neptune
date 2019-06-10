/*-----------------------------------------------------------------------
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

using System;
using GeoJSON.Net.Feature;
using LtInfo.Common;
using LtInfo.Common.GdalOgr;
using LtInfo.Common.Models;
using LtInfo.Common.Mvc;
using LtInfo.Common.MvcResults;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.Shared;
using Neptune.Web.Views.TreatmentBMP;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using LtInfo.Common.DhtmlWrappers;
using Neptune.Web.Views.TreatmentBMPAssessmentObservationType;
using Detail = Neptune.Web.Views.TreatmentBMP.Detail;
using DetailViewData = Neptune.Web.Views.TreatmentBMP.DetailViewData;
using Edit = Neptune.Web.Views.TreatmentBMP.Edit;
using EditViewData = Neptune.Web.Views.TreatmentBMP.EditViewData;
using EditViewModel = Neptune.Web.Views.TreatmentBMP.EditViewModel;
using TreatmentBMPAssessmentSummary = Neptune.Web.Models.TreatmentBMPAssessmentSummary;

namespace Neptune.Web.Controllers
{
    public class TreatmentBMPController : NeptuneBaseController
    {
        [NeptuneViewFeature]
        public ViewResult FindABMP()
        {
            var treatmentBmps = HttpRequestStorage.DatabaseEntities.TreatmentBMPs.ToList()
                .Where(x => x.CanView(CurrentPerson)).ToList();
            var mapInitJson = new SearchMapInitJson("StormwaterIndexMap",
                StormwaterMapInitJson.MakeTreatmentBMPLayerGeoJson(treatmentBmps, false, false));
            var jurisdictionLayerGeoJson =
                mapInitJson.Layers.Single(x => x.LayerName == MapInitJsonHelpers.CountyCityLayerName);
            jurisdictionLayerGeoJson.LayerOpacity = 0;
            jurisdictionLayerGeoJson.LayerInitialVisibility = LayerInitialVisibility.Show;
            var treatmentBMPTypeSimples = treatmentBmps.GroupBy(x => x.TreatmentBMPType)
                .Select(x => new TreatmentBMPTypeSimple(x.Key)).ToList();
            var neptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.FindABMP);
            var viewData = new FindABMPViewData(CurrentPerson, mapInitJson, neptunePage, treatmentBmps,
                treatmentBMPTypeSimples);
            return RazorView<FindABMP, FindABMPViewData>(viewData);
        }

        [NeptuneViewFeature]
        public ViewResult Index()
        {
            var neptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.TreatmentBMP);
            var treatmentBmpsCurrentUserCanSee = HttpRequestStorage.DatabaseEntities.TreatmentBMPs.ToList()
                .Where(x => x.CanView(CurrentPerson)).ToList();
            var treatmentBmpsInExportCount = treatmentBmpsCurrentUserCanSee.Count;
            var featureClassesInExportCount =
                treatmentBmpsCurrentUserCanSee.Select(x => x.TreatmentBMPTypeID).Distinct().Count() + 1;
            var bulkBMPUploadUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(x => x.UploadBMPs());
            var viewData = new IndexViewData(CurrentPerson, neptunePage, treatmentBmpsInExportCount,
                featureClassesInExportCount, bulkBMPUploadUrl);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [NeptuneViewFeature]
        public GridJsonNetJObjectResult<TreatmentBMP> TreatmentBMPGridJsonData()
        {
            // ReSharper disable once InconsistentNaming
            var treatmentBMPs = GetTreatmentBmpsAndGridSpec(out var gridSpec, CurrentPerson);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<TreatmentBMP>(treatmentBMPs, gridSpec);
            return gridJsonNetJObjectResult;
        }

        private List<TreatmentBMP> GetTreatmentBmpsAndGridSpec(out TreatmentBMPGridSpec gridSpec, Person currentPerson)
        {
            var showDelete = new JurisdictionManageFeature().HasPermissionByPerson(currentPerson);
            var showEdit = new JurisdictionEditFeature().HasPermissionByPerson(currentPerson);
            gridSpec = new TreatmentBMPGridSpec(currentPerson, showDelete, showEdit);
            return HttpRequestStorage.DatabaseEntities.TreatmentBMPs
                .Include(x => x.TreatmentBMPBenchmarkAndThresholds)
                .Include(x => x.MaintenanceRecords)
                .Include(x => x.TreatmentBMPType)
                .Include(x => x.TreatmentBMPAssessments)
                .Include(x => x.TreatmentBMPAssessments.Select(y => y.FieldVisit))
                .Include(x => x.WaterQualityManagementPlan)
                .ToList().Where(x => x.CanView(CurrentPerson)).ToList();
        }

        [NeptuneViewFeature]
        public ViewResult TreatmentBMPAssessmentSummary()
        {
            var neptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.TreatmentBMPAssessment);
            var viewData = new TreatmentBMPAssessmentSummaryViewData(CurrentPerson, neptunePage);
            return RazorView<Views.TreatmentBMP.TreatmentBMPAssessmentSummary, TreatmentBMPAssessmentSummaryViewData>(
                viewData);
        }

        [NeptuneViewFeature]
        public GridJsonNetJObjectResult<TreatmentBMPAssessmentSummary> TreatmentBMPAssessmentSummaryGridJsonData()
        {
            var vMostRecentTreatmentBMPAssessments =
                GetTreatmentBMPAssessmentSummariesAndGridSpec(out GridSpec<TreatmentBMPAssessmentSummary> gridSpec,
                    CurrentPerson);
            var gridJsonNetJObjectResult =
                new GridJsonNetJObjectResult<TreatmentBMPAssessmentSummary>(vMostRecentTreatmentBMPAssessments,
                    gridSpec);
            return gridJsonNetJObjectResult;
        }

        private List<TreatmentBMPAssessmentSummary> GetTreatmentBMPAssessmentSummariesAndGridSpec(
            out GridSpec<TreatmentBMPAssessmentSummary> gridSpec, Person currentPerson)
        {
            gridSpec = new TreatmentBMPAssessmentSummaryGridSpec();
            var stormwaterJurisdictionIDsCurrentUserCanEdit = currentPerson.GetStormwaterJurisdictionsPersonCanEdit()
                .Select(y => y.StormwaterJurisdictionID).ToList();

            var vMostRecentTreatmentBMPAssessments =
                HttpRequestStorage.DatabaseEntities.vMostRecentTreatmentBMPAssessments.Where(x =>
                    stormwaterJurisdictionIDsCurrentUserCanEdit.Contains(x.StormwaterJurisdictionID)).ToList();
            var vMostRecentTreatmentBMPAssessmentIDs =
                vMostRecentTreatmentBMPAssessments.Select(y => y.LastAssessmentID).ToList();

            var treatmentBMPObservations = HttpRequestStorage.DatabaseEntities.TreatmentBMPObservations.Where(x =>
                vMostRecentTreatmentBMPAssessmentIDs
                    .Contains(x.TreatmentBMPAssessmentID)).ToList().Where(x=> x.TreatmentBMPAssessmentObservationType.ObservationTypeSpecification.ObservationTypeCollectionMethodID == ObservationTypeCollectionMethod.PassFail.ObservationTypeCollectionMethodID && x.GetPassFailObservationData().SingleValueObservations.Any(y=> Convert.ToBoolean(y.ObservationValue) == false));


            var notes = treatmentBMPObservations.ToList().Select(x =>
            {
                var notems = JsonConvert.DeserializeObject<DiscreteObservationSchema>(x.ObservationData)
                    .SingleValueObservations.Select(y => y.Notes);

                var @join = String.Join(". ", notems);

                if (string.IsNullOrWhiteSpace(@join))
                {
                    @join = "[None provided]";
                }

                return new
                {
                    x.TreatmentBMPAssessmentID,
                    Notes =
                        $"{x.TreatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeName} Failure Notes: {@join}"
                };
            });

            var treatmentBMPAssessmentSummaries = vMostRecentTreatmentBMPAssessments.GroupJoin(notes, 
                x => x.LastAssessmentID,
                y => y.TreatmentBMPAssessmentID,
                (x,y) => new Models.TreatmentBMPAssessmentSummary {AssessmentSummary = x, Notes = string.Join(";", y.Select(z=>z.Notes))});

            return treatmentBMPAssessmentSummaries.OrderByDescending(x=>x.AssessmentSummary.LastAssessmentDate).ToList();
        }

        [TreatmentBMPViewFeature]
        public ViewResult Detail(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;

            var mapInitJson = new TreatmentBMPDetailMapInitJson("StormwaterDetailMap", treatmentBMP.LocationPoint);
            mapInitJson.Layers.Add(
                StormwaterMapInitJson.MakeTreatmentBMPLayerGeoJson(new[] {treatmentBMP}, false, true));
            if (treatmentBMP.Delineation?.DelineationGeometry != null)
            {
                mapInitJson.DelineationLayer =
                    StormwaterMapInitJson.MakeTreatmentBMPDelineationLayerGeoJson(treatmentBMP);
            }

            var carouselImages = treatmentBMP.TreatmentBMPImages.OrderBy(x => x.TreatmentBMPImageID).ToList();
            var imageCarouselViewData = new ImageCarouselViewData(carouselImages, 400);
            var verifiedUnverifiedUrl =
                SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(
                    x => x.VerifyInventory(treatmentBMPPrimaryKey));

            var viewData = new DetailViewData(CurrentPerson, treatmentBMP, mapInitJson, imageCarouselViewData,
                verifiedUnverifiedUrl);
            return RazorView<Detail, DetailViewData>(viewData);
        }

        [HttpGet]
        [JurisdictionEditFeature]
        public ViewResult New()
        {
            var viewModel = new NewViewModel();
            return ViewNew(viewModel);
        }

        [HttpPost]
        [JurisdictionEditFeature]
        public ActionResult New(NewViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewNew(viewModel);
            }

            var inventoryIsVerified = false;
            var treatmentBMP = new TreatmentBMP(string.Empty, viewModel.TreatmentBMPTypeID,
                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                viewModel.StormwaterJurisdictionID, CurrentPerson.OrganizationID, inventoryIsVerified,
                viewModel.TrashCaptureStatusTypeID.GetValueOrDefault(),
                viewModel.SizingBasisTypeID.GetValueOrDefault());
            viewModel.UpdateModel(treatmentBMP, CurrentPerson);
            HttpRequestStorage.DatabaseEntities.TreatmentBMPs.Add(treatmentBMP);
            HttpRequestStorage.DatabaseEntities.SaveChanges(CurrentPerson);

            SetMessageForDisplay("Treatment BMP successfully created.");

            return RedirectToAction(new SitkaRoute<TreatmentBMPController>(c => c.Detail(treatmentBMP.PrimaryKey)));
        }

        private ViewResult ViewNew(NewViewModel viewModel)
        {
            var treatmentBMP = ModelObjectHelpers.IsRealPrimaryKeyValue(viewModel.TreatmentBMPID)
                ? HttpRequestStorage.DatabaseEntities.TreatmentBMPs.GetTreatmentBMP(viewModel.TreatmentBMPID)
                : null;
            var stormwaterJurisdictions = HttpRequestStorage.DatabaseEntities.StormwaterJurisdictions
                .ToList()
                .Where(x => CurrentPerson.IsAssignedToStormwaterJurisdiction(x))
                .ToList();
            var treatmentBMPTypes = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.ToList();
            var organizations = HttpRequestStorage.DatabaseEntities.Organizations.ToList();
            var layerGeoJsons = MapInitJsonHelpers.GetJurisdictionMapLayers().ToList();
            var boundingBox = treatmentBMP?.LocationPoint != null
                ? new BoundingBox(treatmentBMP.LocationPoint)
                : BoundingBox.MakeNewDefaultBoundingBox();
            var mapInitJson =
                new MapInitJson($"BMP_{CurrentPerson.PersonID}_EditBMP", 10, layerGeoJsons, boundingBox, false)
                {
                    AllowFullScreen = false
                };
            var editLocationViewData = new Views.Shared.Location.EditLocationViewData(CurrentPerson, treatmentBMP,
                mapInitJson, "treatmentBMPLocation");
            var treatmentBMPStormwaterJurisdictionIDs = treatmentBMP != null
                ? new List<int> {treatmentBMP.StormwaterJurisdictionID}
                : stormwaterJurisdictions.Select(x => x.StormwaterJurisdictionID).ToList();
            var waterQualityManagementPlans = HttpRequestStorage.DatabaseEntities.WaterQualityManagementPlans
                .Where(x => treatmentBMPStormwaterJurisdictionIDs.Contains(x.StormwaterJurisdictionID)).ToList();

            if (ModelObjectHelpers.IsRealPrimaryKeyValue(viewModel.StormwaterJurisdictionID))
            {
                stormwaterJurisdictions.Add(
                    HttpRequestStorage.DatabaseEntities.StormwaterJurisdictions.GetStormwaterJurisdiction(
                        viewModel.StormwaterJurisdictionID));
                stormwaterJurisdictions = stormwaterJurisdictions.Distinct().ToList();
            }

            var viewData = new NewViewData(CurrentPerson, treatmentBMP, stormwaterJurisdictions, treatmentBMPTypes,
                organizations, editLocationViewData, waterQualityManagementPlans, TreatmentBMPLifespanType.All,
                TrashCaptureStatusType.All, SizingBasisType.All);
            return RazorView<New, NewViewData, NewViewModel>(viewData, viewModel);
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
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel);
            }

            var refreshTGUs = treatmentBMP.Delineation != null &&
                              (viewModel.TrashCaptureStatusTypeID != treatmentBMP.TrashCaptureStatusTypeID ||
                               viewModel.TrashCaptureEffectiveness != treatmentBMP.TrashCaptureEffectiveness);

            treatmentBMP.MarkInventoryAsProvisionalIfNonManager(CurrentPerson);
            viewModel.UpdateModel(treatmentBMP, CurrentPerson);

            if (refreshTGUs)
            {
                treatmentBMP.Delineation.UpdateTrashGeneratingUnits(CurrentPerson);
            }

            SetMessageForDisplay("Treatment BMP successfully saved.");

            return RedirectToAction(new SitkaRoute<TreatmentBMPController>(c => c.Detail(treatmentBMP.PrimaryKey)));
        }

        private ViewResult ViewEdit(EditViewModel viewModel)
        {
            var treatmentBMP = ModelObjectHelpers.IsRealPrimaryKeyValue(viewModel.TreatmentBMPID)
                ? HttpRequestStorage.DatabaseEntities.TreatmentBMPs.GetTreatmentBMP(viewModel.TreatmentBMPID)
                : null;
            var stormwaterJurisdictions = HttpRequestStorage.DatabaseEntities.StormwaterJurisdictions
                .ToList()
                .Where(x => CurrentPerson.IsAssignedToStormwaterJurisdiction(x))
                .ToList();
            var treatmentBMPTypes = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.ToList();
            var organizations = HttpRequestStorage.DatabaseEntities.Organizations.ToList();
            var waterQualityManagementPlans = HttpRequestStorage.DatabaseEntities.WaterQualityManagementPlans
                .Where(x => x.StormwaterJurisdictionID == treatmentBMP.StormwaterJurisdictionID)
                .ToList();

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

            var viewData = new EditViewData(CurrentPerson, treatmentBMP, stormwaterJurisdictions, treatmentBMPTypes,
                organizations, waterQualityManagementPlans, TreatmentBMPLifespanType.All, TrashCaptureStatusType.All,
                SizingBasisType.All);
            return RazorView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [TreatmentBMPManageFeature]
        public PartialViewResult VerifyInventory(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(treatmentBMP.TreatmentBMPID);
            return ViewVerifyInventoryTreatmentBMP(treatmentBMP, viewModel);
        }

        [HttpPost]
        [TreatmentBMPManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult VerifyInventory(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey,
            ConfirmDialogFormViewModel viewModel)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewVerifyInventoryTreatmentBMP(treatmentBMP, viewModel);
            }

            if (!treatmentBMP.InventoryIsVerified)
            {
                treatmentBMP.MarkAsVerified(CurrentPerson);
            }
            else
            {
                treatmentBMP.InventoryIsVerified = false;
            }

            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewVerifyInventoryTreatmentBMP(TreatmentBMP treatmentBMP,
            ConfirmDialogFormViewModel viewModel)
        {
            var action = treatmentBMP.InventoryIsVerified ? "provisional" : "verified";
            var viewData = new ConfirmDialogFormViewData(
                $"Are you sure you want to mark BMP record, '{treatmentBMP.TreatmentBMPName}', as '{action}'?");
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData,
                viewModel);
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

            var treatmentBMPTreatmentBMPName = treatmentBMP.TreatmentBMPName;
            var treatmentBMPDelineation = treatmentBMP.Delineation;
            treatmentBMPDelineation?.DelineationGeometry.UpdateTrashGeneratingUnitsAfterDelete(CurrentPerson);

            treatmentBMP.DeleteFull(HttpRequestStorage.DatabaseEntities);
            HttpRequestStorage.DatabaseEntities.SaveChanges();

            treatmentBMPDelineation?.Delete(HttpRequestStorage.DatabaseEntities);
            HttpRequestStorage.DatabaseEntities.SaveChanges();

            SetMessageForDisplay($"Successfully deleted the Treatment BMP {treatmentBMPTreatmentBMPName}");
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewDeleteTreatmentBMP(TreatmentBMP treatmentBMP,
            ConfirmDialogFormViewModel viewModel)
        {
            var viewData =
                new ConfirmDialogFormViewData(
                    $"Are you sure you want to delete the '{treatmentBMP.TreatmentBMPName}' treatment BMP?");
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData,
                viewModel);
        }

        [NeptuneViewFeature]
        public PartialViewResult SummaryForMap(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var viewData = new SummaryForMapViewData(CurrentPerson, treatmentBMP);
            return RazorPartialView<SummaryForMap, SummaryForMapViewData>(viewData);
        }

        [NeptuneViewFeature]
        [HttpGet]
        public ContentResult FindByName()
        {
            return new ContentResult();
        }

        [NeptuneViewFeature]
        [HttpPost]
        public JsonResult FindByName(FindABMPViewModel viewModel)
        {
            var searchString = viewModel.SearchTerm.Trim();
            var treatmentBMPTypeIDs = viewModel.TreatmentBMPTypeIDs ?? new List<int>();
            // ReSharper disable once InconsistentNaming
            var allTreatmentBMPsMatchingSearchString =
                HttpRequestStorage.DatabaseEntities.TreatmentBMPs.Where(
                    x => treatmentBMPTypeIDs.Contains(x.TreatmentBMPTypeID) &&
                         x.TreatmentBMPName.Contains(searchString)).ToList();

            var listItems = allTreatmentBMPsMatchingSearchString.OrderBy(x => x.TreatmentBMPName).Take(20).Select(bmp =>
            {
                var treatmentBMPMapSummaryData = new SearchMapSummaryData(bmp.GetMapSummaryUrl(), bmp.LocationPoint,
                    bmp.LocationPoint.YCoordinate.GetValueOrDefault(),
                    bmp.LocationPoint.XCoordinate.GetValueOrDefault(),
                    bmp.TreatmentBMPID); // X/YCoordinate will never be null
                var listItem = new ListItem(bmp.TreatmentBMPName,
                    JsonConvert.SerializeObject(treatmentBMPMapSummaryData));
                return listItem;
            }).ToList();

            return Json(listItems, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [TreatmentBMPEditFeature]
        public ViewResult EditAttributes(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey,
            CustomAttributeTypePurposePrimaryKey customAttributeTypePurposePrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var customAttributeTypePurpose = customAttributeTypePurposePrimaryKey.EntityObject;
            var viewModel = new EditAttributesViewModel(treatmentBMP, customAttributeTypePurpose);
            return ViewEditAttributes(viewModel, treatmentBMP, customAttributeTypePurpose);
        }

        [HttpPost]
        [TreatmentBMPEditFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditAttributes(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey,
            CustomAttributeTypePurposePrimaryKey customAttributeTypePurposePrimaryKey,
            EditAttributesViewModel viewModel)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var customAttributeTypePurpose = customAttributeTypePurposePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEditAttributes(viewModel, treatmentBMP, customAttributeTypePurpose);
            }

            var allCustomAttributeTypes = HttpRequestStorage.DatabaseEntities.CustomAttributeTypes.ToList();
            viewModel.UpdateModel(treatmentBMP, CurrentPerson, customAttributeTypePurpose, allCustomAttributeTypes);
            treatmentBMP.MarkInventoryAsProvisionalIfNonManager(CurrentPerson);
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
            var layerGeoJsons = MapInitJsonHelpers.GetJurisdictionMapLayers().ToList();
            var boundingBox = treatmentBMP?.LocationPoint != null
                ? new BoundingBox(treatmentBMP.LocationPoint)
                : BoundingBox.MakeNewDefaultBoundingBox();
            var mapInitJson =
                new MapInitJson($"BMP_{CurrentPerson.PersonID}_EditBMP", 10, layerGeoJsons, boundingBox, false)
                {
                    AllowFullScreen = false
                };
            var viewData = new EditLocationViewData(CurrentPerson, treatmentBMP, mapInitJson, mapFormID);

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

            treatmentBMP.MarkInventoryAsProvisionalIfNonManager(CurrentPerson);

            viewModel.UpdateModel(treatmentBMP, CurrentPerson);

            SetMessageForDisplay("Successfully updated Treatment BMP Location.");

            return new RedirectResult(
                SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(x =>
                    x.Detail(treatmentBMPPrimaryKey)));
        }

        [NeptuneViewFeature]
        public ContentResult MapPopup(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var properties = new Dictionary<string, string>
            {
                {"Name", treatmentBMP.GetDisplayNameAsUrl().ToString()},
                {
                    $"{FieldDefinition.Jurisdiction.GetFieldDefinitionLabel()}",
                    treatmentBMP.StormwaterJurisdiction.GetDisplayNameAsDetailUrl().ToString()
                },
                {"Type", treatmentBMP.TreatmentBMPType.TreatmentBMPTypeName},
            };
            var dl = new TagBuilder("dl")
            {
                InnerHtml = string.Join("", properties.Select(x =>
                {
                    var dt = new TagBuilder("dt") {InnerHtml = x.Key};
                    var dd = new TagBuilder("dd") {InnerHtml = x.Value};
                    return $"{dt}{dd}";
                }).ToList())
            };
            return Content(dl.ToString());
        }

        [HttpGet]
        [NeptuneViewFeature]
        public FileResult BMPInventoryExport()
        {
            var ogr2OgrCommandLineRunner = new Ogr2OgrCommandLineRunner(NeptuneWebConfiguration.Ogr2OgrExecutable,
                Ogr2OgrCommandLineRunner.DefaultCoordinateSystemId,
                NeptuneWebConfiguration.HttpRuntimeExecutionTimeout.TotalMilliseconds);
            var treatmentBmps = HttpRequestStorage.DatabaseEntities.TreatmentBMPs
                .Include(x => x.TreatmentBMPType).Include(x => x.TreatmentBMPType.TreatmentBMPTypeCustomAttributeTypes)
                .Include(x => x.TreatmentBMPAssessments).Include(x => x.WaterQualityManagementPlan)
                .Include(x => x.CustomAttributes)
                .ToList().Where(x => x.CanView(CurrentPerson)).ToList();

            using (var workingDirectory = new DisposableTempDirectory())
            {
                FeatureCollection allTreatmentBMPsFeatureCollection = treatmentBmps.ToExportGeoJsonFeatureCollection();
                var outputPath = Path.Combine(workingDirectory.DirectoryInfo.FullName, "AllTreatmentBMPs");
                CreateEsriShapefileFromFeatureCollection(allTreatmentBMPsFeatureCollection, ogr2OgrCommandLineRunner,
                    "AllTreatmentBMPs", outputPath, false);

                foreach (var grouping in treatmentBmps.GroupBy(x => x.TreatmentBMPType))
                {
                    string outputLayerName =
                        Ogr2OgrCommandLineRunner.SanitizeStringForGdb(grouping.Key.TreatmentBMPTypeName);
                    var outputPathForLayer =
                        Path.Combine(workingDirectory.DirectoryInfo.FullName, outputLayerName);
                    var subsetTreatmentBMPsFeatureCollection = grouping.ToExportGeoJsonFeatureCollection(grouping.Key);
                    CreateEsriShapefileFromFeatureCollection(subsetTreatmentBMPsFeatureCollection,
                        ogr2OgrCommandLineRunner, outputLayerName, outputPathForLayer, false);
                }

                using (var zipFile = DisposableTempFile.MakeDisposableTempFileEndingIn(".zip"))
                {
                    ZipFile.CreateFromDirectory(workingDirectory.DirectoryInfo.FullName, zipFile.FileInfo.FullName);
                    var fileStream = zipFile.FileInfo.OpenRead();
                    var bytes = fileStream.ReadFully();
                    fileStream.Close();
                    fileStream.Dispose();
                    return File(bytes, "application/zip");
                }
            }
        }

        [HttpGet]
        [NeptuneAdminFeature]
        public ViewResult UploadBMPs()
        {
            var viewModel = new UploadTreatmentBMPsViewModel();
            var errorList = new List<string>();
            return ViewUploadBMPs(viewModel, errorList);
        }


        [HttpPost]
        [NeptuneAdminFeature]
        public ActionResult UploadBMPs(UploadTreatmentBMPsViewModel viewModel)
        {
            var uploadCSV = viewModel.UploadCSV;
            var bmpType = viewModel.BMPType;

            var treatmentBMPs = TreatmentBMPCsvParserHelper.CSVUpload(uploadCSV.InputStream, bmpType, out var errorList,
                out var customAttributes, out var customAttributeValues);

            if (errorList.Count != 0)
            {
                return ViewUploadBMPs(viewModel, errorList);
            }

            HttpRequestStorage.DatabaseEntities.TreatmentBMPs.AddRange(treatmentBMPs);
            HttpRequestStorage.DatabaseEntities.CustomAttributes.AddRange(customAttributes);
            HttpRequestStorage.DatabaseEntities.CustomAttributeValues.AddRange(customAttributeValues);
            HttpRequestStorage.DatabaseEntities.SaveChanges(CurrentPerson);

            SetMessageForDisplay($"Upload Successful: {treatmentBMPs.Count} records added");
            return new RedirectResult(SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(x => x.Index()));
        }

        private ViewResult ViewUploadBMPs(UploadTreatmentBMPsViewModel viewModel, List<string> errorList)
        {
            var bmpTypes = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.OrderBy(x => x.TreatmentBMPTypeName)
                .ToSelectListWithEmptyFirstRow(
                    x => x.TreatmentBMPTypeID.ToString(CultureInfo.InvariantCulture),
                    x => x.TreatmentBMPTypeName.ToString(CultureInfo.InvariantCulture));
            var viewData = new UploadTreatmentBMPsViewData(CurrentPerson, bmpTypes, errorList,
                SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(x => x.UploadBMPs()));
            return RazorView<UploadTreatmentBMPs, UploadTreatmentBMPsViewData, UploadTreatmentBMPsViewModel>(viewData,
                viewModel);
        }

        private static void CreateEsriShapefileFromFeatureCollection(FeatureCollection featureCollection,
            Ogr2OgrCommandLineRunner ogr2OgrCommandLineRunner, string outputShapefileName, string outputPath,
            bool update)
        {
            ogr2OgrCommandLineRunner.ImportGeoJsonToFileGdb(JsonConvert.SerializeObject(featureCollection), outputPath,
                outputShapefileName, update);
        }
    }
}
