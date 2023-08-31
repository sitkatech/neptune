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
using System.Text.Json;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Neptune.Common;
using Neptune.Common.GeoSpatial;
using Neptune.Common.Mvc;
using Neptune.EFModels;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using Neptune.Web.Common.Models;
using Neptune.Web.Common.MvcResults;
using Neptune.Web.Services.Filters;
using Neptune.Web.Views.Shared.HRUCharacteristics;
using Neptune.Web.Views.Shared.ModeledPerformance;
using Detail = Neptune.Web.Views.TreatmentBMP.Detail;
using DetailViewData = Neptune.Web.Views.TreatmentBMP.DetailViewData;
using Edit = Neptune.Web.Views.TreatmentBMP.Edit;
using EditViewData = Neptune.Web.Views.TreatmentBMP.EditViewData;
using EditViewModel = Neptune.Web.Views.TreatmentBMP.EditViewModel;
using TreatmentBMPAssessmentSummary = Neptune.EFModels.Entities.TreatmentBMPAssessmentSummary;

namespace Neptune.Web.Controllers
{
    public class TreatmentBMPController : NeptuneBaseController<TreatmentBMPController>
    {
        public TreatmentBMPController(NeptuneDbContext dbContext, ILogger<TreatmentBMPController> logger, LinkGenerator linkGenerator) : base(dbContext, logger, linkGenerator)
        {
        }

        public ViewResult FindABMP()
        {
            var stormwaterJurisdictionsPersonCanView = CurrentPerson.GetStormwaterJurisdictionsPersonCanViewWithContext(_dbContext);
            var treatmentBMPs = CurrentPerson.GetTreatmentBmpsPersonCanView(stormwaterJurisdictionsPersonCanView, _dbContext);
            var jurisdictions = stormwaterJurisdictionsPersonCanView.Select(x => x.AsDisplayDto()).ToList();
            var jurisdictionMapLayers = MapInitJsonHelpers.GetJurisdictionMapLayers(_dbContext);
            var mapInitJson = new SearchMapInitJson("StormwaterIndexMap", jurisdictionMapLayers,
                StormwaterMapInitJson.MakeTreatmentBMPLayerGeoJson(treatmentBMPs, false, false))
            {
                JurisdictionLayerGeoJson = jurisdictionMapLayers.Single(x => x.LayerName == MapInitJsonHelpers.CountyCityLayerName)
            };
            var treatmentBMPDisplayDtos = treatmentBMPs.Select(x => x.AsDisplayDto()).ToList();
            var treatmentBMPTypeDisplayDtos = treatmentBMPs.Select(x => x.TreatmentBMPType).Distinct(new HavePrimaryKeyComparer<TreatmentBMPType>()).Select(x => x.AsDisplayDto()).ToList();
            var neptunePage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.FindABMP);
            var viewData = new FindABMPViewData(CurrentPerson, mapInitJson, neptunePage, treatmentBMPDisplayDtos,
                treatmentBMPTypeDisplayDtos, jurisdictions, _linkGenerator, HttpContext);
            return RazorView<FindABMP, FindABMPViewData>(viewData);
        }

