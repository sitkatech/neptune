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

using System.Collections.Generic;
using System.Linq;
using System.Web;
using LtInfo.Common;
using LtInfo.Common.GeoJson;
using Neptune.Web.Common;
using Neptune.Web.Controllers;

namespace Neptune.Web.Models
{
   public static class TreatmentBMPModelExtensions
    {
        public static readonly UrlTemplate<int> DetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(t => t.Detail(UrlTemplate.Parameter1Int)));
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

        public static readonly UrlTemplate<int> EditBenchmarkAndThresholdsUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPBenchmarkAndThresholdController>.BuildUrlFromExpression(t => t.Instructions(UrlTemplate.Parameter1Int)));
        public static string GetEditBenchmarkAndThresholdsUrl(this TreatmentBMP treatmentBMP)
        {
            return EditBenchmarkAndThresholdsUrlTemplate.ParameterReplace(treatmentBMP.TreatmentBMPID);
        }

        public static HtmlString GetDisplayNameAsUrl(this TreatmentBMP treatmentBMP)
        {
            return treatmentBMP == null ? new HtmlString(string.Empty) : UrlTemplate.MakeHrefString(DetailUrlTemplate.ParameterReplace(treatmentBMP.TreatmentBMPID), treatmentBMP.TreatmentBMPName);
        }

        public static GeoJSON.Net.Feature.FeatureCollection ToGeoJsonFeatureCollection(this IEnumerable<TreatmentBMP> treatmentBMPs)
        {
            var featureCollection = new GeoJSON.Net.Feature.FeatureCollection();
            featureCollection.Features.AddRange(treatmentBMPs.Select(x =>
            {
                var feature = DbGeometryToGeoJsonHelper.FromDbGeometry(x.LocationPoint);
                feature.Properties.Add("Name", x.TreatmentBMPName);
                feature.Properties.Add("FeatureColor", "#935F59");
                feature.Properties.Add("FeatureGlyph", "water"); // TODO: Need to be able to customize this per Treatment BMP Type
                feature.Properties.Add("Info", x.TreatmentBMPType.TreatmentBMPTypeName);
                feature.Properties.Add("MapSummaryUrl", x.GetMapSummaryUrl() );
                return feature;
            }));
            return featureCollection;
        }

        public static GeoJSON.Net.Feature.FeatureCollection ToGeoJsonFeatureCollectionGeneric(this IEnumerable<TreatmentBMP> treatmentBMPs)
        {
            var featureCollection = new GeoJSON.Net.Feature.FeatureCollection();
            featureCollection.Features.AddRange(treatmentBMPs.Select(x =>
            {
                var feature = DbGeometryToGeoJsonHelper.FromDbGeometry(x.LocationPoint);
                feature.Properties.Add("FeatureColor", "#935F59");
                feature.Properties.Add("FeatureGlyph", "water"); // TODO: Need to be able to customize this per Treatment BMP Type
                feature.Properties.Add("Info", x.TreatmentBMPType.TreatmentBMPTypeName);
                return feature;
            }));
            return featureCollection;
        }

    }
}
