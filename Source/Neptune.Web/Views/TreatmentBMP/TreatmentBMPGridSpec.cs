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

using LtInfo.Common;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.Views;
using Neptune.Web.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;


namespace Neptune.Web.Views.TreatmentBMP
{
    public class TreatmentBMPGridSpec : GridSpec<Models.vTreatmentBMPDetailed>
    {
        public TreatmentBMPGridSpec(Person currentPerson, bool showDelete, bool showEdit)
        {
            if (showDelete)
            {
                Add(string.Empty, x =>
                {
                    var userHasDeletePermission = currentPerson.IsManagerOrAdmin() && currentPerson.IsAssignedToStormwaterJurisdiction(x.StormwaterJurisdictionID);
                    return DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(TreatmentBMPModelExtensions.DeleteUrlTemplate.ParameterReplace(x.TreatmentBMPID),
                            userHasDeletePermission, userHasDeletePermission);
                }, 30, DhtmlxGridColumnFilterType.None);
            }
            if (showEdit)
            {
                Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeEditIconAsHyperlinkBootstrap(TreatmentBMPModelExtensions.EditUrlTemplate.ParameterReplace(x.TreatmentBMPID), currentPerson.IsAssignedToStormwaterJurisdiction(x.StormwaterJurisdictionID)), 30, DhtmlxGridColumnFilterType.None);
            }

            Add(string.Empty, x => UrlTemplate.MakeHrefString(TreatmentBMPModelExtensions.DetailUrlTemplate.ParameterReplace(x.TreatmentBMPID), "View", new Dictionary<string, string> { { "class", "gridButton" } }), 50, DhtmlxGridColumnFilterType.None);
            Add(Models.FieldDefinition.TreatmentBMP.ToGridHeaderString("Name"), x => UrlTemplate.MakeHrefString(TreatmentBMPModelExtensions.DetailUrlTemplate.ParameterReplace(x.TreatmentBMPID), x.TreatmentBMPName), 170, DhtmlxGridColumnFilterType.Html);
            Add(Models.FieldDefinition.Jurisdiction.ToGridHeaderString("Jurisdiction"), x => UrlTemplate.MakeHrefString(StormwaterJurisdictionModelExtensions.DetailUrlTemplate.ParameterReplace(x.StormwaterJurisdictionID), x.OrganizationName), 170);
            Add("Owner Organization", x => UrlTemplate.MakeHrefString(OrganizationModelExtensions.DetailUrlTemplate.ParameterReplace(x.OwnerOrganizationID), x.OwnerOrganizationName), 170);
            Add(Models.FieldDefinition.TreatmentBMPType.ToGridHeaderString("Type"), x => x.TreatmentBMPTypeName, 100, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Notes", x => x.Notes, 195);
            Add("Last Assessment Date", x => x.LatestAssessmentDate, 130);
            Add("Last Assessed Score", x => x.LatestAssessmentScore.HasValue ? x.LatestAssessmentScore.Value.ToString("0.0") : "-", 100, DhtmlxGridColumnFilterType.FormattedNumeric);
            Add("# of Assessments", x => x.NumberOfAssessments, 100, DhtmlxGridColumnFormatType.Integer, DhtmlxGridColumnAggregationType.Total);
            Add("Last Maintenance Date", x => x.LatestMaintenanceDate, 130, DhtmlxGridColumnFormatType.Date);
            Add("# of Maintenance Events", x => x.NumberOfMaintenanceRecords, 100, DhtmlxGridColumnFormatType.Integer, DhtmlxGridColumnAggregationType.Total);
            Add("Benchmark and Threshold Set?", x => (x.NumberOfBenchmarkAndThresholds == x.NumberOfBenchmarkAndThresholdsEntered).ToYesNo(), 80, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Required Lifespan of Installation", x => x.TreatmentBMPLifespanTypeDisplayName ?? "Unknown", 170, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Lifespan End Date (if Fixed End Date)", x => x.TreatmentBMPLifespanEndDate, 130, DhtmlxGridColumnFormatType.Date);
            Add(Models.FieldDefinition.RequiredFieldVisitsPerYear.ToGridHeaderString(), x => x.RequiredFieldVisitsPerYear, 130, DhtmlxGridColumnFormatType.Integer);
            Add(Models.FieldDefinition.RequiredPostStormFieldVisitsPerYear.ToGridHeaderString(), x => x.RequiredPostStormFieldVisitsPerYear, 130, DhtmlxGridColumnFormatType.Integer);
            Add(Models.FieldDefinition.SizingBasis.ToGridHeaderString(), x => x.SizingBasisTypeDisplayName, 130, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.TrashCaptureStatus.ToGridHeaderString(), x => x.TrashCaptureStatusTypeDisplayName, 130, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.DelineationType.ToGridHeaderString(), x => string.IsNullOrWhiteSpace(x.DelineationTypeDisplayName) ? new HtmlString("<p class='systemText'>No Delineation Provided</p>") : new HtmlString(x.DelineationTypeDisplayName), 130,
                DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
        }
    }

