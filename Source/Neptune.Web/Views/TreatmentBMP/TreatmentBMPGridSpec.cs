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

using System;
using LtInfo.Common;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.Views;
using Neptune.Web.Models;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using DocumentFormat.OpenXml.EMMA;


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
            Add("Fully Parameterized?", x => CheckForParameterizationErrors((new TreatmentBMPPrimaryKey(x.PrimaryKey)).EntityObject), 100);
            Add(Models.FieldDefinition.TreatmentBMPType.ToGridHeaderString("Type"), x => UrlTemplate.MakeHrefString(Models.TreatmentBMPTypeModelExtensions.DetailUrlTemplate.ParameterReplace(x.TreatmentBMPTypeID), x.TreatmentBMPTypeName), 100);
            Add(Models.FieldDefinition.Jurisdiction.ToGridHeaderString("Jurisdiction"), x => UrlTemplate.MakeHrefString(StormwaterJurisdictionModelExtensions.DetailUrlTemplate.ParameterReplace(x.StormwaterJurisdictionID), x.OrganizationName), 170);
            Add(Models.FieldDefinition.UpstreamBMP.ToGridHeaderString("Upstream BMP"), x => x.UpstreamBMPID != null ? UrlTemplate.MakeHrefString(TreatmentBMPModelExtensions.DetailUrlTemplate.ParameterReplace((int)x.UpstreamBMPID), x.UpstreamBMPName) : new HtmlString(""), 170, DhtmlxGridColumnFilterType.Html);
            Add(Models.FieldDefinition.AverageDivertedFlowrate.ToGridHeaderString("Average Diverted Flow Rate"), x => x.AverageDivertedFlowrate, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(Models.FieldDefinition.AverageTreatmentFlowrate.ToGridHeaderString("Average Treatment Flow Rate"), x => x.AverageTreatmentFlowrate, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(Models.FieldDefinition.DesignDryWeatherTreatmentCapacity.ToGridHeaderString("Design Dry Weather Treatment Capacity"), x => x.DesignDryWeatherTreatmentCapacity, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(Models.FieldDefinition.DesignLowFlowDiversionCapacity.ToGridHeaderString("Design Low Flow Diversion Capacity"), x => x.DesignLowFlowDiversionCapacity, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(Models.FieldDefinition.DesignMediaFiltrationRate.ToGridHeaderString("Design Media Filtration Rate"), x => x.DesignMediaFiltrationRate, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(Models.FieldDefinition.DesignResidenceTimeForPermanentPool.ToGridHeaderString("Design Residence Time for Permanent Pool"), x => x.DesignResidenceTimeforPermanentPool, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(Models.FieldDefinition.DrawdownTimeForWQDetentionVolume.ToGridHeaderString("Drawdown Time For WQ Detention Volume"), x => x.DrawdownTimeforWQDetentionVolume, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(Models.FieldDefinition.EffectiveFootprint.ToGridHeaderString("Effective Footprint"), x => x.EffectiveFootprint, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(Models.FieldDefinition.EffectiveRetentionDepth.ToGridHeaderString("Effective Retention Depth"), x => x.EffectiveRetentionDepth, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(Models.FieldDefinition.InfiltrationDischargeRate.ToGridHeaderString("Infiltration Discharge Rate"), x => x.InfiltrationDischargeRate, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(Models.FieldDefinition.InfiltrationSurfaceArea.ToGridHeaderString("Infiltration Surface Area"), x => x.InfiltrationSurfaceArea, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(Models.FieldDefinition.MediaBedFootprint.ToGridHeaderString("Media Bed Footprint"), x => x.MediaBedFootprint, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(Models.FieldDefinition.MonthsOfOperation.ToGridHeaderString("Months Of Operation"), x => x.OperationMonths, 100, DhtmlxGridColumnFilterType.Html);
            Add(Models.FieldDefinition.PermanentPoolOrWetlandVolume.ToGridHeaderString("Permanent Pool Or Wetland Volume"), x => x.PermanentPoolorWetlandVolume, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(Models.FieldDefinition.StorageVolumeBelowLowestOutletElevation.ToGridHeaderString("Storage Volume Below Lowest Outlet Elevation"), x => x.StorageVolumeBelowLowestOutletElevation, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(Models.FieldDefinition.SummerHarvestedWaterDemand.ToGridHeaderString("Summer Harvested Water Demand"), x => x.SummerHarvestedWaterDemand, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(Models.FieldDefinition.TimeOfConcentration.ToGridHeaderString("Time Of Concentration (minutes)"), x => x.TimeOfConcentrationID != null ? Int32.Parse(TimeOfConcentration.AllLookupDictionary[x.TimeOfConcentrationID.Value].TimeOfConcentrationDisplayName) : (int?)null, 100, DhtmlxGridColumnFormatType.Integer);
            Add(Models.FieldDefinition.TotalDrawdownTime.ToGridHeaderString("Total Drawdown Time"), x => x.TotalDrawdownTime, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(Models.FieldDefinition.TotalEffectiveBMPVolume.ToGridHeaderString("Total Effective BMP Volume"), x => x.TotalEffectiveBMPVolume, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(Models.FieldDefinition.TotalEffectiveDrywellBMPVolume.ToGridHeaderString("Total Effective Drywell BMP Volume"), x => x.TotalEffectiveDrywellBMPVolume, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(Models.FieldDefinition.TreatmentRate.ToGridHeaderString("Treatment Rate"), x => x.TreatmentRate, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(Models.FieldDefinition.UnderlyingHydrologicSoilGroupHSG.ToGridHeaderString("Underlying Hydrologic Soil Group"), x => x.UnderlyingHydrologicSoilGroupID != null ? UnderlyingHydrologicSoilGroup.AllLookupDictionary[x.UnderlyingHydrologicSoilGroupID.Value].UnderlyingHydrologicSoilGroupDisplayName : "", 100, DhtmlxGridColumnFilterType.Html);
            Add(Models.FieldDefinition.UnderlyingInfiltrationRate.ToGridHeaderString("Underlying Infiltration Rate"), x => x.UnderlyingInfiltrationRate, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(Models.FieldDefinition.WaterQualityDetentionVolume.ToGridHeaderString("Water Quality Detention Volume"), x => x.WaterQualityDetentionVolume, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(Models.FieldDefinition.WettedFootprint.ToGridHeaderString("Wetted Footprint"), x => x.WettedFootprint, 100, DhtmlxGridColumnFormatType.Decimal);
            Add(Models.FieldDefinition.WinterHarvestedWaterDemand.ToGridHeaderString("Winter Harvested Water Demand"), x => x.WinterHarvestedWaterDemand, 100, DhtmlxGridColumnFormatType.Decimal);
        }

        private HtmlString CheckForParameterizationErrors(Models.TreatmentBMP treatmentBMP)
        {

            if (treatmentBMP.Delineation == null && treatmentBMP.UpstreamBMP?.Delineation == null)
            {
                return new HtmlString("No");
            }

            var bmpModelingType = treatmentBMP.TreatmentBMPType.TreatmentBMPModelingType.ToEnum;
            var bmpModelingAttributes = treatmentBMP.TreatmentBMPModelingAttribute;

            if (bmpModelingAttributes != null)
            {
                if (bmpModelingType ==
                    TreatmentBMPModelingTypeEnum.BioinfiltrationBioretentionWithRaisedUnderdrain && (
                        !bmpModelingAttributes.RoutingConfigurationID.HasValue ||
                        (bmpModelingAttributes.RoutingConfigurationID == (int)RoutingConfigurationEnum.Offline &&
                         !bmpModelingAttributes.DiversionRate.HasValue) ||
                        !bmpModelingAttributes.TotalEffectiveBMPVolume.HasValue ||
                        !bmpModelingAttributes.StorageVolumeBelowLowestOutletElevation.HasValue ||
                        !bmpModelingAttributes.MediaBedFootprint.HasValue ||
                        !bmpModelingAttributes.DesignMediaFiltrationRate.HasValue))
                {
                    return new HtmlString("No");
                }
                else if ((bmpModelingType == TreatmentBMPModelingTypeEnum.BioretentionWithNoUnderdrain ||
                          bmpModelingType == TreatmentBMPModelingTypeEnum.InfiltrationBasin ||
                          bmpModelingType == TreatmentBMPModelingTypeEnum.InfiltrationTrench ||
                          bmpModelingType == TreatmentBMPModelingTypeEnum.PermeablePavement ||
                          bmpModelingType == TreatmentBMPModelingTypeEnum.UndergroundInfiltration) &&
                         (!bmpModelingAttributes.RoutingConfigurationID.HasValue ||
                          (bmpModelingAttributes.RoutingConfigurationID == (int)RoutingConfigurationEnum.Offline &&
                           !bmpModelingAttributes.DiversionRate.HasValue) ||
                          !bmpModelingAttributes.TotalEffectiveBMPVolume.HasValue ||
                          !bmpModelingAttributes.InfiltrationSurfaceArea.HasValue ||
                          !bmpModelingAttributes.UnderlyingInfiltrationRate.HasValue))
                {
                    return new HtmlString("No");
                }
                else if ((bmpModelingType ==
                          TreatmentBMPModelingTypeEnum.BioretentionWithUnderdrainAndImperviousLiner ||
                          bmpModelingType == TreatmentBMPModelingTypeEnum.SandFilters) &&
                         (!bmpModelingAttributes.RoutingConfigurationID.HasValue ||
                          (bmpModelingAttributes.RoutingConfigurationID == (int)RoutingConfigurationEnum.Offline &&
                           !bmpModelingAttributes.DiversionRate.HasValue) ||
                          !bmpModelingAttributes.TotalEffectiveBMPVolume.HasValue ||
                          !bmpModelingAttributes.MediaBedFootprint.HasValue ||
                          !bmpModelingAttributes.DesignMediaFiltrationRate.HasValue))
                {
                    return new HtmlString("No");
                }
                else if (bmpModelingType == TreatmentBMPModelingTypeEnum.CisternsForHarvestAndUse &&
                         (!bmpModelingAttributes.RoutingConfigurationID.HasValue ||
                          (bmpModelingAttributes.RoutingConfigurationID == (int)RoutingConfigurationEnum.Offline &&
                           !bmpModelingAttributes.DiversionRate.HasValue) ||
                          !bmpModelingAttributes.TotalEffectiveBMPVolume.HasValue ||
                          !bmpModelingAttributes.WinterHarvestedWaterDemand.HasValue ||
                          !bmpModelingAttributes.SummerHarvestedWaterDemand.HasValue))
                {
                    return new HtmlString("No");
                }
                else if ((bmpModelingType == TreatmentBMPModelingTypeEnum.ConstructedWetland ||
                          bmpModelingType == TreatmentBMPModelingTypeEnum.WetDetentionBasin) &&
                         (!bmpModelingAttributes.RoutingConfigurationID.HasValue ||
                          (bmpModelingAttributes.RoutingConfigurationID == (int)RoutingConfigurationEnum.Offline &&
                           !bmpModelingAttributes.DiversionRate.HasValue) ||
                          !bmpModelingAttributes.PermanentPoolorWetlandVolume.HasValue ||
                          !bmpModelingAttributes.DrawdownTimeforWQDetentionVolume.HasValue ||
                          !bmpModelingAttributes.WaterQualityDetentionVolume.HasValue ||
                          !bmpModelingAttributes.WinterHarvestedWaterDemand.HasValue ||
                          !bmpModelingAttributes.SummerHarvestedWaterDemand.HasValue))
                {
                    return new HtmlString("No");
                }
                else if ((bmpModelingType == TreatmentBMPModelingTypeEnum.DryExtendedDetentionBasin ||
                          bmpModelingType == TreatmentBMPModelingTypeEnum.FlowDurationControlBasin ||
                          bmpModelingType == TreatmentBMPModelingTypeEnum.FlowDurationControlTank) &&
                         (!bmpModelingAttributes.RoutingConfigurationID.HasValue ||
                          (bmpModelingAttributes.RoutingConfigurationID == (int)RoutingConfigurationEnum.Offline &&
                           !bmpModelingAttributes.DiversionRate.HasValue) ||
                          !bmpModelingAttributes.TotalEffectiveBMPVolume.HasValue ||
                          !bmpModelingAttributes.StorageVolumeBelowLowestOutletElevation.HasValue ||
                          !bmpModelingAttributes.EffectiveFootprint.HasValue ||
                          !bmpModelingAttributes.TotalDrawdownTime.HasValue))
                {
                    return new HtmlString("No");
                }
                else if (bmpModelingType == TreatmentBMPModelingTypeEnum.DryWeatherTreatmentSystems &&
                         (!bmpModelingAttributes.DesignDryWeatherTreatmentCapacity.HasValue &&
                          !bmpModelingAttributes.AverageTreatmentFlowrate.HasValue))
                {
                    return new HtmlString("No");
                }
                else if (bmpModelingType == TreatmentBMPModelingTypeEnum.Drywell &&
                         (!bmpModelingAttributes.RoutingConfigurationID.HasValue ||
                          (bmpModelingAttributes.RoutingConfigurationID == (int)RoutingConfigurationEnum.Offline &&
                           !bmpModelingAttributes.DiversionRate.HasValue) ||
                          !bmpModelingAttributes.TotalEffectiveDrywellBMPVolume.HasValue ||
                          !bmpModelingAttributes.InfiltrationDischargeRate.HasValue))
                {
                    return new HtmlString("No");
                }
                else if ((bmpModelingType == TreatmentBMPModelingTypeEnum.HydrodynamicSeparator ||
                          bmpModelingType == TreatmentBMPModelingTypeEnum.ProprietaryBiotreatment ||
                          bmpModelingType == TreatmentBMPModelingTypeEnum.ProprietaryTreatmentControl) &&
                         !bmpModelingAttributes.TreatmentRate.HasValue)
                {
                    return new HtmlString("No");
                }
                else if (bmpModelingType == TreatmentBMPModelingTypeEnum.LowFlowDiversions &&
                         (!bmpModelingAttributes.DesignLowFlowDiversionCapacity.HasValue &&
                          !bmpModelingAttributes.AverageDivertedFlowrate.HasValue))
                {
                    return new HtmlString("No");
                }
                else if ((bmpModelingType == TreatmentBMPModelingTypeEnum.VegetatedFilterStrip ||
                          bmpModelingType == TreatmentBMPModelingTypeEnum.VegetatedSwale) &&
                         (!bmpModelingAttributes.RoutingConfigurationID.HasValue ||
                          (bmpModelingAttributes.RoutingConfigurationID == (int)RoutingConfigurationEnum.Offline &&
                           !bmpModelingAttributes.DiversionRate.HasValue) ||
                          !bmpModelingAttributes.TreatmentRate.HasValue ||
                          !bmpModelingAttributes.WettedFootprint.HasValue ||
                          !bmpModelingAttributes.EffectiveRetentionDepth.HasValue))
                {
                    return new HtmlString("No");
                }
            }
            else
            {
                return new HtmlString("No");
            }

            return new HtmlString("Yes");
        }
    }
}

