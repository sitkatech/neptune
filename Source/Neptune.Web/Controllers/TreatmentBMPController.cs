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

using GeoJSON.Net.Feature;
using Hangfire;
using LtInfo.Common;
using LtInfo.Common.GdalOgr;
using LtInfo.Common.Models;
using LtInfo.Common.Mvc;
using LtInfo.Common.MvcResults;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.ScheduledJobs;
using Neptune.Web.Security;
using Neptune.Web.Views.Shared;
using Neptune.Web.Views.Shared.HRUCharacteristics;
using Neptune.Web.Views.TreatmentBMP;
using Neptune.Web.Views.TreatmentBMPAssessmentObservationType;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Web.Mvc;
using Neptune.Web.Security.Shared;
using Neptune.Web.Views.Shared.ModeledPerformance;
using Detail = Neptune.Web.Views.TreatmentBMP.Detail;
using DetailViewData = Neptune.Web.Views.TreatmentBMP.DetailViewData;
using Edit = Neptune.Web.Views.TreatmentBMP.Edit;
using EditViewData = Neptune.Web.Views.TreatmentBMP.EditViewData;
using EditViewModel = Neptune.Web.Views.TreatmentBMP.EditViewModel;
using ListItem = System.Web.UI.WebControls.ListItem;
using TreatmentBMPAssessmentSummary = Neptune.Web.Models.TreatmentBMPAssessmentSummary;

namespace Neptune.Web.Controllers
{
    public class TreatmentBMPController : NeptuneBaseController
    {
        [AnonymousUnclassifiedFeature]
        public ViewResult FindABMP()
        {
            var treatmentBmps = CurrentPerson.GetTreatmentBmpsPersonCanView();
            var jurisdictions = CurrentPerson.GetStormwaterJurisdictionsPersonCanView().Select(x => new StormwaterJurisdictionSimple(x)).ToList();
            var mapInitJson = new SearchMapInitJson("StormwaterIndexMap",
                StormwaterMapInitJson.MakeTreatmentBMPLayerGeoJson(treatmentBmps, false, false));
            var treatmentBMPTypeSimples = treatmentBmps.GroupBy(x => x.TreatmentBMPType)
                .Select(x => new TreatmentBMPTypeSimple(x.Key)).ToList();
            var neptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.FindABMP);
            var viewData = new FindABMPViewData(CurrentPerson, mapInitJson, neptunePage, treatmentBmps,
                treatmentBMPTypeSimples, jurisdictions);
            return RazorView<FindABMP, FindABMPViewData>(viewData);
        }