    public class TreatmentBMPAssessmentSummaryGridSpec : GridSpec<Models.TreatmentBMPAssessmentSummary>
    {
        public TreatmentBMPAssessmentSummaryGridSpec()
        {
            Add(Models.FieldDefinition.TreatmentBMP.ToGridHeaderString("Name"), x => UrlTemplate.MakeHrefString(x.AssessmentSummary.GetDetailUrl(), x.AssessmentSummary.TreatmentBMPName), 170, DhtmlxGridColumnFilterType.Html);
            Add(Models.FieldDefinition.Jurisdiction.ToGridHeaderString("Jurisdiction"), x => UrlTemplate.MakeHrefString(x.AssessmentSummary.GetJurisdictionSummaryUrl(), x.AssessmentSummary.StormwaterJurisdictionName), 170);
            Add("Owner Organization", x => UrlTemplate.MakeHrefString(x.AssessmentSummary.GetOwnerOrganizationSummaryUrl(), x.AssessmentSummary.StormwaterJurisdictionName), 170);
            Add("Last Assessment Date", x => x.AssessmentSummary.LastAssessmentDate, 130, DhtmlxGridColumnFormatType.Date);
            Add("Last Assessed Score", x => x.AssessmentSummary.AssessmentScore.GetValueOrDefault().ToString(CultureInfo.InvariantCulture), 100, DhtmlxGridColumnFilterType.FormattedNumeric);
            Add("Last Assessment Type", x => x.AssessmentSummary.FieldVisitType, 100, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Failure Notes", x => x.Notes, 800, DhtmlxGridColumnFilterType.Text);
        }
    }

