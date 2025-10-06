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

using Hangfire;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Neptune.Common;
using Neptune.Common.GeoSpatial;
using Neptune.Common.Services.GDAL;
using Neptune.EFModels;
using Neptune.EFModels.Entities;
using Neptune.EFModels.Nereid;
using Neptune.Jobs.Services;
using Neptune.Models.DataTransferObjects;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.Models;
using Neptune.WebMvc.Common.Mvc;
using Neptune.WebMvc.Common.MvcResults;
using Neptune.WebMvc.Models;
using Neptune.WebMvc.Security;
using Neptune.WebMvc.Services.Filters;
using Neptune.WebMvc.Views.HRUCharacteristic;
using Neptune.WebMvc.Views.Shared;
using Neptune.WebMvc.Views.Shared.EditAttributes;
using Neptune.WebMvc.Views.Shared.HRUCharacteristics;
using Neptune.WebMvc.Views.Shared.Location;
using Neptune.WebMvc.Views.Shared.ModeledPerformance;
using Neptune.WebMvc.Views.TreatmentBMP;
using System.Globalization;
using Detail = Neptune.WebMvc.Views.TreatmentBMP.Detail;
using DetailViewData = Neptune.WebMvc.Views.TreatmentBMP.DetailViewData;
using Edit = Neptune.WebMvc.Views.TreatmentBMP.Edit;
using EditOtherDesignAttributes = Neptune.WebMvc.Views.TreatmentBMP.EditOtherDesignAttributes;
using EditViewData = Neptune.WebMvc.Views.TreatmentBMP.EditViewData;
using EditViewModel = Neptune.WebMvc.Views.TreatmentBMP.EditViewModel;
using IndexViewData = Neptune.WebMvc.Views.TreatmentBMP.IndexViewData;
using TreatmentBMPAssessmentSummary = Neptune.EFModels.Entities.TreatmentBMPAssessmentSummary;

