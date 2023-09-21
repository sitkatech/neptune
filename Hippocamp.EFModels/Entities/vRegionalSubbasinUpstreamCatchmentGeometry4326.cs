using Hippocamp.API.Util;

namespace Hippocamp.EFModels.Entities
{
    public partial class vRegionalSubbasinUpstreamCatchmentGeometry4326
    {
        public string UpstreamCatchGeometry4326GeoJson => GeoJsonHelpers.GetGeoJsonFromGeometry(UpstreamCatchmentGeometry4326);
    }
}