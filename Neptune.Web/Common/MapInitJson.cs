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

using Neptune.Models.DataTransferObjects;

namespace Neptune.Web.Common
{
    public class MapInitJson
    {
        protected internal const int DefaultZoomLevel = 10;

        public string MapDivID { get; set; }
        public string LayerControlClass { get; set; }
        public BoundingBoxDto BoundingBox { get; set; }
        public int ZoomLevel { get; set; }
        public List<LayerGeoJson> Layers { get; set; }
        public bool TurnOnFeatureIdentify { get; set; }
        public bool AllowFullScreen { get; set; } = true;

        public MapInitJson()
        {

        }

        public MapInitJson(string mapDivID, int zoomLevel, List<LayerGeoJson> layers, BoundingBoxDto boundingBox, bool turnOnFeatureIdentify)
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
        public MapInitJson(string mapDivID, int zoomLevel, List<LayerGeoJson> layers, BoundingBoxDto boundingBox) : this(mapDivID, zoomLevel, layers, boundingBox, true)
        {
        }
    }
}