        [NeptuneViewFeature]
        public ViewResult Index()
        {
            var neptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.TreatmentBMP);
            var treatmentBmpsCurrentUserCanSee = CurrentPerson.GetTreatmentBmpsPersonCanView();
            var treatmentBmpsInExportCount = treatmentBmpsCurrentUserCanSee.Count;
            var featureClassesInExportCount =
                treatmentBmpsCurrentUserCanSee.Select(x => x.TreatmentBMPTypeID).Distinct().Count() + 1;
            var bulkBMPUploadUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(x => x.UploadBMPs());
            var viewData = new IndexViewData(CurrentPerson, neptunePage, treatmentBmpsInExportCount,
                featureClassesInExportCount, bulkBMPUploadUrl);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [NeptuneViewFeature]
        public GridJsonNetJObjectResult<vTreatmentBMPDetailed> TreatmentBMPGridJsonData()
        {
            var stormwaterJurisdictionIDsPersonCanView = CurrentPerson.GetStormwaterJurisdictionIDsPersonCanView();
            var showDelete = new JurisdictionManageFeature().HasPermissionByPerson(CurrentPerson);
            var showEdit = new JurisdictionEditFeature().HasPermissionByPerson(CurrentPerson);
            var gridSpec = new TreatmentBMPGridSpec(CurrentPerson, showDelete, showEdit);
            var treatmentBMPs = HttpRequestStorage.DatabaseEntities.vTreatmentBMPDetaileds.Where(x => stormwaterJurisdictionIDsPersonCanView.Contains(x.StormwaterJurisdictionID)).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<vTreatmentBMPDetailed>(treatmentBMPs, gridSpec);
            return gridJsonNetJObjectResult;
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
            var gridSpec = new TreatmentBMPAssessmentSummaryGridSpec();
            var stormwaterJurisdictionIDsCurrentUserCanEdit = CurrentPerson.GetStormwaterJurisdictionIDsPersonCanView();

            var vMostRecentTreatmentBMPAssessments1 =
                HttpRequestStorage.DatabaseEntities.vMostRecentTreatmentBMPAssessments.Where(x =>
                    stormwaterJurisdictionIDsCurrentUserCanEdit.Contains(x.StormwaterJurisdictionID)).ToList();
            var vMostRecentTreatmentBMPAssessmentIDs =
                vMostRecentTreatmentBMPAssessments1.Select(y => y.LastAssessmentID).ToList();

            var treatmentBMPObservations = HttpRequestStorage.DatabaseEntities.TreatmentBMPObservations.OrderBy(x=>x.TreatmentBMPAssessment.TreatmentBMPID).ThenBy(x=>x.TreatmentBMPTypeAssessmentObservationType.SortOrder).Where(x =>
                vMostRecentTreatmentBMPAssessmentIDs
                    .Contains(x.TreatmentBMPAssessmentID)).ToList().Where(x=> x.TreatmentBMPAssessmentObservationType.ObservationTypeSpecification.ObservationTypeCollectionMethodID == ObservationTypeCollectionMethod.PassFail.ObservationTypeCollectionMethodID && x.GetPassFailObservationData().SingleValueObservations.Any(y=> Convert.ToBoolean(y.ObservationValue) == false));


            var notes = treatmentBMPObservations.ToList().Select(x =>
            {
                var notesForThisObservation = JsonConvert.DeserializeObject<DiscreteObservationSchema>(x.ObservationData)
                    .SingleValueObservations.Select(y => y.Notes);

                var joinedNotes = String.Join(". ", notesForThisObservation);

                if (string.IsNullOrWhiteSpace(joinedNotes))
                {
                    joinedNotes = "[None provided]";
                }

                return new
                {
                    x.TreatmentBMPAssessmentID,
                    Notes =
                        $"{x.TreatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeName} Failure Notes: {joinedNotes}"
                };
            });

            var treatmentBMPAssessmentSummaries = vMostRecentTreatmentBMPAssessments1.OrderBy(x=>x.TreatmentBMPID).GroupJoin(notes, 
                x => x.LastAssessmentID,
                y => y.TreatmentBMPAssessmentID,
                (x,y) => new TreatmentBMPAssessmentSummary {AssessmentSummary = x, Notes = string.Join("; ", y.Select(z=>z.Notes).OrderBy(z=>z))});
            var vMostRecentTreatmentBMPAssessments =
                treatmentBMPAssessmentSummaries.OrderByDescending(x=>x.AssessmentSummary.LastAssessmentDate).ToList();
            var gridJsonNetJObjectResult =
                new GridJsonNetJObjectResult<TreatmentBMPAssessmentSummary>(vMostRecentTreatmentBMPAssessments,
                    gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpGet]
        [TreatmentBMPViewFeature]
        public ViewResult Detail(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var mapServiceUrl = NeptuneWebConfiguration.ParcelMapServiceUrl;
            var mapInitJson = new TreatmentBMPDetailMapInitJson("StormwaterDetailMap", treatmentBMP.LocationPoint4326);
            mapInitJson.Layers.Add(
                StormwaterMapInitJson.MakeTreatmentBMPLayerGeoJson(new[] {treatmentBMP}, false, true));
            if (treatmentBMP.Delineation?.DelineationGeometry != null || treatmentBMP.UpstreamBMP?.Delineation?.DelineationGeometry != null)
            {
                mapInitJson.DelineationLayer =
                    StormwaterMapInitJson.MakeTreatmentBMPDelineationLayerGeoJson(treatmentBMP.UpstreamBMP ?? treatmentBMP);
            }

            var carouselImages = treatmentBMP.TreatmentBMPImages.OrderBy(x => x.TreatmentBMPImageID).ToList();
            var imageCarouselViewData = new ImageCarouselViewData(carouselImages, 400);
            var verifiedUnverifiedUrl =
                SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(
                    x => x.VerifyInventory(treatmentBMPPrimaryKey));

            IHaveHRUCharacteristics entityWithHRUCharacteristics = treatmentBMP.UpstreamBMP ?? treatmentBMP;

            var modeledBMPPerformanceViewData = new ModeledPerformanceViewData(treatmentBMP, CurrentPerson);
            var viewData = new DetailViewData(CurrentPerson, treatmentBMP, mapInitJson, imageCarouselViewData,
                verifiedUnverifiedUrl,
                new HRUCharacteristicsViewData(entityWithHRUCharacteristics,
                    entityWithHRUCharacteristics.GetHRUCharacteristics().ToList()), mapServiceUrl, modeledBMPPerformanceViewData);
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
            var stormwaterJurisdictions = CurrentPerson.GetStormwaterJurisdictionsPersonCanView();
            var treatmentBMPTypes = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.ToList();
            var organizations = HttpRequestStorage.DatabaseEntities.Organizations.ToList();
            var layerGeoJsons = MapInitJsonHelpers.GetJurisdictionMapLayers().ToList();
            var boundingBox = treatmentBMP?.LocationPoint != null
                ? new BoundingBox(treatmentBMP.LocationPoint4326)
                : BoundingBox.GetBoundingBox(stormwaterJurisdictions);
            var zoomLevel = CurrentPerson.IsAdministrator() ? MapInitJson.DefaultZoomLevel : MapInitJson.DefaultZoomLevel + 2;

            var mapInitJson =
                new MapInitJson($"BMP_{CurrentPerson.PersonID}_EditBMP", zoomLevel, layerGeoJsons, boundingBox, false)
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

            treatmentBMP.MarkInventoryAsProvisionalIfNonManager(CurrentPerson);
            viewModel.UpdateModel(treatmentBMP, CurrentPerson);


            SetMessageForDisplay("Treatment BMP successfully saved.");

            return RedirectToAction(new SitkaRoute<TreatmentBMPController>(c => c.Detail(treatmentBMP.PrimaryKey)));
        }

        private ViewResult ViewEdit(EditViewModel viewModel)
        {
            var treatmentBMP = ModelObjectHelpers.IsRealPrimaryKeyValue(viewModel.TreatmentBMPID)
                ? HttpRequestStorage.DatabaseEntities.TreatmentBMPs.GetTreatmentBMP(viewModel.TreatmentBMPID)
                : null;
            var stormwaterJurisdictions = CurrentPerson.GetStormwaterJurisdictionsPersonCanView();
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
        [TreatmentBMPEditFeature]
        public PartialViewResult EditUpstreamBMP(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var viewModel = new EditUpstreamBMPViewModel(treatmentBMP);
            
            return ViewEditUpstreamBMP(treatmentBMP, viewModel);
        }

        [HttpPost]
        [TreatmentBMPEditFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditUpstreamBMP(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey,
            EditUpstreamBMPViewModel viewModel)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEditUpstreamBMP(treatmentBMP, viewModel);
            }

            viewModel.UpdateModel(treatmentBMP);

            treatmentBMP.Delineation?.DeleteDelineation(HttpRequestStorage.DatabaseEntities);

            // need to re-execute the Nereid model for this node since source of run-off was changed.
            NereidUtilities.MarkTreatmentBMPDirty(treatmentBMP, HttpRequestStorage.DatabaseEntities);

            SetMessageForDisplay("Upstream BMP successfully updated");
            return new ModalDialogFormJsonResult(SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(x => x.Detail(treatmentBMP)));
        }

        [HttpPost]
        [TreatmentBMPEditFeature]
        public ActionResult RemoveUpstreamBMP(TreatmentBMPPrimaryKey treatmentBmpPrimaryKey)
        {
            var treatmentBMP = treatmentBmpPrimaryKey.EntityObject;

            treatmentBMP.RemoveUpstreamBMP();
            
            HttpRequestStorage.DatabaseEntities.SaveChanges();
            SetMessageForDisplay("Upstream BMP successfully removed");

            // need to re-execute the Nereid model here since source of run-off was removed.
            NereidUtilities.MarkTreatmentBMPDirty(treatmentBMP, HttpRequestStorage.DatabaseEntities);

            return RedirectToAction(new SitkaRoute<TreatmentBMPController>(c => c.Detail(treatmentBMP.PrimaryKey)));
        }


        private PartialViewResult ViewEditUpstreamBMP(TreatmentBMP treatmentBMP, EditUpstreamBMPViewModel viewModel)
        {
            var treatmentBMPs = treatmentBMP.GetRegionalSubbasin().GetTreatmentBMPs()
                .Where(x => x.TreatmentBMPID != treatmentBMP.TreatmentBMPID);

            var treatmentBMPSelectList = treatmentBMPs.ToSelectListWithDisabledEmptyFirstRow(
                    x => x.TreatmentBMPID.ToString(CultureInfo.InvariantCulture), x => x.TreatmentBMPName,
                    "Select an Upstream BMP");

            var viewData = new EditUpstreamBMPViewData(treatmentBMPSelectList);

            return RazorPartialView<EditUpstreamBMP, EditUpstreamBMPViewData, EditUpstreamBMPViewModel>(viewData, viewModel);
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
        [TreatmentBMPManageFeature]
        public PartialViewResult ConvertTreatmentBMPType(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var viewModel = new ConvertTreatmentBMPTypeViewModel();
            return ViewConvertTreatmentBMPTypeTreatmentBMP(treatmentBMP, viewModel);
        }

        [HttpPost]
        [TreatmentBMPManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult ConvertTreatmentBMPType(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey,
            ConvertTreatmentBMPTypeViewModel viewModel)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewConvertTreatmentBMPTypeTreatmentBMP(treatmentBMP, viewModel);
            }

            // delete any field visit, assessment, benchmark, and maintenance records
            foreach (var fieldVisit in treatmentBMP.FieldVisits.ToList())
            {
                fieldVisit.DeleteFull(HttpRequestStorage.DatabaseEntities);
            }

            foreach (var treatmentBMPBenchmarkAndThreshold in treatmentBMP.TreatmentBMPBenchmarkAndThresholds.ToList())
            {
                treatmentBMPBenchmarkAndThreshold.DeleteFull(HttpRequestStorage.DatabaseEntities);
            }

            treatmentBMP.TreatmentBMPBenchmarkAndThresholds = null;

            var newTreatmentBMPType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.GetTreatmentBMPType(viewModel.TreatmentBMPTypeID.GetValueOrDefault());
            var validCustomAttributeTypes = newTreatmentBMPType.TreatmentBMPTypeCustomAttributeTypes.ToList();

            // we need to clone the attributes instead of simply changing the bmp type and treatmentbmptypecustomattributetype ids
            var customAttributesCloned = new List<CustomAttribute>();
            foreach (var customAttribute in treatmentBMP.CustomAttributes.Where(z => validCustomAttributeTypes.Select(y => y.CustomAttributeTypeID).Contains(z.CustomAttributeTypeID))
                .ToList())
            {
                var treatmentBMPTypeCustomAttributeType = newTreatmentBMPType.TreatmentBMPTypeCustomAttributeTypes.Single(c => c.CustomAttributeTypeID == customAttribute.CustomAttributeTypeID);
                var customAttributeCloned = new CustomAttribute(treatmentBMP.TreatmentBMPID, treatmentBMPTypeCustomAttributeType.TreatmentBMPTypeCustomAttributeTypeID, treatmentBMPTypeCustomAttributeType.TreatmentBMPTypeID, treatmentBMPTypeCustomAttributeType.CustomAttributeTypeID);
                customAttributesCloned.Add(customAttributeCloned);
                foreach (var value in customAttribute.CustomAttributeValues)
                {
                    // ReSharper disable once UnusedVariable
                    // (never used, but creating it with this constructor adds it to the CustomAttributeValue collection of customAttributeCloned automatically, so it's done its job)
                    var customAttributeValue = new CustomAttributeValue(customAttributeCloned, value.AttributeValue);
                    
                }
            }

            // delete any custom attributes that are not valid for the new treatment bmp type
            foreach (var customAttribute in treatmentBMP.CustomAttributes.ToList())
            {
                customAttribute.DeleteFull(HttpRequestStorage.DatabaseEntities);
            }
            // force a save changes to clear out fk references when we switch the bmp type
            HttpRequestStorage.DatabaseEntities.SaveChanges();

            // now add the cloned custom attributes to the db context
            HttpRequestStorage.DatabaseEntities.CustomAttributes.AddRange(customAttributesCloned);
            treatmentBMP.TreatmentBMPTypeID = viewModel.TreatmentBMPTypeID.GetValueOrDefault();

            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewConvertTreatmentBMPTypeTreatmentBMP(TreatmentBMP treatmentBMP,
            ConvertTreatmentBMPTypeViewModel viewModel)
        {
            var treatmentBMPTypes = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Where(x => x.TreatmentBMPTypeID != treatmentBMP.TreatmentBMPTypeID).ToList();
            var viewData = new ConvertTreatmentBMPTypeViewData(treatmentBMP, treatmentBMPTypes);
            return RazorPartialView<ConvertTreatmentBMPType, ConvertTreatmentBMPTypeViewData, ConvertTreatmentBMPTypeViewModel>(viewData, viewModel);
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
            var delineationGeometry = treatmentBMP.Delineation?.DelineationGeometry;
            var isDelineationDistributed = treatmentBMP.Delineation?.DelineationType == DelineationType.Distributed;

            NereidUtilities.MarkDownstreamNodeDirty(treatmentBMP, HttpRequestStorage.DatabaseEntities);

            if (!ModelState.IsValid)
            {
                return ViewDeleteTreatmentBMP(treatmentBMP, viewModel);
            }

            var treatmentBMPTreatmentBMPName = treatmentBMP.TreatmentBMPName;

            foreach (var downstreamBMP in treatmentBMP.TreatmentBMPsWhereYouAreTheUpstreamBMP)
            {
                downstreamBMP.UpstreamBMPID = null;
            }
            HttpRequestStorage.DatabaseEntities.SaveChanges();
            
            // todo: The code-generated DeleteFull is brittle since it touches the LGU system.
            // We should write a more finely-grained delete that deletes delineations via the
            // pattern established in DelineationController
            treatmentBMP.DeleteFull(HttpRequestStorage.DatabaseEntities);
            HttpRequestStorage.DatabaseEntities.SaveChanges();
            
            // queue an LGU refresh for the area no longer governed by this BMP
            if (isDelineationDistributed && delineationGeometry != null)
            {
                ModelingEngineUtilities.QueueLGURefreshForArea(delineationGeometry, null);
            }

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

        [HttpGet]
        [JurisdictionManageFeature]
        public ContentResult BulkDeleteTreatmentBMPs()
        {
            return new ContentResult();
        }

        [HttpPost]
        [JurisdictionManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public PartialViewResult BulkDeleteTreatmentBMPs(BulkDeleteTreatmentBMPsViewModel viewModel)
        {
            var treatmentBMPs = new List<TreatmentBMP>();

            if (viewModel.TreatmentBMPIDList != null)
            {
                treatmentBMPs = HttpRequestStorage.DatabaseEntities.TreatmentBMPs.Where(x => viewModel.TreatmentBMPIDList.Contains(x.TreatmentBMPID)).ToList();
            }
            var viewData = new BulkDeleteTreatmentBMPsViewData(treatmentBMPs);
            return RazorPartialView<BulkDeleteTreatmentBMPs, BulkDeleteTreatmentBMPsViewData, BulkDeleteTreatmentBMPsViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [JurisdictionManageFeature]
        public ContentResult BulkDeleteTreatmentBMPsModal()
        {
            return new ContentResult();
        }

        [HttpPost]
        [JurisdictionManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult BulkDeleteTreatmentBMPsModal(BulkDeleteTreatmentBMPsViewModel viewModel)
        {
            var treatmentBMPDisplayNames = new List<string>();
            if (!ModelState.IsValid)
            {
                return new ModalDialogFormJsonResult();
            }

            if (viewModel.TreatmentBMPIDList != null)
            {
                var treatmentBMPs = HttpRequestStorage.DatabaseEntities.TreatmentBMPs.Where(x => viewModel.TreatmentBMPIDList.Contains(x.TreatmentBMPID)).ToList();
                treatmentBMPDisplayNames = treatmentBMPs.Select(x => x.TreatmentBMPName).ToList();

                foreach (var treatmentBMP in treatmentBMPs)
                {
                    var delineationGeometry = treatmentBMP.Delineation?.DelineationGeometry;
                    var isDelineationDistributed = treatmentBMP.Delineation?.DelineationType == DelineationType.Distributed;

                    NereidUtilities.MarkDownstreamNodeDirty(treatmentBMP, HttpRequestStorage.DatabaseEntities);

                    foreach (var downstreamBMP in treatmentBMP.TreatmentBMPsWhereYouAreTheUpstreamBMP)
                    {
                        downstreamBMP.UpstreamBMPID = null;
                    }
                    HttpRequestStorage.DatabaseEntities.SaveChanges();

                    // todo: The code-generated DeleteFull is brittle since it touches the LGU system.
                    // We should write a more finely-grained delete that deletes delineations via the
                    // pattern established in DelineationController
                    treatmentBMP.DeleteFull(HttpRequestStorage.DatabaseEntities);
                    HttpRequestStorage.DatabaseEntities.SaveChanges();

                    // queue an LGU refresh for the area no longer governed by this BMP
                    if (isDelineationDistributed && delineationGeometry != null)
                    {
                        ModelingEngineUtilities.QueueLGURefreshForArea(delineationGeometry, null);
                    }
                }

            }

            SetMessageForDisplay($"Successfully deleted Treatment BMPs: {string.Join(", ", treatmentBMPDisplayNames)}");
            return new ModalDialogFormJsonResult();
            
        }

        [AnonymousUnclassifiedFeature]
        public PartialViewResult SummaryForMap(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var viewData = new SummaryForMapViewData(CurrentPerson, treatmentBMP);
            return RazorPartialView<SummaryForMap, SummaryForMapViewData>(viewData);
        }

        [AnonymousUnclassifiedFeature]
        [HttpGet]
        public ContentResult FindByName()
        {
            return new ContentResult();
        }

        [AnonymousUnclassifiedFeature]
        [HttpPost]
        public JsonResult FindByName(FindABMPViewModel viewModel)
        {
            var searchString = viewModel.SearchTerm.Trim().ToLower();
            var treatmentBMPTypeIDs = viewModel.TreatmentBMPTypeIDs ?? new List<int>();
            var stormwaterJurisdictionIDs = viewModel.StormwaterJurisdictionIDs ?? new List<int>();
            // ReSharper disable once InconsistentNaming
            var allTreatmentBMPsMatchingSearchString = CurrentPerson.GetTreatmentBmpsPersonCanView()
                .Where(x => treatmentBMPTypeIDs.Contains(x.TreatmentBMPTypeID) &&
                            stormwaterJurisdictionIDs.Contains(x.StormwaterJurisdiction.StormwaterJurisdictionID) &&
                            x.TreatmentBMPName.ToLower().Contains(searchString)).ToList();

            var listItems = allTreatmentBMPsMatchingSearchString.OrderBy(x => x.TreatmentBMPName).Take(20).Select(bmp =>
            {
                var reprojectedLocationPoint = bmp.LocationPoint4326;
                var treatmentBMPMapSummaryData = new SearchMapSummaryData(bmp.GetMapSummaryUrl(), reprojectedLocationPoint,
                    reprojectedLocationPoint.YCoordinate.GetValueOrDefault(),
                    reprojectedLocationPoint.XCoordinate.GetValueOrDefault(),
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
        [NeptuneViewFeature]
        public ViewResult ViewTreatmentBMPModelingAttributes()
        {
            var neptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.ViewTreatmentBMPModelingAttributes);
            var viewData = new ViewTreatmentBMPModelingAttributesViewData(CurrentPerson, neptunePage);
            return RazorView<ViewTreatmentBMPModelingAttributes, ViewTreatmentBMPModelingAttributesViewData>(viewData);
        }

        [NeptuneViewFeature]
        public GridJsonNetJObjectResult<vViewTreatmentBMPModelingAttributes> ViewTreatmentBMPModelingAttributesGridJsonData()
        {
            var stormwaterJurisdictionIDsPersonCanView = CurrentPerson.GetStormwaterJurisdictionIDsPersonCanView();
            var gridSpec = new ViewTreatmentBMPModelingAttributesGridSpec();
            var treatmentBMPs = HttpRequestStorage.DatabaseEntities.vViewTreatmentBMPModelingAttributes.Where(x => stormwaterJurisdictionIDsPersonCanView.Contains(x.StormwaterJurisdictionID)).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<vViewTreatmentBMPModelingAttributes>(treatmentBMPs, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpGet]
        [TreatmentBMPEditFeature]
        public ViewResult EditModelingAttributes(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var treatmentBMPModelingAttribute = treatmentBMP.TreatmentBMPModelingAttribute ?? new TreatmentBMPModelingAttribute(treatmentBMP)
            {
                /* defaults for a brand new record; note, we probably should only set these for the given type,
                 * but it doesn't really matter too much since we are using it to prepopulate, and if a certain
                 * type does not have a property it won't show on the editor and therefore be considered null when saving
                 */
                UnderlyingHydrologicSoilGroupID = UnderlyingHydrologicSoilGroup.D.UnderlyingHydrologicSoilGroupID,
                RoutingConfigurationID = RoutingConfiguration.Online.RoutingConfigurationID,
                TimeOfConcentrationID = TimeOfConcentration.FiveMinutes.TimeOfConcentrationID,
                DesignResidenceTimeforPermanentPool = 720,
                DryWeatherFlowOverrideID = DryWeatherFlowOverride.No.DryWeatherFlowOverrideID
            };

            var viewModel = new EditModelingAttributesViewModel(treatmentBMPModelingAttribute, treatmentBMP.TreatmentBMPType.TreatmentBMPModelingTypeID);
            return ViewEditModelingAttributes(viewModel, treatmentBMP);
        }

        [HttpPost]
        [TreatmentBMPEditFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditModelingAttributes(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey,
            EditModelingAttributesViewModel viewModel)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEditModelingAttributes(viewModel, treatmentBMP);
            }

            var treatmentBMPModelingAttribute = treatmentBMP.TreatmentBMPModelingAttribute;
            if (treatmentBMPModelingAttribute == null)
            {
                treatmentBMPModelingAttribute = new TreatmentBMPModelingAttribute(treatmentBMP);
                HttpRequestStorage.DatabaseEntities.TreatmentBMPModelingAttributes.Add(treatmentBMPModelingAttribute);
            }
            viewModel.UpdateModel(treatmentBMPModelingAttribute, CurrentPerson);
            SetMessageForDisplay("Modeling Attributes successfully saved.");

            // need to re-execute the model at this node since it was re-parameterized
            NereidUtilities.MarkTreatmentBMPDirty(treatmentBMP, HttpRequestStorage.DatabaseEntities);

            return RedirectToAction(new SitkaRoute<TreatmentBMPController>(c => c.Detail(treatmentBMP.PrimaryKey)));
        }

        private ViewResult ViewEditModelingAttributes(EditModelingAttributesViewModel viewModel, TreatmentBMP treatmentBMP)
        {
            var routingConfigurations = RoutingConfiguration.All.OrderBy(x => x.RoutingConfigurationDisplayName);
            var timeOfConcentrations = TimeOfConcentration.All.OrderBy(x => x.TimeOfConcentrationID);
            var underlyingHydrologicSoilGroups = UnderlyingHydrologicSoilGroup.All.OrderBy(x => x.UnderlyingHydrologicSoilGroupID);
            var monthsOfOperation = MonthsOfOperation.All;
            var dryWeatherFlowOverride = DryWeatherFlowOverride.All;
            var viewData = new EditModelingAttributesViewData(CurrentPerson, treatmentBMP, routingConfigurations, timeOfConcentrations, underlyingHydrologicSoilGroups, monthsOfOperation, dryWeatherFlowOverride);
            return RazorView<EditModelingAttributes, EditModelingAttributesViewData, EditModelingAttributesViewModel>(viewData, viewModel);
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
            var stormwaterJurisdictions = CurrentPerson.GetStormwaterJurisdictionsPersonCanView();
            var boundingBox = treatmentBMP.LocationPoint != null
                ? new BoundingBox(treatmentBMP.LocationPoint4326)
                : BoundingBox.GetBoundingBox(stormwaterJurisdictions);
            var zoomLevel = MapInitJson.DefaultZoomLevel + 6;

            var mapInitJson =
                new MapInitJson($"BMP_{CurrentPerson.PersonID}_EditBMP", zoomLevel, layerGeoJsons, boundingBox, false)
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

        [HttpGet]
        [TreatmentBMPEditFeature]
        public ContentResult EditLocationFromDelineationMap(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            return Content("");
        }

        [HttpPost]
        [TreatmentBMPEditFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public JsonResult EditLocationFromDelineationMap(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey, EditLocationViewModel viewModel)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;

            if (!ModelState.IsValid)
            {
                throw new SitkaDisplayErrorException("The location update parameters were invalid.");
            }

            treatmentBMP.MarkInventoryAsProvisionalIfNonManager(CurrentPerson);

            viewModel.UpdateModel(treatmentBMP, CurrentPerson);

            return Json(new {success = true});
        }

        [HttpGet]
        [NeptuneAdminFeature]
        public PartialViewResult RefreshModelBasinsFromOCSurvey()
        {
            return ViewRefreshModelBasinsFromOCSurvey(new ConfirmDialogFormViewModel());
        }

        [HttpPost]
        [NeptuneAdminFeature]
        public ActionResult RefreshModelBasinsFromOCSurvey(ConfirmDialogFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewRefreshModelBasinsFromOCSurvey(viewModel);
            }

            BackgroundJob.Enqueue(() => ScheduledBackgroundJobLaunchHelper.RunModelBasinRefreshBackgroundJob(CurrentPerson.PersonID));
            SetMessageForDisplay("Model Basins refresh will run in the background.");
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewRefreshModelBasinsFromOCSurvey(ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = "Are you sure you want to refresh the Model Basins layer from OC Survey?<br /><br />This can take a little while to run.";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [NeptuneAdminFeature]
        public PartialViewResult RefreshPrecipitationZonesFromOCSurvey()
        {
            return ViewRefreshPrecipitationZonesFromOCSurvey(new ConfirmDialogFormViewModel());
        }

        [HttpPost]
        [NeptuneAdminFeature]
        public ActionResult RefreshPrecipitationZonesFromOCSurvey(ConfirmDialogFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewRefreshPrecipitationZonesFromOCSurvey(viewModel);
            }

            BackgroundJob.Enqueue(() => ScheduledBackgroundJobLaunchHelper.RunPrecipitationZoneRefreshBackgroundJob(CurrentPerson.PersonID));
            SetMessageForDisplay("Precipitation Zones refresh will run in the background.");
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewRefreshPrecipitationZonesFromOCSurvey(ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = "Are you sure you want to refresh the Precipitation Zones layer from OC Survey?<br /><br />This can take a little while to run.";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [NeptuneAdminFeature]
        public PartialViewResult RefreshOCTAPrioritizationLayerFromOCSurvey()
        {
            return ViewRefreshOCTAPrioritizationLayerFromOCSurvey(new ConfirmDialogFormViewModel());
        }

        [HttpPost]
        [NeptuneAdminFeature]
        public ActionResult RefreshOCTAPrioritizationLayerFromOCSurvey(ConfirmDialogFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewRefreshOCTAPrioritizationLayerFromOCSurvey(viewModel);
            }

            BackgroundJob.Enqueue(() => ScheduledBackgroundJobLaunchHelper.RunOCTAPrioritizationRefreshBackgroundJob(CurrentPerson.PersonID));
            SetMessageForDisplay("OCTA Prioritization refresh will run in the background.");
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewRefreshOCTAPrioritizationLayerFromOCSurvey(ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = "Are you sure you want to refresh the OCTA Prioritization layer from OC Survey?<br /><br />This can take a little while to run.";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }


        [AnonymousUnclassifiedFeature]
        public ContentResult MapPopup(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var properties = new Dictionary<string, string>
            {
                {"Name", treatmentBMP.GetDisplayNameAsUrl().ToString()},
                {
                    $"{FieldDefinitionType.Jurisdiction.GetFieldDefinitionLabel()}",
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
            var uploadedCSVFile = viewModel.UploadCSV;
            // ReSharper disable once PossibleInvalidOperationException
            var treatmentBMPType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.GetTreatmentBMPType(viewModel.TreatmentBMPTypeID.Value);
            var treatmentBMPs = TreatmentBMPCsvParserHelper.CSVUpload(uploadedCSVFile.InputStream, treatmentBMPType, out var errorList, out var customAttributes, out var customAttributeValues, out var modelingAttributes);

            if (errorList.Any())
            {
                return ViewUploadBMPs(viewModel, errorList);
            }

            var treatmentBmpsAdded = treatmentBMPs.Where(x => !ModelObjectHelpers.IsRealPrimaryKeyValue(x.PrimaryKey)).ToList();
            var treatmentBmpsUpdated = treatmentBMPs.Where(x => ModelObjectHelpers.IsRealPrimaryKeyValue(x.PrimaryKey)).ToList();

            HttpRequestStorage.DatabaseEntities.TreatmentBMPs.AddRange(treatmentBmpsAdded);
            HttpRequestStorage.DatabaseEntities.CustomAttributes.AddRange(customAttributes.Where(x => !ModelObjectHelpers.IsRealPrimaryKeyValue(x.PrimaryKey)));
            HttpRequestStorage.DatabaseEntities.CustomAttributeValues.AddRange(customAttributeValues.Where(x => !ModelObjectHelpers.IsRealPrimaryKeyValue(x.PrimaryKey)));
            HttpRequestStorage.DatabaseEntities.TreatmentBMPModelingAttributes.AddRange(modelingAttributes.Where(x => !ModelObjectHelpers.IsRealPrimaryKeyValue(x.PrimaryKey)));
            HttpRequestStorage.DatabaseEntities.SaveChanges(CurrentPerson);

            // Need to re-executed model for updated BMPs since they may have been re-parameterized
            // can safely ignore the new BMPs since they won't have delineations yet
            NereidUtilities.MarkTreatmentBMPDirty(treatmentBmpsUpdated, HttpRequestStorage.DatabaseEntities);

            var message = $"Upload Successful: {treatmentBmpsAdded.Count} records added, {treatmentBmpsUpdated.Count} records updated!";
            SetMessageForDisplay(message);
            return new RedirectResult(SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(x => x.Index()));
        }

        [HttpGet]
        [AnonymousUnclassifiedFeature]
        public JsonResult GetModelResults(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var treatmentBMPModelResultSimple = new ModeledPerformanceResultSimple(treatmentBMP);
            return Json(treatmentBMPModelResultSimple, JsonRequestBehavior.AllowGet);
        }

        private ViewResult ViewUploadBMPs(UploadTreatmentBMPsViewModel viewModel, List<string> errorList)
        {
            var bmpTypes = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.OrderBy(x => x.TreatmentBMPTypeName)
                .ToSelectListWithEmptyFirstRow(
                    x => x.TreatmentBMPTypeID.ToString(CultureInfo.InvariantCulture),
                    x => x.TreatmentBMPTypeName.ToString(CultureInfo.InvariantCulture));
            var neptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.UploadTreatmentBMPs);
            var viewData = new UploadTreatmentBMPsViewData(CurrentPerson, bmpTypes, errorList, neptunePage,
                SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(x => x.UploadBMPs()));
            return RazorView<UploadTreatmentBMPs, UploadTreatmentBMPsViewData, UploadTreatmentBMPsViewModel>(viewData,
                viewModel);
        }

        private static void CreateEsriShapefileFromFeatureCollection(FeatureCollection featureCollection,
            Ogr2OgrCommandLineRunner ogr2OgrCommandLineRunner, string outputShapefileName, string outputPath,
            bool update)
        {
            ogr2OgrCommandLineRunner.ImportGeoJsonToFileGdb(JsonConvert.SerializeObject(featureCollection), outputPath,
                outputShapefileName, update, false);
        }
    }
}
