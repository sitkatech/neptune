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

using Neptune.WebMvc.Common;
using Microsoft.EntityFrameworkCore;
using Neptune.EFModels.Entities;
using Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessmentExport;
using Neptune.WebMvc.Controllers;
using NetTopologySuite.Features;

namespace Neptune.WebMvc.Models
{
    public static class StormwaterJurisdictionModelExtensions
    {
        public static LayerGeoJson GetBoundaryLayerGeoJson(NeptuneDbContext dbContext, bool clickThrough, LinkGenerator linkGenerator)
        {
            // all jurisdictions are Organization Type "Local"
            var stormwaterJurisdictionGeometries = dbContext.StormwaterJurisdictionGeometries.AsNoTracking()
                .Include(x => x.StormwaterJurisdiction)
                .ThenInclude(x => x.Organization).ThenInclude(x => x.OrganizationType).ToList();

            var featureCollection = new FeatureCollection();
            foreach (var stormwaterJurisdictionGeometry in stormwaterJurisdictionGeometries)
            {
                var stormwaterJurisdiction = stormwaterJurisdictionGeometry.StormwaterJurisdiction;
                var attributesTable = new AttributesTable
                {
                    { "Short Name", stormwaterJurisdiction.Organization.OrganizationName },
                    { "Target URL", SitkaRoute<JurisdictionController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(stormwaterJurisdiction)) }
                };
                var feature = new Feature(stormwaterJurisdictionGeometry.Geometry4326, attributesTable);
                featureCollection.Add(feature);
            }

            var organizationType = stormwaterJurisdictionGeometries.First().StormwaterJurisdiction.Organization.OrganizationType;
            return new LayerGeoJson(
                $"{organizationType.OrganizationTypeName} {FieldDefinitionType.Jurisdiction.GetFieldDefinitionLabelPluralized()}",
                featureCollection,
                organizationType.LegendColor, 1,
                LayerInitialVisibility.Show,
                clickThrough);
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
                { "County/City", stormwaterJurisdiction.Organization.OrganizationName },
                { "StormwaterJurisdictionID", stormwaterJurisdiction.StormwaterJurisdictionID }
            };
            return new Feature(stormwaterJurisdiction.StormwaterJurisdictionGeometry.Geometry4326, attributesTable);
        }

        public static string GetGeoserverRequestUrl(this StormwaterJurisdiction stormwaterJurisdiction,
            OnlandVisualTrashAssessmentExportTypeEnum exportType, string mapServiceUrl)
        {
            string typeName;

            switch (exportType)
            {
                case OnlandVisualTrashAssessmentExportTypeEnum.ExportAreas:
                    typeName = "OCStormwater:AssessmentAreaExport";
                    break;
                case OnlandVisualTrashAssessmentExportTypeEnum.ExportTransects:
                    typeName = "OCStormwater:TransectLineExport";
                    break;
                case OnlandVisualTrashAssessmentExportTypeEnum.ExportObservationPoints:
                    typeName = "OCStormwater:ObservationPointExport";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(exportType), exportType, null);
            }

            var cqlFilter = $"JurisID={stormwaterJurisdiction.StormwaterJurisdictionID}";

            var parameters = new
            {
                service = "WFS",
                version = "1.0.0",
                request = "GetFeature",
                typeName,
                outputFormat = "shape-zip",
                cql_filter = cqlFilter
            };

            return $"{mapServiceUrl}?{NeptuneHelpers.GetQueryString(parameters)}";
        }
    }
}
