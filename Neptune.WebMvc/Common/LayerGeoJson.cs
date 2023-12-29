/*-----------------------------------------------------------------------
<copyright file="LayerGeoJson.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
Copyright (c) Tahoe Regional Planning Agency and Sitka Technology Group. All rights reserved.
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

using System.Drawing;
using NetTopologySuite.Features;

namespace Neptune.WebMvc.Common
{
    /// <summary>
    /// example: Jurisdiction layer or Road layer
    /// </summary>
    public class LayerGeoJson
    {
        public string LayerName { get; set; }
        public FeatureCollection GeoJsonFeatureCollection { get; set; }
        public string MapServerUrl { get; set; }
        public string MapServerLayerName { get; set; }
        public string TooltipUrlTemplate { get; set; }
        public string LayerColor { get; set; }
        public double LayerOpacity { get; set; }
        public LayerInitialVisibility LayerInitialVisibility { get; set; }
        public LayerGeoJsonType LayerType { get; set; }
        public bool HasCustomPopups { get; set; }
        public bool HasClickThrough { get; set; }
        public bool EnablePopups { get; set; } = true;

        public LayerGeoJson(string layerName, FeatureCollection geoJsonFeatureCollection, string layerColor,
            double layerOpacity, LayerInitialVisibility layerInitialVisibility) : this(layerName,
            geoJsonFeatureCollection, layerColor, layerOpacity, layerInitialVisibility, false)
        {
        }

        /// <summary>
        /// Constructor for LayerGeoJson with Vector Type
        /// </summary>
        public LayerGeoJson(string layerName, FeatureCollection geoJsonFeatureCollection, string layerColor, double layerOpacity, LayerInitialVisibility layerInitialVisibility, bool clickThrough)
        {
            LayerName = layerName;
            GeoJsonFeatureCollection = geoJsonFeatureCollection;
            LayerColor = layerColor.StartsWith("#") ? layerColor : GetColorString(layerColor);
            LayerOpacity = layerOpacity;
            LayerInitialVisibility = layerInitialVisibility;
            LayerType = LayerGeoJsonType.Vector;
            HasCustomPopups = geoJsonFeatureCollection.Any(x => x.Attributes.GetNames().Any(y => y == "PopupUrl"));
            HasClickThrough = clickThrough;
        }
        /// <summary>
        /// Constructor for LayerGeoJson with WMS Type
        /// </summary>
        public LayerGeoJson(string layerName, string mapServerUrl, string mapServerLayerName, string tooltipUrlTemplate, string layerColor, double layerOpacity, LayerInitialVisibility layerInitialVisibility)
        {
            LayerName = layerName;
            MapServerUrl = mapServerUrl;
            MapServerLayerName = mapServerLayerName;
            TooltipUrlTemplate = tooltipUrlTemplate;
            LayerColor = layerColor;
            LayerOpacity = layerOpacity;
            LayerInitialVisibility = layerInitialVisibility;
            LayerType = LayerGeoJsonType.Wms;
        }

        /// <summary>
        /// Needed by Serializer
        /// </summary>
        public LayerGeoJson()
        {
        }

        private static string GetColorString(string colorName)
        {
            var color = Color.FromName(colorName);
            return $"#{color.R:x2}{color.G:x2}{color.B:x2}";
        }
    }
}
