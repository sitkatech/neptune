﻿using System.Collections.Generic;
using System.Linq;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public static class MapInitJsonHelpers
    {
        public const string CountyCityLayerName = "Jurisdictions";

        public static IEnumerable<LayerGeoJson> GetJurisdictionMapLayers()
        {
            var layerGeoJsons = new List<LayerGeoJson>();
            var jurisdictions = HttpRequestStorage.DatabaseEntities.StormwaterJurisdictionGeometries
                .Select(x => x.StormwaterJurisdiction).ToList();
            var geoJsonForJurisdictions = StormwaterJurisdiction.ToGeoJsonFeatureCollection(jurisdictions);
            layerGeoJsons.Add(new LayerGeoJson(CountyCityLayerName, geoJsonForJurisdictions, "#FF6C2D", 0m, LayerInitialVisibility.Hide));
            return layerGeoJsons;
        }
    }
}