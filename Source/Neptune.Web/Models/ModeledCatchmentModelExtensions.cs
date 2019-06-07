/*-----------------------------------------------------------------------
<copyright file="ModeledCatchmentModelExtensions.cs" company="Tahoe Regional Planning Agency">
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
using LtInfo.Common;
using LtInfo.Common.GeoJson;
using Neptune.Web.Common;
using Neptune.Web.Controllers;

namespace Neptune.Web.Models
{
    public static class ModeledCatchmentModelExtensions
    {
        public static readonly UrlTemplate<int> DetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<DelineationUploadController>.BuildUrlFromExpression(t => t.Detail(UrlTemplate.Parameter1Int)));
        public static readonly UrlTemplate<int> DetailJurisdictionUrlTemplate = new UrlTemplate<int>(SitkaRoute<JurisdictionController>.BuildUrlFromExpression(t => t.Detail(UrlTemplate.Parameter1Int)));
        public static readonly UrlTemplate<int> EditUrlTemplate = new UrlTemplate<int>(SitkaRoute<DelineationUploadController>.BuildUrlFromExpression(t => t.Edit(UrlTemplate.Parameter1Int)));
        public static readonly UrlTemplate<int> MapSummaryUrlTemplate = new UrlTemplate<int>(SitkaRoute<DelineationUploadController>.BuildUrlFromExpression(t => t.SummaryForMap(UrlTemplate.Parameter1Int)));
        public static readonly UrlTemplate<int> DeleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<DelineationUploadController>.BuildUrlFromExpression(t => t.Delete(UrlTemplate.Parameter1Int)));

        public static string GetDetailUrl(this ModeledCatchment modeledCatchment)
        {
            if (modeledCatchment == null) { return ""; }
            return DetailUrlTemplate.ParameterReplace(modeledCatchment.ModeledCatchmentID);
        }

        public static string GetJurisdictionSummaryUrl(this ModeledCatchment modeledCatchment)
        {
            if (modeledCatchment == null) { return ""; }
            return DetailJurisdictionUrlTemplate.ParameterReplace(modeledCatchment.StormwaterJurisdictionID);
        }
       
        public static string GetEditUrl(this ModeledCatchment modeledCatchment)
        {
            return EditUrlTemplate.ParameterReplace(modeledCatchment.ModeledCatchmentID);
        }

        public static string GetMapSummaryUrl(this ModeledCatchment modeledCatchment)
        {
            return MapSummaryUrlTemplate.ParameterReplace(modeledCatchment.ModeledCatchmentID);
        }

        public static string GetDeleteUrl(this ModeledCatchment bmpRegistration)
        {
            return DeleteUrlTemplate.ParameterReplace(bmpRegistration.ModeledCatchmentID);
        }

        public static GeoJSON.Net.Feature.FeatureCollection ToGeoJsonFeatureCollection(this IEnumerable<ModeledCatchment> modeledCatchments)
        {
            var featureCollection = new GeoJSON.Net.Feature.FeatureCollection();
            featureCollection.Features.AddRange(modeledCatchments.Where(x => x.ModeledCatchmentGeometry != null).Select(x =>
            {
                var feature = DbGeometryToGeoJsonHelper.FromDbGeometry(x.ModeledCatchmentGeometry);
                feature.Properties.Add("ModeledCatchmentID", x.ModeledCatchmentID);
                feature.Properties.Add("Name", x.ModeledCatchmentName);
                feature.Properties.Add("Notes", x.Notes);
                feature.Properties.Add("FeatureWeight", 1);
                feature.Properties.Add("FillPolygon", true);
                feature.Properties.Add("FeatureColor", "#405d74");
                feature.Properties.Add("FillOpacity", "0.2");
                feature.Properties.Add("MapSummaryUrl", x.GetMapSummaryUrl());
                return feature;
            }));
            return featureCollection;
        }

        public static GeoJSON.Net.Feature.FeatureCollection ToGeoJsonFeatureCollectionGeneric(this IEnumerable<ModeledCatchment> modeledCatchments)
        {
            var featureCollection = new GeoJSON.Net.Feature.FeatureCollection();
            featureCollection.Features.AddRange(modeledCatchments.Where(x => x.ModeledCatchmentGeometry != null).Select(x =>
            {
                var feature = DbGeometryToGeoJsonHelper.FromDbGeometry(x.ModeledCatchmentGeometry);
                feature.Properties.Add("ModeledCatchmentID", x.ModeledCatchmentID);
                feature.Properties.Add("Name", x.ModeledCatchmentName);
                feature.Properties.Add("Notes", x.Notes);
                feature.Properties.Add("FeatureWeight", 1);
                feature.Properties.Add("FillPolygon", true);
                feature.Properties.Add("FillOpacity", "0.2");
                feature.Properties.Add("FeatureColor", "#405d74");
                return feature;
            }));
            return featureCollection;
        }

    }
}
