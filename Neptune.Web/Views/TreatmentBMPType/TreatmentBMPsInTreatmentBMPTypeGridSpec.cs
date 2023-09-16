using Microsoft.AspNetCore.Html;
using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Common.DhtmlWrappers;
using Neptune.Web.Common.HtmlHelperExtensions;
using Neptune.Web.Controllers;
using Neptune.Web.Views.Shared.SortOrder;

namespace Neptune.Web.Views.TreatmentBMPType;

public class TreatmentBMPsInTreatmentBMPTypeGridSpec : GridSpec<vTreatmentBMPDetailedWithTreatmentBMPEntity>
{
    public TreatmentBMPsInTreatmentBMPTypeGridSpec(Person currentPerson, bool showDelete, bool showEdit,
        EFModels.Entities.TreatmentBMPType treatmentBMPType, LinkGenerator linkGenerator)
    {
        var organizationDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<OrganizationController>.BuildUrlFromExpression(linkGenerator, t => t.Detail(UrlTemplate.Parameter1Int)));
        var stormwaterJurisdictionDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<JurisdictionController>.BuildUrlFromExpression(linkGenerator, t => t.Detail(UrlTemplate.Parameter1Int)));
        var wqmpDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(linkGenerator, t => t.Detail(UrlTemplate.Parameter1Int)));
        var detailUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, t => t.Detail(UrlTemplate.Parameter1Int)));

        if (showDelete)
        {
            var deleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, t => t.Delete(UrlTemplate.Parameter1Int)));
            Add(string.Empty, x =>
            {
                var userHasDeletePermission = currentPerson.IsManagerOrAdmin() &&
                                              currentPerson.IsAssignedToStormwaterJurisdiction(x.TreatmentBMP
                                                  .StormwaterJurisdictionID);
                return DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(
                    deleteUrlTemplate.ParameterReplace(x.TreatmentBMP.TreatmentBMPID),
                    userHasDeletePermission, userHasDeletePermission);
            }, 30, DhtmlxGridColumnFilterType.None);
        }

        if (showEdit)
        {
            var editUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, t => t.Edit(UrlTemplate.Parameter1Int)));
            Add(string.Empty,
                x => DhtmlxGridHtmlHelpers.MakeEditIconAsHyperlinkBootstrap(
                    editUrlTemplate.ParameterReplace(x.TreatmentBMP.TreatmentBMPID),
                    currentPerson.IsAssignedToStormwaterJurisdiction(x.TreatmentBMP.StormwaterJurisdictionID)), 30,
                DhtmlxGridColumnFilterType.None);
        }

        Add(string.Empty,
            x => UrlTemplate.MakeHrefString(
                detailUrlTemplate.ParameterReplace(x.TreatmentBMP.TreatmentBMPID),
                "View", new Dictionary<string, string> {{"class", "gridButton"}}), 50,
            DhtmlxGridColumnFilterType.None);
        Add(FieldDefinitionType.TreatmentBMP.ToGridHeaderString("Name"),
            x => UrlTemplate.MakeHrefString(
                detailUrlTemplate.ParameterReplace(x.TreatmentBMP.TreatmentBMPID),
                x.TreatmentBMP.TreatmentBMPName), 170, DhtmlxGridColumnFilterType.Html);
        Add(FieldDefinitionType.Jurisdiction.ToGridHeaderString("Jurisdiction"),
            x => UrlTemplate.MakeHrefString(
                stormwaterJurisdictionDetailUrlTemplate.ParameterReplace(x.TreatmentBMP
                    .StormwaterJurisdictionID), x.vTreatmentBMPDetailed.OrganizationName), 170);
        Add("Owner Organization",
            x => UrlTemplate.MakeHrefString(
                organizationDetailUrlTemplate.ParameterReplace(x.TreatmentBMP.OwnerOrganizationID),
                x.vTreatmentBMPDetailed.OwnerOrganizationName), 170);
        Add(FieldDefinitionType.TreatmentBMPType.ToGridHeaderString("Type"),
            x => x.vTreatmentBMPDetailed.TreatmentBMPTypeName, 100, DhtmlxGridColumnFilterType.SelectFilterStrict);
        Add("Year Built", x => x.TreatmentBMP.YearBuilt, 100, DhtmlxGridColumnFormatType.Date);
        Add("ID in System of Record", x => x.TreatmentBMP.SystemOfRecordID, 100);
        Add("Water Quality Management Plan", x => x.TreatmentBMP.WaterQualityManagementPlanID.HasValue ? 
            UrlTemplate.MakeHrefString(wqmpDetailUrlTemplate.ParameterReplace(x.TreatmentBMP.WaterQualityManagementPlan.WaterQualityManagementPlanID), x.TreatmentBMP.WaterQualityManagementPlan.WaterQualityManagementPlanName):
            "<p></p>".ToHTMLFormattedString(), 170);
        Add("Notes", x => x.TreatmentBMP.Notes, 195);
        Add("Last Assessment Date", x => x.vTreatmentBMPDetailed.LatestAssessmentDate, 130);
        Add("Last Assessed Score",
            x => x.vTreatmentBMPDetailed.LatestAssessmentScore.HasValue
                ? x.vTreatmentBMPDetailed.LatestAssessmentScore.Value.ToString("0.0")
                : "-", 100, DhtmlxGridColumnFilterType.FormattedNumeric);
        Add("# of Assessments", x => x.vTreatmentBMPDetailed.NumberOfAssessments, 100,
            DhtmlxGridColumnFormatType.Integer, DhtmlxGridColumnAggregationType.Total);
        Add("Last Maintenance Date", x => x.vTreatmentBMPDetailed.LatestMaintenanceDate, 130,
            DhtmlxGridColumnFormatType.Date);
        Add("# of Maintenance Events", x => x.vTreatmentBMPDetailed.NumberOfMaintenanceRecords, 100,
            DhtmlxGridColumnFormatType.Integer, DhtmlxGridColumnAggregationType.Total);
        Add("Benchmark and Threshold Set?",
            x => (x.vTreatmentBMPDetailed.NumberOfBenchmarkAndThresholds ==
                  x.vTreatmentBMPDetailed.NumberOfBenchmarkAndThresholdsEntered).ToYesNo(), 80,
            DhtmlxGridColumnFilterType.SelectFilterStrict);
        Add("Required Lifespan of Installation",
            x => x.vTreatmentBMPDetailed.TreatmentBMPLifespanTypeDisplayName ?? "Unknown", 170,
            DhtmlxGridColumnFilterType.SelectFilterStrict);
        Add("Lifespan End Date (if Fixed End Date)", x => x.vTreatmentBMPDetailed.TreatmentBMPLifespanEndDate, 130,
            DhtmlxGridColumnFormatType.Date);
        Add(FieldDefinitionType.RequiredFieldVisitsPerYear.ToGridHeaderString(),
            x => x.vTreatmentBMPDetailed.RequiredFieldVisitsPerYear, 130, DhtmlxGridColumnFormatType.Integer);
        Add(FieldDefinitionType.RequiredPostStormFieldVisitsPerYear.ToGridHeaderString(),
            x => x.vTreatmentBMPDetailed.RequiredPostStormFieldVisitsPerYear, 130,
            DhtmlxGridColumnFormatType.Integer);
        Add(FieldDefinitionType.SizingBasis.ToGridHeaderString(),
            x => x.vTreatmentBMPDetailed.SizingBasisTypeDisplayName, 130,
            DhtmlxGridColumnFilterType.SelectFilterStrict);
        Add(FieldDefinitionType.TrashCaptureStatus.ToGridHeaderString(),
            x => x.vTreatmentBMPDetailed.TrashCaptureStatusTypeDisplayName, 130,
            DhtmlxGridColumnFilterType.SelectFilterStrict);
        Add(FieldDefinitionType.DelineationType.ToGridHeaderString(),
            x => string.IsNullOrWhiteSpace(x.vTreatmentBMPDetailed.DelineationTypeDisplayName)
                ? new HtmlString("<p class='systemText'>No Delineation Provided</p>")
                : new HtmlString(x.vTreatmentBMPDetailed.DelineationTypeDisplayName), 130,
            DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);

        foreach (var purpose in CustomAttributeTypePurpose.All)
        {
            if (purpose == CustomAttributeTypePurpose.Maintenance)
            {
                continue;
            }

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
                            x => decimal.TryParse(GetCustomAttributeValue(x.TreatmentBMP.CustomAttributes, customAttributeType),  out var decimalResult) ? decimalResult : null , 130,
                            DhtmlxGridColumnFormatType.Decimal);
                        break;
                    case (int)CustomAttributeDataTypeEnum.Integer:
                        Add(customAttributeType.GetDisplayNameWithUnits(),
                            x => int.TryParse(GetCustomAttributeValue(x.TreatmentBMP.CustomAttributes, customAttributeType), out var intResult) ? intResult : (int?)null, 130);
                        break;
                    case (int)CustomAttributeDataTypeEnum.DateTime:
                        Add(customAttributeType.GetDisplayNameWithUnits(),
                            x => DateTime.TryParse(GetCustomAttributeValue(x.TreatmentBMP.CustomAttributes, customAttributeType), out var dateResult) ? dateResult : null, 130);
                        break;
                    case (int)CustomAttributeDataTypeEnum.MultiSelect:
                        Add(customAttributeType.GetDisplayNameWithUnits(),
                            x => GetCustomAttributeValue(x.TreatmentBMP.CustomAttributes, customAttributeType), 130,
                            DhtmlxGridColumnFilterType.SelectFilterStrict);
                        break;
                    case (int)CustomAttributeDataTypeEnum.PickFromList:
                        Add(customAttributeType.GetDisplayNameWithUnits(),
                            x => GetCustomAttributeValue(x.TreatmentBMP.CustomAttributes, customAttributeType), 130,
                            DhtmlxGridColumnFilterType.SelectFilterStrict);
                        break;
                    case (int)CustomAttributeDataTypeEnum.String:
                        Add(customAttributeType.GetDisplayNameWithUnits(),
                            x => GetCustomAttributeValue(x.TreatmentBMP.CustomAttributes, customAttributeType), 130,
                            DhtmlxGridColumnFilterType.Text);
                        break;
                }
            }
        }
            

        if (!treatmentBMPType.TreatmentBMPModelingTypeID.HasValue)
        {
            return;
        }

        Add(FieldDefinitionType.Watershed.ToGridHeaderString(),
            x => x.TreatmentBMP.Watershed?.WatershedName, 100);
        switch (treatmentBMPType.TreatmentBMPModelingType.ToEnum)
        {
            case TreatmentBMPModelingTypeEnum.BioinfiltrationBioretentionWithRaisedUnderdrain:
                Add(FieldDefinitionType.TotalEffectiveBMPVolume.ToGridHeaderString("Total Effective BMP Volume (cu ft)"),
                    x => x.TreatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP?.TotalEffectiveBMPVolume, 100);
                Add(FieldDefinitionType.StorageVolumeBelowLowestOutletElevation.ToGridHeaderString("Storage Volume Below Lowest Outlet Elevation (cu ft)"),
                    x => x.TreatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP?.StorageVolumeBelowLowestOutletElevation, 100);
                Add(FieldDefinitionType.MediaBedFootprint.ToGridHeaderString("Media Bed Footprint (sq ft)"),
                    x => x.TreatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP?.MediaBedFootprint, 100);
                Add(FieldDefinitionType.DesignMediaFiltrationRate.ToGridHeaderString("Design Media Filtration Rate (in/hr)"),
                    x => x.TreatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP?.DesignMediaFiltrationRate, 100);
                Add(FieldDefinitionType.UnderlyingHydrologicSoilGroupHSG.ToGridHeaderString("Underlying Hydrologic Soil Group"),
                    x => x.TreatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP?.UnderlyingHydrologicSoilGroup?
                        .UnderlyingHydrologicSoilGroupDisplayName, 100);
                break;
            case TreatmentBMPModelingTypeEnum.BioretentionWithNoUnderdrain:
            case TreatmentBMPModelingTypeEnum.InfiltrationBasin:
            case TreatmentBMPModelingTypeEnum.InfiltrationTrench:
            case TreatmentBMPModelingTypeEnum.PermeablePavement:
            case TreatmentBMPModelingTypeEnum.UndergroundInfiltration:
                Add(FieldDefinitionType.TotalEffectiveBMPVolume.ToGridHeaderString("Total Effective BMP Volume (cu ft)"),
                    x => x.TreatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP?.TotalEffectiveBMPVolume, 100);
                Add(FieldDefinitionType.InfiltrationSurfaceArea.ToGridHeaderString("Infiltration Surface Area (sq ft)"),
                    x => x.TreatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP?.InfiltrationSurfaceArea, 100);
                Add(FieldDefinitionType.UnderlyingInfiltrationRate.ToGridHeaderString("Underlying Infiltration Rate (in/hr)"),
                    x => x.TreatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP?.UnderlyingInfiltrationRate, 100);
                break;
            case TreatmentBMPModelingTypeEnum.BioretentionWithUnderdrainAndImperviousLiner:
            case TreatmentBMPModelingTypeEnum.SandFilters:
                Add(FieldDefinitionType.TotalEffectiveBMPVolume.ToGridHeaderString("Total Effective BMP Volume (cu ft)"),
                    x => x.TreatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP?.TotalEffectiveBMPVolume, 100);
                Add(FieldDefinitionType.MediaBedFootprint.ToGridHeaderString("Media Bed Footprint (sq ft)"),
                    x => x.TreatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP?.MediaBedFootprint, 100);
                Add(FieldDefinitionType.DesignMediaFiltrationRate.ToGridHeaderString("Design Media Filtration Rate (in/hr)"),
                    x => x.TreatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP?.DesignMediaFiltrationRate, 100);
                break;
            case TreatmentBMPModelingTypeEnum.CisternsForHarvestAndUse:
                Add(FieldDefinitionType.TotalEffectiveBMPVolume.ToGridHeaderString("Total Effective BMP Volume (cu ft)"),
                    x => x.TreatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP?.TotalEffectiveBMPVolume, 100);
                Add(FieldDefinitionType.WinterHarvestedWaterDemand.ToGridHeaderString("Winter Harvested Water Demand (gpd)"),
                    x => x.TreatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP?.WinterHarvestedWaterDemand, 100);
                Add(FieldDefinitionType.SummerHarvestedWaterDemand.ToGridHeaderString("Summer Harvested Water Demand (gpd)"),
                    x => x.TreatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP?.SummerHarvestedWaterDemand, 100);
                break;
            case TreatmentBMPModelingTypeEnum.ConstructedWetland:
            case TreatmentBMPModelingTypeEnum.WetDetentionBasin:
                Add(FieldDefinitionType.PermanentPoolOrWetlandVolume.ToGridHeaderString("Permanent Pool Or Wetland Volume (cu ft)"),
                    x => x.TreatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP?.PermanentPoolorWetlandVolume, 100);
                Add(FieldDefinitionType.WaterQualityDetentionVolume.ToGridHeaderString("Extended Detention Surcharge Volume (cu ft)"),
                    x => x.TreatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP?.WaterQualityDetentionVolume, 100);
                break;
            case TreatmentBMPModelingTypeEnum.DryExtendedDetentionBasin:
            case TreatmentBMPModelingTypeEnum.FlowDurationControlBasin:
            case TreatmentBMPModelingTypeEnum.FlowDurationControlTank:
                Add(FieldDefinitionType.TotalEffectiveBMPVolume.ToGridHeaderString("Total Effective BMP Volume (cu ft)"),
                    x => x.TreatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP?.TotalEffectiveBMPVolume, 100);
                Add(FieldDefinitionType.StorageVolumeBelowLowestOutletElevation.ToGridHeaderString("Storage Volume Below Lowest Outlet Elevation (cu ft)"),
                    x => x.TreatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP?.StorageVolumeBelowLowestOutletElevation, 100);
                Add(FieldDefinitionType.EffectiveFootprint.ToGridHeaderString("Effective Footprint (sq ft)"),
                    x => x.TreatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP?.EffectiveFootprint, 100);
                Add(FieldDefinitionType.DrawdownTimeForWQDetentionVolume.ToGridHeaderString("Drawdown Time For WQ Detention Volume (hours)"),
                    x => x.TreatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP?.DrawdownTimeforWQDetentionVolume, 100);
                Add(FieldDefinitionType.UnderlyingHydrologicSoilGroupHSG.ToGridHeaderString("Underlying Hydrologic Soil Group"),
                    x => x.TreatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP?.UnderlyingHydrologicSoilGroup?
                        .UnderlyingHydrologicSoilGroupDisplayName, 100);
                break;
            case TreatmentBMPModelingTypeEnum.DryWeatherTreatmentSystems:
                Add(FieldDefinitionType.DesignDryWeatherTreatmentCapacity.ToGridHeaderString("Design Dry Weather Treatment Capacity (cfs)"),
                    x => x.TreatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP?.DesignDryWeatherTreatmentCapacity, 100);
                Add(FieldDefinitionType.AverageTreatmentFlowrate.ToGridHeaderString("Average Treatment Flow Rate (cfs)"),
                    x => x.TreatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP?.AverageTreatmentFlowrate, 100);
                Add(FieldDefinitionType.MonthsOperational.ToGridHeaderString("Months Operational"),
                    x => x.TreatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP?.MonthsOfOperation?
                        .MonthsOfOperationDisplayName, 100);
                break;
            case TreatmentBMPModelingTypeEnum.Drywell:
                Add(FieldDefinitionType.TotalEffectiveBMPVolume.ToGridHeaderString("Total Effective BMP Volume (cu ft)"),
                    x => x.TreatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP?.TotalEffectiveBMPVolume, 100);
                Add(FieldDefinitionType.InfiltrationDischargeRate.ToGridHeaderString("Infiltration Discharge Rate (cfs)"),
                    x => x.TreatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP?.InfiltrationDischargeRate, 100);
                break;
            case TreatmentBMPModelingTypeEnum.HydrodynamicSeparator:
            case TreatmentBMPModelingTypeEnum.ProprietaryBiotreatment:
            case TreatmentBMPModelingTypeEnum.ProprietaryTreatmentControl:
                Add(FieldDefinitionType.TreatmentRate.ToGridHeaderString("Treatment Rate (cfs)"),
                    x => x.TreatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP?.TreatmentRate, 100);
                Add(FieldDefinitionType.TimeOfConcentration.ToGridHeaderString("Time Of Concentration (mins)"),
                    x => x.TreatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP?.TimeOfConcentration?
                        .TimeOfConcentrationDisplayName, 100);
                break;
            case TreatmentBMPModelingTypeEnum.LowFlowDiversions:
                Add(FieldDefinitionType.DesignLowFlowDiversionCapacity.ToGridHeaderString("Design Low Flow Diversion Capacity (gpd)"),
                    x => x.TreatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP?.DesignLowFlowDiversionCapacity, 100);
                Add(FieldDefinitionType.AverageDivertedFlowrate.ToGridHeaderString("Average Diverted Flow Rate (gpd)"),
                    x => x.TreatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP?.AverageDivertedFlowrate, 100);
                Add(FieldDefinitionType.MonthsOperational.ToGridHeaderString("Months Operational"),
                    x => x.TreatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP?.MonthsOfOperation?
                        .MonthsOfOperationDisplayName, 100);
                break;
            case TreatmentBMPModelingTypeEnum.VegetatedFilterStrip:
            case TreatmentBMPModelingTypeEnum.VegetatedSwale:
                Add(FieldDefinitionType.TimeOfConcentration.ToGridHeaderString("Time Of Concentration (mins)"),
                    x => x.TreatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP?.TimeOfConcentration?
                        .TimeOfConcentrationDisplayName, 100);
                Add(FieldDefinitionType.TreatmentRate.ToGridHeaderString("Treatment Rate (cfs)"),
                    x => x.TreatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP?.TreatmentRate, 100);
                Add(FieldDefinitionType.WettedFootprint.ToGridHeaderString("Wetted Footprint (sq ft)"),
                    x => x.TreatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP?.WettedFootprint, 100);
                Add(FieldDefinitionType.EffectiveRetentionDepth.ToGridHeaderString("Effective Retention Depth (ft)"),
                    x => x.TreatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP?.EffectiveRetentionDepth, 100);
                Add(FieldDefinitionType.UnderlyingHydrologicSoilGroupHSG.ToGridHeaderString("Underlying Hydrologic Soil Group"),
                    x => x.TreatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP?.UnderlyingHydrologicSoilGroup?
                        .UnderlyingHydrologicSoilGroupDisplayName, 100);
                break;
        }

        Add(FieldDefinitionType.DryWeatherFlowOverride.ToGridHeaderString("Dry Weather Flow Override"),
            x => x.TreatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP?.DryWeatherFlowOverride?.DryWeatherFlowOverrideDisplayName, 100);
    }

    private string GetCustomAttributeValue(ICollection<CustomAttribute> customAttributes, TreatmentBMPTypeCustomAttributeType treatmentBMPTypeCustomAttributeType)
    {
        if (customAttributes.Any())
        {
            var customAttribute = customAttributes.SingleOrDefault(x =>
                x.CustomAttributeTypeID == treatmentBMPTypeCustomAttributeType.CustomAttributeTypeID);
            if (customAttribute != null)
            {
                return string.Join(", ", customAttribute.CustomAttributeValues.OrderBy(x => x.AttributeValue).Select(x => x.AttributeValue));
            }
        }
        return string.Empty;
    }


}