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

using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.Shared;
using Neptune.Web.Views.TreatmentBMP;
using System.Globalization;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Neptune.Common;
using Neptune.Common.GeoSpatial;
using Neptune.Common.Mvc;
using Neptune.EFModels;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using Neptune.Web.Common.Models;
using Neptune.Web.Common.MvcResults;
using Neptune.Web.Services.Filters;
using Neptune.Web.Views.Shared.EditAttributes;
using Neptune.Web.Views.Shared.HRUCharacteristics;
using Neptune.Web.Views.Shared.Location;
using Neptune.Web.Views.Shared.ModeledPerformance;
using Detail = Neptune.Web.Views.TreatmentBMP.Detail;
using DetailViewData = Neptune.Web.Views.TreatmentBMP.DetailViewData;
using Edit = Neptune.Web.Views.TreatmentBMP.Edit;
using EditOtherDesignAttributes = Neptune.Web.Views.TreatmentBMP.EditOtherDesignAttributes;
using EditViewData = Neptune.Web.Views.TreatmentBMP.EditViewData;
using EditViewModel = Neptune.Web.Views.TreatmentBMP.EditViewModel;
using TreatmentBMPAssessmentSummary = Neptune.EFModels.Entities.TreatmentBMPAssessmentSummary;

namespace Neptune.Web.Controllers
{
    public class TreatmentBMPController : NeptuneBaseController<TreatmentBMPController>
    {
        public TreatmentBMPController(NeptuneDbContext dbContext, ILogger<TreatmentBMPController> logger, IOptions<WebConfiguration> webConfiguration, LinkGenerator linkGenerator) : base(dbContext, logger, linkGenerator, webConfiguration)
        {
        }

        [HttpGet]
        public ViewResult FindABMP()
        {
            var stormwaterJurisdictions = StormwaterJurisdictions.ListViewableByPerson(_dbContext, CurrentPerson);
            var stormwaterJurisdictionIDsPersonCanView = stormwaterJurisdictions.Select(x => x.StormwaterJurisdictionID);
            var treatmentBMPs = CurrentPerson.GetTreatmentBmpsPersonCanView(_dbContext, stormwaterJurisdictionIDsPersonCanView);
            var jurisdictions = stormwaterJurisdictions.Select(x => x.AsDisplayDto()).ToList();
            var jurisdictionMapLayers = MapInitJsonHelpers.GetJurisdictionMapLayers(_dbContext);
            var mapInitJson = new SearchMapInitJson("StormwaterIndexMap", jurisdictionMapLayers,
                StormwaterMapInitJson.MakeTreatmentBMPLayerGeoJson(treatmentBMPs, false, false, _linkGenerator))
            {
                JurisdictionLayerGeoJson = jurisdictionMapLayers.Single(x => x.LayerName == MapInitJsonHelpers.CountyCityLayerName)
            };
            var treatmentBMPDisplayDtos = treatmentBMPs.Select(x => x.AsDisplayDto()).ToList();
            var treatmentBMPTypeDisplayDtos = treatmentBMPs.Select(x => x.TreatmentBMPType).Distinct(new HavePrimaryKeyComparer<TreatmentBMPType>()).Select(x => x.AsDisplayDto()).ToList();
            var neptunePage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.FindABMP);
            var viewData = new FindABMPViewData(HttpContext, _linkGenerator, CurrentPerson, mapInitJson, neptunePage, treatmentBMPDisplayDtos, treatmentBMPTypeDisplayDtos, jurisdictions);
            return RazorView<FindABMP, FindABMPViewData>(viewData);
        }

