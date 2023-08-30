using Microsoft.EntityFrameworkCore;
using Neptune.EFModels.Entities;
using Neptune.Web.Models;

namespace Neptune.Web.Common
{
    public static class MapInitJsonHelpers
    {
        public const string CountyCityLayerName = "Jurisdictions";

        public static List<LayerGeoJson> GetJurisdictionMapLayers(NeptuneDbContext dbContext)
        {
            var layerGeoJsons = new List<LayerGeoJson>();
            var jurisdictions = dbContext.StormwaterJurisdictions.AsNoTracking()
                .Include(x => x.StormwaterJurisdictionGeometry)
                .Include(x => x.StateProvince)
                .Include(x => x.Organization)
                .Where(x => x.StormwaterJurisdictionGeometry != null)
                .ToList();
            var geoJsonForJurisdictions = StormwaterJurisdictionModelExtensions.ToGeoJsonFeatureCollection(jurisdictions);
            layerGeoJsons.Add(new LayerGeoJson(CountyCityLayerName, geoJsonForJurisdictions, "#FF6C2D", 0f, LayerInitialVisibility.Hide));
            return layerGeoJsons;
        }
    }
}