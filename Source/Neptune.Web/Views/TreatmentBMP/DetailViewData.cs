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

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using LtInfo.Common;
using LtInfo.Common.DbSpatial;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.FieldVisit;
using Neptune.Web.Views.Shared;
using Neptune.Web.Views.Shared.HRUCharacteristics;

namespace Neptune.Web.Views.TreatmentBMP
{
    public class DetailViewData : NeptuneViewData
    {
        public bool UserIsAdmin { get; }
        public Models.TreatmentBMP TreatmentBMP { get; }
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
        public string HRURefreshUrl { get; }

        public HRUCharacteristicsViewData HRUCharacteristicsViewData { get; }
        public string MapServiceUrl { get; }

        public List<HtmlString> DelineationErrors { get; }
        public Models.FieldDefinition FieldDefinitionForAverageDivertedFlowrate { get; }
        public Models.FieldDefinition FieldDefinitionForAverageTreatmentFlowrate { get; }
        public Models.FieldDefinition FieldDefinitionForDesignDryWeatherTreatmentCapacity { get; }
        public Models.FieldDefinition FieldDefinitionForDesignLowFlowDiversionCapacity { get; }
        public Models.FieldDefinition FieldDefinitionForDesignMediaFiltrationRate { get; }
        public Models.FieldDefinition FieldDefinitionForDesignResidenceTimeforPermanentPool { get; }
        public Models.FieldDefinition FieldDefinitionForDiversionRate { get; }
        public Models.FieldDefinition FieldDefinitionForDrawdownTimeforWQDetentionVolume { get; }
        public Models.FieldDefinition FieldDefinitionForEffectiveFootprint { get; }
        public Models.FieldDefinition FieldDefinitionForEffectiveRetentionDepth { get; }
        public Models.FieldDefinition FieldDefinitionForInfiltrationDischargeRate { get; }
        public Models.FieldDefinition FieldDefinitionForInfiltrationSurfaceArea { get; }
        public Models.FieldDefinition FieldDefinitionForMediaBedFootprint { get; }
        public Models.FieldDefinition FieldDefinitionForMonthsofOperation { get; }
        public Models.FieldDefinition FieldDefinitionForPermanentPoolorWetlandVolume { get; }
        public Models.FieldDefinition FieldDefinitionForRoutingConfiguration { get; }
        public Models.FieldDefinition FieldDefinitionForStorageVolumeBelowLowestOutletElevation { get; }
        public Models.FieldDefinition FieldDefinitionForSummerHarvestedWaterDemand { get; }
        public Models.FieldDefinition FieldDefinitionForTimeofConcentration { get; }
        public Models.FieldDefinition FieldDefinitionForTotalDrawdownTime { get; }
        public Models.FieldDefinition FieldDefinitionForTotalEffectiveBMPVolume { get; }
        public Models.FieldDefinition FieldDefinitionForTotalEffectiveDrywellBMPVolume { get; }
        public Models.FieldDefinition FieldDefinitionForTreatmentRate { get; }
        public Models.FieldDefinition FieldDefinitionForUnderlyingHydrologicSoilGroupHSG { get; }
        public Models.FieldDefinition FieldDefinitionForUnderlyingInfiltrationRate { get; }
        public Models.FieldDefinition FieldDefinitionForUpstreamBMP { get; }
        public Models.FieldDefinition FieldDefinitionForWaterQualityDetentionVolume { get; }
        public Models.FieldDefinition FieldDefinitionForWettedFootprint { get; }
        public Models.FieldDefinition FieldDefinitionForWinterHarvestedWaterDemand { get; }

        public DetailViewData(Person currentPerson, Models.TreatmentBMP treatmentBMP,
            TreatmentBMPDetailMapInitJson mapInitJson, ImageCarouselViewData imageCarouselViewData,
            string verifiedUnverifiedUrl, HRUCharacteristicsViewData hruCharacteristicsViewData, string mapServiceUrl)
            : base(currentPerson, NeptuneArea.OCStormwaterTools)
        {
            TreatmentBMP = treatmentBMP;
            PageTitle = treatmentBMP.TreatmentBMPName;
            EntityName = $"{Models.FieldDefinition.TreatmentBMP.GetFieldDefinitionLabelPluralized()}";
            EntityUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(x => x.FindABMP());
            MapInitJson = mapInitJson;
            ImageCarouselViewData = imageCarouselViewData;
            AddBenchmarkAndThresholdUrl = SitkaRoute<TreatmentBMPBenchmarkAndThresholdController>.BuildUrlFromExpression(x => x.Instructions(treatmentBMP.TreatmentBMPID));
            HasSettableBenchmarkAndThresholdValues = TreatmentBMP.HasSettableBenchmarkAndThresholdValues();
            CurrentPersonCanManage = new TreatmentBMPManageFeature().HasPermission(currentPerson, TreatmentBMP).HasPermission;
            CanManageStormwaterJurisdiction = currentPerson.CanManageStormwaterJurisdiction(treatmentBMP.StormwaterJurisdiction);
            CanEditStormwaterJurisdiction = currentPerson.CanEditStormwaterJurisdiction(treatmentBMP.StormwaterJurisdiction);
            UserIsAdmin = new NeptuneAdminFeature().HasPermissionByPerson(currentPerson);

            CanEditBenchmarkAndThresholds = CurrentPersonCanManage && HasSettableBenchmarkAndThresholdValues;

            FieldVisitGridSpec = new FieldVisitGridSpec(CurrentPerson, true);
            FieldVisitGridName = "FieldVisit";
            FieldVisitGridDataUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.FieldVisitGridJsonData(treatmentBMP));
            NewFieldVisitUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.New(treatmentBMP));

