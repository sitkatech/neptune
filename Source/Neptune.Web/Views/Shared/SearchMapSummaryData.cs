/*-----------------------------------------------------------------------
<copyright file="StormwaterSearchMapSummaryData.cs" company="Tahoe Regional Planning Agency">
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
using LtInfo.Common.GeoJson;
using System.Data.Entity.Spatial;

namespace Neptune.Web.Views.Shared
{
    public class SearchMapSummaryData
    {
        public readonly string MapSummaryUrl;
        public readonly Feature GeometryJson;
        public readonly double? Latitude;
        public readonly double? Longitude;
        public readonly int EntityID;

        public SearchMapSummaryData(string mapSummaryUrl, DbGeometry geometry, double? latitude, double? longitude, int entityID)
        {
            MapSummaryUrl = mapSummaryUrl;
            GeometryJson = DbGeometryToGeoJsonHelper.FromDbGeometryWithReprojectionCheck(geometry);
            Latitude = latitude;
            Longitude = longitude;
            EntityID = entityID;
        }
    }
}
