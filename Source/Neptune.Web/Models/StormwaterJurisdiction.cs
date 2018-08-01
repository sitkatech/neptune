/*-----------------------------------------------------------------------
<copyright file="StormwaterJurisdiction.cs" company="Tahoe Regional Planning Agency">
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
using LtInfo.Common.Views;

namespace Neptune.Web.Models
{
    public partial class StormwaterJurisdiction : IAuditableEntity
    {
        public string GetAuditDescriptionString()
        {
            var organizationName = ViewUtilities.NotFoundString;
            if (this.Organization != null)
            {
                organizationName = Organization.OrganizationName;
            }

            return "Organization: " + organizationName;
        }

        public string OrganizationDisplayName => IsTransportationJurisdiction ? Organization.OrganizationShortName : Organization.OrganizationName;

        public static GeoJSON.Net.Feature.FeatureCollection ToGeoJsonFeatureCollection(List<StormwaterJurisdiction> stormwaterJurisdictions)
        {
            var featureCollection = new GeoJSON.Net.Feature.FeatureCollection();
            featureCollection.Features.AddRange(stormwaterJurisdictions.Select(MakeFeatureWithRelevantProperties).ToList());
            return featureCollection;
        }

        private static GeoJSON.Net.Feature.Feature MakeFeatureWithRelevantProperties(StormwaterJurisdiction stormwaterJurisdiction)
        {
            var feature = DbGeometryToGeoJsonHelper.FromDbGeometry(stormwaterJurisdiction.StormwaterJurisdictionGeometry);
            feature.Properties.Add("State", stormwaterJurisdiction.StateProvince.StateProvinceAbbreviation);
            feature.Properties.Add("County/City", stormwaterJurisdiction.Organization.OrganizationName);
            return feature;
        }

    }
}
