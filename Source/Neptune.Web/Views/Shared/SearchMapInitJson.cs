﻿/*-----------------------------------------------------------------------
<copyright file="StormwaterSearchMapInitJson.cs" company="Tahoe Regional Planning Agency">
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
using Neptune.Web.Models;

namespace Neptune.Web.Views.Shared
{
    public class SearchMapInitJson : MapInitJson
    {
        public readonly LayerGeoJson SearchableLayerGeoJson;
        public readonly LayerGeoJson JurisdictionLayerGeoJson;

        public SearchMapInitJson(string mapDivID, LayerGeoJson searchableLayerGeoJson)
            : base(mapDivID, DefaultZoomLevel, MapInitJsonHelpers.GetJurisdictionMapLayers().ToList(),
                BoundingBox.MakeNewDefaultBoundingBox())
        {
            SearchableLayerGeoJson = searchableLayerGeoJson;
            JurisdictionLayerGeoJson = Layers.Single(x => x.LayerName == MapInitJsonHelpers.CountyCityLayerName);
        }

        public SearchMapInitJson(string mapDivID, List<LayerGeoJson> layers, LayerGeoJson searchableLayerGeoJson)
            : base(mapDivID, DefaultZoomLevel, layers, BoundingBox.MakeNewDefaultBoundingBox())
        {
            SearchableLayerGeoJson = searchableLayerGeoJson;
        }

        public SearchMapInitJson(string mapDivID, int zoomLevel, List<LayerGeoJson> layers, BoundingBox boundingBox, LayerGeoJson searchableLayerGeoJson)
            : base(mapDivID, zoomLevel, layers, boundingBox)
        {
            SearchableLayerGeoJson = searchableLayerGeoJson;
        }
    }
}