            NewTreatmentBMPDocumentUrl = SitkaRoute<TreatmentBMPDocumentController>.BuildUrlFromExpression(x => x.New(treatmentBMP));
            NewFundingSourcesUrl = SitkaRoute<FundingEventController>.BuildUrlFromExpression(x => x.NewFundingEvent(treatmentBMP));
            EditTreatmentBMPPerformanceAndModelingAttributesUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(x => x.EditModelingAttributes(treatmentBMP));
            EditTreatmentBMPOtherDesignAttributesUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(x => x.EditAttributes(treatmentBMP, CustomAttributeTypePurpose.OtherDesignAttributes));

            LocationEditUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(x => x.EditLocation(treatmentBMP));
            ManageTreatmentBMPImagesUrl = SitkaRoute<TreatmentBMPImageController>.BuildUrlFromExpression(x => x.ManageTreatmentBMPImages(TreatmentBMP));

            VerifiedUnverifiedUrl = verifiedUnverifiedUrl;
            HRUCharacteristicsViewData = hruCharacteristicsViewData;
            MapServiceUrl = mapServiceUrl;

            DelineationArea = (TreatmentBMP.Delineation?.DelineationGeometry.Area * DbSpatialHelper.SquareMetersToAcres)?.ToString("0.00") ?? "-";
            DelineationStatus = TreatmentBMP.Delineation?.IsVerified == false ? "Provisional" : "Verified";

            DelineationErrors = CheckForDelineationErrors(treatmentBMP);

            DisplayTrashCaptureEffectiveness = TreatmentBMP.TrashCaptureStatusTypeID ==
                                               TrashCaptureStatusType.Partial.TrashCaptureStatusTypeID;

            TrashCaptureEffectiveness = TreatmentBMP.TrashCaptureEffectiveness == null ? "Not Provided" : TreatmentBMP.TrashCaptureEffectiveness + "%";

            DelineationMapUrl = treatmentBMP.GetDelineationMapUrl();

            ChangeTreatmentBMPTypeUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(x => x.ConvertTreatmentBMPType(treatmentBMP));

            HRURefreshUrl =
                SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(
                    x => x.RefreshHRUCharacteristics(treatmentBMP));

