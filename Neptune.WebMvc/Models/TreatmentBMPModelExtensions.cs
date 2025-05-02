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

using Neptune.Common.DesignByContract;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;
using NetTopologySuite.Features;

namespace Neptune.WebMvc.Models
{
    public static class TreatmentBMPModelExtensions
    {
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

        public static FeatureCollection ToGeoJsonFeatureCollectionForTrashMap(this IEnumerable<TreatmentBMP> treatmentBMPs, LinkGenerator linkGenerator)
        {
            UrlTemplate<int> trashMapAssetUrlTemplate = new(SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.TrashMapAssetPanel(UrlTemplate.Parameter1Int)));
            var featureCollection = new FeatureCollection();
            foreach (var treatmentBMP in treatmentBMPs)
            {
                var attributesTable = new AttributesTable
                {
                    { "Name", treatmentBMP.TreatmentBMPName },
                    { "FeatureColor", treatmentBMP.TrashCaptureStatusType.FeatureColorOnTrashModuleMap() },
                    { "MapSummaryUrl", trashMapAssetUrlTemplate.ParameterReplace(treatmentBMP.TreatmentBMPID) },
                    { "TreatmentBMPID", treatmentBMP.TreatmentBMPID },
                    { "TrashCaptureStatusTypeID", treatmentBMP.TrashCaptureStatusTypeID },
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
                    { "Info", treatmentBMP.TreatmentBMPType.TreatmentBMPTypeName },
                    { "TreatmentBMPID", treatmentBMP.TreatmentBMPID },
                    { "TreatmentBMPTypeID", treatmentBMP.TreatmentBMPTypeID },
                    { "StormwaterJurisdictionID", treatmentBMP.StormwaterJurisdictionID }
                };
                var feature = new Feature(treatmentBMP.LocationPoint4326, attributesTable);
                onEachFeature?.Invoke(feature, treatmentBMP);
                featureCollection.Add(feature);
            }
            return featureCollection;
        }

        public static FeatureCollection ToExportGeoJsonFeatureCollection(
            this IEnumerable<vTreatmentBMPGdbExport> treatmentBMPs)
        {
            var featureCollection = new FeatureCollection();
            foreach (var treatmentBMP in treatmentBMPs)
            {
                var attributesTable = AddAllCommonPropertiesToTreatmentBMPFeature(treatmentBMP);
                var feature = new Feature(treatmentBMP.LocationPoint, attributesTable);
                featureCollection.Add(feature);
            }
            return featureCollection;
        }

        /// <summary>
        /// Overload taking a TreatmentBMPType so it can access the Custom Attributes
        /// </summary>
        /// <param name="treatmentBMPs"></param>
        /// <param name="treatmentBMPTypeCustomAttributeTypes"></param>
        /// <param name="customAttributes"></param>
        /// <returns></returns>
        public static FeatureCollection ToExportGeoJsonFeatureCollection(this IEnumerable<vTreatmentBMPGdbExport> treatmentBMPs, ICollection<TreatmentBMPTypeCustomAttributeType> treatmentBMPTypeCustomAttributeTypes, ILookup<int, CustomAttribute> customAttributes)
        {
            var featureCollection = new FeatureCollection();
            foreach (var treatmentBMP in treatmentBMPs)
            {
                var attributesTable = AddAllCommonPropertiesToTreatmentBMPFeature(treatmentBMP);
                var attributes = customAttributes[treatmentBMP.TreatmentBMPID].ToList();
                foreach (var treatmentBMPTypeCustomAttributeType in treatmentBMPTypeCustomAttributeTypes.OrderBy(x => x.SortOrder))
                {
                    attributesTable.Add(treatmentBMPTypeCustomAttributeType.CustomAttributeType.CustomAttributeTypeName.SanitizeStringForGdb(), TreatmentBMP.GetCustomAttributeValueWithUnits(treatmentBMPTypeCustomAttributeType, attributes));
                }
                var feature = new Feature(treatmentBMP.LocationPoint, attributesTable);
                featureCollection.Add(feature);
            }
            return featureCollection;
        }

        private static AttributesTable AddAllCommonPropertiesToTreatmentBMPFeature(vTreatmentBMPGdbExport x)
        {
            var attributesTable = new AttributesTable
            {
                { "Name", x.TreatmentBMPName },
                { "Jurisdiction", x.OrganizationName },
                { "Type", x.TreatmentBMPTypeName },
                { "Owner", x.OwnerOrganizationName },
                { "Year_Built", x.YearBuilt },
                { "ID_in_System_of_Record", x.SystemOfRecordID },
                { "Water_Quality_Management_Plan", x.WaterQualityManagementPlanName },
                { "Trash_Capture_Effectiveness", x.TrashCaptureEffectiveness },
                { "Notes", x.Notes },
                { "Last_Assessment_Date", x.LatestAssessmentDate },
                { "Last_Assessed_Score", x.LatestAssessmentScore },
                // todo: { "Number_of_Assessments", x.TreatmentBMPAssessments.Count },
                { "Benchmark_and_Threshold_Set", (x.NumberOfBenchmarkAndThresholds > 0).ToYesNo() },
                { "Required_Lifespan_of_Installation", x.TreatmentBMPLifespanTypeDisplayName ?? "Unknown" },
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
                    delineation.DateLastModified = DateTime.UtcNow;
                }
                else
                {
                    delineation.DeleteFull(dbContext);
                }
            }
        }

        public static void MarkInventoryAsProvisionalIfNonManager(this TreatmentBMP treatmentBMP, Person person)
        {
            var isAssignedToStormwaterJurisdiction = person.CanManageStormwaterJurisdiction(treatmentBMP.StormwaterJurisdictionID);
            if (!isAssignedToStormwaterJurisdiction)
            {
                treatmentBMP.InventoryIsVerified = false;
            }
            treatmentBMP.InventoryLastChangedDate = DateTime.UtcNow;
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
