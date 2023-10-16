﻿/*-----------------------------------------------------------------------
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

using Microsoft.EntityFrameworkCore;
using Neptune.Common.DesignByContract;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;

namespace Neptune.WebMvc.Models
{
    public static class TreatmentBMPModelExtensions
    {
        //public static string GetDetailUrl(this vMostRecentTreatmentBMPAssessment vMostRecentTreatmentBMPAssessment)
        //{
        //    if (vMostRecentTreatmentBMPAssessment == null) { return ""; }
        //    return DetailUrlTemplate.ParameterReplace(vMostRecentTreatmentBMPAssessment.TreatmentBMPID);
        //}

        //public static readonly UrlTemplate<int> DetailJurisdictionUrlTemplate = new UrlTemplate<int>(SitkaRoute<JurisdictionController>.BuildUrlFromExpression(t => t.Detail(UrlTemplate.Parameter1Int)));
        //public static string GetJurisdictionSummaryUrl(this TreatmentBMP treatmentBMP)
        //{
        //    if (treatmentBMP == null) { return ""; }
        //    return DetailJurisdictionUrlTemplate.ParameterReplace(treatmentBMP.StormwaterJurisdictionID);
        //}

        //public static string GetJurisdictionSummaryUrl(this vMostRecentTreatmentBMPAssessment vMostRecentTreatmentBMPAssessment)
        //{
        //    if (vMostRecentTreatmentBMPAssessment == null) { return ""; }
        //    return DetailJurisdictionUrlTemplate.ParameterReplace(vMostRecentTreatmentBMPAssessment.StormwaterJurisdictionID);
        //}

        //public static readonly UrlTemplate<int> DetailOrganizationUrlTemplate = new UrlTemplate<int>(SitkaRoute<OrganizationController>.BuildUrlFromExpression(t => t.Detail(UrlTemplate.Parameter1Int)));
        //public static string GetOwnerOrganizationSummaryUrl(
        //    this vMostRecentTreatmentBMPAssessment vMostRecentTreatmentBMPAssessment)
        //{
        //    if (vMostRecentTreatmentBMPAssessment == null) { return ""; }

        //    return DetailOrganizationUrlTemplate.ParameterReplace(vMostRecentTreatmentBMPAssessment
        //        .OwnerOrganizationID);
        //}

        //public static readonly UrlTemplate<int> TrashMapSummaryUrlTemplate = new UrlTemplate<int>(SitkaRoute<Areas.Trash.Controllers.TreatmentBMPController>.BuildUrlFromExpression(t => t.TrashMapAssetPanel(UrlTemplate.Parameter1Int)));
        //public static string GetTrashMapAssetUrl(this TreatmentBMP treatmentBMP)
        //{
        //    return TrashMapSummaryUrlTemplate.ParameterReplace(treatmentBMP.TreatmentBMPID);
        //}

        //public static readonly UrlTemplate<int> EditBenchmarkAndThresholdsUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPBenchmarkAndThresholdController>.BuildUrlFromExpression(t => t.Instructions(UrlTemplate.Parameter1Int)));
        //public static string GetEditBenchmarkAndThresholdsUrl(this TreatmentBMP treatmentBMP)
        //{
        //    return EditBenchmarkAndThresholdsUrlTemplate.ParameterReplace(treatmentBMP.TreatmentBMPID);
        //}

        //public static readonly UrlTemplate<int> DelineationUrlTemplate = new UrlTemplate<int>(SitkaRoute<DelineationController>.BuildUrlFromExpression(t => t.ForTreatmentBMP(UrlTemplate.Parameter1Int)));
        //public static string GetDelineationUrl(this TreatmentBMP treatmentBMP)
        //{
        //    return DelineationUrlTemplate.ParameterReplace(treatmentBMP.TreatmentBMPID);
        //}

        //public static HtmlString GetDisplayNameAsUrl(this TreatmentBMP treatmentBMP)
        //{
        //    return treatmentBMP == null ? new HtmlString(String.Empty) : UrlTemplate.MakeHrefString(DetailUrlTemplate.ParameterReplace(treatmentBMP.TreatmentBMPID), treatmentBMP.TreatmentBMPName);
        //}

        public static FeatureCollection ToGeoJsonFeatureCollection(this IEnumerable<TreatmentBMP> treatmentBMPs,
            LinkGenerator linkGenerator)
        {
            var mapSummaryUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, t => t.SummaryForMap(UrlTemplate.Parameter1Int)));

            var featureCollection = new FeatureCollection();
            foreach (var treatmentBMP in treatmentBMPs)
            {
                var attributesTable = new AttributesTable
                {
                    { "Name", treatmentBMP.TreatmentBMPName },
                    { "FeatureColor", "#935F59" },
                    { "FeatureGlyph", "water" }, // TODO: Need to be able to customize this per Treatment BMP Type
                    { "Info", treatmentBMP.TreatmentBMPType.TreatmentBMPTypeName },
                    { "MapSummaryUrl", mapSummaryUrlTemplate.ParameterReplace(treatmentBMP.TreatmentBMPID) },
                    { "TreatmentBMPID", treatmentBMP.TreatmentBMPID },
                    { "TreatmentBMPTypeID", treatmentBMP.TreatmentBMPTypeID },
                    { "TrashCaptureStatusTypeID", treatmentBMP.TrashCaptureStatusTypeID },
                    { "StormwaterJurisdictionID", treatmentBMP.StormwaterJurisdictionID }
                };
                var feature = new Feature(treatmentBMP.LocationPoint4326, attributesTable);
                featureCollection.Add(feature);
            }
            return featureCollection;
        }

        public static FeatureCollection ToGeoJsonFeatureCollectionForTrashMap(this IEnumerable<TreatmentBMP> treatmentBMPs)
        {
            var featureCollection = new FeatureCollection();
            foreach (var treatmentBMP in treatmentBMPs)
            {
                var trashCaptureStatusType = treatmentBMP.TrashCaptureStatusType;
                var attributesTable = new AttributesTable
                {
                    { "Name", treatmentBMP.TreatmentBMPName },
                    { "FeatureColor", trashCaptureStatusType.FeatureColorOnTrashModuleMap() },
                    { "FeatureGlyph", "water" },
                    { "Info", treatmentBMP.TreatmentBMPType.TreatmentBMPTypeName },
                    //todo: { "MapSummaryUrl", treatmentBMP.GetTrashMapAssetUrl() },
                    { "TreatmentBMPID", treatmentBMP.TreatmentBMPID },
                    { "TreatmentBMPTypeID", treatmentBMP.TreatmentBMPTypeID },
                    { "TrashCaptureStatusTypeID", trashCaptureStatusType.TrashCaptureStatusTypeID },
                    { "TrashCaptureStatus", trashCaptureStatusType.TrashCaptureStatusTypeName },
                    { "StormwaterJurisdictionID", treatmentBMP.StormwaterJurisdictionID }
                };
                var feature = new Feature(treatmentBMP.LocationPoint4326, attributesTable);
                featureCollection.Add(feature);
            }
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
            foreach (var treatmentBMP in treatmentBMPs)
            {
                var attributesTable = new AttributesTable
                {
                    { "Name", treatmentBMP.TreatmentBMPName },
                    { "FeatureColor", "#935F59" },
                    { "FeatureGlyph", "water" }, // TODO: Need to be able to customize this per Treatment BMP Type
                    { "Info", treatmentBMP.TreatmentBMPType.TreatmentBMPTypeName },
                    { "TreatmentBMPID", treatmentBMP.TreatmentBMPID },
                    { "TreatmentBMPTypeID", treatmentBMP.TreatmentBMPTypeID },
                    { "StormwaterJurisdictionID", treatmentBMP.StormwaterJurisdiction.StormwaterJurisdictionID }
                };
                var feature = new Feature(treatmentBMP.LocationPoint4326, attributesTable);
                onEachFeature?.Invoke(feature, treatmentBMP);
                featureCollection.Add(feature);
            }
            return featureCollection;
        }

        public static FeatureCollection ToExportGeoJsonFeatureCollection(
            this IEnumerable<TreatmentBMP> treatmentBMPs)
        {
            var featureCollection = new FeatureCollection();
            foreach (var treatmentBMP in treatmentBMPs)
            {
                var attributesTable = AddAllCommonPropertiesToTreatmentBMPFeature(treatmentBMP);
                var feature = new Feature(treatmentBMP.LocationPoint4326, attributesTable);
                featureCollection.Add(feature);
            }
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
            foreach (var treatmentBMP in treatmentBMPs)
            {
                var attributesTable = AddAllCommonPropertiesToTreatmentBMPFeature(treatmentBMP);
                foreach (var ca in treatmentBMPType.TreatmentBMPTypeCustomAttributeTypes.OrderBy(x => x.SortOrder))
                {
                    attributesTable.Add(ca.CustomAttributeType.CustomAttributeTypeName.SanitizeStringForGdb(), treatmentBMP.GetCustomAttributeValueWithUnits(ca, treatmentBMP.CustomAttributes));
                }
                var feature = new Feature(treatmentBMP.LocationPoint4326, attributesTable);
                featureCollection.Add(feature);
            }
            return featureCollection;
        }

        private static AttributesTable AddAllCommonPropertiesToTreatmentBMPFeature(TreatmentBMP x)
        {
            var attributesTable = new AttributesTable
            {
                { "Name", x.TreatmentBMPName },
                { "Jurisdiction", x.StormwaterJurisdiction.GetOrganizationDisplayName() },
                { "Type", x.TreatmentBMPType.TreatmentBMPTypeName },
                { "Owner", x.OwnerOrganization.GetDisplayName() },
                { "Year_Built", x.YearBuilt },
                { "ID_in_System_of_Record", x.SystemOfRecordID },
                { "Water_Quality_Management_Plan", x.WaterQualityManagementPlan?.WaterQualityManagementPlanName },
                { "Notes", x.Notes },
                { "Last_Assessment_Date", x.GetMostRecentAssessment()?.GetAssessmentDate() },
                { "Last_Assessed_Score", x.GetMostRecentScoreAsString() },
                // todo: { "Number_of_Assessments", x.TreatmentBMPAssessments.Count },
                { "Benchmark_and_Threshold_Set", x.IsBenchmarkAndThresholdsComplete(x.TreatmentBMPType).ToYesNo() },
                { "Required_Lifespan_of_Installation", x.TreatmentBMPLifespanType?.TreatmentBMPLifespanTypeDisplayName ?? "Unknown" },
                { "Lifespan_End_Date", x.TreatmentBMPLifespanEndDate },
                { "Required_Field_Visits_Per_Year", x.RequiredFieldVisitsPerYear },
                { "Required_Post_Storm_Visits_Per_Year", x.RequiredPostStormFieldVisitsPerYear }
            };
            return attributesTable;
        }


        public static void UpdateUpstreamBMPReferencesIfNecessary(this TreatmentBMP treatmentBMP, NeptuneDbContext dbContext)
        {
            //If this BMP has an Upstream BMP, after the location change, can that Upstream BMP still fulfill its duty?
            if (treatmentBMP.UpstreamBMPID != null)
            {
                var regionalSubbasin = treatmentBMP.GetRegionalSubbasin(dbContext);
                if (regionalSubbasin != null)
                {
                    if (!regionalSubbasin.GetTreatmentBMPs(dbContext).Select(x => x.TreatmentBMPID)
                            .Contains(treatmentBMP.UpstreamBMPID.Value))
                    {
                        //Do we need to check ahead of time and warn them this will happen?
                        //Do we need to return a message indicating that this has changed?
                        treatmentBMP.UpstreamBMPID = null;
                    }
                }
                else
                {
                    treatmentBMP.UpstreamBMPID = null;
                }
            }

            //todo: need to load inverse upstream bmps
            //If this BMP is an Upstream BMP for any other BMPs, after the location change, can this BMP still fulfill its duty?
            if (treatmentBMP.InverseUpstreamBMP.Any())
            {
                treatmentBMP.InverseUpstreamBMP.ToList().ForEach(x =>
                {
                    var regionalSubbasin = x.GetRegionalSubbasin(dbContext);
                    if (regionalSubbasin != null)
                    {
                        if (!regionalSubbasin.CatchmentGeometry.Contains(treatmentBMP.LocationPoint))
                        {
                            x.UpstreamBMPID = null;
                        }
                    }
                    else
                    {
                        x.UpstreamBMPID = null;
                    }
                });
            }
        }

        public static void UpdatedCentralizedBMPDelineationIfPresent(this TreatmentBMP treatmentBMP, NeptuneDbContext dbContext, Delineation? delineation)
        {
            if (delineation is not { DelineationTypeID: (int)DelineationTypeEnum.Centralized })
            {
                return;
            }

            var updated4326Geometry =
                treatmentBMP.GetCentralizedDelineationGeometry4326(dbContext);

            if (updated4326Geometry == null || !updated4326Geometry.EqualsExact(delineation.DelineationGeometry4326))
            {
                var oldShape = delineation.DelineationGeometry;
                var newShape = treatmentBMP.GetCentralizedDelineationGeometry2771(dbContext);
                if (updated4326Geometry != null)
                {
                    delineation.DelineationGeometry = newShape;
                    delineation.DelineationGeometry4326 = updated4326Geometry;
                    delineation.IsVerified = false;
                    delineation.DateLastModified = DateTime.Now;
                }
                else
                {
                    delineation.DeleteDelineation(dbContext);
                }
            }
        }

        public static void SetTreatmentBMPPointInPolygonDataByLocationPoint(this TreatmentBMP treatmentBMP,
            Geometry locationPoint, NeptuneDbContext dbContext)
        {
            treatmentBMP.WatershedID = dbContext.Watersheds.AsNoTracking()
                .FirstOrDefault(x => locationPoint.Intersects(x.WatershedGeometry))?.WatershedID;
            treatmentBMP.ModelBasinID = dbContext.ModelBasins.AsNoTracking()
                .FirstOrDefault(x => locationPoint.Intersects(x.ModelBasinGeometry))?.ModelBasinID;
            treatmentBMP.PrecipitationZoneID = dbContext.PrecipitationZones.AsNoTracking()
                .FirstOrDefault(x => locationPoint.Intersects(x.PrecipitationZoneGeometry))?.PrecipitationZoneID;
            treatmentBMP.RegionalSubbasinID = dbContext.RegionalSubbasins.AsNoTracking()
                .FirstOrDefault(x => locationPoint.Intersects(x.CatchmentGeometry))?.RegionalSubbasinID;
        }

        public static void MarkInventoryAsProvisionalIfNonManager(this TreatmentBMP treatmentBMP, Person person)
        {
            var isAssignedToStormwaterJurisdiction = person.CanManageStormwaterJurisdiction(treatmentBMP.StormwaterJurisdictionID);
            if (!isAssignedToStormwaterJurisdiction)
            {
                treatmentBMP.InventoryIsVerified = false;
            }
            treatmentBMP.InventoryLastChangedDate = DateTime.Now;
        }

        public static IEnumerable<HRUCharacteristic> GetHRUCharacteristics(this TreatmentBMP treatmentBMP, NeptuneDbContext dbContext, Delineation? delineation)
        {
            if (delineation == null)
            {
                return new List<HRUCharacteristic>();
            }

            if (delineation.DelineationType == DelineationType.Centralized && !treatmentBMP.TreatmentBMPType.TreatmentBMPModelingTypeID.HasValue)
            {
                if (!treatmentBMP.RegionalSubbasinID.HasValue)
                {
                    return Enumerable.Empty<HRUCharacteristic>();
                }
                var catchmentRegionalSubbasins = vRegionalSubbasinUpstreams.ListUpstreamRegionalBasinIDs(dbContext, treatmentBMP.RegionalSubbasinID.Value);
                    
                return dbContext.HRUCharacteristics.Include(x => x.LoadGeneratingUnit).AsNoTracking().Where(x =>
                    x.LoadGeneratingUnit.RegionalSubbasinID != null &&
                    catchmentRegionalSubbasins.Contains(x.LoadGeneratingUnit.RegionalSubbasinID.Value));
            }

            return dbContext.HRUCharacteristics
                .Include(x => x.LoadGeneratingUnit)
                .ThenInclude(x => x.Delineation)
                .AsNoTracking()
                .Where(x =>
                    x.LoadGeneratingUnit.Delineation != null && x.LoadGeneratingUnit.Delineation.TreatmentBMPID ==
                    treatmentBMP.TreatmentBMPID);
        }

        public static TreatmentBMPTypeAssessmentObservationType GetTreatmentBMPTypeObservationType(this TreatmentBMPType treatmentBMPType, TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType)
        {
            var treatmentBMPTypeAssessmentObservationType = treatmentBMPType.GetTreatmentBMPTypeObservationTypeOrDefault(treatmentBMPAssessmentObservationType);
            Check.Require(treatmentBMPTypeAssessmentObservationType != null,
                $"The Observation Type '{treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeName}' is not applicable to the Treatment BMP Type '{treatmentBMPType.TreatmentBMPTypeName}'.");
            return treatmentBMPTypeAssessmentObservationType;
        }

        public static TreatmentBMPTypeAssessmentObservationType? GetTreatmentBMPTypeObservationTypeOrDefault(this TreatmentBMPType treatmentBMPType, TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType)
        {
            var treatmentBMPTypeAssessmentObservationType = treatmentBMPType.TreatmentBMPTypeAssessmentObservationTypes.SingleOrDefault(
                x => x.TreatmentBMPTypeID == treatmentBMPType.TreatmentBMPTypeID && x.TreatmentBMPAssessmentObservationTypeID == treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID);

            return treatmentBMPTypeAssessmentObservationType;
        }
    }
}
