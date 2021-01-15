/*-----------------------------------------------------------------------
<copyright file="treatmentBMPModelExtensions.cs" company="Tahoe Regional Planning Agency">
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

using GeoJSON.Net.Feature;
using LtInfo.Common;
using LtInfo.Common.DbSpatial;
using LtInfo.Common.GdalOgr;
using LtInfo.Common.GeoJson;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;

namespace Neptune.Web.Models
{
    public static class TreatmentBMPModelExtensions
    {
        public static readonly UrlTemplate<int> DetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPController>.BuildAbsoluteUrlHttpsFromExpression(t => t.Detail(UrlTemplate.Parameter1Int), NeptuneWebConfiguration.CanonicalHostNameRoot));
        public static string GetDetailUrl(this TreatmentBMP treatmentBMP)
        {
            if (treatmentBMP == null) { return ""; }
            return DetailUrlTemplate.ParameterReplace(treatmentBMP.TreatmentBMPID);
        }
        public static string GetDetailUrl(this vMostRecentTreatmentBMPAssessment vMostRecentTreatmentBMPAssessment)
        {
            if (vMostRecentTreatmentBMPAssessment == null) { return ""; }
            return DetailUrlTemplate.ParameterReplace(vMostRecentTreatmentBMPAssessment.TreatmentBMPID);
        }

        public static readonly UrlTemplate<int> DetailJurisdictionUrlTemplate = new UrlTemplate<int>(SitkaRoute<JurisdictionController>.BuildUrlFromExpression(t => t.Detail(UrlTemplate.Parameter1Int)));
        public static string GetJurisdictionSummaryUrl(this TreatmentBMP treatmentBMP)
        {
            if (treatmentBMP == null) { return ""; }
            return DetailJurisdictionUrlTemplate.ParameterReplace(treatmentBMP.StormwaterJurisdictionID);
        }

        public static string GetJurisdictionSummaryUrl(this vMostRecentTreatmentBMPAssessment vMostRecentTreatmentBMPAssessment)
        {
            if (vMostRecentTreatmentBMPAssessment == null) { return ""; }
            return DetailJurisdictionUrlTemplate.ParameterReplace(vMostRecentTreatmentBMPAssessment.StormwaterJurisdictionID);
        }

        public static readonly UrlTemplate<int> DetailOrganizationUrlTemplate = new UrlTemplate<int>(SitkaRoute<OrganizationController>.BuildUrlFromExpression(t => t.Detail(UrlTemplate.Parameter1Int)));
        public static string GetOwnerOrganizationSummaryUrl(
            this vMostRecentTreatmentBMPAssessment vMostRecentTreatmentBMPAssessment)
        {
            if (vMostRecentTreatmentBMPAssessment == null) { return ""; }

            return DetailOrganizationUrlTemplate.ParameterReplace(vMostRecentTreatmentBMPAssessment
                .OwnerOrganizationID);
        }

        public static readonly UrlTemplate<int> EditUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(t => t.Edit(UrlTemplate.Parameter1Int)));
        public static string GetEditUrl(this TreatmentBMP treatmentBMP)
        {
            return EditUrlTemplate.ParameterReplace(treatmentBMP.TreatmentBMPID);
        }

        public static readonly UrlTemplate<int> DeleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(t => t.Delete(UrlTemplate.Parameter1Int)));
        public static string GetDeleteUrl(this TreatmentBMP treatmentBMP)
        {
            return DeleteUrlTemplate.ParameterReplace(treatmentBMP.TreatmentBMPID);
        }

        public static readonly UrlTemplate<int> MapSummaryUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(t => t.SummaryForMap(UrlTemplate.Parameter1Int)));
        public static string GetMapSummaryUrl(this TreatmentBMP treatmentBMP)
        {
            return MapSummaryUrlTemplate.ParameterReplace(treatmentBMP.TreatmentBMPID);
        }

        public static readonly UrlTemplate<int> TrashMapSummaryUrlTemplate = new UrlTemplate<int>(SitkaRoute<Areas.Trash.Controllers.TreatmentBMPController>.BuildUrlFromExpression(t => t.TrashMapAssetPanel(UrlTemplate.Parameter1Int)));
        public static string GetTrashMapAssetUrl(this TreatmentBMP treatmentBMP)
        {
            return TrashMapSummaryUrlTemplate.ParameterReplace(treatmentBMP.TreatmentBMPID);
        }

        public static readonly UrlTemplate<int> EditBenchmarkAndThresholdsUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPBenchmarkAndThresholdController>.BuildUrlFromExpression(t => t.Instructions(UrlTemplate.Parameter1Int)));
        public static string GetEditBenchmarkAndThresholdsUrl(this TreatmentBMP treatmentBMP)
        {
            return EditBenchmarkAndThresholdsUrlTemplate.ParameterReplace(treatmentBMP.TreatmentBMPID);
        }

        public static readonly UrlTemplate<int> DelineationUrlTemplate = new UrlTemplate<int>(SitkaRoute<DelineationController>.BuildUrlFromExpression(t => t.ForTreatmentBMP(UrlTemplate.Parameter1Int)));
        public static string GetDelineationUrl(this TreatmentBMP treatmentBMP)
        {
            return DelineationUrlTemplate.ParameterReplace(treatmentBMP.TreatmentBMPID);
        }

        public static readonly UrlTemplate<int> DelineationMapUrlTemplate = new UrlTemplate<int>(SitkaRoute<DelineationController>.BuildUrlFromExpression(t => t.DelineationMap(UrlTemplate.Parameter1Int)));
        public static string GetDelineationMapUrl(this TreatmentBMP treatmentBMP)
        {
            return DelineationMapUrlTemplate.ParameterReplace(treatmentBMP.TreatmentBMPID);
        }

        public static HtmlString GetDisplayNameAsUrl(this TreatmentBMP treatmentBMP)
        {
            return treatmentBMP == null ? new HtmlString(String.Empty) : UrlTemplate.MakeHrefString(DetailUrlTemplate.ParameterReplace(treatmentBMP.TreatmentBMPID), treatmentBMP.TreatmentBMPName);
        }

        public static FeatureCollection ToGeoJsonFeatureCollection(this IEnumerable<TreatmentBMP> treatmentBMPs)
        {
            var featureCollection = new FeatureCollection();
            featureCollection.Features.AddRange(treatmentBMPs.Select(x =>
            {
                var feature = DbGeometryToGeoJsonHelper.FromDbGeometryWithNoReproject(x.LocationPoint4326);
                feature.Properties.Add("Name", x.TreatmentBMPName);
                feature.Properties.Add("FeatureColor", "#935F59");
                feature.Properties.Add("FeatureGlyph", "water"); // TODO: Need to be able to customize this per Treatment BMP Type
                feature.Properties.Add("Info", x.TreatmentBMPType.TreatmentBMPTypeName);
                feature.Properties.Add("MapSummaryUrl", x.GetMapSummaryUrl() );
                feature.Properties.Add("TreatmentBMPID",x.TreatmentBMPID);
                feature.Properties.Add("TreatmentBMPTypeID",x.TreatmentBMPTypeID);
                feature.Properties.Add("TrashCaptureStatusTypeID", x.TrashCaptureStatusTypeID);
                feature.Properties.Add("StormwaterJurisdictionID", x.StormwaterJurisdiction.StormwaterJurisdictionID);
                return feature;
            }));
            return featureCollection;
        }

        public static FeatureCollection ToGeoJsonFeatureCollectionForTrashMap(this IEnumerable<TreatmentBMP> treatmentBMPs)
        {
            var featureCollection = new FeatureCollection();
            featureCollection.Features.AddRange(treatmentBMPs.Select(x =>
            {
                var feature = DbGeometryToGeoJsonHelper.FromDbGeometryWithNoReproject(x.LocationPoint4326);
                var trashCaptureStatusType = x.TrashCaptureStatusType;
                
                feature.Properties.Add("Name", x.TreatmentBMPName);
                feature.Properties.Add("FeatureColor", trashCaptureStatusType.FeatureColorOnTrashModuleMap());
                feature.Properties.Add("FeatureGlyph", "water");
                feature.Properties.Add("Info", x.TreatmentBMPType.TreatmentBMPTypeName);
                feature.Properties.Add("MapSummaryUrl", x.GetTrashMapAssetUrl() );
                feature.Properties.Add("TreatmentBMPID",x.TreatmentBMPID);
                feature.Properties.Add("TreatmentBMPTypeID",x.TreatmentBMPTypeID);
                feature.Properties.Add("TrashCaptureStatusTypeID", trashCaptureStatusType.TrashCaptureStatusTypeID);
                feature.Properties.Add("TrashCaptureStatus", trashCaptureStatusType.TrashCaptureStatusTypeName);
                feature.Properties.Add("StormwaterJurisdictionID", x.StormwaterJurisdiction.StormwaterJurisdictionID);
                return feature;
            }));
            return featureCollection;
        }

        /// <summary>
        /// Creates a GeoJson FeatureCollection from an enumerable of TreatmentBMPs.
        /// Adds some common properties (Name, Treatment BMP Type, ID, Type ID) to each feature.
        /// </summary>
        /// <param name="treatmentBMPs"></param>
        /// <returns></returns>
        public static FeatureCollection ToGeoJsonFeatureCollectionGeneric(this IEnumerable<TreatmentBMP> treatmentBMPs)
        {
            return treatmentBMPs.ToGeoJsonFeatureCollectionGeneric(null);
        }

        /// <summary>
        /// Creates a GeoJson FeatureCollection from an enumerable of TreatmentBMPs.
        /// The onEachFeature Action parameter allows the caller to add additional properties to the features.
        /// </summary>
        /// <param name="treatmentBMPs"></param>
        /// <param name="onEachFeature"></param>
        /// <returns></returns>
        public static FeatureCollection ToGeoJsonFeatureCollectionGeneric(this IEnumerable<TreatmentBMP> treatmentBMPs, Action<Feature, TreatmentBMP> onEachFeature)
        {
            var featureCollection = new FeatureCollection();
            featureCollection.Features.AddRange(treatmentBMPs.Select(treatmentBMP =>
            {
                var feature = DbGeometryToGeoJsonHelper.FromDbGeometryWithNoReproject(treatmentBMP.LocationPoint4326);
                feature.Properties.Add("Name", treatmentBMP.TreatmentBMPName);
                feature.Properties.Add("FeatureColor", "#935F59");
                feature.Properties.Add("FeatureGlyph", "water"); // TODO: Need to be able to customize this per Treatment BMP Type
                feature.Properties.Add("Info", treatmentBMP.TreatmentBMPType.TreatmentBMPTypeName);
                feature.Properties.Add("TreatmentBMPID",treatmentBMP.TreatmentBMPID);
                feature.Properties.Add("TreatmentBMPTypeID", treatmentBMP.TreatmentBMPTypeID);
                feature.Properties.Add("StormwaterJurisdictionID", treatmentBMP.StormwaterJurisdiction.StormwaterJurisdictionID);
                onEachFeature?.Invoke(feature, treatmentBMP);
                return feature;
            }));
            return featureCollection;
        }

        public static FeatureCollection ToExportGeoJsonFeatureCollection(
            this IEnumerable<TreatmentBMP> treatmentBMPs)
        {
            var featureCollection = new FeatureCollection();
            featureCollection.Features.AddRange(treatmentBMPs.Select(x =>
            {
                var feature = DbGeometryToGeoJsonHelper.FromDbGeometryWithNoReproject(x.LocationPoint4326);
                AddAllCommonPropertiesToTreatmentBMPFeature(feature, x);
                return feature;
            }));
            return featureCollection;
        }

        /// <summary>
        /// Overload taking a TreatmentBMPType so it can access the Custom Attributes
        /// </summary>
        /// <param name="treatmentBMPs"></param>
        /// <param name="treatmentBMPType"></param>
        /// <returns></returns>
        public static FeatureCollection ToExportGeoJsonFeatureCollection(this IEnumerable<TreatmentBMP> treatmentBMPs, TreatmentBMPType treatmentBMPType)
        {
            var featureCollection = new FeatureCollection();
            featureCollection.Features.AddRange(treatmentBMPs.Select(treatmentBMP =>
            {
                var feature = DbGeometryToGeoJsonHelper.FromDbGeometryWithNoReproject(treatmentBMP.LocationPoint4326);
                AddAllCommonPropertiesToTreatmentBMPFeature(feature, treatmentBMP);
                foreach (var ca in treatmentBMPType.TreatmentBMPTypeCustomAttributeTypes.OrderBy(x => x.SortOrder))
                {
                    feature.Properties.Add(Ogr2OgrCommandLineRunner.SanitizeStringForGdb(ca.CustomAttributeType.CustomAttributeTypeName), treatmentBMP.GetCustomAttributeValueWithUnits(ca));
                }
                return feature;
            }));
            return featureCollection;
        }

        private static void AddAllCommonPropertiesToTreatmentBMPFeature(Feature feature, TreatmentBMP x)
        {
            feature.Properties.Add("Name", x.TreatmentBMPName);
            feature.Properties.Add("Jurisdiction", x.StormwaterJurisdiction.GetOrganizationDisplayName());
            feature.Properties.Add("Type", x.TreatmentBMPType.TreatmentBMPTypeName);
            feature.Properties.Add("Owner", x.OwnerOrganization.GetDisplayName());
            feature.Properties.Add("Year_Built", x.YearBuilt);
            feature.Properties.Add("ID_in_System_of_Record", x.SystemOfRecordID);
            feature.Properties.Add("Water_Quality_Management_Plan", x.WaterQualityManagementPlan?.WaterQualityManagementPlanName);
            feature.Properties.Add("Notes", x.Notes);
            feature.Properties.Add("Last_Assessment_Date", x.GetMostRecentAssessment()?.GetAssessmentDate());
            feature.Properties.Add("Last_Assessed_Score", x.GetMostRecentScoreAsString());
            feature.Properties.Add("Number_of_Assessments", x.TreatmentBMPAssessments.Count);
            feature.Properties.Add("Benchmark_and_Threshold_Set", x.IsBenchmarkAndThresholdsComplete().ToYesNo());
            feature.Properties.Add("Required_Lifespan_of_Installation",
                x.TreatmentBMPLifespanType?.TreatmentBMPLifespanTypeDisplayName ?? "Unknown");
            feature.Properties.Add("Lifespan_End_Date", x.TreatmentBMPLifespanEndDate);
            feature.Properties.Add("Required_Field_Visits_Per_Year",
                x.RequiredFieldVisitsPerYear);
            feature.Properties.Add("Required_Post_Storm_Visits_Per_Year",
                x.RequiredPostStormFieldVisitsPerYear);
        }

        public static string GetDelineationAreaString(this TreatmentBMP treatmentBMP)
        {
            return (treatmentBMP.Delineation?.DelineationGeometry.Area * DbSpatialHelper.SquareMetersToAcres)?.ToString("0.00") ?? "-";
        }

        public static string GetDelineationStatus(this TreatmentBMP treatmentBMP)
        {
            return treatmentBMP.Delineation != null ? treatmentBMP.Delineation.IsVerified ? "Verified" : "Provisional" : "None";
        }

        /// <summary>
        /// Performs the RSB trace for a given Treatment BMP using the EPSG 4326 representation of the regional subbasin geometries
        /// </summary>
        /// <param name="treatmentBMP"></param>
        /// <param name="dbContext"></param>
        /// <returns></returns>
        public static DbGeometry GetCentralizedDelineationGeometry4326(this TreatmentBMP treatmentBMP,
            DatabaseEntities dbContext)
        {
            var regionalSubbasin =
                dbContext.RegionalSubbasins.SingleOrDefault(x =>
                    x.CatchmentGeometry.Contains(treatmentBMP.LocationPoint));

            var regionalSubbasinIDs = regionalSubbasin.TraceUpstreamCatchmentsReturnIDList(dbContext);

            regionalSubbasinIDs.Add(regionalSubbasin.RegionalSubbasinID);

            var unionOfUpstreamRegionalSubbasins = dbContext.RegionalSubbasins
                .Where(x => regionalSubbasinIDs.Contains(x.RegionalSubbasinID)).Select(x => x.CatchmentGeometry4326)
                .ToList().UnionListGeometries();

            // Remove interior slivers introduced in the case that the non-cascading union strategy is used (see UnionListGeometries for more info)
            var dbGeometry = unionOfUpstreamRegionalSubbasins.Buffer(0);
            return dbGeometry;
        }

        /// <summary>
        /// Performs the RSB trace for a given Treatment BMP using the EPSG 2771 representation of the regional subbasin geometries
        /// </summary>
        /// <param name="treatmentBMP"></param>
        /// <param name="dbContext"></param>
        /// <returns></returns>
        public static DbGeometry GetCentralizedDelineationGeometry2771(this TreatmentBMP treatmentBMP,
            DatabaseEntities dbContext)
        {
            var regionalSubbasin =
                dbContext.RegionalSubbasins.SingleOrDefault(x =>
                    x.CatchmentGeometry.Contains(treatmentBMP.LocationPoint));

            var regionalSubbasinIDs = regionalSubbasin.TraceUpstreamCatchmentsReturnIDList(dbContext);

            regionalSubbasinIDs.Add(regionalSubbasin.RegionalSubbasinID);

            var unionOfUpstreamRegionalSubbasins = dbContext.RegionalSubbasins
                .Where(x => regionalSubbasinIDs.Contains(x.RegionalSubbasinID)).Select(x => x.CatchmentGeometry)
                .ToList().UnionListGeometries();

            // Remove interior slivers introduced in the case that the non-cascading union strategy is used (see UnionListGeometries for more info)
            var dbGeometry = unionOfUpstreamRegionalSubbasins.Buffer(0);
            return dbGeometry;
        }

        public static RegionalSubbasin GetRegionalSubbasin(this TreatmentBMP treatmentBMP)
        {
            return HttpRequestStorage.DatabaseEntities.RegionalSubbasins.SingleOrDefault(x =>
                    x.CatchmentGeometry.Contains(treatmentBMP.LocationPoint));
        }


        public static void UpdateUpstreamBMPReferencesIfNecessary(this TreatmentBMP treatmentBMP)
        {
            //If this BMP has an Upstream BMP, after the location change, can that Upstream BMP still fulfill its duty?
            if (treatmentBMP.UpstreamBMPID != null && !treatmentBMP.GetRegionalSubbasin().GetTreatmentBMPs().Contains(treatmentBMP.UpstreamBMP))
            {
                //Do we need to check ahead of time and warn them this will happen?
                //Do we need to return a message indicating that this has changed?
                treatmentBMP.UpstreamBMPID = null;
            }

            //If this BMP is an Upstream BMP for any other BMPs, after the location change, can this BMP still fulfill its duty?
            if (treatmentBMP.TreatmentBMPsWhereYouAreTheUpstreamBMP.Any())
            {
                treatmentBMP.TreatmentBMPsWhereYouAreTheUpstreamBMP.ToList().ForEach(x =>
                {
                    if (!x.GetRegionalSubbasin().CatchmentGeometry.Contains(treatmentBMP.LocationPoint))
                    {
                        x.UpstreamBMPID = null;
                    }
                });
            }
        }

        public static void UpdatedCentralizedBMPDelineationIfPresent(this TreatmentBMP treatmentBMP)
        {
            if (treatmentBMP.Delineation == null || treatmentBMP.Delineation.DelineationTypeID != (int) DelineationTypeEnum.Centralized)
            {
                return;
            }

            var updated4326Geometry =
                treatmentBMP.GetCentralizedDelineationGeometry4326(HttpRequestStorage.DatabaseEntities);

            if (updated4326Geometry == null || !updated4326Geometry.SpatialEquals(treatmentBMP.Delineation.DelineationGeometry4326))
            {
                var oldShape = treatmentBMP.Delineation.DelineationGeometry;
                var newShape = treatmentBMP.GetCentralizedDelineationGeometry2771(HttpRequestStorage.DatabaseEntities);
                if (updated4326Geometry != null)
                {
                    treatmentBMP.Delineation.DelineationGeometry = newShape;
                    treatmentBMP.Delineation.DelineationGeometry4326 = updated4326Geometry;
                    treatmentBMP.Delineation.IsVerified = false;
                    treatmentBMP.Delineation.DateLastModified = DateTime.Now;
                }
                else
                {
                    treatmentBMP.Delineation.DeleteDelineation(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static bool HasVerifiedDelineationForModelingPurposes(this TreatmentBMP treatmentBMP)
        {
            if (treatmentBMP.UpstreamBMP != null)
            {
                return treatmentBMP.UpstreamBMP.HasVerifiedDelineationForModelingPurposes();
            }

            return treatmentBMP.Delineation?.IsVerified ?? false;
        }

        public static bool IsFullyParameterized(this TreatmentBMP treatmentBMP)
        {
            if (!treatmentBMP.HasVerifiedDelineationForModelingPurposes())
            {
                return false;
            }

            if (treatmentBMP.TreatmentBMPType.TreatmentBMPModelingType == null)
            {
                return false;
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
                    return false;
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
                    return false;
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
                    return false;
                }
                else if (bmpModelingType == TreatmentBMPModelingTypeEnum.CisternsForHarvestAndUse &&
                         (!bmpModelingAttributes.RoutingConfigurationID.HasValue ||
                          (bmpModelingAttributes.RoutingConfigurationID == (int)RoutingConfigurationEnum.Offline &&
                           !bmpModelingAttributes.DiversionRate.HasValue) ||
                          !bmpModelingAttributes.TotalEffectiveBMPVolume.HasValue ||
                          !bmpModelingAttributes.WinterHarvestedWaterDemand.HasValue ||
                          !bmpModelingAttributes.SummerHarvestedWaterDemand.HasValue))
                {
                    return false;
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
                    return false;
                }
                else if ((bmpModelingType == TreatmentBMPModelingTypeEnum.DryExtendedDetentionBasin ||
                          bmpModelingType == TreatmentBMPModelingTypeEnum.FlowDurationControlBasin ||
                          bmpModelingType == TreatmentBMPModelingTypeEnum.FlowDurationControlTank) &&
                         (!bmpModelingAttributes.RoutingConfigurationID.HasValue ||
                          (bmpModelingAttributes.RoutingConfigurationID == (int)RoutingConfigurationEnum.Offline &&
                           !bmpModelingAttributes.DiversionRate.HasValue) ||
                          !bmpModelingAttributes.DrawdownTimeforWQDetentionVolume.HasValue ||
                          !bmpModelingAttributes.StorageVolumeBelowLowestOutletElevation.HasValue ||
                          !bmpModelingAttributes.EffectiveFootprint.HasValue))
                {
                    return false;
                }
                else if (bmpModelingType == TreatmentBMPModelingTypeEnum.DryWeatherTreatmentSystems &&
                         (!bmpModelingAttributes.DesignDryWeatherTreatmentCapacity.HasValue &&
                          !bmpModelingAttributes.AverageTreatmentFlowrate.HasValue))
                {
                    return false;
                }
                else if (bmpModelingType == TreatmentBMPModelingTypeEnum.Drywell &&
                         (!bmpModelingAttributes.RoutingConfigurationID.HasValue ||
                          (bmpModelingAttributes.RoutingConfigurationID == (int)RoutingConfigurationEnum.Offline &&
                           !bmpModelingAttributes.DiversionRate.HasValue) ||
                          !bmpModelingAttributes.TotalEffectiveDrywellBMPVolume.HasValue ||
                          !bmpModelingAttributes.InfiltrationDischargeRate.HasValue))
                {
                    return false;
                }
                else if ((bmpModelingType == TreatmentBMPModelingTypeEnum.HydrodynamicSeparator ||
                          bmpModelingType == TreatmentBMPModelingTypeEnum.ProprietaryBiotreatment ||
                          bmpModelingType == TreatmentBMPModelingTypeEnum.ProprietaryTreatmentControl) &&
                         !bmpModelingAttributes.TreatmentRate.HasValue)
                {
                    return false;
                }
                else if (bmpModelingType == TreatmentBMPModelingTypeEnum.LowFlowDiversions &&
                         (!bmpModelingAttributes.DesignLowFlowDiversionCapacity.HasValue &&
                          !bmpModelingAttributes.AverageDivertedFlowrate.HasValue))
                {
                    return false;
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
                    return false;
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        public static void SetTreatmentBMPPointInPolygonDataByLocationPoint(this TreatmentBMP treatmentBMP,
            DbGeometry locationPoint)
        {
            treatmentBMP.WatershedID = HttpRequestStorage.DatabaseEntities.Watersheds
                .FirstOrDefault(x => locationPoint.Intersects(x.WatershedGeometry))?.WatershedID;
            treatmentBMP.LSPCBasinID = HttpRequestStorage.DatabaseEntities.LSPCBasins
                .FirstOrDefault(x => locationPoint.Intersects(x.LSPCBasinGeometry))?.LSPCBasinID;
            treatmentBMP.PrecipitationZoneID = HttpRequestStorage.DatabaseEntities.PrecipitationZones
                .FirstOrDefault(x => locationPoint.Intersects(x.PrecipitationZoneGeometry))?.PrecipitationZoneID;
            treatmentBMP.RegionalSubbasinID = HttpRequestStorage.DatabaseEntities.RegionalSubbasins
                .FirstOrDefault(x => locationPoint.Intersects(x.CatchmentGeometry))?.RegionalSubbasinID;
        }
    }
}