    public class ViewTreatmentBMPModelingAttributesGridSpec : GridSpec<Models.vViewTreatmentBMPModelingAttributes>
    {
        public ViewTreatmentBMPModelingAttributesGridSpec()
        {
            Add(Models.FieldDefinition.TreatmentBMP.ToGridHeaderString("BMP Name"), x => UrlTemplate.MakeHrefString(TreatmentBMPModelExtensions.DetailUrlTemplate.ParameterReplace(x.PrimaryKey), x.TreatmentBMPName), 170, DhtmlxGridColumnFilterType.Html);
            Add(Models.FieldDefinition.FullyParameterized.ToGridHeaderString("Fully Parameterized?"), x => (new TreatmentBMPPrimaryKey(x.PrimaryKey)).EntityObject.IsFullyParameterized() ? new HtmlString("Yes") : new HtmlString("No"), 120);
            Add(Models.FieldDefinition.DelineationType.ToGridHeaderString("Delineation Type"), x=>x.DelineationType, 120, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("DelineationStatus", x=>x.DelineationStatus, 120, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.TreatmentBMPType.ToGridHeaderString("Type"), x => UrlTemplate.MakeHrefString(Models.TreatmentBMPTypeModelExtensions.DetailUrlTemplate.ParameterReplace(x.TreatmentBMPTypeID), x.TreatmentBMPTypeName), 100, DhtmlxGridColumnFilterType.Text);
            Add(Models.FieldDefinition.Jurisdiction.ToGridHeaderString("Jurisdiction"), x => UrlTemplate.MakeHrefString(StormwaterJurisdictionModelExtensions.DetailUrlTemplate.ParameterReplace(x.StormwaterJurisdictionID), x.OrganizationName), 170);
            Add(Models.FieldDefinition.Watershed.ToGridHeaderString("Watershed"), x => x.WatershedName, 170);
            Add(Models.FieldDefinition.DesignStormwaterDepth.ToGridHeaderString("Design Stormwater Depth (in)"), x => x.DesignStormwaterDepthInInches, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(Models.FieldDefinition.AverageDivertedFlowrate.ToGridHeaderString("Average Diverted Flow Rate (gpd)"), x => x.AverageDivertedFlowrate, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(Models.FieldDefinition.AverageTreatmentFlowrate.ToGridHeaderString("Average Treatment Flow Rate (cfs)"), x => x.AverageTreatmentFlowrate, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(Models.FieldDefinition.DesignDryWeatherTreatmentCapacity.ToGridHeaderString("Design Dry Weather Treatment Capacity (cfs)"), x => x.DesignDryWeatherTreatmentCapacity, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(Models.FieldDefinition.DesignLowFlowDiversionCapacity.ToGridHeaderString("Design Low Flow Diversion Capacity (gpd)"), x => x.DesignLowFlowDiversionCapacity, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(Models.FieldDefinition.DesignMediaFiltrationRate.ToGridHeaderString("Design Media Filtration Rate (in/hr)"), x => x.DesignMediaFiltrationRate, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(Models.FieldDefinition.DesignResidenceTimeForPermanentPool.ToGridHeaderString("Design Residence Time for Permanent Pool (days)"), x => x.DesignResidenceTimeforPermanentPool, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(Models.FieldDefinition.DrawdownTimeForWQDetentionVolume.ToGridHeaderString("Drawdown Time For WQ Detention Volume (hours)"), x => x.DrawdownTimeforWQDetentionVolume, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(Models.FieldDefinition.EffectiveFootprint.ToGridHeaderString("Effective Footprint (sq ft)"), x => x.EffectiveFootprint, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(Models.FieldDefinition.EffectiveRetentionDepth.ToGridHeaderString("Effective Retention Depth (ft)"), x => x.EffectiveRetentionDepth, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(Models.FieldDefinition.InfiltrationDischargeRate.ToGridHeaderString("Infiltration Discharge Rate (cfs)"), x => x.InfiltrationDischargeRate, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(Models.FieldDefinition.InfiltrationSurfaceArea.ToGridHeaderString("Infiltration Surface Area (sq ft)"), x => x.InfiltrationSurfaceArea, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(Models.FieldDefinition.MediaBedFootprint.ToGridHeaderString("Media Bed Footprint (sq ft)"), x => x.MediaBedFootprint, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(Models.FieldDefinition.MonthsOperational.ToGridHeaderString("Months Operational"), x => x.OperationMonths, 100, DhtmlxGridColumnFilterType.Html);
            Add(Models.FieldDefinition.PermanentPoolOrWetlandVolume.ToGridHeaderString("Permanent Pool Or Wetland Volume (cu ft)"), x => x.PermanentPoolorWetlandVolume, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(Models.FieldDefinition.StorageVolumeBelowLowestOutletElevation.ToGridHeaderString("Storage Volume Below Lowest Outlet Elevation (cu ft)"), x => x.StorageVolumeBelowLowestOutletElevation, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(Models.FieldDefinition.SummerHarvestedWaterDemand.ToGridHeaderString("Summer Harvested Water Demand (gpd)"), x => x.SummerHarvestedWaterDemand, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(Models.FieldDefinition.TimeOfConcentration.ToGridHeaderString("Time Of Concentration (mins)"), x => x.TimeOfConcentrationID != null ? Int32.Parse(TimeOfConcentration.AllLookupDictionary[x.TimeOfConcentrationID.Value].TimeOfConcentrationDisplayName) : (int?)null, 100, DhtmlxGridColumnFormatType.Integer);
            Add(Models.FieldDefinition.TotalEffectiveBMPVolume.ToGridHeaderString("Total Effective BMP Volume (cu ft)"), x => x.TotalEffectiveBMPVolume, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(Models.FieldDefinition.TotalEffectiveDrywellBMPVolume.ToGridHeaderString("Total Effective Drywell BMP Volume (cu ft)"), x => x.TotalEffectiveDrywellBMPVolume, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(Models.FieldDefinition.TreatmentRate.ToGridHeaderString("Treatment Rate (cfs)"), x => x.TreatmentRate, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(Models.FieldDefinition.UnderlyingHydrologicSoilGroupHSG.ToGridHeaderString("Underlying Hydrologic Soil Group"), x => x.UnderlyingHydrologicSoilGroupID != null ? UnderlyingHydrologicSoilGroup.AllLookupDictionary[x.UnderlyingHydrologicSoilGroupID.Value].UnderlyingHydrologicSoilGroupDisplayName : String.Empty, 100, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.UnderlyingInfiltrationRate.ToGridHeaderString("Underlying Infiltration Rate (in/hr)"), x => x.UnderlyingInfiltrationRate, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(Models.FieldDefinition.WaterQualityDetentionVolume.ToGridHeaderString("Water Quality Detention Volume (cu ft)"), x => x.WaterQualityDetentionVolume, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(Models.FieldDefinition.WettedFootprint.ToGridHeaderString("Wetted Footprint (sq ft)"), x => x.WettedFootprint, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(Models.FieldDefinition.WinterHarvestedWaterDemand.ToGridHeaderString("Winter Harvested Water Demand (gpd)"), x => x.WinterHarvestedWaterDemand, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(Models.FieldDefinition.UpstreamBMP.ToGridHeaderString("Upstream BMP"), x => x.UpstreamBMPID != null ? UrlTemplate.MakeHrefString(TreatmentBMPModelExtensions.DetailUrlTemplate.ParameterReplace((int)x.UpstreamBMPID), x.UpstreamBMPName) : new HtmlString(""), 170, DhtmlxGridColumnFilterType.Html);

        }
    }
}

