/*-----------------------------------------------------------------------
<copyright file="TreatmentBMPGridSpec.cs" company="Tahoe Regional Planning Agency">
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

using Neptune.Web.Models;
using System.Globalization;
using Microsoft.AspNetCore.Html;
using Neptune.EFModels.Entities;
using Neptune.Web.Controllers;
using Neptune.Web.Common;
using Neptune.Web.Common.DhtmlWrappers;
using Neptune.Web.Common.HtmlHelperExtensions;
using Microsoft.AspNetCore.Routing;


namespace Neptune.Web.Views.TreatmentBMP
{
    public class TreatmentBMPGridSpec : GridSpec<EFModels.Entities.vTreatmentBMPDetailed>
    {
        public TreatmentBMPGridSpec(Person currentPerson, bool showDelete, bool showEdit, LinkGenerator linkGenerator)
        {
            var organizationDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<OrganizationController>.BuildUrlFromExpression(linkGenerator, t => t.Detail(UrlTemplate.Parameter1Int)));
            var stormwaterJurisdictionDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<JurisdictionController>.BuildUrlFromExpression(linkGenerator, t => t.Detail(UrlTemplate.Parameter1Int)));
            var detailUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, t => t.Detail(UrlTemplate.Parameter1Int)));
            var editUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, t => t.Edit(UrlTemplate.Parameter1Int)));
            if (showDelete)
            {
                var deleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, t => t.Delete(UrlTemplate.Parameter1Int)));
                BulkDeleteModalDialogForm = new BulkDeleteModalDialogForm(
                        SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.BulkDeleteTreatmentBMPs(null)),
                        "Delete Checked BMPs",
                        "Delete Treatment BMPs",
                        "TreatmentBMPID",
                        "TreatmentBMPIDList");
                AddCheckBoxColumn();
                Add("TreatmentBMPID", x => x.TreatmentBMPID, 0);
                Add(string.Empty, x =>
                {
                    var userHasDeletePermission = currentPerson.IsManagerOrAdmin() && currentPerson.IsAssignedToStormwaterJurisdiction(x.StormwaterJurisdictionID);
                    return DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(deleteUrlTemplate.ParameterReplace(x.TreatmentBMPID),
                            userHasDeletePermission, userHasDeletePermission);
                }, 30, DhtmlxGridColumnFilterType.None);
            }
            if (showEdit)
            {
                Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeEditIconAsHyperlinkBootstrap(editUrlTemplate.ParameterReplace(x.TreatmentBMPID), currentPerson.IsAssignedToStormwaterJurisdiction(x.StormwaterJurisdictionID)), 30, DhtmlxGridColumnFilterType.None);
            }

            Add(string.Empty, x => UrlTemplate.MakeHrefString(detailUrlTemplate.ParameterReplace(x.TreatmentBMPID), "View", new Dictionary<string, string> { { "class", "gridButton" } }), 50, DhtmlxGridColumnFilterType.None);
            Add(FieldDefinitionType.TreatmentBMP.ToGridHeaderString("Name"), x => UrlTemplate.MakeHrefString(detailUrlTemplate.ParameterReplace(x.TreatmentBMPID), x.TreatmentBMPName), 170, DhtmlxGridColumnFilterType.Html);
            Add(FieldDefinitionType.Jurisdiction.ToGridHeaderString("Jurisdiction"), x => UrlTemplate.MakeHrefString(stormwaterJurisdictionDetailUrlTemplate.ParameterReplace(x.StormwaterJurisdictionID), x.OrganizationName), 170);
            Add("Owner Organization", x => UrlTemplate.MakeHrefString(organizationDetailUrlTemplate.ParameterReplace(x.OwnerOrganizationID), x.OwnerOrganizationName), 170);
            Add(FieldDefinitionType.TreatmentBMPType.ToGridHeaderString("Type"), x => x.TreatmentBMPTypeName, 100, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Year Built", x => x.YearBuilt,100, DhtmlxGridColumnFormatType.Date);
            Add("Notes", x => x.Notes, 195);
            Add("Last Assessment Date", x => x.LatestAssessmentDate, 130);
            Add("Last Assessed Score", x => x.LatestAssessmentScore.HasValue ? x.LatestAssessmentScore.Value.ToString("0.0") : "-", 100, DhtmlxGridColumnFilterType.FormattedNumeric);
            Add("# of Assessments", x => x.NumberOfAssessments, 100, DhtmlxGridColumnFormatType.Integer, DhtmlxGridColumnAggregationType.Total);
            Add("Last Maintenance Date", x => x.LatestMaintenanceDate, 130, DhtmlxGridColumnFormatType.Date);
            Add("# of Maintenance Events", x => x.NumberOfMaintenanceRecords, 100, DhtmlxGridColumnFormatType.Integer, DhtmlxGridColumnAggregationType.Total);
            Add("Benchmark and Threshold Set?", x => (x.NumberOfBenchmarkAndThresholds == x.NumberOfBenchmarkAndThresholdsEntered).ToYesNo(), 80, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Required Lifespan of Installation", x => x.TreatmentBMPLifespanTypeDisplayName ?? "Unknown", 170, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Lifespan End Date (if Fixed End Date)", x => x.TreatmentBMPLifespanEndDate, 130, DhtmlxGridColumnFormatType.Date);
            Add(FieldDefinitionType.RequiredFieldVisitsPerYear.ToGridHeaderString(), x => x.RequiredFieldVisitsPerYear, 130, DhtmlxGridColumnFormatType.Integer);
            Add(FieldDefinitionType.RequiredPostStormFieldVisitsPerYear.ToGridHeaderString(), x => x.RequiredPostStormFieldVisitsPerYear, 130, DhtmlxGridColumnFormatType.Integer);
            Add(FieldDefinitionType.SizingBasis.ToGridHeaderString(), x => x.SizingBasisTypeDisplayName, 130, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(FieldDefinitionType.TrashCaptureStatus.ToGridHeaderString(), x => x.TrashCaptureStatusTypeDisplayName, 130, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(FieldDefinitionType.DelineationType.ToGridHeaderString(), x => string.IsNullOrWhiteSpace(x.DelineationTypeDisplayName) ? new HtmlString("<p class='systemText'>No Delineation Provided</p>") : new HtmlString(x.DelineationTypeDisplayName), 130,
                DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
        }
    }

    public class TreatmentBMPAssessmentSummaryGridSpec : GridSpec<EFModels.Entities.TreatmentBMPAssessmentSummary>
    {
        public TreatmentBMPAssessmentSummaryGridSpec(LinkGenerator linkGenerator)
        {
            var detailUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, t => t.Detail(UrlTemplate.Parameter1Int)));
            var organizationDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<OrganizationController>.BuildUrlFromExpression(linkGenerator, t => t.Detail(UrlTemplate.Parameter1Int)));
            var stormwaterJurisdictionDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<JurisdictionController>.BuildUrlFromExpression(linkGenerator, t => t.Detail(UrlTemplate.Parameter1Int)));

            Add(FieldDefinitionType.TreatmentBMP.ToGridHeaderString("Name"), x => UrlTemplate.MakeHrefString(detailUrlTemplate.ParameterReplace(x.AssessmentSummary.TreatmentBMPID), x.AssessmentSummary.TreatmentBMPName), 170, DhtmlxGridColumnFilterType.Html);
            Add(FieldDefinitionType.Jurisdiction.ToGridHeaderString("Jurisdiction"), x => UrlTemplate.MakeHrefString(stormwaterJurisdictionDetailUrlTemplate.ParameterReplace( x.AssessmentSummary.StormwaterJurisdictionID), x.AssessmentSummary.StormwaterJurisdictionName), 170);
            Add("Owner Organization", x => UrlTemplate.MakeHrefString(organizationDetailUrlTemplate.ParameterReplace(x.AssessmentSummary.OwnerOrganizationID), x.AssessmentSummary.StormwaterJurisdictionName), 170);
            Add("Last Assessment Date", x => x.AssessmentSummary.LastAssessmentDate, 130, DhtmlxGridColumnFormatType.Date);
            Add("Last Assessed Score", x => x.AssessmentSummary.AssessmentScore.GetValueOrDefault().ToString(CultureInfo.InvariantCulture), 100, DhtmlxGridColumnFilterType.FormattedNumeric);
            Add("Last Assessment Type", x => x.AssessmentSummary.FieldVisitType, 100, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Failure Notes", x => x.Notes, 800, DhtmlxGridColumnFilterType.Text);
        }
    }

    public class ViewTreatmentBMPModelingAttributesGridSpec : GridSpec<EFModels.Entities.TreatmentBMP>
    {
        public ViewTreatmentBMPModelingAttributesGridSpec(LinkGenerator linkGenerator, Dictionary<int, EFModels.Entities.Delineation?> delineationsDict, Dictionary<int, string?> watershedsDict, Dictionary<int, double> precipitationZonesDict)
        {
            var treatmentBMPTypeDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPTypeController>.BuildUrlFromExpression(linkGenerator, t => t.Detail(UrlTemplate.Parameter1Int)));
            var stormwaterJurisdictionDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<JurisdictionController>.BuildUrlFromExpression(linkGenerator, t => t.Detail(UrlTemplate.Parameter1Int)));
            var detailUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, t => t.Detail(UrlTemplate.Parameter1Int)));
            Add(FieldDefinitionType.TreatmentBMP.ToGridHeaderString("BMP Name"), x => UrlTemplate.MakeHrefString(detailUrlTemplate.ParameterReplace(x.PrimaryKey), x.TreatmentBMPName), 170, DhtmlxGridColumnFilterType.Html);
            Add(FieldDefinitionType.FullyParameterized.ToGridHeaderString("Fully Parameterized?"), x => x.IsFullyParameterized(delineationsDict[x.TreatmentBMPID]) ? new HtmlString("Yes") : new HtmlString("No"), 120);
            Add(FieldDefinitionType.DelineationType.ToGridHeaderString("Delineation Type"), x=> delineationsDict[x.TreatmentBMPID]?.DelineationType.DelineationTypeDisplayName, 120, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("DelineationStatus", x=> delineationsDict[x.TreatmentBMPID]?.GetDelineationStatus(), 120, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(FieldDefinitionType.TreatmentBMPType.ToGridHeaderString("Type"), x => UrlTemplate.MakeHrefString(treatmentBMPTypeDetailUrlTemplate.ParameterReplace(x.TreatmentBMPTypeID), x.TreatmentBMPType.TreatmentBMPTypeName), 100, DhtmlxGridColumnFilterType.Text);
            Add(FieldDefinitionType.Jurisdiction.ToGridHeaderString("Jurisdiction"), x => UrlTemplate.MakeHrefString(stormwaterJurisdictionDetailUrlTemplate.ParameterReplace(x.StormwaterJurisdictionID), x.StormwaterJurisdiction.Organization.OrganizationName), 170);
            Add(FieldDefinitionType.Watershed.ToGridHeaderString("Watershed"), x => x.WatershedID.HasValue ? watershedsDict[x.WatershedID.Value] : null, 170);
            Add(FieldDefinitionType.DesignStormwaterDepth.ToGridHeaderString("Design Stormwater Depth (in)"), x => x.PrecipitationZoneID.HasValue ? precipitationZonesDict[x.PrecipitationZoneID.Value] : null, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(FieldDefinitionType.AverageDivertedFlowrate.ToGridHeaderString("Average Diverted Flow Rate (gpd)"), x => x.TreatmentBMPModelingAttributeTreatmentBMP?.AverageDivertedFlowrate, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(FieldDefinitionType.AverageTreatmentFlowrate.ToGridHeaderString("Average Treatment Flow Rate (cfs)"), x => x.TreatmentBMPModelingAttributeTreatmentBMP?.AverageTreatmentFlowrate, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(FieldDefinitionType.DesignDryWeatherTreatmentCapacity.ToGridHeaderString("Design Dry Weather Treatment Capacity (cfs)"), x => x.TreatmentBMPModelingAttributeTreatmentBMP?.DesignDryWeatherTreatmentCapacity, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(FieldDefinitionType.DesignLowFlowDiversionCapacity.ToGridHeaderString("Design Low Flow Diversion Capacity (gpd)"), x => x.TreatmentBMPModelingAttributeTreatmentBMP?.DesignLowFlowDiversionCapacity, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(FieldDefinitionType.DesignMediaFiltrationRate.ToGridHeaderString("Design Media Filtration Rate (in/hr)"), x => x.TreatmentBMPModelingAttributeTreatmentBMP?.DesignMediaFiltrationRate, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(FieldDefinitionType.DrawdownTimeForWQDetentionVolume.ToGridHeaderString("Drawdown Time For WQ Detention Volume (hours)"), x => x.TreatmentBMPModelingAttributeTreatmentBMP?.DrawdownTimeforWQDetentionVolume, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(FieldDefinitionType.EffectiveFootprint.ToGridHeaderString("Effective Footprint (sq ft)"), x => x.TreatmentBMPModelingAttributeTreatmentBMP?.EffectiveFootprint, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(FieldDefinitionType.EffectiveRetentionDepth.ToGridHeaderString("Effective Retention Depth (ft)"), x => x.TreatmentBMPModelingAttributeTreatmentBMP?.EffectiveRetentionDepth, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(FieldDefinitionType.InfiltrationDischargeRate.ToGridHeaderString("Infiltration Discharge Rate (cfs)"), x => x.TreatmentBMPModelingAttributeTreatmentBMP?.InfiltrationDischargeRate, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(FieldDefinitionType.InfiltrationSurfaceArea.ToGridHeaderString("Infiltration Surface Area (sq ft)"), x => x.TreatmentBMPModelingAttributeTreatmentBMP?.InfiltrationSurfaceArea, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(FieldDefinitionType.MediaBedFootprint.ToGridHeaderString("Media Bed Footprint (sq ft)"), x => x.TreatmentBMPModelingAttributeTreatmentBMP?.MediaBedFootprint, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(FieldDefinitionType.MonthsOperational.ToGridHeaderString("Months Operational"), x => x.TreatmentBMPModelingAttributeTreatmentBMP?.MonthsOfOperation != null ?x.TreatmentBMPModelingAttributeTreatmentBMP?.MonthsOfOperation.MonthsOfOperationDisplayName : string.Empty, 100, DhtmlxGridColumnFilterType.Html);
            Add(FieldDefinitionType.PermanentPoolOrWetlandVolume.ToGridHeaderString("Permanent Pool Or Wetland Volume (cu ft)"), x => x.TreatmentBMPModelingAttributeTreatmentBMP?.PermanentPoolorWetlandVolume, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(FieldDefinitionType.StorageVolumeBelowLowestOutletElevation.ToGridHeaderString("Storage Volume Below Lowest Outlet Elevation (cu ft)"), x => x.TreatmentBMPModelingAttributeTreatmentBMP?.StorageVolumeBelowLowestOutletElevation, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(FieldDefinitionType.SummerHarvestedWaterDemand.ToGridHeaderString("Summer Harvested Water Demand (gpd)"), x => x.TreatmentBMPModelingAttributeTreatmentBMP?.SummerHarvestedWaterDemand, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(FieldDefinitionType.TimeOfConcentration.ToGridHeaderString("Time Of Concentration (mins)"), x => x.TreatmentBMPModelingAttributeTreatmentBMP?.TimeOfConcentrationID != null ? int.Parse(x.TreatmentBMPModelingAttributeTreatmentBMP.TimeOfConcentration.TimeOfConcentrationDisplayName) : null, 100, DhtmlxGridColumnFormatType.Integer);
            Add(FieldDefinitionType.TotalEffectiveBMPVolume.ToGridHeaderString("Total Effective BMP Volume (cu ft)"), x => x.TreatmentBMPModelingAttributeTreatmentBMP?.TotalEffectiveBMPVolume, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(FieldDefinitionType.TotalEffectiveDrywellBMPVolume.ToGridHeaderString("Total Effective Drywell BMP Volume (cu ft)"), x => x.TreatmentBMPModelingAttributeTreatmentBMP?.TotalEffectiveDrywellBMPVolume, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(FieldDefinitionType.TreatmentRate.ToGridHeaderString("Treatment Rate (cfs)"), x => x.TreatmentBMPModelingAttributeTreatmentBMP?.TreatmentRate, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(FieldDefinitionType.UnderlyingHydrologicSoilGroupHSG.ToGridHeaderString("Underlying Hydrologic Soil Group"), x => x.TreatmentBMPModelingAttributeTreatmentBMP?.UnderlyingHydrologicSoilGroup != null ? x.TreatmentBMPModelingAttributeTreatmentBMP?.UnderlyingHydrologicSoilGroup.UnderlyingHydrologicSoilGroupDisplayName : string.Empty, 100, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(FieldDefinitionType.UnderlyingInfiltrationRate.ToGridHeaderString("Underlying Infiltration Rate (in/hr)"), x => x.TreatmentBMPModelingAttributeTreatmentBMP?.UnderlyingInfiltrationRate, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(FieldDefinitionType.WaterQualityDetentionVolume.ToGridHeaderString("Extended Detention Surcharge Volume (cu ft)"), x => x.TreatmentBMPModelingAttributeTreatmentBMP?.WaterQualityDetentionVolume, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(FieldDefinitionType.WettedFootprint.ToGridHeaderString("Wetted Footprint (sq ft)"), x => x.TreatmentBMPModelingAttributeTreatmentBMP?.WettedFootprint, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(FieldDefinitionType.WinterHarvestedWaterDemand.ToGridHeaderString("Winter Harvested Water Demand (gpd)"), x => x.TreatmentBMPModelingAttributeTreatmentBMP?.WinterHarvestedWaterDemand, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(FieldDefinitionType.UpstreamBMP.ToGridHeaderString("Upstream BMP"), x => delineationsDict[x.TreatmentBMPID] != null ? UrlTemplate.MakeHrefString(detailUrlTemplate.ParameterReplace(delineationsDict[x.TreatmentBMPID].TreatmentBMPID), delineationsDict[x.TreatmentBMPID].TreatmentBMP.TreatmentBMPName) : new HtmlString(""), 170, DhtmlxGridColumnFilterType.Html);
            Add(FieldDefinitionType.DryWeatherFlowOverride.ToGridHeaderString("Dry Weather Flow Override"),
                x => x.TreatmentBMPModelingAttributeTreatmentBMP?.DryWeatherFlowOverride != null
                    ? x.TreatmentBMPModelingAttributeTreatmentBMP.DryWeatherFlowOverride.DryWeatherFlowOverrideDisplayName
                    : DryWeatherFlowOverride.No.DryWeatherFlowOverrideDisplayName, 100, DhtmlxGridColumnFilterType.SelectFilterStrict);
        }
    }
}

