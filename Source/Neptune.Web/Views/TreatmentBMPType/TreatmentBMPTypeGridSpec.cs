/*-----------------------------------------------------------------------
<copyright file="IndexGridSpec.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
Copyright (c) Tahoe Regional Planning Agency and Sitka Technology Group. All rights reserved.
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
using LtInfo.Common;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.Views;
using Neptune.Web.Models;
using Neptune.Web.Security;
using System.Linq;
using System.Web;
using Neptune.Web.Views.Shared.SortOrder;

namespace Neptune.Web.Views.TreatmentBMPType
{
    public class TreatmentBMPTypeGridSpec : GridSpec<Models.TreatmentBMPType>
    {
        public TreatmentBMPTypeGridSpec(Person currentPerson)
        {
            Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteUrl(), new NeptuneAdminFeature().HasPermissionByPerson(currentPerson)), 30, DhtmlxGridColumnFilterType.None);
            Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeEditIconAsHyperlinkBootstrap(x.GetEditUrl(), new NeptuneAdminFeature().HasPermissionByPerson(currentPerson)), 30, DhtmlxGridColumnFilterType.None);
            Add(Models.FieldDefinition.TreatmentBMPType.ToGridHeaderString(), a => UrlTemplate.MakeHrefString(a.GetDetailUrl(), a.TreatmentBMPTypeName), 400, DhtmlxGridColumnFilterType.Html);
            Add($"Number of {Models.FieldDefinition.TreatmentBMPAssessmentObservationType.ToGridHeaderStringPlural("Observation Types")}", a => a.TreatmentBMPTypeAssessmentObservationTypes.Select(x => x.TreatmentBMPAssessmentObservationType).Count(), 100);
            Add($"Number of {Models.FieldDefinition.TreatmentBMP.ToGridHeaderStringPlural("Treatment BMPs")}", a => a.TreatmentBMPs.Count, 100, DhtmlxGridColumnAggregationType.Total);
        }
    }

    public class TreatmentBMPsInTreatmentBMPTypeGridSpec : GridSpec<Models.TreatmentBMPDetailed>
    {
        public TreatmentBMPsInTreatmentBMPTypeGridSpec(Person currentPerson, bool showDelete, bool showEdit,
            Models.TreatmentBMPType treatmentBMPType)
        {
            if (showDelete)
            {
                Add(string.Empty, x =>
                {
                    var userHasDeletePermission = currentPerson.IsManagerOrAdmin() &&
                                                  currentPerson.IsAssignedToStormwaterJurisdiction(x.TreatmentBMP
                                                      .StormwaterJurisdictionID);
                    return DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(
                        TreatmentBMPModelExtensions.DeleteUrlTemplate.ParameterReplace(x.TreatmentBMP.TreatmentBMPID),
                        userHasDeletePermission, userHasDeletePermission);
                }, 30, DhtmlxGridColumnFilterType.None);
            }

            if (showEdit)
            {
                Add(string.Empty,
                    x => DhtmlxGridHtmlHelpers.MakeEditIconAsHyperlinkBootstrap(
                        TreatmentBMPModelExtensions.EditUrlTemplate.ParameterReplace(x.TreatmentBMP.TreatmentBMPID),
                        currentPerson.IsAssignedToStormwaterJurisdiction(x.TreatmentBMP.StormwaterJurisdictionID)), 30,
                    DhtmlxGridColumnFilterType.None);
            }

            Add(string.Empty,
                x => UrlTemplate.MakeHrefString(
                    TreatmentBMPModelExtensions.DetailUrlTemplate.ParameterReplace(x.TreatmentBMP.TreatmentBMPID),
                    "View", new Dictionary<string, string> {{"class", "gridButton"}}), 50,
                DhtmlxGridColumnFilterType.None);
            Add(Models.FieldDefinition.TreatmentBMP.ToGridHeaderString("Name"),
                x => UrlTemplate.MakeHrefString(
                    TreatmentBMPModelExtensions.DetailUrlTemplate.ParameterReplace(x.TreatmentBMP.TreatmentBMPID),
                    x.TreatmentBMP.TreatmentBMPName), 170, DhtmlxGridColumnFilterType.Html);
            Add(Models.FieldDefinition.Jurisdiction.ToGridHeaderString("Jurisdiction"),
                x => UrlTemplate.MakeHrefString(
                    StormwaterJurisdictionModelExtensions.DetailUrlTemplate.ParameterReplace(x.TreatmentBMP
                        .StormwaterJurisdictionID), x.vTreatmentBmpDetailed.OrganizationName), 170);
            Add("Owner Organization",
                x => UrlTemplate.MakeHrefString(
                    OrganizationModelExtensions.DetailUrlTemplate.ParameterReplace(x.TreatmentBMP.OwnerOrganizationID),
                    x.vTreatmentBmpDetailed.OwnerOrganizationName), 170);
            Add(Models.FieldDefinition.TreatmentBMPType.ToGridHeaderString("Type"),
                x => x.vTreatmentBmpDetailed.TreatmentBMPTypeName, 100, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Year Built", x => x.TreatmentBMP.YearBuilt, 100, DhtmlxGridColumnFormatType.Date);
            Add("Notes", x => x.TreatmentBMP.Notes, 195);
            Add("Last Assessment Date", x => x.vTreatmentBmpDetailed.LatestAssessmentDate, 130);
            Add("Last Assessed Score",
                x => x.vTreatmentBmpDetailed.LatestAssessmentScore.HasValue
                    ? x.vTreatmentBmpDetailed.LatestAssessmentScore.Value.ToString("0.0")
                    : "-", 100, DhtmlxGridColumnFilterType.FormattedNumeric);
            Add("# of Assessments", x => x.vTreatmentBmpDetailed.NumberOfAssessments, 100,
                DhtmlxGridColumnFormatType.Integer, DhtmlxGridColumnAggregationType.Total);
            Add("Last Maintenance Date", x => x.vTreatmentBmpDetailed.LatestMaintenanceDate, 130,
                DhtmlxGridColumnFormatType.Date);
            Add("# of Maintenance Events", x => x.vTreatmentBmpDetailed.NumberOfMaintenanceRecords, 100,
                DhtmlxGridColumnFormatType.Integer, DhtmlxGridColumnAggregationType.Total);
            Add("Benchmark and Threshold Set?",
                x => (x.vTreatmentBmpDetailed.NumberOfBenchmarkAndThresholds ==
                      x.vTreatmentBmpDetailed.NumberOfBenchmarkAndThresholdsEntered).ToYesNo(), 80,
                DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Required Lifespan of Installation",
                x => x.vTreatmentBmpDetailed.TreatmentBMPLifespanTypeDisplayName ?? "Unknown", 170,
                DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Lifespan End Date (if Fixed End Date)", x => x.vTreatmentBmpDetailed.TreatmentBMPLifespanEndDate, 130,
                DhtmlxGridColumnFormatType.Date);
            Add(Models.FieldDefinition.RequiredFieldVisitsPerYear.ToGridHeaderString(),
                x => x.vTreatmentBmpDetailed.RequiredFieldVisitsPerYear, 130, DhtmlxGridColumnFormatType.Integer);
            Add(Models.FieldDefinition.RequiredPostStormFieldVisitsPerYear.ToGridHeaderString(),
                x => x.vTreatmentBmpDetailed.RequiredPostStormFieldVisitsPerYear, 130,
                DhtmlxGridColumnFormatType.Integer);
            Add(Models.FieldDefinition.SizingBasis.ToGridHeaderString(),
                x => x.vTreatmentBmpDetailed.SizingBasisTypeDisplayName, 130,
                DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.TrashCaptureStatus.ToGridHeaderString(),
                x => x.vTreatmentBmpDetailed.TrashCaptureStatusTypeDisplayName, 130,
                DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.DelineationType.ToGridHeaderString(),
                x => string.IsNullOrWhiteSpace(x.vTreatmentBmpDetailed.DelineationTypeDisplayName)
                    ? new HtmlString("<p class='systemText'>No Delineation Provided</p>")
                    : new HtmlString(x.vTreatmentBmpDetailed.DelineationTypeDisplayName), 130,
                DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);

            foreach (var purpose in CustomAttributeTypePurpose.All)
            {
                var attributes = treatmentBMPType.TreatmentBMPTypeCustomAttributeTypes.Where(x =>
                    x.CustomAttributeType.CustomAttributeTypePurpose.CustomAttributeTypePurposeID ==
                    purpose.CustomAttributeTypePurposeID).ToList();

                if (!attributes.Any())
                {
                    continue;
                }

                foreach (var customAttributeType in attributes.SortByOrderThenName())
                {
                    switch (customAttributeType.CustomAttributeType.CustomAttributeDataTypeID)
                    {
                        case (int)CustomAttributeDataTypeEnum.Decimal:
                            Add(customAttributeType.GetDisplayNameWithUnits(),
                                x => Decimal.TryParse(x.TreatmentBMP.GetCustomAttributeValue(customAttributeType),  out var decimalResult) ? decimalResult : (Decimal?)null , 130,
                                DhtmlxGridColumnFormatType.Decimal);
                            break;
                        case (int)CustomAttributeDataTypeEnum.Integer:
                            Add(customAttributeType.GetDisplayNameWithUnits(),
                                x => Int32.TryParse(x.TreatmentBMP.GetCustomAttributeValue(customAttributeType), out var intResult) ? intResult : (Int32?)null, 130);
                            break;
                        case (int)CustomAttributeDataTypeEnum.DateTime:
                            Add(customAttributeType.GetDisplayNameWithUnits(),
                                x => DateTime.TryParse(x.TreatmentBMP.GetCustomAttributeValue(customAttributeType), out var dateResult) ? dateResult : (DateTime?)null, 130);
                            break;
                        case (int)CustomAttributeDataTypeEnum.MultiSelect:
                            Add(customAttributeType.GetDisplayNameWithUnits(),
                                x => x.TreatmentBMP.GetCustomAttributeValue(customAttributeType), 130,
                                DhtmlxGridColumnFilterType.SelectFilterStrict);
                            break;
                        case (int)CustomAttributeDataTypeEnum.PickFromList:
                            Add(customAttributeType.GetDisplayNameWithUnits(),
                                x => x.TreatmentBMP.GetCustomAttributeValue(customAttributeType), 130,
                                DhtmlxGridColumnFilterType.SelectFilterStrict);
                            break;
                        case (int)CustomAttributeDataTypeEnum.String:
                            Add(customAttributeType.GetDisplayNameWithUnits(),
                                x => x.TreatmentBMP.GetCustomAttributeValue(customAttributeType), 130,
                                DhtmlxGridColumnFilterType.Text);
                            break;
                    }
                }
            }
            

            if (!treatmentBMPType.TreatmentBMPModelingTypeID.HasValue)
            {
                return;
            }

            Add(Models.FieldDefinition.Watershed.ToGridHeaderString(),
                x => x.TreatmentBMP.Watershed?.WatershedName, 100);
            switch (treatmentBMPType.TreatmentBMPModelingType.ToEnum)
            {
                case TreatmentBMPModelingTypeEnum.BioinfiltrationBioretentionWithRaisedUnderdrain:
                    Add(Models.FieldDefinition.TotalEffectiveBMPVolume.ToGridHeaderString(),
                        x => x.TreatmentBMP.TreatmentBMPModelingAttribute?.TotalEffectiveBMPVolume, 100);
                    Add(Models.FieldDefinition.StorageVolumeBelowLowestOutletElevation.ToGridHeaderString(),
                        x => x.TreatmentBMP.TreatmentBMPModelingAttribute?.StorageVolumeBelowLowestOutletElevation, 100);
                    Add(Models.FieldDefinition.MediaBedFootprint.ToGridHeaderString(),
                        x => x.TreatmentBMP.TreatmentBMPModelingAttribute?.MediaBedFootprint, 100);
                    Add(Models.FieldDefinition.TotalEffectiveBMPVolume.ToGridHeaderString(),
                        x => x.TreatmentBMP.TreatmentBMPModelingAttribute?.DesignMediaFiltrationRate, 100);
                    Add(Models.FieldDefinition.TotalEffectiveBMPVolume.ToGridHeaderString(),
                        x => x.TreatmentBMP.TreatmentBMPModelingAttribute?.TotalEffectiveBMPVolume, 100);
                    Add(Models.FieldDefinition.UnderlyingHydrologicSoilGroupHSG.ToGridHeaderString(),
                        x => x.TreatmentBMP.TreatmentBMPModelingAttribute?.UnderlyingHydrologicSoilGroup?
                            .UnderlyingHydrologicSoilGroupDisplayName, 100);
                    break;
                case TreatmentBMPModelingTypeEnum.BioretentionWithNoUnderdrain:
                case TreatmentBMPModelingTypeEnum.InfiltrationBasin:
                case TreatmentBMPModelingTypeEnum.InfiltrationTrench:
                case TreatmentBMPModelingTypeEnum.PermeablePavement:
                case TreatmentBMPModelingTypeEnum.UndergroundInfiltration:
                    Add(Models.FieldDefinition.TotalEffectiveBMPVolume.ToGridHeaderString(),
                        x => x.TreatmentBMP.TreatmentBMPModelingAttribute?.TotalEffectiveBMPVolume, 100);
                    Add(Models.FieldDefinition.InfiltrationSurfaceArea.ToGridHeaderString(),
                        x => x.TreatmentBMP.TreatmentBMPModelingAttribute?.InfiltrationSurfaceArea, 100);
                    Add(Models.FieldDefinition.UnderlyingInfiltrationRate.ToGridHeaderString(),
                        x => x.TreatmentBMP.TreatmentBMPModelingAttribute?.UnderlyingInfiltrationRate, 100);
                    break;
                case TreatmentBMPModelingTypeEnum.BioretentionWithUnderdrainAndImperviousLiner:
                case TreatmentBMPModelingTypeEnum.SandFilters:
                    Add(Models.FieldDefinition.TotalEffectiveBMPVolume.ToGridHeaderString(),
                        x => x.TreatmentBMP.TreatmentBMPModelingAttribute?.TotalEffectiveBMPVolume, 100);
                    Add(Models.FieldDefinition.MediaBedFootprint.ToGridHeaderString(),
                        x => x.TreatmentBMP.TreatmentBMPModelingAttribute?.MediaBedFootprint, 100);
                    Add(Models.FieldDefinition.DesignMediaFiltrationRate.ToGridHeaderString(),
                        x => x.TreatmentBMP.TreatmentBMPModelingAttribute?.DesignMediaFiltrationRate, 100);
                    break;
                case TreatmentBMPModelingTypeEnum.CisternsForHarvestAndUse:
                    Add(Models.FieldDefinition.TotalEffectiveBMPVolume.ToGridHeaderString(),
                        x => x.TreatmentBMP.TreatmentBMPModelingAttribute?.TotalEffectiveBMPVolume, 100);
                    Add(Models.FieldDefinition.WinterHarvestedWaterDemand.ToGridHeaderString(),
                        x => x.TreatmentBMP.TreatmentBMPModelingAttribute?.WinterHarvestedWaterDemand, 100);
                    Add(Models.FieldDefinition.SummerHarvestedWaterDemand.ToGridHeaderString(),
                        x => x.TreatmentBMP.TreatmentBMPModelingAttribute?.SummerHarvestedWaterDemand, 100);
                    break;
                case TreatmentBMPModelingTypeEnum.ConstructedWetland:
                case TreatmentBMPModelingTypeEnum.WetDetentionBasin:
                    Add(Models.FieldDefinition.PermanentPoolOrWetlandVolume.ToGridHeaderString(),
                        x => x.TreatmentBMP.TreatmentBMPModelingAttribute?.PermanentPoolorWetlandVolume, 100);
                    Add(Models.FieldDefinition.DesignResidenceTimeForPermanentPool.ToGridHeaderString(),
                        x => x.TreatmentBMP.TreatmentBMPModelingAttribute?.DesignResidenceTimeforPermanentPool, 100);
                    Add(Models.FieldDefinition.WaterQualityDetentionVolume.ToGridHeaderString(),
                        x => x.TreatmentBMP.TreatmentBMPModelingAttribute?.WaterQualityDetentionVolume, 100);
                    Add(Models.FieldDefinition.DrawdownTimeForWQDetentionVolume.ToGridHeaderString(),
                        x => x.TreatmentBMP.TreatmentBMPModelingAttribute?.DrawdownTimeforWQDetentionVolume, 100);
                    Add(Models.FieldDefinition.WinterHarvestedWaterDemand.ToGridHeaderString(),
                        x => x.TreatmentBMP.TreatmentBMPModelingAttribute?.WinterHarvestedWaterDemand, 100);
                    Add(Models.FieldDefinition.SummerHarvestedWaterDemand.ToGridHeaderString(),
                        x => x.TreatmentBMP.TreatmentBMPModelingAttribute?.SummerHarvestedWaterDemand, 100);
                    break;
                case TreatmentBMPModelingTypeEnum.DryExtendedDetentionBasin:
                case TreatmentBMPModelingTypeEnum.FlowDurationControlBasin:
                case TreatmentBMPModelingTypeEnum.FlowDurationControlTank:
                    Add(Models.FieldDefinition.TotalEffectiveBMPVolume.ToGridHeaderString(),
                        x => x.TreatmentBMP.TreatmentBMPModelingAttribute?.TotalEffectiveBMPVolume, 100);
                    Add(Models.FieldDefinition.StorageVolumeBelowLowestOutletElevation.ToGridHeaderString(),
                        x => x.TreatmentBMP.TreatmentBMPModelingAttribute?.StorageVolumeBelowLowestOutletElevation, 100);
                    Add(Models.FieldDefinition.EffectiveFootprint.ToGridHeaderString(),
                        x => x.TreatmentBMP.TreatmentBMPModelingAttribute?.EffectiveFootprint, 100);
                    Add(Models.FieldDefinition.DrawdownTimeForWQDetentionVolume.ToGridHeaderString(),
                        x => x.TreatmentBMP.TreatmentBMPModelingAttribute?.DrawdownTimeforWQDetentionVolume, 100);
                    Add(Models.FieldDefinition.UnderlyingHydrologicSoilGroupHSG.ToGridHeaderString(),
                        x => x.TreatmentBMP.TreatmentBMPModelingAttribute?.UnderlyingHydrologicSoilGroup?
                            .UnderlyingHydrologicSoilGroupDisplayName, 100);
                    break;
                case TreatmentBMPModelingTypeEnum.DryWeatherTreatmentSystems:
                    Add(Models.FieldDefinition.DesignDryWeatherTreatmentCapacity.ToGridHeaderString(),
                        x => x.TreatmentBMP.TreatmentBMPModelingAttribute?.DesignDryWeatherTreatmentCapacity, 100);
                    Add(Models.FieldDefinition.AverageTreatmentFlowrate.ToGridHeaderString(),
                        x => x.TreatmentBMP.TreatmentBMPModelingAttribute?.AverageTreatmentFlowrate, 100);
                    Add(Models.FieldDefinition.MonthsOperational.ToGridHeaderString(),
                        x => x.TreatmentBMP.TreatmentBMPModelingAttribute?.MonthsOfOperation?
                            .MonthsOfOperationDisplayName, 100);
                    break;
                case TreatmentBMPModelingTypeEnum.Drywell:
                    Add(Models.FieldDefinition.TotalEffectiveBMPVolume.ToGridHeaderString(),
                        x => x.TreatmentBMP.TreatmentBMPModelingAttribute?.TotalEffectiveBMPVolume, 100);
                    Add(Models.FieldDefinition.InfiltrationDischargeRate.ToGridHeaderString(),
                        x => x.TreatmentBMP.TreatmentBMPModelingAttribute?.InfiltrationDischargeRate, 100);
                    break;
                case TreatmentBMPModelingTypeEnum.HydrodynamicSeparator:
                case TreatmentBMPModelingTypeEnum.ProprietaryBiotreatment:
                case TreatmentBMPModelingTypeEnum.ProprietaryTreatmentControl:
                    Add(Models.FieldDefinition.TreatmentRate.ToGridHeaderString(),
                        x => x.TreatmentBMP.TreatmentBMPModelingAttribute?.TreatmentRate, 100);
                    Add(Models.FieldDefinition.TimeOfConcentration.ToGridHeaderString(),
                        x => x.TreatmentBMP.TreatmentBMPModelingAttribute?.TimeOfConcentration?
                            .TimeOfConcentrationDisplayName, 100);
                    break;
                case TreatmentBMPModelingTypeEnum.LowFlowDiversions:
                    Add(Models.FieldDefinition.DesignLowFlowDiversionCapacity.ToGridHeaderString(),
                        x => x.TreatmentBMP.TreatmentBMPModelingAttribute?.DesignLowFlowDiversionCapacity, 100);
                    Add(Models.FieldDefinition.AverageDivertedFlowrate.ToGridHeaderString(),
                        x => x.TreatmentBMP.TreatmentBMPModelingAttribute?.AverageDivertedFlowrate, 100);
                    Add(Models.FieldDefinition.MonthsOperational.ToGridHeaderString(),
                        x => x.TreatmentBMP.TreatmentBMPModelingAttribute?.MonthsOfOperation?
                            .MonthsOfOperationDisplayName, 100);
                    break;
                case TreatmentBMPModelingTypeEnum.VegetatedFilterStrip:
                case TreatmentBMPModelingTypeEnum.VegetatedSwale:
                    Add(Models.FieldDefinition.TimeOfConcentration.ToGridHeaderString(),
                        x => x.TreatmentBMP.TreatmentBMPModelingAttribute?.TimeOfConcentration?
                            .TimeOfConcentrationDisplayName, 100);
                    Add(Models.FieldDefinition.TreatmentRate.ToGridHeaderString(),
                        x => x.TreatmentBMP.TreatmentBMPModelingAttribute?.TreatmentRate, 100);
                    Add(Models.FieldDefinition.WettedFootprint.ToGridHeaderString(),
                        x => x.TreatmentBMP.TreatmentBMPModelingAttribute?.WettedFootprint, 100);
                    Add(Models.FieldDefinition.EffectiveRetentionDepth.ToGridHeaderString(),
                        x => x.TreatmentBMP.TreatmentBMPModelingAttribute?.EffectiveRetentionDepth, 100);
                    Add(Models.FieldDefinition.UnderlyingHydrologicSoilGroupHSG.ToGridHeaderString(),
                        x => x.TreatmentBMP.TreatmentBMPModelingAttribute?.UnderlyingHydrologicSoilGroup?
                            .UnderlyingHydrologicSoilGroupDisplayName, 100);
                    break;
            }

            Add(Models.FieldDefinition.DryWeatherFlowOverride.ToGridHeaderString(),
                x => x.TreatmentBMP.TreatmentBMPModelingAttribute?.DryWeatherFlowOverride?.DryWeatherFlowOverrideDisplayName, 100);
        }
    }
}
