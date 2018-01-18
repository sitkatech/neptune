﻿/*-----------------------------------------------------------------------
<copyright file="StormwaterJurisdictionModelExtensions.cs" company="Tahoe Regional Planning Agency">
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
using GeoJSON.Net.Feature;
using LtInfo.Common;
using LtInfo.Common.GeoJson;
using Neptune.Web.Common;
using Neptune.Web.Controllers;

namespace Neptune.Web.Models
{
    public static class StormwaterJurisdictionModelExtensions
    {
        public static readonly UrlTemplate<int> DetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<JurisdictionController>.BuildUrlFromExpression(t => t.Detail(UrlTemplate.Parameter1Int)));        
        public static string GetDetailUrl(this StormwaterJurisdiction stormwaterJurisdiction)
        {
            if (stormwaterJurisdiction == null) { return ""; }
            return DetailUrlTemplate.ParameterReplace(stormwaterJurisdiction.StormwaterJurisdictionID);
        }

        public static HtmlString GetDisplayNameAsDetailUrl(this StormwaterJurisdiction stormwaterJurisdiction)
        {
            return new HtmlString(
                $"<a href=\"{stormwaterJurisdiction.GetDetailUrl()}\" alt=\"{stormwaterJurisdiction.OrganizationDisplayName}\" title=\"{stormwaterJurisdiction.OrganizationDisplayName}\" >{stormwaterJurisdiction.OrganizationDisplayName}</a>");
        }

        public static List<Person> PeopleWhoCanManageStormwaterJurisdiction(this StormwaterJurisdiction stormwaterJurisdiction)
        {
            return stormwaterJurisdiction.StormwaterJurisdictionPeople.Select(x => x.Person).ToList();
        }

        public static List<Person> PeopleWhoCanManageStormwaterJurisdictionExceptSitka(this StormwaterJurisdiction stormwaterJurisdiction)
        {
            return stormwaterJurisdiction.PeopleWhoCanManageStormwaterJurisdiction().ToList();
        }
        
        public static List<LayerGeoJson> GetBoundaryLayerGeoJson(this IEnumerable<StormwaterJurisdiction> jurisdictions, bool clickThrough)
        {
            var jurisdictionsToShow =
                jurisdictions?.Where(x => x.StormwaterJurisdictionGeometry != null)
                    .ToList();
            if (jurisdictionsToShow == null || !jurisdictionsToShow.Any())
            {
                return new List<LayerGeoJson>();
            }


            return jurisdictionsToShow.GroupBy(x => x.Organization.OrganizationType, (organizationType, jurisdictionList) =>
            {
                return new LayerGeoJson(
                    $"{organizationType.OrganizationTypeName} {FieldDefinition.StormwaterJurisdiction.GetFieldDefinitionLabelPluralized()}",
                    new FeatureCollection(jurisdictionList.Select(jurisdiction =>
                    {
                        var feature = DbGeometryToGeoJsonHelper.FromDbGeometry(jurisdiction.StormwaterJurisdictionGeometry);
                        feature.Properties.Add("Organization Name", UrlTemplate.MakeHrefString(jurisdiction.GetDetailUrl(), jurisdiction.Organization.OrganizationName).ToHtmlString());
                        feature.Properties.Add("Short Name", UrlTemplate.MakeHrefString(jurisdiction.GetDetailUrl(), jurisdiction.Organization.OrganizationName).ToHtmlString());
                        feature.Properties.Add("Target URL", jurisdiction.GetDetailUrl());
                        return feature;
                    }).ToList()),
                    organizationType.LegendColor, 1,
                    LayerInitialVisibility.Show,
                    clickThrough);
            }).ToList();
        }
    }
}
