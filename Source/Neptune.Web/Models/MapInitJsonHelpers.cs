using System.Collections.Generic;
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
            var jurisdictions = HttpRequestStorage.DatabaseEntities.AllStormwaterJurisdictions.GetJurisdictionsWithGeospatialFeatures();
            var geoJsonForJurisdictions = StormwaterJurisdiction.ToGeoJsonFeatureCollection(jurisdictions);
            layerGeoJsons.Add(new LayerGeoJson(CountyCityLayerName, geoJsonForJurisdictions, "#FF6C2D", 0m, LayerInitialVisibility.Hide));
            return layerGeoJsons;
        }

        public static IEnumerable<LayerGeoJson> GetParcelMapLayers(TenantAttribute tenantAttribute, LayerInitialVisibility layerInitialVisibility)
        {
            if (!string.IsNullOrWhiteSpace(NeptuneWebConfiguration.ParcelMapServiceUrl))
            {
                yield return ParcelModelExtensions.GetParcelWmsLayerGeoJson("#dddddd", 0.1m, layerInitialVisibility,
                    tenantAttribute);
            }
            else
            {
                var parcels = HttpRequestStorage.DatabaseEntities.Parcels.ToList();
                if (parcels.Any())
                {
                    yield return new LayerGeoJson(FieldDefinition.Parcel.GetFieldDefinitionLabelPluralized(),
                        parcels.ToGeoJsonFeatureCollection(),
                        "#dddddd",
                        0.1m,
                        layerInitialVisibility);
                }
            }
        }
    }
}