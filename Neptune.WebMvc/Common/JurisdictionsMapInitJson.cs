﻿/*-----------------------------------------------------------------------
<copyright file="JurisdictionsMapInitJson.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

namespace Neptune.WebMvc.Common
{
    public class JurisdictionsMapInitJson : MapInitJson
    {
        // Needed by serializer
        public JurisdictionsMapInitJson()
        {
        }

        public JurisdictionsMapInitJson(string mapDivID)
            : base(mapDivID, DefaultZoomLevel, new List<LayerGeoJson>(), new BoundingBoxDto())
        {
        }
    }
}