            FieldDefinitionForAverageDivertedFlowrate = Models.FieldDefinition.AverageDivertedFlowrate;
            FieldDefinitionForAverageTreatmentFlowrate = Models.FieldDefinition.AverageTreatmentFlowrate;
            FieldDefinitionForDesignDryWeatherTreatmentCapacity = Models.FieldDefinition.DesignDryWeatherTreatmentCapacity;
            FieldDefinitionForDesignLowFlowDiversionCapacity = Models.FieldDefinition.DesignLowFlowDiversionCapacity;
            FieldDefinitionForDesignMediaFiltrationRate = Models.FieldDefinition.DesignMediaFiltrationRate;
            FieldDefinitionForDesignResidenceTimeforPermanentPool = Models.FieldDefinition.DesignResidenceTimeforPermanentPool;
            FieldDefinitionForDiversionRate = Models.FieldDefinition.DiversionRate;
            FieldDefinitionForDrawdownTimeforWQDetentionVolume = Models.FieldDefinition.DrawdownTimeforWQDetentionVolume;
            FieldDefinitionForEffectiveFootprint = Models.FieldDefinition.EffectiveFootprint;
            FieldDefinitionForEffectiveRetentionDepth = Models.FieldDefinition.EffectiveRetentionDepth;
            FieldDefinitionForInfiltrationDischargeRate = Models.FieldDefinition.InfiltrationDischargeRate;
            FieldDefinitionForInfiltrationSurfaceArea = Models.FieldDefinition.InfiltrationSurfaceArea;
            FieldDefinitionForMediaBedFootprint = Models.FieldDefinition.MediaBedFootprint;
            FieldDefinitionForMonthsofOperation = Models.FieldDefinition.MonthsofOperation;
            FieldDefinitionForPermanentPoolorWetlandVolume = Models.FieldDefinition.PermanentPoolorWetlandVolume;
            FieldDefinitionForRoutingConfiguration = Models.FieldDefinition.RoutingConfiguration;
            FieldDefinitionForStorageVolumeBelowLowestOutletElevation = Models.FieldDefinition.StorageVolumeBelowLowestOutletElevation;
            FieldDefinitionForSummerHarvestedWaterDemand = Models.FieldDefinition.SummerHarvestedWaterDemand;
            FieldDefinitionForTimeofConcentration = Models.FieldDefinition.TimeofConcentration;
            FieldDefinitionForTotalDrawdownTime = Models.FieldDefinition.TotalDrawdownTime;
            FieldDefinitionForTotalEffectiveBMPVolume = Models.FieldDefinition.TotalEffectiveBMPVolume;
            FieldDefinitionForTotalEffectiveDrywellBMPVolume = Models.FieldDefinition.TotalEffectiveDrywellBMPVolume;
            FieldDefinitionForTreatmentRate = Models.FieldDefinition.TreatmentRate;
            FieldDefinitionForUnderlyingHydrologicSoilGroupHSG = Models.FieldDefinition.UnderlyingHydrologicSoilGroupHSG;
            FieldDefinitionForUnderlyingInfiltrationRate = Models.FieldDefinition.UnderlyingInfiltrationRate;
            FieldDefinitionForUpstreamBMP = Models.FieldDefinition.UpstreamBMP;
            FieldDefinitionForWaterQualityDetentionVolume = Models.FieldDefinition.WaterQualityDetentionVolume;
            FieldDefinitionForWettedFootprint = Models.FieldDefinition.WettedFootprint;
            FieldDefinitionForWinterHarvestedWaterDemand = Models.FieldDefinition.WinterHarvestedWaterDemand;
        }

        private List<HtmlString> CheckForDelineationErrors(Models.TreatmentBMP treatmentBMP)
        {
            var delineationErrors = new List<HtmlString>();
            var delineationHasDiscrepancies = TreatmentBMP.Delineation?.HasDiscrepancies ?? false;
            if (delineationHasDiscrepancies)
            {
                delineationErrors.Add(new HtmlString("It is not consistent with the most recent Regional Sub-basin Layer."));
            }

            if (treatmentBMP.Delineation != null && treatmentBMP.Delineation.DelineationOverlaps.Any())
            {
                delineationErrors.Add(new HtmlString("It is overlapping with the following Treatment BMP(s): " +
                                                     string.Join(", ",
                                                         treatmentBMP.Delineation.DelineationOverlaps.Select(x =>
                                                             x.OverlappingDelineation.TreatmentBMP.GetDisplayNameAsUrl()))));
            }

            return delineationErrors;
        }

        public string DisplayModelingAttributeValue(Func<TreatmentBMPModelingAttribute, double?> modelAttributeValueFunc, string units)
        {
            if (TreatmentBMP.TreatmentBMPModelingAttribute == null)
            {
                return null;
            }

            var modelAttributeValue = modelAttributeValueFunc.Invoke(TreatmentBMP.TreatmentBMPModelingAttribute);
            return modelAttributeValue == null ? string.Empty : $"{modelAttributeValue.ToGroupedNumeric()} {units}";
        }

        public string DisplayUnderlyingHydrologicSoilGroup()
        {
            return TreatmentBMP.TreatmentBMPModelingAttribute != null && TreatmentBMP.TreatmentBMPModelingAttribute.UnderlyingHydrologicSoilGroup != null
                ? TreatmentBMP.TreatmentBMPModelingAttribute.UnderlyingHydrologicSoilGroup.UnderlyingHydrologicSoilGroupDisplayName : null;
        }

        public string DisplayRoutingConfiguration()
        {
            return TreatmentBMP.TreatmentBMPModelingAttribute != null && TreatmentBMP.TreatmentBMPModelingAttribute.RoutingConfiguration != null
                ? TreatmentBMP.TreatmentBMPModelingAttribute.RoutingConfiguration.RoutingConfigurationDisplayName : null;
        }

        public string DisplayTimeOfConcentration()
        {
            return TreatmentBMP.TreatmentBMPModelingAttribute != null && TreatmentBMP.TreatmentBMPModelingAttribute.TimeOfConcentration != null
                    ? $"{TreatmentBMP.TreatmentBMPModelingAttribute.TimeOfConcentration.TimeOfConcentrationDisplayName} mins" : null;
        }

        public string DisplayMonthsOfOperation()
        {
            return string.Join(", ",
                TreatmentBMP.TreatmentBMPOperationMonths.OrderBy(x => x.OperationMonth).Select(x =>
                    CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(x.OperationMonth)));
        }
    }
}
