using Neptune.Common.GeoSpatial;

namespace Neptune.EFModels.Entities
{
    public partial class vRegionalSubbasinUpstreamCatchmentGeometry4326
    {
        public string UpstreamCatchGeometry4326GeoJson => GeoJsonSerializer.SerializeGeometryToGeoJsonString(UpstreamCatchmentGeometry4326);
    }
}