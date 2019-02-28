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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GeoJSON.Net.Feature;
using LtInfo.Common;
using LtInfo.Common.GdalOgr;
using LtInfo.Common.GeoJson;
using Neptune.Web.Common;
using Neptune.Web.Controllers;

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

        public static readonly UrlTemplate<int> DetailJurisdictionUrlTemplate = new UrlTemplate<int>(SitkaRoute<JurisdictionController>.BuildUrlFromExpression(t => t.Detail(UrlTemplate.Parameter1Int)));
        public static string GetJurisdictionSummaryUrl(this TreatmentBMP treatmentBMP)
        {
            if (treatmentBMP == null) { return ""; }
            return DetailJurisdictionUrlTemplate.ParameterReplace(treatmentBMP.StormwaterJurisdictionID);
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

        public static HtmlString GetDisplayNameAsUrl(this TreatmentBMP treatmentBMP)
        {
            return treatmentBMP == null ? new HtmlString(string.Empty) : UrlTemplate.MakeHrefString(DetailUrlTemplate.ParameterReplace(treatmentBMP.TreatmentBMPID), treatmentBMP.TreatmentBMPName);
        }

        public static FeatureCollection ToGeoJsonFeatureCollection(this IEnumerable<TreatmentBMP> treatmentBMPs)
        {
            var featureCollection = new FeatureCollection();
            featureCollection.Features.AddRange(treatmentBMPs.Select(x =>
            {
                var feature = DbGeometryToGeoJsonHelper.FromDbGeometry(x.LocationPoint);
                feature.Properties.Add("Name", x.TreatmentBMPName);
                feature.Properties.Add("FeatureColor", "#935F59");
                feature.Properties.Add("FeatureGlyph", "water"); // TODO: Need to be able to customize this per Treatment BMP Type
                feature.Properties.Add("Info", x.TreatmentBMPType.TreatmentBMPTypeName);
                feature.Properties.Add("MapSummaryUrl", x.GetMapSummaryUrl() );
                feature.Properties.Add("TreatmentBMPID",x.TreatmentBMPID);
                feature.Properties.Add("TreatmentBMPTypeID",x.TreatmentBMPTypeID);
                feature.Properties.Add("TrashCaptureStatusTypeID", x.TrashCaptureStatusTypeID);
                return feature;
            }));
            return featureCollection;
        }

        public static FeatureCollection ToGeoJsonFeatureCollectionForTrashMap(this IEnumerable<TreatmentBMP> treatmentBMPs)
        {
            var featureCollection = new FeatureCollection();
            featureCollection.Features.AddRange(treatmentBMPs.Select(x =>
            {
                var feature = DbGeometryToGeoJsonHelper.FromDbGeometry(x.LocationPoint);
                var trashCaptureStatusType = x.TrashCaptureStatusType;
                
                feature.Properties.Add("Name", x.TreatmentBMPName);
                feature.Properties.Add("FeatureColor", trashCaptureStatusType.FeatureColorOnTrashModuleMap());
                feature.Properties.Add("FeatureGlyph", "water");
                feature.Properties.Add("Info", x.TreatmentBMPType.TreatmentBMPTypeName);
                feature.Properties.Add("MapSummaryUrl", x.GetTrashMapAssetUrl() );
                feature.Properties.Add("TreatmentBMPID",x.TreatmentBMPID);
                feature.Properties.Add("TreatmentBMPTypeID",x.TreatmentBMPTypeID);
                feature.Properties.Add("TrashCaptureStatusTypeID", trashCaptureStatusType.TrashCaptureStatusTypeID);
                return feature;
            }));
            return featureCollection;
        }

        public static FeatureCollection ToGeoJsonFeatureCollectionGeneric(this IEnumerable<TreatmentBMP> treatmentBMPs)
        {
            var featureCollection = new FeatureCollection();
            featureCollection.Features.AddRange(treatmentBMPs.Select(x =>
            {
                var feature = DbGeometryToGeoJsonHelper.FromDbGeometry(x.LocationPoint);
                feature.Properties.Add("Name", x.TreatmentBMPName);
                feature.Properties.Add("FeatureColor", "#935F59");
                feature.Properties.Add("FeatureGlyph", "water"); // TODO: Need to be able to customize this per Treatment BMP Type
                feature.Properties.Add("Info", x.TreatmentBMPType.TreatmentBMPTypeName);
                feature.Properties.Add("TreatmentBMPID",x.TreatmentBMPID);
                feature.Properties.Add("TreatmentBMPTypeID", x.TreatmentBMPTypeID);
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
                var feature = DbGeometryToGeoJsonHelper.FromDbGeometry(x.LocationPoint);
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
                var feature = DbGeometryToGeoJsonHelper.FromDbGeometry(treatmentBMP.LocationPoint);
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
    }
}
