using Neptune.API.Util;

namespace Neptune.EFModels.Entities
{
    public partial class Delineation
    {
        public string Geometry4326GeoJson => GeoJsonHelpers.GetGeoJsonFromGeometry(DelineationGeometry4326);
    }
}