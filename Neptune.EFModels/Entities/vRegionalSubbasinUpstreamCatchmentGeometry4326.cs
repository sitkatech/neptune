using Neptune.Common.GeoSpatial;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities
{
    public partial class vRegionalSubbasinUpstreamCatchmentGeometry4326
    {
        public string GetUpstreamCatchGeometry4326GeoJson()
        {
            var attributesTable = new AttributesTable();

            var feature = new Feature(UpstreamCatchmentGeometry4326, attributesTable);
            return GeoJsonSerializer.Serialize(feature);
        }
    }
}