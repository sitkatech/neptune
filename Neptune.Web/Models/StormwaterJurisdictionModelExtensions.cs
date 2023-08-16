/*-----------------------------------------------------------------------
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

using Neptune.Web.Common;
using Microsoft.AspNetCore.Html;
using Microsoft.EntityFrameworkCore;
using Neptune.EFModels.Entities;
using NetTopologySuite.Features;

namespace Neptune.Web.Models
{
    public static class StormwaterJurisdictionModelExtensions
    {
        public static string GetDetailUrl(this StormwaterJurisdiction stormwaterJurisdiction)
        {
            if (stormwaterJurisdiction == null) { return ""; }

            return ""; //todo: DetailUrlTemplate.ParameterReplace(stormwaterJurisdiction.StormwaterJurisdictionID);
        }

        public static HtmlString GetDisplayNameAsDetailUrl(this StormwaterJurisdiction stormwaterJurisdiction)
        {
            return new HtmlString(
                $"<a href=\"{stormwaterJurisdiction.GetDetailUrl()}\" alt=\"{stormwaterJurisdiction.GetOrganizationDisplayName()}\" title=\"{stormwaterJurisdiction.GetOrganizationDisplayName()}\" >{stormwaterJurisdiction.GetOrganizationDisplayName()}</a>");
        }

        public static List<Person> PeopleWhoCanManageStormwaterJurisdiction(this StormwaterJurisdiction stormwaterJurisdiction)
        {
            return stormwaterJurisdiction.StormwaterJurisdictionPeople.Select(x => x.Person).ToList();
        }

        public static List<Person> PeopleWhoCanManageStormwaterJurisdictionExceptSitka(this StormwaterJurisdiction stormwaterJurisdiction)
        {
            return stormwaterJurisdiction.PeopleWhoCanManageStormwaterJurisdiction().ToList();
        }
        
        public static List<LayerGeoJson> GetBoundaryLayerGeoJson(NeptuneDbContext dbContext, bool clickThrough)
        {
            var jurisdictionsToShow = dbContext.StormwaterJurisdictionGeometries.AsNoTracking()
                .Include(x => x.StormwaterJurisdiction)
                .ThenInclude(x => x.Organization).ThenInclude(x => x.OrganizationType).ToList();

            return jurisdictionsToShow.GroupBy(x => x.StormwaterJurisdiction.Organization.OrganizationType, (organizationType, stormwaterJurisdictionGeometries) =>
            {
                var featureCollection = new FeatureCollection();
                foreach (var stormwaterJurisdictionGeometry in stormwaterJurisdictionGeometries)
                {
                    var stormwaterJurisdiction = stormwaterJurisdictionGeometry.StormwaterJurisdiction;
                    var organizationAnchorTag = UrlTemplate.MakeHrefString(stormwaterJurisdiction.GetDetailUrl(), stormwaterJurisdiction.Organization.OrganizationName);
                    var attributesTable = new AttributesTable
                    {
                        { "Organization Name", organizationAnchorTag },
                        { "Short Name", organizationAnchorTag },
                        { "Target URL", stormwaterJurisdiction.GetDetailUrl() }
                    };
                    var feature = new Feature(stormwaterJurisdictionGeometry.Geometry4326, attributesTable);
                    featureCollection.Add(feature);
                }
                return new LayerGeoJson(
                    $"{organizationType.OrganizationTypeName} {FieldDefinitionType.Jurisdiction.GetFieldDefinitionLabelPluralized()}",
                    featureCollection,
                    organizationType.LegendColor, 1,
                    LayerInitialVisibility.Show,
                    clickThrough);
            }).ToList();
        }

        public static FeatureCollection ToGeoJsonFeatureCollection(List<StormwaterJurisdiction> stormwaterJurisdictions)
        {
            var featureCollection = new FeatureCollection();
            foreach (var stormwaterJurisdiction in stormwaterJurisdictions)
            {
                featureCollection.Add(MakeFeatureWithRelevantProperties(stormwaterJurisdiction));
            }
            return featureCollection;
        }

        private static Feature MakeFeatureWithRelevantProperties(StormwaterJurisdiction stormwaterJurisdiction)
        {
            var attributesTable = new AttributesTable
            {
                { "State", stormwaterJurisdiction.StateProvince.StateProvinceAbbreviation },
                { "County/City", stormwaterJurisdiction.Organization.OrganizationName },
                { "StormwaterJurisdictionID", stormwaterJurisdiction.StormwaterJurisdictionID }
            };
            return new Feature(stormwaterJurisdiction.StormwaterJurisdictionGeometry.Geometry4326, attributesTable);
        }


        //public static string GetGeoserverRequestUrl(this StormwaterJurisdiction stormwaterJurisdiction,
        //    OnlandVisualTrashAssessmentExportTypeEnum exportType)
        //{
        //    string typeName;

        //    switch (exportType)
        //    {
        //        case OnlandVisualTrashAssessmentExportTypeEnum.ExportAreas:
        //            typeName = "OCStormwater:AssessmentAreaExport";
        //            break;
        //        case OnlandVisualTrashAssessmentExportTypeEnum.ExportTransects:
        //            typeName = "OCStormwater:TransectLineExport";
        //            break;
        //        case OnlandVisualTrashAssessmentExportTypeEnum.ExportObservationPoints:
        //            typeName = "OCStormwater:ObservationPointExport";
        //            break;
        //        default:
        //            throw new ArgumentOutOfRangeException(nameof(exportType), exportType, null);
        //    }

        //    var cqlFilter = $"JurisID={stormwaterJurisdiction.StormwaterJurisdictionID}";

        //    var parameters = new
        //    {
        //        service = "WFS",
        //        version = "1.0.0",
        //        request = "GetFeature",
        //        typeName,
        //        outputFormat = "shape-zip",
        //        cql_filter = cqlFilter
        //    };

        //    return $"{NeptuneWebConfiguration.ParcelMapServiceUrl}?{NeptuneHelpers.GetQueryString(parameters)}";
        //}
    }
}
