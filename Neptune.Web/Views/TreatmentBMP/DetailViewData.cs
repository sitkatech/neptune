/*-----------------------------------------------------------------------
<copyright file="DetailViewData.cs" company="Tahoe Regional Planning Agency">
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
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.Shared;
using Microsoft.AspNetCore.Html;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using Neptune.Web.Views.FieldVisit;
using Neptune.Web.Views.Shared.HRUCharacteristics;
using Neptune.Web.Views.Shared.ModeledPerformance;

namespace Neptune.Web.Views.TreatmentBMP
{
    public class DetailViewData : NeptuneViewData
    {
        public bool UserIsAdmin { get; }
        public EFModels.Entities.TreatmentBMP TreatmentBMP { get; }
        public TreatmentBMPDetailMapInitJson MapInitJson { get; }
        public string AddBenchmarkAndThresholdUrl { get; }

        public bool HasSettableBenchmarkAndThresholdValues { get; }
        public bool CurrentPersonCanManage { get; }
        public bool CanManageStormwaterJurisdiction { get; }
        public bool CanEditStormwaterJurisdiction { get; }

        public bool CanEditBenchmarkAndThresholds { get; }

        public FieldVisitGridSpec FieldVisitGridSpec { get; }
        public string FieldVisitGridName { get; }
        public string FieldVisitGridDataUrl { get; }

        public string NewTreatmentBMPDocumentUrl { get; }
        public string NewFundingSourcesUrl { get; }

        public bool OtherTreatmentBmpsExistInSubbasin { get; }

        public ImageCarouselViewData ImageCarouselViewData { get; }
        public string EditTreatmentBMPPerformanceAndModelingAttributesUrl { get; }
        public string EditTreatmentBMPOtherDesignAttributesUrl { get; }
        public string NewFieldVisitUrl { get; }
        public string LocationEditUrl { get; }
        public string DelineationMapUrl { get; }
        public string ManageTreatmentBMPImagesUrl { get; }
        public string DelineationArea { get; }
        public string DelineationStatus { get; }
        public bool DisplayTrashCaptureEffectiveness { get; }

        public readonly string VerifiedUnverifiedUrl;
        public string TrashCaptureEffectiveness { get; }
        public string ChangeTreatmentBMPTypeUrl { get; }
        
        public HRUCharacteristicsViewData? HRUCharacteristicsViewData { get; }
        public string MapServiceUrl { get; }

        public List<HtmlString> DelineationErrors { get; }
        public List<HtmlString> ParameterizationErrors { get; }
        public FieldDefinitionType FieldDefinitionForAverageDivertedFlowrate { get; }
        public FieldDefinitionType FieldDefinitionForAverageTreatmentFlowrate { get; }
        public FieldDefinitionType FieldDefinitionForDesignDryWeatherTreatmentCapacity { get; }
        public FieldDefinitionType FieldDefinitionForDesignLowFlowDiversionCapacity { get; }
        public FieldDefinitionType FieldDefinitionForDesignMediaFiltrationRate { get; }
        public FieldDefinitionType FieldDefinitionForDiversionRate { get; }
        public FieldDefinitionType FieldDefinitionForDrawdownTimeforWQDetentionVolume { get; }
        public FieldDefinitionType FieldDefinitionForEffectiveFootprint { get; }
        public FieldDefinitionType FieldDefinitionForEffectiveRetentionDepth { get; }
        public FieldDefinitionType FieldDefinitionForInfiltrationDischargeRate { get; }
        public FieldDefinitionType FieldDefinitionForInfiltrationSurfaceArea { get; }
        public FieldDefinitionType FieldDefinitionForMediaBedFootprint { get; }
        public FieldDefinitionType FieldDefinitionForMonthsofOperation { get; }
        public FieldDefinitionType FieldDefinitionForPermanentPoolorWetlandVolume { get; }
        public FieldDefinitionType FieldDefinitionForRoutingConfiguration { get; }
        public FieldDefinitionType FieldDefinitionForStorageVolumeBelowLowestOutletElevation { get; }
        public FieldDefinitionType FieldDefinitionForSummerHarvestedWaterDemand { get; }
        public FieldDefinitionType FieldDefinitionForTimeofConcentration { get; }
        public FieldDefinitionType FieldDefinitionForDrawdownTimeForDetentionVolume { get; }
        public FieldDefinitionType FieldDefinitionForTotalEffectiveBMPVolume { get; }
        public FieldDefinitionType FieldDefinitionForTotalEffectiveDrywellBMPVolume { get; }
        public FieldDefinitionType FieldDefinitionForTreatmentRate { get; }
        public FieldDefinitionType FieldDefinitionForUnderlyingHydrologicSoilGroupHSG { get; }
        public FieldDefinitionType FieldDefinitionForUnderlyingInfiltrationRate { get; }
        public FieldDefinitionType FieldDefinitionForUpstreamBMP { get; }
        public FieldDefinitionType FieldDefinitionForWaterQualityDetentionVolume { get; }
        public FieldDefinitionType FieldDefinitionForWettedFootprint { get; }
        public FieldDefinitionType FieldDefinitionForWinterHarvestedWaterDemand { get; }
        public FieldDefinitionType FieldDefinitionForWatershed { get; }
        public FieldDefinitionType FieldDefinitionForDesignStormwaterDepth { get; }
        public FieldDefinitionType FieldDefinitionForDryWeatherFlowOverride { get; }
        public bool HasModelingAttributes { get; }
        public EFModels.Entities.RegionalSubbasinRevisionRequest? OpenRevisionRequest { get; }
        public string EditUpstreamBMPUrl { get; }
        public string RemoveUpstreamBMPUrl { get; }

        public bool IsAnalyzedInModelingModule { get; }
        public bool IsFullyParameterized { get; }

        public ModeledPerformanceViewData ModeledPerformanceViewData { get; }
        public bool CurrentPersonIsAnonymousOrUnassigned { get; }
        public string EditUrl { get; }
        public string DetailUrl { get; }
        public string StormwaterJurisdictionDetailUrl { get; }
        public UrlTemplate<int> DetailUrlTemplate { get; }
        public UrlTemplate<int> FundingEventEditUrlTemplate { get; }
        public UrlTemplate<int> FundingEventDeleteUrlTemplate { get; }
        public UrlTemplate<int> FundingSourceDetailUrlTemplate { get; }
        public UrlTemplate<int> TreatmentBMPDocumentEditUrlTemplate { get; }
        public UrlTemplate<int> TreatmentBMPDocumentDeleteUrlTemplate { get; }
        public UrlTemplate<int> OrganizationDetailUrlTemplate { get; }
        public string TreatmentBMPTypeDetailUrl { get; }
        public string UpstreamBMPDetailUrl { get; }
        public string WaterQualityManagementPlanDetailUrl { get; }
        public UrlTemplate<int> WaterQualityManagementPlanDetailUrlTemplate { get; }
        public UrlTemplate<int> CustomAttributeTypeDetailUrlTemplate { get; }
        public UrlTemplate<int> TreatmentBMPAssessmentObservationTypeDetailUrlTemplate { get; }
        public string OpenRevisionRequestDetailUrl { get; }
        public EFModels.Entities.TreatmentBMPType TreatmentBMPType { get; }
        public List<CustomAttribute> CustomAttributes { get; }
        public List<EFModels.Entities.FundingEvent> FundingEvents { get; }
        public List<EFModels.Entities.TreatmentBMPBenchmarkAndThreshold> TreatmentBMPBenchmarkAndThresholds { get; }
        public List<EFModels.Entities.TreatmentBMPDocument> TreatmentBMPDocuments { get; }
        public EFModels.Entities.Delineation? Delineation { get; }
        public EFModels.Entities.TreatmentBMP? UpstreamestBMP { get; }


        public DetailViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson,
            EFModels.Entities.TreatmentBMP treatmentBMP,
            EFModels.Entities.TreatmentBMPType treatmentBMPType,
            TreatmentBMPDetailMapInitJson mapInitJson, ImageCarouselViewData imageCarouselViewData,
            string verifiedUnverifiedUrl, HRUCharacteristicsViewData hruCharacteristicsViewData, string mapServiceUrl,
            ModeledPerformanceViewData modeledPerformanceViewData, bool otherTreatmentBmpsExistInSubbasin,
            bool hasMissingModelingAttributes, List<CustomAttribute> customAttributes, List<EFModels.Entities.FundingEvent> fundingEvents, List<EFModels.Entities.TreatmentBMPBenchmarkAndThreshold> treatmentBMPBenchmarkAndThresholds, List<EFModels.Entities.TreatmentBMPDocument> treatmentBMPDocuments, EFModels.Entities.Delineation? delineation, ICollection<DelineationOverlap>? delineationOverlapDelineations, EFModels.Entities.TreatmentBMP? upstreamestBMP, EFModels.Entities.RegionalSubbasinRevisionRequest? regionalSubbasinRevisionRequest)
            : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools)
        {
            TreatmentBMP = treatmentBMP;
            TreatmentBMPType = treatmentBMPType;
            Delineation = delineation;
            UpstreamestBMP = upstreamestBMP;
            DelineationArea = delineation?.GetDelineationAreaString();;
            DelineationStatus = delineation.GetDelineationStatus();
            DelineationErrors = CheckForDelineationErrors(delineation, delineationOverlapDelineations);
            ParameterizationErrors = CheckForParameterizationErrors(treatmentBMP, hasMissingModelingAttributes, delineation);

            PageTitle = treatmentBMP.TreatmentBMPName;
            EntityName = $"{FieldDefinitionType.TreatmentBMP.GetFieldDefinitionLabelPluralized()}";
            EntityUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(LinkGenerator, x => x.FindABMP());
            MapInitJson = mapInitJson;
            ImageCarouselViewData = imageCarouselViewData;
            AddBenchmarkAndThresholdUrl = SitkaRoute<TreatmentBMPBenchmarkAndThresholdController>.BuildUrlFromExpression(LinkGenerator, x => x.Instructions(treatmentBMP.TreatmentBMPID));
            HasSettableBenchmarkAndThresholdValues = treatmentBMPType.HasSettableBenchmarkAndThresholdValues();
            CurrentPersonCanManage = new TreatmentBMPManageFeature().HasPermission(currentPerson, TreatmentBMP).HasPermission;
            CurrentPersonIsAnonymousOrUnassigned = currentPerson.IsAnonymousOrUnassigned();
            CanManageStormwaterJurisdiction = currentPerson.CanManageStormwaterJurisdiction(treatmentBMP.StormwaterJurisdictionID);
            CanEditStormwaterJurisdiction = currentPerson.IsAssignedToStormwaterJurisdiction(treatmentBMP.StormwaterJurisdictionID);
            UserIsAdmin = new NeptuneAdminFeature().HasPermissionByPerson(currentPerson);

            CanEditBenchmarkAndThresholds = CurrentPersonCanManage && HasSettableBenchmarkAndThresholdValues;

            OrganizationDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<OrganizationController>.BuildUrlFromExpression(LinkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
            DetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(LinkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
            DetailUrl = DetailUrlTemplate.ParameterReplace(treatmentBMP.TreatmentBMPID);
            EditUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(LinkGenerator, x => x.Edit(treatmentBMP));
            StormwaterJurisdictionDetailUrl = SitkaRoute<JurisdictionController>.BuildUrlFromExpression(LinkGenerator, x => x.Detail(treatmentBMP.StormwaterJurisdictionID));
            FundingSourceDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<FundingSourceController>.BuildUrlFromExpression(LinkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
            TreatmentBMPDocumentEditUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPDocumentController>.BuildUrlFromExpression(LinkGenerator, x => x.Edit(UrlTemplate.Parameter1Int)));
            TreatmentBMPDocumentDeleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPDocumentController>.BuildUrlFromExpression(LinkGenerator, x => x.Delete(UrlTemplate.Parameter1Int)));
            WaterQualityManagementPlanDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
            WaterQualityManagementPlanDetailUrl = WaterQualityManagementPlanDetailUrlTemplate.ParameterReplace(treatmentBMP.TreatmentBMPID);
            TreatmentBMPTypeDetailUrl = SitkaRoute<TreatmentBMPTypeController>.BuildUrlFromExpression(LinkGenerator, x => x.Detail(treatmentBMP.TreatmentBMPTypeID));
            UpstreamBMPDetailUrl = upstreamestBMP == null ? string.Empty : DetailUrlTemplate.ParameterReplace(upstreamestBMP.TreatmentBMPID);

            FieldVisitGridSpec = new FieldVisitGridSpec(CurrentPerson, true, LinkGenerator);
            FieldVisitGridName = "FieldVisit";
            FieldVisitGridDataUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(LinkGenerator, x => x.FieldVisitGridJsonData(treatmentBMP));
            NewFieldVisitUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(LinkGenerator, x => x.New(treatmentBMP));

            NewTreatmentBMPDocumentUrl = SitkaRoute<TreatmentBMPDocumentController>.BuildUrlFromExpression(LinkGenerator, x => x.New(treatmentBMP));
            FundingEventEditUrlTemplate = new UrlTemplate<int>( SitkaRoute<FundingEventController>.BuildUrlFromExpression(LinkGenerator, x => x.Edit(UrlTemplate.Parameter1Int)));
            FundingEventDeleteUrlTemplate = new UrlTemplate<int>( SitkaRoute<FundingEventController>.BuildUrlFromExpression(LinkGenerator, x => x.Delete(UrlTemplate.Parameter1Int)));
            NewFundingSourcesUrl = SitkaRoute<FundingEventController>.BuildUrlFromExpression(LinkGenerator, x => x.New(treatmentBMP));

            //This handles an extreme edge case, but a bmp came back without a regional subbasin
            OtherTreatmentBmpsExistInSubbasin = otherTreatmentBmpsExistInSubbasin;
            CustomAttributes = customAttributes;
            FundingEvents = fundingEvents;
            TreatmentBMPBenchmarkAndThresholds = treatmentBMPBenchmarkAndThresholds;
            TreatmentBMPDocuments = treatmentBMPDocuments;

            EditTreatmentBMPPerformanceAndModelingAttributesUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(LinkGenerator, x => x.EditModelingAttributes(treatmentBMP));
            EditTreatmentBMPOtherDesignAttributesUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(LinkGenerator, x => x.EditOtherDesignAttributes(treatmentBMP));

            LocationEditUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(LinkGenerator, x => x.EditLocation(treatmentBMP));
            ManageTreatmentBMPImagesUrl = SitkaRoute<TreatmentBMPImageController>.BuildUrlFromExpression(LinkGenerator, x => x.ManageTreatmentBMPImages(TreatmentBMP));

            CustomAttributeTypeDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<CustomAttributeTypeController>.BuildUrlFromExpression(LinkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
            TreatmentBMPAssessmentObservationTypeDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPAssessmentObservationTypeController>.BuildUrlFromExpression(LinkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));

            VerifiedUnverifiedUrl = verifiedUnverifiedUrl;
            HRUCharacteristicsViewData = hruCharacteristicsViewData;
            MapServiceUrl = mapServiceUrl;
            ModeledPerformanceViewData = modeledPerformanceViewData;

            DisplayTrashCaptureEffectiveness = TreatmentBMP.TrashCaptureStatusTypeID ==
                                               TrashCaptureStatusType.Partial.TrashCaptureStatusTypeID;

            TrashCaptureEffectiveness = TreatmentBMP.TrashCaptureEffectiveness == null ? "Not Provided" : TreatmentBMP.TrashCaptureEffectiveness + "%";

            DelineationMapUrl = SitkaRoute<DelineationController>.BuildUrlFromExpression(linkGenerator, x => x.DelineationMap(treatmentBMP.TreatmentBMPID));

            ChangeTreatmentBMPTypeUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(LinkGenerator, x => x.ConvertTreatmentBMPType(treatmentBMP));
            
            HasModelingAttributes = TreatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP != null;

            FieldDefinitionForAverageDivertedFlowrate = FieldDefinitionType.AverageDivertedFlowrate;
            FieldDefinitionForAverageTreatmentFlowrate = FieldDefinitionType.AverageTreatmentFlowrate;
            FieldDefinitionForDesignDryWeatherTreatmentCapacity = FieldDefinitionType.DesignDryWeatherTreatmentCapacity;
            FieldDefinitionForDesignLowFlowDiversionCapacity = FieldDefinitionType.DesignLowFlowDiversionCapacity;
            FieldDefinitionForDesignMediaFiltrationRate = FieldDefinitionType.DesignMediaFiltrationRate;
            FieldDefinitionForDiversionRate = FieldDefinitionType.DiversionRate;
            FieldDefinitionForDrawdownTimeforWQDetentionVolume = FieldDefinitionType.DrawdownTimeForWQDetentionVolume;
            FieldDefinitionForEffectiveFootprint = FieldDefinitionType.EffectiveFootprint;
            FieldDefinitionForEffectiveRetentionDepth = FieldDefinitionType.EffectiveRetentionDepth;
            FieldDefinitionForInfiltrationDischargeRate = FieldDefinitionType.InfiltrationDischargeRate;
            FieldDefinitionForInfiltrationSurfaceArea = FieldDefinitionType.InfiltrationSurfaceArea;
            FieldDefinitionForMediaBedFootprint = FieldDefinitionType.MediaBedFootprint;
            FieldDefinitionForMonthsofOperation = FieldDefinitionType.MonthsOperational;
            FieldDefinitionForPermanentPoolorWetlandVolume = FieldDefinitionType.PermanentPoolOrWetlandVolume;
            FieldDefinitionForRoutingConfiguration = FieldDefinitionType.RoutingConfiguration;
            FieldDefinitionForStorageVolumeBelowLowestOutletElevation = FieldDefinitionType.StorageVolumeBelowLowestOutletElevation;
            FieldDefinitionForSummerHarvestedWaterDemand = FieldDefinitionType.SummerHarvestedWaterDemand;
            FieldDefinitionForTimeofConcentration = FieldDefinitionType.TimeOfConcentration;
            FieldDefinitionForDrawdownTimeForDetentionVolume = FieldDefinitionType.DrawdownTimeForDetentionVolume;
            FieldDefinitionForTotalEffectiveBMPVolume = FieldDefinitionType.TotalEffectiveBMPVolume;
            FieldDefinitionForTotalEffectiveDrywellBMPVolume = FieldDefinitionType.TotalEffectiveDrywellBMPVolume;
            FieldDefinitionForTreatmentRate = FieldDefinitionType.TreatmentRate;
            FieldDefinitionForUnderlyingHydrologicSoilGroupHSG = FieldDefinitionType.UnderlyingHydrologicSoilGroupHSG;
            FieldDefinitionForUnderlyingInfiltrationRate = FieldDefinitionType.UnderlyingInfiltrationRate;
            FieldDefinitionForUpstreamBMP = FieldDefinitionType.UpstreamBMP;
            FieldDefinitionForWaterQualityDetentionVolume = FieldDefinitionType.WaterQualityDetentionVolume;
            FieldDefinitionForWettedFootprint = FieldDefinitionType.WettedFootprint;
            FieldDefinitionForWinterHarvestedWaterDemand = FieldDefinitionType.WinterHarvestedWaterDemand;
            FieldDefinitionForWatershed = FieldDefinitionType.Watershed;
            FieldDefinitionForDesignStormwaterDepth = FieldDefinitionType.DesignStormwaterDepth;
            FieldDefinitionForDryWeatherFlowOverride = FieldDefinitionType.DryWeatherFlowOverride;

            OpenRevisionRequest = regionalSubbasinRevisionRequest;
            OpenRevisionRequestDetailUrl = regionalSubbasinRevisionRequest == null
                ? string.Empty
                : SitkaRoute<RegionalSubbasinRevisionRequestController>.BuildUrlFromExpression(linkGenerator,
                    x => x.Detail(regionalSubbasinRevisionRequest.RegionalSubbasinRevisionRequestID));

            EditUpstreamBMPUrl =
                SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(LinkGenerator, x => x.EditUpstreamBMP(treatmentBMP));
            RemoveUpstreamBMPUrl =
                SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(LinkGenerator, x => x.RemoveUpstreamBMP(treatmentBMP));

            IsAnalyzedInModelingModule = treatmentBMPType.IsAnalyzedInModelingModule;
            IsFullyParameterized = treatmentBMP.IsFullyParameterized(delineation);

        }

        private List<HtmlString> CheckForDelineationErrors(EFModels.Entities.Delineation? delineation, ICollection<DelineationOverlap>? delineationOverlapDelineations)
        {
            var delineationErrors = new List<HtmlString>();
            var delineationHasDiscrepancies = delineation?.HasDiscrepancies ?? false;
            if (delineationHasDiscrepancies)
            {
                delineationErrors.Add(new HtmlString("It is not consistent with the most recent Regional Subbasin Layer."));
            }

            var a = new List<TreatmentBMPDisplayDto>();
            if (delineation != null && delineationOverlapDelineations.Any())
            {
                delineationErrors.Add(new HtmlString("It is overlapping with the following Treatment BMP(s): " +
                                                     string.Join(", ",
                                                         delineationOverlapDelineations.Select(x => UrlTemplate.MakeHrefString(DetailUrlTemplate.ParameterReplace(x.OverlappingDelineation.TreatmentBMPID),x.OverlappingDelineation.TreatmentBMP.TreatmentBMPName)))));
            }

            return delineationErrors;
        }

        private List<HtmlString> CheckForParameterizationErrors(EFModels.Entities.TreatmentBMP treatmentBmp, bool hasMissingModelingAttributes, EFModels.Entities.Delineation? delineation)
        {
            var parameterizationErrors = new List<HtmlString>();

            if (delineation == null)
            {
                var linkToDelineationMapOrNot = treatmentBmp.UpstreamBMPID != null
                    ? "Please add a BMP delineation to the Upstream BMP on the Delineation Map or remove the Upstream BMP and add a BMP delineation for this Treatment BMP."
                    : $"<a href='{DelineationMapUrl}'> Please add a BMP delineation on the Delineation Map</a>.";
                parameterizationErrors.Add(new HtmlString(
                    $"A delineation is required for each Treatment BMP to be included in Modeling or Trash result calculations. {linkToDelineationMapOrNot}"));
            }

            if (treatmentBmp.WaterQualityManagementPlan != null && treatmentBmp.WaterQualityManagementPlan.WaterQualityManagementPlanModelingApproach == WaterQualityManagementPlanModelingApproach.Simplified)
            {
                var modelMissingAttributes = new HtmlString($"This BMP is associated with a {UrlTemplate.MakeHrefString(WaterQualityManagementPlanDetailUrlTemplate.ParameterReplace(treatmentBmp.WaterQualityManagementPlan.WaterQualityManagementPlanID), "WQMP")} that is modeled using the simplified approach; this BMP will not be modeled explicitly.");
                parameterizationErrors.Add(modelMissingAttributes);
            }
            else
            {
                var modelMissingAttributes = new HtmlString("This Treatment BMP record is missing fields required to calculate model results. Please provide required Modeling Parameters below.");

                if (hasMissingModelingAttributes)
                {
                    parameterizationErrors.Add(modelMissingAttributes);
                }
            }

            return parameterizationErrors;
        }


        public string DisplayModelingAttributeValue(Func<TreatmentBMPModelingAttribute, double?> modelAttributeValueFunc, string units)
        {
            if (TreatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP == null)
            {
                return null;
            }

            var modelAttributeValue = modelAttributeValueFunc.Invoke(TreatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP);
            return modelAttributeValue == null ? string.Empty : $"{modelAttributeValue.ToGroupedNumeric()} {units}";
        }

        public string DisplayUnderlyingHydrologicSoilGroup()
        {
            return TreatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP != null && TreatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP.UnderlyingHydrologicSoilGroup != null
                ? TreatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP.UnderlyingHydrologicSoilGroup.UnderlyingHydrologicSoilGroupDisplayName : null;
        }

        public string DisplayRoutingConfiguration()
        {
            return TreatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP != null && TreatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP.RoutingConfiguration != null
                ? TreatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP.RoutingConfiguration.RoutingConfigurationDisplayName : null;
        }

        public string DisplayTimeOfConcentration()
        {
            return TreatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP != null && TreatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP.TimeOfConcentration != null
                    ? $"{TreatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP.TimeOfConcentration.TimeOfConcentrationDisplayName} mins" : null;
        }

        public string DisplayMonthsOfOperation()
        {
            return TreatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP?.MonthsOfOperation?.MonthsOfOperationDisplayName;
        }

        public string DisplayDryWeatherFlowOverride()
        {
            return TreatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP != null ? TreatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP?.DryWeatherFlowOverride?.DryWeatherFlowOverrideDisplayName : DryWeatherFlowOverride.No.DryWeatherFlowOverrideDisplayName;
        }
    }
}
