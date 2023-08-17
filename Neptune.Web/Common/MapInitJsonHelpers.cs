using Microsoft.EntityFrameworkCore;
using Neptune.EFModels.Entities;
using Neptune.Web.Models;

namespace Neptune.Web.Common
{
    public static class MapInitJsonHelpers
    {
        public const string CountyCityLayerName = "Jurisdictions";

        public static IEnumerable<LayerGeoJson> GetJurisdictionMapLayers(NeptuneDbContext dbContext)
        {
            var layerGeoJsons = new List<LayerGeoJson>();
            var jurisdictions = dbContext.StormwaterJurisdictionGeometries.AsNoTracking()
                .Select(x => x.StormwaterJurisdiction).ToList();
            var geoJsonForJurisdictions = StormwaterJurisdictionModelExtensions.ToGeoJsonFeatureCollection(jurisdictions);
            layerGeoJsons.Add(new LayerGeoJson(CountyCityLayerName, geoJsonForJurisdictions, "#FF6C2D", 0f, LayerInitialVisibility.Hide));
            return layerGeoJsons;
        }
    }
}