        [HttpGet]
        [NeptuneViewFeature]
        public ViewResult Index()
        {
            var neptunePage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.TreatmentBMP);
            var stormwaterJurisdictionIDsPersonCanView = StormwaterJurisdictionPeople.ListViewableStormwaterJurisdictionIDsByPerson(_dbContext, CurrentPerson);
            var treatmentBmpsCurrentUserCanSee = CurrentPerson.GetTreatmentBmpsPersonCanView(_dbContext, stormwaterJurisdictionIDsPersonCanView);
            var treatmentBmpsInExportCount = treatmentBmpsCurrentUserCanSee.Count;
            var featureClassesInExportCount =
                treatmentBmpsCurrentUserCanSee.Select(x => x.TreatmentBMPTypeID).Distinct().Count() + 1;
            var bulkBMPUploadUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(_linkGenerator, x => x.UploadBMPs());
            var viewData = new IndexViewData(HttpContext, _linkGenerator, CurrentPerson, neptunePage, treatmentBmpsInExportCount, featureClassesInExportCount, bulkBMPUploadUrl);
            return RazorView<Views.TreatmentBMP.Index, IndexViewData>(viewData);
        }

        [HttpGet]
        [NeptuneViewFeature]
        public GridJsonNetJObjectResult<vTreatmentBMPDetailed> TreatmentBMPGridJsonData()
        {
            var stormwaterJurisdictionIDsPersonCanView = StormwaterJurisdictionPeople.ListViewableStormwaterJurisdictionIDsByPerson(_dbContext, CurrentPerson);
            var showDelete = new JurisdictionManageFeature().HasPermissionByPerson(CurrentPerson);
            var showEdit = new JurisdictionEditFeature().HasPermissionByPerson(CurrentPerson);
            var gridSpec = new TreatmentBMPGridSpec(CurrentPerson, showDelete, showEdit, _linkGenerator);
            var treatmentBMPs = _dbContext.vTreatmentBMPDetaileds.Where(x => stormwaterJurisdictionIDsPersonCanView.Contains(x.StormwaterJurisdictionID)).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<vTreatmentBMPDetailed>(treatmentBMPs, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpGet]
        [NeptuneViewFeature]
        public ViewResult TreatmentBMPAssessmentSummary()
        {
            var neptunePage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.TreatmentBMPAssessment);
            var viewData = new TreatmentBMPAssessmentSummaryViewData(HttpContext, _linkGenerator, CurrentPerson, neptunePage);
            return RazorView<Views.TreatmentBMP.TreatmentBMPAssessmentSummary, TreatmentBMPAssessmentSummaryViewData>(
                viewData);
        }

        [HttpGet]
        [NeptuneViewFeature]
        public GridJsonNetJObjectResult<TreatmentBMPAssessmentSummary> TreatmentBMPAssessmentSummaryGridJsonData()
        {
            var gridSpec = new TreatmentBMPAssessmentSummaryGridSpec(_linkGenerator);
            var stormwaterJurisdictionIDsCurrentUserCanEdit = StormwaterJurisdictionPeople.ListViewableStormwaterJurisdictionIDsByPerson(_dbContext, CurrentPerson);

            var vMostRecentTreatmentBMPAssessments1 =
                _dbContext.vMostRecentTreatmentBMPAssessments.Where(x =>
                    stormwaterJurisdictionIDsCurrentUserCanEdit.Contains(x.StormwaterJurisdictionID)).ToList();
            var vMostRecentTreatmentBMPAssessmentIDs =
                vMostRecentTreatmentBMPAssessments1.Select(y => y.LastAssessmentID).ToList();

            var treatmentBMPObservations = _dbContext.TreatmentBMPObservations.OrderBy(x=>x.TreatmentBMPAssessment.TreatmentBMPID).ThenBy(x=>x.TreatmentBMPTypeAssessmentObservationType.SortOrder).Where(x =>
                vMostRecentTreatmentBMPAssessmentIDs
                    .Contains(x.TreatmentBMPAssessmentID)).ToList().Where(x=> x.TreatmentBMPAssessmentObservationType.ObservationTypeSpecification.ObservationTypeCollectionMethodID == ObservationTypeCollectionMethod.PassFail.ObservationTypeCollectionMethodID && x.GetPassFailObservationData().SingleValueObservations.Any(y=> Convert.ToBoolean(y.ObservationValue) == false));


            var notes = treatmentBMPObservations.ToList().Select(x =>
            {
                var notesForThisObservation = GeoJsonSerializer.Deserialize<DiscreteObservationSchema>(x.ObservationData)
                    .SingleValueObservations.Select(y => y.Notes);

                var joinedNotes = string.Join(". ", notesForThisObservation);

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

        [HttpGet("{treatmentBMPPrimaryKey}")]
        [TreatmentBMPViewFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        public ViewResult Detail([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = TreatmentBMPs.GetByID(_dbContext, treatmentBMPPrimaryKey);
            var treatmentBMPType = TreatmentBMPTypes.GetByID(_dbContext, treatmentBMP.TreatmentBMPTypeID);
            var mapServiceUrl = _webConfiguration.ParcelMapServiceUrl;
            var mapInitJson = new TreatmentBMPDetailMapInitJson("StormwaterDetailMap", treatmentBMP.LocationPoint4326, _dbContext);
            mapInitJson.Layers.Add(
                StormwaterMapInitJson.MakeTreatmentBMPLayerGeoJson(new[] {treatmentBMP}, false, true, _linkGenerator));
            var treatmentBMPTree = _dbContext.vTreatmentBMPUpstreams.AsNoTracking()
                .Single(x => x.TreatmentBMPID == treatmentBMP.TreatmentBMPID);

            var upstreamestBMP = treatmentBMPTree.UpstreamBMPID.HasValue ? TreatmentBMPs.GetByID(_dbContext, treatmentBMPTree.UpstreamBMPID) : null;
            var delineation = Delineations.GetByTreatmentBMPID(_dbContext, upstreamestBMP?.TreatmentBMPID ?? treatmentBMP.TreatmentBMPID);
            if (delineation?.DelineationGeometry != null)
            {
                mapInitJson.DelineationLayer = StormwaterMapInitJson.MakeTreatmentBMPDelineationLayerGeoJson(delineation);
            }
            var delineationOverlapDelineations = delineation?.DelineationOverlapDelineations;

            var carouselImages = TreatmentBMPImages.ListByTreatmentBMPID(_dbContext, treatmentBMP.TreatmentBMPID);
            var imageCarouselViewData = new ImageCarouselViewData(carouselImages, 400, _linkGenerator);
            var verifiedUnverifiedUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(_linkGenerator, x => x.VerifyInventory(treatmentBMPPrimaryKey));

            var modelingResultsUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(_linkGenerator, x => x.GetModelResults(treatmentBMP));
            var modeledBMPPerformanceViewData = new ModeledPerformanceViewData(_linkGenerator, modelingResultsUrl, "To BMP");
            var hruCharacteristics = (upstreamestBMP ?? treatmentBMP).GetHRUCharacteristics(_dbContext, delineation).ToList();
            var hruCharacteristicsViewData = new HRUCharacteristicsViewData(hruCharacteristics);
            var otherTreatmentBmpsExistInSubbasin = treatmentBMP.GetRegionalSubbasin(_dbContext)?.GetTreatmentBMPs(_dbContext).Any(x => x.TreatmentBMPID != treatmentBMP.TreatmentBMPID) ?? false;
            var customAttributes = CustomAttributes.ListByTreatmentBMPID(_dbContext, treatmentBMP.TreatmentBMPID);
            var fundingEvents = FundingEvents.ListByTreatmentBMPID(_dbContext, treatmentBMP.TreatmentBMPID);
            var treatmentBMPBenchmarkAndThresholds = TreatmentBMPBenchmarkAndThresholds.ListByTreatmentBMPID(_dbContext, treatmentBMP.TreatmentBMPID);
            var treatmentBMPDocuments = TreatmentBMPDocuments.ListByTreatmentBMPID(_dbContext, treatmentBMP.TreatmentBMPID);
            var hasMissingModelingAttributes = treatmentBMPType.HasMissingModelingAttributes(treatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP);
            var viewData = new DetailViewData(HttpContext, _linkGenerator, CurrentPerson, treatmentBMP, treatmentBMPType, mapInitJson, imageCarouselViewData, verifiedUnverifiedUrl, hruCharacteristicsViewData, mapServiceUrl, modeledBMPPerformanceViewData, otherTreatmentBmpsExistInSubbasin, hasMissingModelingAttributes, customAttributes, fundingEvents, treatmentBMPBenchmarkAndThresholds, treatmentBMPDocuments, delineation, delineationOverlapDelineations, upstreamestBMP);
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
        public async Task<IActionResult> New(NewViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewNew(viewModel);
            }

            var treatmentBMP = new TreatmentBMP
            {
                TreatmentBMPTypeID = viewModel.TreatmentBMPTypeID,
                StormwaterJurisdictionID = viewModel.StormwaterJurisdictionID,
                OwnerOrganizationID = CurrentPerson.OrganizationID, 
                InventoryIsVerified = false,
                TrashCaptureStatusTypeID = viewModel.TrashCaptureStatusTypeID.GetValueOrDefault(),
                SizingBasisTypeID = viewModel.SizingBasisTypeID.GetValueOrDefault()
            };
            viewModel.UpdateModel(_dbContext, treatmentBMP, CurrentPerson, null);
            treatmentBMP.SetTreatmentBMPPointInPolygonDataByLocationPoint(treatmentBMP.LocationPoint, _dbContext);
            await _dbContext.TreatmentBMPs.AddAsync(treatmentBMP);
            await _dbContext.SaveChangesAsync();

            SetMessageForDisplay("Treatment BMP successfully created.");

            return RedirectToAction(new SitkaRoute<TreatmentBMPController>(_linkGenerator, x => x.Detail(treatmentBMP.PrimaryKey)));
        }

        private ViewResult ViewNew(NewViewModel viewModel)
        {
            var stormwaterJurisdictions = StormwaterJurisdictions.ListViewableByPerson(_dbContext, CurrentPerson);
            var geometries = StormwaterJurisdictionGeometries
                .ListByStormwaterJurisdictionIDList(_dbContext,
                    stormwaterJurisdictions.Select(x => x.StormwaterJurisdictionID)).Select(x => x.Geometry4326);
            var treatmentBMPTypes = TreatmentBMPTypes.List(_dbContext);
            var organizations = Organizations.List(_dbContext);
            var layerGeoJsons = MapInitJsonHelpers.GetJurisdictionMapLayers(_dbContext).ToList();
            var boundingBox = stormwaterJurisdictions.Any()
                ? new BoundingBoxDto(geometries)
                : new BoundingBoxDto();
            var zoomLevel = CurrentPerson.IsAdministrator() ? MapInitJson.DefaultZoomLevel : MapInitJson.DefaultZoomLevel + 2;

            var mapInitJson =
                new MapInitJson($"BMP_{CurrentPerson.PersonID}_EditBMP", zoomLevel, layerGeoJsons, boundingBox, false)
                {
                    AllowFullScreen = false
                };
            var editLocationViewData = new EditLocationViewData(mapInitJson, "treatmentBMPLocation");
            var waterQualityManagementPlans = WaterQualityManagementPlans.ListViewableByPerson(_dbContext, CurrentPerson);

            var viewData = new NewViewData(HttpContext, _linkGenerator, CurrentPerson, stormwaterJurisdictions, treatmentBMPTypes, organizations, editLocationViewData, waterQualityManagementPlans, TreatmentBMPLifespanType.All, TrashCaptureStatusType.All, SizingBasisType.All);
            return RazorView<New, NewViewData, NewViewModel>(viewData, viewModel);
        }

        [HttpGet("{treatmentBMPPrimaryKey}")]
        [TreatmentBMPEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        public ViewResult Edit([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = TreatmentBMPs.GetByID(_dbContext, treatmentBMPPrimaryKey);
            var viewModel = new EditViewModel(treatmentBMP);
            return ViewEdit(treatmentBMP, viewModel);
        }

        [HttpPost("{treatmentBMPPrimaryKey}")]
        [TreatmentBMPEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        public async Task<IActionResult> Edit([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey, EditViewModel viewModel)
        {
            var treatmentBMP = TreatmentBMPs.GetByIDWithChangeTracking(_dbContext, treatmentBMPPrimaryKey);
            if (!ModelState.IsValid)
            {
                return ViewEdit(treatmentBMP, viewModel);
            }

            treatmentBMP.MarkInventoryAsProvisionalIfNonManager(CurrentPerson);
            viewModel.UpdateModel(_dbContext, treatmentBMP);
            await _dbContext.SaveChangesAsync();

            SetMessageForDisplay("Treatment BMP successfully saved.");

            return RedirectToAction(new SitkaRoute<TreatmentBMPController>(_linkGenerator, x => x.Detail(treatmentBMP.PrimaryKey)));
        }

        private ViewResult ViewEdit(TreatmentBMP treatmentBMP, EditViewModel viewModel)
        {
            var stormwaterJurisdictions = StormwaterJurisdictions.ListViewableByPerson(_dbContext, CurrentPerson);
            var treatmentBMPTypes = TreatmentBMPTypes.List(_dbContext);
            var organizations = Organizations.List(_dbContext);
            var waterQualityManagementPlans = WaterQualityManagementPlans.ListByStormwaterJurisdictionID(_dbContext, treatmentBMP.StormwaterJurisdictionID);
            var viewData = new EditViewData(HttpContext, _linkGenerator, CurrentPerson, treatmentBMP, stormwaterJurisdictions, treatmentBMPTypes, organizations, waterQualityManagementPlans, TreatmentBMPLifespanType.All, TrashCaptureStatusType.All, SizingBasisType.All);
            return RazorView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }

        [HttpGet("{treatmentBMPPrimaryKey}")]
        [TreatmentBMPEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        public PartialViewResult EditUpstreamBMP([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = TreatmentBMPs.GetByID(_dbContext, treatmentBMPPrimaryKey);
            var viewModel = new EditUpstreamBMPViewModel(treatmentBMP);
            return ViewEditUpstreamBMP(treatmentBMP, viewModel);
        }

        [HttpPost("{treatmentBMPPrimaryKey}")]
        [TreatmentBMPEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        public async Task<IActionResult> EditUpstreamBMP([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey, EditUpstreamBMPViewModel viewModel)
        {
            var treatmentBMP = TreatmentBMPs.GetByIDWithChangeTracking(_dbContext, treatmentBMPPrimaryKey);
            if (!ModelState.IsValid)
            {
                return ViewEditUpstreamBMP(treatmentBMP, viewModel);
            }

            viewModel.UpdateModel(treatmentBMP);
            await _dbContext.SaveChangesAsync();

            var delineation = Delineations.GetByTreatmentBMPIDWithChangeTracking(_dbContext, treatmentBMP.TreatmentBMPID);
            if (delineation != null)
            {
                await delineation.DeleteDelineation(_dbContext);
            }

            SetMessageForDisplay("Upstream BMP successfully updated");
            return new ModalDialogFormJsonResult(SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(_linkGenerator, x => x.Detail(treatmentBMP)));
        }

        [HttpPost("{treatmentBMPPrimaryKey}")]
        [TreatmentBMPEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        public async Task<IActionResult> RemoveUpstreamBMP([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = TreatmentBMPs.GetByIDWithChangeTracking(_dbContext, treatmentBMPPrimaryKey);
            await treatmentBMP.RemoveUpstreamBMP(_dbContext);
            SetMessageForDisplay("Upstream BMP successfully removed");

            return RedirectToAction(new SitkaRoute<TreatmentBMPController>(_linkGenerator, x => x.Detail(treatmentBMP.PrimaryKey)));
        }

        private PartialViewResult ViewEditUpstreamBMP(TreatmentBMP treatmentBMP, EditUpstreamBMPViewModel viewModel)
        {
            var treatmentBMPs = treatmentBMP.GetRegionalSubbasin(_dbContext).GetTreatmentBMPs(_dbContext)
                .Where(x => x.TreatmentBMPID != treatmentBMP.TreatmentBMPID);

            var treatmentBMPSelectList = treatmentBMPs.ToSelectListWithDisabledEmptyFirstRow(
                    x => x.TreatmentBMPID.ToString(CultureInfo.InvariantCulture), x => x.TreatmentBMPName,
                    "Select an Upstream BMP");

            var viewData = new EditUpstreamBMPViewData(treatmentBMPSelectList);

            return RazorPartialView<EditUpstreamBMP, EditUpstreamBMPViewData, EditUpstreamBMPViewModel>(viewData, viewModel);
        }

        [HttpGet("{treatmentBMPPrimaryKey}")]
        [TreatmentBMPManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        public PartialViewResult VerifyInventory([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(treatmentBMP.TreatmentBMPID);
            return ViewVerifyInventoryTreatmentBMP(treatmentBMP, viewModel);
        }

        [HttpPost("{treatmentBMPPrimaryKey}")]
        [TreatmentBMPManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        public async Task<IActionResult> VerifyInventory([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey,
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

            await _dbContext.SaveChangesAsync();
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

        [HttpGet("{treatmentBMPPrimaryKey}")]
        [TreatmentBMPManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        public PartialViewResult ConvertTreatmentBMPType([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = TreatmentBMPs.GetByID(_dbContext, treatmentBMPPrimaryKey);
            var viewModel = new ConvertTreatmentBMPTypeViewModel();
            return ViewConvertTreatmentBMPTypeTreatmentBMP(treatmentBMP, viewModel);
        }

        [HttpPost("{treatmentBMPPrimaryKey}")]
        [TreatmentBMPManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        public async Task<IActionResult> ConvertTreatmentBMPType([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey,
            ConvertTreatmentBMPTypeViewModel viewModel)
        {
            var treatmentBMP = TreatmentBMPs.GetByIDWithChangeTracking(_dbContext, treatmentBMPPrimaryKey);
            if (!ModelState.IsValid)
            {
                return ViewConvertTreatmentBMPTypeTreatmentBMP(treatmentBMP, viewModel);
            }

            // delete any field visit, assessment, benchmark, and maintenance records
            foreach (var fieldVisit in _dbContext.FieldVisits.Where(x => x.TreatmentBMPID == treatmentBMP.TreatmentBMPID).ToList())
            {
                fieldVisit.DeleteFull(_dbContext);
            }

            foreach (var treatmentBMPBenchmarkAndThreshold in treatmentBMP.TreatmentBMPBenchmarkAndThresholds.ToList())
            {
                treatmentBMPBenchmarkAndThreshold.DeleteFull(_dbContext);
            }

            treatmentBMP.TreatmentBMPBenchmarkAndThresholds = null;

            var newTreatmentBMPType = TreatmentBMPTypes.GetByID(_dbContext, viewModel.TreatmentBMPTypeID);
            var validCustomAttributeTypes = newTreatmentBMPType.TreatmentBMPTypeCustomAttributeTypes.ToList();

            // we need to clone the attributes instead of simply changing the bmp type and treatmentbmptypecustomattributetype ids
            var customAttributesCloned = new List<CustomAttribute>();
            foreach (var customAttribute in treatmentBMP.CustomAttributes.Where(z => validCustomAttributeTypes.Select(y => y.CustomAttributeTypeID).Contains(z.CustomAttributeTypeID))
                .ToList())
            {
                var treatmentBMPTypeCustomAttributeType = newTreatmentBMPType.TreatmentBMPTypeCustomAttributeTypes.Single(x => x.CustomAttributeTypeID == customAttribute.CustomAttributeTypeID);
                var customAttributeCloned = new CustomAttribute()
                {
                    TreatmentBMPID = treatmentBMP.TreatmentBMPID,
                    TreatmentBMPTypeCustomAttributeTypeID = treatmentBMPTypeCustomAttributeType.TreatmentBMPTypeCustomAttributeTypeID,
                    TreatmentBMPTypeID = treatmentBMPTypeCustomAttributeType.TreatmentBMPTypeID,
                    CustomAttributeTypeID = treatmentBMPTypeCustomAttributeType.CustomAttributeTypeID
                };
                customAttributesCloned.Add(customAttributeCloned);
                foreach (var value in customAttribute.CustomAttributeValues)
                {
                    // ReSharper disable once UnusedVariable
                    // (never used, but creating it with this constructor adds it to the CustomAttributeValue collection of customAttributeCloned automatically, so it's done its job)
                    var customAttributeValue = new CustomAttributeValue
                    {
                        CustomAttribute = customAttributeCloned,
                        AttributeValue = value.AttributeValue
                    };

                }
            }

            // delete any custom attributes that are not valid for the new treatment bmp type
            foreach (var customAttribute in treatmentBMP.CustomAttributes.ToList())
            {
                customAttribute.DeleteFull(_dbContext);
            }
            // force a save changes to clear out fk references when we switch the bmp type
            await _dbContext.SaveChangesAsync();

            // now add the cloned custom attributes to the db context
            _dbContext.CustomAttributes.AddRange(customAttributesCloned);
            treatmentBMP.TreatmentBMPTypeID = viewModel.TreatmentBMPTypeID.GetValueOrDefault();
            await _dbContext.SaveChangesAsync();
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewConvertTreatmentBMPTypeTreatmentBMP(TreatmentBMP treatmentBMP,
            ConvertTreatmentBMPTypeViewModel viewModel)
        {
            var treatmentBMPTypes = TreatmentBMPTypes.List(_dbContext).Where(x => x.TreatmentBMPTypeID != treatmentBMP.TreatmentBMPTypeID);
            var viewData = new ConvertTreatmentBMPTypeViewData(treatmentBMP, treatmentBMPTypes);
            return RazorPartialView<ConvertTreatmentBMPType, ConvertTreatmentBMPTypeViewData, ConvertTreatmentBMPTypeViewModel>(viewData, viewModel);
        }

        [HttpGet("{treatmentBMPPrimaryKey}")]
        [TreatmentBMPDeleteFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        public PartialViewResult Delete([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(treatmentBMP.TreatmentBMPID);
            return ViewDeleteTreatmentBMP(treatmentBMP, viewModel);
        }

        [HttpPost("{treatmentBMPPrimaryKey}")]
        [TreatmentBMPDeleteFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        public async Task<IActionResult> Delete([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var delineation = Delineations.GetByTreatmentBMPIDWithChangeTracking(_dbContext, treatmentBMP.TreatmentBMPID);
            var delineationGeometry = delineation?.DelineationGeometry;
            var isDelineationDistributed = delineation != null && delineation.DelineationType == DelineationType.Distributed;

            NereidUtilities.MarkDownstreamNodeDirty(treatmentBMP, _dbContext);

            if (!ModelState.IsValid)
            {
                return ViewDeleteTreatmentBMP(treatmentBMP, viewModel);
            }

            var treatmentBMPTreatmentBMPName = treatmentBMP.TreatmentBMPName;

            foreach (var downstreamBMP in treatmentBMP.InverseUpstreamBMP)
            {
                downstreamBMP.UpstreamBMPID = null;
            }
            await _dbContext.SaveChangesAsync();
            
            // todo: The code-generated DeleteFull is brittle since it touches the LGU system.
            // We should write a more finely-grained delete that deletes delineations via the
            // pattern established in DelineationController
            treatmentBMP.DeleteFull(_dbContext);
            await _dbContext.SaveChangesAsync();
            
            // queue an LGU refresh for the area no longer governed by this BMP
            if (isDelineationDistributed && delineationGeometry != null)
            {
                ModelingEngineUtilities.QueueLGURefreshForArea(delineationGeometry, null, _dbContext);
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
        public PartialViewResult BulkDeleteTreatmentBMPs([FromBody] BulkDeleteTreatmentBMPsViewModel viewModel)
        {
            var treatmentBMPs = new List<TreatmentBMP>();

            if (viewModel.TreatmentBMPIDList != null)
            {
                treatmentBMPs = TreatmentBMPs.ListByTreatmentBMPIDList(_dbContext, viewModel.TreatmentBMPIDList).ToList();
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
        public async Task<IActionResult> BulkDeleteTreatmentBMPsModal(BulkDeleteTreatmentBMPsViewModel viewModel)
        {
            var treatmentBMPDisplayNames = new List<string>();
            if (!ModelState.IsValid)
            {
                return new ModalDialogFormJsonResult();
            }

            if (viewModel.TreatmentBMPIDList != null)
            {
                var treatmentBMPs = TreatmentBMPs.ListByTreatmentBMPIDListWithChangeTracking(_dbContext, viewModel.TreatmentBMPIDList);
                treatmentBMPDisplayNames = treatmentBMPs.Select(x => x.TreatmentBMPName).ToList();
                var delineations = Delineations.ListByTreatmentBMPIDList(_dbContext, viewModel.TreatmentBMPIDList).ToDictionary(x => x.TreatmentBMPID);
                foreach (var treatmentBMP in treatmentBMPs)
                {
                    // todo: revisit this during delineations
                    var delineation = delineations.ContainsKey(treatmentBMP.TreatmentBMPID) ? delineations[treatmentBMP.TreatmentBMPID] : null;
                    var isDelineationDistributed = delineation != null && delineation.DelineationType == DelineationType.Distributed;

                    NereidUtilities.MarkDownstreamNodeDirty(treatmentBMP, _dbContext);

                    foreach (var downstreamBMP in treatmentBMP.InverseUpstreamBMP)
                    {
                        downstreamBMP.UpstreamBMPID = null;
                    }
                    await _dbContext.SaveChangesAsync();

                    // todo: The code-generated DeleteFull is brittle since it touches the LGU system.
                    // We should write a more finely-grained delete that deletes delineations via the
                    // pattern established in DelineationController
                    treatmentBMP.DeleteFull(_dbContext);
                    await _dbContext.SaveChangesAsync();

                    // queue an LGU refresh for the area no longer governed by this BMP
                    if (isDelineationDistributed && delineation?.DelineationGeometry != null)
                    {
                        ModelingEngineUtilities.QueueLGURefreshForArea(delineation?.DelineationGeometry, null, _dbContext);
                    }
                }

            }

            SetMessageForDisplay($"Successfully deleted Treatment BMPs: {string.Join(", ", treatmentBMPDisplayNames)}");
            return new ModalDialogFormJsonResult();
            
        }

        [HttpGet("{treatmentBMPPrimaryKey}")]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        public PartialViewResult SummaryForMap([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = TreatmentBMPs.GetByID(_dbContext, treatmentBMPPrimaryKey);
            // we don't have the concept of a keyphoto; just arbitrarily pick the first photo
            var keyPhoto = TreatmentBMPImages.ListByTreatmentBMPID(_dbContext, treatmentBMP.TreatmentBMPID).FirstOrDefault();
            var viewData = new SummaryForMapViewData(HttpContext, _linkGenerator, CurrentPerson, treatmentBMP, keyPhoto);
            return RazorPartialView<SummaryForMap, SummaryForMapViewData>(viewData);
        }

        [HttpGet]
        public ContentResult FindByName()
        {
            return new ContentResult();
        }

        [HttpPost]
        public JsonResult FindByName(FindABMPViewModel viewModel)
        {
            var searchString = viewModel.SearchTerm.Trim().ToLower();
            var treatmentBMPTypeIDs = viewModel.TreatmentBMPTypeIDs;
            var stormwaterJurisdictionIDs = viewModel.StormwaterJurisdictionIDs;
            // ReSharper disable once InconsistentNaming
            var stormwaterJurisdictionIDsPersonCanView = StormwaterJurisdictionPeople.ListViewableStormwaterJurisdictionIDsByPerson(_dbContext, CurrentPerson);
            var allTreatmentBMPsMatchingSearchString = CurrentPerson
                .GetTreatmentBmpsPersonCanView(_dbContext, stormwaterJurisdictionIDsPersonCanView)
                .Where(x => treatmentBMPTypeIDs.Contains(x.TreatmentBMPTypeID) &&
                            stormwaterJurisdictionIDs.Contains(x.StormwaterJurisdictionID) &&
                            x.TreatmentBMPName.ToLower().Contains(searchString)).ToList();

            var mapSummaryUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(_linkGenerator, t => t.SummaryForMap(UrlTemplate.Parameter1Int)));
            var listItems = allTreatmentBMPsMatchingSearchString.OrderBy(x => x.TreatmentBMPName).Take(20).Select(x =>
            {
                var locationPoint4326 = x.LocationPoint4326;
                var treatmentBMPMapSummaryData = new SearchMapSummaryData(
                    mapSummaryUrlTemplate.ParameterReplace(x.TreatmentBMPID), locationPoint4326,
                    locationPoint4326.Coordinate.Y,
                    locationPoint4326.Coordinate.X,
                    x.TreatmentBMPID);
                var listItem = new SelectListItem(x.TreatmentBMPName, GeoJsonSerializer.Serialize(treatmentBMPMapSummaryData));
                return listItem;
            }).ToList();

            return Json(listItems);
        }

        [HttpGet("{treatmentBMPPrimaryKey}")]
        [TreatmentBMPEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        public ViewResult EditOtherDesignAttributes([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = TreatmentBMPs.GetByID(_dbContext, treatmentBMPPrimaryKey);
            var treatmentBMPType = TreatmentBMPTypes.GetByID(_dbContext, treatmentBMP.TreatmentBMPTypeID);
            var customAttributes = CustomAttributes.ListByTreatmentBMPID(_dbContext, treatmentBMP.TreatmentBMPID);
            var customAttributeTypePurposeEnum = CustomAttributeTypePurposeEnum.OtherDesignAttributes;
            var customAttributeUpsertDtos = CustomAttributes
                .ListByTreatmentBMPIDWithChangeTracking(_dbContext, treatmentBMPPrimaryKey.PrimaryKeyValue).Where(x =>
                    x.CustomAttributeType.CustomAttributeTypePurposeID ==
                    (int)customAttributeTypePurposeEnum).Select(x => x.AsUpsertDto()).ToList();
            var viewModel = new EditAttributesViewModel(customAttributeUpsertDtos);
            return ViewEditOtherDesignAttributes(viewModel, treatmentBMP, customAttributeTypePurposeEnum, treatmentBMPType, customAttributes);
        }

        [HttpPost("{treatmentBMPPrimaryKey}")]
        [TreatmentBMPEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        public async Task<IActionResult> EditOtherDesignAttributes([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey, EditAttributesViewModel viewModel)
        {
            var treatmentBMP = TreatmentBMPs.GetByIDWithChangeTracking(_dbContext, treatmentBMPPrimaryKey);
            var treatmentBMPType = TreatmentBMPTypes.GetByID(_dbContext, treatmentBMP.TreatmentBMPTypeID);
            var customAttributes = CustomAttributes.ListByTreatmentBMPIDWithChangeTracking(_dbContext, treatmentBMP.TreatmentBMPID);
            var customAttributeTypePurposeEnum = CustomAttributeTypePurposeEnum.OtherDesignAttributes;
            if (!ModelState.IsValid)
            {
                return ViewEditOtherDesignAttributes(viewModel, treatmentBMP, customAttributeTypePurposeEnum, treatmentBMPType, customAttributes);
            }

            var allCustomAttributeTypes = CustomAttributeTypes.List(_dbContext);
            var existingCustomAttributes = CustomAttributes.ListByTreatmentBMPIDWithChangeTracking(_dbContext, treatmentBMPPrimaryKey.PrimaryKeyValue).Where(x =>
                x.CustomAttributeType.CustomAttributeTypePurposeID ==
                (int)customAttributeTypePurposeEnum).ToList();
            await viewModel.UpdateModel(_dbContext, treatmentBMP, existingCustomAttributes, allCustomAttributeTypes);
            treatmentBMP.MarkInventoryAsProvisionalIfNonManager(CurrentPerson);
            await _dbContext.SaveChangesAsync();
            SetMessageForDisplay("Custom Attributes successfully saved.");
            return RedirectToAction(new SitkaRoute<TreatmentBMPController>(_linkGenerator, x => x.Detail(treatmentBMP.PrimaryKey)));
        }

        private ViewResult ViewEditOtherDesignAttributes(EditAttributesViewModel viewModel, TreatmentBMP treatmentBMP, CustomAttributeTypePurposeEnum customAttributeTypePurposeEnum, TreatmentBMPType treatmentBMPType, ICollection<CustomAttribute> customAttributes)
        {
            var missingRequiredAttributes = treatmentBMPType.TreatmentBMPTypeCustomAttributeTypes
                                                .Any(x =>
                                                    x.CustomAttributeType.CustomAttributeTypePurposeID ==
                                                    (int) customAttributeTypePurposeEnum &&
                                                    x.CustomAttributeType.IsRequired &&
                                                    !customAttributes
                                                        .Select(y => y.CustomAttributeTypeID)
                                                        .Contains(x.CustomAttributeTypeID)) ||
                                            customAttributes.Any(x =>
                                                x.CustomAttributeType.CustomAttributeTypePurposeID ==
                                                (int) customAttributeTypePurposeEnum &&
                                                x.CustomAttributeType.IsRequired &&
                                                (x.CustomAttributeValues == null ||
                                                 x.CustomAttributeValues.All(y => string.IsNullOrEmpty(y.AttributeValue)))
                                            );
            var editAttributesViewData = new EditAttributesViewData(treatmentBMPType, customAttributeTypePurposeEnum, missingRequiredAttributes);
            var parentDetailUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(_linkGenerator, x => x.Detail(treatmentBMP));
            var viewData = new EditOtherDesignAttributesViewData(HttpContext, _linkGenerator, CurrentPerson, treatmentBMP, parentDetailUrl, editAttributesViewData);
            return RazorView<EditOtherDesignAttributes, EditOtherDesignAttributesViewData, EditAttributesViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [NeptuneViewFeature]
        public ViewResult ViewTreatmentBMPModelingAttributes()
        {
            var neptunePage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.ViewTreatmentBMPModelingAttributes);
            var viewData = new ViewTreatmentBMPModelingAttributesViewData(HttpContext, _linkGenerator, CurrentPerson, neptunePage);
            return RazorView<ViewTreatmentBMPModelingAttributes, ViewTreatmentBMPModelingAttributesViewData>(viewData);
        }

        [HttpGet]
        [NeptuneViewFeature]
        public GridJsonNetJObjectResult<TreatmentBMP> ViewTreatmentBMPModelingAttributesGridJsonData()
        {
            var stormwaterJurisdictionIDsPersonCanView = StormwaterJurisdictionPeople.ListViewableStormwaterJurisdictionIDsByPerson(_dbContext, CurrentPerson);
            var delineationsDict = vTreatmentBMPUpstreams.ListWithDelineationAsDictionary(_dbContext);
            var watershedsDict = _dbContext.Watersheds.AsNoTracking().Select(x => new {x.WatershedID, x.WatershedName}).ToDictionary(x => x.WatershedID, x => x.WatershedName);
            var precipitationZonesDict = _dbContext.PrecipitationZones.AsNoTracking()
                .Select(x => new { x.PrecipitationZoneID, x.DesignStormwaterDepthInInches })
                .ToDictionary(x => x.PrecipitationZoneID, x => x.DesignStormwaterDepthInInches);
            var gridSpec = new ViewTreatmentBMPModelingAttributesGridSpec(_linkGenerator, delineationsDict, watershedsDict, precipitationZonesDict);
            var treatmentBMPs = TreatmentBMPs.ListWithModelingAttributes(_dbContext).Where(x => stormwaterJurisdictionIDsPersonCanView.Contains(x.StormwaterJurisdictionID)).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<TreatmentBMP>(treatmentBMPs, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpGet("{treatmentBMPPrimaryKey}")]
        [TreatmentBMPEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        public ViewResult EditModelingAttributes([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = TreatmentBMPs.GetByID(_dbContext, treatmentBMPPrimaryKey);
            var treatmentBMPModelingAttribute = treatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP ?? new TreatmentBMPModelingAttribute()
            {
                TreatmentBMP = treatmentBMP,
                /* defaults for a brand new record; note, we probably should only set these for the given type,
                 * but it doesn't really matter too much since we are using it to prepopulate, and if a certain
                 * type does not have a property it won't show on the editor and therefore be considered null when saving
                 */
                UnderlyingHydrologicSoilGroupID = UnderlyingHydrologicSoilGroup.D.UnderlyingHydrologicSoilGroupID,
                RoutingConfigurationID = RoutingConfiguration.Online.RoutingConfigurationID,
                TimeOfConcentrationID = TimeOfConcentration.FiveMinutes.TimeOfConcentrationID,
                DryWeatherFlowOverrideID = DryWeatherFlowOverride.No.DryWeatherFlowOverrideID
            };

            var viewModel = new EditModelingAttributesViewModel(treatmentBMPModelingAttribute, treatmentBMP.TreatmentBMPType.TreatmentBMPModelingTypeID);
            return ViewEditModelingAttributes(viewModel, treatmentBMP);
        }

        [HttpPost("{treatmentBMPPrimaryKey}")]
        [TreatmentBMPEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        public async Task<IActionResult> EditModelingAttributes([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey,
            EditModelingAttributesViewModel viewModel)
        {
            var treatmentBMP = TreatmentBMPs.GetByIDWithChangeTracking(_dbContext, treatmentBMPPrimaryKey);
            if (!ModelState.IsValid)
            {
                return ViewEditModelingAttributes(viewModel, treatmentBMP);
            }

            var treatmentBMPModelingAttribute = treatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP;
            if (treatmentBMPModelingAttribute == null)
            {
                treatmentBMPModelingAttribute = new TreatmentBMPModelingAttribute()
                {
                    TreatmentBMP = treatmentBMP
                };
                _dbContext.TreatmentBMPModelingAttributes.Add(treatmentBMPModelingAttribute);
            }
            viewModel.UpdateModel(treatmentBMPModelingAttribute, CurrentPerson);
            SetMessageForDisplay("Modeling Attributes successfully saved.");

            // need to re-execute the model at this node since it was re-parameterized
            NereidUtilities.MarkTreatmentBMPDirty(treatmentBMP, _dbContext);

            await _dbContext.SaveChangesAsync();
            return RedirectToAction(new SitkaRoute<TreatmentBMPController>(_linkGenerator, x => x.Detail(treatmentBMP.PrimaryKey)));
        }

        private ViewResult ViewEditModelingAttributes(EditModelingAttributesViewModel viewModel, TreatmentBMP treatmentBMP)
        {
            var routingConfigurations = RoutingConfiguration.All.OrderBy(x => x.RoutingConfigurationDisplayName);
            var timeOfConcentrations = TimeOfConcentration.All.OrderBy(x => x.TimeOfConcentrationID);
            var underlyingHydrologicSoilGroups = UnderlyingHydrologicSoilGroup.All.OrderBy(x => x.UnderlyingHydrologicSoilGroupID);
            var monthsOfOperation = MonthsOfOperation.All;
            var dryWeatherFlowOverride = DryWeatherFlowOverride.All;
            var viewData = new EditModelingAttributesViewData(HttpContext, _linkGenerator, CurrentPerson, treatmentBMP, routingConfigurations, timeOfConcentrations, underlyingHydrologicSoilGroups, monthsOfOperation, dryWeatherFlowOverride);
            return RazorView<EditModelingAttributes, EditModelingAttributesViewData, EditModelingAttributesViewModel>(viewData, viewModel);
        }

        [HttpGet("{treatmentBMPPrimaryKey}")]
        [TreatmentBMPEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        public ViewResult EditLocation([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var viewModel = new SetLocationViewModel(treatmentBMP);
            return ViewEditLocation(treatmentBMP, viewModel);
        }

        private ViewResult ViewEditLocation(TreatmentBMP treatmentBMP, SetLocationViewModel viewModel)
        {
            var mapFormID = "treatmentBMPEditLocation";
            var layerGeoJsons = MapInitJsonHelpers.GetJurisdictionMapLayers(_dbContext).ToList();
            var stormwaterJurisdictions = StormwaterJurisdictions.ListViewableByPerson(_dbContext, CurrentPerson);
            var boundingBox = treatmentBMP.LocationPoint != null
                ? new BoundingBoxDto(treatmentBMP.LocationPoint4326)
                : stormwaterJurisdictions.Any()
                    ? new BoundingBoxDto(stormwaterJurisdictions
                        .Select(x => x.StormwaterJurisdictionGeometry.Geometry4326))
                    : new BoundingBoxDto();
            var zoomLevel = MapInitJson.DefaultZoomLevel + 6;

            var mapInitJson =
                new MapInitJson($"BMP_{CurrentPerson.PersonID}_EditBMP", zoomLevel, layerGeoJsons, boundingBox, false)
                {
                    AllowFullScreen = false
                };

            var editLocationViewData = new EditLocationViewData(mapInitJson, mapFormID);
            var viewData = new SetLocationViewData(HttpContext, _linkGenerator, CurrentPerson, treatmentBMP, editLocationViewData);

            return RazorView<SetLocation, SetLocationViewData, SetLocationViewModel>(viewData, viewModel);
        }

        [HttpPost("{treatmentBMPPrimaryKey}")]
        [TreatmentBMPEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        public async Task<IActionResult> EditLocation([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey, SetLocationViewModel viewModel)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;

            if (!ModelState.IsValid)
            {
                return ViewEditLocation(treatmentBMP, viewModel);
            }

            treatmentBMP.MarkInventoryAsProvisionalIfNonManager(CurrentPerson);
            var delineation = Delineations.GetByTreatmentBMPIDWithChangeTracking(_dbContext, treatmentBMP.TreatmentBMPID);
            viewModel.UpdateModel(_dbContext, treatmentBMP, CurrentPerson, delineation);
            await _dbContext.SaveChangesAsync();

            SetMessageForDisplay("Successfully updated Treatment BMP Location.");

            return new RedirectResult(
                SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(_linkGenerator, x =>
                    x.Detail(treatmentBMPPrimaryKey)));
        }

        [HttpGet("{treatmentBMPPrimaryKey}")]
        [TreatmentBMPEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        public ContentResult EditLocationFromDelineationMap([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            return Content("");
        }

        [HttpPost("{treatmentBMPPrimaryKey}")]
        [TreatmentBMPEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        public async Task<JsonResult> EditLocationFromDelineationMap([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey, SetLocationViewModel viewModel)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;

            if (!ModelState.IsValid)
            {
                throw new SitkaDisplayErrorException("The location update parameters were invalid.");
            }

            treatmentBMP.MarkInventoryAsProvisionalIfNonManager(CurrentPerson);
            var delineation = Delineations.GetByTreatmentBMPIDWithChangeTracking(_dbContext, treatmentBMP.TreatmentBMPID);
            viewModel.UpdateModel(_dbContext, treatmentBMP, CurrentPerson, delineation);
            await _dbContext.SaveChangesAsync();

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

            //todo: BackgroundJob.Enqueue(() => ScheduledBackgroundJobLaunchHelper.RunModelBasinRefreshBackgroundJob(CurrentPerson.PersonID));
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

            //todo: BackgroundJob.Enqueue(() => ScheduledBackgroundJobLaunchHelper.RunPrecipitationZoneRefreshBackgroundJob(CurrentPerson.PersonID));
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

            //todo: BackgroundJob.Enqueue(() => ScheduledBackgroundJobLaunchHelper.RunOCTAPrioritizationRefreshBackgroundJob(CurrentPerson.PersonID));
            SetMessageForDisplay("OCTA Prioritization refresh will run in the background.");
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewRefreshOCTAPrioritizationLayerFromOCSurvey(ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = "Are you sure you want to refresh the OCTA Prioritization layer from OC Survey?<br /><br />This can take a little while to run.";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpGet("{treatmentBMPPrimaryKey}")]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        public ContentResult MapPopup([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var properties = new Dictionary<string, HtmlString>
            {
                {"Name", UrlTemplate.MakeHrefString(SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(_linkGenerator, x => x.Detail(treatmentBMP)), treatmentBMP.TreatmentBMPName)},
                {
                    $"{FieldDefinitionType.Jurisdiction.GetFieldDefinitionLabel()}",
                    UrlTemplate.MakeHrefString(SitkaRoute<JurisdictionController>.BuildUrlFromExpression(_linkGenerator, x => x.Detail(treatmentBMP.StormwaterJurisdiction)), treatmentBMP.StormwaterJurisdiction.GetOrganizationDisplayName())
                },
                {"Type", new HtmlString(treatmentBMP.TreatmentBMPType.TreatmentBMPTypeName)},
            };
            var dl = new TagBuilder("dl");
            dl.InnerHtml.AppendHtml(string.Join("", properties.Select(x => $"<dt>{x.Key}</dt><dd>{x.Value}</dd>").ToList()));
            return Content(dl.ToString());
        }

        //todo: use gdalservice
        //[HttpGet]
        //[NeptuneViewFeature]
        //public FileResult BMPInventoryExport()
        //{
        //    var ogr2OgrCommandLineRunner = new Ogr2OgrCommandLineRunner(NeptuneWebConfiguration.Ogr2OgrExecutable,
        //        Ogr2OgrCommandLineRunner.DefaultCoordinateSystemId,
        //        NeptuneWebConfiguration.HttpRuntimeExecutionTimeout.TotalMilliseconds);
        //    var treatmentBmps = _dbContext.TreatmentBMPs
        //        .Include(x => x.TreatmentBMPType).Include(x => x.TreatmentBMPType.TreatmentBMPTypeCustomAttributeTypes)
        //        .Include(x => x.TreatmentBMPAssessments).Include(x => x.WaterQualityManagementPlan)
        //        .Include(x => x.CustomAttributes)
        //        .ToList().Where(x => x.CanView(CurrentPerson)).ToList();

        //    using (var workingDirectory = new DisposableTempDirectory())
        //    {
        //        FeatureCollection allTreatmentBMPsFeatureCollection = treatmentBmps.ToExportGeoJsonFeatureCollection();
        //        var outputPath = Path.Combine(workingDirectory.DirectoryInfo.FullName, "AllTreatmentBMPs");
        //        CreateEsriShapefileFromFeatureCollection(allTreatmentBMPsFeatureCollection, ogr2OgrCommandLineRunner,
        //            "AllTreatmentBMPs", outputPath, false);

        //        foreach (var grouping in treatmentBmps.GroupBy(x => x.TreatmentBMPType))
        //        {
        //            string outputLayerName =
        //                Ogr2OgrCommandLineRunner.SanitizeStringForGdb(grouping.Key.TreatmentBMPTypeName);
        //            var outputPathForLayer =
        //                Path.Combine(workingDirectory.DirectoryInfo.FullName, outputLayerName);
        //            var subsetTreatmentBMPsFeatureCollection = grouping.ToExportGeoJsonFeatureCollection(grouping.Key);
        //            CreateEsriShapefileFromFeatureCollection(subsetTreatmentBMPsFeatureCollection,
        //                ogr2OgrCommandLineRunner, outputLayerName, outputPathForLayer, false);
        //        }

        //        using (var zipFile = DisposableTempFile.MakeDisposableTempFileEndingIn(".zip"))
        //        {
        //            ZipFile.CreateFromDirectory(workingDirectory.DirectoryInfo.FullName, zipFile.FileInfo.FullName);
        //            var fileStream = zipFile.FileInfo.OpenRead();
        //            var bytes = fileStream.ReadFully();
        //            fileStream.Close();
        //            fileStream.Dispose();
        //            return File(bytes, "application/zip");
        //        }
        //    }
        //}

        //private static void CreateEsriShapefileFromFeatureCollection(FeatureCollection featureCollection,
        //    Ogr2OgrCommandLineRunner ogr2OgrCommandLineRunner, string outputShapefileName, string outputPath,
        //    bool update)
        //{
        //    ogr2OgrCommandLineRunner.ImportGeoJsonToFileGdb(JsonConvert.SerializeObject(featureCollection), outputPath,
        //        outputShapefileName, update, false);
        //}


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
        public async Task<IActionResult> UploadBMPs(UploadTreatmentBMPsViewModel viewModel)
        {
            var uploadedCSVFile = viewModel.UploadCSV;
            // ReSharper disable once PossibleInvalidOperationException
            var treatmentBMPType = TreatmentBMPTypes.GetByID(_dbContext, viewModel.TreatmentBMPTypeID.Value);
            var treatmentBMPs = TreatmentBMPCsvParserHelper.CSVUpload(_dbContext, uploadedCSVFile.OpenReadStream(), treatmentBMPType, out var errorList, out var customAttributes, out var customAttributeValues, out var modelingAttributes);

            if (errorList.Any())
            {
                return ViewUploadBMPs(viewModel, errorList);
            }

            var treatmentBmpsAdded = treatmentBMPs.Where(x => !ModelObjectHelpers.IsRealPrimaryKeyValue(x.PrimaryKey)).ToList();
            var treatmentBmpsUpdated = treatmentBMPs.Where(x => ModelObjectHelpers.IsRealPrimaryKeyValue(x.PrimaryKey)).ToList();

            await _dbContext.TreatmentBMPs.AddRangeAsync(treatmentBmpsAdded);
            await _dbContext.CustomAttributes.AddRangeAsync(customAttributes.Where(x => !ModelObjectHelpers.IsRealPrimaryKeyValue(x.PrimaryKey)));
            await _dbContext.CustomAttributeValues.AddRangeAsync(customAttributeValues.Where(x => !ModelObjectHelpers.IsRealPrimaryKeyValue(x.PrimaryKey)));
            await _dbContext.TreatmentBMPModelingAttributes.AddRangeAsync(modelingAttributes.Where(x => !ModelObjectHelpers.IsRealPrimaryKeyValue(x.PrimaryKey)));
            await _dbContext.SaveChangesAsync();

            // Need to re-executed model for updated BMPs since they may have been re-parameterized
            // can safely ignore the new BMPs since they won't have delineations yet
            NereidUtilities.MarkTreatmentBMPDirty(treatmentBmpsUpdated, _dbContext);

            var message = $"Upload Successful: {treatmentBmpsAdded.Count.ToGroupedNumeric()} records added, {treatmentBmpsUpdated.Count.ToGroupedNumeric()} records updated!";
            SetMessageForDisplay(message);
            return new RedirectResult(SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(_linkGenerator, x => x.Index()));
        }

        [HttpGet("{treatmentBMPPrimaryKey}")]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        public JsonResult GetModelResults([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var treatmentBMPModelResultSimple = new ModeledPerformanceResultDto(_dbContext, treatmentBMP);
            return Json(treatmentBMPModelResultSimple);
        }

        private ViewResult ViewUploadBMPs(UploadTreatmentBMPsViewModel viewModel, List<string> errorList)
        {
            var bmpTypes = _dbContext.TreatmentBMPTypes.OrderBy(x => x.TreatmentBMPTypeName)
                .ToSelectListWithEmptyFirstRow(
                    x => x.TreatmentBMPTypeID.ToString(CultureInfo.InvariantCulture),
                    x => x.TreatmentBMPTypeName.ToString(CultureInfo.InvariantCulture));
            var neptunePage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.UploadTreatmentBMPs);
            var viewData = new UploadTreatmentBMPsViewData(HttpContext, _linkGenerator, CurrentPerson, bmpTypes, errorList, neptunePage, SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(_linkGenerator, x => x.UploadBMPs()));
            return RazorView<UploadTreatmentBMPs, UploadTreatmentBMPsViewData, UploadTreatmentBMPsViewModel>(viewData,
                viewModel);
        }
    }
}