namespace Neptune.WebMvc.Controllers
{
    public class TreatmentBMPController(
        NeptuneDbContext dbContext,
        ILogger<TreatmentBMPController> logger,
        IOptions<WebConfiguration> webConfiguration,
        LinkGenerator linkGenerator,
        GDALAPIService gdalApiService)
        : NeptuneBaseController<TreatmentBMPController>(dbContext, logger, linkGenerator, webConfiguration)
    {
        [HttpGet]
        public ViewResult FindABMP()
        {
            var stormwaterJurisdictions = StormwaterJurisdictions.ListViewableByPersonForBMPs(_dbContext, CurrentPerson);
            var stormwaterJurisdictionIDsPersonCanView = stormwaterJurisdictions.Select(x => x.StormwaterJurisdictionID);
            var treatmentBMPs = CurrentPerson.GetTreatmentBmpsPersonCanView(_dbContext, stormwaterJurisdictionIDsPersonCanView);
            var jurisdictions = stormwaterJurisdictions.Select(x => x.AsDisplayDto()).ToList();
            var jurisdictionMapLayers = MapInitJsonHelpers.GetJurisdictionMapLayers(_dbContext);
            var mapInitJson = new SearchMapInitJson("StormwaterIndexMap", jurisdictionMapLayers,
                StormwaterMapInitJson.MakeTreatmentBMPLayerGeoJson(treatmentBMPs, false, false, _linkGenerator))
            {
                JurisdictionLayerGeoJson = jurisdictionMapLayers.Single(x => x.LayerName == MapInitJsonHelpers.CountyCityLayerName)
            };
            var treatmentBMPModelingAttributes =
                vTreatmentBMPModelingAttributes.ListAsDictionary(_dbContext);
            var treatmentBMPDisplayDtos = treatmentBMPs.Select(x => x.AsDisplayDto(treatmentBMPModelingAttributes.TryGetValue(x.TreatmentBMPID, out var attribute) ? attribute : null)).ToList();
            var treatmentBMPTypeDisplayDtos = treatmentBMPs.Select(x => x.TreatmentBMPType).Distinct(new HavePrimaryKeyComparer<TreatmentBMPType>()).Select(x => x.AsDisplayDto()).ToList();
            var neptunePage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.FindABMP);
            var viewData = new FindABMPViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, mapInitJson, neptunePage, treatmentBMPDisplayDtos, treatmentBMPTypeDisplayDtos, jurisdictions);
            return RazorView<FindABMP, FindABMPViewData>(viewData);
        }

        [HttpGet]
        [NeptuneViewFeature]
        public ViewResult Index()
        {
            var neptunePage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.TreatmentBMP);
            var stormwaterJurisdictionIDsPersonCanView = StormwaterJurisdictionPeople.ListViewableStormwaterJurisdictionIDsByPersonForBMPs(_dbContext, CurrentPerson);
            var treatmentBmpsCurrentUserCanSee = CurrentPerson.GetTreatmentBmpsPersonCanView(_dbContext, stormwaterJurisdictionIDsPersonCanView);
            var treatmentBmpsInExportCount = treatmentBmpsCurrentUserCanSee.Count;
            var featureClassesInExportCount =
                treatmentBmpsCurrentUserCanSee.Select(x => x.TreatmentBMPTypeID).Distinct().Count() + 1;
            var bulkBMPUploadUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(_linkGenerator, x => x.UploadBMPs());
            var viewData = new IndexViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, neptunePage, treatmentBmpsInExportCount, featureClassesInExportCount, bulkBMPUploadUrl);
            return RazorView<Views.TreatmentBMP.Index, IndexViewData>(viewData);
        }

        [HttpGet]
        [NeptuneViewFeature]
        public GridJsonNetJObjectResult<vTreatmentBMPDetailed> TreatmentBMPGridJsonData()
        {
            var stormwaterJurisdictionIDsPersonCanView = StormwaterJurisdictionPeople.ListViewableStormwaterJurisdictionIDsByPersonForBMPs(_dbContext, CurrentPerson);
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
            var viewData = new TreatmentBMPAssessmentSummaryViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, neptunePage);
            return RazorView<Views.TreatmentBMP.TreatmentBMPAssessmentSummary, TreatmentBMPAssessmentSummaryViewData>(
                viewData);
        }

        [HttpGet]
        [NeptuneViewFeature]
        public GridJsonNetJObjectResult<TreatmentBMPAssessmentSummary> TreatmentBMPAssessmentSummaryGridJsonData()
        {
            var gridSpec = new TreatmentBMPAssessmentSummaryGridSpec(_linkGenerator);
            var stormwaterJurisdictionIDsCurrentUserCanEdit = StormwaterJurisdictionPeople.ListViewableStormwaterJurisdictionIDsByPersonForBMPs(_dbContext, CurrentPerson);

            var vMostRecentTreatmentBMPAssessments =
                _dbContext.vMostRecentTreatmentBMPAssessments.AsNoTracking().Where(x =>
                    stormwaterJurisdictionIDsCurrentUserCanEdit.Contains(x.StormwaterJurisdictionID)).ToList();
            var vMostRecentTreatmentBMPAssessmentIDs = vMostRecentTreatmentBMPAssessments.Select(y => y.LastAssessmentID).ToList();

            var treatmentBMPObservations = _dbContext.TreatmentBMPObservations
                .Include(x => x.TreatmentBMPAssessmentObservationType)
                .AsNoTracking()
                .Where(x =>
                vMostRecentTreatmentBMPAssessmentIDs
                    .Contains(x.TreatmentBMPAssessmentID)).ToList().Where(x=> x.TreatmentBMPAssessmentObservationType.ObservationTypeSpecification.ObservationTypeCollectionMethodID == (int) ObservationTypeCollectionMethodEnum.PassFail && x.GetPassFailObservationData().SingleValueObservations.Any(y=> bool.Parse(y.ObservationValue.ToString()) == false));


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

            var treatmentBMPAssessmentSummaries = vMostRecentTreatmentBMPAssessments.OrderBy(x=>x.TreatmentBMPID).GroupJoin(notes, 
                x => x.LastAssessmentID,
                y => y.TreatmentBMPAssessmentID,
                (x,y) => new TreatmentBMPAssessmentSummary {AssessmentSummary = x, Notes = string.Join("; ", y.Select(z=>z.Notes).OrderBy(z=>z))});
            var gridJsonNetJObjectResult =
                new GridJsonNetJObjectResult<TreatmentBMPAssessmentSummary>(treatmentBMPAssessmentSummaries.OrderByDescending(x=>x.AssessmentSummary.LastAssessmentDate).ToList(),
                    gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpGet("{treatmentBMPPrimaryKey}")]
        [AnonymousUnclassifiedFeature] // intentionally put here to bypass having to login
        [TreatmentBMPViewFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        public async Task<ViewResult> Detail([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = TreatmentBMPs.GetByID(_dbContext, treatmentBMPPrimaryKey);
            var treatmentBMPType = TreatmentBMPTypes.GetByID(_dbContext, treatmentBMP.TreatmentBMPTypeID);
            var mapServiceUrl = _webConfiguration.MapServiceUrl;
            var mapInitJson = new TreatmentBMPDetailMapInitJson("StormwaterDetailMap", treatmentBMP.LocationPoint4326, _dbContext);
            mapInitJson.Layers.Add(
                StormwaterMapInitJson.MakeTreatmentBMPLayerGeoJson(new[] {treatmentBMP}, false, true, _linkGenerator));
            var treatmentBMPTree = _dbContext.vTreatmentBMPUpstreams.AsNoTracking()
                .Single(x => x.TreatmentBMPID == treatmentBMP.TreatmentBMPID);

            var upstreamestBMP = treatmentBMPTree.UpstreamBMPID.HasValue ? TreatmentBMPs.GetByID(_dbContext, treatmentBMPTree.UpstreamBMPID) : null;
            var isUpstreamestBMPAnalyzedInModelingModule = upstreamestBMP != null && upstreamestBMP.TreatmentBMPType.IsAnalyzedInModelingModule;
            var delineation = Delineations.GetByTreatmentBMPID(_dbContext, upstreamestBMP?.TreatmentBMPID ?? treatmentBMP.TreatmentBMPID);
            if (delineation?.DelineationGeometry != null)
            {
                mapInitJson.DelineationLayer = StormwaterMapInitJson.MakeTreatmentBMPDelineationLayerGeoJson(delineation);
            }
            var delineationOverlapDelineations = delineation?.DelineationOverlapDelineations;

            var carouselImages = TreatmentBMPImages.ListByTreatmentBMPID(_dbContext, treatmentBMP.TreatmentBMPID);
            var imageCarouselViewData = new ImageCarouselViewData(carouselImages, 400, _linkGenerator);
            var verifiedUnverifiedUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(_linkGenerator, x => x.VerifyInventory(treatmentBMPPrimaryKey));

            var isSitkaAdmin = new SitkaAdminFeature().HasPermissionByPerson(CurrentPerson);

            var modelingResultsUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(_linkGenerator, x => x.GetModelResults(treatmentBMP));
            var treatmentBMPNereidLog = NereidLogs.GetByTreatmentBMPID(_dbContext, treatmentBMP.TreatmentBMPID);
            var modeledBMPPerformanceViewData = new ModeledPerformanceViewData(_linkGenerator, modelingResultsUrl, "To BMP", isSitkaAdmin, treatmentBMPNereidLog?.NereidRequest, treatmentBMPNereidLog?.NereidResponse);
            var hruCharacteristics = await vHRUCharacteristics.ListByTreatmentBMP(_dbContext, upstreamestBMP ?? treatmentBMP, delineation);
            var hruCharacteristicsViewData = new HRUCharacteristicsViewData(hruCharacteristics);
            var otherTreatmentBmpsExistInSubbasin = treatmentBMP.GetRegionalSubbasin(_dbContext)?.GetTreatmentBMPs(_dbContext).Any(x => x.TreatmentBMPID != treatmentBMP.TreatmentBMPID) ?? false;
            var customAttributes = CustomAttributes.ListByTreatmentBMPID(_dbContext, treatmentBMP.TreatmentBMPID);
            var fundingEvents = FundingEvents.ListByTreatmentBMPID(_dbContext, treatmentBMP.TreatmentBMPID);
            var treatmentBMPBenchmarkAndThresholds = TreatmentBMPBenchmarkAndThresholds.ListByTreatmentBMPID(_dbContext, treatmentBMP.TreatmentBMPID);
            var treatmentBMPDocuments = TreatmentBMPDocuments.ListByTreatmentBMPID(_dbContext, treatmentBMP.TreatmentBMPID);
            var treatmentBMPModelingAttribute =
                vTreatmentBMPModelingAttributes.GetByTreatmentBMPID(_dbContext, treatmentBMP.TreatmentBMPID);
            var hasMissingModelingAttributes = treatmentBMPType.MissingModelingAttributes(treatmentBMPModelingAttribute).Any();
            var regionalSubbasinRevisionRequest = RegionalSubbasinRevisionRequests.ListByTreatmentBMPID(_dbContext, treatmentBMP.TreatmentBMPID).SingleOrDefault(x =>
                x.RegionalSubbasinRevisionRequestStatus == RegionalSubbasinRevisionRequestStatus.Open);
            var watershed = treatmentBMP.WatershedID.HasValue ? _dbContext.Watersheds.AsNoTracking().Single(x => x.WatershedID == treatmentBMP.WatershedID.Value) : null;
            var watershedFieldDefinitionText =
                FieldDefinitionType.Watershed.GetFieldDefinitionData(_dbContext).FieldDefinitionValue;
            var viewData = new DetailViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, treatmentBMP, treatmentBMPType, mapInitJson, imageCarouselViewData, verifiedUnverifiedUrl, hruCharacteristicsViewData, mapServiceUrl, modeledBMPPerformanceViewData, otherTreatmentBmpsExistInSubbasin, hasMissingModelingAttributes, customAttributes, fundingEvents, treatmentBMPBenchmarkAndThresholds, treatmentBMPDocuments, delineation, delineationOverlapDelineations, upstreamestBMP, isUpstreamestBMPAnalyzedInModelingModule, regionalSubbasinRevisionRequest, watershed, watershedFieldDefinitionText, treatmentBMPModelingAttribute);
            return RazorView<Detail, DetailViewData>(viewData);
        }

        [HttpGet("{treatmentBMPPrimaryKey}")]
        [NeptuneAdminFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        public async Task<GridJsonNetJObjectResult<vHRUCharacteristic>> HRUCharacteristicGridJsonData([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = TreatmentBMPs.GetByID(_dbContext, treatmentBMPPrimaryKey);
            var treatmentBMPID = treatmentBMP.TreatmentBMPID;
            var treatmentBMPTree = _dbContext.vTreatmentBMPUpstreams.AsNoTracking()
                .Single(x => x.TreatmentBMPID != null && x.TreatmentBMPID == treatmentBMPID);

            var upstreamestBMP = treatmentBMPTree.UpstreamBMPID.HasValue ? TreatmentBMPs.GetByID(_dbContext, treatmentBMPTree.UpstreamBMPID) : null;
            var delineation = Delineations.GetByTreatmentBMPID(_dbContext, upstreamestBMP?.TreatmentBMPID ?? treatmentBMPID);
            var gridSpec = new HRUCharacteristicGridSpec(_linkGenerator);
            var hruCharacteristics = await vHRUCharacteristics.ListByTreatmentBMP(_dbContext, upstreamestBMP ?? treatmentBMP, delineation);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<vHRUCharacteristic>(hruCharacteristics, gridSpec);
            return gridJsonNetJObjectResult;
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
            var stormwaterJurisdictions = StormwaterJurisdictions.ListViewableByPersonForBMPs(_dbContext, CurrentPerson);
            var treatmentBMPTypes = TreatmentBMPTypes.List(_dbContext);
            var organizations = Organizations.List(_dbContext);
            var layerGeoJsons = MapInitJsonHelpers.GetJurisdictionMapLayers(_dbContext).ToList();
            BoundingBoxDto boundingBox;
            if (stormwaterJurisdictions.Any())
            {
                var geometries = StormwaterJurisdictionGeometries.ListByStormwaterJurisdictionIDList(_dbContext, stormwaterJurisdictions.Select(x => x.StormwaterJurisdictionID)).Select(x => x.Geometry4326);
                boundingBox = new BoundingBoxDto(geometries);
            }
            else
            {
                boundingBox = new BoundingBoxDto();
            }

            var zoomLevel = CurrentPerson.IsAdministrator() ? MapInitJson.DefaultZoomLevel : MapInitJson.DefaultZoomLevel + 2;

            var mapInitJson =
                new MapInitJson($"BMP_{CurrentPerson.PersonID}_EditBMP", zoomLevel, layerGeoJsons, boundingBox, false)
                {
                    AllowFullScreen = false
                };
            var editLocationViewData = new EditLocationViewData(mapInitJson, "treatmentBMPLocation");
            var waterQualityManagementPlans = WaterQualityManagementPlans.ListViewableByPerson(_dbContext, CurrentPerson);

            var viewData = new NewViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, stormwaterJurisdictions, treatmentBMPTypes, organizations, editLocationViewData, waterQualityManagementPlans, TreatmentBMPLifespanType.All, TrashCaptureStatusType.All, SizingBasisType.All);
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
            var stormwaterJurisdictions = StormwaterJurisdictions.ListViewableByPersonForBMPs(_dbContext, CurrentPerson);
            var treatmentBMPTypes = TreatmentBMPTypes.List(_dbContext);
            var organizations = Organizations.List(_dbContext);
            var waterQualityManagementPlans = WaterQualityManagementPlans.ListByStormwaterJurisdictionID(_dbContext, treatmentBMP.StormwaterJurisdictionID);
            var viewData = new EditViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, treatmentBMP, stormwaterJurisdictions, treatmentBMPTypes, organizations, waterQualityManagementPlans, TreatmentBMPLifespanType.All, TrashCaptureStatusType.All, SizingBasisType.All);
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
                await delineation.DeleteFull(_dbContext);
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
            treatmentBMP.UpstreamBMPID = null;
            await _dbContext.SaveChangesAsync();

            // need to re-execute the Nereid model here since source of run-off was removed.
            await NereidUtilities.MarkTreatmentBMPDirty(treatmentBMP, _dbContext);
            SetMessageForDisplay("Upstream BMP successfully removed");

            return RedirectToAction(new SitkaRoute<TreatmentBMPController>(_linkGenerator, x => x.Detail(treatmentBMP.PrimaryKey)));
        }

        private PartialViewResult ViewEditUpstreamBMP(TreatmentBMP treatmentBMP, EditUpstreamBMPViewModel viewModel)
        {
            var treatmentBMPs = treatmentBMP.GetRegionalSubbasin(_dbContext)?.GetTreatmentBMPs(_dbContext)
                .Where(x => x.TreatmentBMPID != treatmentBMP.TreatmentBMPID);
            IEnumerable<SelectListItem> treatmentBMPSelectList = new List<SelectListItem>();
            if (treatmentBMPs != null)
            {
                treatmentBMPSelectList = treatmentBMPs.ToSelectListWithDisabledEmptyFirstRow(
                    x => x.TreatmentBMPID.ToString(CultureInfo.InvariantCulture), x => x.TreatmentBMPName,
                    "Select an Upstream BMP");
            }

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
            await _dbContext.MaintenanceRecordObservationValues
                .Include(x => x.MaintenanceRecordObservation)
                .ThenInclude(x => x.MaintenanceRecord)
                .Where(x => x.MaintenanceRecordObservation.MaintenanceRecord.TreatmentBMPID ==
                            treatmentBMP.TreatmentBMPID)
                .ExecuteDeleteAsync();
            await _dbContext.MaintenanceRecordObservations
                .Include(x => x.MaintenanceRecord)
                .Where(x => x.MaintenanceRecord.TreatmentBMPID == treatmentBMP.TreatmentBMPID).ExecuteDeleteAsync();
            await _dbContext.MaintenanceRecords.Where(x => x.TreatmentBMPID == treatmentBMP.TreatmentBMPID)
                .ExecuteDeleteAsync();
            await _dbContext.TreatmentBMPAssessmentPhotos
                .Include(x => x.TreatmentBMPAssessment)
                .Where(x => x.TreatmentBMPAssessment.TreatmentBMPID == treatmentBMP.TreatmentBMPID)
                .ExecuteDeleteAsync();
            await _dbContext.TreatmentBMPObservations
                .Include(x => x.TreatmentBMPAssessment)
                .Where(x => x.TreatmentBMPAssessment.TreatmentBMPID == treatmentBMP.TreatmentBMPID)
                .ExecuteDeleteAsync();
            await _dbContext.TreatmentBMPAssessments
                .Where(x => x.TreatmentBMPID == treatmentBMP.TreatmentBMPID)
                .ExecuteDeleteAsync();
            await _dbContext.TreatmentBMPBenchmarkAndThresholds
                .Where(x => x.TreatmentBMPID == treatmentBMP.TreatmentBMPID).ExecuteDeleteAsync();
            await _dbContext.FieldVisits.Where(x => x.TreatmentBMPID == treatmentBMP.TreatmentBMPID)
                .ExecuteDeleteAsync();


            var newTreatmentBMPType = TreatmentBMPTypes.GetByID(_dbContext, viewModel.TreatmentBMPTypeID);
            var validCustomAttributeTypes = newTreatmentBMPType.TreatmentBMPTypeCustomAttributeTypes.ToList();

            // we need to clone the attributes instead of simply changing the bmp type and treatmentbmptypecustomattributetype ids
            var currentCustomAttributes = CustomAttributes.ListByTreatmentBMPID(_dbContext, treatmentBMP.TreatmentBMPID);

            // delete any custom attributes that are not valid for the new treatment bmp type
            await _dbContext.CustomAttributeValues.Include(x => x.CustomAttribute)
                .Where(x => x.CustomAttribute.TreatmentBMPID == treatmentBMP.TreatmentBMPID).ExecuteDeleteAsync();
            await _dbContext.CustomAttributes.Where(x => x.TreatmentBMPID == treatmentBMP.TreatmentBMPID)
                .ExecuteDeleteAsync();

            // now add the cloned custom attributes to the db context
            var customAttributesCloned = new List<CustomAttribute>();
            foreach (var customAttribute in currentCustomAttributes.Where(x => validCustomAttributeTypes.Select(y => y.CustomAttributeTypeID).Contains(x.CustomAttributeTypeID))
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
            await _dbContext.CustomAttributes.AddRangeAsync(customAttributesCloned);
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

            await NereidUtilities.MarkDownstreamNodeDirty(treatmentBMP, _dbContext);

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
            
            await treatmentBMP.DeleteFull(_dbContext);
            
            // queue an LGU refresh for the area no longer governed by this BMP
            if (isDelineationDistributed && delineationGeometry != null)
            {
                await ModelingEngineUtilities.QueueLGURefreshForArea(delineationGeometry, null, _dbContext);
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

            var postUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(_linkGenerator,
                x => x.BulkDeleteTreatmentBMPsModal(null));
            var viewData = new BulkDeleteTreatmentBMPsViewData(treatmentBMPs,
                postUrl);
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

                    await NereidUtilities.MarkDownstreamNodeDirty(treatmentBMP, _dbContext);

                    foreach (var downstreamBMP in treatmentBMP.InverseUpstreamBMP)
                    {
                        downstreamBMP.UpstreamBMPID = null;
                    }
                    await _dbContext.SaveChangesAsync();

                    await treatmentBMP.DeleteFull(_dbContext);

                    // queue an LGU refresh for the area no longer governed by this BMP
                    if (isDelineationDistributed && delineation?.DelineationGeometry != null)
                    {
                        await ModelingEngineUtilities.QueueLGURefreshForArea(delineation.DelineationGeometry, null, _dbContext);
                    }
                }

            }

            SetMessageForDisplay($"Successfully deleted Treatment BMPs: {string.Join(", ", treatmentBMPDisplayNames)}");
            return new ModalDialogFormJsonResult();
            
        }

        [HttpGet("{treatmentBMPPrimaryKey}")]
        [TreatmentBMPManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        public PartialViewResult QueueLGURefreshForTreatmentBMP([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(treatmentBMP.TreatmentBMPID);
            return ViewQueueLGURefreshForTreatmentBMP(treatmentBMP, viewModel);
        }

        [HttpPost("{treatmentBMPPrimaryKey}")]
        [TreatmentBMPEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        public async Task<IActionResult> QueueLGURefreshForTreatmentBMP([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey, ConfirmDialogFormViewModel viewMode)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var delineation = Delineations.GetByTreatmentBMPIDWithChangeTracking(_dbContext, treatmentBMP.TreatmentBMPID);

            if (delineation == null)
            {
                SetErrorForDisplay($"Treatment BMPs require a delineation in order to refresh their Land Use.");
                return new ModalDialogFormJsonResult(SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(_linkGenerator, x => x.Detail(treatmentBMPPrimaryKey)));
            }

            var delineationGeometry = delineation?.DelineationGeometry;
            var isDelineationDistributed = delineation != null && delineation.DelineationType == DelineationType.Distributed;

            if (!isDelineationDistributed)
            {
                SetErrorForDisplay($"This delineation cannot be refreshed because it is not distributed.");
                return new ModalDialogFormJsonResult(SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(_linkGenerator, x => x.Detail(treatmentBMPPrimaryKey)));
            }

            //await NereidUtilities.MarkDownstreamNodeDirty(treatmentBMP, _dbContext);
            await ModelingEngineUtilities.QueueLGURefreshForArea(delineationGeometry, null, _dbContext);

            SetMessageForDisplay($"Successfully queued a Land Use refresh for the Treatment BMP {treatmentBMP.TreatmentBMPName}. It will run in the background, please check back later to view the results.");
            return new ModalDialogFormJsonResult(SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(_linkGenerator, x => x.Detail(treatmentBMPPrimaryKey)));
        }

        private PartialViewResult ViewQueueLGURefreshForTreatmentBMP(TreatmentBMP treatmentBMP,
            ConfirmDialogFormViewModel viewModel)
        {
            var viewData =
                new ConfirmDialogFormViewData(
                    $"Are you sure you want to refresh the Land Use Area for '{treatmentBMP.TreatmentBMPName}' treatment BMP?");
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData,
                viewModel);
        }

        [HttpGet("{treatmentBMPPrimaryKey}")]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        public PartialViewResult SummaryForMap([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = TreatmentBMPs.GetByID(_dbContext, treatmentBMPPrimaryKey);
            // we don't have the concept of a keyphoto; just arbitrarily pick the first photo
            var keyPhoto = TreatmentBMPImages.ListByTreatmentBMPID(_dbContext, treatmentBMP.TreatmentBMPID).FirstOrDefault();
            var viewData = new SummaryForMapViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, treatmentBMP, keyPhoto);
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
            var stormwaterJurisdictionIDsPersonCanView = StormwaterJurisdictionPeople.ListViewableStormwaterJurisdictionIDsByPersonForBMPs(_dbContext, CurrentPerson);
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
            var missingRequiredAttributes =
                treatmentBMPType.HasMissingRequiredCustomAttributes(customAttributeTypePurposeEnum, customAttributes);
            var editAttributesViewData = new EditAttributesViewData(treatmentBMPType, customAttributeTypePurposeEnum, missingRequiredAttributes);
            var parentDetailUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(_linkGenerator, x => x.Detail(treatmentBMP));
            var viewData = new EditOtherDesignAttributesViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, treatmentBMP, parentDetailUrl, editAttributesViewData);
            return RazorView<EditOtherDesignAttributes, EditOtherDesignAttributesViewData, EditAttributesViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [NeptuneViewFeature]
        public ViewResult ViewTreatmentBMPModelingAttributes()
        {
            var neptunePage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.ViewTreatmentBMPModelingAttributes);
            var viewData = new ViewTreatmentBMPModelingAttributesViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, neptunePage);
            return RazorView<ViewTreatmentBMPModelingAttributes, ViewTreatmentBMPModelingAttributesViewData>(viewData);
        }

        [HttpGet]
        [NeptuneViewFeature]
        public GridJsonNetJObjectResult<TreatmentBMP> ViewTreatmentBMPModelingAttributesGridJsonData()
        {
            var stormwaterJurisdictionIDsPersonCanView = StormwaterJurisdictionPeople.ListViewableStormwaterJurisdictionIDsByPersonForBMPs(_dbContext, CurrentPerson);
            var delineationsDict = vTreatmentBMPUpstreams.ListWithDelineationAsDictionaryIncludeTreatmentBMPType(_dbContext);
            var watershedsDict = _dbContext.Watersheds.AsNoTracking().Select(x => new {x.WatershedID, x.WatershedName}).ToDictionary(x => x.WatershedID, x => x.WatershedName);
            var precipitationZonesDict = _dbContext.PrecipitationZones.AsNoTracking()
                .Select(x => new { x.PrecipitationZoneID, x.DesignStormwaterDepthInInches })
                .ToDictionary(x => x.PrecipitationZoneID, x => x.DesignStormwaterDepthInInches);
            var treatmentBMPModeledLandUseAreas = vTreatmentBMPModeledLandUseAreas.List(_dbContext).ToDictionary(x => x.TreatmentBMPID.Value, y => y.Area);
            var vTreatmentBMPModelingAttributes = _dbContext.vTreatmentBMPModelingAttributes.AsNoTracking().ToDictionary(x => x.TreatmentBMPID, y => y);
            var gridSpec = new ViewTreatmentBMPModelingAttributesGridSpec(_linkGenerator, delineationsDict, watershedsDict, precipitationZonesDict, treatmentBMPModeledLandUseAreas, vTreatmentBMPModelingAttributes);
            var treatmentBMPs = TreatmentBMPs.ListModeledOnly(_dbContext).Where(x => stormwaterJurisdictionIDsPersonCanView.Contains(x.StormwaterJurisdictionID)).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<TreatmentBMP>(treatmentBMPs, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpGet("{treatmentBMPPrimaryKey}")]
        [TreatmentBMPEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        public ViewResult EditModelingAttributes([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = TreatmentBMPs.GetByID(_dbContext, treatmentBMPPrimaryKey);
            var treatmentBMPType = TreatmentBMPTypes.GetByID(_dbContext, treatmentBMP.TreatmentBMPTypeID);
            var customAttributes = CustomAttributes.ListByTreatmentBMPID(_dbContext, treatmentBMP.TreatmentBMPID);
            var customAttributeTypePurposeEnum = CustomAttributeTypePurposeEnum.Modeling;
            var customAttributeUpsertDtos = CustomAttributes
                .ListByTreatmentBMPIDWithChangeTracking(_dbContext, treatmentBMPPrimaryKey.PrimaryKeyValue).Where(x =>
                    x.CustomAttributeType.CustomAttributeTypePurposeID ==
                    (int)customAttributeTypePurposeEnum).Select(x => x.AsUpsertDto()).ToList();
            var viewModel = new EditAttributesViewModel(customAttributeUpsertDtos);
            return ViewEditModelingAttributes(viewModel, treatmentBMP, customAttributeTypePurposeEnum, treatmentBMPType, customAttributes);
        }

        [HttpPost("{treatmentBMPPrimaryKey}")]
        [TreatmentBMPEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        public async Task<IActionResult> EditModelingAttributes([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey,
            EditAttributesViewModel viewModel)
        {
            var treatmentBMP = TreatmentBMPs.GetByIDWithChangeTracking(_dbContext, treatmentBMPPrimaryKey);
            var treatmentBMPType = TreatmentBMPTypes.GetByID(_dbContext, treatmentBMP.TreatmentBMPTypeID);
            var customAttributes = CustomAttributes.ListByTreatmentBMPIDWithChangeTracking(_dbContext, treatmentBMP.TreatmentBMPID);
            var customAttributeTypePurposeEnum = CustomAttributeTypePurposeEnum.Modeling;
            if (!ModelState.IsValid)
            {
                return ViewEditModelingAttributes(viewModel, treatmentBMP, customAttributeTypePurposeEnum, treatmentBMPType, customAttributes);
            }

            var allCustomAttributeTypes = CustomAttributeTypes.List(_dbContext);
            var existingCustomAttributes = CustomAttributes.ListByTreatmentBMPIDWithChangeTracking(_dbContext, treatmentBMPPrimaryKey.PrimaryKeyValue).Where(x =>
                x.CustomAttributeType.CustomAttributeTypePurposeID ==
                (int)customAttributeTypePurposeEnum).ToList();
            await viewModel.UpdateModel(_dbContext, treatmentBMP, existingCustomAttributes, allCustomAttributeTypes);
            await _dbContext.SaveChangesAsync();
            var newVTreatmentBMPModelingAttribute = vTreatmentBMPModelingAttributes.GetByTreatmentBMPID(_dbContext, treatmentBMPPrimaryKey.PrimaryKeyValue);
            var missingAttributes = treatmentBMPType.MissingModelingAttributes(newVTreatmentBMPModelingAttribute);
            SetMessageForDisplay(missingAttributes.Any()
                ? "This Treatment BMP is missing required modeling attributes. Modeling Attributes successfully saved."
                : "Modeling Attributes successfully saved.");
            // need to re-execute the model at this node since it was re-parameterized
            await NereidUtilities.MarkTreatmentBMPDirty(treatmentBMP, _dbContext);

            return RedirectToAction(new SitkaRoute<TreatmentBMPController>(_linkGenerator, x => x.Detail(treatmentBMP.PrimaryKey)));
        }

        private ViewResult ViewEditModelingAttributes(EditAttributesViewModel viewModel, TreatmentBMP treatmentBMP, CustomAttributeTypePurposeEnum customAttributeTypePurposeEnum, TreatmentBMPType treatmentBMPType, ICollection<CustomAttribute> customAttributes)
        {
            var missingRequiredAttributes = treatmentBMPType.TreatmentBMPTypeCustomAttributeTypes
                                                .Any(x =>
                                                    x.CustomAttributeType.CustomAttributeTypePurposeID ==
                                                    (int)customAttributeTypePurposeEnum &&
                                                    x.CustomAttributeType.IsRequired &&
                                                    !customAttributes
                                                        .Select(y => y.CustomAttributeTypeID)
                                                        .Contains(x.CustomAttributeTypeID)) ||
                                            customAttributes.Any(x =>
                                                x.CustomAttributeType.CustomAttributeTypePurposeID ==
                                                (int)customAttributeTypePurposeEnum &&
                                                x.CustomAttributeType.IsRequired &&
                                                (x.CustomAttributeValues == null ||
                                                 x.CustomAttributeValues.All(y => string.IsNullOrEmpty(y.AttributeValue)))
                                            );
            var editAttributesViewData = new EditAttributesViewData(treatmentBMPType, customAttributeTypePurposeEnum, missingRequiredAttributes);
            var parentDetailUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(_linkGenerator, x => x.Detail(treatmentBMP));
            var viewData = new EditModelingAttributesViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, treatmentBMP, parentDetailUrl, editAttributesViewData);
            return RazorView<EditModelingAttributes, EditModelingAttributesViewData, EditAttributesViewModel>(viewData, viewModel);
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
            var stormwaterJurisdictionIDs = StormwaterJurisdictionPeople.ListViewableStormwaterJurisdictionIDsByPersonForBMPs(_dbContext, CurrentPerson).ToList();
            BoundingBoxDto boundingBox;
            if (treatmentBMP.LocationPoint != null)
            {
                boundingBox = new BoundingBoxDto(treatmentBMP.LocationPoint4326);
            }
            else
            {
                if (stormwaterJurisdictionIDs.Any())
                {
                    var geometries = StormwaterJurisdictionGeometries .ListByStormwaterJurisdictionIDList(_dbContext, stormwaterJurisdictionIDs).Select(x => x.Geometry4326);
                    boundingBox = new BoundingBoxDto(geometries);
                }
                else
                {
                    boundingBox = new BoundingBoxDto();
                }
            }

            var zoomLevel = MapInitJson.DefaultZoomLevel + 6;

            var mapInitJson =
                new MapInitJson($"BMP_{CurrentPerson.PersonID}_EditBMP", zoomLevel, layerGeoJsons, boundingBox, false)
                {
                    AllowFullScreen = false
                };

            var editLocationViewData = new EditLocationViewData(mapInitJson, mapFormID);
            var viewData = new SetLocationViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, treatmentBMP, editLocationViewData);

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

            BackgroundJob.Enqueue<OCGISService>(x => x.RefreshModelBasins());
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

            BackgroundJob.Enqueue<OCGISService>(x => x.RefreshPrecipitationZones());
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

            BackgroundJob.Enqueue<OCGISService>(x => x.RefreshOCTAPrioritizations());
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
            var treatmentBMP = TreatmentBMPs.GetByID(_dbContext, treatmentBMPPrimaryKey);
            var properties = new Dictionary<string, HtmlString>
            {
                {"Name", UrlTemplate.MakeHrefString(SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(_linkGenerator, x => x.Detail(treatmentBMP)), treatmentBMP.TreatmentBMPName)},
                {"Jurisdiction", UrlTemplate.MakeHrefString(SitkaRoute<JurisdictionController>.BuildUrlFromExpression(_linkGenerator, x => x.Detail(treatmentBMP.StormwaterJurisdiction)), treatmentBMP.StormwaterJurisdiction.GetOrganizationDisplayName())},
                {"Type", new HtmlString(treatmentBMP.TreatmentBMPType.TreatmentBMPTypeName)},
            };
            return Content($"<dl>{string.Join("", properties.Select(x => $"<dt>{x.Key}</dt><dd>{x.Value}</dd>").ToList())}</dl>");
        }

        [HttpGet]
        [NeptuneViewFeature]
        public ViewResult DownloadBMPsToGIS()
        {
            var neptunePage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.ExportBMPInventoryToGIS);
            var stormwaterJurisdictionIDsPersonCanView = StormwaterJurisdictionPeople.ListViewableStormwaterJurisdictionIDsByPersonForBMPs(_dbContext, CurrentPerson);
            var treatmentBmpsCurrentUserCanSee = CurrentPerson.GetTreatmentBmpsPersonCanView(_dbContext, stormwaterJurisdictionIDsPersonCanView);
            var treatmentBmpsInExportCount = treatmentBmpsCurrentUserCanSee.Count;
            var featureClassesInExportCount =
                treatmentBmpsCurrentUserCanSee.Select(x => x.TreatmentBMPTypeID).Distinct().Count() + 1;
            var viewData = new DownloadBMPsToGISViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, neptunePage, treatmentBmpsInExportCount, featureClassesInExportCount);
            return RazorView<DownloadBMPsToGIS, DownloadBMPsToGISViewData>(viewData);
        }

        [HttpGet]
        [NeptuneViewFeature]
        public async Task<FileResult> BMPInventoryExport()
        {
            var treatmentBMPs = _dbContext.vTreatmentBMPGdbExports
                .AsNoTracking()
                .ToList().Where(x => CurrentPerson.IsAssignedToStormwaterJurisdiction(x.StormwaterJurisdictionID)).ToList();
            var treatmentBMPTypes = TreatmentBMPTypes.List(_dbContext).ToDictionary(x => x.TreatmentBMPTypeID);
            var customAttributes = CustomAttributes.List(_dbContext).ToLookup(x => x.TreatmentBMPID);
            var allTreatmentBMPsFeatureCollection = treatmentBMPs.ToExportGeoJsonFeatureCollection();
            var outputLayerName = $"TreatmentBMPs_Export_{DateTime.Now:yyyyMMdd}";

            var jsonSerializerOptions = GeoJsonSerializer.DefaultSerializerOptions;
            var apiRequest = new GdbInputsToGdbRequestDto
            {
                GdbName = outputLayerName
            };
            var gdbInputs = new List<GdbInput>
            {
                new()
                {
                    FileContents = GeoJsonSerializer.SerializeToByteArray(allTreatmentBMPsFeatureCollection, jsonSerializerOptions),
                    LayerName = "AllTreatmentBMPs",
                    CoordinateSystemID = Proj4NetHelper.NAD_83_HARN_CA_ZONE_VI_SRID,
                    GeometryTypeName = "POINT"
                }
            };
            gdbInputs.AddRange(treatmentBMPs.GroupBy(x => x.TreatmentBMPTypeID)
            .Select(grouping =>
            {
                var treatmentBMPType = treatmentBMPTypes[grouping.Key];
                return new GdbInput()
                {
                    FileContents =
                        GeoJsonSerializer.SerializeToByteArray(grouping.ToExportGeoJsonFeatureCollection(treatmentBMPType.TreatmentBMPTypeCustomAttributeTypes, customAttributes),
                            jsonSerializerOptions),
                    LayerName = treatmentBMPType.TreatmentBMPTypeName,
                    CoordinateSystemID = Proj4NetHelper.NAD_83_HARN_CA_ZONE_VI_SRID,
                    GeometryTypeName = "POINT"
                };
            }));
            apiRequest.GdbInputs = gdbInputs;

            var bytes = await gdalApiService.Ogr2OgrInputToGdbAsZip(apiRequest);
            return File(bytes, "application/zip", $"{outputLayerName}.zip");
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
        [RequestSizeLimit(100_000_000_000)]
        [RequestFormLimits(MultipartBodyLengthLimit = 100_000_000_000)]
        public async Task<IActionResult> UploadBMPs(UploadTreatmentBMPsViewModel viewModel)
        {
            var uploadedCSVFile = viewModel.UploadCSV;
            // ReSharper disable once PossibleInvalidOperationException
            var treatmentBMPType = TreatmentBMPTypes.GetByID(_dbContext, viewModel.TreatmentBMPTypeID.Value);
            var treatmentBMPs = TreatmentBMPCsvParserHelper.CSVUpload(_dbContext, uploadedCSVFile.OpenReadStream(), treatmentBMPType, out var errorList, out var customAttributes, out var customAttributeValues);

            if (errorList.Any())
            {
                return ViewUploadBMPs(viewModel, errorList);
            }

            var treatmentBmpsAdded = treatmentBMPs.Where(x => !ModelObjectHelpers.IsRealPrimaryKeyValue(x.PrimaryKey)).ToList();
            var treatmentBmpsUpdated = treatmentBMPs.Where(x => ModelObjectHelpers.IsRealPrimaryKeyValue(x.PrimaryKey)).ToList();

            await _dbContext.TreatmentBMPs.AddRangeAsync(treatmentBmpsAdded);
            await _dbContext.CustomAttributes.AddRangeAsync(customAttributes.Where(x => !ModelObjectHelpers.IsRealPrimaryKeyValue(x.PrimaryKey)));
            await _dbContext.CustomAttributeValues.AddRangeAsync(customAttributeValues.Where(x => !ModelObjectHelpers.IsRealPrimaryKeyValue(x.PrimaryKey)));
            await _dbContext.SaveChangesAsync();

            // Need to re-executed model for updated BMPs since they may have been re-parameterized
            // can safely ignore the new BMPs since they won't have delineations yet
            await NereidUtilities.MarkTreatmentBMPDirty(treatmentBmpsUpdated, _dbContext);

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
            var viewData = new UploadTreatmentBMPsViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, bmpTypes, errorList, neptunePage, SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(_linkGenerator, x => x.UploadBMPs()));
            return RazorView<UploadTreatmentBMPs, UploadTreatmentBMPsViewData, UploadTreatmentBMPsViewModel>(viewData,
                viewModel);
        }

        [HttpGet("{treatmentBMPPrimaryKey}")]
        [NeptuneViewAndRequiresJurisdictionsFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        public PartialViewResult TrashMapAssetPanel([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var viewData = new TrashMapAssetPanelViewData(_linkGenerator, treatmentBMP);
            return RazorPartialView<TrashMapAssetPanel, TrashMapAssetPanelViewData>(viewData);
        }

    }
}