        [NeptuneViewFeature]
        public ViewResult Index()
        {
            var neptunePage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.TreatmentBMP);
            var stormwaterJurisdictionsPersonCanView = CurrentPerson.GetStormwaterJurisdictionsPersonCanViewWithContext(_dbContext);
            var treatmentBmpsCurrentUserCanSee = CurrentPerson.GetTreatmentBmpsPersonCanView(stormwaterJurisdictionsPersonCanView, _dbContext);
            var treatmentBmpsInExportCount = treatmentBmpsCurrentUserCanSee.Count;
            var featureClassesInExportCount =
                treatmentBmpsCurrentUserCanSee.Select(x => x.TreatmentBMPTypeID).Distinct().Count() + 1;
            var bulkBMPUploadUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(_linkGenerator, x => x.UploadBMPs());
            var viewData = new IndexViewData(CurrentPerson, neptunePage, treatmentBmpsInExportCount,
                featureClassesInExportCount, bulkBMPUploadUrl, _linkGenerator, HttpContext);
            return RazorView<Views.TreatmentBMP.Index, IndexViewData>(viewData);
        }

        [NeptuneViewFeature]
        public GridJsonNetJObjectResult<vTreatmentBMPDetailed> TreatmentBMPGridJsonData()
        {
            var stormwaterJurisdictionIDsPersonCanView = CurrentPerson.GetStormwaterJurisdictionIDsPersonCanViewWithContext(_dbContext);
            var showDelete = new JurisdictionManageFeature().HasPermissionByPerson(CurrentPerson);
            var showEdit = new JurisdictionEditFeature().HasPermissionByPerson(CurrentPerson);
            var gridSpec = new TreatmentBMPGridSpec(CurrentPerson, showDelete, showEdit, _linkGenerator);
            var treatmentBMPs = _dbContext.vTreatmentBMPDetaileds.Where(x => stormwaterJurisdictionIDsPersonCanView.Contains(x.StormwaterJurisdictionID)).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<vTreatmentBMPDetailed>(treatmentBMPs, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [NeptuneViewFeature]
        public ViewResult TreatmentBMPAssessmentSummary()
        {
            var neptunePage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.TreatmentBMPAssessment);
            var viewData = new TreatmentBMPAssessmentSummaryViewData(CurrentPerson, neptunePage, _linkGenerator, HttpContext);
            return RazorView<Views.TreatmentBMP.TreatmentBMPAssessmentSummary, TreatmentBMPAssessmentSummaryViewData>(
                viewData);
        }

        [NeptuneViewFeature]
        public GridJsonNetJObjectResult<TreatmentBMPAssessmentSummary> TreatmentBMPAssessmentSummaryGridJsonData()
        {
            var gridSpec = new TreatmentBMPAssessmentSummaryGridSpec(_linkGenerator);
            var stormwaterJurisdictionIDsCurrentUserCanEdit = CurrentPerson.GetStormwaterJurisdictionIDsPersonCanViewWithContext(_dbContext);

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
            var mapServiceUrl = "";//todo: NeptuneWebConfiguration.ParcelMapServiceUrl;
            var mapInitJson = new TreatmentBMPDetailMapInitJson("StormwaterDetailMap", treatmentBMP.LocationPoint4326, _dbContext);
            mapInitJson.Layers.Add(
                StormwaterMapInitJson.MakeTreatmentBMPLayerGeoJson(new[] {treatmentBMP}, false, true));
            if (treatmentBMP.Delineation?.DelineationGeometry != null || treatmentBMP.UpstreamBMP?.Delineation?.DelineationGeometry != null)
            {
                mapInitJson.DelineationLayer =
                    StormwaterMapInitJson.MakeTreatmentBMPDelineationLayerGeoJson(treatmentBMP.UpstreamBMP ?? treatmentBMP);
            }

            var carouselImages = TreatmentBMPImages.ListByTreatmentBMPID(_dbContext, treatmentBMP.TreatmentBMPID);
            var imageCarouselViewData = new ImageCarouselViewData(carouselImages, 400, _linkGenerator);
            var verifiedUnverifiedUrl =
                SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(_linkGenerator, x => x.VerifyInventory(treatmentBMPPrimaryKey));

            var entityWithHRUCharacteristics = treatmentBMP.UpstreamBMP ?? treatmentBMP;

            var modeledBMPPerformanceViewData = new ModeledPerformanceViewData(treatmentBMP, CurrentPerson, _linkGenerator, HttpContext);
            var hruCharacteristics = entityWithHRUCharacteristics.GetHRUCharacteristics(_dbContext).ToList();
            var hruCharacteristicsViewData = new HRUCharacteristicsViewData(entityWithHRUCharacteristics, hruCharacteristics);
            var otherTreatmentBmpsExistInSubbasin = treatmentBMP.GetRegionalSubbasin(_dbContext)?.GetTreatmentBMPs(_dbContext).Any(x => x.TreatmentBMPID != treatmentBMP.TreatmentBMPID) ?? false;
            var viewData = new DetailViewData(CurrentPerson, treatmentBMP, mapInitJson, imageCarouselViewData,
                verifiedUnverifiedUrl,hruCharacteristicsViewData, mapServiceUrl, modeledBMPPerformanceViewData, _linkGenerator, HttpContext, otherTreatmentBmpsExistInSubbasin, HasMissingModelingAttributes(treatmentBMP));
            return RazorView<Detail, DetailViewData>(viewData);
        }

        private bool HasMissingModelingAttributes(TreatmentBMP treatmentBMP)
        {
            var bmpTypeIsAnalyzed =
                _dbContext.TreatmentBMPTypes.SingleOrDefault(x =>
                    x.TreatmentBMPTypeID == treatmentBMP.TreatmentBMPTypeID)?.IsAnalyzedInModelingModule ?? false;
            if (!bmpTypeIsAnalyzed)
            {
                return false;
            }
            var bmpModelingType = treatmentBMP.TreatmentBMPType.TreatmentBMPModelingType.ToEnum;
            var bmpModelingAttributes = treatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP;
            if (bmpModelingAttributes != null)
            {
                if (bmpModelingType ==
                    TreatmentBMPModelingTypeEnum.BioinfiltrationBioretentionWithRaisedUnderdrain && (
                        !bmpModelingAttributes.RoutingConfigurationID.HasValue ||
                        (bmpModelingAttributes.RoutingConfigurationID ==
                         (int)RoutingConfigurationEnum.Offline &&
                         !bmpModelingAttributes.DiversionRate.HasValue) ||
                        !bmpModelingAttributes.TotalEffectiveBMPVolume.HasValue ||
                        !bmpModelingAttributes.StorageVolumeBelowLowestOutletElevation.HasValue ||
                        !bmpModelingAttributes.MediaBedFootprint.HasValue ||
                        !bmpModelingAttributes.DesignMediaFiltrationRate.HasValue))
                {
                    return true;
                }

                if ((bmpModelingType == TreatmentBMPModelingTypeEnum.BioretentionWithNoUnderdrain ||
                     bmpModelingType == TreatmentBMPModelingTypeEnum.InfiltrationBasin ||
                     bmpModelingType == TreatmentBMPModelingTypeEnum.InfiltrationTrench ||
                     bmpModelingType == TreatmentBMPModelingTypeEnum.PermeablePavement ||
                     bmpModelingType == TreatmentBMPModelingTypeEnum.UndergroundInfiltration) &&
                    (!bmpModelingAttributes.RoutingConfigurationID.HasValue ||
                     (bmpModelingAttributes.RoutingConfigurationID ==
                      (int)RoutingConfigurationEnum.Offline &&
                      !bmpModelingAttributes.DiversionRate.HasValue) ||
                     !bmpModelingAttributes.TotalEffectiveBMPVolume.HasValue ||
                     !bmpModelingAttributes.InfiltrationSurfaceArea.HasValue ||
                     !bmpModelingAttributes.UnderlyingInfiltrationRate.HasValue))
                {
                    return true;
                }

                if ((bmpModelingType ==
                     TreatmentBMPModelingTypeEnum.BioretentionWithUnderdrainAndImperviousLiner ||
                     bmpModelingType == TreatmentBMPModelingTypeEnum.SandFilters) &&
                    (!bmpModelingAttributes.RoutingConfigurationID.HasValue ||
                     (bmpModelingAttributes.RoutingConfigurationID ==
                      (int)RoutingConfigurationEnum.Offline &&
                      !bmpModelingAttributes.DiversionRate.HasValue) ||
                     !bmpModelingAttributes.TotalEffectiveBMPVolume.HasValue ||
                     !bmpModelingAttributes.MediaBedFootprint.HasValue ||
                     !bmpModelingAttributes.DesignMediaFiltrationRate.HasValue))
                {
                    return true;
                }

                if (bmpModelingType == TreatmentBMPModelingTypeEnum.CisternsForHarvestAndUse &&
                    (!bmpModelingAttributes.RoutingConfigurationID.HasValue ||
                     (bmpModelingAttributes.RoutingConfigurationID ==
                      (int)RoutingConfigurationEnum.Offline &&
                      !bmpModelingAttributes.DiversionRate.HasValue) ||
                     !bmpModelingAttributes.TotalEffectiveBMPVolume.HasValue ||
                     !bmpModelingAttributes.WinterHarvestedWaterDemand.HasValue ||
                     !bmpModelingAttributes.SummerHarvestedWaterDemand.HasValue))
                {
                    return true;
                }

                if ((bmpModelingType == TreatmentBMPModelingTypeEnum.ConstructedWetland ||
                     bmpModelingType == TreatmentBMPModelingTypeEnum.WetDetentionBasin) &&
                    (!bmpModelingAttributes.RoutingConfigurationID.HasValue ||
                     (bmpModelingAttributes.RoutingConfigurationID ==
                      (int)RoutingConfigurationEnum.Offline &&
                      !bmpModelingAttributes.DiversionRate.HasValue) ||
                     !bmpModelingAttributes.PermanentPoolorWetlandVolume.HasValue ||
                     !bmpModelingAttributes.WaterQualityDetentionVolume.HasValue))
                {
                    return true;
                }

                if ((bmpModelingType == TreatmentBMPModelingTypeEnum.DryExtendedDetentionBasin ||
                     bmpModelingType == TreatmentBMPModelingTypeEnum.FlowDurationControlBasin ||
                     bmpModelingType == TreatmentBMPModelingTypeEnum.FlowDurationControlTank) &&
                    (!bmpModelingAttributes.RoutingConfigurationID.HasValue ||
                     (bmpModelingAttributes.RoutingConfigurationID ==
                      (int)RoutingConfigurationEnum.Offline &&
                      !bmpModelingAttributes.DiversionRate.HasValue) ||
                     !bmpModelingAttributes.TotalEffectiveBMPVolume.HasValue ||
                     !bmpModelingAttributes.StorageVolumeBelowLowestOutletElevation.HasValue ||
                     !bmpModelingAttributes.EffectiveFootprint.HasValue ||
                     !bmpModelingAttributes.DrawdownTimeforWQDetentionVolume.HasValue))
                {
                    return true;
                }

                if (bmpModelingType == TreatmentBMPModelingTypeEnum.DryWeatherTreatmentSystems &&
                    (!bmpModelingAttributes.DesignDryWeatherTreatmentCapacity.HasValue &&
                     !bmpModelingAttributes.AverageTreatmentFlowrate.HasValue))
                {
                    return true;
                }

                if (bmpModelingType == TreatmentBMPModelingTypeEnum.Drywell &&
                    (!bmpModelingAttributes.RoutingConfigurationID.HasValue ||
                     (bmpModelingAttributes.RoutingConfigurationID ==
                      (int)RoutingConfigurationEnum.Offline &&
                      !bmpModelingAttributes.DiversionRate.HasValue) ||
                     !bmpModelingAttributes.TotalEffectiveDrywellBMPVolume.HasValue ||
                     !bmpModelingAttributes.InfiltrationDischargeRate.HasValue))
                {
                    return true;
                }

                if ((bmpModelingType == TreatmentBMPModelingTypeEnum.HydrodynamicSeparator ||
                     bmpModelingType == TreatmentBMPModelingTypeEnum.ProprietaryBiotreatment ||
                     bmpModelingType == TreatmentBMPModelingTypeEnum.ProprietaryTreatmentControl) &&
                    !bmpModelingAttributes.TreatmentRate.HasValue)
                {
                    return true;
                }

                if (bmpModelingType == TreatmentBMPModelingTypeEnum.LowFlowDiversions &&
                    (!bmpModelingAttributes.DesignLowFlowDiversionCapacity.HasValue &&
                     !bmpModelingAttributes.AverageDivertedFlowrate.HasValue))
                {
                    return true;
                }

                if ((bmpModelingType == TreatmentBMPModelingTypeEnum.VegetatedFilterStrip ||
                     bmpModelingType == TreatmentBMPModelingTypeEnum.VegetatedSwale) &&
                    (!bmpModelingAttributes.RoutingConfigurationID.HasValue ||
                     (bmpModelingAttributes.RoutingConfigurationID ==
                      (int)RoutingConfigurationEnum.Offline &&
                      !bmpModelingAttributes.DiversionRate.HasValue) ||
                     !bmpModelingAttributes.TreatmentRate.HasValue ||
                     !bmpModelingAttributes.WettedFootprint.HasValue ||
                     !bmpModelingAttributes.EffectiveRetentionDepth.HasValue))
                {
                    return true;
                }
            }
            else
            {
                return true;
            }

            return false;
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
            viewModel.UpdateModel(treatmentBMP, CurrentPerson, _dbContext);
            treatmentBMP.SetTreatmentBMPPointInPolygonDataByLocationPoint(treatmentBMP.LocationPoint, _dbContext);
            await _dbContext.TreatmentBMPs.AddAsync(treatmentBMP);
            await _dbContext.SaveChangesAsync();

            SetMessageForDisplay("Treatment BMP successfully created.");

            return RedirectToAction(new SitkaRoute<TreatmentBMPController>(_linkGenerator, c => c.Detail(treatmentBMP.PrimaryKey)));
        }

        private ViewResult ViewNew(NewViewModel viewModel)
        {
            var treatmentBMP = ModelObjectHelpers.IsRealPrimaryKeyValue(viewModel.TreatmentBMPID)
                ? TreatmentBMPs.GetByID(_dbContext, viewModel.TreatmentBMPID)
                : null;
            var stormwaterJurisdictions = CurrentPerson.GetStormwaterJurisdictionsPersonCanViewWithContext(_dbContext);
            var treatmentBMPTypes = TreatmentBMPTypes.List(_dbContext);
            var organizations = Organizations.List(_dbContext);
            var layerGeoJsons = MapInitJsonHelpers.GetJurisdictionMapLayers(_dbContext).ToList();
            var boundingBox = treatmentBMP?.LocationPoint != null
                ? new BoundingBoxDto(treatmentBMP.LocationPoint4326)
                : stormwaterJurisdictions.Any()
                    ? new BoundingBoxDto(stormwaterJurisdictions
                        .Select(x => x.StormwaterJurisdictionGeometry.Geometry4326))
                    : new BoundingBoxDto();
            var zoomLevel = CurrentPerson.IsAdministrator() ? MapInitJson.DefaultZoomLevel : MapInitJson.DefaultZoomLevel + 2;

            var mapInitJson =
                new MapInitJson($"BMP_{CurrentPerson.PersonID}_EditBMP", zoomLevel, layerGeoJsons, boundingBox, false)
                {
                    AllowFullScreen = false
                };
            var editLocationViewData = new Views.Shared.Location.EditLocationViewData(CurrentPerson, treatmentBMP,
                mapInitJson, "treatmentBMPLocation", _linkGenerator, HttpContext);
            var treatmentBMPStormwaterJurisdictionIDs = treatmentBMP != null
                ? new List<int> {treatmentBMP.StormwaterJurisdictionID}
                : stormwaterJurisdictions.Select(x => x.StormwaterJurisdictionID).ToList();
            var waterQualityManagementPlans = _dbContext.WaterQualityManagementPlans
                .Where(x => treatmentBMPStormwaterJurisdictionIDs.Contains(x.StormwaterJurisdictionID)).ToList();

            if (ModelObjectHelpers.IsRealPrimaryKeyValue(viewModel.StormwaterJurisdictionID))
            {
                stormwaterJurisdictions.Add(StormwaterJurisdictions.GetByID(_dbContext, viewModel.StormwaterJurisdictionID));
                stormwaterJurisdictions = stormwaterJurisdictions.Distinct().ToList();
            }

            var viewData = new NewViewData(CurrentPerson, treatmentBMP, stormwaterJurisdictions, treatmentBMPTypes,
                organizations, editLocationViewData, waterQualityManagementPlans, TreatmentBMPLifespanType.All,
                TrashCaptureStatusType.All, SizingBasisType.All, _linkGenerator, HttpContext);
            return RazorView<New, NewViewData, NewViewModel>(viewData, viewModel);
        }

        [HttpGet("{treatmentBMPPrimaryKey}")]
        [TreatmentBMPEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        public ViewResult Edit([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var viewModel = new EditViewModel(treatmentBMP);
            return ViewEdit(viewModel);
        }

        [HttpPost("{treatmentBMPPrimaryKey}")]
        [TreatmentBMPEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        public async Task<IActionResult> Edit([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey, EditViewModel viewModel)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel);
            }

            treatmentBMP.MarkInventoryAsProvisionalIfNonManager(CurrentPerson);
            viewModel.UpdateModel(treatmentBMP, CurrentPerson, _dbContext);
            await _dbContext.SaveChangesAsync();

            SetMessageForDisplay("Treatment BMP successfully saved.");

            return RedirectToAction(new SitkaRoute<TreatmentBMPController>(_linkGenerator, c => c.Detail(treatmentBMP.PrimaryKey)));
        }

        private ViewResult ViewEdit(EditViewModel viewModel)
        {
            var treatmentBMP = ModelObjectHelpers.IsRealPrimaryKeyValue(viewModel.TreatmentBMPID)
                ? TreatmentBMPs.GetByID(_dbContext, viewModel.TreatmentBMPID)
                : null;
            var stormwaterJurisdictions = CurrentPerson.GetStormwaterJurisdictionsPersonCanViewWithContext(_dbContext);
            var treatmentBMPTypes = TreatmentBMPTypes.List(_dbContext);
            var organizations = Organizations.List(_dbContext);
            var waterQualityManagementPlans = _dbContext.WaterQualityManagementPlans
                .Where(x => x.StormwaterJurisdictionID == treatmentBMP.StormwaterJurisdictionID)
                .ToList();

            if (ModelObjectHelpers.IsRealPrimaryKeyValue(viewModel.StormwaterJurisdictionID))
            {
                var currentJurisdiction = StormwaterJurisdictions.GetByID(_dbContext, viewModel.StormwaterJurisdictionID ?? ModelObjectHelpers.NotYetAssignedID);
                if (!stormwaterJurisdictions.Contains(currentJurisdiction))
                {
                    stormwaterJurisdictions.Add(currentJurisdiction);
                }
            }

            var viewData = new EditViewData(CurrentPerson, treatmentBMP, stormwaterJurisdictions, treatmentBMPTypes,
                organizations, waterQualityManagementPlans, TreatmentBMPLifespanType.All, TrashCaptureStatusType.All,
                SizingBasisType.All, _linkGenerator, HttpContext);
            return RazorView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }

        [HttpGet("{treatmentBMPPrimaryKey}")]
        [TreatmentBMPEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        public PartialViewResult EditUpstreamBMP([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var viewModel = new EditUpstreamBMPViewModel(treatmentBMP);
            
            return ViewEditUpstreamBMP(treatmentBMP, viewModel);
        }

        [HttpPost("{treatmentBMPPrimaryKey}")]
        [TreatmentBMPEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        public ActionResult EditUpstreamBMP([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey, EditUpstreamBMPViewModel viewModel)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEditUpstreamBMP(treatmentBMP, viewModel);
            }

            viewModel.UpdateModel(treatmentBMP);

            treatmentBMP.Delineation?.DeleteDelineation(_dbContext);

            // need to re-execute the Nereid model for this node since source of run-off was changed.
            NereidUtilities.MarkTreatmentBMPDirty(treatmentBMP, _dbContext);

            SetMessageForDisplay("Upstream BMP successfully updated");
            return new ModalDialogFormJsonResult(SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(_linkGenerator, x => x.Detail(treatmentBMP)));
        }

        [HttpPost("{treatmentBMPPrimaryKey}")]
        [TreatmentBMPEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        public async Task<IActionResult> RemoveUpstreamBMP([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;

            treatmentBMP.RemoveUpstreamBMP();

            await _dbContext.SaveChangesAsync();
            SetMessageForDisplay("Upstream BMP successfully removed");

            // need to re-execute the Nereid model here since source of run-off was removed.
            NereidUtilities.MarkTreatmentBMPDirty(treatmentBMP, _dbContext);

            return RedirectToAction(new SitkaRoute<TreatmentBMPController>(_linkGenerator, c => c.Detail(treatmentBMP.PrimaryKey)));
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
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var viewModel = new ConvertTreatmentBMPTypeViewModel();
            return ViewConvertTreatmentBMPTypeTreatmentBMP(treatmentBMP, viewModel);
        }

        [HttpPost("{treatmentBMPPrimaryKey}")]
        [TreatmentBMPManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        public async Task<IActionResult> ConvertTreatmentBMPType([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey,
            ConvertTreatmentBMPTypeViewModel viewModel)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
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
                var treatmentBMPTypeCustomAttributeType = newTreatmentBMPType.TreatmentBMPTypeCustomAttributeTypes.Single(c => c.CustomAttributeTypeID == customAttribute.CustomAttributeTypeID);
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
            var treatmentBMPTypes = _dbContext.TreatmentBMPTypes.Where(x => x.TreatmentBMPTypeID != treatmentBMP.TreatmentBMPTypeID).ToList();
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
            var delineationGeometry = treatmentBMP.Delineation?.DelineationGeometry;
            var isDelineationDistributed = treatmentBMP.Delineation?.DelineationType == DelineationType.Distributed;

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
        public PartialViewResult BulkDeleteTreatmentBMPs(BulkDeleteTreatmentBMPsViewModel viewModel)
        {
            var treatmentBMPs = new List<TreatmentBMP>();

            if (viewModel.TreatmentBMPIDList != null)
            {
                treatmentBMPs = _dbContext.TreatmentBMPs.Where(x => viewModel.TreatmentBMPIDList.Contains(x.TreatmentBMPID)).ToList();
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
                var treatmentBMPs = _dbContext.TreatmentBMPs.Where(x => viewModel.TreatmentBMPIDList.Contains(x.TreatmentBMPID)).ToList();
                treatmentBMPDisplayNames = treatmentBMPs.Select(x => x.TreatmentBMPName).ToList();

                foreach (var treatmentBMP in treatmentBMPs)
                {
                    var delineationGeometry = treatmentBMP.Delineation?.DelineationGeometry;
                    var isDelineationDistributed = treatmentBMP.Delineation?.DelineationType == DelineationType.Distributed;

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
                    if (isDelineationDistributed && delineationGeometry != null)
                    {
                        ModelingEngineUtilities.QueueLGURefreshForArea(delineationGeometry, null, _dbContext);
                    }
                }

            }

            SetMessageForDisplay($"Successfully deleted Treatment BMPs: {string.Join(", ", treatmentBMPDisplayNames)}");
            return new ModalDialogFormJsonResult();
            
        }

        [HttpGet("{treatmentBMPPrimaryKey}")]
        public PartialViewResult SummaryForMap([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var viewData = new SummaryForMapViewData(CurrentPerson, treatmentBMP, _linkGenerator, HttpContext);
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
            var stormwaterJurisdictionsPersonCanView = CurrentPerson.GetStormwaterJurisdictionsPersonCanViewWithContext(_dbContext);
            var allTreatmentBMPsMatchingSearchString = CurrentPerson.GetTreatmentBmpsPersonCanView(stormwaterJurisdictionsPersonCanView, _dbContext)
                .Where(x => treatmentBMPTypeIDs.Contains(x.TreatmentBMPTypeID) &&
                            stormwaterJurisdictionIDs.Contains(x.StormwaterJurisdictionID) &&
                            x.TreatmentBMPName.ToLower().Contains(searchString)).ToList();

            var geoJSONSerializerOptions = GeoJsonSerializer.CreateGeoJSONSerializerOptions();
            var listItems = allTreatmentBMPsMatchingSearchString.OrderBy(x => x.TreatmentBMPName).Take(20).Select(bmp =>
            {
                var locationPoint4326 = bmp.LocationPoint4326;
                var treatmentBMPMapSummaryData = new SearchMapSummaryData(bmp.GetMapSummaryUrl(), locationPoint4326,
                    locationPoint4326.Coordinate.Y,
                    locationPoint4326.Coordinate.X,
                    bmp.TreatmentBMPID); // X/YCoordinate will never be null
                var listItem = new SelectListItem(bmp.TreatmentBMPName,
                    JsonSerializer.Serialize(treatmentBMPMapSummaryData, geoJSONSerializerOptions));
                return listItem;
            }).ToList();

            return Json(listItems);
        }

        [HttpGet("{treatmentBMPPrimaryKey}")]
        [TreatmentBMPEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        public ViewResult EditAttributes([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey,
            CustomAttributeTypePurposePrimaryKey customAttributeTypePurposePrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var customAttributeTypePurpose = customAttributeTypePurposePrimaryKey.EntityObject;
            var viewModel = new EditAttributesViewModel(treatmentBMP, customAttributeTypePurpose);
            return ViewEditAttributes(viewModel, treatmentBMP, customAttributeTypePurpose);
        }

        [HttpPost("{treatmentBMPPrimaryKey}")]
        [TreatmentBMPEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        public async Task<IActionResult> EditAttributes([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey,
            CustomAttributeTypePurposePrimaryKey customAttributeTypePurposePrimaryKey,
            EditAttributesViewModel viewModel)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var customAttributeTypePurpose = customAttributeTypePurposePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEditAttributes(viewModel, treatmentBMP, customAttributeTypePurpose);
            }

            var allCustomAttributeTypes = _dbContext.CustomAttributeTypes.ToList();
            viewModel.UpdateModel(treatmentBMP, CurrentPerson, customAttributeTypePurpose, allCustomAttributeTypes, _dbContext);
            treatmentBMP.MarkInventoryAsProvisionalIfNonManager(CurrentPerson);
            await _dbContext.SaveChangesAsync();
            SetMessageForDisplay("Custom Attributes successfully saved.");
            return RedirectToAction(new SitkaRoute<TreatmentBMPController>(_linkGenerator, c => c.Detail(treatmentBMP.PrimaryKey)));
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
            var viewData = new EditAttributesViewData(CurrentPerson, treatmentBMP, customAttributeTypePurpose, missingRequiredAttributes, _linkGenerator, HttpContext);
            return RazorView<EditAttributes, EditAttributesViewData, EditAttributesViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [NeptuneViewFeature]
        public ViewResult ViewTreatmentBMPModelingAttributes()
        {
            var neptunePage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.ViewTreatmentBMPModelingAttributes);
            var viewData = new ViewTreatmentBMPModelingAttributesViewData(CurrentPerson, neptunePage, _linkGenerator, HttpContext);
            return RazorView<ViewTreatmentBMPModelingAttributes, ViewTreatmentBMPModelingAttributesViewData>(viewData);
        }

        [NeptuneViewFeature]
        public GridJsonNetJObjectResult<vViewTreatmentBMPModelingAttribute> ViewTreatmentBMPModelingAttributesGridJsonData()
        {
            var stormwaterJurisdictionIDsPersonCanView = CurrentPerson.GetStormwaterJurisdictionIDsPersonCanViewWithContext(_dbContext);
            var gridSpec = new ViewTreatmentBMPModelingAttributesGridSpec(_linkGenerator);
            var treatmentBMPs = _dbContext.vViewTreatmentBMPModelingAttributes.Where(x => stormwaterJurisdictionIDsPersonCanView.Contains(x.StormwaterJurisdictionID)).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<vViewTreatmentBMPModelingAttribute>(treatmentBMPs, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpGet("{treatmentBMPPrimaryKey}")]
        [TreatmentBMPEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        public ViewResult EditModelingAttributes([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
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
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
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
            return RedirectToAction(new SitkaRoute<TreatmentBMPController>(_linkGenerator, c => c.Detail(treatmentBMP.PrimaryKey)));
        }

        private ViewResult ViewEditModelingAttributes(EditModelingAttributesViewModel viewModel, TreatmentBMP treatmentBMP)
        {
            var routingConfigurations = RoutingConfiguration.All.OrderBy(x => x.RoutingConfigurationDisplayName);
            var timeOfConcentrations = TimeOfConcentration.All.OrderBy(x => x.TimeOfConcentrationID);
            var underlyingHydrologicSoilGroups = UnderlyingHydrologicSoilGroup.All.OrderBy(x => x.UnderlyingHydrologicSoilGroupID);
            var monthsOfOperation = MonthsOfOperation.All;
            var dryWeatherFlowOverride = DryWeatherFlowOverride.All;
            var viewData = new EditModelingAttributesViewData(CurrentPerson, treatmentBMP, routingConfigurations, timeOfConcentrations, underlyingHydrologicSoilGroups, monthsOfOperation, dryWeatherFlowOverride, _linkGenerator, HttpContext);
            return RazorView<EditModelingAttributes, EditModelingAttributesViewData, EditModelingAttributesViewModel>(viewData, viewModel);
        }

        [HttpGet("{treatmentBMPPrimaryKey}")]
        [TreatmentBMPEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        public ViewResult EditLocation([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var viewModel = new EditLocationViewModel(treatmentBMP);
            return ViewEditLocation(treatmentBMP, viewModel);
        }

        private ViewResult ViewEditLocation(TreatmentBMP treatmentBMP, EditLocationViewModel viewModel)
        {
            var mapFormID = "treatmentBMPEditLocation";
            var layerGeoJsons = MapInitJsonHelpers.GetJurisdictionMapLayers(_dbContext).ToList();
            var stormwaterJurisdictions = CurrentPerson.GetStormwaterJurisdictionsPersonCanViewWithContext(_dbContext);
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

            var viewData = new EditLocationViewData(CurrentPerson, treatmentBMP, mapInitJson, mapFormID, _linkGenerator, HttpContext);

            return RazorView<EditLocation, EditLocationViewData, EditLocationViewModel>(viewData, viewModel);
        }

        [HttpPost("{treatmentBMPPrimaryKey}")]
        [TreatmentBMPEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        public async Task<IActionResult> EditLocation([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey, EditLocationViewModel viewModel)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;

            if (!ModelState.IsValid)
            {
                return ViewEditLocation(treatmentBMP, viewModel);
            }

            treatmentBMP.MarkInventoryAsProvisionalIfNonManager(CurrentPerson);

            viewModel.UpdateModel(treatmentBMP, CurrentPerson, _dbContext);
            treatmentBMP.SetTreatmentBMPPointInPolygonDataByLocationPoint(treatmentBMP.LocationPoint, _dbContext);

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
        public async Task<JsonResult> EditLocationFromDelineationMap([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey, EditLocationViewModel viewModel)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;

            if (!ModelState.IsValid)
            {
                throw new SitkaDisplayErrorException("The location update parameters were invalid.");
            }

            treatmentBMP.MarkInventoryAsProvisionalIfNonManager(CurrentPerson);

            viewModel.UpdateModel(treatmentBMP, CurrentPerson, _dbContext);
            treatmentBMP.SetTreatmentBMPPointInPolygonDataByLocationPoint(treatmentBMP.LocationPoint, _dbContext);

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
                    treatmentBMP.StormwaterJurisdiction.GetDisplayNameAsDetailUrl()
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
        public ActionResult UploadBMPs(UploadTreatmentBMPsViewModel viewModel)
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

            _dbContext.TreatmentBMPs.AddRange(treatmentBmpsAdded);
            _dbContext.CustomAttributes.AddRange(customAttributes.Where(x => !ModelObjectHelpers.IsRealPrimaryKeyValue(x.PrimaryKey)));
            _dbContext.CustomAttributeValues.AddRange(customAttributeValues.Where(x => !ModelObjectHelpers.IsRealPrimaryKeyValue(x.PrimaryKey)));
            _dbContext.TreatmentBMPModelingAttributes.AddRange(modelingAttributes.Where(x => !ModelObjectHelpers.IsRealPrimaryKeyValue(x.PrimaryKey)));
            _dbContext.SaveChanges();

            // Need to re-executed model for updated BMPs since they may have been re-parameterized
            // can safely ignore the new BMPs since they won't have delineations yet
            NereidUtilities.MarkTreatmentBMPDirty(treatmentBmpsUpdated, _dbContext);

            var message = $"Upload Successful: {treatmentBmpsAdded.Count} records added, {treatmentBmpsUpdated.Count} records updated!";
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
            var viewData = new UploadTreatmentBMPsViewData(CurrentPerson, bmpTypes, errorList, neptunePage,
                SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(_linkGenerator, x => x.UploadBMPs()), _linkGenerator, HttpContext);
            return RazorView<UploadTreatmentBMPs, UploadTreatmentBMPsViewData, UploadTreatmentBMPsViewModel>(viewData,
                viewModel);
        }
    }
}
