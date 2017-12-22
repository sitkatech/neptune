/*-----------------------------------------------------------------------
<copyright file="MapInitJson.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using System.Collections.Generic;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class MapInitJson
    {
        public const int CoordinateSystemId = 4326;
        public const string CountyCityLayerName = "Jurisdictions";
        protected const int DefaultZoomLevel = 10;

        public string MapDivID;
        public BoundingBox BoundingBox;
        public int ZoomLevel;
        public List<LayerGeoJson> Layers;
        public readonly bool TurnOnFeatureIdentify;
        public bool AllowFullScreen = true;

        public MapInitJson(string mapDivID, int zoomLevel, List<LayerGeoJson> layers, BoundingBox boundingBox, bool turnOnFeatureIdentify)
        {
            MapDivID = mapDivID;
            ZoomLevel = zoomLevel;
            Layers = layers;
            BoundingBox = boundingBox;
            TurnOnFeatureIdentify = turnOnFeatureIdentify;
        }

        /// <summary>
        /// Summary maps with no editing should use this constructor
        /// </summary>
        public MapInitJson(string mapDivID, int zoomLevel, List<LayerGeoJson> layers, BoundingBox boundingBox) : this(mapDivID, zoomLevel, layers, boundingBox, true)
        {
        }

        public static List<LayerGeoJson> GetJurisdictionMapLayers()
        {
            var layerGeoJsons = new List<LayerGeoJson>();
            var jurisdictions = HttpRequestStorage.DatabaseEntities.AllStormwaterJurisdictions.GetJurisdictionsWithGeospatialFeatures();
            var geoJsonForJurisdictions = StormwaterJurisdiction.ToGeoJsonFeatureCollection(jurisdictions);

            layerGeoJsons.Add(new LayerGeoJson(CountyCityLayerName, geoJsonForJurisdictions, "#FF6C2D", 0m, LayerInitialVisibility.Hide));
            
            return layerGeoJsons;
        }
    }
}
